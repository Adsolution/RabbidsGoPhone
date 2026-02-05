using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
//using Prime31;
using UnityEngine;

public static class Utility
{
	public enum EFacebookFeedType
	{
		Status = 0,
		Link = 1,
		Photo = 2,
		Video = 3,
		Count = 4
	}

	public struct SFacebookGraphRequest
	{
		public string GraphPath;

		//public HTTPVerb HttpMethod;

		public Hashtable Parameters;

		public FacebookHandlerObjectList SuccessHandler;

		public FacebookHandlerList ErrorHandler;

		public object[] HandlerParams;
	}

	public struct SFacebookFeed
	{
		public string ID;

		public EFacebookFeedType Type;

		public Texture2D Thumbnail;

		public string Author;

		public string Date;

		public string LinkName;

		public string LinkDescription;

		public Texture2D Picture;

		public string Text;
	}

	public struct SProduct
	{
		public EProductID ID;

		public bool Unlocked;
	}

	public enum EBlendFactor
	{
		Zero = 0,
		One = 1,
		SrcColor = 2,
		InvSrcColor = 3,
		SrcAlpha = 4,
		InvSrcAlpha = 5,
		DestAlpha = 6,
		InvDestAlpha = 7,
		DestColor = 8,
		InvDestColor = 9,
		SrcAlphaSat = 10
	}

	public enum EWWWType
	{
		Url = 0,
		Form = 1,
		Data = 2,
		Count = 3
	}

	public class WWWData
	{
		private EWWWType m_Type;

		private string m_Url = string.Empty;

		private float m_TimeOut;

		private int m_NbCall;

		private WWWForm m_Form;

		private byte[] m_Datas;

		private Hashtable m_Header;

		private CallBackWWWSucceed m_Succeed;

		private CallBackWWWFailed m_Failed;

		private object[] m_Parameters = new object[0];

		public EWWWType Type
		{
			get
			{
				return m_Type;
			}
		}

		public string Url
		{
			get
			{
				return m_Url;
			}
			set
			{
				m_Url = value;
			}
		}

		public float TimeOut
		{
			get
			{
				return m_TimeOut;
			}
			set
			{
				m_TimeOut = value;
			}
		}

		public int NbCall
		{
			get
			{
				return m_NbCall;
			}
			set
			{
				m_NbCall = value;
			}
		}

		public WWWForm Form
		{
			get
			{
				return m_Form;
			}
			set
			{
				m_Form = value;
			}
		}

		public byte[] Datas
		{
			get
			{
				return m_Datas;
			}
			set
			{
				m_Datas = value;
			}
		}

		public Hashtable Header
		{
			get
			{
				return m_Header;
			}
			set
			{
				m_Header = value;
			}
		}

		public CallBackWWWSucceed Succeed
		{
			get
			{
				return m_Succeed;
			}
			set
			{
				m_Succeed = value;
			}
		}

		public CallBackWWWFailed Failed
		{
			get
			{
				return m_Failed;
			}
			set
			{
				m_Failed = value;
			}
		}

		public object[] Parameters
		{
			get
			{
				return m_Parameters;
			}
			set
			{
				m_Parameters = value;
			}
		}

		public WWWData(EWWWType type)
		{
			m_Type = type;
		}
	}

	public delegate void FacebookHandlerList(object[] list);

	public delegate void FacebookHandlerObjectList(object result, object[] list);

	public delegate void FacebookHandler();

	public delegate void CallBackWWWHandler(object[] list);

	public delegate void CallBackWWWSucceed(WWW www, object[] list);

	public delegate void CallBackWWWFailed(object[] list);

	private const int MAX_8BIT = 63;

	private const int MAX_16BIT = 16383;

	private const int MAX_32BIT = 1073741823;

	private static bool s_HD;

	private static bool s_Universal;

	private static bool s_Cheat;

	private static int s_GCCounter;

	private static string s_TexPath = "HD/";

	private static bool s_Chinese;

	private static bool s_FourthGenDevice;

	private static bool s_FourthGenDeviceInit;

	private static string s_DocumentPath;

	private static EResolution s_Resolution = EResolution.None;

	private static ScreenOrientation s_LastScreenOrientation = ScreenOrientation.Portrait;

	private static int m_Activity;

	private static int m_StopInput;

	private static bool s_Log = true;

	private static float s_XScale = 1f;

	private static float s_YScale = 1f;

	private static float s_RefWidth = 320f;

	private static float s_RefHeight = 480f;

	private static List<SFacebookGraphRequest> m_FacebookGraphRequestQueue = new List<SFacebookGraphRequest>();

	private static float m_TimeOut = 1.5f;

	private static bool m_GraphRequestProcessEnable = true;

	private static SProduct[] s_Products = new SProduct[3];

	private static bool s_Initialized = false;

	private static Color s_Zero = new Color(0f, 0f, 0f, 0f);

	private static Color s_One = new Color(1f, 1f, 1f, 1f);

	private static Color s_Padding = new Color(1f, 1f, 1f, 0f);

	public static bool Chinese
	{
		get
		{
			return s_Chinese;
		}
		set
		{
			s_Chinese = value;
		}
	}

	public static ScreenOrientation LastScreenOrientation
	{
		get
		{
			return s_LastScreenOrientation;
		}
		set
		{
			s_LastScreenOrientation = value;
		}
	}

	public static DeviceOrientation LastDeviceOrientation
	{
		get
		{
			DeviceOrientation result = DeviceOrientation.Portrait;
			if (s_LastScreenOrientation == ScreenOrientation.PortraitUpsideDown)
			{
				result = DeviceOrientation.PortraitUpsideDown;
			}
			return result;
		}
	}

	public static float RefWidth
	{
		get
		{
			return s_RefWidth;
		}
	}

	public static float RefHeight
	{
		get
		{
			return s_RefHeight;
		}
	}

	public static event FacebookHandler FacebookEndGraphRequest;

	public static event CallBackWWWHandler onWWWSucceed;

	public static event CallBackWWWHandler onWWWFailed;

	public static void SetHD(bool b)
	{
		s_HD = b;
		if (b)
		{
			s_TexPath = "HD/";
		}
		else
		{
			s_TexPath = "LD/";
		}
	}

	public static bool IsHD()
	{
		return s_HD;
	}

	public static void SetUniversal(bool b)
	{
		s_Universal = b;
	}

	public static bool IsUniversal()
	{
		return s_Universal;
	}

	public static string GetTexPath()
	{
		return s_TexPath;
	}

	public static void ActivateCheats()
	{
		s_Cheat = true;
	}

	public static bool IsCheater()
	{
		return s_Cheat;
	}

	public static void FreeMem()
	{
		FreeMem(false);
	}

	public static void FreeMem(bool gcCollect)
	{
		Resources.UnloadUnusedAssets();
		if (!gcCollect && ++s_GCCounter > 10)
		{
			gcCollect = true;
			s_GCCounter = 0;
		}
		if (gcCollect)
		{
			GC.Collect();
		}
		Log(ELog.Info, "FreeMem - Memory Clean - GC.Collect: " + gcCollect);
	}

	public static string GetPersistentDataPath()
	{
		if (s_DocumentPath == null)
		{
			s_DocumentPath = Application.persistentDataPath;
		}
		return s_DocumentPath;
	}

	public static bool IsSmartPhone()
	{
		return Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android;
	}

	public static bool IsFourthGenAppleDevice()
	{
		if (!s_FourthGenDeviceInit)
		{
			int num = Mathf.Min(Screen.width, Screen.height);
			int num2 = Mathf.Max(Screen.width, Screen.height);
			s_FourthGenDevice = num2 == 960 || num == 640;
			Log(ELog.Device, "w: " + Screen.width + ", h: " + Screen.height);
			s_FourthGenDeviceInit = true;
		}
		return s_FourthGenDevice;
	}

	public static EResolution GetResolution()
	{
		if (s_Resolution == EResolution.None)
		{
			if (Mathf.Approximately((float)Screen.width / (float)Screen.height, 1.5f) || Mathf.Approximately((float)Screen.height / (float)Screen.width, 1.5f))
			{
				s_Resolution = EResolution.IPhone;
			}
			else
			{
				s_Resolution = EResolution.IPad;
			}
		}
		return s_Resolution;
	}

	public static void StopInputByActivity(bool b)
	{
		if (b)
		{
			m_StopInput++;
		}
		else if (m_StopInput > 0)
		{
			m_StopInput--;
		}
	}

	private static void LogActivityView()
	{
		Log(ELog.Info, "ShowActivityView :: Activity=" + m_Activity + " :: Input=" + m_StopInput);
	}

	public static void ShowActivityView(bool stopInput)
	{
		Log(ELog.Info, "ActivityView BeforeShow:: stopedInput=" + stopInput + " :: Activity=" + m_Activity + " Input=" + m_StopInput);
		if (m_Activity <= 0)
		{
			//EtceteraAndroid.showProgressDialog(string.Empty, Localization.GetLocalizedText(ELoc.PleaseWait));
		}
		m_Activity++;
		if (stopInput)
		{
			m_StopInput++;
		}
		Log(ELog.Info, "ActivityView AfterShow:: stopedInput=" + stopInput + " :: Activity=" + m_Activity + " Input=" + m_StopInput);
	}

	public static void ClearActivityView()
	{
		Log(ELog.Info, "ActivityView BeforeClear:: Activity=" + m_Activity + " :: Input=" + m_StopInput);
		if (m_Activity > 0)
		{
			//EtceteraAndroid.hideProgressDialog();
		}
		m_Activity = 0;
		m_StopInput = 0;
		Log(ELog.Info, "ActivityView AfterClear:: Activity=" + m_Activity + " :: Input=" + m_StopInput);
	}

	public static void HideActivityView(bool stopedInput)
	{
		Log(ELog.Info, "ActivityView BeforeHide:: stopedInput=" + stopedInput + " :: Activity=" + m_Activity + " - Input=" + m_StopInput);
		int activity = m_Activity;
		int stopInput = m_StopInput;
		if (m_Activity > 0)
		{
			m_Activity--;
		}
		if (stopedInput && m_StopInput > 0)
		{
			m_StopInput--;
		}
		if (m_Activity <= 0)
		{
			//EtceteraAndroid.hideProgressDialog();
		}
		if (activity > 0 && m_Activity != activity - 1)
		{
			m_Activity = activity - 1;
			if (m_Activity <= 0)
			{
				//EtceteraAndroid.hideProgressDialog();
			}
		}
		if (stopedInput && stopInput > 0 && m_StopInput != stopInput - 1)
		{
			m_StopInput = stopInput - 1;
			Log(ELog.Info, "ActivityView Hide Trick:: stopedInput was decreased to " + m_StopInput);
		}
		if (m_Activity == 0 && m_StopInput != 0)
		{
			m_StopInput = 0;
			Log(ELog.Info, "ActivityView Hide Trick:: stopedInput was reset to 0");
		}
		Log(ELog.Info, "ActivityView AfterHide:: stopedInput=" + stopedInput + " :: Activity=" + m_Activity + " - Input=" + m_StopInput);
	}

	public static bool InputStoppedByActivity()
	{
		return m_Activity > 0 && m_StopInput > 0;
	}

	public static byte[] LoadFile(string path)
	{
		byte[] array = null;
		FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
		try
		{
			int num = (int)fileStream.Length;
			array = new byte[num];
			int num2 = 0;
			int num3;
			do
			{
				num3 = fileStream.Read(array, num2, num - num2);
				num2 += num3;
			}
			while (num3 > 0);
			return array;
		}
		finally
		{
			fileStream.Close();
		}
	}

	public static void SaveFile(string path, string fileName, byte[] content)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		char c = path[path.Length - 1];
		path = ((c == '/') ? (path + fileName) : (path + "/" + fileName));
		FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
		try
		{
			fileStream.Write(content, 0, content.Length);
		}
		finally
		{
			fileStream.Close();
		}
	}

	public static void DeleteFile(string path)
	{
		FileInfo fileInfo = new FileInfo(path);
		if (fileInfo.Exists)
		{
			fileInfo.Delete();
		}
	}

	public static void Log(ELog log, string str)
	{
		bool flag = false;
		switch (log)
		{
		case ELog.Errors:
		case ELog.Gameplay:
		case ELog.GameplayDebug:
		case ELog.Device:
		case ELog.Animation:
		case ELog.Microphone:
		case ELog.Sound:
		case ELog.GUI:
		case ELog.Initialize:
		case ELog.Info:
		case ELog.Loading:
		case ELog.Debug:
		case ELog.Plugin:
			flag = true;
			break;
		}
		if (flag && s_Log)
		{
			if (log != ELog.Errors)
			{
				Debug.Log(string.Concat("RabbidsLog: ", Time.time.ToString("f2"), " - ", log, " - ", str));
			}
			else
			{
				Debug.LogWarning(string.Concat("RabbidsLog: ", Time.time.ToString("f2"), " - ", log, " - ", str));
			}
		}
	}

	public static void Log(string str)
	{
		Log(ELog.Debug, str);
	}

	public static void Log(ELog log, ArrayList list)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Hashtable item in list)
		{
			AddToString(item, stringBuilder, 0);
		}
		Log(log, stringBuilder.ToString());
	}

	public static void Log(ArrayList list)
	{
		Log(ELog.Debug, list);
	}

	public static void Log(ELog log, Hashtable table)
	{
		StringBuilder stringBuilder = new StringBuilder();
		AddToString(table, stringBuilder, 0);
		Log(log, stringBuilder.ToString());
	}

	public static void Log(Hashtable table)
	{
		Log(ELog.Debug, table);
	}

	public static void Log(ELog log, object obj)
	{
		if (obj.GetType() == typeof(ArrayList))
		{
			Log(log, (ArrayList)obj);
		}
		else if (obj.GetType() == typeof(Hashtable))
		{
			Log(log, (Hashtable)obj);
		}
		else
		{
			Log(log, obj.ToString());
		}
	}

	public static void Log(object obj)
	{
		Log(ELog.Debug, obj);
	}

	public static void LogRed(string str)
	{
		Debug.LogError("RabbidsLogRed: " + Time.time.ToString("f2") + " - " + str);
	}

	public static void LogYellow(string str)
	{
		Debug.LogWarning("RabbidsLogYellow: " + Time.time.ToString("f2") + " - " + str);
	}

	private static void AddToString(Hashtable item, StringBuilder builder, int depth)
	{
		string text = string.Empty;
		for (int i = 0; i < depth; i++)
		{
			text += "\t";
		}
		builder.AppendFormat("{0}{{\n", text);
		foreach (DictionaryEntry item2 in item)
		{
			if (item2.Value is Hashtable)
			{
				builder.AppendFormat("{0}\t{1}:\n", text, item2.Key);
				AddToString((Hashtable)item2.Value, builder, depth + 1);
			}
			else if (item2.Value is ArrayList)
			{
				builder.AppendFormat("{0}\t{1}:\n", text, item2.Key);
				AddToString((ArrayList)item2.Value, builder, depth + 1);
			}
			else
			{
				builder.AppendFormat("{0}\t{1}: {2}\n", text, item2.Key, item2.Value);
			}
		}
		builder.AppendFormat("{0}}}\n", text);
	}

	public static void AddToString(ArrayList result, StringBuilder builder, int depth)
	{
		string text = string.Empty;
		for (int i = 0; i < depth; i++)
		{
			text += "\t";
		}
		builder.AppendFormat("{0}[\n", text);
		foreach (object item in result)
		{
			if (item is Hashtable)
			{
				AddToString((Hashtable)item, builder, depth + 1);
			}
			else if (item is ArrayList)
			{
				AddToString((ArrayList)item, builder, depth + 1);
			}
			else
			{
				builder.AppendFormat("{0}\t{1}\n", text, item);
			}
		}
		builder.AppendFormat("{0}]\n", text);
	}

	public static float AngleBetweenLines(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
	{
		float num = line1End.x - line1Start.x;
		float num2 = line1End.y - line1Start.y;
		float num3 = line2End.x - line2Start.x;
		float num4 = line2End.y - line2Start.y;
		float num5 = Mathf.Acos((num * num3 + num2 * num4) / (Mathf.Sqrt(num * num + num2 * num2) * Mathf.Sqrt(num3 * num3 + num4 * num4)));
		return 180f * num5 / (float)Math.PI;
	}

	public static float SmoothAngle(float startAngle, float angleToReach, float smoothCoef)
	{
		float num = angleToReach - startAngle;
		float num2 = angleToReach - startAngle + 360f;
		float num3 = angleToReach - startAngle - 360f;
		if (Mathf.Abs(num2) < Mathf.Abs(num))
		{
			num = num2;
		}
		if (Mathf.Abs(num3) < Mathf.Abs(num))
		{
			num = num3;
		}
		num *= smoothCoef;
		for (startAngle += num; startAngle >= 360f; startAngle -= 360f)
		{
		}
		while (startAngle < 0f)
		{
			startAngle += 360f;
		}
		return startAngle;
	}

	public static float SquareDistXZ(Vector3 from, Vector3 to)
	{
		Vector3 vector = to - from;
		return vector.x * vector.x + vector.z * vector.z;
	}

	public static float SquareDistXY(Vector3 from, Vector3 to)
	{
		Vector3 vector = to - from;
		return vector.x * vector.x + vector.y * vector.y;
	}

	public static bool SegmentsIntersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D, Vector3 O, out Vector3 Intersect)
	{
		return SegmentsIntersect(A + O, B + O, C + O, D + O, out Intersect);
	}

	public static bool SegmentsIntersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D, out Vector3 Intersect)
	{
		Intersect = Vector3.zero;
		float x = A.x;
		float z = A.z;
		float x2 = B.x;
		float z2 = B.z;
		float x3 = C.x;
		float z3 = C.z;
		float x4 = D.x;
		float z4 = D.z;
		double num2;
		double num3;
		if (x == x2)
		{
			if (x3 == x4)
			{
				return false;
			}
			double num = (z3 - z4) / (x3 - x4);
			num2 = x;
			num3 = num * (double)(x - x3) + (double)z3;
		}
		else if (x3 == x4)
		{
			double num4 = (z - z2) / (x - x2);
			num2 = x3;
			num3 = num4 * (double)(x3 - x) + (double)z;
		}
		else
		{
			double num5 = (z3 - z4) / (x3 - x4);
			double num6 = (z - z2) / (x - x2);
			double num7 = (double)z3 - num5 * (double)x3;
			double num8 = (double)z - num6 * (double)x;
			num2 = (num8 - num7) / (num5 - num6);
			num3 = num5 * num2 + num7;
		}
		if ((num2 < (double)x && num2 < (double)x2) || (num2 > (double)x && num2 > (double)x2) || (num2 < (double)x3 && num2 < (double)x4) || (num2 > (double)x3 && num2 > (double)x4) || (num3 < (double)z && num3 < (double)z2) || (num3 > (double)z && num3 > (double)z2) || (num3 < (double)z3 && num3 < (double)z4) || (num3 > (double)z3 && num3 > (double)z4))
		{
			return false;
		}
		Intersect.x = (float)num2;
		Intersect.z = (float)num3;
		return true;
	}

	public static float LimitValue(float value, float min, float max)
	{
		return Mathf.Min(Mathf.Max(value, min), max);
	}

	public static float Cerp(float y0, float y1, float y2, float y3, float mu)
	{
		float num = mu * mu;
		float num2 = y3 - y2 - y0 + y1;
		float num3 = y0 - y1 - num2;
		float num4 = y2 - y0;
		return num2 * mu * num + num3 * num + num4 * mu + y1;
	}

	public static void SetLayerRecursivly(Transform t, int lyr)
	{
		if (!(t == null))
		{
			t.gameObject.layer = lyr;
			for (int i = 0; i < t.childCount; i++)
			{
				SetLayerRecursivly(t.GetChild(i), lyr);
			}
		}
	}

	public static GameObject SetLayer(string objName, int lyr)
	{
		if (objName == null)
		{
			return null;
		}
		GameObject gameObject = GameObject.Find(objName);
		if (gameObject != null)
		{
			gameObject.layer = lyr;
		}
		else
		{
			Log(ELog.Errors, "Utility.SetLayer :: Unable to find " + objName);
		}
		return gameObject;
	}

	public static void ShowRecursivly(Transform t, bool shown)
	{
		if (!(t == null))
		{
			if (t.GetComponent<Renderer>() != null)
			{
				t.GetComponent<Renderer>().enabled = shown;
			}
			for (int i = 0; i < t.childCount; i++)
			{
				ShowRecursivly(t.GetChild(i), shown);
			}
		}
	}

	public static void SetAlphaRecursivly(Transform t, float alpha)
	{
		if (t == null)
		{
			return;
		}
		Renderer renderer = t.GetComponent<Renderer>();
		if (renderer != null)
		{
			Material material = renderer.material;
			if (material != null && material.HasProperty("_Color"))
			{
				Color color = material.color;
				color.a = alpha;
				material.color = color;
			}
		}
		for (int i = 0; i < t.childCount; i++)
		{
			SetAlphaRecursivly(t.GetChild(i), alpha);
		}
	}

	public static T LoadResource<T>(string resPath) where T : UnityEngine.Object
	{
		T val = (T)Resources.Load(resPath);
		if (val == null)
		{
			Log(ELog.Errors, "Unable to load " + resPath);
		}
		return val;
	}

	public static T LoadTextureResource<T>(string resPath) where T : UnityEngine.Object
	{
		resPath = GetTexPath() + resPath;
		T val = (T)Resources.Load(resPath);
		if (val == null)
		{
			Log(ELog.Errors, "Unable to load " + resPath);
		}
		return val;
	}

	public static T InstanciateFromResources<T>(string resPath) where T : UnityEngine.Object
	{
		T val = (T)Resources.Load(resPath);
		if (val == null)
		{
			Log(ELog.Errors, "Unable to load " + resPath);
		}
		else
		{
			val = (T)UnityEngine.Object.Instantiate(val);
		}
		return val;
	}

	public static GameObject InstanciateFromResources(string resPath)
	{
		return InstanciateFromResources(resPath, -1);
	}

	public static GameObject InstanciateFromResources(string resPath, int layer)
	{
		GameObject gameObject = null;
		GameObject gameObject2 = LoadResource<GameObject>(resPath);
		if (gameObject2 != null)
		{
			gameObject = (GameObject)UnityEngine.Object.Instantiate(gameObject2);
			if (gameObject != null)
			{
				gameObject.name = gameObject2.name;
				if (layer > 0)
				{
					SetLayerRecursivly(gameObject.transform, layer);
				}
			}
		}
		return gameObject;
	}

	public static GameObject LoadGameObject(string resPath, Transform parent)
	{
		return LoadGameObject(resPath, parent, 8);
	}

	public static GameObject LoadGameObject(string resPath, Transform parent, int layer)
	{
		GameObject gameObject = null;
		gameObject = InstanciateFromResources(resPath);
		if (gameObject != null)
		{
			SetLayerRecursivly(gameObject.transform, layer);
			if (parent != null)
			{
				Vector3 localScale = gameObject.transform.localScale;
				Vector3 localPosition = gameObject.transform.localPosition;
				Quaternion localRotation = gameObject.transform.localRotation;
				gameObject.transform.parent = parent;
				gameObject.transform.localScale = localScale;
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localRotation = localRotation;
			}
		}
		return gameObject;
	}

	public static GameObject UnloadGameObject(GameObject obj)
	{
		if (obj != null)
		{
			obj.transform.parent = null;
			UnityEngine.Object.DestroyImmediate(obj);
			obj = null;
			FreeMem();
		}
		return obj;
	}

	public static T FindGameObjectComponent<T>(string name) where T : Component
	{
		UnityEngine.Object obj = GameObject.Find(name);
		if ((bool)obj)
		{
			T component = GameObject.Find(name).GetComponent<T>();
			if (component == null)
			{
				Log(ELog.Errors, "FindGameObjectComponent : Failed to find the asked conponent on obj " + name);
			}
			return component;
		}
		Log(ELog.Errors, "FindGameObjectComponent : Failed with " + name);
		return (T)null;
	}

	public static GameObject FindGameObject(string name)
	{
		GameObject gameObject = GameObject.Find(name);
		if (gameObject == null)
		{
			Log(ELog.Errors, "FindGameObject : Failed with " + name);
		}
		return gameObject;
	}

	public static void LoadLevel(MonoBehaviour script, string levelName)
	{
		script.StopAllCoroutines();
		Application.LoadLevel(levelName);
	}

	public static void LoadLevel(MonoBehaviour script, int levelID)
	{
		script.StopAllCoroutines();
		Application.LoadLevel(0);
	}

	public static GameObject CreateLoadingScreen()
	{
		GameObject gameObject = InstanciateFromResources<GameObject>("Misc/LoadingScreen");
		if (gameObject != null)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				component.material.mainTexture = LoadResource<Texture2D>(GetTexPath() + "Misc/LoadingScreen");
			}
		}
		else
		{
			Log(ELog.Errors, "Failed to create a LoadingScreen");
		}
		return gameObject;
	}

	public static GameObject CreateMenuBackground()
	{
		GameObject gameObject = InstanciateFromResources<GameObject>("Misc/MenuBackground");
		if (gameObject != null)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				component.material.mainTexture = LoadResource<Texture2D>(GetTexPath() + "Misc/LoadingScreen");
			}
		}
		else
		{
			Log(ELog.Errors, "Failed to create a MenuBackground");
		}
		return gameObject;
	}

	public static GameObject CreateShadow()
	{
		GameObject gameObject = InstanciateFromResources<GameObject>("Misc/Shadow");
		if (gameObject != null)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				component.material.mainTexture = LoadResource<Texture2D>(GetTexPath() + "Misc/FakeShadow");
			}
		}
		else
		{
			Log(ELog.Errors, "Failed to create a Shadow");
		}
		return gameObject;
	}

	public static void SetRatios(float xRatio, float yRatio)
	{
		if (xRatio != 0f)
		{
			s_XScale = 1f / xRatio;
		}
		else
		{
			s_XScale = 1f;
		}
		if (yRatio != 0f)
		{
			s_YScale = 1f / yRatio;
		}
		else
		{
			s_YScale = 1f;
		}
	}

	public static float NewX(float x)
	{
		return x * s_XScale;
	}

	public static float NewY(float y)
	{
		return y * s_YScale;
	}

	public static Vector2 NewVector2(float x, float y)
	{
		x *= s_XScale;
		y *= s_YScale;
		return new Vector2(x, y);
	}

	public static Rect NewRect(float left, float top, float width, float height)
	{
		width *= s_XScale;
		height *= s_YScale;
		left *= s_XScale;
		top *= s_YScale;
		return new Rect(left, top, width, height);
	}

	public static Rect NewRectP(float left, float top, float width, float height)
	{
		width *= (float)Screen.width / 100f;
		height *= (float)Screen.height / 100f;
		left *= (float)Screen.width / 100f;
		top *= (float)Screen.height / 100f;
		return new Rect(left, top, width, height);
	}

	public static Rect ComputeRect(Texture2D tex, float leftFromCenter, float topFromCenter)
	{
		float num = tex.width;
		float num2 = tex.height;
		float num3 = (1024f - num2) * 0.5f + topFromCenter;
		float num4 = (768f - num) * 0.5f + leftFromCenter;
		num /= s_XScale;
		num2 /= s_YScale;
		num4 /= s_XScale;
		num3 /= s_YScale;
		return new Rect(num4, num3, num, num2);
	}

	public static void LocalToScreenResolution(Transform t, Camera cam)
	{
		if ((bool)cam && cam.orthographic)
		{
			t.localScale = Vector3.Scale(t.localScale, new Vector3((float)Screen.width / (float)Screen.height * 2f, 2f, 1f));
			t.localScale *= cam.orthographicSize;
		}
	}

	public static Vector3 MeterToPixel(Vector3 meter, Camera cam)
	{
		if (!cam || !cam.orthographic)
		{
			return Vector3.zero;
		}
		return meter * ((float)Screen.height / (cam.orthographicSize * 2f));
	}

	public static float MeterToPixel(float meter, Camera cam)
	{
		if (!cam || !cam.orthographic)
		{
			return 0f;
		}
		return meter * ((float)Screen.height / (cam.orthographicSize * 2f));
	}

	public static Vector3 PixelToMeter(Vector3 pixel, Camera cam)
	{
		if (!cam || !cam.orthographic)
		{
			return Vector3.zero;
		}
		return pixel / ((float)Screen.height / (cam.orthographicSize * 2f));
	}

	public static float PixelToMeter(float pixel, Camera cam)
	{
		if (!cam || !cam.orthographic)
		{
			return 0f;
		}
		return pixel / ((float)Screen.height / (cam.orthographicSize * 2f));
	}

	public static byte[] Serialize(uint value)
	{
		byte b = 0;
		byte b2 = (byte)(value >> 24);
		byte b3 = (byte)(value >> 16);
		byte b4 = (byte)(value >> 8);
		byte b5 = (byte)value;
		if (value <= 63)
		{
			return new byte[1] { b5 };
		}
		if (value <= 16383)
		{
			return new byte[2]
			{
				(byte)(0x40 | b4),
				b5
			};
		}
		if (value <= 1073741823)
		{
			return new byte[4]
			{
				(byte)(0x80 | b2),
				b3,
				b4,
				b5
			};
		}
		return new byte[5]
		{
			(byte)(0xC0 | b),
			b2,
			b3,
			b4,
			b5
		};
	}

	public static byte[] Serialize(int value)
	{
		return Serialize((uint)value);
	}

	public static byte[] Serialize(float value)
	{
		byte[] bytes = BitConverter.GetBytes(value);
		uint value2 = BitConverter.ToUInt32(bytes, 0);
		return Serialize(value2);
	}

	public static byte[] Serialize(int[] values)
	{
		List<byte> list = new List<byte>();
		list.AddRange(Serialize(values.Length));
		for (int i = 0; i < values.Length; i++)
		{
			list.AddRange(Serialize(values[i]));
		}
		return list.ToArray();
	}

	public static uint UnSerializeU32(byte[] buffer, ref int idx)
	{
		byte b = buffer[idx++];
		uint num = (uint)(b >> 6);
		if (num > 1)
		{
			num++;
		}
		uint num2 = (uint)(b & 0x3F);
		while (num != 0)
		{
			num2 <<= 8;
			num2 |= buffer[idx++];
			num--;
		}
		return num2;
	}

	public static int UnSerializeS32(byte[] buffer, ref int idx)
	{
		return (int)UnSerializeU32(buffer, ref idx);
	}

	public static float UnSerializeF32(byte[] buffer, ref int idx)
	{
		byte[] array = new byte[4];
		byte b = buffer[idx++];
		uint num = (uint)(b >> 6);
		if (num > 1)
		{
			num++;
		}
		array[3] = (byte)(b & 0x3F);
		while (num != 0)
		{
			array[--num] = buffer[idx++];
		}
		return BitConverter.ToSingle(array, 0);
	}

	public static int[] UnSerializeS32Array(byte[] buffer, ref int idx)
	{
		List<int> list = new List<int>();
		int num = UnSerializeS32(buffer, ref idx);
		for (int i = 0; i < num; i++)
		{
			list.Add(UnSerializeS32(buffer, ref idx));
		}
		return list.ToArray();
	}

	public static void RegisterSocialNetworking()
	{
		FacebookClearGraphRequestQueue();
		//FacebookManager.customRequestReceivedEvent += OnReveivedCustomRequest;
		//FacebookManager.customRequestFailedEvent += OnCustomRequestFailed;
	}

	public static void UnregisterSocialNetworking()
	{
		FacebookClearGraphRequestQueue();
		//FacebookManager.customRequestReceivedEvent -= OnReveivedCustomRequest;
		//FacebookManager.customRequestFailedEvent -= OnCustomRequestFailed;
	}

	public static IEnumerator FacebookLogInThenAction(FacebookHandlerList loggedInAction, FacebookHandlerList loggedOutAction, params object[] list)
	{
		bool loggedIn = false;
		//loggedIn = FacebookAndroid.isSessionValid();
		if (!loggedIn)
		{
			//FacebookAndroid.loginWithRequestedPermissions(new string[3] { "publish_stream", "email", "user_birthday" });
			yield return new WaitForSeconds(m_TimeOut);
			//loggedIn = FacebookAndroid.isSessionValid();
		}
		if (loggedIn)
		{
			if (loggedInAction != null)
			{
				loggedInAction(list);
			}
		}
		else if (loggedOutAction != null)
		{
			loggedOutAction(list);
		}
	}

	/*public static void FacebookAddGraphRequest(string graphPath, HTTPVerb httpMethod, Hashtable parameters, FacebookHandlerObjectList successHandler, FacebookHandlerList errorHandler, params object[] list)
	{
		SFacebookGraphRequest item = default(SFacebookGraphRequest);
		item.GraphPath = graphPath;
		item.HttpMethod = httpMethod;
		item.Parameters = parameters;
		item.SuccessHandler = successHandler;
		item.ErrorHandler = errorHandler;
		item.HandlerParams = list;
		m_FacebookGraphRequestQueue.Add(item);
		if (m_FacebookGraphRequestQueue.Count == 1)
		{
			FacebookProcessGraphRequest();
		}
	}*/

	public static int GetFacebookGraphRequestCount()
	{
		return m_FacebookGraphRequestQueue.Count;
	}

	public static void EnableFacebookGraphRequestProcess(bool b)
	{
		m_GraphRequestProcessEnable = b;
		if (b && m_FacebookGraphRequestQueue.Count > 0)
		{
			FacebookProcessGraphRequest();
		}
	}

	private static void FacebookProcessGraphRequest()
	{
		if (!m_GraphRequestProcessEnable)
		{
			return;
		}
		if (m_FacebookGraphRequestQueue.Count > 0)
		{
			SFacebookGraphRequest sFacebookGraphRequest = m_FacebookGraphRequestQueue[0];
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Hashtable parameters = sFacebookGraphRequest.Parameters;
			IEnumerator enumerator = parameters.Keys.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				string key = (string)enumerator.Current;
				dictionary.Add(key, (string)parameters[key]);
			}
			//Facebook.instance.graphRequest(sFacebookGraphRequest.GraphPath, sFacebookGraphRequest.HttpMethod, dictionary, null);
		}
		else if (Utility.FacebookEndGraphRequest != null)
		{
			Utility.FacebookEndGraphRequest();
		}
	}

	private static void FacebookClearGraphRequestQueue()
	{
		m_FacebookGraphRequestQueue.Clear();
	}

	private static void OnReveivedCustomRequest(object result)
	{
		if (m_GraphRequestProcessEnable && m_FacebookGraphRequestQueue.Count > 0)
		{
			SFacebookGraphRequest sFacebookGraphRequest = m_FacebookGraphRequestQueue[0];
			if (sFacebookGraphRequest.SuccessHandler != null)
			{
				sFacebookGraphRequest.SuccessHandler(result, sFacebookGraphRequest.HandlerParams);
			}
			OnEndCustomRequest();
		}
	}

	private static void OnCustomRequestFailed(string error)
	{
		if (m_GraphRequestProcessEnable && m_FacebookGraphRequestQueue.Count > 0)
		{
			Log(ELog.Errors, error);
			SFacebookGraphRequest sFacebookGraphRequest = m_FacebookGraphRequestQueue[0];
			if (sFacebookGraphRequest.ErrorHandler != null)
			{
				sFacebookGraphRequest.ErrorHandler(sFacebookGraphRequest.HandlerParams);
			}
			OnEndCustomRequest();
		}
	}

	private static void OnEndCustomRequest()
	{
		m_FacebookGraphRequestQueue.RemoveAt(0);
		FacebookProcessGraphRequest();
	}

	public static bool IsAvailableProduct(EProductID productID)
	{
		return productID < EProductID.AvailableCount;
	}

	public static int GetUnlockedProducts()
	{
		int num = 0;
		for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.AvailableCount; eProductID++)
		{
			if (IsUnlockedProduct(eProductID))
			{
				num++;
			}
		}
		return num;
	}

	public static bool IsUnlockedProduct(EProductID productID)
	{
		switch (productID)
		{
		case EProductID.None:
			return false;
		case EProductID.Free:
			return true;
		default:
		{
			bool flag = !AchievementTable.IsGoodyNew(AchievementTable.EGoody.Pack, productID.ToString());
			InitializeUnlockedProducts();
			return s_Products[(int)productID].Unlocked || flag;
		}
		}
	}

	public static void ResetUnlockedProducts()
	{
		s_Initialized = false;
	}

	public static void UnlockProduct(EProductID productID)
	{
		AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Pack, productID.ToString());
		if (!IsUnlockedProduct(productID))
		{
			s_Products[(int)productID].Unlocked = true;
		}
	}

	public static EProductID[] GetAllProductIDs()
	{
		int num = 2;
		EProductID[] array = new EProductID[num];
		int i;
		for (i = 0; i < 2; i++)
		{
			array[i] = (EProductID)i;
		}
		for (i++; i < 3; i++)
		{
			array[i - 1] = (EProductID)i;
		}
		return array;
	}

	public static void RequestProductData(EProductID[] list)
	{
		string text = string.Empty;
		for (int i = 0; i < list.Length; i++)
		{
			text = ((!IsUniversal()) ? (text + GlobalVariables.APPLE_PRODUCT_ID_PREFIX_LD) : (text + GlobalVariables.APPLE_PRODUCT_ID_PREFIX_HD));
			text = text + list[i].ToString() + ",";
		}
		if (list.Length > 0)
		{
			text = text.Substring(0, text.Length - 1);
			//StoreKitBinding.requestProductData(text);
		}
	}

	public static EProductID GetProductIDEnum(string str)
	{
		for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.AvailableCount; eProductID++)
		{
			if (str.Equals(eProductID.ToString()))
			{
				return eProductID;
			}
		}
		return EProductID.None;
	}

	public static EProductID GetProductIDEnumWithStoreProductID(string str)
	{
		return GetProductIDEnum(str);
	}

	private static void InitializeUnlockedProducts()
	{
		if (s_Initialized)
		{
			return;
		}
		Log(ELog.Info, "InitializeUnlockedProducts");
		s_Initialized = true;
		//List<StoreKitTransaction> allSavedTransactions = StoreKitBinding.getAllSavedTransactions();
		/*for (int i = 0; i < allSavedTransactions.Count; i++)
		{
			Log(ELog.Initialize, allSavedTransactions[i].productIdentifier);
		}*/
		int j;
		for (j = 0; j < 2; j++)
		{
			s_Products[j].ID = (EProductID)j;
			if (AchievementTable.IsProductFree((EProductID)j))
			{
				s_Products[j].Unlocked = true;
				continue;
			}
			string empty = string.Empty;
			empty += s_Products[j].ID;
			bool flag = false;
			/*for (int i = 0; i < allSavedTransactions.Count; i++)
			{
				if (flag)
				{
					break;
				}
				if (empty.Equals(allSavedTransactions[i].productIdentifier))
				{
					flag = true;
				}
			}*/
			s_Products[j].Unlocked = flag;
			Log(ELog.Info, "'" + empty + "' / " + flag);
		}
		for (; j < 3; j++)
		{
			s_Products[j].ID = (EProductID)j;
			s_Products[j].Unlocked = false;
		}
	}

	public static Texture2D Screenshot(DeviceOrientation deviceOrientation)
	{
		int width = Screen.width;
		int height = Screen.height;
		Texture2D texture2D = null;
		Texture2D texture2D2 = null;
		if (deviceOrientation == DeviceOrientation.Unknown)
		{
			DeviceOrientation lastValidDeviceOrientation = AllInput.GetLastValidDeviceOrientation();
		}
		RenderTexture renderTexture = new RenderTexture(width, height, 24);
		Camera.main.targetTexture = renderTexture;
		Camera.main.Render();
		RenderTexture.active = renderTexture;
		texture2D = new Texture2D(width, height, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0, false);
		texture2D.Apply(false);
		texture2D2 = new Texture2D(height, width, TextureFormat.RGB24, false);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Color pixel = texture2D.GetPixel(i, j);
				texture2D2.SetPixel(height - j, i, pixel);
			}
		}
		UnityEngine.Object.DestroyImmediate(texture2D);
		texture2D = texture2D2;
		Camera.main.targetTexture = null;
		RenderTexture.active = null;
		UnityEngine.Object.DestroyImmediate(renderTexture);
		return texture2D;
	}

	public static Texture2D BlendTextures(Texture2D src, Texture2D dst)
	{
		return BlendTextures(src, dst, EBlendFactor.SrcAlpha, EBlendFactor.InvSrcAlpha);
	}

	public static Texture2D BlendTextures(Texture2D src, Texture2D dst, EBlendFactor srcFactor, EBlendFactor dstFactor)
	{
		if (src == null)
		{
			return dst;
		}
		if (dst == null)
		{
			return src;
		}
		int num = Mathf.Max(src.width, dst.width);
		int num2 = Mathf.Max(src.height, dst.height);
		int num3 = num * num2;
		Color[] pixels = GetPixels(src, num, num2);
		Color[] pixels2 = GetPixels(dst, num, num2);
		Color[] array = new Color[num3];
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.ARGB32, false);
		for (int i = 0; i < num3; i++)
		{
			Color color = ProcessColor(pixels[i], pixels[i], pixels2[i], srcFactor);
			Color color2 = ProcessColor(pixels2[i], pixels[i], pixels2[i], dstFactor);
			array[i] = color + color2;
		}
		texture2D.SetPixels(array);
		texture2D.Apply(false);
		return texture2D;
	}

	public static void ConvertToGreyScale(Texture2D texture)
	{
		Color[] pixels = texture.GetPixels();
		for (int i = 0; i < pixels.Length; i++)
		{
			float num = 0.2125f * pixels[i].r + 0.7154f * pixels[i].g + 0.0721f * pixels[i].b;
			pixels[i].r = num;
			pixels[i].g = num;
			pixels[i].b = num;
		}
		texture.SetPixels(pixels);
	}

	public static Texture2D AddColor(Texture2D texture, Color clr)
	{
		Color[] pixels = texture.GetPixels();
		Color color = default(Color);
		for (int i = 0; i < pixels.Length; i++)
		{
			color.r = pixels[i].r * clr.r;
			color.g = pixels[i].g * clr.g;
			color.b = pixels[i].b * clr.b;
			color.a = pixels[i].a;
			pixels[i] = color;
		}
		Texture2D texture2D = new Texture2D(texture.width, texture.height, texture.format, false);
		texture2D.SetPixels(pixels);
		texture2D.Apply(false);
		return texture2D;
	}

	public static void ReduceSize(ref Texture2D texture, int fact)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int width = texture.width;
		int height = texture.height;
		int num5 = width / fact;
		int num6 = height / fact;
		Texture2D texture2D = new Texture2D(num5, num6, texture.format, texture.mipmapCount > 1);
		while (num4 < height)
		{
			int num7 = ((num + 1 >= num5) ? (width - num3) : fact);
			int num8 = ((num2 + 1 >= num6) ? (height - num4) : fact);
			Color[] pixels = texture.GetPixels(num3, num4, num7, num8);
			int num9 = pixels.Length;
			Color color = s_Zero;
			for (int i = 0; i < num9; i++)
			{
				color += pixels[i];
			}
			color /= (float)num9;
			texture2D.SetPixel(num, num2, color);
			num++;
			if (num >= num5)
			{
				num = 0;
				num2++;
			}
			num3 += num7;
			if (num3 >= width)
			{
				num3 = 0;
				num4 += num8;
			}
		}
		UnityEngine.Object.DestroyImmediate(texture);
		texture = texture2D;
	}

	private static Color[] GetPixels(Texture2D texture, int width, int height)
	{
		Color[] array = new Color[width * height];
		if (texture != null)
		{
			Color[] pixels = texture.GetPixels();
			int num = (height - texture.height) / 2;
			int num2 = num + texture.height - 1;
			int num3 = (width - texture.width) / 2;
			int num4 = num3 + texture.width - 1;
			int num5 = 0;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (i < num || i > num2 || j < num3 || j > num4)
					{
						array[i * width + j] = s_Padding;
					}
					else
					{
						array[i * width + j] = pixels[num5++];
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					array[i * width + j] = s_Padding;
				}
			}
		}
		return array;
	}

	private static Color ProcessColor(Color input, Color src, Color dst, EBlendFactor blend)
	{
		Color result = input;
		switch (blend)
		{
		case EBlendFactor.Zero:
			result = s_Zero;
			break;
		case EBlendFactor.One:
			result = input;
			break;
		case EBlendFactor.SrcColor:
			result = input * src;
			break;
		case EBlendFactor.InvSrcColor:
			result = input * (s_One - src);
			break;
		case EBlendFactor.SrcAlpha:
			result = input * src.a;
			break;
		case EBlendFactor.InvSrcAlpha:
			result = input * (1f - src.a);
			break;
		case EBlendFactor.DestAlpha:
			result = input * dst.a;
			break;
		case EBlendFactor.InvDestAlpha:
			result = input * (1f - dst.a);
			break;
		case EBlendFactor.DestColor:
			result = input * dst;
			break;
		case EBlendFactor.InvDestColor:
			result = input * (s_One - src);
			break;
		case EBlendFactor.SrcAlphaSat:
		{
			float num = Mathf.Min(src.a, 1f - dst.a);
			result = input * new Color(num, num, num, 1f);
			break;
		}
		}
		return result;
	}

	public static void CallWWW(MonoBehaviour script, string url, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Url);
		wWWData.Url = url;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, string url, WWWForm form, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Form);
		wWWData.Url = url;
		wWWData.Form = form;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, string url, byte[] data, Hashtable header, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Data);
		wWWData.Url = url;
		wWWData.Datas = data;
		wWWData.Header = header;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, string url, float timeOut, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Url);
		wWWData.Url = url;
		wWWData.TimeOut = timeOut;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, string url, float timeOut, WWWForm form, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Form);
		wWWData.Url = url;
		wWWData.TimeOut = timeOut;
		wWWData.Form = form;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, string url, float timeOut, byte[] data, Hashtable header, CallBackWWWSucceed endDownload, CallBackWWWFailed error, params object[] list)
	{
		WWWData wWWData = new WWWData(EWWWType.Data);
		wWWData.Url = url;
		wWWData.TimeOut = timeOut;
		wWWData.Datas = data;
		wWWData.Header = header;
		wWWData.Succeed = endDownload;
		wWWData.Failed = error;
		wWWData.Parameters = list;
		CallWWW(script, wWWData);
	}

	public static void CallWWW(MonoBehaviour script, WWWData wwwData)
	{
		if (script != null && wwwData != null)
		{
			script.StartCoroutine(WWWCoroutine(script, wwwData));
		}
	}

	public static void StopWWW(MonoBehaviour script)
	{
		if (script != null)
		{
			script.StopAllCoroutines();
		}
	}

	public static IEnumerator WWWCoroutine(MonoBehaviour script, WWWData wwwData)
	{
		WWW www = null;
		switch (wwwData.Type)
		{
		case EWWWType.Url:
			www = new WWW(wwwData.Url);
			break;
		case EWWWType.Form:
			www = new WWW(wwwData.Url, wwwData.Form);
			break;
		case EWWWType.Data:
			www = new WWW(wwwData.Url, wwwData.Datas, wwwData.Header);
			break;
		default:
			Log(ELog.Errors, string.Concat("Type unknown ", wwwData.Type, " ", wwwData.Url));
			yield break;
		}
		if (wwwData.TimeOut <= 0f)
		{
			yield return www;
		}
		else
		{
			float time = wwwData.TimeOut;
			while (!www.isDone)
			{
				time -= Time.deltaTime;
				if (time < 0f)
				{
					break;
				}
				yield return www;
			}
			if (!www.isDone)
			{
				Log(ELog.Debug, "Time Out " + wwwData.Url);
				if (wwwData.Failed != null)
				{
					wwwData.Failed(wwwData.Parameters);
				}
				if (Utility.onWWWFailed != null)
				{
					Utility.onWWWFailed(wwwData.Parameters);
				}
				yield break;
			}
		}
		if (www.error == null)
		{
			if (wwwData.Succeed != null)
			{
				wwwData.Succeed(www, wwwData.Parameters);
			}
			if (Utility.onWWWSucceed != null)
			{
				Utility.onWWWSucceed(wwwData.Parameters);
			}
			yield break;
		}
		Log(ELog.Errors, www.error);
		int nbCall = wwwData.NbCall;
		if (nbCall > 0)
		{
			wwwData.NbCall = nbCall - 1;
			script.StartCoroutine(WWWCoroutine(script, wwwData));
			yield break;
		}
		if (wwwData.Failed != null)
		{
			wwwData.Failed(wwwData.Parameters);
		}
		if (Utility.onWWWFailed != null)
		{
			Utility.onWWWFailed(wwwData.Parameters);
		}
	}

	public static string DecodeHTML(string text)
	{
		byte[] array = new byte[2];
		int num = text.Length - 8;
		while (num >= 0 && text.Length >= 8)
		{
			if (num <= text.Length - 8 && text.Substring(num, 3).Equals("&#x") && text[num + 7] == ';')
			{
				array[0] = byte.Parse(text.Substring(num + 3, 2), NumberStyles.HexNumber);
				array[1] = byte.Parse(text.Substring(num + 5, 2), NumberStyles.HexNumber);
				text = text.Remove(num, 8);
				text = text.Insert(num, Encoding.BigEndianUnicode.GetString(array));
			}
			num--;
		}
		return text;
	}

	public static string FindUrlParam(string url, string param)
	{
		string[] array = url.Split('?');
		if (array.Length != 2)
		{
			return string.Empty;
		}
		array = array[1].Split('&', '=');
		for (int i = 0; i < array.Length; i += 2)
		{
			if (array[i].Equals(param))
			{
				return array[i + 1];
			}
		}
		return string.Empty;
	}

	public static uint ToNumericIp(string ipAddress)
	{
		uint result = 0u;
		IPAddress address;
		if (!string.IsNullOrEmpty(ipAddress) && IPAddress.TryParse(ipAddress, out address))
		{
			byte[] addressBytes = address.GetAddressBytes();
			result = (uint)(addressBytes[0] << 24);
			result += (uint)(addressBytes[1] << 16);
			result += (uint)(addressBytes[2] << 8);
			result += addressBytes[3];
		}
		return result;
	}
}
