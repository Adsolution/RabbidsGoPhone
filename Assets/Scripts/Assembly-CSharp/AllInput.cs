using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;

public class AllInput : MonoBehaviour
{
	public enum EState
	{
		Began = 0,
		Stationary = 1,
		Moved = 2,
		Leave = 3,
		None = 4
	}

	public enum EScreenOrientation
	{
		Unknown = 0,
		Portrait = 1,
		PortraitUpsideDown = 2,
		LandscapeLeft = 3,
		Landscape = 3,
		LandscapeRight = 4,
		Count = 5
	}

	public struct SInputs
	{
		public EState State;

		public int xScreenPos;

		public int yScreenPos;

		public int FingerID;
	}

	public struct SExtra
	{
		public Vector3 guiPos;

		public bool guiPosComputed;

		public Vector3 guiDelta;

		public bool guiDeltaComputed;

		public Vector2 raycastPos;

		public bool raycastPosComputed;

		public Vector3 worldPos;

		public bool worldPosComputed;

		public int deltaX;

		public bool deltaXComputed;

		public int deltaY;

		public bool deltaYComputed;
	}

	public enum ECameraState
	{
		Started = 0,
		InPause = 1,
		Stopped = 2
	}

	public enum EMessageType
	{
		Touch = 0,
		Acceleration = 1,
		Orientation = 2,
		CameraCapture = 3,
		Count = 4
	}

	public struct SMessageStuff
	{
		public double LastTime;
	}

	public struct SMessageTouch
	{
		public SInputs[] Inputs;

		public double Time;
	}

	public struct SMessageAcceleration
	{
		public Vector3 Acceleration;

		public double Time;
	}

	public struct SMessageOrientation
	{
		public DeviceOrientation Orientation;

		public double Time;
	}

	public struct SMessageCameraCapture
	{
		public Texture2D Texture;

		public double Time;
	}

	public delegate void InputHandler();

	protected const int m_MaxMouseButton = 3;

	protected const int m_MaxTouch = 11;

	private const int m_Connections = 8;

	private const int m_Port = 25000;

	private float m_ScreenDPI = 240f;

	private float m_FingerTolerance;

	protected int m_TouchCount;

	protected SInputs[] m_Datas;

	protected SExtra[] m_ExtraDatas;

	protected Vector3 m_Acceleration = Vector3.zero;

	protected float m_Roll;

	protected DeviceOrientation m_DeviceOrientation = DeviceOrientation.Portrait;

	protected DeviceOrientation m_LastValidDeviceOrientation = DeviceOrientation.Portrait;

	protected DeviceOrientation m_LastValidPortraitOrientation = DeviceOrientation.Portrait;

	private int m_PreviousTouchCount;

	private SInputs[] m_PreviousDatas;

	private SExtra[] m_PreviousExtraDatas;

	private float m_XRatio = 1f;

	private float m_YRatio = 1f;

	private bool m_IsMicroAvailable;

	private static AllInput s_Instance = null;

	private static bool[] s_AutoRotateMem = new bool[4];

	protected ECameraState m_CameraState = ECameraState.Stopped;

	protected bool m_UseFrontCamera;

	protected WebCamTexture m_WebCamTexture;

	protected string m_FrontCamera = string.Empty;

	protected string m_BackCamera = string.Empty;

	protected List<Material> m_MaterialsCapture = new List<Material>();

	protected Material m_LastMaterial;

	protected bool m_CameraAvailable;

	protected bool m_HasPositionChoice;

	private List<SMessageTouch> m_MessagesTouch = new List<SMessageTouch>();

	private List<SMessageAcceleration> m_MessagesAcceleration = new List<SMessageAcceleration>();

	private List<SMessageOrientation> m_MessagesOrientation = new List<SMessageOrientation>();

	private List<SMessageCameraCapture> m_MessagesCameraCapture = new List<SMessageCameraCapture>();

	private SMessageStuff[] m_MessageStuff = new SMessageStuff[4];

	private int m_FrameCnt;

	public int m_TouchPeriod = 1;

	public int m_AccelerometerPeriod = 1;

	public int m_CameraCapturePeriod = 60;

	public int m_ScreenPeriod = 10;

	private bool m_ScreenCapture;

	protected static AllInput Instance
	{
		get
		{
			if (s_Instance == null)
			{
				Debug.LogError("AllInput :: Attempt to access instance of MonoBehaviour singleton earlier than Start().");
			}
			return s_Instance;
		}
	}

	public static event InputHandler onInput;

	private void Awake()
	{
		s_Instance = this;
		AR_Awake();
		m_IsMicroAvailable = Microphone.devices != null && Microphone.devices.Length > 0;
		if (m_IsMicroAvailable)
		{
			string text = SystemInfo.deviceModel.ToUpperInvariant();
			if (text.Contains("TF201".ToUpperInvariant()) || text.Contains("TF101".ToUpperInvariant()) || text.Contains("P1000".ToUpperInvariant()) || text.Contains("Xperia P".ToUpperInvariant()) || text.Contains("Kindle".ToUpperInvariant()))
			{
				m_IsMicroAvailable = false;
			}
		}
		Utility.Log(ELog.Device, SystemInfo.deviceModel + " has microphone: " + m_IsMicroAvailable);
		ScreenOrientation orientation = Screen.orientation;
		ScreenOrientation screenOrientation = orientation;
		if (screenOrientation != ScreenOrientation.Portrait && screenOrientation == ScreenOrientation.PortraitUpsideDown)
		{
			m_DeviceOrientation = DeviceOrientation.PortraitUpsideDown;
		}
		else
		{
			m_DeviceOrientation = DeviceOrientation.Portrait;
		}
		m_Datas = new SInputs[11];
		m_ExtraDatas = new SExtra[11];
		m_PreviousDatas = new SInputs[11];
		m_PreviousExtraDatas = new SExtra[11];
		ComputeRatios();
		for (int i = 0; i < 11; i++)
		{
			m_Datas[i].State = EState.None;
		}
		m_FingerTolerance = 3f;
		float num = 0f;
		num = Screen.dpi;
		if (num > 100f)
		{
			m_ScreenDPI = num;
		}
		m_FingerTolerance *= m_ScreenDPI / 125f;
	}

	private void Start()
	{
		s_AutoRotateMem[0] = Screen.autorotateToLandscapeLeft;
		s_AutoRotateMem[1] = Screen.autorotateToLandscapeRight;
		s_AutoRotateMem[2] = Screen.autorotateToPortrait;
		s_AutoRotateMem[3] = Screen.autorotateToPortraitUpsideDown;
	}

	private void Update()
	{
		if (s_Instance == null)
		{
			s_Instance = Instance;
		}
		float roll = m_Roll;
		Vector3 acceleration = m_Acceleration;
		m_PreviousTouchCount = m_TouchCount;
		for (int i = 0; i < m_TouchCount; i++)
		{
			m_PreviousDatas[i] = m_Datas[i];
			m_PreviousExtraDatas[i] = m_ExtraDatas[i];
		}
		ComputeIphonePlatform();
		ComputeLastValidOrientation();
		ResetExtraDatas();
		if (AllInput.onInput != null && (m_TouchCount > 0 || m_Roll != roll || m_Acceleration != acceleration))
		{
			AllInput.onInput();
		}
	}

	private void OnDisable()
	{
		AR_OnDisable();
	}

	public static int GetTouchCount()
	{
		return Instance.m_TouchCount;
	}

	public static int GetFingerID(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			return instance.m_Datas[touchID].FingerID;
		}
		return 0;
	}

	public static EState GetState(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			return instance.m_Datas[touchID].State;
		}
		return EState.None;
	}

	public static Vector3 GetPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			return new Vector3(instance.m_Datas[touchID].xScreenPos, instance.m_Datas[touchID].yScreenPos);
		}
		return Vector3.zero;
	}

	public static float GetXPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			return instance.m_Datas[touchID].xScreenPos;
		}
		return 0f;
	}

	public static float GetYPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			return instance.m_Datas[touchID].yScreenPos;
		}
		return 0f;
	}

	public static float GetDeltaX(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].deltaXComputed)
			{
				instance.m_ExtraDatas[touchID].deltaXComputed = true;
				int fingerID = instance.m_Datas[touchID].FingerID;
				int previousTouchByFinger = GetPreviousTouchByFinger(fingerID);
				if (previousTouchByFinger < 0)
				{
					instance.m_ExtraDatas[touchID].deltaX = 0;
				}
				else
				{
					instance.m_ExtraDatas[touchID].deltaX = instance.m_Datas[touchID].xScreenPos - instance.m_PreviousDatas[previousTouchByFinger].xScreenPos;
				}
			}
			return instance.m_ExtraDatas[touchID].deltaX;
		}
		return 0f;
	}

	public static float GetDeltaY(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].deltaYComputed)
			{
				instance.m_ExtraDatas[touchID].deltaYComputed = true;
				int fingerID = instance.m_Datas[touchID].FingerID;
				int previousTouchByFinger = GetPreviousTouchByFinger(fingerID);
				if (previousTouchByFinger < 0)
				{
					instance.m_ExtraDatas[touchID].deltaY = 0;
				}
				else
				{
					instance.m_ExtraDatas[touchID].deltaY = instance.m_Datas[touchID].yScreenPos - instance.m_PreviousDatas[previousTouchByFinger].yScreenPos;
				}
			}
			return instance.m_ExtraDatas[touchID].deltaY;
		}
		return 0f;
	}

	public static Vector3 GetDelta(int touchID)
	{
		Vector3 zero = Vector3.zero;
		zero.x = GetDeltaX(touchID);
		zero.y = GetDeltaY(touchID);
		return zero;
	}

	public static Vector3 GetGUIPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].guiPosComputed)
			{
				instance.m_ExtraDatas[touchID].guiPosComputed = true;
				instance.m_ExtraDatas[touchID].guiPos.x = GetXPosition(touchID) / instance.m_XRatio;
				instance.m_ExtraDatas[touchID].guiPos.y = (0f - GetYPosition(touchID)) / instance.m_YRatio + (float)Screen.height - 1f;
			}
			return instance.m_ExtraDatas[touchID].guiPos;
		}
		return Vector3.zero;
	}

	public static Vector3 GetGUIDelta(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].guiDeltaComputed)
			{
				instance.m_ExtraDatas[touchID].guiDeltaComputed = true;
				int fingerID = instance.m_Datas[touchID].FingerID;
				int previousTouchByFinger = GetPreviousTouchByFinger(fingerID);
				if (previousTouchByFinger < 0 || !instance.m_PreviousExtraDatas[previousTouchByFinger].guiPosComputed)
				{
					GetGUIPosition(touchID);
					instance.m_ExtraDatas[touchID].guiDelta = Vector3.zero;
				}
				else
				{
					instance.m_ExtraDatas[touchID].guiDelta = GetGUIPosition(touchID) - instance.m_PreviousExtraDatas[previousTouchByFinger].guiPos;
				}
			}
			return instance.m_ExtraDatas[touchID].guiDelta;
		}
		return Vector3.zero;
	}

	public static Vector2 GetRaycastPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].raycastPosComputed)
			{
				instance.m_ExtraDatas[touchID].raycastPosComputed = true;
				instance.m_ExtraDatas[touchID].raycastPos.x = (float)instance.m_Datas[touchID].xScreenPos / instance.m_XRatio;
				instance.m_ExtraDatas[touchID].raycastPos.y = (float)instance.m_Datas[touchID].yScreenPos / instance.m_YRatio;
			}
			return instance.m_ExtraDatas[touchID].raycastPos;
		}
		return Vector2.zero;
	}

	public static Vector3 GetWorldPosition(int touchID)
	{
		AllInput instance = Instance;
		if (touchID < instance.m_TouchCount)
		{
			if (!instance.m_ExtraDatas[touchID].worldPosComputed)
			{
				Camera mainCamera = Camera.main;
				Vector2 raycastPosition = GetRaycastPosition(touchID);
				Vector3 position = new Vector3(raycastPosition.x, raycastPosition.y, mainCamera.farClipPlane);
				instance.m_ExtraDatas[touchID].worldPos = mainCamera.ScreenToWorldPoint(position);
			}
			return instance.m_ExtraDatas[touchID].worldPos;
		}
		return Vector3.zero;
	}

	public static float GetXRatio()
	{
		return Instance.m_XRatio;
	}

	public static float GetYRatio()
	{
		return Instance.m_YRatio;
	}

	public static float GetScreenDPI()
	{
		return Instance.m_ScreenDPI;
	}

	public static float GetRoll()
	{
		return Instance.m_Roll;
	}

	public static Vector3 GetAcceleration()
	{
		return Instance.m_Acceleration;
	}

	public static DeviceOrientation GetDeviceOrientation()
	{
		return Instance.m_DeviceOrientation;
	}

	public static DeviceOrientation GetLastValidDeviceOrientation()
	{
		return Instance.m_LastValidDeviceOrientation;
	}

	public static DeviceOrientation GetLastValidPortraitOrientation()
	{
		return Instance.m_LastValidPortraitOrientation;
	}

	public static int GetTouchByFinger(int fingerID)
	{
		AllInput instance = Instance;
		for (int i = 0; i < instance.m_TouchCount; i++)
		{
			if (instance.m_Datas[i].FingerID == fingerID)
			{
				return i;
			}
		}
		return -1;
	}

	public static void ForceOrientation(EScreenOrientation orientation)
	{
		switch (orientation)
		{
		case EScreenOrientation.LandscapeLeft:
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			break;
		case EScreenOrientation.LandscapeRight:
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			break;
		case EScreenOrientation.Portrait:
			Screen.orientation = ScreenOrientation.Portrait;
			break;
		case EScreenOrientation.PortraitUpsideDown:
			Screen.orientation = ScreenOrientation.PortraitUpsideDown;
			break;
		}
	}

	public static void ActivateAutoRotateFrame(bool b)
	{
        Screen.autorotateToLandscapeLeft = b;
        Screen.autorotateToLandscapeRight = b;
        Screen.autorotateToPortrait = b;
        Screen.autorotateToPortraitUpsideDown = b;
    }

	public static void ActivateAutoRotateScreen(bool saveRestore, bool b)
	{
		if (saveRestore)
		{
			s_AutoRotateMem[0] = Screen.autorotateToLandscapeLeft;
			s_AutoRotateMem[1] = Screen.autorotateToLandscapeRight;
			s_AutoRotateMem[2] = Screen.autorotateToPortrait;
			s_AutoRotateMem[3] = Screen.autorotateToPortraitUpsideDown;
			Screen.autorotateToLandscapeLeft = b;
			Screen.autorotateToLandscapeRight = b;
			Screen.autorotateToPortrait = b;
			Screen.autorotateToPortraitUpsideDown = b;
		}
		else
		{
			Screen.autorotateToLandscapeLeft = s_AutoRotateMem[0];
			Screen.autorotateToLandscapeRight = s_AutoRotateMem[1];
			Screen.autorotateToPortrait = s_AutoRotateMem[2];
			Screen.autorotateToPortraitUpsideDown = s_AutoRotateMem[3];
		}
	}

	public static bool IsMicroAvailable()
	{
		AllInput instance = Instance;
		if (instance != null)
		{
			return instance.m_IsMicroAvailable;
		}
		return false;
	}

	protected static int GetPreviousTouchByFinger(int fingerID)
	{
		AllInput instance = Instance;
		for (int i = 0; i < instance.m_PreviousTouchCount; i++)
		{
			if (instance.m_PreviousDatas[i].FingerID == fingerID)
			{
				return i;
			}
		}
		return -1;
	}

	private void ComputeUnknownDatas()
	{
		for (int num = m_TouchCount - 1; num >= 0; num--)
		{
			int previousTouchByFinger = GetPreviousTouchByFinger(m_Datas[num].FingerID);
			if (m_Datas[num].State == EState.Moved || m_Datas[num].State == EState.Stationary)
			{
				if (previousTouchByFinger < 0 || m_PreviousDatas[previousTouchByFinger].State == EState.Leave)
				{
					m_Datas[num].State = EState.Began;
				}
			}
			else if ((m_Datas[num].State == EState.Began || m_Datas[num].State == EState.Leave) && ((m_Datas[num].State == EState.Leave && (previousTouchByFinger < 0 || m_PreviousDatas[previousTouchByFinger].State == EState.Leave)) || (m_Datas[num].State == EState.Began && previousTouchByFinger >= 0 && m_PreviousDatas[previousTouchByFinger].State == EState.Began)))
			{
				for (int i = num; i < m_TouchCount - 1; i++)
				{
					m_Datas[i] = m_Datas[i + 1];
				}
				m_TouchCount--;
			}
		}
	}

	private void ComputeForgottenDatas()
	{
		for (int i = 0; i < m_PreviousTouchCount; i++)
		{
			if (m_PreviousDatas[i].State != EState.Moved && m_PreviousDatas[i].State != EState.Stationary && m_PreviousDatas[i].State != EState.Began)
			{
				continue;
			}
			int touchByFinger = GetTouchByFinger(m_PreviousDatas[i].FingerID);
			if (touchByFinger < 0)
			{
				for (int num = m_TouchCount - 1; num >= i; num--)
				{
					m_Datas[num + 1] = m_Datas[num];
				}
				m_Datas[i] = m_PreviousDatas[i];
				m_Datas[i].State = EState.Leave;
				m_TouchCount++;
			}
			else if ((m_PreviousDatas[i].State == EState.Moved || m_PreviousDatas[i].State == EState.Stationary) && m_Datas[touchByFinger].State == EState.Began)
			{
				m_Datas[touchByFinger].State = EState.Leave;
			}
		}
	}

	private int ComputeXPosition(float inPosX)
	{
		inPosX *= m_XRatio;
		return Mathf.FloorToInt(inPosX);
	}

	private int ComputeYPosition(float inPosY)
	{
		inPosY *= m_YRatio;
		return Mathf.FloorToInt(inPosY);
	}

	private void OnOrientationChange(DeviceOrientation newOrientation)
	{
		if (newOrientation != m_DeviceOrientation)
		{
			ComputeRatios();
			m_DeviceOrientation = newOrientation;
		}
	}

	private void ResetExtraDatas()
	{
		for (int i = 0; i < m_TouchCount; i++)
		{
			m_ExtraDatas[i].guiPosComputed = false;
			m_ExtraDatas[i].guiDeltaComputed = false;
			m_ExtraDatas[i].raycastPosComputed = false;
			m_ExtraDatas[i].worldPosComputed = false;
			m_ExtraDatas[i].deltaXComputed = false;
			m_ExtraDatas[i].deltaYComputed = false;
		}
	}

	private void ComputeLastValidOrientation()
	{
		if (m_DeviceOrientation == DeviceOrientation.LandscapeLeft || m_DeviceOrientation == DeviceOrientation.LandscapeRight || m_DeviceOrientation == DeviceOrientation.Portrait || m_DeviceOrientation == DeviceOrientation.PortraitUpsideDown)
		{
			m_LastValidDeviceOrientation = m_DeviceOrientation;
		}
		if (m_DeviceOrientation == DeviceOrientation.Portrait || m_DeviceOrientation == DeviceOrientation.PortraitUpsideDown)
		{
			m_LastValidPortraitOrientation = m_DeviceOrientation;
		}
	}

	public static void ComputeRatios()
	{
		AllInput instance = Instance;
		if (Screen.width != 0)
		{
			instance.m_XRatio = Utility.RefWidth / (float)Screen.width;
		}
		if (Screen.height != 0)
		{
			instance.m_YRatio = Utility.RefHeight / (float)Screen.height;
		}
		Utility.SetRatios(instance.m_XRatio, instance.m_YRatio);
	}

	public static Vector3 TransformWithOrientation(Vector3 vec)
	{
		AllInput instance = Instance;
		Vector3 result = Vector3.zero;
		switch (instance.m_LastValidDeviceOrientation)
		{
		case DeviceOrientation.Portrait:
		case DeviceOrientation.FaceUp:
		case DeviceOrientation.FaceDown:
			result = vec;
			break;
		case DeviceOrientation.PortraitUpsideDown:
			result = vec * -1f;
			break;
		case DeviceOrientation.LandscapeLeft:
			result.x = 0f - vec.y;
			result.y = vec.x;
			break;
		case DeviceOrientation.LandscapeRight:
			result.x = vec.y;
			result.y = 0f - vec.x;
			break;
		}
		return result;
	}

	public static bool IsInternetReachable()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}

	private void ComputeIphonePlatform()
	{
		m_Acceleration = Input.acceleration;
		OnOrientationChange(Input.deviceOrientation);
		m_TouchCount = Input.touchCount;
		if (m_TouchCount > 11)
		{
			m_TouchCount = 11;
		}
		for (int i = 0; i < m_TouchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			Vector2 position = touch.position;
			m_Datas[i].xScreenPos = ComputeXPosition(position.x);
			m_Datas[i].yScreenPos = ComputeYPosition(position.y);
			m_Datas[i].FingerID = touch.fingerId;
			switch (Input.GetTouch(i).phase)
			{
			case TouchPhase.Began:
				m_Datas[i].State = EState.Began;
				break;
			case TouchPhase.Stationary:
				m_Datas[i].State = EState.Stationary;
				break;
			case TouchPhase.Moved:
			{
				m_Datas[i].State = EState.Moved;
				int previousTouchByFinger = GetPreviousTouchByFinger(i);
				if (previousTouchByFinger >= 0 && m_PreviousDatas[previousTouchByFinger].State == EState.Stationary)
				{
					float f = m_PreviousDatas[previousTouchByFinger].xScreenPos - m_Datas[i].xScreenPos;
					float f2 = m_PreviousDatas[previousTouchByFinger].yScreenPos - m_Datas[i].yScreenPos;
					if (Mathf.Abs(f) < m_FingerTolerance && Mathf.Abs(f2) < m_FingerTolerance)
					{
						m_Datas[i].State = EState.Stationary;
					}
					else
					{
						Utility.Log(ELog.Info, "Android Moved");
					}
				}
				break;
			}
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				m_Datas[i].State = EState.Leave;
				break;
			}
		}
	}

	private void ComputeOtherPlatform()
	{
		Vector2 vector = Input.mousePosition;
		int xScreenPos = ComputeXPosition(vector.x);
		int yScreenPos = ComputeYPosition(vector.y);
		m_TouchCount = 0;
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				if (m_Datas != null)
				{
					m_Datas[m_TouchCount].xScreenPos = xScreenPos;
					m_Datas[m_TouchCount].yScreenPos = yScreenPos;
					m_Datas[m_TouchCount].State = EState.Began;
					m_Datas[m_TouchCount].FingerID = i;
				}
				m_TouchCount++;
			}
			else if (Input.GetMouseButtonUp(i))
			{
				m_Datas[m_TouchCount].xScreenPos = xScreenPos;
				m_Datas[m_TouchCount].yScreenPos = yScreenPos;
				m_Datas[m_TouchCount].State = EState.Leave;
				m_Datas[m_TouchCount].FingerID = i;
				m_TouchCount++;
			}
			else
			{
				if (!Input.GetMouseButton(i))
				{
					continue;
				}
				int previousTouchByFinger = GetPreviousTouchByFinger(i);
				m_Datas[m_TouchCount].xScreenPos = xScreenPos;
				m_Datas[m_TouchCount].yScreenPos = yScreenPos;
				m_Datas[m_TouchCount].FingerID = i;
				m_Datas[m_TouchCount].State = EState.Stationary;
				if (previousTouchByFinger >= 0)
				{
					if (m_PreviousDatas[previousTouchByFinger].State != EState.Moved)
					{
						float f = m_PreviousDatas[previousTouchByFinger].xScreenPos - m_Datas[m_TouchCount].xScreenPos;
						float f2 = m_PreviousDatas[previousTouchByFinger].yScreenPos - m_Datas[m_TouchCount].yScreenPos;
						if (Mathf.Abs(f) > m_FingerTolerance || Mathf.Abs(f2) > m_FingerTolerance)
						{
							m_Datas[i].State = EState.Moved;
						}
					}
					else
					{
						m_Datas[i].State = EState.Moved;
					}
				}
				m_TouchCount++;
			}
		}
		m_Roll = Input.GetAxis("Mouse ScrollWheel");
	}

	private static void FindAllInput()
	{
		s_Instance = (AllInput)Object.FindObjectOfType(typeof(AllInput));
		if (s_Instance != null)
		{
			s_Instance.Start();
		}
	}

	private void AR_Awake()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		m_CameraAvailable = devices != null && devices.Length > 0;
		if (m_CameraAvailable)
		{
			string text = SystemInfo.deviceModel.ToUpperInvariant();
			if (text.Contains("TF201".ToUpperInvariant()) || text.Contains("TF101".ToUpperInvariant()) || text.Contains("RAZR".ToUpperInvariant()) || text.Contains("XT910".ToUpperInvariant()) || text.Contains("HTC One X".ToUpperInvariant()) || text.Contains("Nexus".ToUpperInvariant()) || text.Contains("Kindle".ToUpperInvariant()))
			{
				m_CameraAvailable = false;
			}
		}
		if (m_CameraAvailable)
		{
			m_HasPositionChoice = devices.Length > 1;
		}
		Utility.Log(ELog.Device, SystemInfo.deviceModel + " has camera: " + m_CameraAvailable);
	}

	private void AR_OnDisable()
	{
		StopCameraCapture();
	}

	public static bool IsCaptureAvailable()
	{
		return Instance.m_CameraAvailable;
	}

	public static bool HasPositionChoice()
	{
		return Instance.m_HasPositionChoice;
	}

	public static void StartCameraCapture(bool frontCamera, Material material)
	{
		AllInput instance = Instance;
		if (IsCaptureAvailable())
		{
			Utility.Log(ELog.Device, "StartCameraCapture()");
			instance.m_CameraState = ECameraState.Started;
			instance.m_UseFrontCamera = frontCamera;
			instance.StartCameraCapture();
			instance.m_MaterialsCapture.Clear();
			AddMaterialForCameraCapture(material);
			instance.m_LastMaterial = material;
		}
	}

	public static void StopCameraCapture()
	{
		AllInput instance = Instance;
		if (instance.m_CameraState != ECameraState.Stopped)
		{
			Utility.Log(ELog.Device, "StopCameraCapture()");
			instance.m_CameraState = ECameraState.Stopped;
			if (instance.m_WebCamTexture != null)
			{
				instance.m_WebCamTexture.Stop();
				Object.DestroyImmediate(instance.m_WebCamTexture);
				instance.m_WebCamTexture = null;
			}
		}
	}

	public static void PauseCameraCapture()
	{
		AllInput instance = Instance;
		if (instance.m_CameraState == ECameraState.Started)
		{
			Utility.Log(ELog.Device, "PauseCameraCapture()");
			instance.m_CameraState = ECameraState.InPause;
			if (instance.m_WebCamTexture != null)
			{
				instance.m_WebCamTexture.Pause();
			}
		}
	}

	public static void ResumeCameraCapture()
	{
		AllInput instance = Instance;
		if (instance.m_CameraState != ECameraState.Started)
		{
			Utility.Log(ELog.Device, "ResumeCameraCapture()");
			instance.m_CameraState = ECameraState.Started;
			instance.StartCameraCapture();
			instance.m_MaterialsCapture.Clear();
			AddMaterialForCameraCapture(instance.m_LastMaterial);
		}
	}

	public static void AddMaterialForCameraCapture(Material material)
	{
		AllInput instance = Instance;
		if (material != null)
		{
			instance.m_MaterialsCapture.Add(material);
			if (instance.m_WebCamTexture != null)
			{
				material.mainTexture = instance.m_WebCamTexture;
			}
		}
	}

	public static void ModifyCameraCapture(bool frontCamera)
	{
		AllInput instance = Instance;
		instance.m_UseFrontCamera = frontCamera;
		if (instance.m_CameraState == ECameraState.Started)
		{
			PauseCameraCapture();
			ResumeCameraCapture();
		}
		else
		{
			Utility.Log("ModifyCameraCapture - instance.m_CameraState: " + instance.m_CameraState);
		}
	}

	private void StartCameraCapture()
	{
		if (!m_CameraAvailable)
		{
			return;
		}
		WebCamDevice[] devices = WebCamTexture.devices;
		for (int i = 0; i < devices.Length; i++)
		{
			if (devices[i].isFrontFacing)
			{
				m_FrontCamera = devices[i].name;
			}
			else
			{
				m_BackCamera = devices[i].name;
			}
		}
		if (m_WebCamTexture == null)
		{
			m_WebCamTexture = new WebCamTexture();
			m_WebCamTexture.Play();
		}
		string text = ((!m_UseFrontCamera) ? m_BackCamera : m_FrontCamera);
		if (m_WebCamTexture.deviceName != text)
		{
			m_WebCamTexture.Stop();
			m_WebCamTexture.deviceName = text;
			m_WebCamTexture.Play();
		}
		if (!m_WebCamTexture.isPlaying)
		{
			m_WebCamTexture.Play();
		}
	}

	private void OnPlayerConnected()
	{
	}

	private void OnPlayerDisconnected()
	{
	}

	private void Net_Awake()
	{
		Application.runInBackground = true;
	}

	private void Net_Start()
	{
        //NetworkManager.Singleton.StartServer();
    }

	private bool Net_Update()
	{
		return false;
	}

	private void Net_OnDisable()
	{
	}

	private void Net_Reset()
	{
		m_MessagesTouch.Clear();
		m_MessagesAcceleration.Clear();
		m_MessagesOrientation.Clear();
		m_MessagesCameraCapture.Clear();
		for (int i = 0; i < m_MessageStuff.Length; i++)
		{
			m_MessageStuff[i].LastTime = 0.0;
		}
	}

	private void SendTouch(byte[] buffer)
	{
		int idx = 0;
		double timestamp = 0;
		if (timestamp > m_MessageStuff[0].LastTime)
		{
			SMessageTouch item = default(SMessageTouch);
			item.Time = timestamp;
			int num = Utility.UnSerializeS32(buffer, ref idx);
			item.Inputs = new SInputs[num];
			for (int i = 0; i < num; i++)
			{
				float num2 = Utility.UnSerializeF32(buffer, ref idx);
				float num3 = Utility.UnSerializeF32(buffer, ref idx);
				item.Inputs[i].xScreenPos = Mathf.FloorToInt(num2 * Utility.RefWidth);
				item.Inputs[i].yScreenPos = Mathf.FloorToInt(num3 * Utility.RefHeight);
				item.Inputs[i].State = (EState)Utility.UnSerializeS32(buffer, ref idx);
				item.Inputs[i].FingerID = Utility.UnSerializeS32(buffer, ref idx);
			}
			m_MessagesTouch.Add(item);
		}
	}

	private void SendAccel(Vector3 accel)
	{
		double timestamp = 0;
		if (timestamp > m_MessageStuff[1].LastTime)
		{
			SMessageAcceleration item = default(SMessageAcceleration);
			item.Time = timestamp;
			item.Acceleration = accel;
			m_MessagesAcceleration.Add(item);
		}
	}

	private void SendOrientation(int orientation)
	{
		double timestamp = 0;
		if (timestamp > m_MessageStuff[2].LastTime)
		{
			SMessageOrientation item = default(SMessageOrientation);
			item.Time = timestamp;
			item.Orientation = (DeviceOrientation)orientation;
			m_MessagesOrientation.Add(item);
		}
	}

	private void SendCamCapture(int width, int height, byte[] buffer)
	{
		double timestamp = 0;
		if (timestamp > m_MessageStuff[3].LastTime)
		{
			SMessageCameraCapture item = default(SMessageCameraCapture);
			item.Time = timestamp;
			item.Texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
			item.Texture.LoadImage(buffer);
			m_MessagesCameraCapture.Add(item);
		}
	}

	private void SendFreq(int touch, int accel, int cam)
	{
	}

	private void SendScreen(int width, int height, byte[] buffer)
	{
	}
}
