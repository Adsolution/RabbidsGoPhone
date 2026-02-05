using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using UnityEngine;

public class OutWorld2D : MonoBehaviour, SmallXmlParser.IContentHandler
{
	public enum EState
	{
		Idle = 0,
		Photo = 1,
		Video = 2,
		Wallpaper = 3,
		Facebook = 4,
		Stripe = 5,
		Count = 6
	}

	public enum ESubState
	{
		Idle = 0,
		Viewer = 1,
		Share = 2,
		Count = 3
	}

	public enum ETravelling
	{
		FadeIn = 0,
		Idle = 1,
		FadeOut = 2,
		Count = 3
	}

	public enum ECollider
	{
		Photo = 0,
		Video = 1,
		Wallpaper = 2,
		Facebook = 3,
		Stripe = 4,
		Count = 5
	}

	public enum EAudioSource
	{
		BG = 0,
		FX = 1,
		Count = 2
	}

	private enum EPlane
	{
		Idle = 0,
		Collider = 1,
		Front = 2,
		Photo = 3,
		Stripe = 4,
		Video = 5,
		Wallpaper = 6,
		Count = 7
	}

	private struct SHighlighter
	{
		public Material Material;

		public float Alpha;

		public float ReleaseTime;

		public Rect TextRect;

		public string LocString;
	}

	public enum EHighlight
	{
		Wallpaper = 0,
		Comic = 1,
		Movie = 2,
		Facebook = 3,
		Photo = 4,
		Count = 5
	}

	public enum EScroller
	{
		Photo = 0,
		Video = 1,
		Wallpaper = 2,
		Stripe = 3,
		Count = 4
	}

	public enum EPosition
	{
		None = 0,
		Previous = 1,
		Next = 2
	}

	public enum EMessage
	{
		EmptyStore = 0,
		ProductListRequestFailed = 1,
		PurchaseFailed = 2,
		PurchaseFailedClientInvalid = 3,
		PurchaseFailedPaymentCancelled = 4,
		PurchaseFailedPaymentInvalid = 5,
		PurchaseFailedPaymentNotAllowed = 6,
		NetNotReachable = 7,
		PurchaseDisable = 8,
		PurchaseSuccess = 9,
		PictureSent = 10,
		PictureSendingFailed = 11,
		SavedInAlbum = 12,
		YouNeedEmailAccount = 13,
		None = 14
	}

	public enum EScrollState
	{
		Idle = 0,
		StartTouch = 1,
		TouchSlide = 2
	}

	public enum EStripe
	{
		Balloons = 0,
		Cable = 1,
		LakeMoon = 2,
		NewYear = 3,
		Rocket = 4,
		Santa = 5,
		Soda = 6,
		Web = 7,
		Count = 8
	}

	public enum EWallpaper
	{
		Wallpaper1 = 0,
		Wallpaper2 = 1,
		Wallpaper3 = 2,
		Wallpaper4 = 3,
		Wallpaper5 = 4,
		Wallpaper6 = 5,
		Wallpaper7 = 6,
		Wallpaper8 = 7,
		Wallpaper9 = 8,
		Wallpaper10 = 9,
		Wallpaper11 = 10,
		Wallpaper12 = 11,
		Wallpaper13 = 12,
		Wallpaper14 = 13,
		Wallpaper15 = 14,
		Wallpaper16 = 15,
		Wallpaper17 = 16,
		Wallpaper18 = 17,
		Wallpaper19 = 18,
		INVASION_AW3D_RAB10_0057_Resized = 19,
		INVASION_AW3D_RAB10_0061_Resized = 20,
		SEASON_AW3D_RAB10_0178_Resized = 21,
		SEASON_AW3D_RAB10_0179_Resized = 22,
		SEASON_AW3D_RAB10_0180_Resized = 23,
		SEASON_AW3D_RAB10_0181_Resized = 24,
		SPORT_AW3D_RAB11_0196_Resized = 25,
		TRAVEL_INVASION_AW3D_RAB07_0187_Resized = 26,
		TV_AW3D_RAB08_0041_Resized = 27,
		TWM_AW3D_RAB10_0124_Resized = 28,
		Astronaut = 29,
		Cowboy = 30,
		Cleopatra = 31,
		Hippy = 32,
		Knight = 33,
		Count = 34
	}

	public enum EThumbnail
	{
		Thumbnail0 = 0,
		Thumbnail1 = 1,
		Thumbnail2 = 2,
		Thumbnail3 = 3
	}

	public enum ECheck
	{
		Idle = 0,
		CheckDate = 1,
		CheckTitle = 2,
		CheckDesc = 3,
		Count = 4
	}

	public struct SVideo
	{
		public string Url;

		public string ThumbnailUrl;

		public string ThumbnailUrlBig;

		public string Tag;

		public string Title;

		public string Desc;

		public string Date;

		public int Duration;
	}

	private const float m_AutoHideTime = 2f;

	public GUISkin m_CommonSkin;

	public GUISkin m_ScrollerSkin;

	public GUISkin m_OutWorldSkin;

	public GUISkin m_InterfaceSkin;

	public Camera m_Camera;

	public AudioClip m_BackSound;

	public AudioClip m_ValidSound;

	public AudioClip m_BackgroundSound;

	private AudioSource[] m_Audio;

	private Texture2D m_NewTexture_120x90;

	private Texture2D m_NewTexture_110x128;

	private EState m_NextState = EState.Count;

	private EState m_State = EState.Count;

	private EState m_PrevState = EState.Count;

	private ESubState m_SubState;

	private ETravelling m_Travelling = ETravelling.Idle;

	private ETravelling m_PrevTravelling = ETravelling.Idle;

	private float m_StateTime;

	private AchievementTable m_AchievementsTable;

	private EMessage m_Message = EMessage.None;

	private int m_CurrentImage = -1;

	private GUIStyle m_TextContent;

	private GUIStyle m_BlackTextContent;

	private TextAnchor m_OldAlignement;

	private EScrollState m_ScrollState;

	private Vector2 m_ScrollPos = Vector2.zero;

	private string m_CenterTextContent = "CenterTextContent";

	private string m_TitleContent = "CenterTextContent";

	private int m_BackPressed = -1;

	private Camera m_Camera_3D;

	private GameObject[] m_Dummies = new GameObject[6];

	private EState m_CameraFrom;

	private EState m_CameraTo;

	private float m_CameraTime;

	private float m_CameraTimer = 1.6f;

	private int m_CameraWait = 10;

	private GameObject[] m_Planes = new GameObject[7];

	private Material m_IdlePlaneMat;

	private static float s_HighlightTime = 0.5f;

	private static float s_HighlightTimeMin = 0.7f;

	private static float s_HighlightTimeCycle = 1.4f;

	private GameObject m_IdleHighlightRoot;

	private SHighlighter[] m_IdleHighlights = new SHighlighter[5];

	private EHighlight m_HighlightCurrent;

	private float m_HighlightTime;

	private int m_PhotoDownloading;

	private List<string> m_PhotosFileName = new List<string>();

	private bool m_ConfirmDeletion;

	private string m_LastPhotoFilename = string.Empty;

	private EScroller m_CurrentScroller = EScroller.Count;

	private GUIScroller m_Scroller;

	private List<Texture2D>[] m_AllThumbnails = new List<Texture2D>[4];

	private List<Texture2D> m_VideoThumbnails = new List<Texture2D>();

	private string m_PhotoPath = string.Empty;

	private string m_MiniPhotoPath = string.Empty;

	private float m_AutoHideTimer = -1f;

	private string m_TemporaryFileName = "image";

	private Rect m_FaceRect = default(Rect);

	private Rect m_SaveRect = default(Rect);

	private Rect m_DeleteRect = default(Rect);

	private bool m_StartShare = true;

	private string m_PictureFileName = string.Empty;

	private bool m_AuthorizeFacebook = true;

	private string m_Site = string.Empty;

	private bool m_Store;

	private float m_LabelHeight = 25f;

	private Vector2 m_DescriptionScrollPos = Vector2.zero;

	private Rect m_StoreTitleRect;

	private Rect m_StoreDescriptionRect;

	private Rect m_StorePriceRect;

	private Rect m_StorePriceSymbolRect;

	private Rect m_StoreBuyRect;

	private string m_CurrencySymbol = string.Empty;

	private Texture2D m_CurrencySymbolTex;

	private string m_PriceTextStyle = "PriceText";

	//private List<StoreKitProduct> m_ProductList;

	private bool m_CanMakePayments;

	private static float s_StripeScaleFactor = 3.8441f;

	private Texture2D m_StripeTex;

	private Vector3 m_StripeBeganPos = Vector3.zero;

	private Vector3 m_StripeMovedPos = Vector3.zero;

	private Vector3 m_StripeWantedPos = Vector3.zero;

	private Vector3 m_StripeCurrentPos = Vector3.zero;

	private Vector3 m_StripeStartPos = Vector3.zero;

	private Vector3 m_StripeOriginalPos = Vector3.zero;

	private bool m_CheckInput = true;

	private float m_StripeLimit;

	private static float s_VideoLoadingTimeLimit = 25f;

	private Texture2D m_VideoThumbnail;

	private Rect m_VideoTitleRect = default(Rect);

	private Rect m_VideoTimeRect = default(Rect);

	private Rect m_VideoDescScrollRect = default(Rect);

	private bool m_VideoBegan;

	public Texture2D m_BlackScrollThumb;

	private Vector2 m_TitleScrollPos = Vector2.zero;

	private EScrollState m_TitleScrollState;

	private EThumbnail m_UsingThumbnail = EThumbnail.Thumbnail2;

	private List<SVideo> m_Videos = new List<SVideo>();

	private List<int> m_Indices = new List<int>();

	private List<int> m_IndicesToDelete = new List<int>();

	private SVideo m_CurrentVideo;

	private int m_CurrentThumbnail;

	private int m_ThumbnailDownloading;

	private ECheck m_Check;

	public void Start()
	{
		GlobalVariables.SoundEnabled = PlayerPrefs.GetInt("_sound_enable");
		m_AchievementsTable = new AchievementTable();
		m_Audio = base.gameObject.GetComponents<AudioSource>();
		m_Audio[0].clip = m_BackgroundSound;
		if (GlobalVariables.SoundEnabled != 0)
		{
			m_Audio[0].Play();
			m_Audio[0].loop = true;
		}
		string text = Localization.GetLanguage().ToString();
		m_NewTexture_120x90 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/" + text + "/new_120x90");
		m_NewTexture_110x128 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/" + text + "/new_110x128");
		if (Utility.IsHD())
		{
			m_TitleContent = "TextTitle";
			m_CenterTextContent = "BigCenterTextContent";
			m_TextContent = m_CommonSkin.GetStyle("BigTextContent");
			m_BlackTextContent = m_CommonSkin.GetStyle("BigBlackTextContent");
		}
		else
		{
			m_TitleContent = "CenterTextContent";
			m_CenterTextContent = "CenterTextContent";
			m_TextContent = m_CommonSkin.GetStyle("TextContent");
			m_BlackTextContent = m_CommonSkin.GetStyle("BlackTextContent");
		}
		if (Utility.IsFourthGenAppleDevice())
		{
			m_TitleContent = "TextTitle";
			m_CenterTextContent = "HugeCenterTextContent";
			m_TextContent = m_CommonSkin.GetStyle("HugeTextContent");
			m_BlackTextContent = m_CommonSkin.GetStyle("HugeBlackTextContent");
		}
		StartSocialNetworking();
		StartShare();
		StartHelper();
		StartCamera();
		StartScroller();
		StartIdle();
		StartVideo();
		StartFacebook();
		StartStripe();
		StartPhoto();
		StartStore();
		StartEtcetera();
		StartStoreKit();
		if (PlayerPrefs.HasKey("GotoPhoto"))
		{
			SetState(EState.Photo);
		}
		else if (PlayerPrefs.HasKey("GotoVideo"))
		{
			SetState(EState.Video);
		}
		else
		{
			SetState(EState.Idle);
		}
	}

	private void Update()
	{
		if (m_PrevTravelling != m_Travelling && m_Travelling == ETravelling.Idle)
		{
			Utility.FreeMem(true);
		}
		m_PrevTravelling = m_Travelling;
		UpdateHelper();
		UpdateWorld();
		UpdateScrolling();
		UpdateStore();
	}

	private void FixedUpdate()
	{
		if (m_BackPressed >= 0)
		{
			m_BackPressed--;
		}
		UpdateCamera();
	}

	private void OnGUI()
	{
		if ((bool)m_CommonSkin)
		{
			GUI.skin = m_CommonSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "OutWorld Hud: Skin not found");
		}
		switch (m_State)
		{
		case EState.Idle:
			IdleGUI();
			break;
		case EState.Photo:
			PhotoGUI();
			break;
		case EState.Wallpaper:
			WallpaperGUI();
			break;
		case EState.Stripe:
			StripeGUI();
			break;
		case EState.Facebook:
			FacebookGUI();
			break;
		case EState.Video:
			VideoGUI();
			break;
		}
		StoreGUI();
		ScrollerGUI();
		if (m_Message != EMessage.None)
		{
			ShowMessage();
		}
	}

	private void OnDisable()
	{
		OnDisableScroller();
		OnDisableSocialNetworking();
		OnDisableStoreKit();
		OnDisableEtcetera();
		Utility.ClearActivityView();
	}

	private void OnApplicationQuit()
	{
		DataMining.OnApplicationQuit(false);
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			DataMining.OnApplicationQuit(false);
		}
	}

	private void SetNextState(EState State)
	{
		if (State != m_State)
		{
			switch (m_State)
			{
			case EState.Idle:
				OnLeaveIdle();
				break;
			case EState.Photo:
				OnLeavePhoto();
				break;
			case EState.Video:
				OnLeaveVideo();
				break;
			case EState.Wallpaper:
				OnLeaveWallpaper();
				break;
			case EState.Facebook:
				OnLeaveFacebook();
				break;
			case EState.Stripe:
				OnLeaveStripe();
				break;
			}
			m_NextState = State;
			switch (State)
			{
			case EState.Idle:
				OnEnterIdle();
				break;
			case EState.Photo:
				OnEnterPhoto();
				break;
			case EState.Video:
				OnEnterVideo();
				break;
			case EState.Wallpaper:
				OnEnterWallpaper();
				break;
			case EState.Facebook:
				OnEnterFacebook();
				break;
			case EState.Stripe:
				OnEnterStripe();
				break;
			}
		}
	}

	private void SetState(EState State)
	{
		if (State != m_State)
		{
			switch (m_State)
			{
			case EState.Idle:
				OnLeaveIdle();
				break;
			case EState.Photo:
				OnLeavePhoto();
				break;
			case EState.Video:
				OnLeaveVideo();
				break;
			case EState.Wallpaper:
				OnLeaveWallpaper();
				break;
			case EState.Facebook:
				OnLeaveFacebook();
				break;
			case EState.Stripe:
				OnLeaveStripe();
				break;
			}
			Utility.Log(ELog.GUI, string.Concat("SetState: ", m_State, " -> ", State, " ; Time: ", m_StateTime));
			m_PrevState = m_State;
			m_State = State;
			m_StateTime = 0f;
			MoveCameraTo(m_PrevState, m_State);
			switch (m_State)
			{
			case EState.Idle:
				OnEnterIdle();
				break;
			case EState.Photo:
				OnEnterPhoto();
				break;
			case EState.Video:
				OnEnterVideo();
				break;
			case EState.Wallpaper:
				OnEnterWallpaper();
				break;
			case EState.Facebook:
				OnEnterFacebook();
				break;
			case EState.Stripe:
				OnEnterStripe();
				break;
			}
		}
	}

	private void SetSubState(ESubState state)
	{
		m_SubState = state;
	}

	private void UpdateWorld()
	{
		if (m_Message == EMessage.None)
		{
			switch (m_State)
			{
			case EState.Idle:
				OnUpdateIdle();
				break;
			case EState.Photo:
				OnUpdatePhoto();
				break;
			case EState.Video:
				OnUpdateVideo();
				break;
			case EState.Wallpaper:
				OnUpdateWallpaper();
				break;
			case EState.Facebook:
				OnUpdateFacebook();
				break;
			case EState.Stripe:
				OnUpdateStripe();
				break;
			}
		}
	}

	private string RaycastUnderFinger(int fingerID)
	{
		if (m_Camera_3D == null)
		{
			return string.Empty;
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo))
		{
			return hitInfo.collider.name;
		}
		return null;
	}

	private ECollider GetCollider(string cast)
	{
		if (cast == null)
		{
			return ECollider.Count;
		}
		if (cast.Contains("Object126") || cast.Contains("Title_02"))
		{
			return ECollider.Photo;
		}
		if (cast.Contains("Cinema".ToUpper()) || cast.Contains("Title_01"))
		{
			return ECollider.Video;
		}
		if (cast.Contains("Wallpaper".ToUpper()) || cast.Contains("Title_03"))
		{
			return ECollider.Wallpaper;
		}
		if ((cast.Contains("Box071") || cast.Contains("Title_04")) && !Utility.Chinese)
		{
			return ECollider.Facebook;
		}
		if (cast.Contains("building_02"))
		{
			return ECollider.Stripe;
		}
		return ECollider.Count;
	}

	private void PlayAudioClip(AudioClip clip, Vector3 position, float volume)
	{
		if (GlobalVariables.SoundEnabled == 1)
		{
			m_Audio[1].Stop();
			m_Audio[1].clip = clip;
			m_Audio[1].time = 0f;
			m_Audio[1].Play();
		}
	}

	private void StopAudioSource(EAudioSource source)
	{
		m_Audio[(int)source].Stop();
	}

	private void PlayAudioSource(EAudioSource source)
	{
		if (GlobalVariables.SoundEnabled == 1)
		{
			m_Audio[(int)source].time = 0f;
			m_Audio[(int)source].Play();
		}
	}

	private void DeletePhoto()
	{
		int num = 0;
		Utility.DeleteFile(m_MiniPhotoPath + m_PhotosFileName[m_CurrentImage]);
		Utility.DeleteFile(m_PhotoPath + m_PhotosFileName[m_CurrentImage]);
		m_AllThumbnails[num].RemoveAt(m_CurrentImage);
		m_PhotosFileName.RemoveAt(m_CurrentImage);
		GUIUtils.GetScroller(num).Resize(m_AllThumbnails[num].ToArray(), 0);
	}

	private void ShowMessage()
	{
		GUI.skin = m_CommonSkin;
		switch (m_Message)
		{
		case EMessage.NetNotReachable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.YouNeedInternet)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PictureSendingFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PictureSendingFailed)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PictureSent:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PictureSent)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.SavedInAlbum:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.SavedInAlbum)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.YouNeedEmailAccount:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.YouNeedEmailAccount)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.EmptyStore:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.EmptyStore)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.ProductListRequestFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.ProductListRequestFailed)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseFailed)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailedClientInvalid:
			if (GUIUtils.DrawError("Client Invalid"))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailedPaymentCancelled:
			if (GUIUtils.DrawError("Payment Cancelled"))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailedPaymentInvalid:
			if (GUIUtils.DrawError("Payment Invalid"))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailedPaymentNotAllowed:
			if (GUIUtils.DrawError("Payment Not Allowed"))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseDisable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseDisable)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseSuccess:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseSucceed)))
			{
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				SetMessage(EMessage.None);
			}
			break;
		}
	}

	private void SetMessage(EMessage message)
	{
		if (m_Message == EMessage.None || message == EMessage.None)
		{
			Utility.StopInputByActivity(message != EMessage.None);
		}
		m_Message = message;
	}

	private void StartCamera()
	{
		GameObject gameObject = GameObject.Find("Camera_Perspective");
		if (gameObject != null)
		{
			m_Camera_3D = gameObject.GetComponent<Camera>();
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to find Camera_Perspective");
		}
		gameObject = GameObject.Find("Dummies");
		if (gameObject != null)
		{
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				Transform child = gameObject.transform.GetChild(i);
				if (child != null)
				{
					EState stateEnum = GetStateEnum(child.name);
					if (stateEnum != EState.Count)
					{
						m_Dummies[(int)stateEnum] = child.gameObject;
					}
					else
					{
						Utility.Log(ELog.Errors, "Unexpected dummy: " + child.name);
					}
				}
				else
				{
					Utility.Log(ELog.Errors, "child is null");
				}
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to find Dummies");
		}
	}

	private void UpdateCamera()
	{
		if (m_Travelling == ETravelling.Idle || m_Travelling == ETravelling.Count)
		{
			return;
		}
		if (m_CameraWait > 0)
		{
			m_CameraWait--;
			if (m_CameraWait > 0)
			{
				return;
			}
		}
		m_CameraTime += Time.deltaTime;
		Transform transform = m_Dummies[(int)m_CameraFrom].transform;
		Transform transform2 = m_Dummies[(int)m_CameraTo].transform;
		float num = m_CameraTime / m_CameraTimer;
		m_Camera_3D.transform.localPosition = (transform2.localPosition - transform.localPosition) * num + transform.localPosition;
		if (m_IdlePlaneMat != null)
		{
			if (m_CameraFrom == EState.Idle)
			{
				num = Mathf.Clamp(1f - num, 0f, 1f);
			}
			Color color = m_IdlePlaneMat.color;
			color.a = num;
			m_IdlePlaneMat.color = color;
			SetIdleHighlightGlobalAlpha(num);
		}
	}

	private void MoveCameraTo(EState from, EState to)
	{
		bool flag = false;
		m_CameraFrom = from;
		m_CameraTo = to;
		if (m_CameraFrom == EState.Count)
		{
			m_CameraFrom = EState.Idle;
			flag = true;
		}
		if (m_CameraTo == EState.Count)
		{
			m_CameraTo = EState.Idle;
		}
		if (m_CameraFrom == EState.Idle && m_CameraTo != EState.Idle)
		{
			m_Travelling = ETravelling.FadeIn;
		}
		else
		{
			m_Travelling = ETravelling.FadeOut;
		}
		m_CameraTime = 0f;
		m_CameraWait = 1;
		if (flag)
		{
			m_CameraTime = 1.2f;
		}
	}

	private bool IsTravelling()
	{
		return m_CameraTime < m_CameraTimer;
	}

	private void StartEtcetera()
	{/*
		EtceteraManager.mailComposerFinished += OnMailComposerFinished;
		EtceteraManager.saveImageAlbumComplete += OnSaveImageAlbumComplete;
		EtceteraManager.saveImageAlbumFailed += OnSaveImageAlbumFailed;
		EtceteraManager.dismissView += OnDismissView;
		EtceteraAndroidManager.webViewCancelledEvent += OnDismissView;*/
	}

	private void OnDisableEtcetera()
	{/*
		EtceteraManager.mailComposerFinished -= OnMailComposerFinished;
		EtceteraManager.saveImageAlbumComplete -= OnSaveImageAlbumComplete;
		EtceteraManager.saveImageAlbumFailed -= OnSaveImageAlbumFailed;
		EtceteraManager.dismissView -= OnDismissView;
		EtceteraAndroidManager.webViewCancelledEvent -= OnDismissView;*/
	}

	private void OnDismissView()
	{
		switch (m_State)
		{
		case EState.Video:
			AllInput.ActivateAutoRotateFrame(false);
			AllInput.ActivateAutoRotateScreen(false, true);
			PlayAudioSource(EAudioSource.BG);
			break;
		case EState.Idle:
			PlayAudioSource(EAudioSource.BG);
			break;
		case EState.Facebook:
			AllInput.ActivateAutoRotateFrame(false);
			AllInput.ActivateAutoRotateScreen(false, true);
			MoveCameraTo(EState.Facebook, EState.Idle);
			ActivatePlane(EPlane.Idle, true);
			ActivateScroller(EScroller.Count);
			PlayAudioSource(EAudioSource.BG);
			break;
		}
		Utility.ClearActivityView();
	}

	private void OnMailComposerFinished(string txt)
	{
		AllInput.ActivateAutoRotateFrame(false);
		switch (txt)
		{
		case "Sent":
			SetMessage(EMessage.PictureSent);
			if (m_CurrentScroller == EScroller.Photo)
			{
				DataMining.Increment(DataMining.EGeneral.PhotoPostedByMail.ToString());
			}
			break;
		case "Saved":
			Utility.FreeMem();
			break;
		case "Cancelled":
			break;
		default:
			SetMessage(EMessage.PictureSendingFailed);
			break;
		}
	}

	private void SendByMail(Texture2D tex)
	{
		if (tex != null && IsEmailAvailable())
		{
			AllInput.ActivateAutoRotateFrame(true);
			byte[] bytes = tex.EncodeToPNG();
			string text = Path.Combine(Utility.GetPersistentDataPath(), m_TemporaryFileName + ".png");
			File.WriteAllBytes(text, bytes);
			if (m_State == EState.Photo)
			{
				ShowEmailComposer(text, "image/png", Localization.GetLocalizedText(ELoc.Photo) + ".png", string.Empty, Localization.GetLocalizedText(ELoc.CheckOutMyRabbidPhoto), Localization.GetLocalizedText(ELoc.CheckOutMyRabbidPhoto), false);
			}
			else
			{
				ShowEmailComposer(text, "image/png", Localization.GetLocalizedText(ELoc.Photo) + ".png", string.Empty, Localization.GetLocalizedText(ELoc.CheckOutMyRabbidWallpaper), Localization.GetLocalizedText(ELoc.CheckOutCoolWallpaper), false);
			}
		}
		else
		{
			SetMessage(EMessage.YouNeedEmailAccount);
		}
	}

	private bool IsEmailAvailable()
	{
		return true;
	}

	private void ShowEmailComposer(string filePathToAttachment, string attachmentMimeType, string attachmentFilename, string toAddress, string subject, string body, bool isHTML)
	{
		Utility.Log(ELog.Device, "ShowEmailComposer starts");
		//EtceteraAndroid.showEmailComposer(toAddress, subject, body, isHTML, filePathToAttachment);
		Utility.Log(ELog.Device, "ShowEmailComposer ends");
	}

	private void ShowWebPage(string url)
	{
		//EtceteraAndroid.showWebView(string.Empty, url);
	}

	private void ShowYoutube(string url)
	{
		Debug.Log("screen dpi : " + Screen.dpi);
		Application.OpenURL(url);
	}

	private void OnSaveImageAlbumComplete()
	{
		if (m_CurrentScroller == EScroller.Photo)
		{
			DataMining.Increment(DataMining.EGeneral.PhotoSaved.ToString());
		}
		else if (m_CurrentScroller == EScroller.Wallpaper)
		{
			DataMining.Increment(DataMining.EGeneral.WallpaperSaved.ToString());
		}
		SetMessage(EMessage.SavedInAlbum);
		Utility.HideActivityView(true);
	}

	private void OnSaveImageAlbumFailed(string error)
	{
		Utility.Log(ELog.Errors, error);
		SetMessage(EMessage.PictureSendingFailed);
		Utility.HideActivityView(true);
	}

	private void SaveInImageAlbum(Texture2D tex, string filename)
	{
		if (tex != null)
		{
			Utility.ShowActivityView(true);
			byte[] bytes = tex.EncodeToPNG();
			string text = Path.Combine(Application.persistentDataPath, "myImage.png");
			File.WriteAllBytes(text, bytes);
			/*if (EtceteraAndroid.saveImageToGallery(text, "My image from Unity"))
			{
				OnSaveImageAlbumComplete();
			}
			else
			{
				OnSaveImageAlbumFailed("unknown android error");
			}*/
		}
		else
		{
			SetMessage(EMessage.PictureSendingFailed);
		}
	}

	private void StartFacebook()
	{
	}

	private void FacebookGUI()
	{
		if (m_Travelling != ETravelling.Idle)
		{
		}
	}

	private void OnEnterFacebook()
	{
	}

	private void OnUpdateFacebook()
	{
		switch (m_Travelling)
		{
		case ETravelling.FadeIn:
			if (!IsTravelling())
			{
				m_Travelling = ETravelling.Idle;
				StopAudioSource(EAudioSource.BG);
				AllInput.ActivateAutoRotateFrame(true);
				AllInput.ActivateAutoRotateScreen(true, true);
				ShowFacebookSite();
			}
			break;
		case ETravelling.Idle:
			break;
		case ETravelling.FadeOut:
			if (!IsTravelling())
			{
				SetState(EState.Idle);
			}
			break;
		}
	}

	private void OnLeaveFacebook()
	{
	}

	private void StartHelper()
	{
		for (EPlane ePlane = EPlane.Idle; ePlane < EPlane.Count; ePlane++)
		{
			m_Planes[(int)ePlane] = GameObject.Find(ePlane.ToString() + "Plane");
			if (m_Planes[(int)ePlane] != null)
			{
				Utility.SetLayerRecursivly(m_Planes[(int)ePlane].transform, 11);
				m_Planes[(int)ePlane].active = false;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unable to find '" + ePlane.ToString() + "Plane'");
			}
		}
		if (m_Planes[0] != null)
		{
			m_IdlePlaneMat = (Material)Object.Instantiate(m_Planes[0].GetComponent<Renderer>().material);
			m_Planes[0].GetComponent<Renderer>().material = m_IdlePlaneMat;
			m_IdlePlaneMat.color = new Color(1f, 1f, 1f, 1f);
		}
		if (m_Planes[1] != null)
		{
			Utility.SetLayerRecursivly(m_Planes[1].transform, 10);
		}
	}

	private void UpdateHelper()
	{
	}

	private void SetTextureOnMaterial(Material mat, string texName)
	{
		if (mat != null)
		{
			texName = Utility.GetTexPath() + texName;
			Texture2D texture2D = Utility.LoadResource<Texture2D>(texName);
			if (texture2D != null)
			{
				mat.mainTexture = texture2D;
			}
		}
	}

	private void SetTexture(EPlane plane, string texName)
	{
		if (plane == EPlane.Count)
		{
			return;
		}
		ClearPlaneTexture(plane, false);
		texName = Utility.GetTexPath() + texName;
		Texture2D texture2D = Utility.LoadResource<Texture2D>(texName);
		if (texture2D != null)
		{
			m_LastPhotoFilename = texName;
			if (plane == EPlane.Idle)
			{
				m_IdlePlaneMat.mainTexture = texture2D;
			}
			else
			{
				m_Planes[(int)plane].GetComponent<Renderer>().material.mainTexture = texture2D;
			}
			m_Planes[(int)plane].active = true;
		}
	}

	private void SetTexture(EPlane plane, Texture2D tex)
	{
		if (plane != EPlane.Count)
		{
			ClearPlaneTexture(plane, false);
			if (plane == EPlane.Idle)
			{
				m_IdlePlaneMat.mainTexture = tex;
			}
			else
			{
				m_Planes[(int)plane].GetComponent<Renderer>().material.mainTexture = tex;
			}
			m_Planes[(int)plane].active = true;
		}
	}

	private void ClearPlaneTexture(EPlane plane, bool freeMem)
	{
		if (plane != EPlane.Count && plane != EPlane.Idle && m_Planes[(int)plane] != null)
		{
			m_Planes[(int)plane].GetComponent<Renderer>().material.mainTexture = null;
			m_Planes[(int)plane].active = false;
		}
		if (freeMem)
		{
			Utility.FreeMem();
		}
	}

	private void ClearPlaneTexture(EPlane plane)
	{
		if (plane == EPlane.Count)
		{
			for (plane = EPlane.Front; plane < EPlane.Count; plane++)
			{
				ClearPlaneTexture(plane, false);
			}
			Utility.FreeMem(false);
		}
		else
		{
			ClearPlaneTexture(plane, true);
		}
	}

	private string RaycastHUDUnderFinger(int fingerID)
	{
		if (m_Scroller != null && m_Scroller.IsInteract(fingerID))
		{
			return "None";
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 1000f, 2048))
		{
			return hitInfo.collider.name;
		}
		return string.Empty;
	}

	private bool GetContactPointOnInteractivePlane(int fingerID, out Vector3 contactPoint)
	{
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		contactPoint = Vector3.zero;
		if (Physics.Raycast(ray, out hitInfo, 1000f, 1024))
		{
			contactPoint = hitInfo.point;
			return true;
		}
		return false;
	}

	private void ActivatePlane(EPlane plane, bool b)
	{
		if (plane != EPlane.Count && m_Planes[(int)plane] != null)
		{
			m_Planes[(int)plane].active = b;
		}
	}

	private void ComputeAndSetRatio(EPlane plane)
	{
		if (plane == EPlane.Count)
		{
			return;
		}
		GameObject gameObject = m_Planes[(int)plane];
		if (gameObject != null && gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<Renderer>().material != null)
		{
			Texture mainTexture = gameObject.GetComponent<Renderer>().material.mainTexture;
			if (mainTexture != null)
			{
				float num = (float)mainTexture.width / (float)mainTexture.height;
				Vector3 localScale = gameObject.transform.localScale;
				localScale.x = localScale.z * num;
				gameObject.transform.localScale = localScale;
			}
		}
	}

	public static EState GetStateEnum(string str)
	{
		for (EState eState = EState.Idle; eState < EState.Count; eState++)
		{
			if (str.Equals(eState.ToString()))
			{
				return eState;
			}
		}
		return EState.Count;
	}

	private void StartIdle()
	{
		StartIdleHighlight();
	}

	private void IdleGUI()
	{
		IdleHighlightGUI();
		if (m_Message == EMessage.None)
		{
			GUI.skin = m_CommonSkin;
			if (GUIUtils.DrawBackButton() && m_BackPressed < 0)
			{
				m_BackPressed = 5;
				Localization.GenerateWaitText();
				Application.LoadLevel("MainMenu");
			}
		}
	}

	private void OnEnterIdle()
	{
		m_Travelling = ETravelling.Idle;
		ClearPlaneTexture(EPlane.Count);
		SetTexture(EPlane.Idle, "OutWorld2D/OutWorld_Main");
		OnEnterIdleHighlight();
	}

	private void OnUpdateIdle()
	{
		if (m_Scroller != null)
		{
			ActivateScroller(EScroller.Count);
		}
		UpdateIdleHighlight();
	}

	private void OnLeaveIdle()
	{
		OnLeaveIdleHighlight();
	}

	private void OnEndLeaveIdle()
	{
	}

	private void StartIdleHighlight()
	{
		m_IdleHighlightRoot = GameObject.Find("Highlight");
		if (m_IdleHighlightRoot == null)
		{
			Utility.Log(ELog.Errors, "m_IdleHighlightRoot == null");
		}
		for (EHighlight eHighlight = EHighlight.Wallpaper; eHighlight < EHighlight.Count; eHighlight++)
		{
			GameObject gameObject = GameObject.Find(eHighlight.ToString() + "Highlight");
			m_IdleHighlights[(int)eHighlight].Material = gameObject.GetComponent<Renderer>().material;
			m_IdleHighlights[(int)eHighlight].Alpha = 0f;
			m_IdleHighlights[(int)eHighlight].ReleaseTime = 3f;
			m_IdleHighlights[(int)eHighlight].Material.color = new Color(1f, 1f, 1f, 0f);
			switch (eHighlight)
			{
			case EHighlight.Comic:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(160f, 68f, 125f, 30f);
				m_IdleHighlights[(int)eHighlight].LocString = Localization.GetLocalizedText(ELoc.Stripes);
				break;
			case EHighlight.Facebook:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(110f, 172f, 130f, 30f);
				m_IdleHighlights[(int)eHighlight].LocString = Localization.GetLocalizedText(ELoc.Facebook);
				break;
			case EHighlight.Movie:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(185f, 175f, 130f, 30f);
				m_IdleHighlights[(int)eHighlight].LocString = Localization.GetLocalizedText(ELoc.Videos);
				break;
			case EHighlight.Photo:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(15f, 163f, 130f, 30f);
				m_IdleHighlights[(int)eHighlight].LocString = Localization.GetLocalizedText(ELoc.Photos);
				break;
			case EHighlight.Wallpaper:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(5f, 100f, 130f, 30f);
				m_IdleHighlights[(int)eHighlight].LocString = Localization.GetLocalizedText(ELoc.Wallpapers);
				break;
			}
		}
	}

	private void UpdateIdleHighlight()
	{
		if (m_Travelling != ETravelling.Idle || m_Message != EMessage.None || Utility.InputStoppedByActivity())
		{
			return;
		}
		float num = Time.deltaTime / s_HighlightTime;
		if (AllInput.GetTouchCount() == 1 && m_NextState == EState.Count)
		{
			EHighlight eHighlight = RaycastHighlightUnderFinger(0);
			if (eHighlight != EHighlight.Count && AllInput.GetState(0) == AllInput.EState.Began)
			{
				switch (eHighlight)
				{
				case EHighlight.Comic:
					SetNextState(EState.Stripe);
					break;
				case EHighlight.Facebook:
					if (!Utility.Chinese)
					{
						if (!AllInput.IsInternetReachable())
						{
							SetMessage(EMessage.NetNotReachable);
						}
						else
						{
							SetState(EState.Facebook);
						}
					}
					break;
				case EHighlight.Movie:
					if (!AllInput.IsInternetReachable())
					{
						SetMessage(EMessage.NetNotReachable);
					}
					else
					{
						SetNextState(EState.Video);
					}
					break;
				case EHighlight.Photo:
					SetNextState(EState.Photo);
					break;
				case EHighlight.Wallpaper:
					SetNextState(EState.Wallpaper);
					break;
				}
			}
		}
		OnUpdateIdleHighlight();
		if (m_NextState == EState.Count)
		{
			for (int i = 0; i < 5; i++)
			{
				m_IdleHighlights[i].ReleaseTime += Time.deltaTime;
				if (m_IdleHighlights[i].ReleaseTime < s_HighlightTimeMin)
				{
					m_IdleHighlights[i].Alpha += num;
				}
				else
				{
					m_IdleHighlights[i].Alpha -= num;
				}
				m_IdleHighlights[i].Alpha = Mathf.Clamp(m_IdleHighlights[i].Alpha, 0f, 1f);
				m_IdleHighlights[i].Material.color = new Color(1f, 1f, 1f, m_IdleHighlights[i].Alpha);
			}
			return;
		}
		bool flag = true;
		for (int i = 0; i < 5; i++)
		{
			m_IdleHighlights[i].Alpha -= num;
			if (m_IdleHighlights[i].Alpha > 0f)
			{
				flag = false;
			}
			m_IdleHighlights[i].Alpha = Mathf.Clamp(m_IdleHighlights[i].Alpha, 0f, 1f);
			m_IdleHighlights[i].Material.color = new Color(1f, 1f, 1f, m_IdleHighlights[i].Alpha);
		}
		if (flag)
		{
			Utility.Log(ELog.GUI, string.Concat("SetState: ", m_State, " -> ", m_NextState, " ; Time: ", m_StateTime));
			m_PrevState = m_State;
			m_State = m_NextState;
			m_NextState = EState.Count;
			m_StateTime = 0f;
			MoveCameraTo(m_PrevState, m_State);
		}
	}

	private void IdleHighlightGUI()
	{
		Color color = GUI.color;
		for (int i = 0; i < 5; i++)
		{
			if (m_IdleHighlights[i].Material != null)
			{
				GUI.color = m_IdleHighlights[i].Material.color;
				GUI.Label(m_IdleHighlights[i].TextRect, m_IdleHighlights[i].LocString, m_TitleContent);
			}
		}
		GUI.color = color;
	}

	private void SetIdleHighlightGlobalAlpha(float a)
	{
		for (int i = 0; i < 5; i++)
		{
			m_IdleHighlights[i].Material.color = new Color(1f, 1f, 1f, m_IdleHighlights[i].Alpha * a);
		}
		if (a <= 0.01f)
		{
			if (m_IdleHighlightRoot != null && m_IdleHighlightRoot.active)
			{
				m_IdleHighlightRoot.SetActiveRecursively(false);
			}
		}
		else if (m_IdleHighlightRoot != null && !m_IdleHighlightRoot.active)
		{
			m_IdleHighlightRoot.SetActiveRecursively(true);
		}
	}

	private void OnEnterIdleHighlight()
	{
		for (EHighlight eHighlight = EHighlight.Wallpaper; eHighlight < EHighlight.Count; eHighlight++)
		{
			SetTextureOnMaterial(m_IdleHighlights[(int)eHighlight].Material, "OutWorld2D/Highlight/" + eHighlight.ToString().ToLower());
		}
		if (m_IdleHighlightRoot == null)
		{
			m_IdleHighlightRoot = GameObject.Find("Highlight");
		}
		if (m_IdleHighlightRoot != null)
		{
			m_IdleHighlightRoot.SetActiveRecursively(true);
		}
	}

	private void OnUpdateIdleHighlight()
	{
		m_HighlightTime += Time.deltaTime;
		if (m_HighlightTime > s_HighlightTimeCycle)
		{
			m_HighlightCurrent++;
			if (Utility.Chinese && m_HighlightCurrent == EHighlight.Facebook)
			{
				m_HighlightCurrent++;
			}
			if (m_HighlightCurrent == EHighlight.Count)
			{
				m_HighlightCurrent = EHighlight.Wallpaper;
			}
			m_HighlightTime = 0f;
		}
		m_IdleHighlights[(int)m_HighlightCurrent].ReleaseTime = 0f;
	}

	private void OnLeaveIdleHighlight()
	{
		for (EHighlight eHighlight = EHighlight.Wallpaper; eHighlight < EHighlight.Count; eHighlight++)
		{
			m_IdleHighlights[(int)eHighlight].Material.mainTexture = null;
		}
		if (m_IdleHighlightRoot != null)
		{
			m_IdleHighlightRoot.SetActiveRecursively(false);
		}
	}

	private EHighlight RaycastHighlightUnderFinger(int fingerID)
	{
		if (m_Scroller != null && m_Scroller.IsInteract(fingerID))
		{
			return EHighlight.Count;
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 10000f, 2048))
		{
			return GetHighlight(hitInfo.collider.gameObject.name);
		}
		return EHighlight.Count;
	}

	private EHighlight GetHighlight(string cast)
	{
		for (EHighlight eHighlight = EHighlight.Wallpaper; eHighlight < EHighlight.Count; eHighlight++)
		{
			if (cast.Contains(eHighlight.ToString()))
			{
				return eHighlight;
			}
		}
		return EHighlight.Count;
	}

	private void IdleHighlight_UpdateRects()
	{
		if (m_IdleHighlightRoot == null)
		{
			m_IdleHighlightRoot = GameObject.Find("Highlight");
		}
		for (EHighlight eHighlight = EHighlight.Wallpaper; eHighlight < EHighlight.Count; eHighlight++)
		{
			switch (eHighlight)
			{
			case EHighlight.Comic:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(160f, 68f, 125f, 30f);
				break;
			case EHighlight.Facebook:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(110f, 172f, 130f, 30f);
				break;
			case EHighlight.Movie:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(185f, 175f, 130f, 30f);
				break;
			case EHighlight.Photo:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(15f, 163f, 130f, 30f);
				break;
			case EHighlight.Wallpaper:
				m_IdleHighlights[(int)eHighlight].TextRect = Utility.NewRect(20f, 100f, 130f, 30f);
				break;
			}
		}
	}

	private void StartPhoto()
	{
	}

	private void PhotoGUI()
	{
		if (m_Message != EMessage.None)
		{
			return;
		}
		if (m_ConfirmDeletion)
		{
			GUI.skin = m_CommonSkin;
			GUIUtils.EAnswer eAnswer = GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.DeletePicture));
			GUI.skin = m_InterfaceSkin;
			switch (eAnswer)
			{
			case GUIUtils.EAnswer.Yes:
			{
				m_ConfirmDeletion = false;
				PlayAudioClip(m_ValidSound, base.transform.position, 1f);
				DeletePhoto();
				int count = m_AllThumbnails[0].Count;
				if (count > 0)
				{
					if (m_CurrentImage >= count)
					{
						m_CurrentImage--;
					}
					SelectPhoto();
				}
				else
				{
					SetSubState(ESubState.Idle);
					ActivateScroller(EScroller.Count);
					SetTexture(EPlane.Front, "OutWorld2D/OutWorld_PhotoEmpty");
				}
				break;
			}
			case GUIUtils.EAnswer.No:
				m_ConfirmDeletion = false;
				break;
			}
			return;
		}
		float verticalOffset = 200f;
		GUI.skin = m_InterfaceSkin;
		ESubState subState = m_SubState;
		if (subState != ESubState.Idle && subState == ESubState.Viewer)
		{
			if (m_CurrentImage > 0 && GUIUtils.DrawPrevButton(verticalOffset))
			{
				GUIUtils.GetScroller(0).OnScrollItemPressed(m_CurrentImage - 1);
				Utility.FreeMem();
			}
			if (m_CurrentImage < m_AllThumbnails[0].Count - 1 && GUIUtils.DrawNextButton(verticalOffset))
			{
				GUIUtils.GetScroller(0).OnScrollItemPressed(m_CurrentImage + 1);
				Utility.FreeMem();
			}
			ShareGUI(true);
		}
		GUI.skin = m_CommonSkin;
		if (Utility.InputStoppedByActivity())
		{
			GUIUtils.DrawFictiveBackButton();
		}
		else if (m_Travelling == ETravelling.Idle && GUIUtils.DrawBackButton() && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			SetSubState(ESubState.Idle);
			MoveCameraTo(EState.Photo, EState.Idle);
			ActivatePlane(EPlane.Idle, true);
			ActivateScroller(EScroller.Count);
		}
	}

	private void OnEnterPhoto()
	{
		SetTexture(EPlane.Front, "OutWorld2D/OutWorld_Photo");
		m_CurrentImage = 0;
	}

	private void OnUpdatePhoto()
	{
		switch (m_Travelling)
		{
		case ETravelling.FadeIn:
			if (!IsTravelling())
			{
				ActivatePlane(EPlane.Idle, false);
				m_Travelling = ETravelling.Idle;
				LoadPhotos();
			}
			break;
		case ETravelling.Idle:
			break;
		case ETravelling.FadeOut:
			if (!IsTravelling())
			{
				SetState(EState.Idle);
			}
			break;
		}
	}

	private void OnLeavePhoto()
	{
		m_CurrentImage = 0;
		SetSubState(ESubState.Idle);
	}

	private void LoadPhotos()
	{
		m_AllThumbnails[0].Clear();
		m_PhotosFileName.Clear();
		DirectoryInfo directoryInfo = new DirectoryInfo(m_MiniPhotoPath);
		if (directoryInfo != null && directoryInfo.Exists)
		{
			FileInfo[] files = directoryInfo.GetFiles();
			if (files != null)
			{
				m_PhotoDownloading = files.Length;
				if (m_PhotoDownloading > 0)
				{
					Utility.ShowActivityView(false);
					for (int i = 0; i < m_PhotoDownloading; i++)
					{
						Utility.CallWWW(this, "file://" + m_MiniPhotoPath + files[i].Name, OnEndLoadPhoto, OnLoadPhotoError, files[i].Name);
					}
					return;
				}
			}
		}
		SetTexture(EPlane.Front, "OutWorld2D/OutWorld_PhotoEmpty");
	}

	private void OnEndLoadPhoto(WWW www, object[] list)
	{
		string text = (string)list[0];
		Texture2D texture2D = www.texture;
		int num = 0;
		bool flag = false;
		int i;
		for (i = 0; i < m_PhotosFileName.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (text.CompareTo(m_PhotosFileName[i]) < 0)
			{
				Debug.Log("m_PhotosFileName " + i + " " + m_PhotosFileName.Count);
				flag = true;
			}
		}
		if (AchievementTable.IsGoodyNew(AchievementTable.EGoody.ARPicture, text))
		{
			Texture2D texture2D2 = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
			if (texture2D2 != null)
			{
				texture2D = texture2D2;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unable to blend New Texture with: " + text);
			}
		}
		if (flag)
		{
			i--;
			m_AllThumbnails[num].Insert(i, texture2D);
			m_PhotosFileName.Insert(i, text);
		}
		else
		{
			m_AllThumbnails[num].Add(texture2D);
			m_PhotosFileName.Add(text);
		}
		m_PhotoDownloading--;
		if (m_PhotoDownloading <= 0)
		{
			OnEndLoadAllPhotos();
		}
	}

	private void OnLoadPhotoError(object[] list)
	{
		m_PhotoDownloading--;
		if (m_PhotoDownloading <= 0)
		{
			OnEndLoadAllPhotos();
		}
	}

	private void OnEndLoadAllPhotos()
	{
		if (m_AllThumbnails[0].Count > 0)
		{
			ActivateScroller(EScroller.Photo);
			if (m_Scroller != null)
			{
				m_Scroller.Resize(m_AllThumbnails[0].ToArray(), m_CurrentImage);
			}
			if (PlayerPrefs.HasKey("GotoPhoto"))
			{
				int num = PlayerPrefs.GetInt("GotoPhoto");
				if (num == 0)
				{
					SelectPhoto();
				}
				else
				{
					GUIUtils.GetScroller(0).OnScrollItemPressed(num);
				}
				GamePlayerPrefs.DeleteKey("GotoPhoto");
			}
			else
			{
				SelectPhoto();
			}
		}
		else
		{
			SetTexture(EPlane.Front, "OutWorld2D/OutWorld_PhotoEmpty");
		}
		Utility.FreeMem();
		Utility.HideActivityView(false);
	}

	private void SelectPhoto()
	{
		int num = 0;
		if (m_CurrentImage < 0 || m_CurrentImage >= m_AllThumbnails[num].Count)
		{
			Utility.Log(ELog.Errors, "Selected image is out of range " + m_CurrentImage + "/" + m_AllThumbnails[num].Count);
			m_CurrentImage = 0;
		}
		StopCoroutine("SelectPhotoWWW");
		Texture2D texture2D = null;
		if (m_CurrentScroller == EScroller.Photo)
		{
			byte[] data = Utility.LoadFile(m_MiniPhotoPath + m_PhotosFileName[m_CurrentImage]);
			texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, false);
			texture2D.LoadImage(data);
			texture2D.Apply(false);
			m_PictureFileName = m_PhotosFileName[m_CurrentImage];
			if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.ARPicture, m_PhotosFileName[m_CurrentImage]))
			{
				m_AllThumbnails[num][m_CurrentImage] = texture2D;
				if (m_Scroller != null)
				{
					m_Scroller.Resize(m_AllThumbnails[num].ToArray(), m_CurrentImage);
				}
			}
			StartCoroutine("SelectPhotoWWW");
		}
		if (m_Planes[3] != null)
		{
			m_Planes[3].GetComponent<Renderer>().material.mainTexture = texture2D;
			ComputeAndSetRatio(EPlane.Photo);
		}
		SetSubState(ESubState.Viewer);
		ActivatePlane(EPlane.Photo, true);
		Utility.FreeMem();
	}

	private IEnumerator SelectPhotoWWW()
	{
		m_LastPhotoFilename = "file://" + m_PhotoPath + m_PhotosFileName[m_CurrentImage];
		WWW www = new WWW(m_LastPhotoFilename);
		yield return www;
		if (www.error == null)
		{
			Texture2D texture = www.texture;
			m_Planes[3].GetComponent<Renderer>().material.mainTexture = texture;
			ComputeAndSetRatio(EPlane.Photo);
		}
		else
		{
			Utility.Log(ELog.Errors, www.error);
		}
	}

	private void StartScroller()
	{
		for (int i = 0; i < 4; i++)
		{
			m_AllThumbnails[i] = new List<Texture2D>();
		}
		GUIScroller.ScrollItemPressed += OnScrollItemPressed;
		m_PhotoPath = Utility.GetPersistentDataPath() + "/" + GlobalVariables.s_PhotoPath + "/";
		m_MiniPhotoPath = Utility.GetPersistentDataPath() + "/" + GlobalVariables.s_MiniPhotoPath + "/";
		for (int i = 0; i < 4; i++)
		{
			GUIUtils.AddScroller(i, 383f, 5f, m_AllThumbnails[i].ToArray(), false, 0, false);
			GUIUtils.SetScrollerVisibility(i, false);
		}
		GUIUtils.SetTextContent(m_TextContent);
	}

	private void UpdateScrolling()
	{
		if (m_Scroller == null)
		{
			return;
		}
		m_Scroller.Update();
		if (!(m_AutoHideTimer > 0f))
		{
			return;
		}
		if (AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Began)
		{
			if (m_Scroller.IsInteract(0))
			{
				m_AutoHideTimer = -1f;
			}
			return;
		}
		m_AutoHideTimer -= Time.deltaTime;
		if (m_AutoHideTimer < 0f)
		{
			m_Scroller.Hide();
		}
	}

	private void OnDisableScroller()
	{
		GUIScroller.ScrollItemPressed -= OnScrollItemPressed;
		for (int i = 0; i < 4; i++)
		{
			GUIUtils.RemoveScroller(i);
		}
	}

	private void OnScrollItemPressed(int scrollViewID, int itemIdx)
	{
		if (m_ConfirmDeletion)
		{
			return;
		}
		PlayAudioClip(m_ValidSound, base.transform.position, 1f);
		if (scrollViewID == 0 && m_CurrentImage == itemIdx)
		{
			return;
		}
		m_CurrentImage = itemIdx;
		switch ((EScroller)scrollViewID)
		{
		case EScroller.Photo:
			SelectPhoto();
			break;
		case EScroller.Video:
			if (!AllInput.IsInternetReachable())
			{
				SetMessage(EMessage.NetNotReachable);
			}
			else
			{
				SelectVideo();
			}
			break;
		case EScroller.Wallpaper:
			SelectWallpaper();
			break;
		case EScroller.Stripe:
			SelectStripe();
			break;
		}
	}

	private void ScrollerGUI()
	{
		if ((bool)m_ScrollerSkin)
		{
			GUI.skin = m_ScrollerSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "OutWorld Hud: Skin not found");
		}
		if (m_Scroller != null)
		{
			m_Scroller.Draw(Utility.InputStoppedByActivity());
		}
	}

	private void ActivateScroller(EScroller scroller)
	{
		if (scroller == m_CurrentScroller)
		{
			if (m_Scroller != null && m_AllThumbnails[(int)scroller] != null)
			{
				m_Scroller.SetVisible(m_AllThumbnails[(int)scroller].Count != 0);
			}
			return;
		}
		if (m_CurrentScroller != EScroller.Count)
		{
			if (m_Scroller != null)
			{
				m_Scroller.Clear();
				m_Scroller.SetVisible(false);
			}
			m_Scroller = null;
			m_AllThumbnails[(int)m_CurrentScroller].Clear();
			Utility.FreeMem();
		}
		m_CurrentScroller = scroller;
		if (scroller != EScroller.Count)
		{
			m_Scroller = GUIUtils.GetScroller((int)scroller);
			if (m_Scroller != null && m_AllThumbnails[(int)scroller] != null)
			{
				m_Scroller.SetVisible(m_AllThumbnails[(int)scroller].Count != 0);
			}
		}
	}

	private void StartShare()
	{
		if (m_StartShare)
		{
			m_StartShare = false;
			float num = -2f;
			float num2 = GlobalVariables.BACK_BUTTON_WIDTH;
			float height = GlobalVariables.BACK_BUTTON_HEIGHT;
			float num3 = Utility.RefWidth - num2 - num;
			float top = 5f;
			string text = SystemInfo.deviceModel.ToUpperInvariant();
			if (text.Contains("Kindle".ToUpperInvariant()))
			{
				m_AuthorizeFacebook = false;
			}
			if (!Utility.Chinese && m_AuthorizeFacebook)
			{
				m_FaceRect = Utility.NewRect(num3, top, num2, height);
				num3 -= num2 + num;
			}
			m_SaveRect = Utility.NewRect(num3, top, num2, height);
			num3 -= num2 + num;
			m_DeleteRect = Utility.NewRect(num3, top, num2, height);
		}
	}

	private void ShareGUI(bool delete)
	{
		StartShare();
		GUI.skin = m_InterfaceSkin;
		if (Utility.InputStoppedByActivity())
		{
			if (!Utility.Chinese && m_AuthorizeFacebook)
			{
				GUI.Label(m_FaceRect, string.Empty, "ShareButton");
			}
			GUI.Label(m_SaveRect, string.Empty, "SaveButton");
			if (delete)
			{
				GUI.Label(m_DeleteRect, string.Empty, "DeleteButton");
			}
			GUI.skin = m_CommonSkin;
			GUIUtils.DrawFictiveBackButton();
			return;
		}
		int num = 7;
		switch (m_State)
		{
		case EState.Photo:
			num = 3;
			break;
		case EState.Wallpaper:
			num = 6;
			break;
		}
		if (!Utility.Chinese && m_AuthorizeFacebook && GUI.Button(m_FaceRect, string.Empty, "ShareButton"))
		{
			PlayAudioClip(m_ValidSound, base.transform.position, 1f);
			if (!AllInput.IsInternetReachable())
			{
				SetMessage(EMessage.NetNotReachable);
			}
			else if (num < 7)
			{
				ExkeeSocialNetwork.PostOnFacebook((Texture2D)m_Planes[num].GetComponent<Renderer>().material.mainTexture);
			}
		}
		if (GUI.Button(m_SaveRect, string.Empty, "SaveButton"))
		{
			PlayAudioClip(m_ValidSound, base.transform.position, 1f);
			if (num < 7)
			{
				SaveInImageAlbum((Texture2D)m_Planes[num].GetComponent<Renderer>().material.mainTexture, m_PictureFileName);
			}
		}
		if (delete && GUI.Button(m_DeleteRect, string.Empty, "DeleteButton"))
		{
			PlayAudioClip(m_ValidSound, base.transform.position, 1f);
			m_ConfirmDeletion = true;
		}
	}

	private void OnEnterShare()
	{
	}

	private void OnUpdateShare()
	{
	}

	private void OnLeaveShare()
	{
		SetSubState(ESubState.Idle);
	}

	private void StartSocialNetworking()
	{
		switch (Localization.GetLanguage())
		{
		case SystemLanguage.French:
			m_Site = GlobalVariables.FB_RABBIDS_PAGE_FR;
			break;
		case SystemLanguage.Italian:
			m_Site = GlobalVariables.FB_RABBIDS_PAGE_IT;
			break;
		case SystemLanguage.Spanish:
			m_Site = GlobalVariables.FB_RABBIDS_PAGE_ES;
			break;
		default:
			m_Site = GlobalVariables.FB_RABBIDS_PAGE_UK;
			break;
		}
		ExkeeSocialNetwork.OnFacebookPostFailedEvent += OnFacebookPostFailed;
		ExkeeSocialNetwork.OnFacebookPostSucceededEvent += OnFacebookPostSucceeded;
	}

	private void OnDisableSocialNetworking()
	{
	}

	private void OnFacebookPostSucceeded()
	{
		SetMessage(EMessage.PictureSent);
		if (m_CurrentScroller == EScroller.Photo)
		{
			DataMining.Increment(DataMining.EGeneral.PhotoPostedByFB.ToString());
		}
	}

	private void OnFacebookPostFailed()
	{
		SetMessage(EMessage.PictureSendingFailed);
	}

	private void ShowFacebookSite()
	{
		//FacebookAndroid.logout();
		ShowWebPage("http://www.facebook.com/" + m_Site);
	}

	private void StartStore()
	{
		m_StoreTitleRect = Utility.NewRect(50f, 70f, 180f, 40f);
		m_StoreDescriptionRect = Utility.NewRect(42.5f, 170f, Utility.RefWidth - 70f, 150f);
		m_StorePriceRect = Utility.NewRect(130f, 322f, 150f, m_LabelHeight);
		m_StorePriceSymbolRect = Utility.NewRect(278f, 325f, 20f, 20f);
		m_StoreBuyRect = Utility.NewRect(125f, 360f, 160f, 45f);
		if (Utility.IsHD())
		{
			m_PriceTextStyle = "PriceTextBig";
		}
		else
		{
			m_PriceTextStyle = "PriceText";
		}
	}

	private void StoreGUI()
	{
		if (m_Store)
		{
			GUI.skin = m_CommonSkin;
			if (Utility.InputStoppedByActivity())
			{
				GUIUtils.DrawFictiveBackButton();
			}
			else if (GUIUtils.DrawBackButton() && m_BackPressed < 0)
			{
				m_BackPressed = 5;
				if (m_Message != EMessage.None)
				{
					m_Message = EMessage.None;
				}
				else
				{
					ShowStore(false);
				}
			}
			EProductID productIDEnumWithStoreProductID = Utility.GetProductIDEnumWithStoreProductID(PLACEHOLDERS.str/*m_ProductList[0].productIdentifier*/);
			string text = PLACEHOLDERS.str /*productIDEnumWithStoreProductID.ToString()*/;
			GUI.Label(m_StoreTitleRect, Localization.GetLocalizedText(text + "_Title"), "BlackTextTitle");
			GUI.skin = m_OutWorldSkin;
			GUILayout.BeginArea(m_StoreDescriptionRect);
			m_DescriptionScrollPos = GUILayout.BeginScrollView(m_DescriptionScrollPos);
			GUILayout.Label(Localization.GetLocalizedText(text + "_Describe"), m_TextContent);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUI.skin = m_CommonSkin;
			if (true /*productIDEnumWithStoreProductID < EProductID.AvailableCount*/)
			{
				if (true /*Utility.IsUnlockedProduct(productIDEnumWithStoreProductID)*/)
				{
					GUI.Label(m_StoreBuyRect, Localization.GetLocalizedText(ELoc.PurchaseInstalled), "TextTitle");
				}
				else
				{
					LoadMoneySymbol(PLACEHOLDERS.str);
					GUI.Label(m_StorePriceRect, PLACEHOLDERS.str, m_PriceTextStyle);
					if (m_CurrencySymbolTex != null)
					{
						GUI.Label(m_StorePriceSymbolRect, m_CurrencySymbolTex, m_PriceTextStyle);
					}
					if (Utility.InputStoppedByActivity() || m_Message != EMessage.None)
					{
						GUI.Label(m_StoreBuyRect, Localization.GetLocalizedText(ELoc.PurchaseBuy), "BuyText");
					}
					else if (GUI.Button(m_StoreBuyRect, Localization.GetLocalizedText(ELoc.PurchaseBuy), "BuyText"))
					{
						if (!CanMakePayments())
						{
							SetMessage(EMessage.PurchaseDisable);
						}
						else
						{
							BuyProduct(0);
						}
					}
				}
			}
		}
		if (m_Message != EMessage.None)
		{
			ShowMessage();
		}
	}

	private void UpdateStore()
	{
		if (Utility.InputStoppedByActivity() || AllInput.GetTouchCount() != 1)
		{
			return;
		}
		AllInput.EState state = AllInput.GetState(0);
		switch (m_ScrollState)
		{
		case EScrollState.Idle:
			if (state == AllInput.EState.Began)
			{
				Vector3 gUIPosition = AllInput.GetGUIPosition(0);
				if (m_StoreDescriptionRect.Contains(gUIPosition))
				{
					m_ScrollState = EScrollState.StartTouch;
				}
			}
			break;
		case EScrollState.StartTouch:
			if (state == AllInput.EState.Moved)
			{
				m_ScrollState = EScrollState.TouchSlide;
			}
			break;
		case EScrollState.TouchSlide:
			m_DescriptionScrollPos.y -= AllInput.GetGUIDelta(0).y;
			if (state == AllInput.EState.Leave)
			{
				m_ScrollState = EScrollState.Idle;
			}
			break;
		}
	}

	private void ShowStore(bool b)
	{
		if (b)
		{
			SetTexture(EPlane.Front, "GUI/DLC_buy");
			DataMining.Increment(MainMenuScript.EMenu.Store.ToString());
		}
		else
		{
			switch (m_State)
			{
			case EState.Stripe:
				SetTexture(EPlane.Front, "OutWorld2D/OutWorld_BD");
				LoadStripes();
				if (m_Scroller != null)
				{
					m_Scroller.Resize(m_AllThumbnails[3].ToArray(), -1);
				}
				break;
			case EState.Wallpaper:
				SetTexture(EPlane.Front, "OutWorld2D/OutWorld_WallPapers");
				LoadWallpapers();
				GUIUtils.GetScroller(2).Resize(m_AllThumbnails[2].ToArray(), -1);
				break;
			}
		}
		m_ScrollPos = Vector2.zero;
		m_ScrollState = EScrollState.Idle;
		m_Store = b;
		m_Scroller.SetVisible(!b);
	}

	private void LoadMoneySymbol(string symbol)
	{
		if (symbol != m_CurrencySymbol)
		{
			m_CurrencySymbolTex = null;
			Utility.FreeMem();
			Utility.Log(ELog.Info, "symbol: " + symbol + " / int code: " + (int)symbol[0]);
			if (symbol[0] == '€')
			{
				m_CurrencySymbolTex = Utility.LoadResource<Texture2D>("CurrencySymbols/euro_white");
			}
			else if (symbol[0] == '£')
			{
				m_CurrencySymbolTex = Utility.LoadResource<Texture2D>("CurrencySymbols/pound_white");
			}
			else if (symbol[0] == '$')
			{
				m_CurrencySymbolTex = Utility.LoadResource<Texture2D>("CurrencySymbols/dollar_white");
			}
			else
			{
				Utility.Log(ELog.Errors, "Symbol: '" + symbol + "' is unknown");
				symbol = string.Empty;
			}
			m_CurrencySymbol = symbol;
		}
	}

	private void StartStoreKit()
	{
		/*StoreKitManager.productListReceived += OnProductListReceived;
		StoreKitManager.productListRequestFailed += OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful += OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled += OnPurchaseCancelled;
		StoreKitManager.purchaseFailed += OnPurchaseFailed;
		IABAndroidManager.billingSupportedEvent += OnBillingSupportedEvent;*/
		if (!m_CanMakePayments)
		{
		}
	}

	private void OnDisableStoreKit()
	{
		/*StoreKitManager.productListReceived -= OnProductListReceived;
		StoreKitManager.productListRequestFailed -= OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful -= OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled -= OnPurchaseCancelled;
		StoreKitManager.purchaseFailed -= OnPurchaseFailed;
		IABAndroidManager.billingSupportedEvent -= OnBillingSupportedEvent;*/
	}

	private void Request(EProductID product)
	{
		Utility.Log(product);
		if (!Utility.IsAvailableProduct(product))
		{
			Utility.Log(ELog.Errors, "Product not available");
			return;
		}
		if (!CanMakePayments())
		{
			SetMessage(EMessage.PurchaseDisable);
			return;
		}
		if (!AllInput.IsInternetReachable())
		{
			SetMessage(EMessage.NetNotReachable);
			return;
		}
		/*if (m_ProductList == null)
		{
			m_ProductList = new List<StoreKitProduct>();
		}
		else
		{
			m_ProductList.Clear();
		}*/
		string empty = string.Empty;
		empty += product;
		string localizedText = Localization.GetLocalizedText(product.ToString() + "_Title");
		string localizedText2 = Localization.GetLocalizedText(product.ToString() + "_Describe");
		//m_ProductList.Add(StoreKitProduct.productFromString(empty + "|||" + localizedText + "|||" + localizedText2 + "|||0.79|||?"));
		ShowStore(true);
	}

	private void BuyProduct(int productID)
	{
		/*if (m_ProductList != null && productID < 2)
		{
			PurchaseProduct(m_ProductList[productID].productIdentifier);
		}*/
	}

	/*private void OnProductListReceived(List<StoreKitProduct> productList)
	{
		if (productList.Count > 0)
		{
			m_ProductList = productList;
			ShowStore(true);
		}
		else
		{
			SetMessage(EMessage.EmptyStore);
		}
		OnProductListRequestEnded();
	}*/

	private void OnProductListRequestFailed(string error)
	{
		Utility.Log(ELog.Errors, error);
		SetMessage(EMessage.ProductListRequestFailed);
		OnProductListRequestEnded();
	}

	private void OnPurchaseSucceded(string productIdentifier, string receipt, int quantity)
	{
		EProductID productIDEnumWithStoreProductID = Utility.GetProductIDEnumWithStoreProductID(productIdentifier);
		Utility.UnlockProduct(productIDEnumWithStoreProductID);
		if (m_AchievementsTable != null)
		{
			m_AchievementsTable.UnlockPack();
		}
		DataMining.Increment(DataMining.EGeneral.Purchase.ToString());
		SetMessage(EMessage.PurchaseSuccess);
		SetTexture(EPlane.Front, "GUI/DLC");
		OnPurchaseEnded();
	}

	private void OnPurchaseCancelled(string error)
	{
		Utility.Log(ELog.Errors, error);
		OnPurchaseEnded();
	}

	private void OnPurchaseFailed(string error)
	{
		/*switch ((StoreKitManager.SKError)StoreKitManager.s_LastErrorCode)
		{
		case StoreKitManager.SKError.SKErrorClientInvalid:
			SetMessage(EMessage.PurchaseFailedClientInvalid);
			break;
		case StoreKitManager.SKError.SKErrorPaymentCancelled:
			SetMessage(EMessage.PurchaseFailedPaymentCancelled);
			break;
		case StoreKitManager.SKError.SKErrorPaymentInvalid:
			SetMessage(EMessage.PurchaseFailedPaymentInvalid);
			break;
		case StoreKitManager.SKError.SKErrorPaymentNotAllowed:
			SetMessage(EMessage.PurchaseFailedPaymentNotAllowed);
			break;
		default:
			SetMessage(EMessage.PurchaseFailed);
			break;
		}
		OnPurchaseEnded();
		StoreKitManager.s_LastErrorCode = -1;*/
	}

	private void OnRestoreTransactionEnded()
	{
		GlobalVariables.RecomputeMoveCount();
		Utility.HideActivityView(true);
	}

	private void OnProductListRequestEnded()
	{
		Utility.HideActivityView(true);
	}

	private void OnPurchaseEnded()
	{
		GlobalVariables.RecomputeMoveCount();
		Utility.HideActivityView(true);
	}

	private void OnBillingSupportedEvent(bool b)
	{
		Utility.Log(ELog.Plugin, "OnBillingSupportedEvent: " + b);
		m_CanMakePayments = b;
	}

	private void PurchaseProduct(string productID)
	{
		//IABAndroid.purchaseProduct(productID);
	}

	private bool CanMakePayments()
	{
		return m_CanMakePayments;
	}

	private void StartStripe()
	{
		m_StripeOriginalPos = m_Planes[4].transform.localPosition;
	}

	private void StripeGUI()
	{
		if (m_Message != EMessage.None || m_Store)
		{
			return;
		}
		float verticalOffset = 200f;
		GUI.skin = m_InterfaceSkin;
		ESubState subState = m_SubState;
		if (subState == ESubState.Viewer)
		{
			if (m_CurrentImage > 0 && GUIUtils.DrawPrevButton(verticalOffset))
			{
				m_CurrentImage--;
				GUIUtils.GetScroller(3).OnScrollItemPressed(m_CurrentImage);
				Utility.FreeMem();
			}
			if (m_CurrentImage < m_AllThumbnails[3].Count - 1 && GUIUtils.DrawNextButton(verticalOffset))
			{
				m_CurrentImage++;
				GUIUtils.GetScroller(3).OnScrollItemPressed(m_CurrentImage);
				Utility.FreeMem();
			}
		}
		GUI.skin = m_CommonSkin;
		if (m_Travelling == ETravelling.Idle && GUIUtils.DrawBackButton() && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			MoveCameraTo(EState.Stripe, EState.Idle);
			ActivatePlane(EPlane.Idle, true);
			m_StripeTex = null;
			ActivateScroller(EScroller.Count);
		}
	}

	private void OnEnterStripe()
	{
		SetTexture(EPlane.Front, "OutWorld2D/OutWorld_BD");
		ActivatePlane(EPlane.Collider, true);
		m_CurrentImage = 0;
	}

	private void OnUpdateStripe()
	{
		switch (m_Travelling)
		{
		case ETravelling.FadeIn:
			if (!IsTravelling())
			{
				ActivatePlane(EPlane.Idle, false);
				m_Travelling = ETravelling.Idle;
				LoadStripes();
				ActivateScroller(EScroller.Stripe);
				if (m_Scroller != null)
				{
					m_Scroller.Resize(m_AllThumbnails[3].ToArray(), 0);
				}
				if (m_Planes[4] != null)
				{
					m_StripeStartPos = m_Planes[4].transform.localPosition;
					m_StripeWantedPos = m_StripeStartPos;
					m_StripeCurrentPos = m_StripeWantedPos;
				}
				SelectStripe();
			}
			break;
		case ETravelling.Idle:
			UpdateStripeIdle();
			break;
		case ETravelling.FadeOut:
			if (!IsTravelling())
			{
				SetState(EState.Idle);
			}
			break;
		}
	}

	private void OnLeaveStripe()
	{
		m_CurrentImage = 0;
		SetSubState(ESubState.Idle);
		ActivatePlane(EPlane.Collider, false);
	}

	private void LoadStripes()
	{
		int num = 3;
		List<EStripe> stripeList = m_AchievementsTable.GetStripeList();
		m_AllThumbnails[num].Clear();
		for (int i = 0; i < stripeList.Count; i++)
		{
			bool flag = false;
			string text = Utility.GetTexPath() + "Stripes/Thumbnails";
			if (m_AchievementsTable != null)
			{
				EProductID productID = m_AchievementsTable.GetProductID(stripeList[i]);
				if (!Utility.IsUnlockedProduct(productID))
				{
					text += "Locked";
				}
				if (AchievementTable.IsGoodyNew(AchievementTable.EGoody.Stripe, stripeList[i].ToString()))
				{
					flag = true;
				}
			}
			text = text + "/" + stripeList[i];
			Texture2D texture2D = Utility.LoadResource<Texture2D>(text);
			if (flag)
			{
				texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
			}
			if (texture2D != null)
			{
				m_AllThumbnails[num].Add(texture2D);
			}
			else
			{
				Utility.Log(ELog.Errors, "Unable to load Stripe: " + text);
			}
		}
	}

	private void SelectStripe()
	{
		if (m_AchievementsTable == null)
		{
			return;
		}
		Texture2D texture2D = null;
		List<EStripe> stripeList = m_AchievementsTable.GetStripeList();
		EProductID productID = m_AchievementsTable.GetProductID(stripeList[m_CurrentImage]);
		if (!Utility.IsUnlockedProduct(productID))
		{
			Request(productID);
			return;
		}
		if (m_StripeTex != null)
		{
			m_StripeTex = null;
			ClearPlaneTexture(EPlane.Stripe);
		}
		texture2D = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "Stripes/" + stripeList[m_CurrentImage]);
		if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Stripe, stripeList[m_CurrentImage].ToString()))
		{
			LoadStripes();
			if (m_Scroller != null)
			{
				m_Scroller.Resize(m_AllThumbnails[3].ToArray(), -1);
			}
		}
		m_StripeTex = texture2D;
		SetTexture(EPlane.Stripe, texture2D);
		ComputeStripePlaneSize();
		SetSubState(ESubState.Viewer);
		Utility.FreeMem();
	}

	private void ComputeStripePlaneSize()
	{
		if (m_StripeTex != null && (bool)m_Planes[4] && m_StripeTex.height != 0)
		{
			Vector3 localScale = m_Planes[4].transform.localScale;
			float num = localScale.z / (float)m_StripeTex.height;
			localScale.x = num * (float)m_StripeTex.width;
			m_Planes[4].transform.localScale = localScale;
			m_StripeLimit = s_StripeScaleFactor * localScale.x;
			m_StripeWantedPos = m_Planes[4].transform.localPosition;
			m_StripeWantedPos.x = m_StripeOriginalPos.x + m_StripeLimit;
			m_StripeCurrentPos = m_StripeWantedPos;
			m_Planes[4].transform.localPosition = m_StripeWantedPos;
		}
	}

	private void UpdateStripeIdle()
	{
		if (m_Planes[4] == null)
		{
			return;
		}
		if (AllInput.GetTouchCount() == 1)
		{
			switch (AllInput.GetState(0))
			{
			case AllInput.EState.Began:
				m_CheckInput = GetContactPointOnInteractivePlane(0, out m_StripeBeganPos);
				if (!m_CheckInput)
				{
					Utility.Log(ELog.Errors, "Unable to pick on Collider Plane");
				}
				else if (m_Scroller != null)
				{
					m_CheckInput = !m_Scroller.IsInteract(0);
				}
				if (m_CheckInput)
				{
					m_StripeStartPos = m_Planes[4].transform.localPosition;
				}
				break;
			case AllInput.EState.Leave:
				if (!m_CheckInput)
				{
				}
				break;
			default:
				if (m_CheckInput && GetContactPointOnInteractivePlane(0, out m_StripeMovedPos))
				{
					float num = m_StripeBeganPos.x - m_StripeMovedPos.x;
					m_StripeWantedPos.x = m_StripeStartPos.x + num;
					m_StripeWantedPos.x = Mathf.Clamp(m_StripeWantedPos.x, m_StripeOriginalPos.x - m_StripeLimit, m_StripeOriginalPos.x + m_StripeLimit);
				}
				break;
			}
		}
		float num2 = m_StripeWantedPos.x - m_StripeCurrentPos.x;
		if (Mathf.Abs(num2) > 0.1f)
		{
			m_StripeCurrentPos.x += num2 * 0.14f;
			m_Planes[4].transform.localPosition = m_StripeCurrentPos;
		}
	}

	public static EStripe GetStripeEnum(string str)
	{
		for (EStripe eStripe = EStripe.Balloons; eStripe < EStripe.Count; eStripe++)
		{
			if (str.Equals(eStripe.ToString()))
			{
				return eStripe;
			}
		}
		return EStripe.Count;
	}

	private void StartVideo()
	{
		m_VideoTitleRect = Utility.NewRect(130f, 90f, 160f, 40f);
		m_VideoDescScrollRect = Utility.NewRect(42.5f, 270f, Utility.RefWidth - 70f, 65f);
		Vector2 vector = new Vector2(5f, 10f);
		float num = 1.5f;
		float num2 = 1.3333334f;
		float num3 = num - num2;
		float num4 = (float)Mathf.Max(Screen.width, Screen.height) / (float)Mathf.Min(Screen.width, Screen.height);
		num4 -= num;
		num4 /= num3;
		vector *= num4;
		m_VideoTimeRect = Utility.NewRect(255f + vector.x, 65f, 50f + vector.y, 35f);
	}

	private void VideoGUI()
	{
		if (m_Message == EMessage.None)
		{
			GUI.skin = m_CommonSkin;
			if (m_Travelling == ETravelling.Idle && GUIUtils.DrawBackButton() && m_BackPressed < 0)
			{
				m_BackPressed = 5;
				MoveCameraTo(EState.Video, EState.Idle);
				ActivatePlane(EPlane.Idle, true);
				ClearLists();
				ActivateScroller(EScroller.Count);
				return;
			}
		}
		if (m_CurrentImage != -1 && m_CurrentImage < m_Videos.Count)
		{
			GUI.Label(m_VideoTimeRect, m_Videos[m_CurrentImage].Duration + "s", m_CenterTextContent);
			GUI.skin = m_OutWorldSkin;
			Texture2D background = m_OutWorldSkin.verticalScrollbarThumb.normal.background;
			m_OutWorldSkin.verticalScrollbarThumb.normal.background = m_BlackScrollThumb;
			GUILayout.BeginArea(m_VideoTitleRect);
			m_TitleScrollPos = GUILayout.BeginScrollView(m_TitleScrollPos);
			GUILayout.Label(m_Videos[m_CurrentImage].Title, m_BlackTextContent);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			m_OutWorldSkin.verticalScrollbarThumb.normal.background = background;
			GUI.skin = m_CommonSkin;
		}
	}

	private void OnEnterVideo()
	{
		m_TextContent.fixedWidth = m_VideoDescScrollRect.width - 45f;
		m_BlackTextContent.fixedWidth = m_VideoTitleRect.width - 45f;
		SetTexture(EPlane.Front, "OutWorld2D/OutWorld_Videos");
		m_CurrentImage = 0;
		m_ScrollPos = Vector2.zero;
		m_TitleScrollPos = Vector2.zero;
		m_ScrollState = EScrollState.Idle;
		m_TitleScrollState = EScrollState.Idle;
		if (PlayerPrefs.HasKey("GotoVideo"))
		{
			m_CurrentImage = PlayerPrefs.GetInt("GotoVideo");
			PlayerPrefs.DeleteKey("GotoVideo");
		}
	}

	private void OnUpdateVideo()
	{
		switch (m_Travelling)
		{
		case ETravelling.FadeIn:
			if (!IsTravelling())
			{
				ActivatePlane(EPlane.Idle, false);
				m_Travelling = ETravelling.Idle;
				LoadVideos();
			}
			break;
		case ETravelling.Idle:
			if (m_CurrentImage == -1)
			{
				break;
			}
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				string text = RaycastHUDUnderFinger(0);
				if (text.Contains("VideoPlane"))
				{
					m_VideoBegan = true;
				}
			}
			else
			{
				if (!m_VideoBegan || AllInput.GetState(0) != AllInput.EState.Leave)
				{
					break;
				}
				m_VideoBegan = false;
				string text2 = RaycastHUDUnderFinger(0);
				if (text2.Contains("VideoPlane"))
				{
					if (!AllInput.IsInternetReachable())
					{
						SetMessage(EMessage.NetNotReachable);
						break;
					}
					AllInput.ActivateAutoRotateFrame(true);
					AllInput.ActivateAutoRotateScreen(true, true);
					StopAudioSource(EAudioSource.BG);
					ShowYoutube(GetVideoUrl(m_CurrentImage));
				}
			}
			break;
		case ETravelling.FadeOut:
			if (!IsTravelling())
			{
				SetState(EState.Idle);
			}
			break;
		}
		if (Utility.InputStoppedByActivity() || AllInput.GetTouchCount() != 1)
		{
			return;
		}
		AllInput.EState state = AllInput.GetState(0);
		switch (m_ScrollState)
		{
		case EScrollState.Idle:
			if (state == AllInput.EState.Began)
			{
				Vector3 gUIPosition = AllInput.GetGUIPosition(0);
				if (m_VideoDescScrollRect.Contains(gUIPosition))
				{
					m_ScrollState = EScrollState.StartTouch;
				}
			}
			break;
		case EScrollState.StartTouch:
			if (state == AllInput.EState.Moved)
			{
				m_ScrollState = EScrollState.TouchSlide;
			}
			break;
		case EScrollState.TouchSlide:
			m_ScrollPos.y -= AllInput.GetGUIDelta(0).y;
			if (state == AllInput.EState.Leave)
			{
				m_ScrollState = EScrollState.Idle;
			}
			break;
		}
		switch (m_TitleScrollState)
		{
		case EScrollState.Idle:
			if (state == AllInput.EState.Began)
			{
				Vector3 gUIPosition2 = AllInput.GetGUIPosition(0);
				if (m_VideoTitleRect.Contains(gUIPosition2))
				{
					m_TitleScrollState = EScrollState.StartTouch;
				}
			}
			break;
		case EScrollState.StartTouch:
			if (state == AllInput.EState.Moved)
			{
				m_TitleScrollState = EScrollState.TouchSlide;
			}
			break;
		case EScrollState.TouchSlide:
			m_TitleScrollPos.y -= AllInput.GetGUIDelta(0).y;
			if (state == AllInput.EState.Leave)
			{
				m_TitleScrollState = EScrollState.Idle;
			}
			break;
		}
	}

	private void OnLeaveVideo()
	{
		m_TextContent.fixedWidth = 0f;
		m_BlackTextContent.fixedWidth = 0f;
		StopAllCoroutines();
		m_CurrentImage = 0;
		SetSubState(ESubState.Idle);
	}

	private void ClearLists()
	{
		m_AllThumbnails[1].Clear();
		m_VideoThumbnails.Clear();
		m_Videos.Clear();
		m_IndicesToDelete.Clear();
		m_Indices.Clear();
	}

	private void LoadVideos()
	{
		Utility.ShowActivityView(false);
		string url;
		switch (Localization.GetLanguage())
		{
		case SystemLanguage.French:
			url = GlobalVariables.YOUTUBE_FR;
			break;
		case SystemLanguage.German:
			url = GlobalVariables.YOUTUBE_DE;
			break;
		case SystemLanguage.Italian:
			url = GlobalVariables.YOUTUBE_IT;
			break;
		case SystemLanguage.Spanish:
			url = GlobalVariables.YOUTUBE_ES;
			break;
		default:
			url = GlobalVariables.YOUTUBE_UK;
			break;
		}
		Utility.CallWWW(this, url, s_VideoLoadingTimeLimit, OnEndCallRSS, OnEndLoadVideos);
	}

	private void LoadVideoPicture()
	{
		Utility.ShowActivityView(false);
		Utility.CallWWW(this, m_Videos[m_CurrentImage].ThumbnailUrlBig, OnEndCallThumbnailBig, OnCallThumbnailErrorBig);
	}

	private void SelectVideo()
	{
		m_ScrollPos = Vector2.zero;
		m_TitleScrollPos = Vector2.zero;
		if (m_State == EState.Video && m_Videos.Count >= 1)
		{
			if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.YoutubeVideo, GetVideoThumbnailUrl(m_CurrentImage)))
			{
				m_AllThumbnails[1][m_CurrentImage] = m_VideoThumbnails[m_CurrentImage];
				GUIUtils.GetScroller(1).Resize(m_AllThumbnails[1].ToArray(), m_CurrentImage);
			}
			DataMining.Increment(GetVideoTag(m_CurrentImage));
			LoadVideoPicture();
			Utility.FreeMem();
		}
	}

	private void WallpaperGUI()
	{
		if (m_Message != EMessage.None || m_Store)
		{
			return;
		}
		float verticalOffset = 200f;
		GUI.skin = m_InterfaceSkin;
		ESubState subState = m_SubState;
		if (subState != ESubState.Idle && subState == ESubState.Viewer)
		{
			if (m_CurrentImage > 0 && GUIUtils.DrawPrevButton(verticalOffset))
			{
				m_CurrentImage--;
				GUIUtils.GetScroller(2).OnScrollItemPressed(m_CurrentImage);
				Utility.FreeMem();
			}
			if (m_CurrentImage < m_AllThumbnails[2].Count - 1 && GUIUtils.DrawNextButton(verticalOffset))
			{
				m_CurrentImage++;
				GUIUtils.GetScroller(2).OnScrollItemPressed(m_CurrentImage);
				Utility.FreeMem();
			}
			ShareGUI(false);
		}
		GUI.skin = m_CommonSkin;
		if (Utility.InputStoppedByActivity())
		{
			GUIUtils.DrawFictiveBackButton();
		}
		else if (m_Travelling == ETravelling.Idle && GUIUtils.DrawBackButton() && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			SetSubState(ESubState.Idle);
			MoveCameraTo(EState.Wallpaper, EState.Idle);
			ActivatePlane(EPlane.Idle, true);
			ActivateScroller(EScroller.Count);
		}
	}

	private void OnEnterWallpaper()
	{
		SetTexture(EPlane.Front, "OutWorld2D/OutWorld_WallPapers");
		m_CurrentImage = 0;
	}

	private void OnUpdateWallpaper()
	{
		switch (m_Travelling)
		{
		case ETravelling.FadeIn:
			if (!IsTravelling())
			{
				ActivatePlane(EPlane.Idle, false);
				m_Travelling = ETravelling.Idle;
				LoadWallpapers();
				ActivateScroller(EScroller.Wallpaper);
				SelectWallpaper();
			}
			break;
		case ETravelling.Idle:
			break;
		case ETravelling.FadeOut:
			if (!IsTravelling())
			{
				SetState(EState.Idle);
			}
			break;
		}
	}

	private void OnLeaveWallpaper()
	{
		m_CurrentImage = 0;
		SetSubState(ESubState.Idle);
	}

	private void LoadWallpapers()
	{
		bool flag = false;
		EPack ePack = EPack.Count;
		int num = 2;
		List<EWallpaper> wallpaperList = m_AchievementsTable.GetWallpaperList();
		GUIScroller.SPack[] array = new GUIScroller.SPack[6];
		for (ePack = EPack.Interactions; ePack < EPack.Count; ePack++)
		{
			array[(int)ePack].Texture = null;
			array[(int)ePack].Pack = ePack;
			array[(int)ePack].StartIndex = -1;
			array[(int)ePack].EndIndex = -1;
		}
		if (m_AchievementsTable == null)
		{
			Utility.Log(ELog.Errors, "LoadWallpapers failed: m_AchievementsTable == null");
			return;
		}
		m_AllThumbnails[num].Clear();
		for (int i = 0; i < wallpaperList.Count; i++)
		{
			bool flag2 = false;
			bool flag3 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			EProductID productID = m_AchievementsTable.GetProductID(wallpaperList[i]);
			ePack = AchievementTable.GetPackFromProductID(productID);
			flag = Utility.IsUnlockedProduct(productID);
			string text = Utility.GetTexPath() + "Wallpapers/Thumbnails";
			if (ePack != EPack.Count && array[(int)ePack].StartIndex == -1)
			{
				array[(int)ePack].StartIndex = m_AllThumbnails[num].Count;
			}
			if (ePack2 != ePack)
			{
				EPack ePack3 = ePack2;
				if (ePack3 == EPack.Count)
				{
					ePack3 = EPack.Interactions;
				}
				array[(int)ePack3].EndIndex = m_AllThumbnails[num].Count - 1;
				if (!Utility.IsUnlockedProduct(AchievementTable.GetProductIDFromPack(ePack3)))
				{
					int eltCount = array[(int)ePack3].EndIndex - array[(int)ePack3].StartIndex;
					array[(int)ePack3].Texture = AchievementTable.LoadPackTexture(ePack3, eltCount);
				}
			}
			if (!flag)
			{
				flag3 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag2 = AchievementTable.IsGoodyNew(AchievementTable.EGoody.Wallpaper, wallpaperList[i].ToString());
			}
			text = text + "/" + wallpaperList[i];
			Texture2D texture2D = Utility.LoadResource<Texture2D>(text);
			if (texture2D != null)
			{
				if (flag2)
				{
					texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
				}
				if (flag3)
				{
					texture2D = Utility.AddColor(texture2D, clr);
				}
				m_AllThumbnails[num].Add(texture2D);
			}
			else
			{
				Utility.Log(ELog.Errors, "Failed to load: " + text);
			}
		}
		if (ePack != EPack.Count)
		{
			array[(int)ePack].EndIndex = m_AllThumbnails[num].Count - 1;
			if (!Utility.IsUnlockedProduct(AchievementTable.GetProductIDFromPack(ePack)))
			{
				int eltCount2 = array[(int)ePack].EndIndex - array[(int)ePack].StartIndex;
				array[(int)ePack].Texture = AchievementTable.LoadPackTexture(ePack, eltCount2);
			}
		}
		GUIUtils.GetScroller(num).Resize(m_AllThumbnails[num].ToArray(), 0);
		GUIUtils.GetScroller(num).SetPacks(array);
	}

	private void SelectWallpaper()
	{
		if (m_AchievementsTable != null)
		{
			Texture2D texture2D = null;
			List<EWallpaper> wallpaperList = m_AchievementsTable.GetWallpaperList();
			EProductID productID = m_AchievementsTable.GetProductID(wallpaperList[m_CurrentImage]);
			if (!Utility.IsUnlockedProduct(productID))
			{
				Request(productID);
			}
			else
			{
				if (m_Planes[6] != null && m_Planes[6].GetComponent<Renderer>().material.mainTexture != null)
				{
					m_Planes[6].GetComponent<Renderer>().material.mainTexture = null;
					Utility.FreeMem();
				}
				m_LastPhotoFilename = Utility.GetTexPath() + "Wallpapers/" + wallpaperList[m_CurrentImage];
				texture2D = Utility.LoadResource<Texture2D>(m_LastPhotoFilename);
				m_PictureFileName = wallpaperList[m_CurrentImage].ToString();
				if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Wallpaper, wallpaperList[m_CurrentImage].ToString()))
				{
					LoadWallpapers();
					GUIUtils.GetScroller(2).Resize(m_AllThumbnails[2].ToArray(), -1);
				}
				if (m_Planes[6] != null)
				{
					m_Planes[6].GetComponent<Renderer>().material.mainTexture = texture2D;
					ComputeAndSetRatio(EPlane.Wallpaper);
				}
				SetSubState(ESubState.Viewer);
				ActivatePlane(EPlane.Wallpaper, true);
				Utility.FreeMem();
			}
		}
		if (m_Scroller != null)
		{
			m_Scroller.Resize(m_AllThumbnails[2].ToArray(), m_CurrentImage);
		}
	}

	public static EWallpaper GetWallpaperEnum(string str)
	{
		for (EWallpaper eWallpaper = EWallpaper.Wallpaper1; eWallpaper < EWallpaper.Count; eWallpaper++)
		{
			if (str.Equals(eWallpaper.ToString()))
			{
				return eWallpaper;
			}
		}
		return EWallpaper.Count;
	}

	private string GetVideoUrl(int idx)
	{
		return m_Videos[idx].Url;
	}

	private string GetVideoThumbnailUrl(int idx)
	{
		return m_Videos[idx].ThumbnailUrl;
	}

	private string GetVideoTag(int idx)
	{
		return m_Videos[idx].Tag;
	}

	private string GetVideoTitle(int idx)
	{
		return m_Videos[idx].Title;
	}

	private string GetVideoDate(int idx)
	{
		return m_Videos[idx].Date;
	}

	private int GetVideoDuration(int idx)
	{
		return m_Videos[idx].Duration;
	}

	private void OnEndCallRSS(WWW www, object[] list)
	{
		m_Videos.Clear();
		SmallXmlParser smallXmlParser = new SmallXmlParser();
		smallXmlParser.Parse(new StringReader(www.text), this);
		string text = PlayerPrefs.GetString("VideoIds");
		string text2 = text;
		m_ThumbnailDownloading = m_Videos.Count;
		for (int i = 0; i < m_Videos.Count; i++)
		{
			if (!text.Contains(m_Videos[i].Tag))
			{
				text2 = text2 + "/" + m_Videos[i].Tag;
			}
			Utility.CallWWW(this, m_Videos[i].ThumbnailUrl, OnEndCallThumbnail, OnCallThumbnailError, i, m_Videos[i].ThumbnailUrl);
		}
		GamePlayerPrefs.SetString("VideoIds", text2);
	}

	private void OnEndLoadVideos(params object[] list)
	{
		if (m_State == EState.Video && m_Travelling == ETravelling.Idle)
		{
			ActivateScroller(EScroller.Video);
			if (m_Scroller != null)
			{
				m_Scroller.Resize(m_AllThumbnails[1].ToArray(), 0);
			}
		}
		for (int num = m_IndicesToDelete.Count - 1; num >= 0; num--)
		{
			m_Videos.RemoveAt(m_IndicesToDelete[num]);
		}
		m_IndicesToDelete.Clear();
		m_Indices.Clear();
		SelectVideo();
		Utility.HideActivityView(false);
	}

	private void OnEndCallThumbnail(WWW www, object[] list)
	{
		int num = (int)list[0];
		string text = (string)list[1];
		Texture2D texture = www.texture;
		Texture2D texture2D = null;
		int num2 = 1;
		bool flag = false;
		int i;
		for (i = 0; i < m_Indices.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (num < m_Indices[i])
			{
				flag = true;
			}
		}
		if (AchievementTable.IsGoodyNew(AchievementTable.EGoody.YoutubeVideo, text))
		{
			texture2D = Utility.BlendTextures(m_NewTexture_120x90, texture);
			if (texture2D == null)
			{
				Utility.Log(ELog.Errors, "Unable to blend New Texture with: " + text);
			}
		}
		if (flag)
		{
			i--;
			if (texture2D != null)
			{
				m_AllThumbnails[num2].Insert(i, texture2D);
			}
			else
			{
				m_AllThumbnails[num2].Insert(i, texture);
			}
			m_VideoThumbnails.Insert(i, texture);
			m_Indices.Insert(i, num);
		}
		else
		{
			if (texture2D != null)
			{
				m_AllThumbnails[num2].Add(texture2D);
			}
			else
			{
				m_AllThumbnails[num2].Add(texture);
			}
			m_VideoThumbnails.Add(texture);
			m_Indices.Add(num);
		}
		m_ThumbnailDownloading--;
		if (m_ThumbnailDownloading <= 0)
		{
			OnEndLoadVideos();
		}
	}

	private void OnCallThumbnailError(object[] list)
	{
		int num = (int)list[0];
		bool flag = false;
		int i;
		for (i = 0; i < m_IndicesToDelete.Count; i++)
		{
			if (flag)
			{
				break;
			}
			if (num < m_IndicesToDelete[i])
			{
				flag = true;
			}
		}
		if (flag)
		{
			m_IndicesToDelete.Insert(i, num);
		}
		else
		{
			m_IndicesToDelete.Add(num);
		}
		m_ThumbnailDownloading--;
		if (m_ThumbnailDownloading <= 0)
		{
			OnEndLoadVideos();
		}
	}

	private void OnEndLoadVideosBig()
	{
		if (m_VideoThumbnail != null)
		{
			SetTexture(EPlane.Video, m_VideoThumbnail);
		}
		m_VideoThumbnail = null;
		Utility.FreeMem();
		Utility.HideActivityView(false);
	}

	private void OnEndCallThumbnailBig(WWW www, object[] list)
	{
		Texture2D texture = www.texture;
		m_VideoThumbnail = texture;
		OnEndLoadVideosBig();
	}

	private void OnCallThumbnailErrorBig(object[] list)
	{
		OnEndLoadVideosBig();
	}

	public void OnStartParsing(SmallXmlParser parser)
	{
	}

	public void OnEndParsing(SmallXmlParser parser)
	{
	}

	public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
	{
		if (name.Equals("entry"))
		{
			m_CurrentVideo.Url = string.Empty;
			m_CurrentVideo.ThumbnailUrl = string.Empty;
			m_CurrentVideo.Tag = string.Empty;
			m_CurrentVideo.Date = string.Empty;
			m_CurrentVideo.Duration = -1;
			m_CurrentThumbnail = (int)m_UsingThumbnail;
		}
		else if (name.Equals("updated"))
		{
			m_Check = ECheck.CheckDate;
		}
		else if (name.Equals("title"))
		{
			m_Check = ECheck.CheckTitle;
		}
		else if (name.Equals("content"))
		{
			m_Check = ECheck.CheckDesc;
		}
		else if (name.Equals("media:player"))
		{
			string value = attrs.GetValue("url");
			m_CurrentVideo.Url = value.Replace("https", "http");
		}
		else if (name.Equals("media:thumbnail"))
		{
			if (m_CurrentThumbnail == 0)
			{
				string value2 = attrs.GetValue("url");
				m_CurrentVideo.ThumbnailUrl = value2;
				char[] separator = new char[1] { '/' };
				string[] array = value2.Split(separator);
				m_CurrentVideo.Tag = array[array.Length - 2];
			}
			else if (m_CurrentThumbnail == (int)m_UsingThumbnail)
			{
				string value3 = attrs.GetValue("url");
				m_CurrentVideo.ThumbnailUrlBig = value3;
			}
			m_CurrentThumbnail--;
		}
		else if (name.Equals("yt:duration"))
		{
			m_CurrentVideo.Duration = int.Parse(attrs.GetValue("seconds"));
		}
	}

	public void OnEndElement(string name)
	{
		if (name.Equals("entry"))
		{
			if (m_CurrentVideo.Url.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Video url not fount");
			}
			else if (m_CurrentVideo.ThumbnailUrl.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Thumbnail url not found");
			}
			else if (m_CurrentVideo.ThumbnailUrlBig.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : ThumbnailBig url not found");
			}
			else if (m_CurrentVideo.Tag.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Video tag not found");
			}
			else if (m_CurrentVideo.Title.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Video title not found");
			}
			else if (m_CurrentVideo.Desc.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Video description not found");
			}
			else if (m_CurrentVideo.Date.Length == 0)
			{
				Utility.Log(ELog.Errors, "Youtube : Video date not found");
			}
			else if (m_CurrentVideo.Duration == -1)
			{
				Utility.Log(ELog.Errors, "Youtube : Video duration not found");
			}
			else
			{
				m_Videos.Add(m_CurrentVideo);
			}
		}
	}

	public void OnProcessingInstruction(string name, string text)
	{
	}

	public void OnChars(string text)
	{
		switch (m_Check)
		{
		case ECheck.CheckDate:
			m_CurrentVideo.Date = text.Substring(0, text.LastIndexOf('T'));
			m_Check = ECheck.Idle;
			break;
		case ECheck.CheckTitle:
			m_CurrentVideo.Title = text;
			m_Check = ECheck.Idle;
			break;
		case ECheck.CheckDesc:
			m_CurrentVideo.Desc = text;
			m_Check = ECheck.Idle;
			break;
		}
	}

	public void OnIgnorableWhitespace(string text)
	{
	}
}
