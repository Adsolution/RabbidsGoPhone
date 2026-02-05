using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InGameScript : Rabbid
{
	public enum EDirection
	{
		Down = 0,
		Right = 1,
		Up = 2,
		Left = 3,
		Count = 4,
		Undefined = 5
	}

	public enum EState
	{
		Idle = 0,
		Tickle = 1,
		Turn = 2,
		Run = 3,
		String = 4,
		Ear = 5,
		Burp = 6,
		Blow = 7,
		Yell = 8,
		WallBounce = 9,
		Poke = 10,
		Dance = 11,
		Rotation = 12,
		FaceDown = 13,
		CustomCostume = 14,
		CustomEnvironment = 15,
		PresentTuto = 16,
		ClownBox = 17,
		YellowDuck = 18,
		Night = 19,
		Steam = 20,
		Piranha = 21,
		Swatter = 22,
		WCBrush = 23,
		Micro = 24,
		PhoneReal = 25,
		PopGun = 26,
		Jetpack = 27,
		Guitar = 28,
		Shield = 29,
		EgyptianToiletPaper = 30,
		Bow = 31,
		GhettoBlaster = 32,
		Nunchaku = 33,
		RugbyBall = 34,
		SausagePolice = 35,
		Count = 36,
		MiniGameCount = 19,
		ActionCount = 15,
		IngnoredMiniGames = 37
	}

	public enum ETutorial
	{
		Blow = 0,
		Burp = 1,
		Facedown = 2,
		Run = 3,
		String = 4,
		Turn = 5,
		Yell = 6,
		Count = 7
	}

	public enum EPanel
	{
		Bwwaaah = 0,
		ArrowTurn = 1,
		Count = 2
	}

	public enum ESide
	{
		Left = 0,
		Right = 1,
		Front = 2,
		Back = 3,
		Up = 4,
		Down = 5,
		Count = 6
	}

	public enum ECollider
	{
		LeftFoot = 0,
		RightFoot = 1,
		LeftEye = 2,
		RightEye = 3,
		LeftEar = 4,
		RightEar = 5,
		Tummy = 6,
		String = 7,
		Panel = 8,
		ClownBox = 9,
		YellowDuck = 10,
		Piranha = 11,
		PiranhaBowl = 12,
		Swatter = 13,
		Micro = 14,
		PopGun = 15,
		Jetpack = 16,
		Guitar = 17,
		Shield = 18,
		EgyptianToiletPaper = 19,
		GhettoBlaster = 20,
		Count = 21
	}

	public enum EPullState
	{
		Pull = 0,
		Release = 1,
		Count = 2
	}

	public enum EScroller
	{
		MiniGame = 0,
		Costume = 1,
		Environment = 2,
		ARPoses = 3,
		Count = 4
	}

	public enum ESound
	{
		IZI_Poke_hurt1 = 0,
		IZI_Poke_hurt2 = 1,
		IZI_Standby_part1 = 2,
		IZI_Standby_part2 = 3,
		IZI_Standby = 4,
		IZI_Standby2 = 5,
		IZI_Crazy = 6,
		IZI_Knock = 7,
		IZI_Yell_Near = 8,
		Idle_Count = 9,
		N05_Costel = 10,
		Rabbid_Baaah = 11,
		Rabbid_ShortBaaah = 12,
		IZI_pulling_begin = 13,
		IZI_Blow_fall_and_getup = 14,
		IZI_Blow_on_wall = 15,
		IZI_Blow = 16,
		IZI_Burp = 17,
		IZI_Choke = 18,
		IZI_Dance3 = 19,
		IZI_Gift_begin = 20,
		IZI_Gift_open = 21,
		IZI_Gift_smile = 22,
		IZI_GiftDuck_hit = 23,
		IZI_GiftDuck_open = 24,
		IZI_GiftDuck_throw = 25,
		IZI_GiftDuck_wait = 26,
		IZI_lightoff = 27,
		IZI_Lookdown_back_to_initial = 28,
		IZI_Lookdown_standup_and_wave = 29,
		IZI_pulling_hit1 = 30,
		IZI_Rotate1 = 31,
		IZI_Rotate2 = 32,
		IZI_Rotate3 = 33,
		IZI_Run_begin = 34,
		IZI_Run_fall = 35,
		IZI_Run_loop = 36,
		IZI_Screen_Bouncing_back_to_front1 = 37,
		IZI_Screen_Bouncing_back_to_front2 = 38,
		IZI_Screen_Bouncing_fall_andgetup = 39,
		IZI_Screen_Bouncing_front_to_back1 = 40,
		IZI_Steam = 41,
		IZI_String_ouch1 = 42,
		IZI_String_ouch2 = 43,
		IZI_String_ouch3 = 44,
		IZI_String_slap = 45,
		IZI_String_tension = 46,
		IZI_Tickle_step1 = 47,
		IZI_Tickle_step2_loop = 48,
		IZI_Tickle_step2_loop2 = 49,
		IZI_Tickle_step3 = 50,
		IZI_Turn_hit1 = 51,
		IZI_Turn = 52,
		IZI_Tutorial = 53,
		IZI_Waddle = 54,
		IZI_Wall_Horisontal_fall_and_getup = 55,
		IZI_Wall_Horisontal_left_to_right1 = 56,
		IZI_Wall_Horisontal_right_to_left1 = 57,
		IZI_Wall_Vertical_bottom_to_top1 = 58,
		IZI_Wall_Vertical_getup = 59,
		IZI_Wall_Vertical_top_to_bottom1 = 60,
		IZI_Yell = 61,
		IZI_Screen_Bouncing_frontfall_andgetup = 62,
		IZI_Gift_loop = 63,
		Human_Clean_window_loop = 64,
		ChocMat_a = 65,
		ChocMat_b = 66,
		ChocMat_c = 67,
		ChocRabbid = 68,
		EgyptianToiletPaper_Start = 69,
		Fart = 70,
		Lick = 71,
		EgyptianToiletPaper_Count = 72,
		Guitar_Start = 73,
		Guitar_Bad = 74,
		Guitar_Good = 75,
		Guitar_Good_2 = 76,
		Woosh = 77,
		Guitar_Count = 78,
		Jetpack_Start = 79,
		JetPack_Whirl_Loop = 80,
		Jetpack_Count = 81,
		Micro_Start = 82,
		Micro_Crunch = 83,
		Micro_Electricity = 84,
		Micro_Song = 85,
		Micro_Count = 86,
		PhoneReal_Start = 87,
		PhoneReal_Dial = 88,
		PhoneReal_Explode = 89,
		PhoneReal_Ring_a = 90,
		PhoneReal_Ring_b = 91,
		PhoneReal_Swallow = 92,
		PhoneReal_Count = 93,
		Piranha_Start = 94,
		Piranha_Clap_a = 95,
		Piranha_Clap_b = 96,
		Piranha_Clap_c = 97,
		Piranha_FishFall = 98,
		Piranha_GlassSplash = 99,
		Piranha_Water = 100,
		Piranha_FishedBwah = 101,
		Piranha_RabbidShot = 102,
		Piranha_Count = 103,
		PopGun_Start = 104,
		Bulb_Explode = 105,
		PopGun_Broken = 106,
		PopGun_Pop = 107,
		PopGun_Count = 108,
		Shield_Start = 109,
		Hawai_Loop = 110,
		Shield_Count = 111,
		Swatter_Start = 112,
		Swatter_Clap_a = 113,
		Swatter_Clap_b = 114,
		Swatter_Clap_c = 115,
		Swatter_StifflingYell = 116,
		Swatter_Count = 117,
		WCBrush_Start = 118,
		WCBrush_BodyBrush = 119,
		WCBrush_TeethBrush = 120,
		WCBrush_EyeLashBrush = 121,
		WCBrush_EarRemoval = 122,
		WCBrush_Scratch = 123,
		WCBrush_Count = 124,
		Bow_Start = 125,
		Bow_Idle = 126,
		Bow_Action1 = 127,
		Bow_Action2 = 128,
		Bow_Count = 129,
		GhettoBlaster_Start = 130,
		GhettoBlaster_Idle = 131,
		GhettoBlaster_Action_1 = 132,
		GhettoBlaster_Action_2 = 133,
		GhettoBlaster_Count = 134,
		Nunchaku_Start = 135,
		Nunchaku_Idle = 136,
		Nunchaku_Action1 = 137,
		Nunchaku_Action2 = 138,
		Nunchaku_Count = 139,
		RugbyBall_Start = 140,
		RugbyBall_Idle = 141,
		RugbyBall_Action1 = 142,
		RugbyBall_Action2 = 143,
		RugbyBall_Count = 144,
		SausagePolice_Start = 145,
		SausagePolice_Idle = 146,
		SausagePolice_Action_1 = 147,
		SausagePolice_Action_2 = 148,
		SausagePolice_Count = 149,
		Count = 150
	}

	public enum ERabbidSound
	{
		Bun_Collect_XS_get1 = 0,
		Bun_Collect_XS_get2 = 1,
		Bun_Collect_XS_get3 = 2,
		Bun_Collect_XS_get4 = 3,
		Bun_Grab_action1 = 4,
		Bun_Grab_action2 = 5,
		Bun_Grab_action3 = 6,
		Bun_StandBy_ScratchAss = 7,
		Burp = 8,
		Count = 9
	}

	public enum EBwahSound
	{
		Bwah_A = 0,
		Bwah_B = 1,
		Bwah_C = 2,
		Bwah_D = 3,
		Bwah_E = 4,
		Bwah_F = 5,
		Bwah_G = 6,
		Bwah_H = 7,
		Bwah_I = 8,
		Bwah_J = 9,
		Bwah_K = 10,
		Bwah_L = 11,
		Bwah_M = 12,
		Bwah_N = 13,
		Hysteric_Bwaah = 14,
		IZW_Bun_Wonder2 = 15,
		Count = 16
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
		ImgSizeError = 10,
		None = 11
	}

	public enum EScrollState
	{
		Idle = 0,
		StartTouch = 1,
		TouchSlide = 2
	}

	private const float m_TutorialWaitTime = 40f;

	private const float m_StateWaitTime = 15f;

	private const float m_LoopStart = 2.1f;

	private const float m_LoopEnd = 4.25f;

	private const float m_AutoHideTime = 2f;

	private int m_NewMoveCounter;

	private int m_BouncingCounter;

	private int m_BlockBouncingCounter;

	private int m_LastOrientation;

	private bool m_NewWallpaper;

	private bool m_NewCostume;

	private bool m_NewARPict;

	private bool m_NewEnvironment;

	private bool m_NewStripe;

	private float m_GoodiesTime;

	private bool m_DestroyIntroPanel;

	private static int s_GravityBlocker = 50;

	private static bool s_ReverseBeforePausing;

	public static int s_TargetOrientation = 1;

	public static int s_CurrentOrientation = 1;

	private static float kAccelerationThreshold = 1.7f;

	private static float kCircleClosureDistanceVariance = 50f;

	private static float kRadiusVariancePercent = 0.7f;

	private static float kOverlapTolerance = 6f;

	private static float kGoodiesTime = 6f;

	private static int kBouncingDelay = 15;

	private static int kBlockBouncingDelay = 20;

	private static int kNewMoveTimer = 120;

	private bool m_IsBurpGesture;

	private bool m_IsBlowGesture;

	private bool m_IsYellGesture;

	private bool m_IsCircleGesture;

	private ArrayList m_NewMoveArray = new ArrayList();

	private Vector3 m_FirstTouch = Vector3.zero;

	private ArrayList m_Points = new ArrayList();

	private float m_InitialDistance;

	private int m_MusicButton;

	public AudioClip BackSound;

	public AudioClip NewMoveSound;

	public AudioClip NewWallPaperSound;

	private int m_FixedUpdateFrameCount;

	private int m_UpdateFrameCount;

	private GameObject m_IntroLoadingScreen;

	private GameObject m_OutroLoadingScreen;

	private GameObject m_IntroPanelObject;

	public Transform stringPointTransform;

	public Transform earLeftPointTransform;

	public Transform earRightPointTransform;

	private Transform m_CostumeEarLeftPointTransform;

	private Transform m_CostumeEarRightPointTransform;

	public Texture2D blackBackground;

	private Rect m_BlackBGRect = default(Rect);

	private float m_BlackBGRectHeight = 1f;

	public GUISkin m_ScrollerSkin;

	public GUISkin m_InGameSkin;

	public GUISkin m_OptionsSkin;

	private Texture2D m_NewTexture_110x128;

	private bool m_RemoveTPose = true;

	private bool m_IsBlowing;

	private bool m_IsBlowingDone;

	private float m_BlowTime;

	private static int s_StartOrientation;

	private static int s_LastValidOrientation;

	private static int s_TmpLastValidOrientation;

	private bool m_HasBeenUpdated;

	private Vector3 m_StartPosition = Vector3.zero;

	private string m_NextSceneName = string.Empty;

	private int m_FrameCountBeforeNextScene = -1;

	private EState m_StartState;

	private int m_BackPressed = -1;

	private float m_MicrophoneThreshold = 15f;

	private float m_CurrentRotation;

	private float m_WantedRotation;

	private float m_StartRotation;

	private float m_ReferenceRotation;

	private bool m_ForceCustomIdle;

	private bool m_AskCostumeConfirm;

	private bool m_AskEnvironmentConfirm;

	private bool m_Leave;

	private int m_IndexSave;

	private EState m_FallState = EState.Count;

	private Fonctionality m_Fonctionality;

	private FonctionalityItem m_FonctionalityRoot;

	private FonctionalityItem.EType m_FonctionalityItemType = FonctionalityItem.EType.None;

	private MeshMerger m_FonctionalityMerger;

	private Fader m_Flash;

	private float m_FlashDuration = 0.1f;

	private bool m_GoToPhotoRoom;

	private bool m_GoToMainMenu;

	private bool m_ShowProgress;

	private Hashtable m_ProgressPicts = new Hashtable();

	private Hashtable m_ProgressPackPicts = new Hashtable();

	private Rect m_ProgressBGRect = default(Rect);

	private Rect m_ProgressTitleRect = default(Rect);

	private Rect m_ProgressScreenRect = default(Rect);

	private Rect m_ProgressViewRect = default(Rect);

	private float m_ProgressPictRatio = 1f;

	private GUIStyle m_ProgressStyle;

	private Vector2 m_ProgressScrollPos = Vector2.zero;

	private float m_ProgressScrollIntertia;

	private GUIStyle m_ProgressTitleStyle;

	private int m_ProgressEndedActionCount;

	private int m_ProgressActionCount;

	private Texture2D m_ProgressLockedPastille;

	private Texture2D m_ProgressUnlockedPastille;

	private float m_ProgressScreenRatio = 1f;

	private float m_ProgressScreenWidth = 90f;

	protected static float s_PackBorderX = 2f;

	protected static float s_PackBorderY = 1f;

	private static InGameScript s_Instance;

	private Camera m_Camera_3D;

	private Camera m_Camera_2D;

	private MainCameraScript m_CameraScript;

	private InGameSoundPlayer m_SoundPlayer;

	private bool m_PauseIsPlaying;

	private int m_PauseCounter;

	private string m_PausedAnim = string.Empty;

	private float m_PausedAnimTime;

	private EState m_PausedState = EState.Count;

	private bool m_PausedRecord;

	private bool m_PausedMediaPlayer;

	private Vector3 m_PausedPosition = Vector3.zero;

	private Vector3 m_PausedScale = new Vector3(1f, 1f, 1f);

	private Quaternion m_PausedRotation = Quaternion.identity;

	private bool m_InputValidity = true;

	private GameObject m_Plane;

	private Quaternion m_PlaneRotation = Quaternion.identity;

	private Quaternion m_PlaneRotationDephase = Quaternion.identity;

	private bool m_IsPlaneDephase;

	private static int s_ReverseChangeFrameCount;

	private int m_CategoryRefreshRate = 1;

	private int m_CategoryRefreshCounter;

	private bool m_SkipRestoreAfterPause;

	public EState currentState = EState.Count;

	private EState m_State = EState.Count;

	private float m_StateTime;

	private float m_StateFrameCount;

	private float m_TimeSinceLastSuccess;

	private float m_TimeSinceLastState;

	private NightScript m_NightScript;

	private float m_MaxSteamTime = 10f;

	private bool m_SteamFirstWiping;

	private bool m_FirstDance;

	private bool m_RecordAudio = true;

	private int m_RecordRetest;

	private bool m_CheckInput = true;

	private bool m_BurnInput;

	private float m_TouchTime;

	private bool m_TouchMoved;

	private EState m_StartTouchState;

	private bool m_DetectTouchMoved;

	private int m_TickleCounter;

	private int m_TickleMinFrame = 5;

	private float m_TickleLoopTime;

	private bool m_TickleRestart;

	private EDirection m_RunDirection = EDirection.Count;

	private EDirection m_CurrentRunDirection = EDirection.Count;

	private bool m_RunCondition;

	private ESide m_EarSide = ESide.Count;

	private GameObject m_EarsInteractionPlane;

	private Vector3 m_EarWantedPosition = Vector3.zero;

	private Vector3 m_EarCurrentPosition = Vector3.zero;

	private EPullState m_PullState = EPullState.Release;

	private GameObject m_StringInteractionPlane;

	private GameObject m_String;

	private ESide m_LastBounce = ESide.Count;

	private int m_FakeBouncingCounter;

	private bool m_FirstAnim = true;

	private bool m_ForceIdle;

	private bool m_ForceIdleAnim;

	private bool m_ReactivateShadow;

	private GameObject m_RabbidShadow;

	private GameObject m_PanelObject;

	private GameObject m_PanelSign;

	private ScreenOrientation m_MemOrientation = ScreenOrientation.Portrait;

	private bool m_UseMediaPlayer = true;

	public EState[] lockedStates = new EState[0];

	public EState[] randomStates = new EState[0];

	public EState[] randomStatesInPack = new EState[0];

	public EState[] unlockedStates = new EState[0];

	public EState[] currentStates = new EState[0];

	public ETutorial[] tutorialsStates = new ETutorial[0];

	private List<EState> m_LockedStates = new List<EState>();

	private List<EState> m_RandomStates = new List<EState>();

	private List<EState> m_RandomStatesInPack = new List<EState>();

	private List<EState> m_UnlockedStates = new List<EState>();

	private List<EState> m_CurrentStates = new List<EState>();

	private List<ETutorial> m_TutorialsStates = new List<ETutorial>();

	private EPack m_CurrentPack = EPack.Count;

	private GameObject m_DuckFace;

	private GameObject m_DuckHand;

	private GameObject m_ClownBox;

	private int m_LoopIdleIndex;

	private GameObject m_PiranhaBowl;

	private GameObject m_Piranha;

	private GameObject m_Swatter;

	private bool m_SlideOnSwatter;

	private GameObject m_WCBrush;

	private bool m_WCBrushGrible;

	private GameObject m_Micro;

	private int m_MicroLickingCount;

	private GameObject m_PhoneReal;

	private GameObject m_PhoneRealCall;

	private Rect m_PhoneRealInteractiveRed = default(Rect);

	private Rect m_PhoneRealInteractiveGreen = default(Rect);

	private int m_PhoneRealRingStyle;

	private GameObject m_PopGun;

	private bool m_PopGunSlide;

	private GameObject m_Jetpack;

	private float m_JetpackWhirlTime = -1f;

	private GameObject m_Guitar;

	private GameObject m_Shield;

	private bool m_ShieldSlide;

	private GameObject m_EgyptianToiletPaper;

	private bool m_EgyptianToiletPaperSlide;

	private GameObject m_Bow;

	private GameObject m_GhettoBlaster;

	private GameObject m_Nunchaku;

	private GameObject m_RugbyBall;

	private GameObject m_SausagePolice;

	private GameObject m_Panel;

	private GameObject m_MiniGameShadow1;

	private FakeShadow m_FakeShadow1;

	private GameObject m_MiniGameShadow2;

	private FakeShadow m_FakeShadow2;

	private bool m_ReleaseMemory;

	private GameObject m_Anchor;

	private List<GameObject> m_ObjectsToClean = new List<GameObject>();

	private bool m_AnimBis;

	public AudioClip PhotoSound;

	public AudioClip ValidSound;

	private Texture2D m_MenuLastPhoto;

	private Texture2D m_Screenshot;

	private string m_PhotoPath = string.Empty;

	private string m_MiniPhotoPath = string.Empty;

	private int m_PhotoNumber;

	private bool m_IsListening;

	private bool m_CanMakePayments;

	private float m_RecordingTime = 1f;

	private string m_UsedMic = string.Empty;

	private int m_Frequency = 48000;

	private float[] m_Samples;

	private int m_QSamples = 1024;

	private float m_RefValue = 0.1f;

	private float m_RmsValue;

	private float m_DbValue = -160f;

	private EScroller m_CurrentScroller = EScroller.Count;

	private GUIScroller m_Scroller;

	private List<Texture2D>[] m_AllThumbnails = new List<Texture2D>[4];

	private bool m_DrawScroller = true;

	private int m_CostumeIndex;

	private ECostume m_StartCostume;

	private int m_EnvironmentIndex;

	private EEnvironment m_StartEnvironment;

	private float m_AutoHideTimer = -1f;

	private int m_PauseSoundCounter;

	private Hashtable m_SoundTableCallback = new Hashtable();

	private float m_MuteSwitchTimer;

	private bool m_Store;

	private EMessage m_Message = EMessage.None;

	private float m_LabelHeight = 25f;

	private Vector2 m_DescriptionScrollPos = Vector2.zero;

	private Rect m_StoreTitleRect;

	private Rect m_StoreDescriptionRect;

	private Rect m_StorePriceRect;

	private Rect m_StorePriceSymbolRect;

	private Rect m_StoreBuyRect;

	private GUIStyle m_TextContent;

	private EScrollState m_ScrollState;

	private string m_CurrencySymbol = string.Empty;

	private Texture2D m_CurrencySymbolTex;

	private string m_PriceTextStyle = "PriceText";

	private float m_FixedWidth;

	//private List<StoreKitProduct> m_ProductList;

	private EPack m_BuyPack = EPack.Count;

	public static InGameScript Instance
	{
		get
		{
			return s_Instance;
		}
	}

	private void Awake()
	{
		s_Instance = this;
	}

	public override void Start()
	{
		if (PlayerPrefs.HasKey("Goto"))
		{
			string text = PlayerPrefs.GetString("Goto");
			PlayerPrefs.DeleteKey("Goto");
			if (text.Equals("Dance"))
			{
				m_StartState = EState.Dance;
			}
			else if (text.Equals("Sing"))
			{
				m_StartState = EState.Micro;
			}
		}
		Utility.ShowActivityView(false);
		base.Start();
		Screen.sleepTimeout = 0;
		s_TargetOrientation = (int)AllInput.GetDeviceOrientation();
		s_CurrentOrientation = (int)AllInput.GetDeviceOrientation();
		m_LastOrientation = s_CurrentOrientation;
		s_StartOrientation = (int)Utility.LastDeviceOrientation;
		s_LastValidOrientation = s_StartOrientation;
		if (AllInput.IsMicroAvailable())
		{
			m_RecordAudio = true;
		}
		m_NewTexture_110x128 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/" + Localization.GetLanguage().ToString() + "/new_110x128");
		if (m_NewTexture_110x128 == null)
		{
			Utility.Log(ELog.Errors, "m_NewTexture_110x128 == null");
		}
		GlobalVariables.InitScoringTimer();
		CreateMoveKeys();
		StartPlugins();
		StartHelper();
		StartSounds();
		StartCustomize();
		StartPhoto();
		StartStore();
		StartScroller();
		StartInteractions();
		StartMiniGames();
		StartHUD();
		StartMediaPlayer();
		StartStoreKit();
		LoadAvailableStates();
		LoadAvailableTutorials();
		RecreateMiniGameThumbnailsBuffer();
		ActivateScroller(EScroller.MiniGame);
		CreateAnimationEvents();
		m_IntroLoadingScreen = Utility.CreateLoadingScreen();
		if (m_IntroLoadingScreen == null)
		{
			Utility.Log(ELog.Errors, "m_IntroLoadingScreen == null");
		}
		int num = PlayerPrefs.GetInt("_total_number_of_moves");
		int num2 = PlayerPrefs.GetInt("_total_number_of_ended_moves");
		m_DestroyIntroPanel = num2 == num;
		if (!m_DestroyIntroPanel && !IsSpecialStart() && !m_ShowProgress)
		{
			LoadIntroPanel();
			if (m_IntroPanelObject != null)
			{
				m_StartPosition = m_IntroPanelObject.transform.localPosition;
				m_IntroPanelObject.SetActiveRecursively(false);
			}
			else
			{
				Utility.Log(ELog.Errors, "m_IntroPanelObject == null");
			}
		}
		else
		{
			m_DestroyIntroPanel = false;
		}
		int num3 = 500;
		int num4 = 320;
		float num5 = ((float)blackBackground.width + Utility.RefWidth) / 2f;
		m_BlackBGRect = Utility.NewRect((Utility.RefWidth - num5) / 2f, 45f, num5, (float)blackBackground.height / 3f);
		if (Screen.width <= num3)
		{
			float num6 = (float)num3 - (float)Screen.width;
			float num7 = (float)num3 - (float)num4;
			num6 /= num7;
			num6 *= 0.5f;
			float num8 = 1f + num6;
			m_BlackBGRect.height *= num8;
		}
		m_BlackBGRectHeight = m_BlackBGRect.height;
		DataMining.OnLevelStart();
		AddPonctualAnimationClip("BunnyARPoses/", PhotoRoom.EARPose.pose00.ToString());
		PlayAnim(PhotoRoom.EARPose.pose00.ToString(), 0f);
		LoadIdleAnimationSet(ERabbid.InGame);
		LoadIdleAnims();
		string text2 = SystemInfo.deviceModel.ToUpperInvariant();
		if (text2.Contains("P1000".ToUpperInvariant()))
		{
			m_MicrophoneThreshold = 18f;
		}
	}

	private void StartWithOrientation()
	{
		if (ReverseScreen())
		{
			if (m_IntroLoadingScreen != null)
			{
				m_IntroLoadingScreen.transform.localEulerAngles = new Vector3(270f, 180f, 0f);
			}
			if (m_IntroPanelObject != null)
			{
				m_IntroPanelObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
				m_IntroPanelObject.transform.localPosition = new Vector3(0f - m_StartPosition.x, 0f - m_StartPosition.y, m_StartPosition.z);
			}
			SetTR(new Vector3(0f, 0.9f, -0.7f), new Vector3(0f, 0f, 180f));
		}
		else
		{
			if (m_IntroLoadingScreen != null)
			{
				m_IntroLoadingScreen.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			if (m_IntroPanelObject != null)
			{
				m_IntroPanelObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				m_IntroPanelObject.transform.localPosition = new Vector3(m_StartPosition.x, m_StartPosition.y, m_StartPosition.z);
			}
			SetTR(new Vector3(0f, -0.9f, -0.7f), new Vector3(0f, 0f, 0f));
		}
		s_TargetOrientation = s_LastValidOrientation;
		s_CurrentOrientation = s_LastValidOrientation;
	}

	public override void Update()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			return;
		}
		if (!IsPaused())
		{
			base.Update();
		}
		UpdateSound();
		m_InputValidity = true;
		if (!m_HasBeenUpdated)
		{
			m_HasBeenUpdated = true;
		}
		m_SoundTableCallback.Clear();
		if (m_IntroLoadingScreen != null)
		{
			StartWithOrientation();
			if (++m_UpdateFrameCount >= 50 && m_FixedUpdateFrameCount >= 50)
			{
				Object.DestroyImmediate(m_IntroLoadingScreen);
				m_IntroLoadingScreen = null;
				Utility.FreeMem(true);
				Utility.ClearActivityView();
				if (m_IntroPanelObject != null)
				{
					m_IntroPanelObject.SetActiveRecursively(true);
				}
				else
				{
					m_Fonctionality.Active = true;
				}
			}
			return;
		}
		if (m_IntroPanelObject != null)
		{
			StartWithOrientation();
			if (m_DestroyIntroPanel)
			{
				OnDestroyIntroPanel();
				m_DestroyIntroPanel = false;
			}
			return;
		}
		if (m_State == EState.Count)
		{
			StartGame();
		}
		s_GravityBlocker++;
		UpdateScrolling();
		UpdateInputs();
		UpdateStore();
		UpdateHUD();
		if (!IsPaused())
		{
			UpdateTouchInteractions();
			UpdateInteractions();
			UpdateMiniGames();
			GlobalVariables.UpdateScoringTimer(Time.deltaTime);
		}
		UpdateHelper();
		DataMining.InGameUpdate();
	}

	private void FixedUpdate()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			m_FrameCountBeforeNextScene--;
			if (m_FrameCountBeforeNextScene == 0)
			{
				RenderSettings.fog = false;
				Localization.GenerateWaitText();
				Application.LoadLevel(m_NextSceneName);
			}
			return;
		}
		if (m_BouncingCounter > 0)
		{
			m_BouncingCounter--;
		}
		if (m_BlockBouncingCounter > 0)
		{
			m_BlockBouncingCounter--;
		}
		if (m_BackPressed >= 0)
		{
			m_BackPressed--;
		}
		m_FixedUpdateFrameCount++;
	}

	private void LateUpdate()
	{
		if (m_RemoveTPose)
		{
			PhotoRoom.EARPose aRPoseEnum = PhotoRoom.GetARPoseEnum(m_CurrentAnimation);
			if (aRPoseEnum != PhotoRoom.EARPose.Count)
			{
				StopAnim();
				RemoveAnimationClip(m_CurrentAnimation);
			}
			m_RemoveTPose = false;
		}
		LateUpdateInteractions();
		LateUpdateMiniGames();
	}

	private void OnApplicationQuit()
	{
		s_GravityBlocker = 0;
		DataMining.OnApplicationQuit(m_State != EState.CustomCostume && m_State != EState.CustomEnvironment);
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			DataMining.OnApplicationQuit(m_State != EState.CustomCostume && m_State != EState.CustomEnvironment);
			if (m_HasBeenUpdated && AllInput.IsMicroAvailable())
			{
				m_PausedRecord = IsListening();
				StopListener();
			}
			s_ReverseBeforePausing = ReverseScreen();
		}
		else if (m_HasBeenUpdated && AllInput.IsMicroAvailable() && m_PausedRecord)
		{
			Listen();
		}
		s_GravityBlocker = 0;
		Pause(pause);
	}

	private void OnDisable()
	{
		OnDisableScroller();
		OnDisableHUD();
		OnDisableMediaPlayer();
		OnDisableStoreKit();
		OnDisablePlugins();
		DataMining.InGameOnDisable();
	}

	private void OnGUI()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			return;
		}
		if (ReverseScreen())
		{
			Vector2 pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
			GUIUtility.RotateAroundPivot(180f, pivotPoint);
		}
		if ((bool)m_CommonSkin)
		{
			GUI.skin = m_CommonSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "Game Hud: Skin not found");
		}
		StoreGUI();
		if (m_Store)
		{
			return;
		}
		if (m_IntroLoadingScreen != null || m_OutroLoadingScreen != null)
		{
			GUIUtils.DrawLoadingText();
			return;
		}
		HUDGUI();
		if (m_IntroPanelObject != null)
		{
			DrawIntroPanel();
			return;
		}
		switch (m_State)
		{
		case EState.CustomCostume:
		case EState.CustomEnvironment:
			CustomizeGUI();
			break;
		default:
			MiniGamesGUI();
			break;
		case EState.Night:
			break;
		}
		DrawRewards();
		ScrollerGUI();
	}

	private void DrawIntroPanel()
	{
		int num = PlayerPrefs.GetInt("_total_number_of_moves");
		int num2 = PlayerPrefs.GetInt("_total_number_of_ended_moves");
		GUISkin skin = GUI.skin;
		GUI.skin = m_InGameSkin;
		if (GUI.Button(Utility.NewRect(250f, 130f, 64f, 64f), string.Empty, "CrossButton"))
		{
			m_DestroyIntroPanel = true;
		}
		string text = "IntroPanelText";
		if (Utility.IsFourthGenAppleDevice())
		{
			text += "Huge";
		}
		GUI.Label(Utility.NewRect(50f, 180f, 220f, 180f), Localization.GetLocalizedText(ELoc.FindAllMoves), text);
		GUI.Label(Utility.NewRect(35f, 300f, 250f, 180f), Localization.GetLocalizedText(ELoc.CurrentScore) + " " + num2 + "/" + num, text);
		GUI.skin = skin;
		if (Input.GetKeyDown(KeyCode.Escape) && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			GoToMainMenu();
		}
	}

	private void DrawRewards()
	{
		GUI.skin = m_CommonSkin;
		bool flag = m_NewWallpaper || m_NewCostume || m_NewARPict || m_NewEnvironment || m_NewStripe;
		if (m_NewMoveCounter <= 0 && !flag)
		{
			return;
		}
		string empty = string.Empty;
		if (blackBackground != null)
		{
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.normal.background = blackBackground;
			if (flag)
			{
				m_BlackBGRect.height = m_BlackBGRectHeight * 1.2f;
			}
			else
			{
				m_BlackBGRect.height = 2f * m_BlackBGRectHeight / 3f;
			}
			if (GUI.Button(m_BlackBGRect, string.Empty, gUIStyle))
			{
				if (flag)
				{
					m_GoodiesTime = kGoodiesTime;
				}
				else
				{
					m_NewMoveCounter = 0;
				}
			}
		}
		m_NewMoveCounter--;
		int num = PlayerPrefs.GetInt("_total_number_of_moves");
		int num2 = PlayerPrefs.GetInt("_total_number_of_ended_moves");
		empty = ((num2 != num) ? (empty + Localization.GetLocalizedText(ELoc.NewMove) + "\n") : (empty + Localization.GetLocalizedText(ELoc.YouDidIt) + "\n"));
		string text = empty;
		empty = text + num2 + "/" + num;
		if (flag)
		{
			string text2 = string.Empty;
			if (m_NewWallpaper)
			{
				text2 = Localization.GetLocalizedText(ELoc.NewWallpaper);
			}
			else if (m_NewCostume)
			{
				text2 = Localization.GetLocalizedText(ELoc.NewWallpaper);
			}
			else if (m_NewARPict)
			{
				text2 = Localization.GetLocalizedText(ELoc.NewARPict);
			}
			else if (m_NewEnvironment)
			{
				text2 = Localization.GetLocalizedText(ELoc.NewEnvironment);
			}
			else if (m_NewStripe)
			{
				text2 = Localization.GetLocalizedText(ELoc.NewStripe);
			}
			m_GoodiesTime += Time.deltaTime;
			if (m_GoodiesTime >= kGoodiesTime)
			{
				m_GoodiesTime = 0f;
				if (m_NewWallpaper)
				{
					m_NewWallpaper = false;
				}
				else if (m_NewCostume)
				{
					m_NewCostume = false;
				}
				else if (m_NewARPict)
				{
					m_NewARPict = false;
				}
				else if (m_NewEnvironment)
				{
					m_NewEnvironment = false;
				}
				else if (m_NewStripe)
				{
					m_NewStripe = false;
				}
			}
			empty = empty + "\n" + text2;
		}
		float num3 = 1f;
		string text3 = "CenterTextContent";
		if (Screen.width > 500)
		{
			text3 = "TextTitle";
			num3 = 2f;
		}
		float num4 = m_BlackBGRect.width * 0.8f;
		float num5 = (m_BlackBGRect.width - num4) / 2f;
		Rect position = new Rect(m_BlackBGRect.xMin + num5 - num3, m_BlackBGRect.yMin, num4, m_BlackBGRect.height);
		GUI.contentColor = new Color(0.08f, 0.35f, 0.54f, 1f);
		GUI.Label(position, empty, text3);
		position = new Rect(m_BlackBGRect.xMin + num5 + num3, m_BlackBGRect.yMin, num4, m_BlackBGRect.height);
		GUI.contentColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(position, empty, text3);
	}

	private void ManageMicrophoneInteractions()
	{
		m_IsBlowGesture = false;
		m_IsYellGesture = false;
		UpdateRecording();
		if (m_IsBlowing)
		{
			m_BlowTime += Time.deltaTime;
		}
		if (Time.frameCount % 20 != 0 || !AllInput.IsMicroAvailable() || !IsListening())
		{
			return;
		}
		UpdateLevels();
		GlobalVariables.s_AveragePower = GetAveragePower();
		if (m_BouncingCounter != 0)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		float num = 2f;
		float s_AveragePower = GlobalVariables.s_AveragePower;
		if (m_IsBlowing)
		{
			if (s_AveragePower < m_MicrophoneThreshold)
			{
				m_IsBlowing = false;
				if (m_BlowTime > 0.1f)
				{
					if (m_BlowTime < num)
					{
						flag2 = true;
					}
					else if (!m_IsBlowingDone)
					{
						m_IsBlowingDone = true;
						flag = true;
					}
				}
			}
			else if (m_BlowTime > num && !m_IsBlowingDone)
			{
				flag = true;
				m_IsBlowingDone = true;
			}
		}
		else if (s_AveragePower > m_MicrophoneThreshold)
		{
			m_BlowTime = 0f;
			m_IsBlowing = true;
			m_IsBlowingDone = false;
		}
		if (flag || GlobalVariables.FAKE_BLOW)
		{
			Utility.Log(ELog.GameplayDebug, "m_IsBlowGesture = true: ");
			m_IsBlowGesture = true;
		}
		else if (flag2 || GlobalVariables.FAKE_YELL)
		{
			Utility.Log(ELog.GameplayDebug, "m_IsYellGesture = true: ");
			m_IsYellGesture = true;
		}
		GlobalVariables.FAKE_BLOW = false;
		GlobalVariables.FAKE_YELL = false;
	}

	private void UpdateTouchInteractions()
	{
		m_IsBurpGesture = false;
		m_IsCircleGesture = false;
		if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 1)
		{
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_FirstTouch = new Vector3(AllInput.GetXPosition(0), AllInput.GetYPosition(0), 0f);
				m_Points.Clear();
			}
			else
			{
				if (AllInput.GetState(0) != AllInput.EState.Moved || RenderSettings.fog)
				{
					return;
				}
				Vector3 vector = new Vector3(AllInput.GetXPosition(0), AllInput.GetYPosition(0), 0f);
				m_Points.Add(vector);
				if (m_Points.Count <= 5)
				{
					return;
				}
				float f = Vector3.Distance(m_FirstTouch, vector);
				if (Mathf.Abs(f) < kCircleClosureDistanceVariance && m_Points.Count > 5)
				{
					if (DetectCircle())
					{
						m_IsCircleGesture = true;
					}
					m_FirstTouch = new Vector3(AllInput.GetXPosition(0), AllInput.GetYPosition(0), 0f);
					m_Points.Clear();
				}
			}
		}
		else if ((!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 2) || GlobalVariables.FAKE_BURP)
		{
			Vector3 vector2 = new Vector3(AllInput.GetXPosition(0), AllInput.GetYPosition(0), 0f);
			Vector3 vector3 = new Vector3(AllInput.GetXPosition(1), AllInput.GetYPosition(1), 0f);
			if (AllInput.GetState(0) == AllInput.EState.Began || AllInput.GetState(1) == AllInput.EState.Began || (GlobalVariables.FAKE_BURP && m_InitialDistance == 0f))
			{
				if (GlobalVariables.FAKE_BURP)
				{
					m_InitialDistance = 40f;
				}
				else
				{
					m_InitialDistance = Vector3.Distance(vector2, vector3);
				}
			}
			if (((AllInput.GetState(0) == AllInput.EState.Moved || AllInput.GetState(0) == AllInput.EState.Stationary) && (AllInput.GetState(1) == AllInput.EState.Moved || AllInput.GetState(1) == AllInput.EState.Stationary)) || GlobalVariables.FAKE_BURP)
			{
				if (m_InitialDistance == 0f)
				{
					if (GlobalVariables.FAKE_BURP)
					{
						m_InitialDistance = 40f;
					}
					else
					{
						m_InitialDistance = Vector3.Distance(vector2, vector3);
					}
				}
				else
				{
					if (!(m_InitialDistance - 40f > Vector3.Distance(vector2, vector3)) && !GlobalVariables.FAKE_BURP)
					{
						return;
					}
					if (!GlobalVariables.FAKE_BURP)
					{
						Vector3 vector4 = vector2 - vector3;
						switch ((DeviceOrientation)s_CurrentOrientation)
						{
						case DeviceOrientation.LandscapeLeft:
						case DeviceOrientation.LandscapeRight:
							if (Mathf.Abs(vector4.y) < Mathf.Abs(vector4.x))
							{
								return;
							}
							break;
						case DeviceOrientation.Portrait:
						case DeviceOrientation.PortraitUpsideDown:
							if (Mathf.Abs(vector4.x) < Mathf.Abs(vector4.y))
							{
								return;
							}
							break;
						case DeviceOrientation.FaceDown:
							return;
						}
					}
					m_InitialDistance = 0f;
					m_IsBurpGesture = true;
				}
			}
			else
			{
				m_InitialDistance = 0f;
			}
		}
		else
		{
			m_InitialDistance = 0f;
		}
	}

	private void OnDestroyIntroPanel()
	{
		Object.DestroyImmediate(m_IntroPanelObject);
		m_IntroPanelObject = null;
		Utility.FreeMem();
		m_Fonctionality.Active = true;
	}

	private void LoadIntroPanel()
	{
		m_IntroPanelObject = Utility.InstanciateFromResources("MiniGames/StartPanel", 8);
		if (!(m_IntroPanelObject != null))
		{
			return;
		}
		string text = Utility.GetTexPath() + "MiniGames/Panel/";
		Transform child = m_IntroPanelObject.transform.GetChild(0);
		if (child != null)
		{
			child.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(text + "PanelHandle");
			child = child.GetChild(0);
			if (child != null)
			{
				child.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(text + "PanelFront");
			}
		}
	}

	private void StartGame()
	{
		SetState(m_StartState);
		if (IsSpecialStart())
		{
			m_StartState = EState.Idle;
		}
		else
		{
			PlayAnim(m_IdleAnims[Random.Range(0, m_IdleAnims.Count)]);
		}
	}

	private bool IsSpecialStart()
	{
		return m_StartState != EState.Idle;
	}

	private bool ShouldWallBounce()
	{
		Vector3 gravityDirection = GetGravityDirection();
		return Mathf.Abs(gravityDirection.x) > kAccelerationThreshold || Mathf.Abs(gravityDirection.y) > kAccelerationThreshold || Mathf.Abs(gravityDirection.z) > kAccelerationThreshold || GlobalVariables.FAKE_HWALLBOUNCE || GlobalVariables.FAKE_VWALLBOUNCE || GlobalVariables.FAKE_SWALLBOUNCE;
	}

	private Vector3 GetGravityDirection()
	{
		Vector3 zero = Vector3.zero;
		if (s_CurrentOrientation == 1)
		{
			zero.x = AllInput.GetAcceleration().x;
			zero.y = AllInput.GetAcceleration().y;
			zero.z = AllInput.GetAcceleration().z;
		}
		else if (s_CurrentOrientation == 2)
		{
			zero.x = 0f - AllInput.GetAcceleration().x;
			zero.y = 0f - AllInput.GetAcceleration().y;
			zero.z = AllInput.GetAcceleration().z;
		}
		else if (s_CurrentOrientation == 4)
		{
			zero.x = AllInput.GetAcceleration().y;
			zero.y = 0f - AllInput.GetAcceleration().x;
			zero.z = AllInput.GetAcceleration().z;
		}
		else if (s_CurrentOrientation == 3)
		{
			zero.x = 0f - AllInput.GetAcceleration().y;
			zero.y = AllInput.GetAcceleration().x;
			zero.z = AllInput.GetAcceleration().z;
		}
		return zero;
	}

	private EDirection GetDirection(Vector3 dir)
	{
		EDirection dir2 = EDirection.Count;
		if (dir.sqrMagnitude > 2f)
		{
			dir2 = ((Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) ? ((dir.x > 0f) ? EDirection.Right : EDirection.Left) : ((dir.y > 0f) ? EDirection.Up : EDirection.Down));
		}
		return TransformDirectionToGravity(dir2);
	}

	private EDirection TransformDirectionToGravity(EDirection dir)
	{
		if (dir == EDirection.Count)
		{
			return EDirection.Count;
		}
		int num = 0;
		switch ((DeviceOrientation)s_CurrentOrientation)
		{
		case DeviceOrientation.LandscapeLeft:
			num = 1;
			break;
		case DeviceOrientation.PortraitUpsideDown:
			num = 2;
			break;
		case DeviceOrientation.LandscapeRight:
			num = 3;
			break;
		}
		if (num != 0)
		{
			num = (int)(num + dir) % 4;
			dir = (EDirection)num;
		}
		return dir;
	}

	private void CreateAnimationEvents()
	{
		CreateAnimationEvent("standby_night", "PlayStandbyShoutSound", 0);
		CreateAnimationEvent("poke2", "PlayPokeEyeSound", 0);
		CreateAnimationEvent("poke1", "PlayPokeTummySound", 0);
	}

	private void CreateMoveKeys()
	{
		m_NewMoveArray.Clear();
		if (PlayerPrefs.HasKey("run"))
		{
			m_NewMoveArray.Add("run");
		}
		if (PlayerPrefs.HasKey("poke_eye"))
		{
			m_NewMoveArray.Add("poke_eye");
		}
		if (PlayerPrefs.HasKey("poke_body"))
		{
			m_NewMoveArray.Add("poke_body");
		}
		if (PlayerPrefs.HasKey("wallbounce_horizontal"))
		{
			m_NewMoveArray.Add("wallbounce_horizontal");
		}
		if (PlayerPrefs.HasKey("wallbounce_vertical"))
		{
			m_NewMoveArray.Add("wallbounce_vertical");
		}
		if (PlayerPrefs.HasKey("wallbounce_screen"))
		{
			m_NewMoveArray.Add("wallbounce_screen");
		}
		if (PlayerPrefs.HasKey("rotate_down"))
		{
			m_NewMoveArray.Add("rotate_down");
		}
		if (PlayerPrefs.HasKey("rotate"))
		{
			m_NewMoveArray.Add("rotate");
		}
		if (PlayerPrefs.HasKey("yell"))
		{
			m_NewMoveArray.Add("yell");
		}
		if (PlayerPrefs.HasKey("blow"))
		{
			m_NewMoveArray.Add("blow");
		}
		if (PlayerPrefs.HasKey("burp"))
		{
			m_NewMoveArray.Add("burp");
		}
		if (PlayerPrefs.HasKey("turn"))
		{
			m_NewMoveArray.Add("turn");
		}
		if (PlayerPrefs.HasKey("tickle"))
		{
			m_NewMoveArray.Add("tickle");
		}
		if (PlayerPrefs.HasKey("string"))
		{
			m_NewMoveArray.Add("string");
		}
		if (PlayerPrefs.HasKey("ear"))
		{
			m_NewMoveArray.Add("ear");
		}
		if (PlayerPrefs.HasKey("dance"))
		{
			m_NewMoveArray.Add("dance");
		}
	}

	private void OnEnterGameMode()
	{
		RecreateMiniGameThumbnailsBuffer();
		ActivateScroller(EScroller.MiniGame);
	}

	private void OnLeaveGameMode()
	{
		DataMining.OnLeaveGameMode();
	}

	private void StartCustomize()
	{
		CreateEnvironment();
		string str = PlayerPrefs.GetString("EnvironmentName");
		m_StartEnvironment = Rabbid.GetEnvironmentEnum(str);
		m_EnvironmentIndex = (int)m_StartEnvironment;
		if (m_EnvironmentIndex != -1)
		{
			SwapEnvironment(m_StartEnvironment);
		}
		string str2 = PlayerPrefs.GetString("CostumeName");
		m_StartCostume = Rabbid.GetCostumeEnum(str2);
		m_CostumeIndex = (int)m_StartCostume;
		m_StartRotation = base.transform.eulerAngles.y;
		SwapCostume(m_StartCostume);
		FindCostumeEars();
		AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Costume, m_StartCostume.ToString());
		AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Environment, m_StartEnvironment.ToString());
		DataMining.OnLevelStart();
	}

	private void CustomizeGUI()
	{
		GUIUtils.EAnswer eAnswer = GUIUtils.EAnswer.Count;
		if (m_AskCostumeConfirm)
		{
			eAnswer = GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.ConfirmCostume));
			switch (eAnswer)
			{
			case GUIUtils.EAnswer.Yes:
				ConfirmCostume();
				OnEndConfirm();
				break;
			case GUIUtils.EAnswer.No:
				SwapCostume(m_StartCostume);
				SyncCostumeAnim();
				if (m_Scroller != null)
				{
					m_Scroller.Resize(m_AllThumbnails[1].ToArray(), m_IndexSave);
				}
				OnEndConfirm();
				break;
			}
		}
		else if (m_AskEnvironmentConfirm)
		{
			eAnswer = GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.ConfirmEnvironment));
			switch (eAnswer)
			{
			case GUIUtils.EAnswer.Yes:
				ConfirmEnvironment();
				OnEndConfirm();
				break;
			case GUIUtils.EAnswer.No:
				SwapEnvironment(m_StartEnvironment);
				if (m_Scroller != null)
				{
					m_Scroller.Resize(m_AllThumbnails[2].ToArray(), m_IndexSave);
				}
				OnEndConfirm();
				break;
			}
		}
		if (eAnswer != GUIUtils.EAnswer.Count || m_Leave)
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			eulerAngles.y = m_StartRotation;
			SetR(eulerAngles);
			SetState(EState.Idle);
			m_AskCostumeConfirm = false;
			m_AskEnvironmentConfirm = false;
			m_Leave = false;
			OnEndConfirm();
		}
	}

	private void OnEnterCustomCostume()
	{
		ClearAdditionalAnimationClips(false);
		LoadSound("Common/", ESound.N05_Costel);
		PlaySound(ESound.N05_Costel, 0.5f, true);
		LoadIdleAnimationSet(ERabbid.Customize);
		for (int i = 0; i < m_IdleAnims.Count; i++)
		{
			AddAnimationClip("BunnyIdle/", m_IdleAnims[i], false);
		}
		AddAnimationClip("BunnyInteractions/PresentTuto/", "basic_sign");
		InstanciateAnimationClips();
		CreateBaseAnimEvents();
		LoadPanel();
		EnablePanel(EPanel.ArrowTurn, "basic_sign", m_Panel);
		PlayAnim("basic_sign");
		m_IndexSave = m_CostumeIndex;
		RecreateCostumeThumbnailsBuffer();
		ActivateScroller(EScroller.Costume);
		if (m_Scroller != null)
		{
			m_Scroller.Resize(m_AllThumbnails[1].ToArray(), m_CostumeIndex);
		}
		m_CurrentRotation = (m_WantedRotation = m_ReferenceRotation);
	}

	private void OnUpdateCustomCostume()
	{
		ComputeRabbidFall();
		if (m_FallState == EState.Count)
		{
			ManageAnimations();
			ManageInputs();
			m_CurrentRotation = Utility.SmoothAngle(m_CurrentRotation, m_WantedRotation, 0.14f);
			Vector3 eulerAngles = ComputeRabbidDefaultOrientation();
			if (s_CurrentOrientation == 3)
			{
				eulerAngles.x = 0f - m_CurrentRotation;
			}
			else if (s_CurrentOrientation == 4)
			{
				eulerAngles.x = m_CurrentRotation;
			}
			else if (s_CurrentOrientation == 1)
			{
				eulerAngles.y = m_CurrentRotation;
			}
			else
			{
				eulerAngles.y = 0f - m_CurrentRotation;
			}
			base.transform.eulerAngles = eulerAngles;
		}
		if (m_Costume != null)
		{
			m_Costume.transform.eulerAngles = base.transform.eulerAngles;
		}
	}

	private void OnLeaveCustomCostume()
	{
		StopSound();
		ClearAudioClips();
		ClearAdditionalAnimationClips(false);
		LoadIdleAnimationSet(ERabbid.InGame);
		LoadIdleAnims();
		InstanciateAnimationClips();
		m_Panel = UnloadMiniGameObject(m_Panel);
		RecreateMiniGameThumbnailsBuffer();
		ActivateScroller(EScroller.MiniGame);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseCustomCostume(bool pauseResume)
	{
	}

	private void OnEnterCustomEnvironment()
	{
		LoadSound("Common/", ESound.N05_Costel);
		PlaySound(ESound.N05_Costel, 0.5f, true);
		LoadIdleAnimationSet(ERabbid.Customize);
		for (int i = 0; i < m_IdleAnims.Count; i++)
		{
			AddAnimationClip("BunnyIdle/", m_IdleAnims[i], false);
		}
		PlayAnim(m_IdleAnims[Random.Range(0, m_IdleAnims.Count)]);
		m_IndexSave = m_EnvironmentIndex;
		RecreateEnvironmentThumbnailsBuffer();
		ActivateScroller(EScroller.Environment);
		if (m_Scroller != null)
		{
			m_Scroller.Resize(m_AllThumbnails[2].ToArray(), m_EnvironmentIndex);
		}
		m_CurrentRotation = (m_WantedRotation = m_ReferenceRotation);
	}

	private void OnUpdateCustomEnvironment()
	{
		ComputeRabbidFall();
		if (m_FallState == EState.Count)
		{
			ManageAnimations();
		}
	}

	private void OnLeaveCustomEnvironment()
	{
		StopSound();
		ClearAudioClips();
		ClearAdditionalAnimationClips(false);
		LoadIdleAnimationSet(ERabbid.InGame);
		LoadIdleAnims();
		InstanciateAnimationClips();
		RecreateMiniGameThumbnailsBuffer();
		ActivateScroller(EScroller.MiniGame);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseCustomEnvironment(bool pauseResume)
	{
	}

	private void SelectCostume()
	{
		if (m_AchievementsTable == null)
		{
			return;
		}
		EProductID productID = m_AchievementsTable.GetProductID((ECostume)m_CostumeIndex);
		if (!Utility.IsUnlockedProduct(productID))
		{
			Request(productID);
			return;
		}
		StopAnim();
		ClearAdditionalAnimationClips(false);
		m_CurrentCostume = (ECostume)m_CostumeIndex;
		SwapCostume(m_CurrentCostume);
		LoadIdleAnimationSet(ERabbid.Customize);
		for (int i = 0; i < m_IdleAnims.Count; i++)
		{
			AddAnimationClip("BunnyIdle/", m_IdleAnims[i], false);
			CreateAnimEventDance(m_IdleAnims[i]);
			CreateAnimEventHideSeek(m_IdleAnims[i]);
		}
		AddAnimationClip("BunnyInteractions/PresentTuto/", "basic_sign");
		InstanciateAnimationClips();
		if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Costume, m_CurrentCostume.ToString()))
		{
			RecreateCostumeThumbnailsElement(m_CostumeIndex);
			GUIUtils.GetScroller(1).Resize(m_AllThumbnails[1].ToArray(), -1);
		}
		DataMining.Increment(DataMining.s_Seen + m_CurrentCostume);
	}

	private void SelectNextCostume()
	{
		m_CostumeIndex++;
		if (m_CostumeIndex == 12)
		{
			m_CostumeIndex = 0;
		}
		SelectCostume();
		if (m_Scroller != null)
		{
			m_Scroller.SetScrollPos(m_CostumeIndex, true);
		}
	}

	private void SelectPrevCostume()
	{
		m_CostumeIndex--;
		if (m_CostumeIndex == -1)
		{
			m_CostumeIndex = 11;
		}
		SelectCostume();
		if (m_Scroller != null)
		{
			m_Scroller.SetScrollPos(m_CostumeIndex, true);
		}
	}

	private void SelectEnvironment()
	{
		if (m_AchievementsTable == null)
		{
			return;
		}
		EProductID productID = m_AchievementsTable.GetProductID((EEnvironment)m_EnvironmentIndex);
		if (!Utility.IsUnlockedProduct(productID))
		{
			Request(productID);
			return;
		}
		m_CurrentEnvironment = (EEnvironment)m_EnvironmentIndex;
		SwapEnvironment(m_CurrentEnvironment);
		if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.Environment, m_CurrentEnvironment.ToString()))
		{
			RecreateEnvironmentThumbnailsElement(m_EnvironmentIndex);
			if (m_Scroller != null)
			{
				m_Scroller.Resize(m_AllThumbnails[2].ToArray(), -1);
			}
		}
		DataMining.Increment(DataMining.s_Seen + m_CurrentEnvironment);
	}

	private void CreateBaseAnimEvents()
	{
		CreateAnimationEvent("IchyAss", "Custom_PlayBwahHSound", 40);
		CreateAnimationEvent("ComePlay", "PlayBwahSound", 90);
		CreateAnimationEvent("Yarning", "Custom_PlayBwahHSound", 20);
		CreateAnimationEvent("TurnInPlace", "PlayBwahSound", 25);
		CreateAnimationEvent("BellTap", "PlayBwahSound", 20);
		CreateAnimationEvent("Tired", "PlayBwahSound", 30);
		CreateAnimationEvent("Idle_Look", "PlayBwahSound", 30);
		CreateAnimationEvent("Idle_Look", "PlayBwahSound", 70);
		CreateAnimationEvent("Idle_Look", "PlayBwahSound", 120);
		CreateAnimationEvent("Idle_Scratch", "PlayBwahSound", 100);
		CreateAnimationEvent("Idle_Turn", "PlayBwahSound", 30);
	}

	private void CreateAnimEventDance(string checkAnim)
	{
		if (!(checkAnim != "Idle_Dance"))
		{
			CreateAnimationEvent("Idle_Dance", "PlayBwahSound", 70);
		}
	}

	private void CreateAnimEventHideSeek(string checkAnim)
	{
		if (!(checkAnim != "HideSeek"))
		{
			CreateAnimationEvent("HideSeek", "PlayBwahSound", 40);
			CreateAnimationEvent("HideSeek", "PlayBwahSound", 70);
		}
	}

	private void Custom_PlayBwahHSound()
	{
		PlaySound(EBwahSound.Bwah_H);
	}

	private void ManageAnimations()
	{
		bool flag = !base.animIsPlaying || m_ForceCustomIdle;
		if (!flag)
		{
			float time = base.GetComponent<Animation>()[m_CurrentAnimation].time;
			flag = m_CurrentAnimLength > 1f && time >= m_CurrentAnimLength - 0.3f;
		}
		if (!flag)
		{
			return;
		}
		string text = m_IdleAnims[Random.Range(0, m_IdleAnims.Count)];
		if (!base.animIsPlaying || text != m_CurrentAnimation)
		{
			PlayAnim(text);
			if (m_Panel != null && m_Panel.active)
			{
				m_Panel = UnloadMiniGameObject(m_Panel);
			}
		}
		m_ForceCustomIdle = false;
	}

	private void ManageInputs()
	{
		if (Utility.InputStoppedByActivity() || !m_InputValidity || AllInput.GetTouchCount() != 1 || !m_CheckInput || m_BurnInput)
		{
			return;
		}
		if (AllInput.GetState(0) == AllInput.EState.Leave)
		{
			if (!(m_CurrentAnimation == "basic_sign") || !(m_CurrentAnimTime > 2f))
			{
				return;
			}
			string cast = RaycastUnderFinger(0);
			if (GetCollider(cast) == ECollider.Panel)
			{
				if (m_Panel != null)
				{
					m_Panel = UnloadMiniGameObject(m_Panel);
				}
				m_ForceCustomIdle = true;
			}
		}
		else if (AllInput.GetState(0) == AllInput.EState.Moved)
		{
			m_WantedRotation -= AllInput.TransformWithOrientation(AllInput.GetDelta(0)).x;
			while (m_WantedRotation >= 360f)
			{
				m_WantedRotation -= 360f;
			}
			while (m_WantedRotation < 0f)
			{
				m_WantedRotation += 360f;
			}
		}
	}

	private void QuitCustomizeMode()
	{
		if (m_StartCostume != m_CurrentCostume)
		{
			Utility.StopInputByActivity(true);
			m_AskCostumeConfirm = true;
		}
		else if (m_StartEnvironment != m_CurrentEnvironment)
		{
			Utility.StopInputByActivity(true);
			m_AskEnvironmentConfirm = true;
		}
		else
		{
			m_Leave = true;
		}
	}

	private void ConfirmCostume()
	{
		m_StartCostume = m_CurrentCostume;
		LoadAvailableStates();
		RecreateMiniGameThumbnailsBuffer();
		GamePlayerPrefs.SetString("CostumeName", m_CurrentCostume.ToString());
		DataMining.OnConfirmCostume(m_CurrentCostume);
	}

	private void ConfirmEnvironment()
	{
		m_StartEnvironment = m_CurrentEnvironment;
		GamePlayerPrefs.SetString("EnvironmentName", m_CurrentEnvironment.ToString());
		DataMining.Increment(DataMining.s_Applied + m_CurrentEnvironment);
	}

	private void OnEndConfirm()
	{
		Utility.StopInputByActivity(false);
	}

	private Vector3 ComputeRabbidDefaultOrientation()
	{
		Vector3 zero = Vector3.zero;
		if (s_CurrentOrientation == 3)
		{
			zero.z = 90f;
		}
		else if (s_CurrentOrientation == 4)
		{
			zero.z = 270f;
		}
		else if (s_CurrentOrientation == 2)
		{
			zero.z = 180f;
		}
		if (DephaseRabbid())
		{
			zero.z += 180f;
		}
		return zero;
	}

	private void ComputeRabbidFall()
	{
		if (m_FallState == EState.Count)
		{
			m_FallState = DetectGravityChangesInCustom();
			switch (m_FallState)
			{
			case EState.Rotation:
				base.transform.eulerAngles = ComputeRabbidDefaultOrientation();
				OnEnterRotation();
				break;
			case EState.FaceDown:
				base.transform.eulerAngles = ComputeRabbidDefaultOrientation();
				OnEnterFaceDown();
				break;
			}
			if (m_FallState != EState.Count && m_Panel != null && m_Panel.active)
			{
				m_Panel = UnloadMiniGameObject(m_Panel);
			}
		}
		switch (m_FallState)
		{
		case EState.Rotation:
			if (CheckEndRotation())
			{
				OnLeaveRotation();
				m_CurrentRotation = (m_WantedRotation = 0f);
				m_FallState = EState.Count;
			}
			break;
		case EState.FaceDown:
			if (CheckEndFaceDown())
			{
				OnLeaveFaceDown();
				m_FallState = EState.Count;
			}
			else
			{
				ComputeFaceDown();
			}
			break;
		}
	}

	private void StartHUD()
	{
		GameObject gameObject = Utility.LoadGameObject("GUI/PlaneFade", m_Camera_2D.transform);
		if (gameObject != null)
		{
			Utility.SetLayerRecursivly(gameObject.transform, 11);
			m_Flash = gameObject.GetComponent<Fader>();
		}
		GameObject gameObject2 = Utility.LoadGameObject("GUI/Fonctionality", m_Camera_2D.transform);
		if (gameObject2 != null)
		{
			Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null && componentsInChildren.Length > 0)
			{
				Material material = (Material)Object.Instantiate(componentsInChildren[0].sharedMaterial);
				material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/GUIAtlas");
				Renderer[] array = componentsInChildren;
				foreach (Renderer renderer in array)
				{
					if (renderer != null)
					{
						renderer.material = material;
					}
				}
			}
			gameObject2.SetActiveRecursively(false);
			gameObject2.active = true;
			Utility.SetLayerRecursivly(gameObject2.transform, 11);
			m_FonctionalityRoot = gameObject2.transform.GetChild(0).GetComponent<FonctionalityItem>();
			m_Fonctionality = m_Camera_2D.GetComponent<Fonctionality>();
			m_Fonctionality.Initialize(m_FonctionalityRoot);
			m_Fonctionality.Enable(false, FonctionalityItem.EType.LastPicture);
			m_Fonctionality.Enable(false, FonctionalityItem.EType.LastScreenshot);
			m_Fonctionality.Enable(false, FonctionalityItem.EType.Sing);
			if (!AllInput.IsCaptureAvailable() || !AllInput.HasPositionChoice())
			{
				m_Fonctionality.Enable(false, FonctionalityItem.EType.SwitchCamera);
			}
			GameObject gameObject3 = new GameObject("Merger");
			m_FonctionalityMerger = gameObject3.AddComponent<MeshMerger>();
			gameObject3.layer = 11;
			gameObject3.transform.parent = m_Camera_2D.transform;
			gameObject3.transform.position = m_FonctionalityRoot.transform.position;
			m_FonctionalityMerger.SetSourceMeshesRoot(m_FonctionalityRoot.gameObject);
		}
		InitProgress();
		Fader.Shown += OnFaderShown;
		Fader.Hided += OnFaderHided;
		Fonctionality.RootPressed += OnFonctionalityRootPressed;
		Fonctionality.ItemPressed += OnFonctionalityItemPressed;
		Fonctionality.Closed += OnFonctionalityClosed;
	}

	private void UpdateHUD()
	{
		UpdateProgress();
		if (m_FonctionalityMerger != null && m_Fonctionality.ResfreshMeshMerger())
		{
			m_FonctionalityMerger.MergeMeshes();
			m_Fonctionality.SetMergeMeshesFinished();
		}
		if (m_GoToPhotoRoom || m_GoToMainMenu)
		{
			if (m_SoundPlayer != null)
			{
				m_SoundPlayer.Stop();
			}
			//AudioPlayerBinding.stop(true);
			StopListener();
			StopMusicDance();
			if (GlobalVariables.SoundEnabled == 1)
			{
				base.GetComponent<AudioSource>().Stop();
				base.GetComponent<AudioSource>().clip = BackSound;
				base.GetComponent<AudioSource>().Play();
				base.GetComponent<AudioSource>().loop = false;
			}
			StopAnim();
			DataMining.OnApplicationQuit(true);
			if (m_GoToPhotoRoom)
			{
				SetNextLevel("PhotoRoom", true);
			}
			if (m_GoToMainMenu)
			{
				SetNextLevel("EmptySceneToMainMenu", true);
			}
		}
	}

	private void OnDisableHUD()
	{
		Fader.Shown -= OnFaderShown;
		Fader.Hided -= OnFaderHided;
		Fonctionality.RootPressed -= OnFonctionalityRootPressed;
		Fonctionality.ItemPressed -= OnFonctionalityItemPressed;
		Fonctionality.Closed -= OnFonctionalityClosed;
	}

	private void HUDGUI()
	{
		ShowProgress();
		if (Utility.InputStoppedByActivity())
		{
			GUIUtils.DrawFictiveBackButton();
		}
		else
		{
			if (!(m_IntroLoadingScreen == null) || !(m_OutroLoadingScreen == null) || !(m_IntroPanelObject == null) || !GUIUtils.DrawBackButton() || m_BackPressed >= 0)
			{
				return;
			}
			m_BackPressed = 5;
			if (m_ShowProgress)
			{
				ShowProgress(false);
			}
			else if (m_Fonctionality != null)
			{
				if (m_Fonctionality.IsRoot())
				{
					GoToMainMenu();
				}
				else
				{
					m_Fonctionality.OnHitRootItem();
				}
			}
		}
	}

	private void GoToPhotoRoom()
	{
		if (m_IntroPanelObject != null)
		{
			OnDestroyIntroPanel();
		}
		m_GoToPhotoRoom = true;
		Utility.ShowActivityView(true);
		ClearAudioClips();
		ClearMiniGameObjects();
		ClearAnimationClipsToBeRemoved();
		ClearAdditionalAnimationClips(true);
		m_Fonctionality.Active = false;
		m_OutroLoadingScreen = Utility.CreateLoadingScreen();
		if (m_OutroLoadingScreen == null)
		{
			Utility.Log(ELog.Errors, "m_OutroLoadingScreen == null");
		}
		HideRabbid();
	}

	private void GoToMainMenu()
	{
		if (m_IntroPanelObject != null)
		{
			OnDestroyIntroPanel();
		}
		m_GoToMainMenu = true;
		Utility.ShowActivityView(false);
		ClearAudioClips();
		ClearMiniGameObjects();
		ClearAnimationClipsToBeRemoved();
		ClearAdditionalAnimationClips(true);
		m_Fonctionality.Active = false;
		m_OutroLoadingScreen = Utility.CreateLoadingScreen();
		if (m_OutroLoadingScreen == null)
		{
			Utility.Log(ELog.Errors, "m_OutroLoadingScreen == null");
		}
		HideRabbid();
	}

	private void OnFaderShown()
	{
		m_Flash.FadeOut(m_FlashDuration);
	}

	private void OnFaderHided()
	{
		ProcessPhoto();
	}

	private void OnFonctionalityClosed(int ID, FonctionalityItem.EType type)
	{
		if (m_ShowProgress)
		{
			return;
		}
		switch (type)
		{
		case FonctionalityItem.EType.InGameMenuOpen:
		{
			FonctionalityItem.EType fonctionalityItemType = m_FonctionalityItemType;
			if (fonctionalityItemType == FonctionalityItem.EType.AR)
			{
				m_Fonctionality.Wait = true;
				GoToPhotoRoom();
			}
			break;
		}
		case FonctionalityItem.EType.CustomizeCostumeRoot:
			QuitCustomizeMode();
			break;
		case FonctionalityItem.EType.CustomizeEnvironmentRoot:
			QuitCustomizeMode();
			break;
		case FonctionalityItem.EType.PauseRoot:
			Pause(!IsPaused());
			if (m_Scroller != null)
			{
				ActivateScroller(EScroller.MiniGame);
			}
			break;
		}
		m_FonctionalityItemType = FonctionalityItem.EType.None;
	}

	private void OnFonctionalityRootPressed(int ID, FonctionalityItem.EType type)
	{
		if (!m_ShowProgress)
		{
			m_Fonctionality.RefreshMerger();
			switch (type)
			{
			case FonctionalityItem.EType.InGameMenuClose:
				m_Fonctionality.SetType(FonctionalityItem.EType.InGameMenuOpen, type);
				break;
			case FonctionalityItem.EType.InGameMenuOpen:
				m_Fonctionality.SetType(FonctionalityItem.EType.InGameMenuClose, type);
				break;
			}
		}
	}

	private void OnFonctionalityItemPressed(int ID, FonctionalityItem.EType item, FonctionalityItem.EType root)
	{
		if (m_ShowProgress)
		{
			return;
		}
		m_Fonctionality.RefreshMerger();
		switch (item)
		{
		case FonctionalityItem.EType.CustomizeCostume:
			SetState(EState.CustomCostume);
			DataMining.Increment(MainMenuScript.EMenu.Customization.ToString());
			break;
		case FonctionalityItem.EType.CustomizeEnvironment:
			SetState(EState.CustomEnvironment);
			DataMining.Increment(MainMenuScript.EMenu.Customization.ToString());
			break;
		case FonctionalityItem.EType.AR:
			m_FonctionalityItemType = FonctionalityItem.EType.AR;
			break;
		case FonctionalityItem.EType.MusicOn:
			m_MusicButton = 1;
			ShowMediaPicker();
			m_Fonctionality.SetType(FonctionalityItem.EType.MusicOff, item);
			break;
		case FonctionalityItem.EType.MusicOff:
			StopMusicDance();
			m_Fonctionality.SetType(FonctionalityItem.EType.MusicOn, item);
			break;
		case FonctionalityItem.EType.Progress:
			ShowProgress(true);
			break;
		case FonctionalityItem.EType.Back:
			GoToMainMenu();
			break;
		case FonctionalityItem.EType.Pause:
			Pause(!IsPaused());
			StopAnim();
			if (m_Scroller != null)
			{
				m_Scroller.SetVisible(false);
			}
			break;
		case FonctionalityItem.EType.LastPicture:
		case FonctionalityItem.EType.LastScreenshot:
			GamePlayerPrefs.SetInt("GotoPhoto", m_PhotoNumber - 1);
			DataMining.Increment(MainMenuScript.EMenu.OutWorld.ToString());
			SetNextLevel("OutWorld2D", false);
			break;
		case FonctionalityItem.EType.Valid:
			switch (root)
			{
			case FonctionalityItem.EType.CustomizeCostumeRoot:
				ConfirmCostume();
				m_Fonctionality.OnHitRootItem();
				break;
			case FonctionalityItem.EType.CustomizeEnvironmentRoot:
				ConfirmEnvironment();
				m_Fonctionality.OnHitRootItem();
				break;
			case FonctionalityItem.EType.PauseRoot:
				PlayExternalAudioClip(PhotoSound);
				ValidatePhoto();
				break;
			}
			break;
		case FonctionalityItem.EType.CustomizeCostumeRoot:
		case FonctionalityItem.EType.CustomizeEnvironmentRoot:
		case FonctionalityItem.EType.ARRoot:
		case FonctionalityItem.EType.OpenLibrary:
		case FonctionalityItem.EType.SwitchCamera:
		case FonctionalityItem.EType.PauseRoot:
		case FonctionalityItem.EType.Screenshot:
			break;
		}
	}

	private void StopMusicDance()
	{
		m_MusicButton = 0;
		if (m_Fonctionality != null)
		{
			m_Fonctionality.SetType(FonctionalityItem.EType.MusicOn, FonctionalityItem.EType.MusicOff);
		}
		if (m_UseMediaPlayer)
		{
			//MediaPlayerBinding.stop();
		}
	}

	private void SetNextLevel(string sceneName, bool showActivityView)
	{
		m_NextSceneName = sceneName;
		m_FrameCountBeforeNextScene = 10;
		StopAnim();
		if (showActivityView)
		{
			Utility.ShowActivityView(true);
		}
	}

	private void InitProgress()
	{
		m_ProgressBGRect = Utility.NewRectP(0f, 0f, 100f, 100f);
		m_ProgressViewRect = Utility.NewRectP(0f, 0f, 75f, 71f);
		m_ProgressScreenRect = Utility.NewRectP((100f - m_ProgressScreenWidth) * 0.5f, 20f, m_ProgressScreenWidth, 70f);
		m_ProgressTitleRect = Utility.NewRectP(10f, 5f, 80f, 10f);
		m_ProgressStyle = new GUIStyle();
		m_ProgressTitleStyle = m_CommonSkin.GetStyle("HugeTextButtonLarge");
		if (m_ProgressTitleStyle == null)
		{
			Utility.Log(ELog.Errors, "Unable to find style 'HugeTextButtonLarge' into common skin");
		}
		m_ProgressScreenRatio = (float)Screen.width / (float)Screen.height;
	}

	private void LoadProgressPicts()
	{
		bool flag = true;
		Color white = Color.white;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		EPack ePack = EPack.Count;
		if (m_AchievementsTable == null)
		{
			Utility.Log(ELog.Errors, "RecreateMiniGameThumbnailsBuffer failed: m_AchievementsTable == null");
			return;
		}
		m_ProgressPictRatio = 1f;
		string text;
		Texture2D texture2D;
		string resPath;
		for (EState eState = EState.ClownBox; eState < EState.Count; eState++)
		{
			flag3 = false;
			flag2 = false;
			white = Color.white;
			ePack = GetPackFromState(eState);
			EProductID miniGamePack = m_AchievementsTable.GetMiniGamePack(eState);
			flag4 = Utility.IsUnlockedProduct(miniGamePack);
			text = "MiniGames/Thumbnails";
			if (!m_UnlockedStates.Contains(eState) || !flag4)
			{
				flag2 = false;
				text += "Locked";
				white = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag3 = AchievementTable.IsGoodyNew(AchievementTable.EGoody.MiniGame, eState.ToString());
			}
			resPath = text + "/" + eState;
			texture2D = Utility.LoadTextureResource<Texture2D>(resPath);
			if (texture2D != null)
			{
				if (flag3)
				{
					texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
				}
				if (flag2)
				{
					texture2D = Utility.AddColor(texture2D, white);
				}
				m_ProgressPicts.Add(eState, texture2D);
				if (flag)
				{
					m_ProgressPictRatio = (float)texture2D.width / (float)texture2D.height;
					flag = false;
				}
			}
		}
		text = "ARPics/ARPoses/Thumbnails/";
		resPath = text + PhotoRoom.EARPose.pose08;
		texture2D = Utility.LoadTextureResource<Texture2D>(resPath);
		if (texture2D != null)
		{
			m_ProgressPicts.Add(EState.Idle, texture2D);
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to load: " + resPath);
		}
		for (ePack = EPack.Interactions; ePack < EPack.Count; ePack++)
		{
			EProductID miniGamePack = AchievementTable.GetProductIDFromPack(ePack);
			if (!Utility.IsUnlockedProduct(miniGamePack))
			{
				m_ProgressPackPicts.Add(ePack, AchievementTable.LoadPackTexture(ePack, 5));
			}
		}
		resPath = "GUI/Pastilles/";
		m_ProgressLockedPastille = Utility.LoadTextureResource<Texture2D>(resPath + "Locked");
		if (m_ProgressLockedPastille == null)
		{
			Utility.Log(ELog.Errors, "Unable to load: " + resPath + "Locked");
		}
		m_ProgressUnlockedPastille = Utility.LoadTextureResource<Texture2D>(resPath + "Unlocked");
		if (m_ProgressUnlockedPastille == null)
		{
			Utility.Log(ELog.Errors, "Unable to load: " + resPath + "Unlocked");
		}
	}

	private void ShowProgress(bool b)
	{
		m_ShowProgress = b;
		Pause(b);
		if (m_Scroller != null)
		{
			m_Scroller.SetVisible(!b);
		}
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Lock = b;
		}
		if (m_ShowProgress)
		{
			StopAnim();
			LoadProgressPicts();
			m_ProgressActionCount = GamePlayerPrefs.GetInt("_number_of_moves");
			m_ProgressEndedActionCount = GamePlayerPrefs.GetInt("_number_of_ended_moves");
			m_ProgressScrollPos = Vector2.zero;
		}
		else
		{
			m_ProgressPicts.Clear();
			m_ProgressPackPicts.Clear();
			m_ProgressLockedPastille = null;
			m_ProgressUnlockedPastille = null;
			Utility.FreeMem();
		}
	}

	private void UpdateProgress()
	{
		if (!m_ShowProgress)
		{
			return;
		}
		if (AllInput.GetTouchCount() == 1)
		{
			if (AllInput.GetState(0) == AllInput.EState.Moved)
			{
				m_ProgressScrollPos.y -= AllInput.GetGUIDelta(0).y;
				m_ProgressScrollIntertia = AllInput.GetGUIDelta(0).y;
			}
		}
		else if (AllInput.GetTouchCount() == 0)
		{
			m_ProgressScrollPos.y -= m_ProgressScrollIntertia;
			m_ProgressScrollIntertia *= 0.87f;
		}
	}

	private void ShowProgress()
	{
		if (m_ShowProgress)
		{
			GUISkin skin = GUI.skin;
			GUI.skin = m_CommonSkin;
			Color textColor = m_ProgressTitleStyle.normal.textColor;
			if (m_ProgressPicts.Count == 0)
			{
				LoadProgressPicts();
			}
			GUI.Label(m_ProgressBGRect, string.Empty, "BlackBackground");
			m_ProgressTitleStyle.normal.textColor = Color.yellow;
			GUI.Label(m_ProgressTitleRect, Localization.GetLocalizedText(ELoc.StartGame).ToUpper(), m_ProgressTitleStyle);
			m_ProgressTitleStyle.normal.textColor = textColor;
			GUI.skin = m_ScrollerSkin;
			m_ProgressScrollPos = GUI.BeginScrollView(m_ProgressScreenRect, m_ProgressScrollPos, m_ProgressViewRect, false, false);
			if (m_ProgressStyle != null)
			{
				DrawProgress(5, 5f, 5f, 2f, 2f, m_ProgressPictRatio, m_ProgressScreenWidth - 2.5f);
				m_ProgressStyle.normal.background = null;
			}
			GUI.EndScrollView();
			GUI.skin = skin;
		}
	}

	private void DrawProgress(int maxEltPerLine, float hMargin, float vMargin, float hSpace, float vSpace, float pictRatio, float refWidth)
	{
		EPack ePack = EPack.Count;
		EPack ePack2 = EPack.Count;
		float num = hMargin;
		float num2 = vMargin;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = refWidth;
		num5 -= hMargin * 2f;
		num5 -= hSpace * (float)(maxEltPerLine - 1);
		num5 /= (float)maxEltPerLine;
		float num6 = num5 / pictRatio * m_ProgressScreenRatio;
		num += num5 + hSpace;
		m_ProgressStyle.normal.background = (Texture2D)m_ProgressPicts[EState.Idle];
		GUI.Label(Utility.NewRectP(num, num2, num5, num6), string.Empty, m_ProgressStyle);
		num += num5 + hSpace;
		string text = m_ProgressEndedActionCount + " / " + m_ProgressActionCount;
		TextAnchor alignment = m_ProgressTitleStyle.alignment;
		m_ProgressTitleStyle.alignment = TextAnchor.MiddleLeft;
		GUI.Label(Utility.NewRectP(num, num2, num5 * 2f + hSpace, num6), text, m_ProgressTitleStyle);
		m_ProgressTitleStyle.alignment = alignment;
		for (EState eState = EState.ClownBox; eState < EState.IngnoredMiniGames; eState++)
		{
			ePack2 = GetPackFromState(eState);
			if (ePack != ePack2)
			{
				EProductID productIDFromPack = AchievementTable.GetProductIDFromPack(ePack);
				if (productIDFromPack != EProductID.Free && !Utility.IsUnlockedProduct(productIDFromPack))
				{
					Texture2D texture2D = (Texture2D)m_ProgressPackPicts[ePack];
					if (texture2D != null)
					{
						m_ProgressStyle.normal.background = texture2D;
						Rect position = Utility.NewRectP(num3 - s_PackBorderX, num2 - s_PackBorderY, num4 + s_PackBorderX * 2f, num6 + s_PackBorderY * 2f);
						if (GUI.Button(position, string.Empty, m_ProgressStyle))
						{
							ShowProgress(false);
							Request(productIDFromPack);
						}
					}
					DrawTitle(num3 - s_PackBorderX, num2 - s_PackBorderY, num4 + s_PackBorderX * 2f, num6 + s_PackBorderY * 2f, ePack);
				}
				ePack = ePack2;
				num = hMargin;
				num2 += num6 + vSpace;
				num3 = num;
				num4 = 0f;
			}
			if (eState < EState.Count)
			{
				m_ProgressStyle.normal.background = (Texture2D)m_ProgressPicts[eState];
				GUI.Label(Utility.NewRectP(num, num2, num5, num6), string.Empty, m_ProgressStyle);
				DrawPastilles(num, num2, num5, num6, eState);
			}
			num += num5 + hSpace;
			num4 += num5 + hSpace;
		}
		m_ProgressViewRect = Utility.NewRectP(0f, 0f, 75f, num2);
	}

	private void DrawPastilles(float left, float top, float hSize, float vSize, EState state)
	{
		int actionCount = m_AchievementsTable.GetActionCount(state);
		int num = PlayerPrefs.GetInt(state.ToString() + "EndedActionCount");
		float num2 = hSize / 100f;
		top += num2 * 12f;
		float width = num2 * 20f;
		float num3 = num2 * 20f * m_ProgressScreenRatio;
		for (int i = 0; i < actionCount; i++)
		{
			float top2 = top + (num3 + num2) * (float)i;
			float num4 = left;
			switch (i)
			{
			case 0:
				num4 -= num2 * 2f;
				break;
			case 1:
				num4 += num2 * 1f;
				break;
			case 2:
				num4 += num2 * 5f;
				break;
			case 3:
				num4 += num2 * 10f;
				break;
			}
			m_ProgressStyle.normal.background = ((i >= num) ? m_ProgressLockedPastille : m_ProgressUnlockedPastille);
			GUI.Label(Utility.NewRectP(num4, top2, width, num3), string.Empty, m_ProgressStyle);
		}
	}

	private void DrawTitle(float left, float top, float hSize, float vSize, EPack pack)
	{
		string localizedText = Localization.GetLocalizedText(AchievementTable.GetTitleFromPack(pack));
		TextAnchor alignment = m_TextContent.alignment;
		float fixedWidth = m_TextContent.fixedWidth;
		float num = 45f;
		float num2 = 72f;
		float num3 = 50f;
		float num4 = 20f;
		float left2 = hSize * num * 0.01f + left;
		float top2 = vSize * num2 * 0.01f + top;
		float width = hSize * num3 * 0.01f;
		float height = vSize * num4 * 0.01f;
		m_TextContent.alignment = TextAnchor.MiddleCenter;
		m_TextContent.fixedWidth = 0f;
		GUI.Label(Utility.NewRectP(left2, top2, width, height), localizedText, m_TextContent);
		m_TextContent.alignment = alignment;
		m_TextContent.fixedWidth = fixedWidth;
	}

	private void StartHelper()
	{
		FindCamera();
		m_Plane = Utility.LoadGameObject("GUI/PlaneDLC", m_Camera_2D.transform);
		Utility.SetLayerRecursivly(m_Plane.transform, 11);
		m_Plane.active = false;
		m_PlaneRotation = Quaternion.Euler(90f, 180f, 0f);
		m_PlaneRotationDephase = Quaternion.Euler(270f, 0f, 0f);
		if (m_CategoryRefreshRate != 0 && m_CategoryRefreshCounter != 0)
		{
		}
	}

	private void UpdateHelper()
	{
		UpdatePlaneOrientation();
	}

	private void HelperGUI()
	{
	}

	public static void ActivateShadow(bool b)
	{
		s_Instance.m_RabbidShadow.SetActiveRecursively(b);
		Utility.Log(ELog.Gameplay, "ActivateShadow(" + b + ")");
	}

	public static bool ReverseScreen()
	{
		if (s_GravityBlocker < 20)
		{
			return s_ReverseBeforePausing;
		}
		switch (AllInput.GetDeviceOrientation())
		{
		case DeviceOrientation.PortraitUpsideDown:
			s_TmpLastValidOrientation = 2;
			break;
		case DeviceOrientation.Portrait:
			s_TmpLastValidOrientation = 1;
			break;
		}
		if (s_TmpLastValidOrientation != s_LastValidOrientation)
		{
			if (++s_ReverseChangeFrameCount > 10)
			{
				s_LastValidOrientation = s_TmpLastValidOrientation;
				s_ReverseChangeFrameCount = 0;
			}
		}
		else
		{
			s_ReverseChangeFrameCount = 0;
		}
		return s_StartOrientation != s_LastValidOrientation;
	}

	public static bool DephaseRabbid()
	{
		return s_StartOrientation == 2;
	}

	private void UpdateRecording()
	{
		if (AllInput.IsMicroAvailable() && m_RecordAudio && !IsListening() && ++m_RecordRetest == 15)
		{
			Utility.Log(ELog.GameplayDebug, "Trying To Listen");
			Listen();
			m_RecordRetest = 0;
		}
	}

	private void EndRecording()
	{
		if (AllInput.IsMicroAvailable())
		{
			StopListener();
			if (++m_CategoryRefreshCounter >= m_CategoryRefreshRate)
			{
				m_CategoryRefreshCounter = 0;
			}
			GlobalVariables.s_AveragePower = -166f;
		}
	}

	private string RaycastUnderFinger(int fingerID)
	{
		if (m_Scroller != null && m_Scroller.IsInteract(fingerID))
		{
			return "None";
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 1000f, 256))
		{
			return hitInfo.collider.name;
		}
		return null;
	}

	private string RaycastHUDUnderFinger(int fingerID)
	{
		if (m_Scroller != null && m_Scroller.IsInteract(fingerID))
		{
			return "None";
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_2D.ScreenPointToRay(raycastPosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 1000f, 2048))
		{
			return hitInfo.collider.name;
		}
		return null;
	}

	private List<ECollider> RaycastAllUnderFinger(int fingerID)
	{
		List<ECollider> list = new List<ECollider>();
		if (m_Scroller != null && m_Scroller.IsInteract(fingerID))
		{
			return list;
		}
		Vector2 raycastPosition = AllInput.GetRaycastPosition(fingerID);
		Ray ray = m_Camera_3D.ScreenPointToRay(raycastPosition);
		RaycastHit[] array = Physics.RaycastAll(ray, 1000f, 256);
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(GetCollider(array[i].collider.gameObject.name));
			}
		}
		return list;
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

	private ECollider GetCollider(string cast)
	{
		if (cast == null)
		{
			return ECollider.Count;
		}
		if (cast.Contains("Pied"))
		{
			if (cast.Contains("G"))
			{
				return ECollider.LeftFoot;
			}
			if (cast.Contains("D"))
			{
				return ECollider.RightFoot;
			}
			return ECollider.Count;
		}
		if (cast.Equals("B_Bassin"))
		{
			return ECollider.String;
		}
		if (cast.Equals("B_Bassin01"))
		{
			return ECollider.Tummy;
		}
		if (cast.Contains("_eye"))
		{
			if (cast.Contains("_L"))
			{
				return ECollider.LeftEye;
			}
			if (cast.Contains("_R"))
			{
				return ECollider.RightEye;
			}
			return ECollider.Count;
		}
		if (cast.Contains("Oreille"))
		{
			if (cast.Contains("_G"))
			{
				return ECollider.LeftEar;
			}
			if (cast.Contains("_D"))
			{
				return ECollider.RightEar;
			}
			return ECollider.Count;
		}
		if (cast.Contains("Panel"))
		{
			return ECollider.Panel;
		}
		if (cast.Contains("duck"))
		{
			return ECollider.YellowDuck;
		}
		if (cast.Contains("ClownBox"))
		{
			return ECollider.ClownBox;
		}
		if (cast.Contains("Piranha"))
		{
			return ECollider.Piranha;
		}
		if (cast.Contains("Swatter"))
		{
			return ECollider.Swatter;
		}
		if (cast.Contains("Micro"))
		{
			return ECollider.Micro;
		}
		if (cast.Contains("PopGun"))
		{
			return ECollider.PopGun;
		}
		if (cast.Contains("Jetpack"))
		{
			return ECollider.Jetpack;
		}
		if (cast.Contains("Guitar"))
		{
			return ECollider.Guitar;
		}
		if (cast.Contains("Shield"))
		{
			return ECollider.Shield;
		}
		if (cast.Contains("Toilet"))
		{
			return ECollider.EgyptianToiletPaper;
		}
		if (cast.Contains("ghettoblaster"))
		{
			return ECollider.GhettoBlaster;
		}
		return ECollider.Count;
	}

	public void EnableTutorial(ETutorial tuto, GameObject panel)
	{
		m_PanelObject = panel;
		if (m_PanelObject != null && m_PanelObject.transform.childCount > 0)
		{
			Transform child = m_PanelObject.transform.GetChild(0);
			if (child != null && child.childCount > 0)
			{
				child = child.GetChild(0);
				m_PanelSign = child.gameObject;
			}
		}
		m_PanelObject.SetActiveRecursively(true);
		m_PanelSign.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Tutorials/" + tuto);
		RewindAnim(m_PanelObject);
	}

	public void EnablePanel(EPanel panel, string anim, GameObject panelObj)
	{
		if (!(panelObj != null))
		{
			return;
		}
		m_PanelObject = panelObj;
		if (m_PanelObject != null && m_PanelObject.transform.childCount > 0)
		{
			Transform child = m_PanelObject.transform.GetChild(0);
			if (child != null && child.childCount > 0)
			{
				child = child.GetChild(0);
				m_PanelSign = child.gameObject;
			}
		}
		m_PanelObject.SetActiveRecursively(true);
		m_PanelObject.GetComponent<Animation>().Rewind();
		m_PanelSign.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Panel/" + panel);
	}

	public void DisablePanel()
	{
		if (m_PanelObject != null)
		{
			m_PanelObject = null;
		}
		if (m_PanelSign != null)
		{
			m_PanelSign = null;
		}
	}

	public bool IsPaused()
	{
		return m_PauseCounter > 0;
	}

	private void PauseAnimatedObject(GameObject obj, bool pauseResume)
	{
		if (!(obj == null))
		{
			if (pauseResume)
			{
				StopAnim(obj);
				return;
			}
			StopAnim(obj);
			RewindAnim(obj);
			SetAnimTime(m_PausedAnim, m_PausedAnimTime, obj);
			PlayAnim(m_PausedAnim, obj);
		}
	}

	private void SavePauseData()
	{
		m_PausedState = m_State;
		m_PausedAnim = m_CurrentAnimation;
		m_PausedRecord = IsListening();
		m_PauseIsPlaying = base.animIsPlaying;
		if (base.GetComponent<Animation>()[m_PausedAnim] != null)
		{
			m_PausedAnimTime = base.GetComponent<Animation>()[m_PausedAnim].time;
		}
		else
		{
			m_PausedAnimTime = m_CurrentAnimTime;
		}
		m_PausedPosition = base.transform.localPosition;
		m_PausedScale = base.transform.localScale;
		m_PausedRotation = base.transform.localRotation;
		StopListener();
	}

	private void RestorePauseData()
	{
		if (m_SkipRestoreAfterPause)
		{
			m_SkipRestoreAfterPause = false;
			return;
		}
		m_State = m_PausedState;
		SetTRS(m_PausedPosition, m_PausedRotation, m_PausedScale);
		StopAnim();
		RewindAnim();
		SetAnimTime(m_PausedAnim, m_PausedAnimTime);
		if (m_PauseIsPlaying)
		{
			PlayAnim(m_PausedAnim);
			m_CurrentAnimTime = m_PausedAnimTime;
		}
		if (m_PausedRecord)
		{
			Listen();
		}
	}

	public void Pause(bool pauseResume)
	{
		bool flag = false;
		if (pauseResume)
		{
			if (m_PauseCounter++ == 0)
			{
				SavePauseData();
				flag = true;
			}
		}
		else if (m_PauseCounter > 0 && --m_PauseCounter == 0)
		{
			RestorePauseData();
			flag = true;
		}
		if (!flag)
		{
			return;
		}
		PauseSound(pauseResume);
		if (m_UseMediaPlayer)
		{
			/*if (pauseResume)
			{
				m_PausedMediaPlayer = MediaPlayerBinding.isPlaying();
				if (m_PausedMediaPlayer)
				{
					MediaPlayerBinding.pause();
				}
			}
			else if (m_PausedMediaPlayer)
			{
				MediaPlayerBinding.play();
			}*/
		}
		switch (m_State)
		{
		case EState.Idle:
			OnPauseIdle(pauseResume);
			break;
		case EState.Tickle:
			OnPauseTickle(pauseResume);
			break;
		case EState.Turn:
			OnPauseTurn(pauseResume);
			break;
		case EState.Run:
			OnPauseRun(pauseResume);
			break;
		case EState.String:
			OnPauseString(pauseResume);
			break;
		case EState.Ear:
			OnPauseEar(pauseResume);
			break;
		case EState.Burp:
			OnPauseBurp(pauseResume);
			break;
		case EState.Blow:
			OnPauseBlow(pauseResume);
			break;
		case EState.Yell:
			OnPauseYell(pauseResume);
			break;
		case EState.WallBounce:
			OnPauseWallBounce(pauseResume);
			break;
		case EState.Poke:
			OnPausePoke(pauseResume);
			break;
		case EState.Dance:
			OnPauseDance(pauseResume);
			break;
		case EState.Rotation:
			OnPauseRotation(pauseResume);
			break;
		case EState.FaceDown:
			OnPauseFaceDown(pauseResume);
			break;
		case EState.CustomCostume:
			OnPauseCustomCostume(pauseResume);
			break;
		case EState.CustomEnvironment:
			OnPauseCustomEnvironment(pauseResume);
			break;
		case EState.PresentTuto:
			OnPausePresentTuto(pauseResume);
			break;
		case EState.ClownBox:
			OnPauseClownBox(pauseResume);
			break;
		case EState.YellowDuck:
			OnPauseYellowDuck(pauseResume);
			break;
		case EState.Night:
			OnPauseNight(pauseResume);
			break;
		case EState.Steam:
			OnPauseSteam(pauseResume);
			break;
		case EState.Piranha:
			OnPausePiranha(pauseResume);
			break;
		case EState.Swatter:
			OnPauseSwatter(pauseResume);
			break;
		case EState.WCBrush:
			OnPauseWCBrush(pauseResume);
			break;
		case EState.Micro:
			OnPauseMicro(pauseResume);
			break;
		case EState.PhoneReal:
			OnPausePhoneReal(pauseResume);
			break;
		case EState.PopGun:
			OnPausePopGun(pauseResume);
			break;
		case EState.Guitar:
			OnPauseGuitar(pauseResume);
			break;
		case EState.Jetpack:
			OnPauseJetpack(pauseResume);
			break;
		case EState.Shield:
			OnPauseShield(pauseResume);
			break;
		case EState.EgyptianToiletPaper:
			OnPauseEgyptianToiletPaper(pauseResume);
			break;
		}
	}

	private void FindCamera()
	{
		GameObject gameObject = GameObject.Find("Main Camera");
		if (gameObject == null)
		{
			gameObject = GameObject.Find("Camera_3D");
		}
		if (gameObject != null)
		{
			m_SoundPlayer = gameObject.GetComponent<InGameSoundPlayer>();
			m_CameraScript = gameObject.GetComponent<MainCameraScript>();
			m_Camera_3D = gameObject.GetComponent<Camera>();
		}
		if (m_SoundPlayer == null)
		{
			Utility.Log(ELog.Errors, "Unable to find sound player on camera");
		}
		if (m_CameraScript == null)
		{
			Utility.Log(ELog.Errors, "Unable to find main camera script component on main camera");
		}
		if (m_Camera_3D == null)
		{
			Utility.Log(ELog.Errors, "Unable to find a camera component on main camera");
		}
		gameObject = GameObject.Find("Camera_2D");
		if (gameObject != null)
		{
			m_Camera_2D = gameObject.GetComponent<Camera>();
		}
		if (m_Camera_2D == null)
		{
			Utility.Log(ELog.Errors, "Unable to find a camera 2D component on Camera2D");
		}
	}

	private void FindCostumeEars()
	{
		if (m_Costume == null || earLeftPointTransform == null || earRightPointTransform == null)
		{
			return;
		}
		string text = string.Empty;
		Transform parent = earLeftPointTransform.parent;
		while (parent != null)
		{
			if (parent.parent != null)
			{
				text = parent.name + "/" + text;
			}
			parent = parent.parent;
		}
		string text2 = text + earLeftPointTransform.name;
		parent = m_Costume.transform.Find(text2);
		if (parent != null)
		{
			m_CostumeEarLeftPointTransform = parent;
		}
		text = string.Empty;
		parent = earLeftPointTransform.parent;
		while (parent != null)
		{
			if (parent.parent != null)
			{
				text = parent.name + "/" + text;
			}
			parent = parent.parent;
		}
		text2 = text + earRightPointTransform.name;
		parent = m_Costume.transform.Find(text2);
		if (parent != null)
		{
			m_CostumeEarRightPointTransform = parent;
		}
	}

	private void SetPlaneTexture(string texName)
	{
		ClearPlaneTexture();
		texName = Utility.GetTexPath() + texName;
		Texture2D texture2D = Utility.LoadResource<Texture2D>(texName);
		if (texture2D != null)
		{
			m_Plane.GetComponent<Renderer>().material.mainTexture = texture2D;
			m_Plane.active = true;
			m_IsPlaneDephase = DephaseRabbid();
		}
	}

	private void ClearPlaneTexture()
	{
		if (m_Plane != null)
		{
			m_Plane.GetComponent<Renderer>().material.mainTexture = null;
			m_Plane.active = false;
		}
		Utility.FreeMem();
	}

	private void UpdatePlaneOrientation()
	{
		if (ReverseScreen())
		{
			if (!m_IsPlaneDephase)
			{
				m_Plane.transform.localEulerAngles = m_PlaneRotationDephase.eulerAngles;
				m_IsPlaneDephase = true;
			}
		}
		else if (m_IsPlaneDephase)
		{
			m_Plane.transform.localEulerAngles = m_PlaneRotation.eulerAngles;
			m_IsPlaneDephase = false;
		}
	}

	public static EState GetState()
	{
		if (s_Instance != null)
		{
			return s_Instance.m_State;
		}
		return EState.Idle;
	}

	public static void SetState(EState state)
	{
		if (s_Instance != null)
		{
			s_Instance.SetInternalState(state);
		}
	}

	private void StartInteractions()
	{
		Rigidbody[] componentsInChildren = base.gameObject.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].useGravity || !componentsInChildren[i].isKinematic)
			{
				componentsInChildren[i].useGravity = false;
				componentsInChildren[i].isKinematic = true;
				Utility.Log(ELog.Errors, componentsInChildren[i].name + " has incorrect props");
			}
		}
		m_NightScript = GetComponentInChildren<NightScript>();
		if (m_NightScript == null)
		{
			Utility.Log(ELog.Errors, "m_NightScript not found");
		}
		Utility.SetLayerRecursivly(base.transform, 8);
		m_StringInteractionPlane = Utility.SetLayer("StringPlane", 10);
		if (m_StringInteractionPlane != null)
		{
			m_StringInteractionPlane.active = false;
		}
		m_EarsInteractionPlane = Utility.SetLayer("EarsPlane", 10);
		if (m_EarsInteractionPlane != null)
		{
			m_EarsInteractionPlane.active = false;
		}
		m_String = GameObject.Find("StringObject");
		if (m_String == null)
		{
			Utility.Log(ELog.Errors, "String not found");
		}
		else
		{
			Utility.SetLayerRecursivly(m_String.transform, 8);
			m_String.SetActiveRecursively(false);
		}
		Transform transform = base.transform.Find("Shadows");
		if (transform != null)
		{
			m_RabbidShadow = transform.gameObject;
			if (m_RabbidShadow == null)
			{
				Utility.Log(ELog.Errors, "Shadows not found");
				return;
			}
			Renderer[] componentsInChildren2 = m_RabbidShadow.GetComponentsInChildren<Renderer>();
			if (componentsInChildren2 != null)
			{
				Texture2D mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "Misc/FakeShadow");
				Renderer[] array = componentsInChildren2;
				foreach (Renderer renderer in array)
				{
					renderer.material.mainTexture = mainTexture;
				}
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Shadows root not found");
		}
	}

	private void UpdateInputs()
	{
		if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 1)
		{
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_TouchTime = 0f;
				m_TouchMoved = false;
				m_TickleCounter = 0;
				m_CheckInput = true;
				m_BurnInput = false;
				m_StartTouchState = m_State;
				if (m_Scroller != null)
				{
					m_CheckInput = !m_Scroller.IsInteract(0);
				}
			}
			else if (m_StartTouchState == m_State)
			{
				if (AllInput.GetState(0) == AllInput.EState.Moved && !m_TouchMoved)
				{
					m_TouchMoved = true;
				}
				if (AllInput.GetState(0) != AllInput.EState.Leave && AllInput.GetState(0) != AllInput.EState.Began)
				{
					m_TouchTime += Time.deltaTime;
				}
			}
			else
			{
				m_BurnInput = true;
				m_CheckInput = false;
			}
		}
		else
		{
			m_BurnInput = true;
			m_CheckInput = false;
		}
	}

	private void UpdateInteractions()
	{
		if (IsPaused())
		{
			return;
		}
		m_StateTime += Time.deltaTime;
		m_TimeSinceLastSuccess += Time.deltaTime;
		m_TimeSinceLastState += Time.deltaTime;
		m_StateFrameCount += 1f;
		if (m_Camera_3D == null)
		{
			return;
		}
		EState state = m_State;
		switch (m_State)
		{
		case EState.Idle:
			OnUpdateIdle();
			break;
		case EState.Tickle:
			OnUpdateTickle();
			break;
		case EState.Turn:
			OnUpdateTurn();
			break;
		case EState.Run:
			OnUpdateRun();
			break;
		case EState.String:
			OnUpdateString();
			break;
		case EState.Ear:
			OnUpdateEar();
			break;
		case EState.Burp:
			OnUpdateBurp();
			break;
		case EState.Blow:
			OnUpdateBlow();
			break;
		case EState.Yell:
			OnUpdateYell();
			break;
		case EState.WallBounce:
			OnUpdateWallBounce();
			break;
		case EState.Poke:
			OnUpdatePoke();
			break;
		case EState.Dance:
			OnUpdateDance();
			break;
		case EState.Rotation:
			OnUpdateRotation();
			break;
		case EState.FaceDown:
			OnUpdateFaceDown();
			break;
		case EState.CustomCostume:
			OnUpdateCustomCostume();
			break;
		case EState.CustomEnvironment:
			OnUpdateCustomEnvironment();
			break;
		case EState.PresentTuto:
			OnUpdatePresentTuto();
			break;
		case EState.ClownBox:
			OnUpdateClownBox();
			break;
		case EState.YellowDuck:
			OnUpdateYellowDuck();
			break;
		case EState.Night:
			OnUpdateNight();
			break;
		case EState.Steam:
			OnUpdateSteam();
			break;
		case EState.Piranha:
			OnUpdatePiranha();
			break;
		case EState.Swatter:
			OnUpdateSwatter();
			break;
		case EState.WCBrush:
			OnUpdateWCBrush();
			break;
		case EState.Micro:
			OnUpdateMicro();
			break;
		case EState.PhoneReal:
			OnUpdatePhoneReal();
			break;
		case EState.PopGun:
			OnUpdatePopGun();
			break;
		case EState.Jetpack:
			OnUpdateJetpack();
			break;
		case EState.Shield:
			OnUpdateShield();
			break;
		case EState.Guitar:
			OnUpdateGuitar();
			break;
		case EState.EgyptianToiletPaper:
			OnUpdateEgyptianToiletPaper();
			break;
		case EState.Bow:
			OnUpdateBow();
			break;
		case EState.GhettoBlaster:
			OnUpdateGhettoBlaster();
			break;
		case EState.Nunchaku:
			OnUpdateNunchaku();
			break;
		case EState.RugbyBall:
			OnUpdateRugbyBall();
			break;
		case EState.SausagePolice:
			OnUpdateSausagePolice();
			break;
		}
		if (state != m_State)
		{
			return;
		}
		switch (m_State)
		{
		case EState.Dance:
		case EState.FaceDown:
		case EState.CustomCostume:
		case EState.CustomEnvironment:
		case EState.Micro:
			return;
		}
		if (!DetectGravityChanges())
		{
		}
	}

	private void LateUpdateInteractions()
	{
		if (m_State == EState.Ear)
		{
			Transform transform = ((m_EarSide != ESide.Left) ? earRightPointTransform : earLeftPointTransform);
			Transform transform2 = ((m_EarSide != ESide.Left) ? m_CostumeEarRightPointTransform : m_CostumeEarLeftPointTransform);
			if (m_PullState == EPullState.Release)
			{
				Vector3 vector = transform.position - m_EarWantedPosition;
				vector *= 0.38f;
				m_EarWantedPosition += vector;
				if (transform != null)
				{
					transform.position = m_EarWantedPosition;
				}
				if (transform2 != null)
				{
					transform2.position = m_EarWantedPosition;
				}
			}
			else
			{
				Vector3 vector = m_EarWantedPosition - m_EarCurrentPosition;
				vector *= 0.28f;
				m_EarCurrentPosition += vector;
				transform.position = m_EarCurrentPosition;
				if (transform != null)
				{
					transform.position = m_EarCurrentPosition;
				}
				if (transform2 != null)
				{
					transform2.position = m_EarCurrentPosition;
				}
			}
		}
		if (!m_ReactivateShadow)
		{
			return;
		}
		m_ReactivateShadow = false;
		ActivateShadow(true);
		if (s_Instance != null && s_Instance.m_RabbidShadow != null)
		{
			Transform transform3 = s_Instance.m_RabbidShadow.transform;
			for (int i = 0; i < transform3.childCount; i++)
			{
				FakeShadow component = transform3.GetChild(i).GetComponent<FakeShadow>();
				component.SetTR();
			}
		}
	}

	private void OnEnterGameTransition()
	{
		base.transform.parent = null;
		if (m_Costume != null)
		{
			m_Costume.transform.parent = null;
		}
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Active = false;
		}
	}

	private void OnLeaveGameTransition()
	{
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Active = true;
			m_Fonctionality.Wait = false;
		}
	}

	private EPack FindPackWithRandomStates()
	{
		if (m_RandomStates.Count > 0)
		{
			return GetPackFromState(m_RandomStates[0]);
		}
		return EPack.Count;
	}

	private void FillRandomStatesWithPack(EPack pack)
	{
		m_RandomStatesInPack.Clear();
		foreach (EState randomState in m_RandomStates)
		{
			if (GetPackFromState(randomState) == m_CurrentPack)
			{
				m_RandomStatesInPack.Add(randomState);
			}
		}
	}

	private void InternalOnLeave(EState state)
	{
		switch (m_State)
		{
		case EState.Idle:
			OnLeaveIdle();
			break;
		case EState.Tickle:
			OnLeaveTickle();
			break;
		case EState.Turn:
			OnLeaveTurn();
			break;
		case EState.Run:
			OnLeaveRun();
			break;
		case EState.String:
			OnLeaveString();
			break;
		case EState.Ear:
			OnLeaveEar();
			break;
		case EState.Burp:
			OnLeaveBurp();
			break;
		case EState.Blow:
			OnLeaveBlow();
			break;
		case EState.Yell:
			OnLeaveYell();
			break;
		case EState.WallBounce:
			OnLeaveWallBounce();
			break;
		case EState.Poke:
			OnLeavePoke();
			break;
		case EState.Dance:
			OnLeaveDance();
			break;
		case EState.Rotation:
			OnLeaveRotation();
			break;
		case EState.FaceDown:
			OnLeaveFaceDown();
			break;
		case EState.CustomCostume:
			OnLeaveCustomCostume();
			break;
		case EState.CustomEnvironment:
			OnLeaveCustomEnvironment();
			break;
		case EState.PresentTuto:
			OnLeavePresentTuto();
			break;
		case EState.ClownBox:
			OnLeaveClownBox();
			break;
		case EState.YellowDuck:
			OnLeaveYellowDuck();
			break;
		case EState.Night:
			OnLeaveNight();
			break;
		case EState.Steam:
			OnLeaveSteam();
			break;
		case EState.Piranha:
			OnLeavePiranha();
			break;
		case EState.Swatter:
			OnLeaveSwatter();
			break;
		case EState.WCBrush:
			OnLeaveWCBrush();
			break;
		case EState.Micro:
			OnLeaveMicro();
			break;
		case EState.PhoneReal:
			OnLeavePhoneReal();
			break;
		case EState.PopGun:
			OnLeavePopGun();
			break;
		case EState.Jetpack:
			OnLeaveJetpack();
			break;
		case EState.Shield:
			OnLeaveShield();
			break;
		case EState.Guitar:
			OnLeaveGuitar();
			break;
		case EState.EgyptianToiletPaper:
			OnLeaveEgyptianToiletPaper();
			break;
		case EState.Bow:
			OnLeaveBow();
			break;
		case EState.GhettoBlaster:
			OnLeaveGhettoBlaster();
			break;
		case EState.Nunchaku:
			OnLeaveNunchaku();
			break;
		case EState.RugbyBall:
			OnLeaveRugbyBall();
			break;
		case EState.SausagePolice:
			OnLeaveSausagePolice();
			break;
		}
	}

	private void InternalOnEnter(EState state)
	{
		switch (m_State)
		{
		case EState.Idle:
			OnEnterIdle();
			break;
		case EState.Tickle:
			OnEnterTickle();
			break;
		case EState.Turn:
			OnEnterTurn();
			break;
		case EState.Run:
			OnEnterRun();
			break;
		case EState.String:
			OnEnterString();
			break;
		case EState.Ear:
			OnEnterEar();
			break;
		case EState.Burp:
			OnEnterBurp();
			break;
		case EState.Blow:
			OnEnterBlow();
			break;
		case EState.Yell:
			OnEnterYell();
			break;
		case EState.WallBounce:
			OnEnterWallBounce();
			break;
		case EState.Poke:
			OnEnterPoke();
			break;
		case EState.Dance:
			OnEnterDance();
			break;
		case EState.Rotation:
			OnEnterRotation();
			break;
		case EState.FaceDown:
			OnEnterFaceDown();
			break;
		case EState.CustomCostume:
			OnEnterCustomCostume();
			break;
		case EState.CustomEnvironment:
			OnEnterCustomEnvironment();
			break;
		case EState.PresentTuto:
			OnEnterPresentTuto();
			break;
		case EState.ClownBox:
			OnEnterClownBox();
			break;
		case EState.YellowDuck:
			OnEnterYellowDuck();
			break;
		case EState.Night:
			OnEnterNight();
			break;
		case EState.Steam:
			OnEnterSteam();
			break;
		case EState.Piranha:
			OnEnterPiranha();
			break;
		case EState.Swatter:
			OnEnterSwatter();
			break;
		case EState.WCBrush:
			OnEnterWCBrush();
			break;
		case EState.Micro:
			OnEnterMicro();
			break;
		case EState.PhoneReal:
			OnEnterPhoneReal();
			break;
		case EState.PopGun:
			OnEnterPopGun();
			break;
		case EState.Jetpack:
			OnEnterJetpack();
			break;
		case EState.Shield:
			OnEnterShield();
			break;
		case EState.Guitar:
			OnEnterGuitar();
			break;
		case EState.EgyptianToiletPaper:
			OnEnterEgyptianToiletPaper();
			break;
		case EState.Bow:
			OnEnterBow();
			break;
		case EState.GhettoBlaster:
			OnEnterGhettoBlaster();
			break;
		case EState.Nunchaku:
			OnEnterNunchaku();
			break;
		case EState.RugbyBall:
			OnEnterRugbyBall();
			break;
		case EState.SausagePolice:
			OnEnterSausagePolice();
			break;
		default:
			Utility.Log(ELog.Errors, "New Behaviour Undefined " + m_State);
			break;
		}
	}

	private void InternalOnChangePack(EPack pack)
	{
		if (pack != m_CurrentPack)
		{
			EPack currentPack = m_CurrentPack;
			if (pack != EPack.Interactions && pack != EPack.Count)
			{
				m_CurrentPack = pack;
			}
			else if (m_CurrentPack == EPack.Count)
			{
				m_CurrentPack = EPack.Base;
			}
			if (currentPack != m_CurrentPack)
			{
				Utility.Log(ELog.Gameplay, string.Concat("InternalOnChangePack: ", currentPack, " -> ", pack));
				Utility.ShowActivityView(true);
				FillRandomStatesWithPack(m_CurrentPack);
				ClearAudioClips();
				ClearMiniGameObjects();
				ClearAnimationClipsToBeRemoved();
				ClearAdditionalAnimationClips(true);
				Utility.HideActivityView(true);
			}
		}
	}

	private void SetInternalState(EState state)
	{
		if (state != m_State)
		{
			InternalOnLeave(m_State);
			m_ResetXScale = true;
			Utility.Log(ELog.Gameplay, string.Concat("SetState: ", m_State, " -> ", state, " ; Time: ", m_StateTime));
			EPack packFromState = GetPackFromState(state);
			InternalOnChangePack(packFromState);
			m_State = state;
			m_StateTime = 0f;
			m_StateFrameCount = 0f;
			currentState = state;
			InternalOnEnter(m_State);
		}
	}

	private void LoadIdleAnims()
	{
		for (int i = 0; i < m_IdleAnims.Count; i++)
		{
			AddAnimationClip("BunnyIdle/", m_IdleAnims[i], false);
		}
		Resources.UnloadUnusedAssets();
		InstanciateAnimationClips();
		Idle_LoadSounds();
		Idle_CreateAnimationEvents();
	}

	private void OnEnterIdle()
	{
		m_TickleCounter = 0;
		FillPublicArrays();
	}

	private void OnUpdateIdle()
	{
		if (!base.animIsPlaying || m_ForceIdle)
		{
			SetXScale(1f);
			currentAnim = "None";
			StopSound();
		}
		if (m_MusicButton == 1)
		{
			SetState(EState.Dance);
		}
		else
		{
			if (DetectInteractions() || FakeRandomStates())
			{
				return;
			}
			if (!base.animIsPlaying || m_ForceIdle)
			{
				StartRandomState();
				m_ForceIdle = false;
				return;
			}
			bool flag = true;
			if (m_Scroller != null)
			{
				flag = !m_Scroller.IsInteract(0);
			}
			if (!Utility.InputStoppedByActivity() && m_InputValidity && m_CheckInput && AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Leave && flag && (m_CurrentAnimation == "standby_crazy" || m_CurrentAnimation == "knock" || m_CurrentAnimation == "standby_cri_de_pres"))
			{
				StopSound();
				PlayAnim(m_IdleAnims[Random.Range(0, m_IdleAnims.Count / 2)]);
				m_ForceIdle = false;
				m_ForceIdleAnim = false;
			}
		}
	}

	private void OnLeaveIdle()
	{
		StopSound();
	}

	private void OnPauseIdle(bool pauseResume)
	{
		PauseSound(pauseResume);
	}

	private bool FakeRandomStates()
	{
		return false;
	}

	private void StartRandomState()
	{
		bool flag = true;
		RewindAnim();
		if (m_FirstAnim)
		{
			switch (Random.Range(0, 4))
			{
			case 0:
				PlayAnim("stand_part01");
				break;
			case 1:
				PlayAnim("stand_middle");
				break;
			case 2:
				PlayAnim("stand_part02");
				break;
			}
			m_FirstAnim = false;
			return;
		}
		if (!m_ForceIdleAnim)
		{
			int num = Random.Range(0, 2);
			if (num == 1 && m_TimeSinceLastSuccess > 40f && m_TutorialsStates.Count > 0)
			{
				SetState(EState.PresentTuto);
				flag = false;
			}
			else if (m_TimeSinceLastState > 15f && m_RandomStates.Count > 0)
			{
				if (m_RandomStatesInPack.Count == 0)
				{
					EPack ePack = FindPackWithRandomStates();
					Utility.LogYellow(string.Concat("FindPackWithRandomStates: ", ePack, " / ", m_CurrentPack));
					InternalOnChangePack(ePack);
				}
				if (m_RandomStatesInPack.Count > 0)
				{
					int max = Mathf.Min(3, m_RandomStatesInPack.Count);
					SetState(m_RandomStatesInPack[Random.Range(0, max)]);
					flag = false;
				}
			}
		}
		if (flag)
		{
			if (m_CurrentAnimation == "standby_crazy")
			{
				PlayAnim("Idle_jump");
			}
			else
			{
				Debug.LogWarning("m_IdleAnims is empty! Fix!!");
				if (m_IdleAnims.Count > 0)
					PlayAnim(m_IdleAnims[Random.Range(0, m_IdleAnims.Count)]);
			}
			m_ForceIdleAnim = false;
		}
		else
		{
			m_TimeSinceLastState = 0f;
		}
	}

	private void Idle_LoadSounds()
	{
	}

	private void PlayTauntSound()
	{
		LoadSound("Izi/", ESound.IZI_Turn);
		PlaySound(ESound.IZI_Turn);
	}

	private void Idle_CreateAnimationEvents()
	{
		CreateAnimationEvent("stand_part02", "PlayStandByYellSound", 0);
		CreateAnimationEvent("stand_part01", "PlayAssSound", 0);
		CreateAnimationEvent("stand_middle", "PlayStandby1Sound", 0);
		CreateAnimationEvent("knock", "PlayKnockSound", 0);
		CreateAnimationEvent("standby_crazy", "PlayCrazySound", 0);
		CreateAnimationEvent("standby_cri_de_pres", "PlayYellNearSound", 0);
		if ((bool)base.GetComponent<Animation>()["Idle_Taunt"])
		{
			CreateAnimationEvent("Idle_Taunt", "PlayTauntSound", 0);
		}
	}

	private void LoadPanel()
	{
		m_Panel = LoadMiniGameObject("MiniGames/Panel");
		if (!(m_Panel != null))
		{
			return;
		}
		string text = Utility.GetTexPath() + "MiniGames/Panel/";
		Transform child = m_Panel.transform.GetChild(0);
		if (!(child != null))
		{
			return;
		}
		child.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(text + "PanelHandle");
		child = child.GetChild(0);
		if (child != null)
		{
			child = child.GetChild(0);
			if (child != null)
			{
				child.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(text + "PanelBack");
			}
		}
	}

	private void OnEnterPresentTuto()
	{
		PresentTuto_LoadSounds();
		PresentTuto_CreateAnimationEvents();
		LoadPanel();
		ETutorial tuto = m_TutorialsStates[Random.Range(0, Mathf.Min(3, m_TutorialsStates.Count))];
		EnableTutorial(tuto, m_Panel);
		PlayAnim("basic_sign");
	}

	private void OnUpdatePresentTuto()
	{
		if (base.GetComponent<Animation>()["basic_sign"].time > 1.5f && base.GetComponent<Animation>()["basic_sign"].time < 6f && !Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Began)
		{
			string cast = RaycastUnderFinger(0);
			if (GetCollider(cast) == ECollider.Panel)
			{
				SetAnimTime("basic_sign", 6f);
				PlayTutoEatSound();
				SetState(EState.Idle);
			}
		}
		if (!AnimIsPlaying("basic_sign"))
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeavePresentTuto()
	{
		ClearAdditionalAnimationClips();
		DisablePanel();
		m_Panel = UnloadMiniGameObject(m_Panel);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPausePresentTuto(bool pauseResume)
	{
		PauseAnimatedObject(m_Panel, pauseResume);
	}

	private void PresentTuto_LoadSounds()
	{
	}

	private void PresentTuto_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Common/", "choke"))
		{
			CreateAnimationEvent("choke", "PlayChokeSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/PresentTuto/", "basic_sign"))
		{
			CreateAnimationEvent("basic_sign", "PlayTutoSound", 0);
			CreateAnimationEvent("basic_sign", "StartChoke", base.GetComponent<Animation>()["basic_sign"].length - 0.1f);
		}
	}

	private void OnEnterDance()
	{
		OnEndMediaPicker();
		if (IsSpecialStart())
		{
			m_MusicButton = 1;
			ShowMediaPicker();
			m_Fonctionality.SetType(FonctionalityItem.EType.MusicOff, FonctionalityItem.EType.MusicOn);
			GUIUtils.GetScroller(0).ForceHide();
		}
		if (Application.platform != RuntimePlatform.IPhonePlayer)
		{
			LoadSound("Common/", ESound.N05_Costel);
			PlaySound(ESound.N05_Costel, 0.5f, true);
		}
	}

	private void OnUpdateDance()
	{
		if (m_StateFrameCount == 2f)
		{
			Dance_LoadSounds();
			Dance_CreateAnimationEvents();
			PlayAnim("dance_all");
			CheckNewMove("dance");
			m_FirstDance = true;
		}
		else if (m_StateFrameCount < 3f)
		{
			return;
		}
		if (!AnimIsPlaying("dance_all"))
		{
			m_FirstDance = false;
			if (IsMediaPlaying())
			{
				PlayAnim("dance_all");
			}
			else
			{
				StopMusicDance();
			}
		}
		if (!m_FirstDance && !IsMediaPlaying())
		{
			StopMusicDance();
		}
		if (m_MusicButton == 0)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveDance()
	{
		ClearAdditionalAnimationClips();
		StopMusicDance();
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseDance(bool pauseResume)
	{
	}

	private void Dance_LoadSounds()
	{
	}

	private void Dance_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Dance/", "dance_all"))
		{
			CreateAnimationEvent("dance_all", "PlayDanceSound", 0);
		}
	}

	private bool CheckEndRotation()
	{
		return !base.animIsPlaying && (m_CurrentAnimation == "get up" || EndRotation());
	}

	private bool EndRotation()
	{
		bool flag = false;
		Vector3 euler = Vector3.zero;
		Vector3 pos = Vector3.zero;
		if (s_TargetOrientation == 4)
		{
			if (s_CurrentOrientation == 3)
			{
				flag = true;
			}
			else
			{
				euler = new Vector3(0f, 0f, 270f);
				pos = new Vector3(-0.9f, 0f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 3)
		{
			if (s_CurrentOrientation == 4)
			{
				flag = true;
			}
			else
			{
				euler = new Vector3(0f, 0f, 90f);
				pos = new Vector3(0.9f, 0f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 1)
		{
			if (s_CurrentOrientation == 2)
			{
				flag = true;
			}
			else
			{
				euler = new Vector3(0f, 0f, 0f);
				pos = new Vector3(0f, -0.9f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 2)
		{
			if (s_CurrentOrientation == 1)
			{
				flag = true;
			}
			else
			{
				euler = new Vector3(0f, 0f, 180f);
				pos = new Vector3(0f, 0.9f, -0.7f);
			}
		}
		if (flag)
		{
			Rotation_CreateAndPlayGetUp();
		}
		else
		{
			if (DephaseRabbid())
			{
				euler.z += 180f;
				pos.x *= -1f;
				pos.y *= -1f;
			}
			SetXScale(1f);
			SetTR(pos, euler);
			m_ReactivateShadow = true;
			Rotation_CreateAndPlayStandPart();
		}
		m_BlockBouncingCounter = kBlockBouncingDelay;
		s_CurrentOrientation = s_TargetOrientation;
		m_BouncingCounter = 0;
		return !flag;
	}

	private void OnEnterRotation()
	{
		Rotation_LoadSounds();
		Rotation_CreateAnimationEvents();
		bool flag = false;
		Vector3 euler = Vector3.zero;
		Vector3 pos = Vector3.zero;
		CheckNewMove("rotate");
		s_TargetOrientation = (int)AllInput.GetDeviceOrientation();
		if (s_TargetOrientation == 4)
		{
			if (s_CurrentOrientation == 3)
			{
				flag = true;
				euler = new Vector3(0f, 0f, 270f);
			}
			else if (s_CurrentOrientation == 2)
			{
				SetXScale(-1f);
				euler = new Vector3(0f, 180f, 270f);
			}
			else
			{
				euler = new Vector3(0f, 0f, 270f);
			}
			pos = new Vector3(-0.9f, 0f, -0.7f);
		}
		else if (s_TargetOrientation == 3)
		{
			if (s_CurrentOrientation == 4)
			{
				flag = true;
				euler = new Vector3(0f, 0f, 90f);
			}
			else if (s_CurrentOrientation == 1)
			{
				SetXScale(-1f);
				euler = new Vector3(0f, 180f, 90f);
			}
			else
			{
				euler = new Vector3(0f, 0f, 90f);
			}
			pos = new Vector3(0.9f, 0f, -0.7f);
		}
		else if (s_TargetOrientation == 1)
		{
			if (s_CurrentOrientation == 2)
			{
				flag = true;
				euler = new Vector3(0f, 0f, 0f);
			}
			else if (s_CurrentOrientation == 4)
			{
				SetXScale(-1f);
				euler = new Vector3(-180f, 0f, 0f);
				pos = new Vector3(0f, 0.9f, -0.7f);
			}
			else
			{
				euler = new Vector3(0f, 0f, 0f);
			}
			pos = new Vector3(0f, -0.9f, -0.7f);
		}
		else if (s_TargetOrientation == 2)
		{
			if (s_CurrentOrientation == 1)
			{
				flag = true;
				euler = new Vector3(0f, 0f, 180f);
			}
			else if (s_CurrentOrientation == 4)
			{
				euler = new Vector3(180f, 0f, 0f);
				pos = new Vector3(0f, 0.9f, -0.7f);
			}
			else
			{
				SetXScale(-1f);
				euler = new Vector3(0f, 0f, 0f);
			}
			pos = new Vector3(0f, 0.9f, -0.7f);
		}
		m_ResetXScale = false;
		if (flag)
		{
			if (DephaseRabbid())
			{
				euler.z += 180f;
				pos.x *= -1f;
				pos.y *= -1f;
			}
			SetTR(pos, euler);
			StopAnim();
			Rotation_CreateAndPlayWallBounceDown();
		}
		else
		{
			Rotation_CreateAndPlayScreenRotate();
		}
		m_ResetXScale = true;
	}

	private void OnUpdateRotation()
	{
		if (CheckEndRotation())
		{
			SetState(EState.Idle);
		}
		else if (AllInput.GetDeviceOrientation() != (DeviceOrientation)s_CurrentOrientation && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceUp && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceDown && AllInput.GetDeviceOrientation() != DeviceOrientation.Unknown)
		{
			if (m_CurrentAnimation == "get up")
			{
				OnEnterRotation();
			}
		}
		else if (AllInput.GetDeviceOrientation() != (DeviceOrientation)s_TargetOrientation && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceUp && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceDown && AllInput.GetDeviceOrientation() != DeviceOrientation.Unknown && m_CurrentAnimation == "screen_rotate" && m_CurrentAnimTime > 0.5f)
		{
			FinalizeRotation();
			OnEnterRotation();
		}
	}

	private void OnLeaveRotation()
	{
	}

	private void FinalizeRotation()
	{
		Vector3 euler = Vector3.zero;
		Vector3 pos = Vector3.zero;
		s_CurrentOrientation = s_TargetOrientation;
		if (s_TargetOrientation == 4)
		{
			if (s_CurrentOrientation != 3)
			{
				euler = new Vector3(0f, 0f, 270f);
				pos = new Vector3(-0.9f, 0f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 3)
		{
			if (s_CurrentOrientation != 4)
			{
				euler = new Vector3(0f, 0f, 90f);
				pos = new Vector3(0.9f, 0f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 1)
		{
			if (s_CurrentOrientation != 2)
			{
				euler = new Vector3(0f, 0f, 0f);
				pos = new Vector3(0f, -0.9f, -0.7f);
			}
		}
		else if (s_TargetOrientation == 2 && s_CurrentOrientation != 1)
		{
			euler = new Vector3(0f, 0f, 180f);
			pos = new Vector3(0f, 0.9f, -0.7f);
		}
		if (DephaseRabbid())
		{
			euler.z += 180f;
			pos.x *= -1f;
			pos.y *= -1f;
		}
		SetXScale(1f);
		SetTR(pos, euler);
		m_ReactivateShadow = true;
	}

	private void OnPauseRotation(bool pauseResume)
	{
	}

	private void Rotation_LoadSounds()
	{
	}

	private void Rotation_CreateAnimationEvents()
	{
	}

	private void Rotation_CreateAndPlayStandPart()
	{
		if (AddAnimationClip("BunnyIdle/", "stand_part01", false))
		{
		}
		PlayAnim("stand_part01");
	}

	private void Rotation_CreateAndPlayScreenRotate()
	{
		if (AddAnimationClip("BunnyInteractions/Rotation/", "screen_rotate"))
		{
			CreateAnimationEvent("screen_rotate", "PlayRotateSound", 0);
		}
		StopAnim();
		RewindAnim();
		PlayAnim("screen_rotate", 2f);
	}

	private void Rotation_CreateAndPlayWallBounceDown()
	{
		if (AddAnimationClip("BunnyInteractions/Common/", "wall bounce down"))
		{
			CreateAnimationEvent("wall bounce down", "PlayPafSound", 0);
		}
		RewindAnim("wall bounce down");
		PlayAnim("wall bounce down", 1f);
	}

	private void Rotation_CreateAndPlayGetUp()
	{
		if (AddAnimationClip("BunnyInteractions/Common/", "get up"))
		{
			CreateAnimationEvent("get up", "PlayVerticalGetUpSound", 0);
		}
		PlayAnim("get up");
		FinalizeRotation();
	}

	private bool CheckEndFaceDown()
	{
		return m_CurrentAnimation == "step_back" && (!base.animIsPlaying || m_CurrentAnimTime > m_CurrentAnimLength);
	}

	private void OnEnterFaceDown()
	{
		Facedown_LoadSounds();
		FaceDown_CreateAnimationEvents();
		CheckNewMove("rotate_down");
		if (m_TutorialsStates.Contains(ETutorial.Facedown))
		{
			m_TutorialsStates.Remove(ETutorial.Facedown);
		}
		m_LastOrientation = s_CurrentOrientation;
		s_TargetOrientation = 6;
		s_CurrentOrientation = 6;
		m_BlockBouncingCounter = kBlockBouncingDelay;
		m_BouncingCounter = 0;
		PlayAnim("screen_frontbouncing");
		ActivateShadow(false);
	}

	private void OnUpdateFaceDown()
	{
		if (CheckEndFaceDown())
		{
			SetState(EState.Idle);
		}
		else
		{
			ComputeFaceDown();
		}
	}

	private void ComputeFaceDown()
	{
		if (!base.animIsPlaying)
		{
			if (m_CurrentAnimation == "screen_frontbouncing")
			{
				PlayAnim("screen_stand_up");
			}
			else if (m_CurrentAnimation == "screen_stand_up" || m_CurrentAnimation == "look_down")
			{
				PlayAnim("look_down");
			}
		}
		if (AllInput.GetDeviceOrientation() != DeviceOrientation.FaceDown && AllInput.GetDeviceOrientation() != DeviceOrientation.Unknown && s_TargetOrientation == s_CurrentOrientation)
		{
			s_TargetOrientation = m_LastOrientation;
			m_BlockBouncingCounter = kBlockBouncingDelay;
			PlayAnim("step_back");
		}
	}

	private void OnLeaveFaceDown()
	{
		ClearAdditionalAnimationClips();
		m_ForceIdle = true;
		s_CurrentOrientation = s_TargetOrientation;
		ActivateShadow(true);
	}

	private void OnPauseFaceDown(bool pauseResume)
	{
	}

	private void Facedown_LoadSounds()
	{
	}

	private void FaceDown_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/FaceDown/", "screen_stand_up"))
		{
			CreateAnimationEvent("screen_stand_up", "PlayScreenGetUpSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/FaceDown/", "step_back"))
		{
			CreateAnimationEvent("step_back", "PlayStepBackSound", 0);
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.functionName = "ActivateShadow";
			animationEvent.intParameter = 1;
			animationEvent.time = 0.95f;
			base.GetComponent<Animation>()["step_back"].clip.AddEvent(animationEvent);
			SetWrapMode("step_back", WrapMode.Once);
		}
		AddAnimationClip("BunnyInteractions/FaceDown/", "look_down");
		AddAnimationClip("BunnyInteractions/Common/", "screen_frontbouncing");
	}

	private void OnEnterTickle()
	{
		Tickle_LoadSounds();
		Tickle_CreateAnimationEvents();
		CheckNewMove("tickle");
		RewindAnim("tickle_start");
		PlayAnim("tickle_start");
		LoadSound("Izi/", ESound.IZI_Tickle_step1);
		PlaySound(ESound.IZI_Tickle_step1);
		m_TickleLoopTime = 0f;
		m_TickleRestart = false;
	}

	private void OnUpdateTickle()
	{
		if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 1)
		{
			if (AllInput.GetState(0) == AllInput.EState.Moved)
			{
				string text = RaycastUnderFinger(0);
				if (text != null && GetCollider(text) == ECollider.Tummy && (AnimIsPlaying("tickle_end") || AnimIsPlaying("tickle_loop")))
				{
					m_TickleRestart = true;
				}
			}
			else if (m_CheckInput && AllInput.GetState(0) == AllInput.EState.Leave)
			{
				PlayAnim("tickle_end");
				LoadSound("Izi/", ESound.IZI_Tickle_step3);
				PlaySound(ESound.IZI_Tickle_step3);
			}
		}
		if (DetectInteractions())
		{
			return;
		}
		if (!base.animIsPlaying)
		{
			if (m_CurrentAnimation == "tickle_start")
			{
				DoTickleLoop();
				m_TickleLoopTime = 0f;
			}
			else if (m_CurrentAnimation == "tickle_loop")
			{
				DoTickleLoop();
				m_TickleLoopTime = 0f;
				m_TickleRestart = false;
			}
			else if (m_CurrentAnimation == "tickle_end")
			{
				SetState(EState.Idle);
			}
		}
		else if (m_CurrentAnimation == "tickle_loop")
		{
			m_TickleLoopTime += Time.deltaTime;
			if (!m_TickleRestart && m_TickleLoopTime > m_CurrentAnimLength * 1.2f)
			{
				RewindAnim("tickle_end");
				PlayAnim("tickle_end");
			}
		}
	}

	private void OnLeaveTickle()
	{
	}

	private void OnPauseTickle(bool pauseResume)
	{
	}

	private void Tickle_LoadSounds()
	{
	}

	private void Tickle_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Tickle/", "tickle_loop"))
		{
			SetWrapMode("tickle_loop", WrapMode.PingPong);
		}
		if (AddAnimationClip("BunnyInteractions/Tickle/", "tickle_start"))
		{
		}
		AddAnimationClip("BunnyInteractions/Tickle/", "tickle_end");
	}

	private void OnEnterTurn()
	{
		if (AnimIsPlaying("turn") && base.GetComponent<Animation>()["turn"].time >= 0.5f)
		{
			SetAnimTime("turn", 0.5f);
			LoadSound("Izi/", ESound.IZI_Turn);
			PlaySound(ESound.IZI_Turn);
			return;
		}
		Turn_LoadSounds();
		Turn_CreateAnimationEvents();
		LoadSound("Izi/", ESound.IZI_Turn);
		PlaySound(ESound.IZI_Turn);
		PlayAnim("turn");
		CheckNewMove("turn");
		if (m_TutorialsStates.Contains(ETutorial.Turn))
		{
			m_TutorialsStates.Remove(ETutorial.Turn);
		}
	}

	private void OnUpdateTurn()
	{
		if (!DetectInteractions() && !base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveTurn()
	{
		ClearAdditionalAnimationClips();
		ActivateShadow(true);
	}

	private void OnPauseTurn(bool pauseResume)
	{
	}

	private void Turn_LoadSounds()
	{
	}

	private void Turn_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Turn/", "turn"))
		{
			CreateAnimationEvent("turn", "PlayTurnHitSound", 3.23f);
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.functionName = "ActivateShadow";
			animationEvent.intParameter = 0;
			animationEvent.time = 3.21f;
			base.GetComponent<Animation>()["turn"].clip.AddEvent(animationEvent);
			animationEvent.intParameter = 1;
			animationEvent.time = 3.95f;
			base.GetComponent<Animation>()["turn"].clip.AddEvent(animationEvent);
		}
	}

	private void OnEnterRun()
	{
		Run_LoadSounds();
		Run_CreateAnimationEvents();
		CheckNewMove("run");
		if (m_TutorialsStates.Contains(ETutorial.Run))
		{
			m_TutorialsStates.Remove(ETutorial.Run);
		}
		if (m_RunDirection == EDirection.Left)
		{
			SetXScale(-1f);
			m_ResetXScale = false;
		}
		RewindAnim("run_acceleration");
		PlayAnim("run_acceleration");
		m_CurrentRunDirection = m_RunDirection;
		Utility.Log(ELog.Gameplay, "Run: " + m_RunDirection);
	}

	private void OnUpdateRun()
	{
		if (DetectRunning(null) && AnimIsPlaying("run_running_fast") && base.GetComponent<Animation>()["run_running_fast"].time > 2f && m_CurrentRunDirection == m_RunDirection)
		{
			Utility.Log(ELog.Gameplay, "Re Run: " + m_RunDirection);
			PlayRunSound2();
			DoRunningFast();
			m_CurrentRunDirection = m_RunDirection;
		}
		if (!DetectInteractions() && !base.animIsPlaying)
		{
			if (m_CurrentAnimation == "run_deceleration")
			{
				SetState(EState.Idle);
				return;
			}
			DoDeceleration();
			SetState(EState.Idle);
		}
	}

	private void OnLeaveRun()
	{
		ClearAdditionalAnimationClips();
	}

	private void OnPauseRun(bool pauseResume)
	{
	}

	private void Run_LoadSounds()
	{
	}

	private void Run_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Run/", "run_acceleration"))
		{
			CreateAnimationEvent("run_acceleration", "PlayRunSound1", 0);
			CreateAnimationEvent("run_acceleration", "DoRunningFast", base.GetComponent<Animation>()["run_acceleration"].length - 0.1f);
		}
		if (AddAnimationClip("BunnyInteractions/Run/", "run_deceleration"))
		{
			CreateAnimationEvent("run_deceleration", "PlayRunSound3", 0);
		}
	}

	private void PlayPullStringAnim()
	{
		switch (Random.Range(0, 3))
		{
		case 0:
			PlayAnim("stand_part01");
			break;
		case 1:
			PlayAnim("stand_part02");
			break;
		default:
			PlayAnim("stand_middle");
			break;
		}
	}

	private void PlayReleaseStringAnim()
	{
		if (!(stringPointTransform == null))
		{
			if (stringPointTransform.localPosition.x < -0.1f)
			{
				PlayAnim("string_right");
			}
			else if (stringPointTransform.localPosition.x > 0.5f)
			{
				PlayAnim("string_left");
			}
			else
			{
				PlayAnim("string_up");
			}
		}
	}

	private void OnEnterString()
	{
		String_LoadSounds();
		String_CreateAnimationEvents();
		Vector3 contactPoint = Vector3.zero;
		if (m_String != null)
		{
			m_String.SetActiveRecursively(true);
		}
		if (m_StringInteractionPlane != null)
		{
			m_StringInteractionPlane.active = true;
		}
		if (GetContactPointOnInteractivePlane(0, out contactPoint))
		{
			stringPointTransform.position = contactPoint;
		}
		m_PullState = EPullState.Pull;
		PlayPullStringAnim();
	}

	private void OnUpdateString()
	{
		if (m_PullState == EPullState.Pull)
		{
			if (!base.animIsPlaying)
			{
				PlayPullStringAnim();
			}
			if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() != 1)
			{
				m_PullState = EPullState.Release;
				PlayReleaseStringAnim();
				m_String.SetActiveRecursively(false);
				if (m_TutorialsStates.Contains(ETutorial.String))
				{
					m_TutorialsStates.Remove(ETutorial.String);
				}
				return;
			}
			Vector3 contactPoint = Vector3.zero;
			if (GetContactPointOnInteractivePlane(0, out contactPoint))
			{
				Vector3 vector = contactPoint - stringPointTransform.position;
				vector *= 0.28f;
				stringPointTransform.position += vector;
				if (stringPointTransform.localPosition.z < 0f)
				{
					vector = stringPointTransform.localPosition;
					vector.z = 0f;
					stringPointTransform.localPosition = vector;
				}
			}
		}
		else if (m_PullState == EPullState.Release && !base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveString()
	{
		ClearAdditionalAnimationClips();
		if (m_String != null)
		{
			m_String.SetActiveRecursively(false);
		}
		if (m_StringInteractionPlane != null)
		{
			m_StringInteractionPlane.active = false;
		}
	}

	private void OnPauseString(bool pauseResume)
	{
		if (m_String != null)
		{
			m_String.SetActiveRecursively(!pauseResume);
		}
		if (m_StringInteractionPlane != null)
		{
			m_StringInteractionPlane.SetActiveRecursively(!pauseResume);
		}
	}

	private void String_LoadSounds()
	{
	}

	private void String_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/String/", "string_left"))
		{
			CreateAnimationEvent("string_left", "PlayStringOuchSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/String/", "string_up"))
		{
			CreateAnimationEvent("string_up", "PlayStringOuchSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/String/", "string_right"))
		{
			CreateAnimationEvent("string_right", "PlayStringOuchSound", 0);
			CreateAnimationEvent("string_right", "HideString", base.GetComponent<Animation>()["string_right"].length - 0.1f);
		}
		if (!AddAnimationClip("BunnyIdle/", "stand_part02", false))
		{
		}
	}

	private void PlayPullEarAnim()
	{
		switch (Random.Range(0, 3))
		{
		case 0:
			PlayAnim("stand_part01");
			break;
		case 1:
			PlayAnim("stand_part02");
			break;
		default:
			PlayAnim("stand_middle");
			break;
		}
	}

	private void PlayReleaseEarAnim()
	{
		switch (Random.Range(0, 2))
		{
		case 0:
			PlayAnim("ears2");
			break;
		case 1:
			PlayAnim("ears1");
			break;
		}
		LoadSound("Izi/", ESound.IZI_pulling_hit1);
		PlaySound(ESound.IZI_pulling_hit1);
	}

	private void OnEnterEar()
	{
		Ear_LoadSounds();
		Ear_CreateAnimationEvents();
		Vector3 contactPoint = Vector3.zero;
		if (m_EarSide == ESide.Left)
		{
			m_EarCurrentPosition = earLeftPointTransform.transform.position;
		}
		else
		{
			m_EarCurrentPosition = earRightPointTransform.transform.position;
		}
		if (m_EarsInteractionPlane != null)
		{
			m_EarsInteractionPlane.active = true;
		}
		if (GetContactPointOnInteractivePlane(0, out contactPoint))
		{
			m_EarWantedPosition = contactPoint;
		}
		m_PullState = EPullState.Pull;
		PlayPullEarAnim();
	}

	private void OnUpdateEar()
	{
		if (m_PullState == EPullState.Pull)
		{
			if (!base.animIsPlaying)
			{
				PlayPullEarAnim();
			}
			if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() != 1)
			{
				m_PullState = EPullState.Release;
				PlayReleaseEarAnim();
				CheckNewMove("ear");
				LoadSound("Izi/", ESound.IZI_String_slap);
				PlaySound(ESound.IZI_String_slap);
				return;
			}
			Vector3 contactPoint = Vector3.zero;
			if (GetContactPointOnInteractivePlane(0, out contactPoint))
			{
				m_EarWantedPosition = contactPoint;
			}
			if (m_StateTime > 0.5f)
			{
				ManageMicrophoneInteractions();
			}
			if (m_IsBlowGesture)
			{
				m_IsBlowGesture = false;
				SetState(EState.Blow);
			}
			else if (m_IsYellGesture)
			{
				m_IsYellGesture = false;
				SetState(EState.Yell);
			}
		}
		else if (m_PullState == EPullState.Release && !base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveEar()
	{
		if (m_EarsInteractionPlane != null)
		{
			m_EarsInteractionPlane.active = false;
		}
		m_PullState = EPullState.Release;
		m_EarSide = ESide.Count;
		EndRecording();
	}

	private void OnPauseEar(bool pauseResume)
	{
		if (m_EarsInteractionPlane != null)
		{
			m_EarsInteractionPlane.SetActiveRecursively(!pauseResume);
		}
	}

	private void Ear_LoadSounds()
	{
	}

	private void Ear_CreateAnimationEvents()
	{
		AddAnimationClip("BunnyInteractions/Ears/", "ears1");
		AddAnimationClip("BunnyInteractions/Ears/", "ears2");
		AddAnimationClip("BunnyIdle/", "stand_middle", false);
		AddAnimationClip("BunnyIdle/", "stand_part01", false);
		AddAnimationClip("BunnyIdle/", "stand_part02", false);
	}

	private void OnEnterBurp()
	{
		Burp_LoadSounds();
		Burp_CreateAnimationEvents();
		PlayAnim("burp");
		CheckNewMove("burp");
		if (m_TutorialsStates.Contains(ETutorial.Burp))
		{
			m_TutorialsStates.Remove(ETutorial.Burp);
		}
	}

	private void OnUpdateBurp()
	{
		if (!DetectInteractions() && !base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveBurp()
	{
		ClearAdditionalAnimationClips();
	}

	private void OnPauseBurp(bool pauseResume)
	{
	}

	private void Burp_LoadSounds()
	{
		LoadSound("Izi/", ESound.IZI_Burp);
	}

	private void Burp_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Burp/", "burp"))
		{
			CreateAnimationEvent("burp", "PlayBurpSound", 0);
		}
	}

	private void OnEnterYell()
	{
		Yell_LoadSounds();
		Yell_CreateAnimationEvents();
		RewindAnim();
		PlayAnim("yell");
		CheckNewMove("yell");
		if (m_TutorialsStates.Contains(ETutorial.Yell))
		{
			m_TutorialsStates.Remove(ETutorial.Yell);
		}
	}

	private void OnUpdateYell()
	{
		if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveYell()
	{
		ClearAdditionalAnimationClips();
	}

	private void OnPauseYell(bool pauseResume)
	{
	}

	private void Yell_LoadSounds()
	{
	}

	private void Yell_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Yell/", "yell"))
		{
			CreateAnimationEvent("yell", "PlayYellMicroSound", 0);
			CreateAnimationEvent("yell", "PlayYellSound", 3f);
		}
	}

	private void OnEnterBlow()
	{
		Blow_LoadSounds();
		Blow_CreateAnimationEvents();
		PlayAnim("blow_start");
		CheckNewMove("blow");
		if (m_TutorialsStates.Contains(ETutorial.Blow))
		{
			m_TutorialsStates.Remove(ETutorial.Blow);
		}
		LoadSound("Izi/", ESound.IZI_Blow);
		PlaySound(ESound.IZI_Blow);
	}

	private void OnUpdateBlow()
	{
		if (m_CurrentAnimation == "blow_getup" && !base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
		else if (m_IsBlowGesture && m_CurrentAnimation == "blow_loop")
		{
			RewindAnim("blow_loop");
			PlayAnim("blow_loop");
			LoadSound("Izi/", ESound.IZI_Blow_on_wall);
			PlaySound(ESound.IZI_Blow_on_wall);
		}
	}

	private void OnLeaveBlow()
	{
		ClearAdditionalAnimationClips();
	}

	private void OnPauseBlow(bool pauseResume)
	{
	}

	private void Blow_LoadSounds()
	{
		LoadSound("Izi/", ESound.IZI_Blow_fall_and_getup);
		LoadSound("Izi/", ESound.IZI_Blow_on_wall);
		LoadSound("Izi/", ESound.IZI_Blow);
	}

	private void Blow_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Blow/", "blow_getup"))
		{
		}
		if (AddAnimationClip("BunnyInteractions/Blow/", "blow_loop"))
		{
			CreateAnimationEvent("blow_loop", "DoBlowGetUp", base.GetComponent<Animation>()["blow_loop"].length - 0.1f);
		}
		if (AddAnimationClip("BunnyInteractions/Blow/", "blow_start"))
		{
			CreateAnimationEvent("blow_start", "DoBlowLoop", base.GetComponent<Animation>()["blow_start"].length - 0.1f);
		}
	}

	private ESide ComputeBouncingAnimSide()
	{
		bool flag = false;
		ESide eSide = ESide.Count;
		Vector3 vector = GetGravityDirection();
		if (GlobalVariables.FAKE_SWALLBOUNCE || GlobalVariables.FAKE_HWALLBOUNCE || GlobalVariables.FAKE_VWALLBOUNCE)
		{
			vector = Vector3.zero;
			vector.x = ((!GlobalVariables.FAKE_HWALLBOUNCE) ? 0f : 2f);
			vector.y = ((!GlobalVariables.FAKE_VWALLBOUNCE) ? 0f : 2f);
			vector.z = ((!GlobalVariables.FAKE_SWALLBOUNCE) ? 0f : 2f);
			GlobalVariables.FAKE_HWALLBOUNCE = false;
			GlobalVariables.FAKE_VWALLBOUNCE = false;
			GlobalVariables.FAKE_SWALLBOUNCE = false;
			flag = true;
		}
		if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y) && Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
		{
			if (flag)
			{
				return (m_FakeBouncingCounter++ % 2 != 0) ? ESide.Right : ESide.Left;
			}
			return (!(vector.x > kAccelerationThreshold)) ? ESide.Right : ESide.Left;
		}
		if (Mathf.Abs(vector.y) > Mathf.Abs(vector.z))
		{
			if (flag)
			{
				return (m_FakeBouncingCounter++ % 2 != 0) ? ESide.Down : ESide.Up;
			}
			return (!(vector.y > kAccelerationThreshold)) ? ESide.Down : ESide.Up;
		}
		if (flag)
		{
			return (m_FakeBouncingCounter++ % 2 != 0) ? ESide.Back : ESide.Front;
		}
		if (vector.z < 0f - kAccelerationThreshold)
		{
			return ESide.Back;
		}
		return ESide.Front;
	}

	private string ComputeBouncingAnimName(ESide side)
	{
		string text = "wall bounce ";
		switch (side)
		{
		case ESide.Front:
			return "screen_frontbouncing";
		case ESide.Back:
			return "start_bounce_back";
		default:
			return text + side.ToString().ToLower();
		}
	}

	private void CheckBouncingMove()
	{
		switch (m_LastBounce)
		{
		case ESide.Left:
		case ESide.Right:
			CheckNewMove("wallbounce_horizontal");
			break;
		case ESide.Up:
		case ESide.Down:
			CheckNewMove("wallbounce_vertical");
			break;
		case ESide.Front:
		case ESide.Back:
			CheckNewMove("wallbounce_screen");
			break;
		}
	}

	private void OnEnterWallBounce()
	{
		Wallbounce_LoadSounds();
		Wallbounce_CreateAnimationEvents();
		ESide eSide = ESide.Count;
		string empty = string.Empty;
		eSide = ComputeBouncingAnimSide();
		eSide = ((eSide != ESide.Down) ? eSide : ESide.Up);
		empty = ComputeBouncingAnimName(eSide);
		m_LastBounce = eSide;
		m_BouncingCounter = kBouncingDelay;
		if (eSide == ESide.Left)
		{
			empty = "start " + empty;
		}
		RewindAnim(empty);
		PlayAnim(empty, 2f);
		CheckBouncingMove();
	}

	private void OnUpdateWallBounce()
	{
		if (m_BouncingCounter != 0 && ShouldWallBounce())
		{
			ESide eSide = ESide.Count;
			string empty = string.Empty;
			eSide = ComputeBouncingAnimSide();
			empty = ComputeBouncingAnimName(eSide);
			if (m_LastBounce != eSide)
			{
				m_BouncingCounter = kBouncingDelay;
				m_LastBounce = eSide;
				RewindAnim(empty);
				PlayAnim(empty, 2f);
				CheckBouncingMove();
				ActivateShadow(true);
			}
		}
		else
		{
			if (base.animIsPlaying)
			{
				return;
			}
			if (m_CurrentAnimation == "wall bounce up")
			{
				RewindAnim("wall bounce down");
				PlayAnim("wall bounce down", 1f);
				m_LastBounce = ESide.Up;
			}
			if (m_LastBounce != ESide.Count)
			{
				m_BouncingCounter = kBouncingDelay;
				switch (m_LastBounce)
				{
				case ESide.Left:
					RewindAnim("fall left");
					PlayAnim("fall left", 1.5f);
					break;
				case ESide.Right:
					RewindAnim("fall_bouncing_right");
					PlayAnim("fall_bouncing_right", 1.5f);
					break;
				case ESide.Up:
					RewindAnim("get up");
					PlayAnim("get up", 1.5f);
					break;
				case ESide.Down:
					RewindAnim("wall bounce down");
					PlayAnim("wall bounce down", 1f);
					break;
				case ESide.Front:
					RewindAnim("screen_frontfalling");
					PlayAnim("screen_frontfalling", 1.5f);
					break;
				case ESide.Back:
					RewindAnim("fall_back");
					PlayAnim("fall_back", 1.5f);
					break;
				}
				m_LastBounce = ESide.Count;
			}
			else
			{
				SetState(EState.Idle);
			}
		}
	}

	private void OnLeaveWallBounce()
	{
		m_LastBounce = ESide.Count;
		m_BlockBouncingCounter = kBlockBouncingDelay;
	}

	private void OnPauseWallBounce(bool pauseResume)
	{
	}

	private void Wallbounce_LoadSounds()
	{
		LoadSound("Izi/", ESound.IZI_Wall_Vertical_bottom_to_top1);
		LoadSound("Izi/", ESound.IZI_Wall_Vertical_top_to_bottom1);
		LoadSound("Izi/", ESound.IZI_Screen_Bouncing_front_to_back1);
	}

	private void Wallbounce_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "fall left"))
		{
			CreateAnimationEvent("fall left", "PlayGetUpSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "fall_back"))
		{
			CreateAnimationEvent("fall_back", "PlayBackGetUpSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "fall_bouncing_right"))
		{
			CreateAnimationEvent("fall_bouncing_right", "PlayGetUpSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "screen_frontfalling"))
		{
			CreateAnimationEvent("screen_frontfalling", "PlayFrontBackGetUpSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "start wall bounce left"))
		{
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "start_bounce_back"))
		{
			CreateAnimationEvent("start_bounce_back", "PlayPafSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "wall bounce left"))
		{
			CreateAnimationEvent("wall bounce left", "PlayPafSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "wall bounce right"))
		{
			CreateAnimationEvent("wall bounce right", "PlayPafSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Wallbouncing/", "wall bounce up"))
		{
			CreateAnimationEvent("wall bounce up", "PlayPafSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Common/", "screen_frontbouncing"))
		{
			CreateAnimationEvent("screen_frontbouncing", "FrontBouncingShadow", 0.35f);
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.functionName = "ActivateShadow";
			animationEvent.intParameter = 1;
			animationEvent.time = 0.7f;
			base.GetComponent<Animation>()["screen_frontfalling"].clip.AddEvent(animationEvent);
		}
		if (AddAnimationClip("BunnyInteractions/Common/", "wall bounce down"))
		{
			CreateAnimationEvent("wall bounce down", "PlayPafSound", 0);
		}
		if (AddAnimationClip("BunnyInteractions/Common/", "get up"))
		{
			CreateAnimationEvent("get up", "PlayVerticalGetUpSound", 0);
		}
	}

	private void OnEnterPoke()
	{
	}

	private void OnUpdatePoke()
	{
		if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeavePoke()
	{
	}

	private void OnPausePoke(bool pauseResume)
	{
	}

	private void PokeRightEye()
	{
		StopSound();
		if (m_State != EState.Idle)
		{
			SetState(EState.Poke);
		}
		PlayAnim("poke2");
		CheckNewMove("poke_eye");
	}

	private void PokeLeftEye()
	{
		StopSound();
		if (m_State != EState.Idle)
		{
			SetState(EState.Poke);
		}
		SetXScale(-1f);
		PlayAnim("poke2");
		CheckNewMove("poke_eye");
	}

	private void PokeTummy()
	{
		StopSound();
		if (m_State != EState.Idle)
		{
			SetState(EState.Poke);
		}
		PlayAnim("poke1");
		CheckNewMove("poke_body");
	}

	private bool DetectInteractions()
	{
		if (!Utility.InputStoppedByActivity() && m_InputValidity && AllInput.GetTouchCount() == 1)
		{
			bool flag = false;
			string text = RaycastUnderFinger(0);
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_DetectTouchMoved = false;
			}
			else if (AllInput.GetState(0) == AllInput.EState.Moved)
			{
				if (!m_DetectTouchMoved)
				{
					m_DetectTouchMoved = true;
					flag = true;
				}
			}
			else if (AllInput.GetState(0) == AllInput.EState.Leave && m_CheckInput && !m_TouchMoved && text != null && DetectPoking(text))
			{
				return true;
			}
			if (flag && DetectEar(text))
			{
				SetState(EState.Ear);
				return true;
			}
			if (DetectRunning(text))
			{
				SetState(EState.Run);
				return true;
			}
			if (DetectTickling(text) && m_State != EState.Turn)
			{
				SetState(EState.Tickle);
				return true;
			}
		}
		if (m_IsCircleGesture && m_State != EState.Tickle)
		{
			m_IsCircleGesture = false;
			SetState(EState.Turn);
			return true;
		}
		if (m_IsBurpGesture)
		{
			m_IsBurpGesture = false;
			SetState(EState.Burp);
			return true;
		}
		return false;
	}

	private EState DetectGravityChangesInCustom()
	{
		if (m_State != EState.CustomCostume && m_State != EState.CustomEnvironment)
		{
			return EState.Count;
		}
		if (m_BlockBouncingCounter == 0 && m_BouncingCounter == 0)
		{
			if (AllInput.GetDeviceOrientation() == DeviceOrientation.FaceDown)
			{
				return EState.FaceDown;
			}
			if (AllInput.GetDeviceOrientation() != (DeviceOrientation)s_CurrentOrientation && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceUp && AllInput.GetDeviceOrientation() != DeviceOrientation.Unknown)
			{
				return EState.Rotation;
			}
		}
		return EState.Count;
	}

	private bool DetectGravityChanges()
	{
		if (m_BlockBouncingCounter == 0 && m_BouncingCounter == 0 && s_GravityBlocker > 20)
		{
			if (ShouldWallBounce())
			{
				SetState(EState.WallBounce);
				return true;
			}
			if (AllInput.GetDeviceOrientation() == DeviceOrientation.FaceDown)
			{
				SetState(EState.FaceDown);
				return true;
			}
			if (AllInput.GetDeviceOrientation() != (DeviceOrientation)s_CurrentOrientation && AllInput.GetDeviceOrientation() != DeviceOrientation.FaceUp && AllInput.GetDeviceOrientation() != DeviceOrientation.Unknown)
			{
				SetState(EState.Rotation);
				return true;
			}
		}
		return false;
	}

	private bool DetectPoking(string cast)
	{
		if (m_TouchTime < 0.5f)
		{
			if (GetCollider(cast) == ECollider.LeftEye)
			{
				PokeLeftEye();
				return true;
			}
			if (GetCollider(cast) == ECollider.RightEye)
			{
				PokeRightEye();
				return true;
			}
			if (GetCollider(cast) == ECollider.Tummy)
			{
				PokeTummy();
				return true;
			}
		}
		return false;
	}

	private bool DetectTickling(string cast)
	{
		if (cast != null && AllInput.GetState(0) == AllInput.EState.Moved)
		{
			if (!m_TouchMoved)
			{
				m_TouchMoved = true;
			}
			if (cast != null && GetCollider(cast) == ECollider.Tummy && ++m_TickleCounter >= m_TickleMinFrame)
			{
				return true;
			}
		}
		return false;
	}

	private bool DetectRunning(string cast)
	{
		if (cast == null)
		{
			cast = RaycastUnderFinger(0);
		}
		if (AllInput.GetState(0) == AllInput.EState.Began)
		{
			m_RunDirection = EDirection.Count;
			m_RunCondition = false;
		}
		else if (m_CheckInput && AllInput.GetState(0) == AllInput.EState.Leave)
		{
			if (m_TouchMoved)
			{
				EDirection eDirection = DetectSlide();
				if ((eDirection == EDirection.Right || eDirection == EDirection.Left) && m_RunCondition && m_TouchTime < 0.9f)
				{
					switch (eDirection)
					{
					case EDirection.Left:
						m_RunDirection = EDirection.Left;
						break;
					case EDirection.Right:
						m_RunDirection = EDirection.Right;
						break;
					}
					return true;
				}
			}
		}
		else
		{
			if (cast == null)
			{
				return false;
			}
			if (AllInput.GetState(0) == AllInput.EState.Moved)
			{
				ECollider collider = GetCollider(cast);
				if (collider == ECollider.RightFoot)
				{
					m_RunCondition = true;
				}
				else if (GetCollider(cast) == ECollider.LeftFoot)
				{
					m_RunCondition = true;
				}
			}
		}
		return false;
	}

	private bool DetectString(string cast)
	{
		if (cast != null && AllInput.GetState(0) == AllInput.EState.Began && GetCollider(cast) == ECollider.String)
		{
			return true;
		}
		return false;
	}

	private bool DetectEar(string cast)
	{
		if (cast != null)
		{
			if (GetCollider(cast) == ECollider.LeftEar)
			{
				m_EarSide = ESide.Left;
				return true;
			}
			if (GetCollider(cast) == ECollider.RightEar)
			{
				m_EarSide = ESide.Right;
				return true;
			}
		}
		return false;
	}

	private bool DetectCircle()
	{
		Vector3 vector = m_FirstTouch;
		int num = 10000;
		Vector3 vector2 = m_FirstTouch;
		int num2 = 10000;
		Vector3 vector3 = m_FirstTouch;
		int num3 = 10000;
		Vector3 vector4 = m_FirstTouch;
		int num4 = 10000;
		int num5 = 0;
		foreach (Vector3 point in m_Points)
		{
			if (point.x > vector3.x)
			{
				vector3 = point;
				num3 = num5;
			}
			if (point.x < vector.x)
			{
				vector = point;
				num = num5;
			}
			if (point.y > vector2.y)
			{
				vector2 = point;
				num2 = num5;
			}
			if (point.y < vector4.y)
			{
				vector4 = point;
				num4 = num5;
			}
			num5++;
		}
		if (num3 == 10000)
		{
			vector3 = m_FirstTouch;
		}
		if (num == 10000)
		{
			vector = m_FirstTouch;
		}
		if (num2 == 10000)
		{
			vector2 = m_FirstTouch;
		}
		if (num4 == 10000)
		{
			vector4 = m_FirstTouch;
		}
		Vector3 vector6 = new Vector3(vector.x + (vector3.x - vector.x) / 2f, vector4.y + (vector2.y - vector4.y) / 2f, 0f);
		float num6 = Mathf.Abs(Vector3.Distance(vector6, m_FirstTouch));
		if (num6 < 30f)
		{
			return false;
		}
		float num7 = 0f;
		bool flag = false;
		float num8 = num6 - num6 * kRadiusVariancePercent;
		float num9 = num6 + num6 * kRadiusVariancePercent;
		num5 = 0;
		foreach (Vector3 point2 in m_Points)
		{
			float num10 = Mathf.Abs(Vector3.Distance(vector6, point2));
			if (num10 < num8 || num10 > num9)
			{
				return false;
			}
			float num11 = Utility.AngleBetweenLines(m_FirstTouch, vector6, point2, vector6);
			if (num11 > num7 && flag && (float)num5 < (float)m_Points.Count - kOverlapTolerance)
			{
				return false;
			}
			if (num11 < num7 && !flag)
			{
				flag = true;
			}
			num7 = num11;
			num5++;
		}
		return true;
	}

	private EDirection DetectSlide()
	{
		EDirection eDirection = EDirection.Count;
		if (m_Points.Count < 2)
		{
			return eDirection;
		}
		for (int i = 1; i < m_Points.Count; i++)
		{
			Vector3 dir = (Vector3)m_Points[i] - (Vector3)m_Points[i - 1];
			EDirection direction = GetDirection(dir);
			if (direction == EDirection.Count)
			{
				continue;
			}
			switch (eDirection)
			{
			case EDirection.Count:
				eDirection = direction;
				continue;
			case EDirection.Undefined:
				continue;
			}
			if (eDirection != direction)
			{
				eDirection = EDirection.Undefined;
			}
		}
		if (DephaseRabbid())
		{
			switch (eDirection)
			{
			case EDirection.Down:
				eDirection = EDirection.Up;
				break;
			case EDirection.Up:
				eDirection = EDirection.Down;
				break;
			case EDirection.Left:
				eDirection = EDirection.Right;
				break;
			case EDirection.Right:
				eDirection = EDirection.Left;
				break;
			}
		}
		return eDirection;
	}

	private void StartMediaPlayer()
	{
		/*MediaPlayerManager.mediaPlayerFinished += OnMediaPlayerFinised;
		MediaPlayerManager.mediaPlayerCancelled += OnMediaPlayerCancelled;*/
	}

	private void OnDisableMediaPlayer()
	{
		/*MediaPlayerManager.mediaPlayerFinished -= OnMediaPlayerFinised;
		MediaPlayerManager.mediaPlayerCancelled -= OnMediaPlayerCancelled;*/
	}

	private void OnMediaPlayerFinised(int count)
	{
		OnEndMediaPicker();
	}

	private void OnMediaPlayerCancelled()
	{
		OnEndMediaPicker();
	}

	private void OnEndMediaPicker()
	{
		Screen.orientation = m_MemOrientation;
	}

	private void ShowMediaPicker()
	{
		StopSound();
		if (m_UseMediaPlayer)
		{
			m_MemOrientation = Screen.orientation;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			StopListener();
			//MediaPlayerBinding.showMediaPicker();
		}
	}

	private bool IsMediaPlaying()
	{
		return false;
	}

	private void StartMiniGames()
	{
		m_MiniGameShadow1 = Utility.CreateShadow();
		m_MiniGameShadow2 = Utility.CreateShadow();
		if (m_MiniGameShadow1 != null && m_MiniGameShadow2 != null)
		{
			m_FakeShadow1 = m_MiniGameShadow1.GetComponent<FakeShadow>();
			m_FakeShadow2 = m_MiniGameShadow2.GetComponent<FakeShadow>();
			if (m_FakeShadow1 == null || m_FakeShadow2 == null)
			{
				Utility.Log(ELog.Errors, "FakeShadow script is missing on shadow object");
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Shadow not found into Resources");
		}
		AttachMiniGameObjectToShadow1(null);
		AttachMiniGameObjectToShadow2(null);
		m_Anchor = new GameObject("MiniGamesAnchor");
		m_Anchor.transform.localPosition = new Vector3(0f, -200f, 0f);
		if (lockedStates != null && randomStates != null && unlockedStates != null && currentStates != null && tutorialsStates != null)
		{
		}
	}

	private void UpdateMiniGames()
	{
		if (!Utility.IsCheater())
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.U))
		{
			for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.AvailableCount; eProductID++)
			{
				UnlockPack(eProductID);
			}
		}
		if (Utility.InputStoppedByActivity() || !m_InputValidity || (AllInput.GetTouchCount() != 3 && !Input.GetKeyDown(KeyCode.T)))
		{
			return;
		}
		int num = 0;
		m_CurrentPack = EPack.Count;
		for (EProductID eProductID2 = EProductID.historypack; eProductID2 < EProductID.AvailableCount; eProductID2++)
		{
			UnlockPack(eProductID2);
		}
		while (m_LockedStates.Count > 0)
		{
			EState eState = m_LockedStates[0];
			UnlockState(m_LockedStates[0], num, false);
			if (m_LockedStates.Count > 0)
			{
				num = ((eState == m_LockedStates[0]) ? (num + 1) : 0);
			}
		}
		if (m_AchievementsTable != null)
		{
			m_AchievementsTable.LoadTable();
		}
		RecreateMiniGameThumbnailsBuffer();
		GlobalVariables.ComputeScore(true);
		FillPublicArrays();
	}

	private void LateUpdateMiniGames()
	{
	}

	private void MiniGamesGUI()
	{
	}

	private void ClownBox_LoadGameObject()
	{
		bool flag = m_ClownBox == null;
		m_ClownBox = LoadMiniGameObject("MiniGames/ClownBox");
		if (!flag || !(m_ClownBox != null))
		{
			return;
		}
		List<Renderer> list = new List<Renderer>();
		List<Renderer> list2 = new List<Renderer>();
		List<Renderer> list3 = new List<Renderer>();
		List<Renderer> list4 = new List<Renderer>();
		List<Renderer> list5 = new List<Renderer>();
		Transform transform = m_ClownBox.transform.Find("dummy_principal_boite01/accordeon");
		if (transform != null)
		{
			list.Add(transform.GetComponent<Renderer>());
			transform = transform.Find("body_low02");
			if (transform != null)
			{
				list5.Add(transform.GetComponent<Renderer>());
				Transform transform2 = transform.Find("B_L_eye01");
				if (transform2 != null)
				{
					list5.Add(transform2.GetComponent<Renderer>());
				}
				transform2 = transform.Find("B_L_upLid01");
				if (transform2 != null)
				{
					list4.Add(transform2.GetComponent<Renderer>());
				}
				transform2 = transform.Find("B_R_eye01");
				if (transform2 != null)
				{
					list5.Add(transform2.GetComponent<Renderer>());
				}
				transform2 = transform.Find("B_R_upLid01");
				if (transform2 != null)
				{
					list4.Add(transform2.GetComponent<Renderer>());
				}
				transform2 = transform.Find("Cone01");
				if (transform2 != null)
				{
					list3.Add(transform2.GetComponent<Renderer>());
				}
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to find dummy_principal_boite01/accordeon");
		}
		Transform transform3 = m_ClownBox.transform.Find("dummy_principal_boite01/boite");
		if (transform3 != null)
		{
			list2.Add(transform3.GetComponent<Renderer>());
			for (int i = 0; i < transform3.childCount; i++)
			{
				list4.Add(transform3.GetChild(i).GetComponent<Renderer>());
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to find dummy_principal_boite01/boite");
		}
		ApplyTexture(list, "MiniGames/ClownBox/Accordion");
		ApplyTexture(list2, "MiniGames/ClownBox/Box");
		ApplyTexture(list3, "MiniGames/ClownBox/CrazyHat");
		ApplyTexture(list4, "MiniGames/ClownBox/BoxCover");
		ApplyTexture(list5, "Costumes/Skins/BunnyNaked");
	}

	private void ApplyTexture(List<Renderer> rnds, string texPath)
	{
		if (rnds.Count <= 0)
		{
			return;
		}
		Texture2D mainTexture = Utility.LoadTextureResource<Texture2D>(texPath);
		foreach (Renderer rnd in rnds)
		{
			rnd.material.mainTexture = mainTexture;
		}
	}

	private void OnEnterClownBox()
	{
		m_AnimBis = false;
		ClownBox_LoadSounds();
		ClownBox_CreateAnimationEvents();
		ClownBox_LoadGameObject();
		AttachMiniGameObjectToShadow1(m_ClownBox);
		RewindAnim("gift01");
		PlayAnim("gift01");
		m_LoopIdleIndex = 0;
	}

	private void OnUpdateClownBox()
	{
		if (base.animIsPlaying && m_CurrentAnimation != "choke")
		{
			float time = base.GetComponent<Animation>()[m_CurrentAnimation].time;
			if (time < 1f)
			{
				return;
			}
			if (time < 6.6f)
			{
				if (!m_CheckInput || AllInput.GetTouchCount() != 1 || AllInput.GetState(0) != AllInput.EState.Leave)
				{
					return;
				}
				string cast = RaycastUnderFinger(0);
				if (GetCollider(cast) == ECollider.ClownBox)
				{
					string text = "gift01";
					if (!m_AnimBis)
					{
						text += "_bis";
					}
					m_AnimBis = !m_AnimBis;
					time = 7f;
					SetAnimTime(text, time);
					PlayAnim(text);
					UnlockState(EState.ClownBox, 0);
				}
			}
			else if (time < 6.99f)
			{
				string text2 = "gift01";
				if (!m_AnimBis)
				{
					text2 += "_bis";
				}
				m_AnimBis = !m_AnimBis;
				time = 2.45f;
				SetAnimTime(text2, time);
				PlayAnim(text2);
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveClownBox()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_ClownBox = UnloadMiniGameObject(m_ClownBox);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseClownBox(bool pauseResume)
	{
		PauseAnimatedObject(m_ClownBox, pauseResume);
	}

	private void ClownBox_LoadSounds()
	{
	}

	private void ClownBox_CreateAnimationEvents()
	{
		AnimationEvent animationEvent = new AnimationEvent();
		AnimationEvent animationEvent2 = new AnimationEvent();
		AnimationEvent animationEvent3 = new AnimationEvent();
		AnimationEvent animationEvent4 = new AnimationEvent();
		AnimationEvent animationEvent5 = new AnimationEvent();
		AnimationEvent animationEvent6 = new AnimationEvent();
		animationEvent.functionName = "PlayGift1SoundBegin";
		animationEvent2.functionName = "PlayGift1SoundMiddle";
		animationEvent2.time = 2.28f;
		animationEvent3.functionName = "PlayGift1SoundLoop";
		animationEvent3.time = 6.8f;
		animationEvent4.functionName = "PlayGift1SoundEnd";
		animationEvent4.time = 7f;
		animationEvent6.functionName = "ActivateShadow1";
		animationEvent6.intParameter = 0;
		animationEvent6.time = 15.3f;
		if (AddAnimationClip("BunnyMiniGames/ClownBox/", "gift01"))
		{
			base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent);
			base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent2);
			base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent3);
			base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent4);
			base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent6);
		}
		if (AddAnimationClip("BunnyMiniGames/ClownBox/", "gift01_bis"))
		{
			base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent);
			base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent2);
			base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent3);
			base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent4);
			base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent6);
		}
		if (AddAnimationClip("BunnyInteractions/Common/", "choke"))
		{
			CreateAnimationEvent("choke", "PlayChokeSound", 0);
		}
		animationEvent5.functionName = "StartChoke";
		animationEvent5.time = (float)m_TimeAnimTable["gift01"] - 0.5f;
		base.GetComponent<Animation>()["gift01"].clip.AddEvent(animationEvent5);
		base.GetComponent<Animation>()["gift01_bis"].clip.AddEvent(animationEvent5);
	}

	private void YellowDuck_LoadGameObject()
	{
		bool flag = m_DuckFace == null;
		bool flag2 = m_DuckHand == null;
		Texture2D mainTexture = null;
		if (flag || flag2)
		{
			mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/YellowDuck/YellowDuck");
		}
		m_DuckFace = LoadMiniGameObject("MiniGames/DuckFace");
		if (flag && m_DuckFace != null)
		{
			Renderer componentInChildren = m_DuckFace.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				componentInChildren.material.mainTexture = mainTexture;
			}
		}
		m_DuckHand = LoadMiniGameObject("MiniGames/DuckHand");
		if (flag2 && m_DuckHand != null)
		{
			Renderer componentInChildren2 = m_DuckHand.GetComponentInChildren<Renderer>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterYellowDuck()
	{
		m_AnimBis = false;
		YellowDuck_LoadSounds();
		YellowDuck_CreateAnimationEvents();
		YellowDuck_LoadGameObject();
		string text = "B_Point02/B_Bassin/B_Bassin01/B_Bassin02/B_Point09/B_Bras_GA/B_Bras_GA2/duckhand";
		Transform transform = m_DuckHand.transform.Find(text);
		if (transform != null)
		{
			AttachMiniGameObjectToShadow1(transform.gameObject);
		}
		else
		{
			Utility.Log(ELog.Errors, text + " not found");
		}
		AttachMiniGameObjectToShadow2(m_DuckFace);
		RewindAnim("gift02");
		PlayAnim("gift02");
		m_LoopIdleIndex = 0;
	}

	private void OnUpdateYellowDuck()
	{
		if (base.animIsPlaying)
		{
			float time = base.GetComponent<Animation>()[m_CurrentAnimation].time;
			if (time < 1f)
			{
				return;
			}
			if (time < 4f)
			{
				if (!m_CheckInput || AllInput.GetTouchCount() != 1 || AllInput.GetState(0) != AllInput.EState.Leave)
				{
					return;
				}
				string cast = RaycastUnderFinger(0);
				if (GetCollider(cast) == ECollider.YellowDuck)
				{
					string text = "gift02";
					if (!m_AnimBis)
					{
						text += "_bis";
					}
					m_AnimBis = !m_AnimBis;
					time = 4.25f;
					SetAnimTime(text, time);
					PlayAnim(text);
					m_CurrentAnimTime = time;
					currentAnimTime = time;
					PlayGift2SoundThrow();
					UnlockState(EState.YellowDuck, 0);
				}
			}
			else if (time < 4.2f)
			{
				string text2 = "gift02";
				if (!m_AnimBis)
				{
					text2 += "_bis";
				}
				m_AnimBis = !m_AnimBis;
				time = 1.9f;
				SetAnimTime(text2, time);
				PlayAnim(text2);
				m_CurrentAnimTime = time;
				currentAnimTime = time;
			}
			else if (time > 5.85f && time < 11.5f && !DetectInteractions())
			{
			}
		}
		else if (!base.animIsPlaying)
		{
			UnlockState(EState.YellowDuck, 1);
			SetState(EState.Idle);
		}
	}

	private void OnLeaveYellowDuck()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		AttachMiniGameObjectToShadow2(null);
		m_DuckFace = UnloadMiniGameObject(m_DuckFace);
		m_DuckHand = UnloadMiniGameObject(m_DuckHand);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseYellowDuck(bool pauseResume)
	{
		PauseAnimatedObject(m_DuckFace, pauseResume);
		PauseAnimatedObject(m_DuckHand, pauseResume);
	}

	private void YellowDuck_LoadSounds()
	{
	}

	private void YellowDuck_PlayBwahASound()
	{
		if (PlayCallBack("YellowDuck_PlayBwahASound"))
		{
			PlaySound(EBwahSound.Bwah_A);
		}
	}

	private void YellowDuck_PlayBwahISound()
	{
		if (PlayCallBack("YellowDuck_PlayBwahISound"))
		{
			PlaySound(EBwahSound.Bwah_I);
		}
	}

	private void YellowDuck_PlayChocMatSound()
	{
		if (PlayCallBack("YellowDuck_PlayChocMatSound"))
		{
			LoadSound("Common/", ESound.ChocMat_a);
			PlaySound(ESound.ChocMat_a);
		}
	}

	private void YellowDuck_CreateAnimationEvents()
	{
		AnimationEvent animationEvent = new AnimationEvent();
		AnimationEvent animationEvent2 = new AnimationEvent();
		AnimationEvent animationEvent3 = new AnimationEvent();
		animationEvent.functionName = "PlayGift2SoundBegin";
		animationEvent.time = 0f;
		animationEvent2.functionName = "PlayGift2SoundMiddle";
		animationEvent2.time = 6.5f;
		animationEvent3.functionName = "PlayGift2SoundEnd";
		animationEvent3.time = 11.5f;
		if (AddAnimationClip("BunnyMiniGames/YellowDuck/", "gift02"))
		{
			base.GetComponent<Animation>()["gift02"].clip.AddEvent(animationEvent);
			base.GetComponent<Animation>()["gift02"].clip.AddEvent(animationEvent2);
			base.GetComponent<Animation>()["gift02"].clip.AddEvent(animationEvent3);
		}
		if (AddAnimationClip("BunnyMiniGames/YellowDuck/", "gift02_bis"))
		{
			base.GetComponent<Animation>()["gift02_bis"].clip.AddEvent(animationEvent);
			base.GetComponent<Animation>()["gift02_bis"].clip.AddEvent(animationEvent2);
			base.GetComponent<Animation>()["gift02_bis"].clip.AddEvent(animationEvent3);
		}
	}

	private void OnEnterNight()
	{
		Night_LoadSounds();
		if (m_NightScript != null)
		{
			m_NightScript.EnableNight();
		}
		if (m_CostumeRenderer != null)
		{
			m_CostumeRenderer.enabled = false;
		}
		ActivateShadow(false);
	}

	private void OnUpdateNight()
	{
		if (m_NightScript != null)
		{
			m_NightScript.UpdateNight();
			if (m_CheckInput && AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Leave && m_StateTime > 0.2f)
			{
				m_NightScript.SwitchOffNight();
				UnlockState(EState.Night, 0);
				m_ForceIdle = true;
				m_ForceIdleAnim = true;
				StopSound();
			}
		}
	}

	private void OnLeaveNight()
	{
		ClearAdditionalAnimationClips();
		if (m_NightScript != null)
		{
			m_NightScript.DisableNight();
		}
		if (m_CostumeRenderer != null)
		{
			m_CostumeRenderer.enabled = true;
		}
		ActivateShadow(true);
	}

	private void OnPauseNight(bool pauseResume)
	{
		if (m_NightScript != null)
		{
			m_NightScript.PauseNight(pauseResume);
		}
	}

	private void Night_LoadSounds()
	{
	}

	private void OnEnterSteam()
	{
		Steam_LoadSounds();
		Steam_CreateAnimationEvents();
		PlayAnim("steam");
		RenderSettings.fogColor = new Color(0.792f, 0.792f, 0.792f, 1f);
		m_SteamFirstWiping = true;
	}

	private void OnUpdateSteam()
	{
		bool flag = false;
		if (RenderSettings.fog)
		{
			if (AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Moved)
			{
				flag = true;
				if (m_SteamFirstWiping)
				{
					LoadSound("Izi/", ESound.Human_Clean_window_loop);
					PlaySound(ESound.Human_Clean_window_loop);
					m_SteamFirstWiping = false;
				}
			}
			if (m_StateTime > m_MaxSteamTime)
			{
				flag = true;
			}
			if (RenderSettings.fogDensity <= 0.5f)
			{
				flag = true;
			}
		}
		else if (DetectInteractions())
		{
			return;
		}
		if (!flag)
		{
			return;
		}
		RenderSettings.fogDensity -= Time.deltaTime;
		if (RenderSettings.fogDensity <= 0.15f)
		{
			RenderSettings.fog = false;
			if (m_StateTime < m_MaxSteamTime)
			{
				UnlockState(EState.Steam, 0);
			}
			SetState(EState.Idle);
		}
	}

	private void OnLeaveSteam()
	{
		ClearAdditionalAnimationClips();
		if (RenderSettings.fog)
		{
			RenderSettings.fog = false;
			RenderSettings.fogDensity = 0f;
		}
		ActivateShadow(true);
	}

	private void OnPauseSteam(bool pauseResume)
	{
	}

	private void Steam_LoadSounds()
	{
	}

	private void Steam_CreateAnimationEvents()
	{
		if (AddAnimationClip("BunnyMiniGames/Steam/", "steam"))
		{
			CreateAnimationEvent("steam", "PlaySteamSound", 0);
			CreateAnimationEvent("steam", "StartSteam", 90);
		}
	}

	private void Piranha_LoadGameObject()
	{
		bool flag = m_Piranha == null;
		bool flag2 = m_PiranhaBowl == null;
		m_Piranha = LoadMiniGameObject("MiniGames/Piranha");
		if (flag && m_Piranha != null)
		{
			Renderer componentInChildren = m_Piranha.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				componentInChildren.material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Piranha/Piranha");
			}
		}
		m_PiranhaBowl = LoadMiniGameObject("MiniGames/PiranhaBowl");
		if (!flag2 || !(m_PiranhaBowl != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_PiranhaBowl.GetComponentsInChildren<Renderer>();
		if (componentsInChildren == null)
		{
			return;
		}
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			if (renderer != null)
			{
				renderer.material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Piranha/Piranha" + renderer.gameObject.name);
			}
		}
	}

	private void OnEnterPiranha()
	{
		Piranha_LoadSounds();
		Piranha_CreateAnimationEvents();
		Piranha_LoadGameObject();
		Piranha_CreateAndPlayStart();
		Transform transform = m_Piranha.transform.Find("Point01");
		if (transform != null)
		{
			AttachMiniGameObjectToShadow1(transform.gameObject);
		}
		else
		{
			Utility.Log(ELog.Errors, "Piranha/Point01 not found");
		}
	}

	private void OnUpdatePiranha()
	{
		bool flag = false;
		float time = base.GetComponent<Animation>()[m_CurrentAnimation].time;
		if (m_CurrentAnimation == "Piranha_Start")
		{
			if (!base.animIsPlaying)
			{
				Piranha_CreateAndPlayIdleLoop();
			}
			else if (time > m_CurrentAnimLength / 2f)
			{
				flag = true;
			}
		}
		else if (m_CurrentAnimation == "Piranha_IdleLoop")
		{
			flag = true;
		}
		else if (m_CurrentAnimation == "Piranha_Jump")
		{
			if (!base.animIsPlaying)
			{
				Piranha_CreateAndPlayIdleLoop();
			}
			else if (m_CheckInput && AllInput.GetState(0) == AllInput.EState.Leave)
			{
				UnlockState(EState.Piranha, 0);
				Piranha_CreateAndPlayScale();
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
			return;
		}
		if (!m_CheckInput || !flag)
		{
			return;
		}
		if (AllInput.GetState(0) == AllInput.EState.Leave)
		{
			switch (DetectSlide())
			{
			case EDirection.Up:
				Piranha_CreateAndPlayJump();
				break;
			case EDirection.Down:
				UnlockState(EState.Piranha, 1);
				Piranha_CreateAndPlayHeadInTank();
				break;
			}
		}
		else
		{
			if (AllInput.GetTouchCount() != 1)
			{
				return;
			}
			EDirection eDirection = DetectSlide();
			if (eDirection == EDirection.Left || eDirection == EDirection.Right)
			{
				List<ECollider> list = RaycastAllUnderFinger(0);
				if (list.Contains(ECollider.RightFoot) || list.Contains(ECollider.LeftFoot) || list.Contains(ECollider.Piranha))
				{
					UnlockState(EState.Piranha, 2);
					Piranha_CreateAndPlayBite();
				}
			}
		}
	}

	private void OnLeavePiranha()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_Piranha = UnloadMiniGameObject(m_Piranha);
		m_PiranhaBowl = UnloadMiniGameObject(m_PiranhaBowl);
	}

	private void OnPausePiranha(bool pauseResume)
	{
		PauseAnimatedObject(m_Piranha, pauseResume);
		PauseAnimatedObject(m_PiranhaBowl, pauseResume);
	}

	private void Piranha_PlayClapSound()
	{
		if (PlayCallBack("Piranha_PlayClapSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_Clap_b);
			PlaySound(ESound.Piranha_Clap_b);
		}
	}

	private void Piranha_PlayBiteSound()
	{
		if (PlayCallBack("Piranha_PlayBiteSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_Clap_c);
			PlaySound(ESound.Piranha_Clap_c);
		}
	}

	private void Piranha_PlayWaterSound()
	{
		if (PlayCallBack("Piranha_PlayWaterSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_Water);
			PlaySound(ESound.Piranha_Water);
		}
	}

	private void Piranha_PlayFishFallSound()
	{
		if (PlayCallBack("Piranha_PlayFishFallSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_FishFall);
			PlaySound(ESound.Piranha_FishFall, 0.5f);
		}
	}

	private void Piranha_PlayGlassSplashSound()
	{
		if (PlayCallBack("Piranha_PlayGlassSplashSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_GlassSplash);
			PlaySound(ESound.Piranha_GlassSplash);
		}
	}

	private void Piranha_PlayScaleDanceSound()
	{
		if (PlayCallBack("Piranha_PlayScaleDanceSound"))
		{
			LoadSound("Piranha/", ESound.IZI_Waddle);
			PlaySound(ESound.IZI_Waddle);
		}
	}

	private void Piranha_PlayFishedBwahSound()
	{
		if (PlayCallBack("Piranha_PlayFishedBwahSound"))
		{
			LoadSound("Piranha/", ESound.Piranha_FishedBwah);
			PlaySound(ESound.Piranha_FishedBwah);
		}
	}

	private void Piranha_PlayRabbidShot()
	{
		if (PlayCallBack("Piranha_PlayRabbidShot"))
		{
			LoadSound("Piranha/", ESound.Piranha_RabbidShot);
			PlaySound(ESound.Piranha_RabbidShot);
		}
	}

	private void Piranha_LoadSounds()
	{
	}

	private void Piranha_CreateAndPlayScale()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_Scale"))
		{
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayBiteSound", 20);
			CreateAnimationEvent("Piranha_Scale", "PlayBigBwahSound", 45);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 45);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 54);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 63);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 72);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 81);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayFishFallSound", 90);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayRabbidShot", 112);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayGlassSplashSound", 115);
			CreateAnimationEvent("Piranha_Scale", "Piranha_PlayScaleDanceSound", 145);
		}
		PlayAnim("Piranha_Scale");
	}

	private void Piranha_CreateAndPlayHeadInTank()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_HeadInTank"))
		{
			CreateAnimationEvent("Piranha_HeadInTank", "Piranha_PlayFishFallSound", 15);
			CreateAnimationEvent("Piranha_HeadInTank", "PlayBigShortBwahSound", 90);
			CreateAnimationEvent("Piranha_HeadInTank", "Piranha_PlayFishFallSound", 142);
		}
		PlayAnim("Piranha_HeadInTank");
	}

	private void Piranha_CreateAndPlayJump()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_Jump"))
		{
			CreateAnimationEvent("Piranha_Jump", "Piranha_PlayWaterSound", 36);
		}
		PlayAnim("Piranha_Jump");
	}

	private void Piranha_CreateAndPlayIdleLoop()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_IdleLoop"))
		{
		}
		PlayAnim("Piranha_IdleLoop");
	}

	private void Piranha_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_Start"))
		{
			CreateAnimationEvent("Piranha_Start", "PlayGift1SoundBegin", 0.3f);
			CreateAnimationEvent("Piranha_Start", "Piranha_PlayWaterSound", 67);
		}
		RewindAnim("Piranha_Start");
		PlayAnim("Piranha_Start");
	}

	private void Piranha_CreateAndPlayBite()
	{
		if (AddAnimationClip("BunnyMiniGames/Piranha/", "Piranha_Bite"))
		{
			CreateAnimationEvent("Piranha_Bite", "PlayBigBwahSound", 30);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayClapSound", 88);
			CreateAnimationEvent("Piranha_Bite", "PlayBigBwahSound", 100);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 20);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 36);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 45);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 54);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 63);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 72);
			CreateAnimationEvent("Piranha_Bite", "Piranha_PlayFishFallSound", 81);
		}
		PlayAnim("Piranha_Bite");
	}

	private void Piranha_CreateAnimationEvents()
	{
	}

	private void Swatter_LoadGameObject()
	{
		bool flag = m_Swatter == null;
		m_Swatter = LoadMiniGameObject("MiniGames/Swatter");
		if (!flag || !(m_Swatter != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Swatter.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Swatter/Swatter");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterSwatter()
	{
		Swatter_LoadSounds();
		Swatter_CreateAnimationEvents();
		Swatter_LoadGameObject();
		Swatter_CreateAndPlayStart();
		m_SlideOnSwatter = false;
	}

	private void OnUpdateSwatter()
	{
		float time = base.GetComponent<Animation>()[m_CurrentAnimation].time;
		if (m_CurrentAnimation == "Swatter_Start")
		{
			if (!base.animIsPlaying)
			{
				Swatter_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Swatter_LoopIdle")
		{
			if (AllInput.GetTouchCount() == 1 && !m_SlideOnSwatter)
			{
				string cast = RaycastUnderFinger(0);
				ECollider collider = GetCollider(cast);
				if (collider == ECollider.Swatter)
				{
					m_SlideOnSwatter = true;
				}
			}
			if (!m_CheckInput || AllInput.GetState(0) != AllInput.EState.Leave)
			{
				return;
			}
			if (!m_TouchMoved)
			{
				string cast2 = RaycastUnderFinger(0);
				ECollider collider2 = GetCollider(cast2);
				if (collider2 == ECollider.Swatter)
				{
					UnlockState(EState.Swatter, 0);
					Swatter_CreateAndPlayHappyEnd();
					Swatter_PlayClapSound();
				}
			}
			else
			{
				EDirection eDirection = DetectSlide();
				if (eDirection != EDirection.Count && m_SlideOnSwatter)
				{
					LoadPanel();
					EnablePanel(EPanel.Bwwaaah, "Swatter_BwaahEnd", m_Panel);
					UnlockState(EState.Swatter, 1);
					Swatter_CreateAndPlayBwaahEnd();
				}
			}
		}
		else if (m_CurrentAnimation == "Swatter_HappyEnd")
		{
			if (!base.animIsPlaying || time > 9.3f)
			{
				StopAnim();
				SetState(EState.Idle);
			}
		}
		else if (m_CurrentAnimation == "Swatter_BwaahEnd" && !base.animIsPlaying)
		{
			DisablePanel();
			SetState(EState.Idle);
		}
	}

	private void OnLeaveSwatter()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_Panel = UnloadMiniGameObject(m_Panel);
		m_Swatter = UnloadMiniGameObject(m_Swatter);
		DisablePanel();
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseSwatter(bool pauseResume)
	{
		PauseAnimatedObject(m_Panel, pauseResume);
		PauseAnimatedObject(m_Swatter, pauseResume);
	}

	private void Swatter_PlayClapSound()
	{
		if (PlayCallBack("Swatter_PlayClapSound"))
		{
			switch (Random.Range(0, 3))
			{
			case 0:
				LoadSound("Swatter/", ESound.Swatter_Clap_a);
				PlaySound(ESound.Swatter_Clap_a);
				break;
			case 1:
				LoadSound("Swatter/", ESound.Swatter_Clap_b);
				PlaySound(ESound.Swatter_Clap_b);
				break;
			default:
				LoadSound("Swatter/", ESound.Swatter_Clap_c);
				PlaySound(ESound.Swatter_Clap_c);
				break;
			}
		}
	}

	private void Swatter_PlayHappyEndSound()
	{
		if (PlayCallBack("Swatter_PlayHappyEndSound"))
		{
			LoadSound("Izi/", ESound.IZI_Tickle_step2_loop2);
			PlaySound(ESound.IZI_Tickle_step2_loop2);
		}
	}

	private void Swatter_PlayBwooh1Sound()
	{
		if (PlayCallBack("Swatter_PlayBwooh1Sound"))
		{
			PlaySound(ERabbidSound.Bun_Collect_XS_get1);
		}
	}

	private void Swatter_PlayBwooh2Sound()
	{
		if (PlayCallBack("Swatter_PlayBwooh2Sound"))
		{
			PlaySound(ERabbidSound.Bun_Collect_XS_get3);
		}
	}

	private void Swatter_PlayBwaahEndSound()
	{
		if (PlayCallBack("Swatter_PlayBwaahEndSound"))
		{
			LoadSound("Swatter/", ESound.Swatter_StifflingYell);
			PlaySound(ESound.Swatter_StifflingYell);
		}
	}

	private void Swatter_LoadSounds()
	{
	}

	private void Swatter_CreateAndPlayBwaahEnd()
	{
		if (AddAnimationClip("BunnyMiniGames/Swatter/", "Swatter_BwaahEnd"))
		{
			CreateAnimationEvent("Swatter_BwaahEnd", "Swatter_PlayBwooh1Sound", 1.1f);
			CreateAnimationEvent("Swatter_BwaahEnd", "Swatter_PlayBwooh2Sound", 1.8f);
			CreateAnimationEvent("Swatter_BwaahEnd", "Swatter_PlayClapSound", 2.6f);
			CreateAnimationEvent("Swatter_BwaahEnd", "Swatter_PlayBwaahEndSound", 3.1f);
			CreateAnimationEvent("Swatter_BwaahEnd", "EgyptianToiletPaper_PlayChocMatSound", 190);
			CreateAnimationEvent("Swatter_BwaahEnd", "EgyptianToiletPaper_PlayChocMatSound", 220);
		}
		RewindAnim("Swatter_BwaahEnd");
		PlayAnim("Swatter_BwaahEnd");
	}

	private void Swatter_CreateAndPlayHappyEnd()
	{
		if (AddAnimationClip("BunnyMiniGames/Swatter/", "Swatter_HappyEnd"))
		{
			CreateAnimationEvent("Swatter_HappyEnd", "Swatter_PlayHappyEndSound", 1f);
		}
		RewindAnim("Swatter_HappyEnd");
		PlayAnim("Swatter_HappyEnd");
	}

	private void Swatter_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Swatter/", "Swatter_LoopIdle"))
		{
			CreateAnimationEvent("Swatter_LoopIdle", "PlayBwahSound", 90);
		}
		PlayAnim("Swatter_LoopIdle");
	}

	private void Swatter_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Swatter/", "Swatter_Start"))
		{
			CreateAnimationEvent("Swatter_Start", "PlayGift1SoundBegin", 2.8f);
		}
		PlayAnim("Swatter_Start");
	}

	private void Swatter_CreateAnimationEvents()
	{
	}

	private void WCBrush_LoadGameObject()
	{
		bool flag = m_WCBrush == null;
		m_WCBrush = LoadMiniGameObject("MiniGames/WCBrush");
		if (!flag || !(m_WCBrush != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_WCBrush.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/WCBrush/WCBrush");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterWCBrush()
	{
		WCBrush_LoadSounds();
		WCBrush_CreateAnimationEvents();
		WCBrush_LoadGameObject();
		WCBrush_CreateAndPlayStart();
		m_WCBrushGrible = false;
	}

	private void OnUpdateWCBrush()
	{
		bool flag = false;
		if (m_CurrentAnimation == "WCBrush_Start")
		{
			if (!base.animIsPlaying)
			{
				WCBrush_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "WCBrush_LoopIdle")
		{
			if (m_CheckInput && AllInput.GetTouchCount() == 1)
			{
				if (AllInput.GetState(0) == AllInput.EState.Leave)
				{
					switch (DetectSlide())
					{
					case EDirection.Up:
						WCBrush_CreateAndPlayEyeLashBrushing();
						UnlockState(EState.WCBrush, 2);
						return;
					case EDirection.Right:
					case EDirection.Left:
						WCBrush_CreateAndPlayTeethBrushing();
						UnlockState(EState.WCBrush, 3);
						return;
					case EDirection.Count:
					{
						string cast = RaycastUnderFinger(0);
						switch (GetCollider(cast))
						{
						case ECollider.LeftEar:
						case ECollider.RightEar:
							UnlockState(EState.WCBrush, 1);
							WCBrush_CreateAndPlayEarBrushing();
							return;
						case ECollider.Tummy:
						case ECollider.String:
							if (m_TouchMoved)
							{
								flag = true;
							}
							break;
						}
						break;
					}
					}
				}
				else if (AllInput.GetState(0) == AllInput.EState.Moved)
				{
					string cast2 = RaycastUnderFinger(0);
					ECollider collider = GetCollider(cast2);
					ECollider eCollider = collider;
					if ((eCollider == ECollider.Tummy || eCollider == ECollider.String) && m_TouchMoved)
					{
						m_WCBrushGrible = true;
					}
					if (m_WCBrushGrible)
					{
						EDirection eDirection = DetectSlide();
						if (eDirection == EDirection.Count)
						{
							flag = true;
						}
					}
				}
			}
		}
		else if ((m_CurrentAnimation == "WCBrush_EarBrushing" || m_CurrentAnimation == "WCBrush_ShowerBrush" || m_CurrentAnimation == "WCBrush_EyeLashBrushing" || m_CurrentAnimation == "WCBrush_TeethBrushing") && !base.animIsPlaying)
		{
			SetState(EState.Idle);
			return;
		}
		if (flag)
		{
			WCBrush_CreateAndPlayShowerBrush();
			UnlockState(EState.WCBrush, 0);
		}
	}

	private void OnLeaveWCBrush()
	{
		ClearAdditionalAnimationClips();
		m_WCBrush = UnloadMiniGameObject(m_WCBrush);
		AttachMiniGameObjectToShadow1(null);
	}

	private void OnPauseWCBrush(bool pauseResume)
	{
		PauseAnimatedObject(m_WCBrush, pauseResume);
	}

	private void WCBrush_PlayStartSound1()
	{
		if (PlayCallBack("WCBrush_PlayStartSound1"))
		{
			PlaySound(ERabbidSound.Bun_Collect_XS_get4);
		}
	}

	private void WCBrush_PlayStartSound2()
	{
		if (PlayCallBack("WCBrush_PlayStartSound2"))
		{
			PlaySound(ERabbidSound.Bun_Collect_XS_get2);
		}
	}

	private void WCBrush_PlayStartSound3()
	{
		if (PlayCallBack("WCBrush_PlayStartSound3"))
		{
			PlaySound(ERabbidSound.Bun_Collect_XS_get1);
		}
	}

	private void WCBrush_PlayBodyBrushSound1()
	{
		if (PlayCallBack("WCBrush_PlayBodyBrushSound1"))
		{
			LoadSound("WCBrush/", ESound.WCBrush_BodyBrush);
			PlaySound(ESound.WCBrush_BodyBrush);
		}
	}

	private void WCBrush_PlayBodyBrushSound2()
	{
		if (PlayCallBack("WCBrush_PlayBodyBrushSound2"))
		{
			PlaySound(ERabbidSound.Burp);
		}
	}

	private void WCBrush_PlayEarBrushingSound1()
	{
		if (PlayCallBack("WCBrush_PlayEarBrushingSound1"))
		{
			PlaySound(ERabbidSound.Bun_StandBy_ScratchAss);
		}
	}

	private void WCBrush_PlayEarRemovalSound()
	{
		if (PlayCallBack("WCBrush_PlayEarRemovalSound"))
		{
			LoadSound("WCBrush/", ESound.WCBrush_EarRemoval);
			PlaySound(ESound.WCBrush_EarRemoval);
		}
	}

	private void WCBrush_PlayEarBrushingSound2()
	{
		if (PlayCallBack("WCBrush_PlayEarBrushingSound2"))
		{
			PlaySound(ERabbidSound.Bun_Grab_action3);
		}
	}

	private void WCBrush_PlayEyeLashBrushingSound()
	{
		if (PlayCallBack("WCBrush_PlayEyeLashBrushingSound"))
		{
			LoadSound("WCBrush/", ESound.WCBrush_EyeLashBrush);
			PlaySound(ESound.WCBrush_EyeLashBrush);
		}
	}

	private void WCBrush_PlayTeethBrushingSound()
	{
		if (PlayCallBack("WCBrush_PlayTeethBrushingSound"))
		{
			LoadSound("WCBrush/", ESound.WCBrush_TeethBrush);
			PlaySound(ESound.WCBrush_TeethBrush);
		}
	}

	private void WCBrush_PlayScratchSound()
	{
		if (PlayCallBack("WCBrush_PlayScratchSound"))
		{
			LoadSound("WCBrush/", ESound.WCBrush_Scratch);
			PlaySound(ESound.WCBrush_Scratch);
		}
	}

	private void WCBrush_PlayWooshSound()
	{
		if (PlayCallBack("WCBrush_PlayWooshSound"))
		{
			LoadSound("Guitar/", ESound.Woosh);
			PlaySound(ESound.Woosh);
		}
	}

	private void WCBrush_LoadSounds()
	{
	}

	private void WCBrush_CreateAndPlayEarBrushing()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_EarBrushing"))
		{
			CreateAnimationEvent("WCBrush_EarBrushing", "WCBrush_PlayEarBrushingSound1", 0f);
			CreateAnimationEvent("WCBrush_EarBrushing", "WCBrush_PlayEarBrushingSound2", 5f);
			CreateAnimationEvent("WCBrush_EarBrushing", "Guitar_PlayStringSound", 100);
			CreateAnimationEvent("WCBrush_EarBrushing", "Guitar_PlayStringSound", 130);
			CreateAnimationEvent("WCBrush_EarBrushing", "PopGun_PlayPopSound", 160);
		}
		PlayAnim("WCBrush_EarBrushing");
	}

	private void WCBrush_CreateAndPlayEyeLashBrushing()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_EyeLashBrushing"))
		{
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 22);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 36);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 44);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 51);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 108);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 117);
			CreateAnimationEvent("WCBrush_EyeLashBrushing", "WCBrush_PlayEyeLashBrushingSound", 132);
		}
		PlayAnim("WCBrush_EyeLashBrushing");
	}

	private void WCBrush_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_LoopIdle"))
		{
			CreateAnimationEvent("WCBrush_LoopIdle", "PlayBwahSound", 10);
			CreateAnimationEvent("WCBrush_LoopIdle", "WCBrush_PlayScratchSound", 5);
		}
		PlayAnim("WCBrush_LoopIdle");
	}

	private void WCBrush_CreateAndPlayShowerBrush()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_ShowerBrush"))
		{
			CreateAnimationEvent("WCBrush_ShowerBrush", "WCBrush_PlayBodyBrushSound1", 0f);
		}
		PlayAnim("WCBrush_ShowerBrush");
	}

	private void WCBrush_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_Start"))
		{
			CreateAnimationEvent("WCBrush_Start", "PlayBwahSound", 3);
			CreateAnimationEvent("WCBrush_Start", "PlayBwahSound", 20);
			CreateAnimationEvent("WCBrush_Start", "PlayBwahSound", 32);
			CreateAnimationEvent("WCBrush_Start", "PlayBwahSound", 81);
			CreateAnimationEvent("WCBrush_Start", "WCBrush_PlayWooshSound", 70);
		}
		PlayAnim("WCBrush_Start");
	}

	private void WCBrush_CreateAndPlayTeethBrushing()
	{
		if (AddAnimationClip("BunnyMiniGames/WCBrush/", "WCBrush_TeethBrushing"))
		{
			CreateAnimationEvent("WCBrush_TeethBrushing", "WCBrush_PlayTeethBrushingSound", 10);
		}
		PlayAnim("WCBrush_TeethBrushing");
	}

	private void WCBrush_CreateAnimationEvents()
	{
	}

	private void Micro_LoadGameObject()
	{
		bool flag = m_Micro == null;
		m_Micro = LoadMiniGameObject("MiniGames/Micro");
		if (!flag || !(m_Micro != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Micro.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Micro/Micro");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterMicro()
	{
		StopAnim();
		if (IsSpecialStart())
		{
			m_MusicButton = 1;
			ShowMediaPicker();
			m_Fonctionality.SetType(FonctionalityItem.EType.MusicOff, FonctionalityItem.EType.MusicOn);
			GUIUtils.GetScroller(0).ForceHide();
		}
	}

	private void OnUpdateMicro()
	{
		if (m_StateFrameCount == 2f)
		{
			Micro_LoadSounds();
			Micro_CreateAnimationEvents();
			Micro_LoadGameObject();
			Micro_CreateAndPlayStart();
			m_MicroLickingCount = 0;
		}
		else if (m_StateFrameCount < 3f)
		{
			return;
		}
		if (m_CurrentAnimation == "Micro_Start")
		{
			if (!base.animIsPlaying)
			{
				Micro_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Micro_LoopIdle")
		{
			if (m_CheckInput && AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Leave)
			{
				string cast = RaycastUnderFinger(0);
				if (GetCollider(cast) == ECollider.Micro)
				{
					if (++m_MicroLickingCount == 3)
					{
						Micro_CreateAndPlayShock();
						UnlockState(EState.Micro, 1);
					}
					else
					{
						Micro_CreateAndPlayLick();
						UnlockState(EState.Micro, 0);
					}
				}
			}
			else if (m_MusicButton == 1)
			{
				StopSound();
				Micro_CreateAndPlaySingASong();
				UnlockState(EState.Micro, 2);
			}
		}
		else if (m_CurrentAnimation == "Micro_Lick")
		{
			if (!base.animIsPlaying)
			{
				Micro_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Micro_SingASong")
		{
			if (!base.animIsPlaying)
			{
				if (IsMediaPlaying())
				{
					m_Micro = UnloadMiniGameObject(m_Micro);
					Dance_CreateAnimationEvents();
					PlayAnim("dance_all");
				}
				else
				{
					StopMusicDance();
					SetState(EState.Idle);
				}
			}
		}
		else if (m_CurrentAnimation == "dance_all")
		{
			if (!base.animIsPlaying)
			{
				if (IsMediaPlaying())
				{
					Dance_CreateAnimationEvents();
					PlayAnim("dance_all");
				}
				else
				{
					StopMusicDance();
					SetState(EState.Idle);
				}
			}
			else if (!IsMediaPlaying() || m_MusicButton == 0)
			{
				StopMusicDance();
				SetState(EState.Idle);
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveMicro()
	{
		ClearAdditionalAnimationClips();
		StopMusicDance();
		AttachMiniGameObjectToShadow1(null);
		m_Micro = UnloadMiniGameObject(m_Micro);
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
	}

	private void OnPauseMicro(bool pauseResume)
	{
		PauseAnimatedObject(m_Micro, pauseResume);
	}

	private void Micro_PlayCrunchSound()
	{
		if (PlayCallBack("Micro_PlayCrunchSound"))
		{
			LoadSound("Micro/", ESound.Micro_Crunch);
			PlaySound(ESound.Micro_Crunch);
		}
	}

	private void Micro_PlaySingSound()
	{
		if (PlayCallBack("Micro_PlaySingSound"))
		{
			LoadSound("Micro/", ESound.Micro_Song);
			PlaySound(ESound.Micro_Song);
		}
	}

	private void Micro_PlayElectricitySound()
	{
		if (PlayCallBack("Micro_PlayElectricitySound"))
		{
			LoadSound("Micro/", ESound.Micro_Electricity);
			PlaySound(ESound.Micro_Electricity);
		}
	}

	private void Micro_PlayLightElectricitySound()
	{
		if (PlayCallBack("Micro_PlayLightElectricitySound"))
		{
			LoadSound("Micro/", ESound.Micro_Electricity);
			PlaySound(ESound.Micro_Electricity, 0.5f);
		}
	}

	private void Micro_LoadSounds()
	{
	}

	private void Micro_CreateAndPlayShock()
	{
		if (AddAnimationClip("BunnyMiniGames/Micro/", "Micro_Shock"))
		{
			CreateAnimationEvent("Micro_Shock", "Micro_PlayCrunchSound", 40);
			CreateAnimationEvent("Micro_Shock", "Micro_PlayElectricitySound", 57);
			CreateAnimationEvent("Micro_Shock", "Micro_PlayElectricitySound", 70);
			CreateAnimationEvent("Micro_Shock", "Micro_PlayLightElectricitySound", 131);
			CreateAnimationEvent("Micro_Shock", "PlayBigShortBwahSound", 180);
			CreateAnimationEvent("Micro_Shock", "Micro_PlayCrunchSound", 240);
		}
		PlayAnim("Micro_Shock");
	}

	private void Micro_CreateAndPlayLick()
	{
		if (AddAnimationClip("BunnyMiniGames/Micro/", "Micro_Lick"))
		{
			CreateAnimationEvent("Micro_Lick", "Piranha_PlayFishFallSound", 5);
			CreateAnimationEvent("Micro_Lick", "Piranha_PlayFishFallSound", 20);
		}
		PlayAnim("Micro_Lick");
	}

	private void Micro_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Micro/", "Micro_LoopIdle"))
		{
			CreateAnimationEvent("Micro_LoopIdle", "Piranha_PlayGlassSplashSound", 80);
			CreateAnimationEvent("Micro_LoopIdle", "Piranha_PlayGlassSplashSound", 85);
		}
		PlayAnim("Micro_LoopIdle");
	}

	private void Micro_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Micro/", "Micro_Start"))
		{
		}
		PlayAnim("Micro_Start");
	}

	private void Micro_CreateAndPlaySingASong()
	{
		if (AddAnimationClip("BunnyMiniGames/Micro/", "Micro_SingASong"))
		{
			CreateAnimationEvent("Micro_SingASong", "Micro_PlaySingSound", 12);
		}
		PlayAnim("Micro_SingASong");
	}

	private void Micro_CreateAnimationEvents()
	{
	}

	private void PhoneReal_LoadGameObject()
	{
		bool flag = m_PhoneReal == null;
		bool flag2 = m_PhoneRealCall == null;
		m_PhoneReal = LoadMiniGameObject("MiniGames/PhoneReal");
		if (flag && m_PhoneReal != null)
		{
			Renderer[] componentsInChildren = m_PhoneReal.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/PhoneReal/Phone");
				Renderer[] array = componentsInChildren;
				foreach (Renderer renderer in array)
				{
					renderer.material.mainTexture = mainTexture;
				}
			}
		}
		m_PhoneRealCall = LoadMiniGameObject("MiniGames/PhoneRealCall", m_Camera_2D.transform, EPhysicLayer.HUD);
		if (flag2 && m_PhoneRealCall != null)
		{
			Renderer[] componentsInChildren2 = m_PhoneRealCall.GetComponentsInChildren<Renderer>();
			if (componentsInChildren2 != null)
			{
				Texture mainTexture2 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/PhoneReal/PhoneCall");
				Renderer[] array2 = componentsInChildren2;
				foreach (Renderer renderer2 in array2)
				{
					renderer2.material.mainTexture = mainTexture2;
				}
			}
		}
		if (m_PhoneRealCall != null)
		{
			m_PhoneRealCall.SetActiveRecursively(false);
		}
	}

	private void OnEnterPhoneReal()
	{
		PhoneReal_LoadSounds();
		PhoneReal_CreateAnimationEvents();
		PhoneReal_LoadGameObject();
		m_PhoneRealInteractiveGreen = Utility.NewRect(20f, Utility.RefHeight - 70f, 100f, 50f);
		m_PhoneRealInteractiveRed = Utility.NewRect(Utility.RefWidth - 120f, Utility.RefHeight - 70f, 100f, 50f);
		if (ReverseScreen())
		{
			m_PhoneRealInteractiveGreen = new Rect((float)Screen.width - m_PhoneRealInteractiveGreen.x - m_PhoneRealInteractiveGreen.width, (float)Screen.height - m_PhoneRealInteractiveGreen.y - m_PhoneRealInteractiveGreen.height, m_PhoneRealInteractiveGreen.width, m_PhoneRealInteractiveGreen.height);
			m_PhoneRealInteractiveRed = new Rect((float)Screen.width - m_PhoneRealInteractiveRed.x - m_PhoneRealInteractiveRed.width, (float)Screen.height - m_PhoneRealInteractiveRed.y - m_PhoneRealInteractiveRed.height, m_PhoneRealInteractiveRed.width, m_PhoneRealInteractiveRed.height);
			if (m_PhoneRealCall != null)
			{
				m_PhoneRealCall.transform.localEulerAngles = new Vector3(270f, 0f, 0f);
			}
		}
		PhoneReal_CreateAndPlayStart();
	}

	private void OnUpdatePhoneReal()
	{
		if (m_CurrentAnimation == "Phone_Start")
		{
			if (!base.animIsPlaying)
			{
				PhoneReal_CreateAndPlayIdleLoop();
				if (m_PhoneRealCall != null)
				{
					m_PhoneRealCall.SetActiveRecursively(true);
				}
				if (m_Scroller != null)
				{
					m_Scroller.SetVisible(false);
				}
			}
		}
		else if (m_CurrentAnimation == "Phone_IdleLoop")
		{
			if (!m_CheckInput || AllInput.GetTouchCount() != 1 || AllInput.GetState(0) != AllInput.EState.Leave)
			{
				return;
			}
			if (m_PhoneRealInteractiveRed.Contains(AllInput.GetGUIPosition(0)))
			{
				PhoneReal_CreateAndPlayBomb();
				UnlockState(EState.PhoneReal, 0);
			}
			else if (m_PhoneRealInteractiveGreen.Contains(AllInput.GetGUIPosition(0)))
			{
				PhoneReal_CreateAndPlayGreen();
				UnlockState(EState.PhoneReal, 1);
			}
			else
			{
				string text = RaycastHUDUnderFinger(0);
				if (text == null || text == "None")
				{
					PhoneReal_CreateAndPlayStunned();
					UnlockState(EState.PhoneReal, 2);
				}
			}
			if (m_CurrentAnimation != "Phone_IdleLoop")
			{
				m_PhoneRealCall.SetActiveRecursively(false);
				if (m_Scroller != null)
				{
					m_Scroller.SetVisible(true);
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeavePhoneReal()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_PhoneReal = UnloadMiniGameObject(m_PhoneReal);
		m_PhoneRealCall = UnloadMiniGameObject(m_PhoneRealCall);
		if (m_Scroller != null)
		{
			m_Scroller.SetVisible(true);
		}
	}

	private void OnPausePhoneReal(bool pauseResume)
	{
		PauseAnimatedObject(m_PhoneReal, pauseResume);
	}

	private void PhoneReal_PlayComposeSound()
	{
		if (PlayCallBack("PhoneReal_PlayComposeSound"))
		{
			LoadSound("PhoneReal/", ESound.PhoneReal_Dial);
			PlaySound(ESound.PhoneReal_Dial);
		}
	}

	private void PhoneReal_PlayRingSound()
	{
		if (PlayCallBack("PhoneReal_PlayRingSound"))
		{
			if (m_PhoneRealRingStyle == 0)
			{
				LoadSound("PhoneReal/", ESound.PhoneReal_Ring_a);
				PlaySound(ESound.PhoneReal_Ring_a);
				m_PhoneRealRingStyle = 1;
			}
			else
			{
				LoadSound("PhoneReal/", ESound.PhoneReal_Ring_b);
				PlaySound(ESound.PhoneReal_Ring_b);
				m_PhoneRealRingStyle = 0;
			}
		}
	}

	private void PhoneReal_PlayExplosionSound()
	{
		if (PlayCallBack("PhoneReal_PlayExplosionSound"))
		{
			LoadSound("PhoneReal/", ESound.PhoneReal_Explode);
			PlaySound(ESound.PhoneReal_Explode);
		}
	}

	private void PhoneReal_PlaySwallowSound()
	{
		if (PlayCallBack("PhoneReal_PlaySwallowSound"))
		{
			LoadSound("PhoneReal/", ESound.PhoneReal_Swallow);
			PlaySound(ESound.PhoneReal_Swallow);
		}
	}

	private void PhoneReal_LoadSounds()
	{
	}

	private void PhoneReal_CreateAndPlayGreen()
	{
		if (AddAnimationClip("BunnyMiniGames/PhoneReal/", "Phone_Green"))
		{
			CreateAnimationEvent("Phone_Green", "PlayBigShortBwahSound", 62);
		}
		PlayAnim("Phone_Green");
	}

	private void PhoneReal_CreateAndPlayIdleLoop()
	{
		if (AddAnimationClip("BunnyMiniGames/PhoneReal/", "Phone_IdleLoop"))
		{
			CreateAnimationEvent("Phone_IdleLoop", "PhoneReal_PlayRingSound", 0);
			CreateAnimationEvent("Phone_IdleLoop", "PhoneReal_PlayRingSound", 70);
		}
		PlayAnim("Phone_IdleLoop");
	}

	private void PhoneReal_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/PhoneReal/", "Phone_Start"))
		{
			CreateAnimationEvent("Phone_Start", "PhoneReal_PlayComposeSound", 0);
		}
		PlayAnim("Phone_Start");
	}

	private void PhoneReal_CreateAndPlayStunned()
	{
		if (AddAnimationClip("BunnyMiniGames/PhoneReal/", "Phone_Stunned"))
		{
		}
		PlayAnim("Phone_Stunned");
	}

	private void PhoneReal_CreateAndPlayBomb()
	{
		if (AddAnimationClip("BunnyMiniGames/PhoneReal/", "Phone_Bomb"))
		{
			CreateAnimationEvent("Phone_Bomb", "PlayBwahSound", 80);
			CreateAnimationEvent("Phone_Bomb", "PlayBwahSound", 100);
			CreateAnimationEvent("Phone_Bomb", "PhoneReal_PlaySwallowSound", 130);
			CreateAnimationEvent("Phone_Bomb", "PhoneReal_PlayExplosionSound", 200);
			CreateAnimationEvent("Phone_Bomb", "Jetpack_PlayReboundSound", 220);
		}
		PlayAnim("Phone_Bomb");
	}

	private void PhoneReal_CreateAnimationEvents()
	{
	}

	private void PopGun_LoadGameObject()
	{
		bool flag = m_PopGun == null;
		m_PopGun = LoadMiniGameObject("MiniGames/PopGun");
		if (!flag || !(m_PopGun != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_PopGun.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/PopGun/PopGun");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterPopGun()
	{
		PopGun_LoadSounds();
		PopGun_CreateAnimationEvents();
		PopGun_LoadGameObject();
		PopGun_CreateAndPlayStart();
		RenderSettings.fogColor = new Color(0f, 0f, 0f, 0f);
	}

	private void OnUpdatePopGun()
	{
		if (m_CurrentAnimation == "PopGun_Shoot")
		{
			if (m_CurrentAnimTime >= 5.2f)
			{
				float fogDensity = RenderSettings.fogDensity;
				fogDensity = (RenderSettings.fogDensity = Mathf.Min(1f, fogDensity + Time.deltaTime));
				if (!RenderSettings.fog)
				{
					RenderSettings.fog = true;
					ActivateShadow(false);
				}
				if (fogDensity > 0.9f && m_CheckInput && AllInput.GetTouchCount() == 1 && AllInput.GetState(0) == AllInput.EState.Leave)
				{
					RenderSettings.fog = false;
					RenderSettings.fogDensity = 0f;
					SetState(EState.Idle);
				}
			}
		}
		else if (m_CurrentAnimation == "PopGun_Start")
		{
			if (!base.animIsPlaying)
			{
				PopGun_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "PopGun_LoopIdle")
		{
			if (!m_CheckInput || AllInput.GetTouchCount() != 1)
			{
				return;
			}
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_PopGunSlide = false;
			}
			string cast = RaycastUnderFinger(0);
			ECollider collider = GetCollider(cast);
			if (collider == ECollider.PopGun && !m_PopGunSlide)
			{
				m_PopGunSlide = true;
			}
			if (AllInput.GetState(0) != AllInput.EState.Leave)
			{
				return;
			}
			if (!m_TouchMoved)
			{
				if (collider == ECollider.PopGun)
				{
					PopGun_CreateAndPlayShoot();
					UnlockState(EState.PopGun, 1);
				}
			}
			else if (DetectSlide() == EDirection.Down)
			{
				if (m_PopGunSlide)
				{
					PopGun_CreateAndPlayButt();
					UnlockState(EState.PopGun, 0);
				}
			}
			else
			{
				PopGun_CreateAndPlayEye();
				UnlockState(EState.PopGun, 2);
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeavePopGun()
	{
		ClearAdditionalAnimationClips();
		if (RenderSettings.fog)
		{
			RenderSettings.fog = false;
			RenderSettings.fogDensity = 0f;
		}
		AttachMiniGameObjectToShadow1(null);
		ActivateShadow(true);
		m_PopGun = UnloadMiniGameObject(m_PopGun);
	}

	private void OnPausePopGun(bool pauseResume)
	{
		PauseAnimatedObject(m_PopGun, pauseResume);
	}

	private void PopGun_LoadSounds()
	{
	}

	private void PopGun_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/PopGun/", "PopGun_Start"))
		{
		}
		PlayAnim("PopGun_Start");
	}

	private void PopGun_CreateAndPlayShoot()
	{
		if (AddAnimationClip("BunnyMiniGames/PopGun/", "PopGun_Shoot"))
		{
			CreateAnimationEvent("PopGun_Shoot", "PopGun_PlayPopSound", 154);
			CreateAnimationEvent("PopGun_Shoot", "PopGun_PlayExplodeSound", 181);
		}
		PlayAnim("PopGun_Shoot");
	}

	private void PopGun_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/PopGun/", "PopGun_LoopIdle"))
		{
		}
		PlayAnim("PopGun_LoopIdle");
	}

	private void PopGun_CreateAndPlayEye()
	{
		if (AddAnimationClip("BunnyMiniGames/PopGun/", "PopGun_Eye"))
		{
			CreateAnimationEvent("PopGun_Eye", "PopGun_PlayChocSound", 110);
			CreateAnimationEvent("PopGun_Eye", "PlayBigShortBwahSound", 170);
			CreateAnimationEvent("PopGun_Eye", "EgyptianToiletPaper_PlayChocMatSound", 220);
		}
		PlayAnim("PopGun_Eye");
	}

	private void PopGun_CreateAndPlayButt()
	{
		if (AddAnimationClip("BunnyMiniGames/PopGun/", "PopGun_Butt"))
		{
			CreateAnimationEvent("PopGun_Butt", "EgyptianToiletPaper_PlayChocMatSound", 20);
			CreateAnimationEvent("PopGun_Butt", "PopGun_PlayChocSound", 34);
			CreateAnimationEvent("PopGun_Butt", "PlayBigBwahSound", 55);
		}
		PlayAnim("PopGun_Butt");
	}

	private void PopGun_CreateAnimationEvents()
	{
	}

	private void PopGun_PlayChocSound()
	{
		if (PlayCallBack("PopGun_PlayChocSound"))
		{
			LoadSound("Common/", ESound.ChocRabbid);
			PlaySound(ESound.ChocRabbid);
		}
	}

	private void PopGun_PlayPopSound()
	{
		if (PlayCallBack("PopGun_PlayPopSound"))
		{
			LoadSound("PopGun/", ESound.PopGun_Pop);
			PlaySound(ESound.PopGun_Pop);
		}
	}

	private void PopGun_PlayExplodeSound()
	{
		if (PlayCallBack("PopGun_PlayExplodeSound"))
		{
			LoadSound("PopGun/", ESound.Bulb_Explode);
			PlaySound(ESound.Bulb_Explode);
		}
	}

	private void PopGun_PlayBrokenSound()
	{
		if (PlayCallBack("PopGun_PlayBrokenSound"))
		{
			LoadSound("PopGun/", ESound.PopGun_Broken);
			PlaySound(ESound.PopGun_Broken);
		}
	}

	private void PopGun_PlayBwahSound()
	{
	}

	private void Jetpack_LoadGameObject()
	{
		bool flag = m_Jetpack == null;
		m_Jetpack = LoadMiniGameObject("MiniGames/Jetpack");
		if (!flag || !(m_Jetpack != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Jetpack.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Jetpack/Jetpack");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterJetpack()
	{
		Jetpack_LoadSounds();
		Jetpack_CreateAnimationEvents();
		Jetpack_LoadGameObject();
		Jetpack_CreateAndPlayStart();
	}

	private void OnUpdateJetpack()
	{
		if (m_CurrentAnimation == "JetPack_Start")
		{
			if (!base.animIsPlaying)
			{
				Jetpack_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "JetPack_LoopIdle")
		{
			if (m_IsCircleGesture)
			{
				Jetpack_CreateAndPlayWhirlStart();
			}
			else if (m_CheckInput && AllInput.GetState(0) == AllInput.EState.Leave && !m_TouchMoved)
			{
				string cast = RaycastUnderFinger(0);
				ECollider collider = GetCollider(cast);
				ECollider eCollider = collider;
				if (eCollider == ECollider.Tummy || eCollider == ECollider.String || eCollider == ECollider.Jetpack)
				{
					Jetpack_CreateAndPlayPushLoop();
					UnlockState(EState.Jetpack, 1);
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			if (m_CurrentAnimation == "JetPack_Whirl_start")
			{
				Jetpack_CreateAndPlayWhirlLoop();
				UnlockState(EState.Jetpack, 0);
			}
			else if (m_CurrentAnimation == "JetPack_Whirl_Loop")
			{
				if (m_JetpackWhirlTime <= 0f)
				{
					Jetpack_CreateAndPlayWhirlEnd();
					return;
				}
				RewindAnim("JetPack_Whirl_Loop");
				PlayAnim("JetPack_Whirl_Loop");
			}
			else if (m_CurrentAnimation.Contains("JetPack_Push_Loop"))
			{
				Jetpack_CreateAndPlayPushEnd();
			}
			else
			{
				SetState(EState.Idle);
			}
		}
		else if (m_CurrentAnimation == "JetPack_Whirl_Loop")
		{
			if (m_IsCircleGesture)
			{
				m_JetpackWhirlTime = m_CurrentAnimLength;
			}
			m_JetpackWhirlTime -= Time.deltaTime;
		}
		else if (m_CurrentAnimation == "JetPack_Push_End" && m_CurrentAnimTime < 0.3f && m_CheckInput && AllInput.GetTouchCount() == 1 && (AllInput.GetState(0) == AllInput.EState.Began || AllInput.GetState(0) == AllInput.EState.Leave))
		{
			string cast2 = RaycastUnderFinger(0);
			ECollider collider2 = GetCollider(cast2);
			ECollider eCollider = collider2;
			if (eCollider == ECollider.Tummy || eCollider == ECollider.String || eCollider == ECollider.Jetpack)
			{
				Jetpack_CreateAndPlayPushLoop();
				UnlockState(EState.Jetpack, 2);
			}
		}
	}

	private void OnLeaveJetpack()
	{
		ClearAdditionalAnimationClips();
		ActivateShadow(true);
		m_Jetpack = UnloadMiniGameObject(m_Jetpack);
		StopSound();
	}

	private void OnPauseJetpack(bool pauseResume)
	{
		PauseAnimatedObject(m_Jetpack, pauseResume);
	}

	private void Jetpack_LoadSounds()
	{
	}

	private void Jetpack_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Start"))
		{
			CreateAnimationEvent("JetPack_Start", "Jetpack_PlayWhirlSound", 10);
		}
		PlayAnim("JetPack_Start");
	}

	private void Jetpack_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_LoopIdle"))
		{
			CreateAnimationEvent("JetPack_LoopIdle", "Jetpack_PlayWhirlSound", 20);
		}
		PlayAnim("JetPack_LoopIdle");
	}

	private void Jetpack_CreateAndPlayPushEnd()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Push_End"))
		{
			CreateAnimationEvent("JetPack_Push_End", "Jetpack_PlayReboundSound", 30);
		}
		PlayAnim("JetPack_Push_End");
	}

	private void Jetpack_CreateAndPlayPushLoop()
	{
		int num = Random.Range(0, 1000) % 2 + 1;
		string anim = "JetPack_Push_Loop" + num;
		if (num == 1)
		{
			if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Push_Loop1"))
			{
				CreateAnimationEvent("JetPack_Push_Loop1", "PlayBwahSound", 7);
			}
		}
		else if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Push_Loop2"))
		{
			CreateAnimationEvent("JetPack_Push_Loop2", "PlayBwahSound", 7);
		}
		PlayAnim(anim);
	}

	private void Jetpack_CreateAndPlayWhirlEnd()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Whirl_end"))
		{
			CreateAnimationEvent("JetPack_Whirl_end", "Jetpack_PlayFallSound", 4);
		}
		PlayAnim("JetPack_Whirl_end");
	}

	private void Jetpack_CreateAndPlayWhirlLoop()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Whirl_Loop"))
		{
		}
		PlayAnim("JetPack_Whirl_Loop");
	}

	private void Jetpack_CreateAndPlayWhirlStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Jetpack/", "JetPack_Whirl_start"))
		{
		}
		PlayAnim("JetPack_Whirl_start");
	}

	private void Jetpack_CreateAnimationEvents()
	{
	}

	private void Jetpack_PlayWhirlSound()
	{
		if (PlayCallBack("Jetpack_PlayWhirlSound"))
		{
			LoadSound("Jetpack/", ESound.JetPack_Whirl_Loop);
			PlaySound(ESound.JetPack_Whirl_Loop, 1f, true);
		}
	}

	private void Jetpack_PlayFallSound()
	{
		if (PlayCallBack("Jetpack_PlayFallSound"))
		{
			LoadSound("Common/", ESound.ChocMat_a);
			PlaySound(ESound.ChocMat_a);
		}
	}

	private void Jetpack_PlayReboundSound()
	{
		if (PlayCallBack("Jetpack_PlayReboundSound"))
		{
			LoadSound("Common/", ESound.ChocRabbid);
			PlaySound(ESound.ChocRabbid, 0.5f);
		}
	}

	private void Jetpack_PlayJokariSound()
	{
		if (PlayCallBack("Jetpack_PlayJokariSound"))
		{
			LoadSound("Common/", ESound.ChocRabbid);
			PlaySound(ESound.ChocRabbid);
		}
	}

	private void Shield_LoadGameObject()
	{
		bool flag = m_Shield == null;
		m_Shield = LoadMiniGameObject("MiniGames/Shield");
		if (!flag || !(m_Shield != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Shield.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Shield/Shield");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterShield()
	{
		Shield_LoadSounds();
		Shield_CreateAnimationEvents();
		Shield_LoadGameObject();
		Shield_CreateAndPlayStart();
	}

	private void OnUpdateShield()
	{
		if (m_CurrentAnimation == "Shield_Start")
		{
			if (!base.animIsPlaying)
			{
				Shield_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Shield_LoopIdle")
		{
			if (!m_CheckInput || AllInput.GetTouchCount() != 1)
			{
				return;
			}
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_ShieldSlide = false;
			}
			string cast = RaycastUnderFinger(0);
			ECollider collider = GetCollider(cast);
			if (collider == ECollider.Shield && !m_ShieldSlide)
			{
				m_ShieldSlide = true;
			}
			if (AllInput.GetState(0) == AllInput.EState.Leave && m_ShieldSlide && m_TouchMoved)
			{
				switch (DetectSlide())
				{
				case EDirection.Up:
					Shield_CreateAndPlaySurf();
					UnlockState(EState.Shield, 1);
					break;
				case EDirection.Right:
				case EDirection.Left:
					Shield_CreateAndPlayTurtle();
					UnlockState(EState.Shield, 0);
					break;
				default:
					Shield_CreateAndPlayFaces();
					UnlockState(EState.Shield, 2);
					break;
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveShield()
	{
		ClearAdditionalAnimationClips();
		ActivateShadow(true);
		m_Shield = UnloadMiniGameObject(m_Shield);
	}

	private void OnPauseShield(bool pauseResume)
	{
		PauseAnimatedObject(m_Shield, pauseResume);
	}

	private void Shield_LoadSounds()
	{
	}

	private void Shield_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Shield/", "Shield_Start"))
		{
		}
		PlayAnim("Shield_Start");
	}

	private void Shield_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Shield/", "Shield_LoopIdle"))
		{
			CreateAnimationEvent("Shield_LoopIdle", "Shield_PlayShieldSound", 22);
			CreateAnimationEvent("Shield_LoopIdle", "Shield_PlayShieldSound", 28);
			CreateAnimationEvent("Shield_LoopIdle", "Shield_PlayShieldSound", 34);
			CreateAnimationEvent("Shield_LoopIdle", "Shield_PlayShieldSound", 41);
		}
		PlayAnim("Shield_LoopIdle");
	}

	private void Shield_CreateAndPlayFaces()
	{
		if (AddAnimationClip("BunnyMiniGames/Shield/", "Shield_Faces"))
		{
			CreateAnimationEvent("Shield_Faces", "Shield_PlayFearSound", 100);
			CreateAnimationEvent("Shield_Faces", "PlayBwahSound", 20);
			CreateAnimationEvent("Shield_Faces", "PlayBwahSound", 40);
			CreateAnimationEvent("Shield_Faces", "PlayBwahSound", 60);
			CreateAnimationEvent("Shield_Faces", "PlayBwahSound", 80);
		}
		PlayAnim("Shield_Faces");
	}

	private void Shield_CreateAndPlaySurf()
	{
		if (AddAnimationClip("BunnyMiniGames/Shield/", "Shield_Surf"))
		{
			CreateAnimationEvent("Shield_Surf", "Shield_PlayShieldSound", 10);
			CreateAnimationEvent("Shield_Surf", "Shield_PlayHawaiSound", 35);
			CreateAnimationEvent("Shield_Surf", "Shield_PlayFallSound", 112);
		}
		PlayAnim("Shield_Surf");
	}

	private void Shield_CreateAndPlayTurtle()
	{
		if (AddAnimationClip("BunnyMiniGames/Shield/", "Shield_Turtle"))
		{
			CreateAnimationEvent("Shield_Turtle", "Shield_PlayRabbidBackSound", 15);
			CreateAnimationEvent("Shield_Turtle", "Shield_PlayHawaiSound", 35);
			CreateAnimationEvent("Shield_Turtle", "Shield_PlayFallSound", 133);
		}
		PlayAnim("Shield_Turtle");
	}

	private void Shield_CreateAnimationEvents()
	{
	}

	private void Shield_PlayRabbidBackSound()
	{
		if (PlayCallBack("Shield_PlayRabbidBackSound"))
		{
			LoadSound("Common/", ESound.ChocRabbid);
			PlaySound(ESound.ChocRabbid);
		}
	}

	private void Shield_PlayShieldSound()
	{
		if (PlayCallBack("Shield_PlayShieldSound"))
		{
			LoadSound("Common/", ESound.ChocMat_b);
			PlaySound(ESound.ChocMat_b);
		}
	}

	private void Shield_PlayFallSound()
	{
		if (PlayCallBack("Shield_PlayFallSound"))
		{
			LoadSound("Common/", ESound.ChocMat_c);
			PlaySound(ESound.ChocMat_c);
		}
	}

	private void Shield_PlayHawaiSound()
	{
		if (PlayCallBack("Shield_PlayHawaiSound"))
		{
			LoadSound("Shield/", ESound.Hawai_Loop);
			PlaySound(ESound.Hawai_Loop);
		}
	}

	private void Shield_PlayFearSound()
	{
		if (PlayCallBack("Shield_PlayFearSound"))
		{
			PlaySound(EBwahSound.Hysteric_Bwaah);
		}
	}

	private void Guitar_LoadGameObject()
	{
		bool flag = m_Guitar == null;
		m_Guitar = LoadMiniGameObject("MiniGames/Guitar");
		if (!flag || !(m_Guitar != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Guitar.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Guitar/Guitar");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterGuitar()
	{
		Guitar_LoadSounds();
		Guitar_CreateAnimationEvents();
		Guitar_LoadGameObject();
		Guitar_CreateAndPlayStart();
	}

	private void OnUpdateGuitar()
	{
		if (m_CurrentAnimation == "Guitar_Start")
		{
			if (!base.animIsPlaying)
			{
				Guitar_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Guitar_LoopIdle")
		{
			bool flag = false;
			if (m_IsCircleGesture)
			{
				flag = true;
				Guitar_CreateAndPlayBelly();
				UnlockState(EState.Guitar, 0);
			}
			else if (AllInput.GetState(0) == AllInput.EState.Leave)
			{
				switch (DetectSlide())
				{
				case EDirection.Up:
					flag = true;
					Guitar_CreateAndPlayGag2();
					UnlockState(EState.Guitar, 1);
					break;
				case EDirection.Down:
					flag = true;
					Guitar_CreateAndPlayGag3();
					UnlockState(EState.Guitar, 2);
					break;
				}
			}
			if (flag)
			{
				Object.DestroyImmediate(m_Guitar.GetComponent<Collider>());
				Object.DestroyImmediate(m_Guitar.GetComponent<Rigidbody>());
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveGuitar()
	{
		ClearAdditionalAnimationClips();
		ActivateShadow(true);
		m_Guitar = UnloadMiniGameObject(m_Guitar);
	}

	private void OnPauseGuitar(bool pauseResume)
	{
		PauseAnimatedObject(m_Guitar, pauseResume);
	}

	private void Guitar_LoadSounds()
	{
	}

	private void Guitar_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Guitar/", "Guitar_Start"))
		{
		}
		PlayAnim("Guitar_Start");
	}

	private void Guitar_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Guitar/", "Guitar_LoopIdle"))
		{
			CreateAnimationEvent("Guitar_LoopIdle", "Guitar_PlayGoodSound", 20);
			CreateAnimationEvent("Guitar_LoopIdle", "Guitar_PlayGood2Sound", 110);
		}
		PlayAnim("Guitar_LoopIdle");
	}

	private void Guitar_CreateAndPlayBelly()
	{
		if (AddAnimationClip("BunnyMiniGames/Guitar/", "Guitar_Belly"))
		{
			CreateAnimationEvent("Guitar_Belly", "Guitar_PlayWooshSound", 5);
			CreateAnimationEvent("Guitar_Belly", "Guitar_PlayBadSound", 25);
			CreateAnimationEvent("Guitar_Belly", "Guitar_PlayBadSound", 87);
			CreateAnimationEvent("Guitar_Belly", "Guitar_PlayWooshSound", 150);
			CreateAnimationEvent("Guitar_Belly", "EgyptianToiletPaper_PlayChocMatSound", 180);
		}
		PlayAnim("Guitar_Belly");
	}

	private void Guitar_CreateAndPlayGag2()
	{
		if (AddAnimationClip("BunnyMiniGames/Guitar/", "Guitar_Gag2"))
		{
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayWooshSound", 5);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayBadSound", 30);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayBadSound", 92);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayStringSound", 170);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayStringSound", 200);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayStringSound", 230);
			CreateAnimationEvent("Guitar_Gag2", "Guitar_PlayWooshSound", 250);
			CreateAnimationEvent("Guitar_Gag2", "Micro_PlayCrunchSound", 270);
		}
		PlayAnim("Guitar_Gag2");
	}

	private void Guitar_CreateAndPlayGag3()
	{
		if (AddAnimationClip("BunnyMiniGames/Guitar/", "Guitar_Gag3"))
		{
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 10);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 18);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 30);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 35);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 50);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 55);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 70);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 75);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 85);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 90);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlayWooshSound", 95);
			CreateAnimationEvent("Guitar_Gag3", "Guitar_PlaySmashSound", 100);
			CreateAnimationEvent("Guitar_Gag3", "EgyptianToiletPaper_PlayChocMatSound", 140);
		}
		PlayAnim("Guitar_Gag3");
	}

	private void Guitar_CreateAnimationEvents()
	{
	}

	private void Guitar_PlayWooshSound()
	{
		if (PlayCallBack("Guitar_PlayWooshSound"))
		{
			LoadSound("Guitar/", ESound.Woosh);
			PlaySound(ESound.Woosh);
		}
	}

	private void Guitar_PlayStringSound()
	{
		if (PlayCallBack("Guitar_PlayStringSound"))
		{
			LoadSound("Izi/", ESound.IZI_pulling_begin);
			PlaySound(ESound.IZI_pulling_begin);
		}
	}

	private void Guitar_PlaySmashSound()
	{
		if (PlayCallBack("Guitar_PlaySmashSound"))
		{
			LoadSound("Common/", ESound.ChocMat_c);
			PlaySound(ESound.ChocMat_c);
		}
	}

	private void Guitar_PlayBadSound()
	{
		if (PlayCallBack("Guitar_PlayBadSound"))
		{
			LoadSound("Guitar/", ESound.Guitar_Bad);
			PlaySound(ESound.Guitar_Bad);
		}
	}

	private void Guitar_PlayGoodSound()
	{
		if (PlayCallBack("Guitar_PlayGoodSound"))
		{
			LoadSound("Guitar/", ESound.Guitar_Good);
			PlaySound(ESound.Guitar_Good);
		}
	}

	private void Guitar_PlayGood2Sound()
	{
		if (PlayCallBack("Guitar_PlayGood2Sound"))
		{
			LoadSound("Guitar/", ESound.Guitar_Good_2);
			PlaySound(ESound.Guitar_Good_2);
		}
	}

	private void EgyptianToiletPaper_LoadGameObject()
	{
		bool flag = m_EgyptianToiletPaper == null;
		m_EgyptianToiletPaper = LoadMiniGameObject("MiniGames/EgyptianToiletPaper");
		if (!flag || !(m_EgyptianToiletPaper != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_EgyptianToiletPaper.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/EgyptianToiletPaper/ToiletPaper");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterEgyptianToiletPaper()
	{
		EgyptianToiletPaper_LoadSounds();
		EgyptianToiletPaper_CreateAnimationEvents();
		EgyptianToiletPaper_LoadGameObject();
		EgyptianToiletPaper_CreateAndPlayStart();
	}

	private void OnUpdateEgyptianToiletPaper()
	{
		if (m_CurrentAnimation == "Toilet_Paper_Start")
		{
			if (!base.animIsPlaying)
			{
				EgyptianToiletPaper_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Toilet_Paper_LoopIdle")
		{
			if (AllInput.GetState(0) == AllInput.EState.Began)
			{
				m_EgyptianToiletPaperSlide = false;
			}
			string cast = RaycastUnderFinger(0);
			ECollider collider = GetCollider(cast);
			if (collider == ECollider.EgyptianToiletPaper && !m_EgyptianToiletPaperSlide)
			{
				m_EgyptianToiletPaperSlide = true;
			}
			if (AllInput.GetState(0) == AllInput.EState.Leave && m_EgyptianToiletPaperSlide)
			{
				switch (DetectSlide())
				{
				case EDirection.Up:
					EgyptianToiletPaper_CreateAndPlayPaperTast();
					UnlockState(EState.EgyptianToiletPaper, 0);
					break;
				case EDirection.Down:
					EgyptianToiletPaper_CreateAndPlayReading();
					UnlockState(EState.EgyptianToiletPaper, 1);
					break;
				case EDirection.Right:
				case EDirection.Left:
					EgyptianToiletPaper_CreateAndPlayJugglin();
					UnlockState(EState.EgyptianToiletPaper, 2);
					break;
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveEgyptianToiletPaper()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_EgyptianToiletPaper = UnloadMiniGameObject(m_EgyptianToiletPaper);
	}

	private void OnPauseEgyptianToiletPaper(bool pauseResume)
	{
		PauseAnimatedObject(m_EgyptianToiletPaper, pauseResume);
	}

	private void EgyptianToiletPaper_LoadSounds()
	{
	}

	private void EgyptianToiletPaper_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/EgyptianToiletPaper/", "Toilet_Paper_Start"))
		{
		}
		PlayAnim("Toilet_Paper_Start");
	}

	private void EgyptianToiletPaper_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/EgyptianToiletPaper/", "Toilet_Paper_LoopIdle"))
		{
		}
		PlayAnim("Toilet_Paper_LoopIdle");
	}

	private void EgyptianToiletPaper_CreateAndPlayJugglin()
	{
		if (AddAnimationClip("BunnyMiniGames/EgyptianToiletPaper/", "Toilet_Paper_Jugglin"))
		{
			CreateAnimationEvent("Toilet_Paper_Jugglin", "Piranha_PlayRabbidShot", 48);
			CreateAnimationEvent("Toilet_Paper_Jugglin", "Piranha_PlayRabbidShot", 125);
			CreateAnimationEvent("Toilet_Paper_Jugglin", "Piranha_PlayRabbidShot", 189);
			CreateAnimationEvent("Toilet_Paper_Jugglin", "Piranha_PlayRabbidShot", 205);
			CreateAnimationEvent("Toilet_Paper_Jugglin", "EgyptianToiletPaper_PlayChocMatSound", 220);
		}
		PlayAnim("Toilet_Paper_Jugglin");
	}

	private void EgyptianToiletPaper_CreateAndPlayReading()
	{
		if (AddAnimationClip("BunnyMiniGames/EgyptianToiletPaper/", "Toilet_Paper_Reading"))
		{
			CreateAnimationEvent("Toilet_Paper_Reading", "Guitar_PlayWooshSound", 18);
			CreateAnimationEvent("Toilet_Paper_Reading", "EgyptianToiletPaper_PlayBwahSound", 150);
			CreateAnimationEvent("Toilet_Paper_Reading", "PlayBigShortBwahSound", 280);
			CreateAnimationEvent("Toilet_Paper_Reading", "EgyptianToiletPaper_PlayChocMatSound", 300);
		}
		PlayAnim("Toilet_Paper_Reading");
	}

	private void EgyptianToiletPaper_CreateAndPlayPaperTast()
	{
		if (AddAnimationClip("BunnyMiniGames/EgyptianToiletPaper/", "Toilet_Paper_Tast"))
		{
			CreateAnimationEvent("Toilet_Paper_Tast", "EgyptianToiletPaper_PlayLickSound", 20);
			CreateAnimationEvent("Toilet_Paper_Tast", "Piranha_PlayClapSound", 80);
			CreateAnimationEvent("Toilet_Paper_Tast", "EgyptianToiletPaper_PlayFartSound", 190);
			CreateAnimationEvent("Toilet_Paper_Tast", "Piranha_PlayRabbidShot", 232);
			CreateAnimationEvent("Toilet_Paper_Tast", "Piranha_PlayRabbidShot", 247);
			CreateAnimationEvent("Toilet_Paper_Tast", "Piranha_PlayRabbidShot", 259);
		}
		PlayAnim("Toilet_Paper_Tast");
	}

	private void EgyptianToiletPaper_CreateAnimationEvents()
	{
	}

	private void EgyptianToiletPaper_PlayLickSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlayLickSound"))
		{
			LoadSound("EgyptianToiletPaper/", ESound.Lick);
			PlaySound(ESound.Lick);
		}
	}

	private void EgyptianToiletPaper_PlaySwallowSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlaySwallowSound"))
		{
			PlaySound(EBwahSound.Bwah_A);
		}
	}

	private void EgyptianToiletPaper_PlaySuffocateSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlaySuffocateSound"))
		{
			PlaySound(EBwahSound.Bwah_N);
		}
	}

	private void EgyptianToiletPaper_PlayFartSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlayFartSound"))
		{
			LoadSound("EgyptianToiletPaper/", ESound.Fart);
			PlaySound(ESound.Fart);
		}
	}

	private void EgyptianToiletPaper_PlayChocMatSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlayChocMatSound"))
		{
			int num = Random.Range(0, 100) % 2;
			if (num == 1)
			{
				LoadSound("Common/", ESound.ChocMat_b);
				PlaySound(ESound.ChocMat_b);
			}
			else
			{
				LoadSound("Common/", ESound.ChocMat_c);
				PlaySound(ESound.ChocMat_c);
			}
		}
	}

	private void EgyptianToiletPaper_PlayBwahSound()
	{
		if (PlayCallBack("EgyptianToiletPaper_PlayBwahSound"))
		{
			switch (Random.Range(0, 100) % 4)
			{
			case 0:
				PlaySound(EBwahSound.Bwah_E);
				break;
			case 1:
				PlaySound(EBwahSound.Bwah_J);
				break;
			case 2:
				PlaySound(EBwahSound.Bwah_M);
				break;
			default:
				PlaySound(EBwahSound.IZW_Bun_Wonder2);
				break;
			}
		}
	}

	private void Bow_LoadGameObject()
	{
		bool flag = m_Bow == null;
		m_Bow = LoadMiniGameObject("MiniGames/Bow");
		if (!flag || !(m_Bow != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Bow.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Bow/Bow");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterBow()
	{
		Bow_LoadGameObject();
		Bow_CreateAndPlayStart();
	}

	private void OnUpdateBow()
	{
		if (m_CurrentAnimation == "Bow_Start")
		{
			if (!base.animIsPlaying)
			{
				Bow_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Bow_Idle")
		{
			if (AllInput.GetTouchCount() != 1)
			{
				return;
			}
			EDirection eDirection = DetectSlide();
			EDirection eDirection2 = eDirection;
			if (eDirection2 == EDirection.Right)
			{
				Bow_CreateAndPlayAction1();
				UnlockState(EState.Bow, 0);
			}
			if (AllInput.GetState(0) == AllInput.EState.Leave)
			{
				string cast = RaycastUnderFinger(0);
				switch (GetCollider(cast))
				{
				case ECollider.LeftEye:
				case ECollider.RightEye:
				case ECollider.LeftEar:
				case ECollider.RightEar:
					Bow_CreateAndPlayAction2();
					UnlockState(EState.Bow, 1);
					break;
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveBow()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_Bow = UnloadMiniGameObject(m_Bow);
		StopSound();
	}

	private void OnPauseBow(bool pauseResume)
	{
		PauseAnimatedObject(m_Bow, pauseResume);
	}

	private void Bow_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Bow/", "Bow_Start"))
		{
		}
		PlayAnim("Bow_Start");
		StopSound();
		LoadSound("Bow/", ESound.Bow_Start);
		PlaySound(ESound.Bow_Start);
	}

	private void Bow_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Bow/", "Bow_Idle"))
		{
			CreateAnimationEvent("Bow_Idle", "Bow_PlayIdleSound", 0);
		}
		PlayAnim("Bow_Idle");
	}

	private void Bow_CreateAndPlayAction1()
	{
		if (AddAnimationClip("BunnyMiniGames/Bow/", "Bow_Action1"))
		{
		}
		PlayAnim("Bow_Action1");
		StopSound();
		LoadSound("Bow/", ESound.Bow_Action1);
		PlaySound(ESound.Bow_Action1);
	}

	private void Bow_CreateAndPlayAction2()
	{
		if (AddAnimationClip("BunnyMiniGames/Bow/", "Bow_Action2"))
		{
		}
		PlayAnim("Bow_Action2");
		StopSound();
		LoadSound("Bow/", ESound.Bow_Action2);
		PlaySound(ESound.Bow_Action2);
	}

	private void Bow_PlayIdleSound()
	{
		if (PlayCallBack("Bow_PlayIdleSound") && !(m_CurrentAnimation != "Bow_Idle"))
		{
			StopSound();
			LoadSound("Bow/", ESound.Bow_Idle);
			PlaySound(ESound.Bow_Idle);
		}
	}

	private void GhettoBlaster_LoadGameObject()
	{
		bool flag = m_GhettoBlaster == null;
		m_GhettoBlaster = LoadMiniGameObject("MiniGames/GhettoBlaster");
		if (!flag || !(m_GhettoBlaster != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_GhettoBlaster.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/GhettoBlaster/GhettoBlaster");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterGhettoBlaster()
	{
		GhettoBlaster_LoadGameObject();
		GhettoBlaster_CreateAndPlayStart();
	}

	private void OnUpdateGhettoBlaster()
	{
		if (m_CurrentAnimation == "GhettoBlaster_Start")
		{
			if (!base.animIsPlaying)
			{
				GhettoBlaster_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "GhettoBlaster_Idle")
		{
			if (!m_CheckInput || AllInput.GetTouchCount() != 1 || AllInput.GetState(0) != AllInput.EState.Leave)
			{
				return;
			}
			string cast = RaycastUnderFinger(0);
			ECollider collider = GetCollider(cast);
			if (collider == ECollider.GhettoBlaster)
			{
				GhettoBlaster_CreateAndPlayAction1();
				UnlockState(EState.GhettoBlaster, 0);
				return;
			}
			EDirection eDirection = DetectSlide();
			if (eDirection == EDirection.Undefined)
			{
				GhettoBlaster_CreateAndPlayAction2();
				UnlockState(EState.GhettoBlaster, 1);
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveGhettoBlaster()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_GhettoBlaster = UnloadMiniGameObject(m_GhettoBlaster);
		StopSound();
	}

	private void OnPauseGhettoBlaster(bool pauseResume)
	{
		PauseAnimatedObject(m_GhettoBlaster, pauseResume);
	}

	private void GhettoBlaster_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/GhettoBlaster/", "GhettoBlaster_Start"))
		{
		}
		PlayAnim("GhettoBlaster_Start");
		StopSound();
		LoadSound("GhettoBlaster/", ESound.GhettoBlaster_Start);
		PlaySound(ESound.GhettoBlaster_Start);
	}

	private void GhettoBlaster_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/GhettoBlaster/", "GhettoBlaster_Idle"))
		{
			CreateAnimationEvent("GhettoBlaster_Idle", "GhettoBlaster_PlayIdleSound", 0);
		}
		PlayAnim("GhettoBlaster_Idle");
	}

	private void GhettoBlaster_CreateAndPlayAction1()
	{
		if (AddAnimationClip("BunnyMiniGames/GhettoBlaster/", "GhettoBlaster_Action_1"))
		{
		}
		PlayAnim("GhettoBlaster_Action_1");
		StopSound();
		LoadSound("GhettoBlaster/", ESound.GhettoBlaster_Action_1);
		PlaySound(ESound.GhettoBlaster_Action_1);
	}

	private void GhettoBlaster_CreateAndPlayAction2()
	{
		if (AddAnimationClip("BunnyMiniGames/GhettoBlaster/", "GhettoBlaster_Action_2"))
		{
		}
		PlayAnim("GhettoBlaster_Action_2");
		StopSound();
		LoadSound("GhettoBlaster/", ESound.GhettoBlaster_Action_2);
		PlaySound(ESound.GhettoBlaster_Action_2);
	}

	private void GhettoBlaster_PlayIdleSound()
	{
		if (PlayCallBack("GhettoBlaster_PlayIdleSound") && !(m_CurrentAnimation != "GhettoBlaster_Idle"))
		{
			StopSound();
			LoadSound("GhettoBlaster/", ESound.GhettoBlaster_Idle);
			PlaySound(ESound.GhettoBlaster_Idle);
		}
	}

	private void Nunchaku_LoadGameObject()
	{
		bool flag = m_Nunchaku == null;
		m_Nunchaku = LoadMiniGameObject("MiniGames/Nunchaku");
		if (!flag || !(m_Nunchaku != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_Nunchaku.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/Nunchaku/Nunchaku");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterNunchaku()
	{
		Nunchaku_LoadGameObject();
		Nunchaku_CreateAndPlayStart();
	}

	private void OnUpdateNunchaku()
	{
		if (m_CurrentAnimation == "Nunchaku_Start")
		{
			if (!base.animIsPlaying)
			{
				Nunchaku_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "Nunchaku_Idle")
		{
			if (m_IsCircleGesture)
			{
				Nunchaku_CreateAndPlayAction2();
				UnlockState(EState.Nunchaku, 1);
			}
			else if (AllInput.GetTouchCount() == 1 && DetectSlide() == EDirection.Down)
			{
				Nunchaku_CreateAndPlayAction1();
				UnlockState(EState.Nunchaku, 0);
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveNunchaku()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_Nunchaku = UnloadMiniGameObject(m_Nunchaku);
		StopSound();
	}

	private void OnPauseNunchaku(bool pauseResume)
	{
		PauseAnimatedObject(m_Nunchaku, pauseResume);
	}

	private void Nunchaku_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/Nunchaku/", "Nunchaku_Start"))
		{
		}
		PlayAnim("Nunchaku_Start");
		StopSound();
		LoadSound("Nunchaku/", ESound.Nunchaku_Start);
		PlaySound(ESound.Nunchaku_Start);
	}

	private void Nunchaku_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/Nunchaku/", "Nunchaku_Idle"))
		{
			CreateAnimationEvent("Nunchaku_Idle", "Nunchaku_PlayIdleSound", 0);
		}
		PlayAnim("Nunchaku_Idle");
	}

	private void Nunchaku_CreateAndPlayAction1()
	{
		if (AddAnimationClip("BunnyMiniGames/Nunchaku/", "Nunchaku_Action1"))
		{
		}
		PlayAnim("Nunchaku_Action1");
		StopSound();
		LoadSound("Nunchaku/", ESound.Nunchaku_Action1);
		PlaySound(ESound.Nunchaku_Action1);
	}

	private void Nunchaku_CreateAndPlayAction2()
	{
		if (AddAnimationClip("BunnyMiniGames/Nunchaku/", "Nunchaku_Action2"))
		{
		}
		PlayAnim("Nunchaku_Action2");
		StopSound();
		LoadSound("Nunchaku/", ESound.Nunchaku_Action2);
		PlaySound(ESound.Nunchaku_Action2);
	}

	private void Nunchaku_PlayIdleSound()
	{
		if (PlayCallBack("Nunchaku_PlayIdleSound") && !(m_CurrentAnimation != "Nunchaku_Idle"))
		{
			StopSound();
			LoadSound("Nunchaku/", ESound.Nunchaku_Idle);
			PlaySound(ESound.Nunchaku_Idle);
		}
	}

	private void RugbyBall_LoadGameObject()
	{
		bool flag = m_RugbyBall == null;
		m_RugbyBall = LoadMiniGameObject("MiniGames/RugbyBall");
		if (flag && m_RugbyBall != null)
		{
			Renderer[] componentsInChildren = m_RugbyBall.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/RugbyBall/RugbyBall");
				Renderer[] array = componentsInChildren;
				foreach (Renderer renderer in array)
				{
					renderer.material.mainTexture = mainTexture;
				}
			}
		}
		string text = "bn_root_RugbyBall";
		Transform transform = m_RugbyBall.transform.Find(text);
		if (transform != null)
		{
			AttachMiniGameObjectToShadow1(transform.gameObject);
		}
		else
		{
			Utility.Log(ELog.Errors, text + " not found");
		}
	}

	private void OnEnterRugbyBall()
	{
		RugbyBall_LoadGameObject();
		RugbyBall_CreateAndPlayStart();
	}

	private void OnUpdateRugbyBall()
	{
		if (m_CurrentAnimation == "RugbyBall_Start")
		{
			if (!base.animIsPlaying)
			{
				RugbyBall_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "RugbyBall_Idle")
		{
			if (AllInput.GetTouchCount() == 1)
			{
				switch (DetectSlide())
				{
				case EDirection.Down:
					RugbyBall_CreateAndPlayAction1();
					UnlockState(EState.RugbyBall, 0);
					break;
				case EDirection.Up:
					RugbyBall_CreateAndPlayAction2();
					UnlockState(EState.RugbyBall, 1);
					break;
				case EDirection.Right:
					break;
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveRugbyBall()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_RugbyBall = UnloadMiniGameObject(m_RugbyBall);
		StopSound();
	}

	private void OnPauseRugbyBall(bool pauseResume)
	{
		PauseAnimatedObject(m_RugbyBall, pauseResume);
	}

	private void RugbyBall_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/RugbyBall/", "RugbyBall_Start"))
		{
		}
		PlayAnim("RugbyBall_Start");
		StopSound();
		LoadSound("RugbyBall/", ESound.RugbyBall_Start);
		PlaySound(ESound.RugbyBall_Start);
	}

	private void RugbyBall_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/RugbyBall/", "RugbyBall_Idle"))
		{
			CreateAnimationEvent("RugbyBall_Idle", "RugbyBall_PlayIdleSound", 0);
		}
		PlayAnim("RugbyBall_Idle");
	}

	private void RugbyBall_CreateAndPlayAction1()
	{
		if (AddAnimationClip("BunnyMiniGames/RugbyBall/", "RugbyBall_Action1"))
		{
		}
		PlayAnim("RugbyBall_Action1");
		StopSound();
		LoadSound("RugbyBall/", ESound.RugbyBall_Action1);
		PlaySound(ESound.RugbyBall_Action1);
	}

	private void RugbyBall_CreateAndPlayAction2()
	{
		if (AddAnimationClip("BunnyMiniGames/RugbyBall/", "RugbyBall_Action2"))
		{
		}
		PlayAnim("RugbyBall_Action2");
		StopSound();
		LoadSound("RugbyBall/", ESound.RugbyBall_Action2);
		PlaySound(ESound.RugbyBall_Action2);
	}

	private void RugbyBall_PlayIdleSound()
	{
		if (PlayCallBack("RugbyBall_PlayIdleSound") && !(m_CurrentAnimation != "RugbyBall_Idle"))
		{
			StopSound();
			LoadSound("RugbyBall/", ESound.RugbyBall_Idle);
			PlaySound(ESound.RugbyBall_Idle);
		}
	}

	private void SausagePolice_LoadGameObject()
	{
		bool flag = m_SausagePolice == null;
		m_SausagePolice = LoadMiniGameObject("MiniGames/SausagePolice");
		if (!flag || !(m_SausagePolice != null))
		{
			return;
		}
		Renderer[] componentsInChildren = m_SausagePolice.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Texture mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "MiniGames/SausagePolice/SausagePolice");
			Renderer[] array = componentsInChildren;
			foreach (Renderer renderer in array)
			{
				renderer.material.mainTexture = mainTexture;
			}
		}
	}

	private void OnEnterSausagePolice()
	{
		SausagePolice_LoadGameObject();
		SausagePolice_CreateAndPlayStart();
	}

	private void OnUpdateSausagePolice()
	{
		if (m_CurrentAnimation == "SausagePolice_Start")
		{
			if (!base.animIsPlaying)
			{
				SausagePolice_CreateAndPlayLoopIdle();
			}
		}
		else if (m_CurrentAnimation == "SausagePolice_Idle")
		{
			if (AllInput.GetTouchCount() == 1)
			{
				switch (DetectSlide())
				{
				case EDirection.Right:
				case EDirection.Left:
					SausagePolice_CreateAndPlayAction2();
					UnlockState(EState.SausagePolice, 1);
					break;
				case EDirection.Down:
				case EDirection.Up:
					SausagePolice_CreateAndPlayAction1();
					UnlockState(EState.SausagePolice, 0);
					break;
				}
			}
		}
		else if (!base.animIsPlaying)
		{
			SetState(EState.Idle);
		}
	}

	private void OnLeaveSausagePolice()
	{
		ClearAdditionalAnimationClips();
		AttachMiniGameObjectToShadow1(null);
		m_SausagePolice = UnloadMiniGameObject(m_SausagePolice);
	}

	private void OnPauseSausagePolice(bool pauseResume)
	{
		PauseAnimatedObject(m_SausagePolice, pauseResume);
	}

	private void SausagePolice_CreateAndPlayStart()
	{
		if (AddAnimationClip("BunnyMiniGames/SausagePolice/", "SausagePolice_Start"))
		{
		}
		PlayAnim("SausagePolice_Start");
		StopSound();
		LoadSound("SausagePolice/", ESound.SausagePolice_Start);
		PlaySound(ESound.SausagePolice_Start);
	}

	private void SausagePolice_CreateAndPlayLoopIdle()
	{
		if (AddAnimationClip("BunnyMiniGames/SausagePolice/", "SausagePolice_Idle"))
		{
			CreateAnimationEvent("SausagePolice_Idle", "SausagePolice_PlayIdleSound", 0);
		}
		PlayAnim("SausagePolice_Idle");
		LoadSound("SausagePolice/", ESound.SausagePolice_Idle);
		PlaySound(ESound.SausagePolice_Idle);
	}

	private void SausagePolice_CreateAndPlayAction1()
	{
		if (AddAnimationClip("BunnyMiniGames/SausagePolice/", "SausagePolice_Action_1"))
		{
		}
		PlayAnim("SausagePolice_Action_1");
		StopSound();
		LoadSound("SausagePolice/", ESound.SausagePolice_Action_1);
		PlaySound(ESound.SausagePolice_Action_1);
	}

	private void SausagePolice_CreateAndPlayAction2()
	{
		if (AddAnimationClip("BunnyMiniGames/SausagePolice/", "SausagePolice_Action_2"))
		{
		}
		PlayAnim("SausagePolice_Action_2");
		StopSound();
		LoadSound("SausagePolice/", ESound.SausagePolice_Action_2);
		PlaySound(ESound.SausagePolice_Action_2);
	}

	private void SausagePolice_PlayIdleSound()
	{
		if (PlayCallBack("SausagePolice_PlayIdleSound") && !(m_CurrentAnimation != "SausagePolice_Idle"))
		{
			StopSound();
			LoadSound("SausagePolice/", ESound.SausagePolice_Idle);
			PlaySound(ESound.SausagePolice_Idle);
		}
	}

	private void OnEnter()
	{
	}

	private void OnUpdate()
	{
	}

	private void OnLeave()
	{
	}

	private void OnPause(bool pauseResume)
	{
	}

	private void _CreateAnimationEvents()
	{
	}

	public static EState GetStateEnum(string str)
	{
		for (EState eState = EState.PresentTuto; eState < EState.Count; eState++)
		{
			if (str.Equals(eState.ToString()))
			{
				return eState;
			}
		}
		return EState.Count;
	}

	public static int GetMiniGameIndex(EState state)
	{
		return (int)(state - 16 - 1);
	}

	public static int GetMiniGameIndex(string str)
	{
		return GetMiniGameIndex(GetStateEnum(str));
	}

	public void AttachMiniGameObjectToShadow1(GameObject go)
	{
		if (m_MiniGameShadow1 != null)
		{
			m_MiniGameShadow1.SetActiveRecursively(go != null);
		}
		if (m_FakeShadow1 != null)
		{
			m_FakeShadow1.m_Anchor = go;
		}
	}

	public void AttachMiniGameObjectToShadow2(GameObject go)
	{
		if (m_MiniGameShadow2 != null)
		{
			m_MiniGameShadow2.SetActiveRecursively(go != null);
		}
		if (m_FakeShadow2 != null)
		{
			m_FakeShadow2.m_Anchor = go;
		}
	}

	public static EPack GetPackFromState(EState state)
	{
		if (state == EState.CustomCostume || state == EState.CustomEnvironment)
		{
			return EPack.Customize;
		}
		if (state <= EState.PresentTuto)
		{
			return EPack.Interactions;
		}
		if (state <= EState.Steam)
		{
			return EPack.Base;
		}
		if (state <= EState.PhoneReal)
		{
			return EPack.XK;
		}
		if (state <= EState.EgyptianToiletPaper)
		{
			return EPack.History;
		}
		if (state < EState.Count)
		{
			return EPack.Moustach;
		}
		return EPack.Count;
	}

	public static EPack GetPackFromCostume(ECostume cst)
	{
		if (cst <= ECostume.String)
		{
			return EPack.Base;
		}
		if (cst <= ECostume.Knight)
		{
			return EPack.History;
		}
		if (cst < ECostume.Count)
		{
			return EPack.Moustach;
		}
		return EPack.Count;
	}

	public static EPack GetPackFromEnvironment(EEnvironment cst)
	{
		if (cst <= EEnvironment.Girl)
		{
			return EPack.Base;
		}
		if (cst <= EEnvironment.Cleopatra)
		{
			return EPack.History;
		}
		if (cst < EEnvironment.Count)
		{
			return EPack.Moustach;
		}
		return EPack.Count;
	}

	private void LoadAvailableStates()
	{
		m_LockedStates.Clear();
		m_RandomStates.Clear();
		m_UnlockedStates.Clear();
		for (EState eState = EState.ClownBox; eState < EState.Count; eState++)
		{
			string key = eState.ToString() + "_Locked";
			if (m_AchievementsTable.IsProductDependent(eState))
			{
				if (m_AchievementsTable.IsMiniGameAvailable(eState))
				{
					m_UnlockedStates.Add(eState);
					string key2 = eState.ToString() + "EndedActionCount";
					int num = PlayerPrefs.GetInt(key2);
					if (num < m_AchievementsTable.GetActionCount(eState))
					{
						m_RandomStates.Insert(0, eState);
					}
				}
				else
				{
					m_LockedStates.Add(eState);
				}
			}
			else if (PlayerPrefs.HasKey(key))
			{
				if (PlayerPrefs.GetInt(key) == 1)
				{
					m_LockedStates.Add(eState);
					if (m_ThematicsTable.IsMiniGameAvailable(m_CurrentCostume, eState))
					{
						m_RandomStates.Add(eState);
					}
				}
				else
				{
					m_UnlockedStates.Add(eState);
				}
			}
			else
			{
				GamePlayerPrefs.SetInt(key, 1);
				m_LockedStates.Add(eState);
				if (m_ThematicsTable.IsMiniGameAvailable(m_CurrentCostume, eState))
				{
					m_RandomStates.Add(eState);
				}
			}
		}
		if (!PlayerPrefs.HasKey("upgrade"))
		{
			if (!PlayerPrefs.HasKey("steam"))
			{
				UnlockState(EState.Steam, 0);
			}
			else
			{
				PlayerPrefs.DeleteKey("steam");
			}
			if (!PlayerPrefs.HasKey("poke_gift"))
			{
				UnlockState(EState.ClownBox, 0);
			}
			else
			{
				PlayerPrefs.DeleteKey("poke_gift");
			}
			GamePlayerPrefs.SetInt("upgrade", 1);
		}
		FillPublicArrays();
	}

	private void LoadAvailableTutorials()
	{
		m_TutorialsStates.Clear();
		if (AllInput.IsMicroAvailable())
		{
			if (PlayerPrefs.GetInt("blow") == 1)
			{
				m_TutorialsStates.Add(ETutorial.Blow);
			}
			if (PlayerPrefs.GetInt("yell") == 1)
			{
				m_TutorialsStates.Add(ETutorial.Yell);
			}
		}
		if (PlayerPrefs.GetInt("burp") == 1)
		{
			m_TutorialsStates.Add(ETutorial.Burp);
		}
		if (PlayerPrefs.GetInt("rotate_down") == 1)
		{
			m_TutorialsStates.Add(ETutorial.Facedown);
		}
		if (PlayerPrefs.GetInt("run") == 1)
		{
			m_TutorialsStates.Add(ETutorial.Run);
		}
		if (PlayerPrefs.GetInt("turn") == 1)
		{
			m_TutorialsStates.Add(ETutorial.Turn);
		}
	}

	private bool UnlockState(EState state, int actionID)
	{
		return UnlockState(state, actionID, true);
	}

	private bool UnlockState(EState state, int actionID, bool readFile)
	{
		bool result = false;
		if (state <= EState.PresentTuto)
		{
			Utility.Log(ELog.Errors, "Forbidden unlocking on " + state);
			return result;
		}
		string key = state.ToString() + "_Locked";
		if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 0)
		{
			return result;
		}
		string key2 = state.ToString() + "_Action_" + actionID + "_Locked";
		if (PlayerPrefs.HasKey(key2) && PlayerPrefs.GetInt(key2) == 0)
		{
			return result;
		}
		GamePlayerPrefs.SetInt(key2, 0);
		if (m_AchievementsTable != null)
		{
			if (readFile)
			{
				m_AchievementsTable.UnlockStateAction(state, actionID);
				List<OutWorld2D.EWallpaper> justUnlockedWallpapers = m_AchievementsTable.GetJustUnlockedWallpapers();
				if (justUnlockedWallpapers != null && justUnlockedWallpapers.Count > 0)
				{
					PlayExternalAudioClip(NewWallPaperSound);
					m_NewWallpaper = true;
				}
				List<ECostume> justUnlockedCostumes = m_AchievementsTable.GetJustUnlockedCostumes();
				if (justUnlockedCostumes != null && justUnlockedCostumes.Count > 0)
				{
					PlayExternalAudioClip(NewWallPaperSound);
					m_NewCostume = true;
				}
				List<EEnvironment> justUnlockedEnvironments = m_AchievementsTable.GetJustUnlockedEnvironments();
				if (justUnlockedEnvironments != null && justUnlockedEnvironments.Count > 0)
				{
					PlayExternalAudioClip(NewWallPaperSound);
					m_NewEnvironment = true;
				}
				List<OutWorld2D.EStripe> justUnlockedStripes = m_AchievementsTable.GetJustUnlockedStripes();
				if (justUnlockedStripes != null && justUnlockedStripes.Count > 0)
				{
					PlayExternalAudioClip(NewWallPaperSound);
					m_NewStripe = true;
				}
				List<PhotoRoom.EARPose> justUnlockedARPoses = m_AchievementsTable.GetJustUnlockedARPoses();
				List<PhotoRoom.EAREighty> justUnlockedAREighties = m_AchievementsTable.GetJustUnlockedAREighties();
				if ((justUnlockedARPoses != null && justUnlockedARPoses.Count > 0) || (justUnlockedAREighties != null && justUnlockedAREighties.Count > 0))
				{
					PlayExternalAudioClip(NewWallPaperSound);
					m_NewARPict = true;
				}
			}
			string key3 = state.ToString() + "EndedActionCount";
			int num = PlayerPrefs.GetInt(key3) + 1;
			GamePlayerPrefs.SetInt(key3, num);
			if (num == m_AchievementsTable.GetActionCount(state))
			{
				GamePlayerPrefs.SetInt(key, 0);
				if (m_LockedStates.Contains(state))
				{
					m_LockedStates.Remove(state);
				}
				if (m_RandomStates.Contains(state))
				{
					m_RandomStates.Remove(state);
				}
				if (m_RandomStatesInPack.Contains(state))
				{
					m_RandomStatesInPack.Remove(state);
				}
				if (!m_UnlockedStates.Contains(state))
				{
					m_UnlockedStates.Add(state);
					if (readFile)
					{
						RecreateMiniGameThumbnailsElement(state);
					}
				}
				result = true;
			}
			DataMining.UnlockSuccess();
			num = PlayerPrefs.GetInt("_number_of_ended_mini_game_moves") + 1;
			GamePlayerPrefs.SetInt("_number_of_ended_mini_game_moves", num);
			num = PlayerPrefs.GetInt("_total_number_of_ended_moves") + 1;
			GamePlayerPrefs.SetInt("_total_number_of_ended_moves", num);
			m_NewMoveCounter = kNewMoveTimer * 4;
			m_TimeSinceLastSuccess = 0f;
			GlobalVariables.ResetEndedMoveCount();
			if (readFile)
			{
				GlobalVariables.ComputeScore(true);
			}
			ActivateScroller(m_CurrentScroller);
		}
		if (readFile)
		{
			FillPublicArrays();
		}
		return result;
	}

	private void UnlockPack(EProductID productID)
	{
		Utility.UnlockProduct(productID);
		if (m_AchievementsTable != null)
		{
			m_AchievementsTable.UnlockPack();
		}
		GlobalVariables.RecomputeMoveCount();
		LoadAvailableStates();
		RecreateCostumeThumbnailsBuffer();
		RecreateEnvironmentThumbnailsBuffer();
		RecreateMiniGameThumbnailsBuffer();
		GUIUtils.GetScroller(1).Resize(m_AllThumbnails[1].ToArray(), 0);
		GUIUtils.GetScroller(2).Resize(m_AllThumbnails[2].ToArray(), -1);
		ActivateScroller(m_CurrentScroller);
	}

	private GameObject LoadMiniGameObject(string resPath)
	{
		return LoadMiniGameObject(resPath, base.transform, EPhysicLayer.Player);
	}

	private GameObject LoadMiniGameObject(string resPath, Transform parent)
	{
		return LoadMiniGameObject(resPath, parent, EPhysicLayer.Player);
	}

	private GameObject LoadMiniGameObject(string resPath, Transform parent, EPhysicLayer layer)
	{
		GameObject gameObject = null;
		if (!m_MiniGamesHashtable.ContainsKey(resPath))
		{
			gameObject = Utility.LoadGameObject(resPath, parent, (int)layer);
			m_MiniGamesHashtable.Add(resPath, gameObject);
			m_MiniGamesHashtableRevert.Add(gameObject, resPath);
			Utility.Log(ELog.Loading, gameObject.name);
		}
		else
		{
			gameObject = (GameObject)m_MiniGamesHashtable[resPath];
		}
		if (gameObject != null)
		{
			m_MiniGamesObjects.Add(gameObject);
			gameObject.SetActiveRecursively(true);
		}
		return gameObject;
	}

	private GameObject UnloadMiniGameObject(GameObject obj)
	{
		if (obj != null)
		{
			if (m_ReleaseMemory)
			{
				if (m_MiniGamesHashtable.ContainsValue(obj))
				{
					m_MiniGamesHashtable.Remove(m_MiniGamesHashtableRevert[obj]);
					m_MiniGamesHashtableRevert.Remove(obj);
				}
				obj.SetActiveRecursively(false);
				m_ObjectsToClean.Add(obj);
				return null;
			}
			if (m_MiniGamesObjects.Contains(obj))
			{
				m_MiniGamesObjects.Remove(obj);
			}
			if (obj.GetComponent<Animation>() != null)
			{
				obj.GetComponent<Animation>().Stop();
				obj.GetComponent<Animation>().Rewind();
			}
			obj.SetActiveRecursively(false);
		}
		return obj;
	}

	private void ClearMiniGameObjects()
	{
		bool releaseMemory = m_ReleaseMemory;
		m_ReleaseMemory = true;
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject value in m_MiniGamesHashtable.Values)
		{
			list.Add(value);
		}
		foreach (GameObject item in list)
		{
			UnloadMiniGameObject(item);
		}
		list.Clear();
		list = null;
		if (m_ObjectsToClean.Count > 0)
		{
			Utility.Log(ELog.Info, "Clear Mini Game Objects");
			foreach (GameObject item2 in m_ObjectsToClean)
			{
				Utility.Log(ELog.Info, "UnloadMiniGameObject: " + item2.name);
				Object.DestroyImmediate(item2);
			}
			m_ObjectsToClean.Clear();
		}
		m_ReleaseMemory = releaseMemory;
	}

	private void SaveMiniGameObjects()
	{
		for (int i = 0; i < m_MiniGamesObjects.Count; i++)
		{
			GameObject gameObject = m_MiniGamesObjects[i];
			Vector3 localPosition = gameObject.transform.localPosition;
			Vector3 localScale = gameObject.transform.localScale;
			Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
			gameObject.transform.parent = m_Anchor.transform;
			gameObject.transform.localPosition = localPosition;
			gameObject.transform.localScale = localScale;
			gameObject.transform.localEulerAngles = localEulerAngles;
		}
	}

	private void RestoreMiniGameObjects()
	{
		if (!(m_Anchor == null))
		{
			for (int i = 0; i < m_MiniGamesObjects.Count; i++)
			{
				GameObject gameObject = m_MiniGamesObjects[i];
				Vector3 localPosition = gameObject.transform.localPosition;
				Vector3 localScale = gameObject.transform.localScale;
				Vector3 localEulerAngles = gameObject.transform.localEulerAngles;
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = localPosition;
				gameObject.transform.localScale = localScale;
				gameObject.transform.localEulerAngles = localEulerAngles;
			}
		}
	}

	private void PlayBwahSound()
	{
		if (PlayCallBack("PlayBwahSound"))
		{
			EBwahSound sndId = (EBwahSound)Random.Range(7, 14);
			PlaySound(sndId);
		}
	}

	private void PlayBigBwahSound()
	{
		if (PlayCallBack("PlayBigBwahSound"))
		{
			LoadSound("Izi/", ESound.Rabbid_Baaah);
			PlaySound(ESound.Rabbid_Baaah);
		}
	}

	private void PlayBigShortBwahSound()
	{
		if (PlayCallBack("PlayBigShortBwahSound"))
		{
			LoadSound("Izi/", ESound.Rabbid_ShortBaaah);
			PlaySound(ESound.Rabbid_ShortBaaah);
		}
	}

	private void CreateAnimationEvent(string animName, string fctName, int frame)
	{
		CreateAnimationEvent(animName, fctName, (float)frame / 30f);
	}

	private void CreateAnimationEvent(string animName, string fctName, float time)
	{
		AnimationEvent animationEvent = new AnimationEvent();
		animationEvent.functionName = fctName;
		animationEvent.time = time;
		base.GetComponent<Animation>()[animName].clip.AddEvent(animationEvent);
	}

	private void FillPublicArrays()
	{
	}

	private void StartPhoto()
	{
		string text = Utility.GetPersistentDataPath() + "/";
		m_PhotoPath = text + GlobalVariables.s_PhotoPath + "/";
		m_MiniPhotoPath = text + GlobalVariables.s_MiniPhotoPath + "/";
		DirectoryInfo directoryInfo = new DirectoryInfo(m_MiniPhotoPath);
		if (directoryInfo != null && directoryInfo.Exists)
		{
			FileInfo[] files = directoryInfo.GetFiles();
			if (files != null)
			{
				m_PhotoNumber = files.Length;
			}
		}
	}

	private void SetAsCameraSon(GameObject go)
	{
		if (go != null && m_Camera_3D != null)
		{
			Vector3 position = go.transform.position;
			Quaternion rotation = go.transform.rotation;
			go.transform.parent = m_Camera_3D.transform;
			go.transform.position = position;
			go.transform.rotation = rotation;
		}
	}

	private void ValidatePhoto()
	{
		Utility.StopInputByActivity(true);
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Active = false;
			m_Fonctionality.Close(false);
		}
		if (ReverseScreen())
		{
			m_Screenshot = Utility.Screenshot(DeviceOrientation.PortraitUpsideDown);
		}
		else
		{
			m_Screenshot = Utility.Screenshot(DeviceOrientation.Portrait);
		}
		if (m_Flash != null)
		{
			m_Flash.FadeIn(m_FlashDuration);
		}
	}

	private void ProcessPhoto()
	{
		m_PhotoNumber++;
		int num = PlayerPrefs.GetInt("PhotoTaken");
		GamePlayerPrefs.SetInt("PhotoTaken", num + 1);
		float num2 = (float)m_Screenshot.width / (float)m_Screenshot.height;
		string text = string.Format("{0:000}.png", num);
		byte[] content = m_Screenshot.EncodeToPNG();
		Utility.SaveFile(m_PhotoPath, text, content);
		Utility.SaveFile(m_MiniPhotoPath, text, content);
		Object.DestroyImmediate(m_Screenshot);
		m_Screenshot = null;
		content = null;
		Utility.FreeMem(true);
		m_MenuLastPhoto = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/LastPicture");
		float num3 = 45f / 64f;
		float num4 = (float)m_MenuLastPhoto.height * num3;
		float width = num4 * num2;
		//EtceteraBinding.resizeImageAtPath(m_MiniPhotoPath + text, width, num4);
		content = Utility.LoadFile(m_MiniPhotoPath + text);
		Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB32, false);
		texture2D.LoadImage(content);
		texture2D.Apply(false);
		texture2D = Utility.BlendTextures(m_MenuLastPhoto, texture2D);
		num4 = m_MenuLastPhoto.height;
		width = num4 * num2;
		Utility.SaveFile(m_MiniPhotoPath, text, content);
		//EtceteraBinding.resizeImageAtPath(m_MiniPhotoPath + text, width, num4);
		if (m_Fonctionality != null)
		{
			if (texture2D != null)
			{
				FonctionalityItem.EType type = FonctionalityItem.EType.LastScreenshot;
				m_Fonctionality.SetTexture(texture2D, type);
				m_Fonctionality.Enable(true, type);
			}
			else
			{
				Utility.Log(ELog.Errors, "BlendTexture returns null");
			}
			m_Fonctionality.Active = true;
			m_Fonctionality.Open(true);
		}
		m_MenuLastPhoto = null;
		texture2D = null;
		content = null;
		Utility.FreeMem(true);
		Utility.StopInputByActivity(false);
	}

	private void StartPlugins()
	{
		StartRecorder();
		StartBilling();
		if (!m_CanMakePayments)
		{
		}
	}

	private void OnEnable()
	{
		/*AudioRecorderAndroidManager.stopRecordingFinishedEvent += OnStopRecordingFinishedEvent;
		AudioRecorderAndroidManager.startRecordingFailedEvent += OnStartRecordingFailedEvent;
		AudioRecorderAndroidManager.stopRecordingFailedEvent += OnStopRecordingFailedEvent;*/
		RegisterIAB();
		RegisterStoreKit();
	}

	private void OnDisablePlugins()
	{
		UnRegisterStoreKit();
		UnRegisterIAB();
		/*AudioRecorderAndroidManager.stopRecordingFinishedEvent -= OnStopRecordingFinishedEvent;
		AudioRecorderAndroidManager.startRecordingFailedEvent -= OnStartRecordingFailedEvent;
		AudioRecorderAndroidManager.stopRecordingFailedEvent -= OnStopRecordingFailedEvent;*/
	}

	private void StartRecorder()
	{
		if (m_IsListening)
		{
		}
		if (Microphone.devices != null && Microphone.devices.Length > 0)
		{
			m_UsedMic = Microphone.devices[0];
		}
		Utility.LogRed("m_UsedMic: " + m_UsedMic);
	}

	private void Listen()
	{
		Utility.LogRed("Listen");
		if (m_UsedMic != string.Empty && AllInput.IsMicroAvailable())
		{
			m_IsListening = true;
			m_Frequency = AudioSettings.outputSampleRate;
			base.GetComponent<AudioSource>().clip = Microphone.Start(m_UsedMic, true, (int)m_RecordingTime, m_Frequency);
			while (Microphone.GetPosition(m_UsedMic) <= 0)
			{
			}
			base.GetComponent<AudioSource>().loop = true;
			base.GetComponent<AudioSource>().mute = true;
			base.GetComponent<AudioSource>().Play();
		}
	}

	private bool IsListening()
	{
		return m_IsListening;
	}

	private void StopListener()
	{
		if (AllInput.IsMicroAvailable())
		{
			Utility.LogRed("StopListener");
			m_IsListening = false;
			if (m_UsedMic != string.Empty)
			{
				base.GetComponent<AudioSource>().Stop();
				Microphone.End(m_UsedMic);
			}
		}
	}

	private float GetAveragePower()
	{
		return m_DbValue;
	}

	private void UpdateLevels()
	{
		AnalyzeMicDb();
	}

	private void OnStopRecordingFinishedEvent(string param)
	{
		m_IsListening = false;
		Utility.Log(ELog.Errors, "OnStopRecordingFinishedEvent: " + param);
	}

	private void OnStartRecordingFailedEvent(string param)
	{
		m_IsListening = false;
		Utility.Log(ELog.Errors, "OnStartRecordingFailedEvent: " + param);
	}

	private void OnStopRecordingFailedEvent(string param)
	{
		m_IsListening = false;
		Utility.Log(ELog.Errors, "OnStopRecordingFailedEvent: " + param);
	}

	private void AnalyzeMicDb()
	{
		float num = 0f;
		if (m_Samples == null)
		{
			m_Samples = new float[m_QSamples];
		}
		if (base.GetComponent<AudioSource>() != null)
		{
			base.GetComponent<AudioSource>().GetOutputData(m_Samples, 0);
		}
		for (int i = 0; i < m_QSamples; i++)
		{
			num += Mathf.Pow(m_Samples[i], 2f);
		}
		m_RmsValue = Mathf.Sqrt(num / (float)m_QSamples);
		m_DbValue = 20f * Mathf.Log10(m_RmsValue / m_RefValue);
		if (m_DbValue < -160f)
		{
			m_DbValue = -160f;
		}
	}

	private void StartBilling()
	{
		//IABAndroid.startCheckBillingAvailableRequest();
	}

	private bool CanMakePayments()
	{
		return m_CanMakePayments;
	}

	private void PurchaseProduct(string productID)
	{
		Utility.Log(ELog.Plugin, "PurchaseProduct: " + productID + " - Start");
		//IABAndroid.purchaseProduct(productID);
		Utility.Log(ELog.Plugin, "PurchaseProduct: " + productID + " - End");
	}

	private void RegisterIAB()
	{
		/*IABAndroidManager.billingSupportedEvent += OnBillingSupportedEvent;
		IABAndroidManager.purchaseSucceededEvent += OnPurchaseSucceededEvent;
		IABAndroidManager.purchaseSignatureVerifiedEvent += OnPurchaseSignatureVerifiedEvent;
		IABAndroidManager.purchaseCancelledEvent += OnPurchaseCancelledEvent;
		IABAndroidManager.purchaseRefundedEvent += OnPurchaseRefundedEvent;
		IABAndroidManager.purchaseFailedEvent += OnPurchaseFailedEvent;*/
	}

	private void UnRegisterIAB()
	{
		/*IABAndroidManager.billingSupportedEvent -= OnBillingSupportedEvent;
		IABAndroidManager.purchaseSucceededEvent -= OnPurchaseSucceededEvent;
		IABAndroidManager.purchaseSignatureVerifiedEvent -= OnPurchaseSignatureVerifiedEvent;
		IABAndroidManager.purchaseCancelledEvent -= OnPurchaseCancelledEvent;
		IABAndroidManager.purchaseRefundedEvent -= OnPurchaseRefundedEvent;
		IABAndroidManager.purchaseFailedEvent -= OnPurchaseFailedEvent;*/
	}

	private void OnBillingSupportedEvent(bool b)
	{
		Utility.Log(ELog.Plugin, "OnBillingSupportedEvent: " + b);
		m_CanMakePayments = b;
	}

	private void OnPurchaseSignatureVerifiedEvent(string signedData, string signature)
	{
		Utility.Log(ELog.Info, "OnPurchaseSignatureVerifiedEvent" + signedData + " - " + signature);
	}

	private void OnPurchaseSucceededEvent(string productId)
	{
		Utility.Log(ELog.Info, "OnPurchaseSucceededEvent: " + productId);
		EProductID productIDEnumWithStoreProductID = Utility.GetProductIDEnumWithStoreProductID(productId);
		UnlockPack(productIDEnumWithStoreProductID);
		DataMining.Increment(DataMining.EGeneral.Purchase.ToString());
		SetMessage(EMessage.PurchaseSuccess);
		SetPlaneTexture("GUI/DLC");
		StopAnim();
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
		m_SkipRestoreAfterPause = true;
		SetState(EState.Idle);
		InternalOnChangePack(m_BuyPack);
		OnPurchaseEnded();
	}

	private void OnPurchaseCancelledEvent(string productId)
	{
		Utility.Log(ELog.Info, "OnPurchaseCancelledEvent: " + productId);
		OnPurchaseEnded();
	}

	private void OnPurchaseFailedEvent(string productId)
	{
		Utility.Log(ELog.Errors, "OnPurchaseFailedEvent: " + productId);
		SetMessage(EMessage.PurchaseFailed);
		OnPurchaseEnded();
	}

	private void OnPurchaseRefundedEvent(string productId)
	{
		Utility.Log(ELog.Info, "OnPurchaseRefundedEvent" + productId);
	}

	private void RegisterStoreKit()
	{
		/*StoreKitManager.productListReceived += OnProductListReceived;
		StoreKitManager.productListRequestFailed += OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful += OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled += OnPurchaseCancelled;
		StoreKitManager.purchaseFailed += OnPurchaseFailed;*/
	}

	private void UnRegisterStoreKit()
	{
		/*StoreKitManager.productListReceived -= OnProductListReceived;
		StoreKitManager.productListRequestFailed -= OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful -= OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled -= OnPurchaseCancelled;
		StoreKitManager.purchaseFailed -= OnPurchaseFailed;*/
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
		UnlockPack(productIDEnumWithStoreProductID);
		DataMining.Increment(DataMining.EGeneral.Purchase.ToString());
		SetMessage(EMessage.PurchaseSuccess);
		SetPlaneTexture("GUI/DLC");
		StopAnim();
		m_ForceIdle = true;
		m_ForceIdleAnim = true;
		m_SkipRestoreAfterPause = true;
		SetState(EState.Idle);
		InternalOnChangePack(m_BuyPack);
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
		Pause(false);
	}

	private void OnProductListRequestEnded()
	{
		Utility.HideActivityView(true);
		Pause(false);
	}

	private void OnPurchaseEnded()
	{
		m_BuyPack = EPack.Count;
		GlobalVariables.RecomputeMoveCount();
		Utility.HideActivityView(true);
		Pause(false);
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
		GUIUtils.AddScroller(0, 0f, m_AllThumbnails[0].ToArray(), false, 0);
		GUIUtils.AddScroller(1, 383f, 0f, m_AllThumbnails[1].ToArray(), true, 0, false);
		GUIUtils.AddScroller(2, 383f, 0f, m_AllThumbnails[2].ToArray(), true, 0, false);
		GUIUtils.AddScroller(3, 0f, m_AllThumbnails[3].ToArray(), true, 0);
		((GUIRetractableScroller)GUIUtils.GetScroller(3)).SetAutoRetractable(false);
		GUIUtils.SetTextContent(m_TextContent);
	}

	private void UpdateScrolling()
	{
		m_DrawScroller = true;
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
		GUIUtils.RemoveScroller(0);
		GUIUtils.RemoveScroller(1);
		GUIUtils.RemoveScroller(2);
	}

	private void OnScrollItemPressed(int scrollViewID, int itemIdx)
	{
		switch ((EScroller)scrollViewID)
		{
		case EScroller.MiniGame:
		{
			if (m_AchievementsTable == null)
			{
				break;
			}
			EState eState = m_CurrentStates[itemIdx];
			EProductID miniGamePack = m_AchievementsTable.GetMiniGamePack(eState);
			if (miniGamePack < EProductID.AvailableCount && !Utility.IsUnlockedProduct(miniGamePack))
			{
				Request(miniGamePack);
			}
			else if (m_UnlockedStates.Contains(eState))
			{
				SetState(eState);
				if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.MiniGame, eState.ToString()))
				{
					RecreateMiniGameThumbnailsElement(itemIdx);
				}
			}
			break;
		}
		case EScroller.Costume:
			m_CostumeIndex = itemIdx;
			SelectCostume();
			break;
		case EScroller.Environment:
			m_EnvironmentIndex = itemIdx;
			SelectEnvironment();
			break;
		}
	}

	private void ScrollerGUI()
	{
		if (m_DrawScroller)
		{
			GUISkin skin = GUI.skin;
			GUI.skin = m_ScrollerSkin;
			if (m_Scroller != null)
			{
				m_Scroller.Draw(Utility.InputStoppedByActivity());
			}
			GUI.skin = skin;
		}
	}

	private void RecreateMiniGameThumbnailsBuffer()
	{
		int num = 0;
		bool flag = false;
		EPack ePack = EPack.Count;
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
			Utility.Log(ELog.Errors, "RecreateMiniGameThumbnailsBuffer failed: m_AchievementsTable == null");
			return;
		}
		m_AllThumbnails[num].Clear();
		m_CurrentStates.Clear();
		for (EState eState = EState.ClownBox; eState < EState.Count; eState++)
		{
			bool flag2 = false;
			bool flag3 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			EProductID miniGamePack = m_AchievementsTable.GetMiniGamePack(eState);
			ePack = GetPackFromState(eState);
			flag = Utility.IsUnlockedProduct(miniGamePack);
			if (array[(int)ePack].StartIndex == -1)
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
			string text = "MiniGames/Thumbnails";
			if (!m_UnlockedStates.Contains(eState) || !flag)
			{
				flag3 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag2 = AchievementTable.IsGoodyNew(AchievementTable.EGoody.MiniGame, eState.ToString());
			}
			text = text + "/" + eState;
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>(text);
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
				m_CurrentStates.Add(eState);
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
		FillPublicArrays();
	}

	private void RecreateMiniGameThumbnailsElement(int itemIdx)
	{
		Texture2D texture2D = Utility.LoadTextureResource<Texture2D>("MiniGames/Thumbnails/" + m_CurrentStates[itemIdx]);
		if (texture2D != null)
		{
			m_AllThumbnails[0][itemIdx] = texture2D;
			GUIUtils.GetScroller(0).Resize(m_AllThumbnails[0].ToArray(), itemIdx);
		}
	}

	private void RecreateMiniGameThumbnailsElement(EState state)
	{
		bool flag = AchievementTable.IsGoodyNew(AchievementTable.EGoody.MiniGame, state.ToString());
		string resPath = Utility.GetTexPath() + "MiniGames/Thumbnails/" + state;
		Texture2D texture2D = Utility.LoadResource<Texture2D>(resPath);
		if (texture2D != null)
		{
			if (flag)
			{
				texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
			}
			int num = m_CurrentStates.IndexOf(state);
			m_AllThumbnails[0][num] = texture2D;
			GUIUtils.GetScroller(0).Resize(m_AllThumbnails[0].ToArray(), num);
		}
	}

	private void RecreateCostumeThumbnailsBuffer()
	{
		int num = 1;
		bool flag = false;
		EPack ePack = EPack.Count;
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
			Utility.Log(ELog.Errors, "RecreateMiniGameThumbnailsBuffer failed: m_AchievementsTable == null");
			return;
		}
		m_AllThumbnails[num].Clear();
		for (ECostume eCostume = ECostume.Naked; eCostume < ECostume.Count; eCostume++)
		{
			bool flag2 = false;
			bool flag3 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			ePack = GetPackFromCostume(eCostume);
			EProductID productID = m_AchievementsTable.GetProductID(eCostume);
			flag = Utility.IsUnlockedProduct(productID);
			if (array[(int)ePack].StartIndex == -1)
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
			string text = "Costumes/Thumbnails";
			if (!flag)
			{
				flag3 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag2 = AchievementTable.IsGoodyNew(AchievementTable.EGoody.Costume, eCostume.ToString());
			}
			text = text + "/" + eCostume;
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>(text);
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

	private void RecreateCostumeThumbnailsElement(int itemIdx)
	{
		Texture2D texture2D = Utility.LoadTextureResource<Texture2D>("Costumes/Thumbnails/" + (ECostume)itemIdx);
		if (texture2D != null)
		{
			m_AllThumbnails[1][itemIdx] = texture2D;
		}
	}

	private void RecreateEnvironmentThumbnailsBuffer()
	{
		int num = 2;
		bool flag = false;
		GUIScroller.SPack[] array = new GUIScroller.SPack[6];
		EPack ePack;
		for (ePack = EPack.Interactions; ePack < EPack.Count; ePack++)
		{
			array[(int)ePack].Texture = null;
			array[(int)ePack].Pack = ePack;
			array[(int)ePack].StartIndex = -1;
			array[(int)ePack].EndIndex = -1;
		}
		if (m_AchievementsTable == null)
		{
			Utility.Log(ELog.Errors, "RecreateMiniGameThumbnailsBuffer failed: m_AchievementsTable == null");
			return;
		}
		m_AllThumbnails[num].Clear();
		for (EEnvironment eEnvironment = EEnvironment.Standard; eEnvironment < EEnvironment.Count; eEnvironment++)
		{
			bool flag2 = false;
			bool flag3 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			ePack = GetPackFromEnvironment(eEnvironment);
			EProductID productID = m_AchievementsTable.GetProductID(eEnvironment);
			flag = Utility.IsUnlockedProduct(productID);
			if (array[(int)ePack].StartIndex == -1)
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
			string text = "Environments/Thumbnails";
			if (!flag)
			{
				flag3 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag2 = AchievementTable.IsGoodyNew(AchievementTable.EGoody.Environment, eEnvironment.ToString());
			}
			text = text + "/" + eEnvironment;
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>(text);
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

	private void RecreateEnvironmentThumbnailsElement(int itemIdx)
	{
		Texture2D texture2D = Utility.LoadTextureResource<Texture2D>("Environments/Thumbnails/" + (EEnvironment)itemIdx);
		if (texture2D != null)
		{
			m_AllThumbnails[2][itemIdx] = texture2D;
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

	private void StartSounds()
	{
		GlobalVariables.SoundEnabled = PlayerPrefs.GetInt("_sound_enable");
	}

	private void UpdateSound()
	{
		m_MuteSwitchTimer += Time.deltaTime;
		if (m_MuteSwitchTimer > 2f)
		{
			if (!Utility.InputStoppedByActivity())
			{
				//AudioPlayerBinding.checkMuteSwitch();
			}
			m_MuteSwitchTimer = 0f;
		}
	}

	private void DoRunningFast()
	{
		if (PlayCallBack("DoRunningFast") && m_CurrentAnimation.Contains("run"))
		{
			if (AddAnimationClip("BunnyInteractions/Run/", "run_running_fast"))
			{
				CreateAnimationEvent("run_running_fast", "PlayRunSound2", 0);
				CreateAnimationEvent("run_running_fast", "DoDeceleration", base.GetComponent<Animation>()["run_running_fast"].length - 0.1f);
			}
			RewindAnim("run_running_fast");
			PlayAnim("run_running_fast");
		}
	}

	private void DoDeceleration()
	{
		if (PlayCallBack("DoDeceleration") && m_CurrentAnimation.Contains("run"))
		{
			RewindAnim("run_deceleration");
			PlayAnim("run_deceleration");
			PlayRunSound3();
		}
	}

	private void DoTickleLoop()
	{
		if (PlayCallBack("DoTickleLoop"))
		{
			RewindAnim("tickle_loop");
			PlayAnim("tickle_loop");
			LoadSound("Izi/", ESound.IZI_Tickle_step2_loop);
			PlaySound(ESound.IZI_Tickle_step2_loop);
		}
	}

	private void HideString()
	{
		if (PlayCallBack("HideString") && m_String != null)
		{
			m_String.SetActiveRecursively(false);
		}
	}

	private void DoBlowLoop()
	{
		if (PlayCallBack("DoBlowLoop"))
		{
			RewindAnim("blow_loop");
			PlayAnim("blow_loop");
			LoadSound("Izi/", ESound.IZI_Blow_on_wall);
			PlaySound(ESound.IZI_Blow_on_wall);
		}
	}

	private void DoBlowGetUp()
	{
		if (PlayCallBack("DoBlowGetUp"))
		{
			RewindAnim("blow_getup");
			PlayAnim("blow_getup");
			LoadSound("Izi/", ESound.IZI_Blow_fall_and_getup);
			PlaySound(ESound.IZI_Blow_fall_and_getup);
		}
	}

	private void StartChoke()
	{
		if (PlayCallBack("StartChoke") && Random.Range(0, 100) < 50)
		{
			if (m_State == EState.ClownBox && m_ClownBox != null)
			{
				m_ClownBox = UnloadMiniGameObject(m_ClownBox);
			}
			RewindAnim();
			PlayAnim("choke");
		}
	}

	private void StartSteam()
	{
		if (PlayCallBack("StartSteam") && m_State == EState.Steam)
		{
			RenderSettings.fogDensity = 1f;
			RenderSettings.fog = true;
			ActivateShadow(false);
		}
	}

	private void CheckNewMove(string animationName)
	{
		int num = PlayerPrefs.GetInt("_number_of_ended_moves");
		int num2 = PlayerPrefs.GetInt("_total_number_of_ended_moves");
		int num3 = PlayerPrefs.GetInt("_new_wallpaper", 0);
		int num4 = PlayerPrefs.GetInt("_new_environment", 0);
		if (m_NewMoveArray.Count <= 0)
		{
			return;
		}
		int num5 = 0;
		foreach (string item in m_NewMoveArray)
		{
			if (item == animationName)
			{
				OutWorld2D.EWallpaper eWallpaper = OutWorld2D.EWallpaper.Count;
				EEnvironment eEnvironment = EEnvironment.Count;
				DataMining.UnlockSuccess();
				m_NewMoveArray.RemoveAt(num5);
				num++;
				GamePlayerPrefs.SetInt("_number_of_ended_moves", num);
				GamePlayerPrefs.SetInt("_total_number_of_ended_moves", num2 + 1);
				PlayerPrefs.DeleteKey(animationName);
				GlobalVariables.ResetEndedMoveCount();
				GlobalVariables.ComputeScore(true);
				m_NewMoveCounter = kNewMoveTimer * 4;
				m_TimeSinceLastSuccess = 0f;
				if (num > 3 && num3 == 0)
				{
					eWallpaper = OutWorld2D.EWallpaper.Wallpaper1;
				}
				else if (num > 7 && num3 == 1)
				{
					eWallpaper = OutWorld2D.EWallpaper.Wallpaper2;
				}
				else if (num > 11 && num3 == 2)
				{
					eWallpaper = OutWorld2D.EWallpaper.Wallpaper3;
				}
				else if (num > 13 && num3 == 3)
				{
					eWallpaper = OutWorld2D.EWallpaper.Wallpaper5;
				}
				if (num > 0 && num4 == 0)
				{
					eEnvironment = EEnvironment.Boy;
				}
				else if (num > 1 && num4 == 1)
				{
					eEnvironment = EEnvironment.Girl;
				}
				if (eEnvironment != EEnvironment.Count || eWallpaper != OutWorld2D.EWallpaper.Count)
				{
					if (eEnvironment != EEnvironment.Count)
					{
						m_NewEnvironment = true;
						if (m_AchievementsTable != null)
						{
							m_AchievementsTable.UnlockEnvironment(eEnvironment);
							GamePlayerPrefs.SetInt("_new_environment", num4 + 1);
							PlayExternalAudioClip(NewWallPaperSound);
						}
					}
					if (eWallpaper != OutWorld2D.EWallpaper.Count)
					{
						m_NewWallpaper = true;
						if (m_AchievementsTable != null)
						{
							m_AchievementsTable.UnlockWallpaper(eWallpaper);
							GamePlayerPrefs.SetInt("_new_wallpaper", num3 + 1);
							PlayExternalAudioClip(NewWallPaperSound);
						}
					}
				}
				else
				{
					PlayExternalAudioClip(NewMoveSound);
				}
				break;
			}
			num5++;
		}
	}

	private void ActivateShadow(int b)
	{
		if (PlayCallBack("ActivateShadow"))
		{
			ActivateShadow(b > 0);
		}
	}

	private void ActivateShadow1(int b)
	{
		if (PlayCallBack("ActivateShadow1") && m_FakeShadow1 != null)
		{
			m_FakeShadow1.gameObject.SetActiveRecursively(b > 0);
		}
	}

	private void ActivateShadow2(int b)
	{
		if (PlayCallBack("ActivateShadow2") && m_FakeShadow2 != null)
		{
			m_FakeShadow2.gameObject.SetActiveRecursively(b > 0);
		}
	}

	private void FrontBouncingShadow()
	{
		if (PlayCallBack("FrontBouncingShadow") && m_CurrentAnimation == "screen_frontbouncing")
		{
			ActivateShadow(false);
		}
	}

	private void PlayYellSound()
	{
		if (PlayCallBack("PlayYellSound") && m_CurrentAnimation.Contains("yell"))
		{
			LoadSound("Izi/", ESound.Rabbid_Baaah);
			PlaySound(ESound.Rabbid_Baaah);
		}
	}

	private void PlayPafSound()
	{
		if (PlayCallBack("PlayPafSound") && m_CurrentAnimation.Contains("bounce"))
		{
			switch (Random.Range(1, 8))
			{
			case 1:
				LoadSound("Izi/", ESound.IZI_Screen_Bouncing_back_to_front1);
				PlaySound(ESound.IZI_Screen_Bouncing_back_to_front1);
				break;
			case 2:
				LoadSound("Izi/", ESound.IZI_Screen_Bouncing_back_to_front2);
				PlaySound(ESound.IZI_Screen_Bouncing_back_to_front2);
				break;
			case 3:
				LoadSound("Izi/", ESound.IZI_Wall_Horisontal_left_to_right1);
				PlaySound(ESound.IZI_Wall_Horisontal_left_to_right1);
				break;
			case 4:
				LoadSound("Izi/", ESound.IZI_Wall_Horisontal_right_to_left1);
				PlaySound(ESound.IZI_Wall_Horisontal_right_to_left1);
				break;
			case 5:
				LoadSound("Izi/", ESound.IZI_Wall_Vertical_bottom_to_top1);
				PlaySound(ESound.IZI_Wall_Vertical_bottom_to_top1);
				break;
			case 6:
				LoadSound("Izi/", ESound.IZI_Wall_Vertical_top_to_bottom1);
				PlaySound(ESound.IZI_Wall_Vertical_top_to_bottom1);
				break;
			case 7:
				LoadSound("Izi/", ESound.IZI_Screen_Bouncing_front_to_back1);
				PlaySound(ESound.IZI_Screen_Bouncing_front_to_back1);
				break;
			}
		}
	}

	private void PlayAssSound()
	{
		if (PlayCallBack("PlayAssSound") && m_CurrentAnimation.Contains("stand_part01"))
		{
			PlayIdleSound(ESound.IZI_Standby_part1);
		}
	}

	private void PlayStandByYellSound()
	{
		if (PlayCallBack("PlayStandByYellSound") && m_CurrentAnimation.Contains("stand_part02"))
		{
			PlayIdleSound(ESound.IZI_Standby_part2);
		}
	}

	private void PlayTutoSound()
	{
		if (PlayCallBack("PlayTutoSound") && m_CurrentAnimation.Contains("basic_sign"))
		{
			LoadSound("Izi/", ESound.IZI_Tutorial);
			PlaySound(ESound.IZI_Tutorial);
		}
	}

	private void PlayTutoEatSound()
	{
		if (PlayCallBack("PlayTutoEatSound"))
		{
		}
	}

	private void PlayStandby1Sound()
	{
		if (PlayCallBack("PlayStandby1Sound") && m_CurrentAnimation.Contains("stand_middle"))
		{
			if (Random.Range(0, 100) > 50)
			{
				PlayIdleSound(ESound.IZI_Standby);
			}
			else
			{
				PlayIdleSound(ESound.IZI_Standby2);
			}
		}
	}

	public void PlayStandbyShoutSound()
	{
		if (PlayCallBack("PlayStandbyShoutSound") && m_State == EState.Night)
		{
			LoadSound("Izi/", ESound.IZI_Waddle);
			PlaySound(ESound.IZI_Waddle);
		}
	}

	private void PlayRotateSound()
	{
		if (PlayCallBack("PlayRotateSound") && m_CurrentAnimation.Contains("rotate"))
		{
			int num = Random.Range(0, 30);
			if (num < 10)
			{
				LoadSound("Izi/", ESound.IZI_Rotate1);
				PlaySound(ESound.IZI_Rotate1);
			}
			else if (num < 20)
			{
				LoadSound("Izi/", ESound.IZI_Rotate2);
				PlaySound(ESound.IZI_Rotate2);
			}
			else if (num < 30)
			{
				LoadSound("Izi/", ESound.IZI_Rotate3);
				PlaySound(ESound.IZI_Rotate3);
			}
		}
	}

	private void PlaySteamSound()
	{
		if (PlayCallBack("PlaySteamSound") && m_State == EState.Steam)
		{
			LoadSound("Izi/", ESound.IZI_Steam);
			PlaySound(ESound.IZI_Steam);
		}
	}

	private void PlayGetUpSound()
	{
		if (PlayCallBack("PlayGetUpSound") && m_CurrentAnimation.Contains("fall"))
		{
			LoadSound("Izi/", ESound.IZI_Wall_Horisontal_fall_and_getup);
			PlaySound(ESound.IZI_Wall_Horisontal_fall_and_getup);
		}
	}

	private void PlayVerticalGetUpSound()
	{
		if (PlayCallBack("PlayVerticalGetUpSound") && m_CurrentAnimation.Contains("get up"))
		{
			LoadSound("Izi/", ESound.IZI_Wall_Vertical_getup);
			PlaySound(ESound.IZI_Wall_Vertical_getup);
		}
	}

	private void PlayBackGetUpSound()
	{
		if (PlayCallBack("PlayBackGetUpSound") && m_CurrentAnimation.Contains("fall_back"))
		{
			LoadSound("Izi/", ESound.IZI_Screen_Bouncing_fall_andgetup);
			PlaySound(ESound.IZI_Screen_Bouncing_fall_andgetup);
		}
	}

	private void PlayFrontBackGetUpSound()
	{
		if (PlayCallBack("PlayFrontBackGetUpSound") && m_CurrentAnimation.Contains("screen_frontfalling"))
		{
			LoadSound("Izi/", ESound.IZI_Screen_Bouncing_frontfall_andgetup);
			PlaySound(ESound.IZI_Screen_Bouncing_frontfall_andgetup);
		}
	}

	private void PlayScreenGetUpSound()
	{
		if (PlayCallBack("PlayScreenGetUpSound") && m_CurrentAnimation.Contains("screen_stand_up"))
		{
			LoadSound("Izi/", ESound.IZI_Lookdown_standup_and_wave);
			PlaySound(ESound.IZI_Lookdown_standup_and_wave);
		}
	}

	private void PlayStepBackSound()
	{
		if (PlayCallBack("PlayStepBackSound") && m_CurrentAnimation.Contains("step_back"))
		{
			LoadSound("Izi/", ESound.IZI_Lookdown_back_to_initial);
			PlaySound(ESound.IZI_Lookdown_back_to_initial);
		}
	}

	private void PlayBurpSound()
	{
		if (PlayCallBack("PlayBurpSound") && m_CurrentAnimation.Contains("burp"))
		{
			LoadSound("Izi/", ESound.IZI_Burp);
			PlaySound(ESound.IZI_Burp);
		}
	}

	private void PlayDanceSound()
	{
		if (PlayCallBack("PlayDanceSound") && m_CurrentAnimation.Contains("dance_all"))
		{
			LoadSound("Izi/", ESound.IZI_Dance3);
			PlaySound(ESound.IZI_Dance3);
		}
	}

	private void PlayKnockSound()
	{
		if (PlayCallBack("PlayKnockSound") && m_CurrentAnimation.Contains("knock"))
		{
			PlayIdleSound(ESound.IZI_Knock);
		}
	}

	private void PlayPokeEyeSound()
	{
		if (PlayCallBack("PlayPokeEyeSound") && m_CurrentAnimation.Contains("poke"))
		{
			PlaySound(ESound.IZI_Poke_hurt2);
		}
	}

	private void PlayPokeTummySound()
	{
		if (PlayCallBack("PlayPokeTummySound") && m_CurrentAnimation.Contains("poke"))
		{
			PlaySound(ESound.IZI_Poke_hurt1);
		}
	}

	private void PlayRunSound1()
	{
		if (PlayCallBack("PlayRunSound1") && m_CurrentAnimation.Contains("run"))
		{
			LoadSound("Izi/", ESound.IZI_Run_begin);
			PlaySound(ESound.IZI_Run_begin);
		}
	}

	private void PlayRunSound2()
	{
		if (PlayCallBack("PlayRunSound2") && m_CurrentAnimation.Contains("run"))
		{
			LoadSound("Izi/", ESound.IZI_Run_loop);
			PlaySound(ESound.IZI_Run_loop);
		}
	}

	private void PlayRunSound3()
	{
		if (PlayCallBack("PlayRunSound3") && m_CurrentAnimation.Contains("run"))
		{
			LoadSound("Izi/", ESound.IZI_Run_fall);
			PlaySound(ESound.IZI_Run_fall);
		}
	}

	private void PlayStringOuchSound()
	{
		if (PlayCallBack("PlayStringOuchSound") && m_CurrentAnimation.Contains("string"))
		{
			int num = Random.Range(0, 30);
			if (num < 10)
			{
				LoadSound("Izi/", ESound.IZI_String_ouch1);
				PlaySound(ESound.IZI_String_ouch1);
			}
			else if (num < 20)
			{
				LoadSound("Izi/", ESound.IZI_String_ouch2);
				PlaySound(ESound.IZI_String_ouch2);
			}
			else if (num < 30)
			{
				LoadSound("Izi/", ESound.IZI_String_ouch3);
				PlaySound(ESound.IZI_String_ouch3);
			}
		}
	}

	private void PlayYellMicroSound()
	{
		if (PlayCallBack("PlayYellMicroSound") && m_CurrentAnimation.Contains("yell"))
		{
			LoadSound("Izi/", ESound.IZI_Yell);
			PlaySound(ESound.IZI_Yell);
		}
	}

	private void PlayChokeSound()
	{
		if (PlayCallBack("PlayChokeSound") && m_CurrentAnimation.Contains("choke"))
		{
			LoadSound("Izi/", ESound.IZI_Choke);
			PlaySound(ESound.IZI_Choke);
		}
	}

	private void PlayCrazySound()
	{
		if (PlayCallBack("PlayCrazySound") && m_CurrentAnimation.Contains("standby_crazy"))
		{
			PlayIdleSound(ESound.IZI_Crazy);
		}
	}

	private void PlayGift1SoundBegin()
	{
		if (PlayCallBack("PlayGift1SoundBegin") && m_CurrentAnimation.Contains("gift01"))
		{
			LoadSound("Izi/", ESound.IZI_Gift_begin);
			PlaySound(ESound.IZI_Gift_begin);
		}
	}

	private void PlayGift1SoundMiddle()
	{
		if (PlayCallBack("PlayGift1SoundMiddle") && m_CurrentAnimation.Contains("gift01"))
		{
			LoadSound("Izi/", ESound.IZI_Gift_smile);
			PlaySound(ESound.IZI_Gift_smile);
		}
	}

	private void PlayGift1SoundLoop()
	{
		if (PlayCallBack("PlayGift1SoundLoop") && m_CurrentAnimation.Contains("gift01") && ++m_LoopIdleIndex % 3 == 0)
		{
			LoadSound("Izi/", ESound.IZI_Gift_loop);
			PlaySound(ESound.IZI_Gift_loop);
		}
	}

	private void PlayGift1SoundEnd()
	{
		if (PlayCallBack("PlayGift1SoundEnd") && m_CurrentAnimation.Contains("gift01"))
		{
			LoadSound("Izi/", ESound.IZI_Gift_open);
			PlaySound(ESound.IZI_Gift_open);
		}
	}

	private void PlayGift2SoundBegin()
	{
		if (PlayCallBack("PlayGift2SoundBegin") && m_CurrentAnimation.Contains("gift02"))
		{
			LoadSound("Izi/", ESound.IZI_GiftDuck_open);
			PlaySound(ESound.IZI_GiftDuck_open);
		}
	}

	private void PlayGift2SoundMiddle()
	{
		if (PlayCallBack("PlayGift2SoundMiddle") && m_CurrentAnimation.Contains("gift02"))
		{
			LoadSound("Izi/", ESound.IZI_GiftDuck_wait);
			PlaySound(ESound.IZI_GiftDuck_wait);
		}
	}

	private void PlayGift2SoundThrow()
	{
		if (PlayCallBack("PlayGift2SoundThrow") && m_CurrentAnimation.Contains("gift02"))
		{
			LoadSound("Izi/", ESound.IZI_GiftDuck_throw);
			PlaySound(ESound.IZI_GiftDuck_throw);
		}
	}

	private void PlayGift2SoundEnd()
	{
		if (PlayCallBack("PlayGift2SoundEnd") && m_CurrentAnimation.Contains("gift02"))
		{
			LoadSound("Izi/", ESound.IZI_GiftDuck_hit);
			PlaySound(ESound.IZI_GiftDuck_hit);
		}
	}

	public void PlayNightSound()
	{
		if (PlayCallBack("PlayNightSound") && m_State == EState.Night)
		{
			LoadSound("Izi/", ESound.IZI_lightoff);
			PlaySound(ESound.IZI_lightoff);
		}
	}

	private void PlayYellNearSound()
	{
		if (PlayCallBack("PlayYellNearSound") && m_CurrentAnimation == "standby_cri_de_pres")
		{
			PlayIdleSound(ESound.IZI_Yell_Near);
		}
	}

	private void PlayTurnHitSound()
	{
		if (PlayCallBack("PlayTurnHitSound") && m_CurrentAnimation == "turn")
		{
			LoadSound("Izi/", ESound.IZI_Turn_hit1);
			PlaySound(ESound.IZI_Turn_hit1);
		}
	}

	private void PlayExternalAudioClip(AudioClip clip)
	{
		/*if (!AudioPlayerManager.s_IsDeviceMuted && GlobalVariables.SoundEnabled != 0)
		{
			if (clip != null)
			{
				base.GetComponent<AudioSource>().volume = 0.5f;
				base.GetComponent<AudioSource>().PlayOneShot(clip);
				Utility.Log(ELog.Sound, clip.name);
			}
			else
			{
				Utility.Log(ELog.Errors, "PlayExternalAudioClip tries to play a Null clip");
			}
		}*/
	}

	private void LoadSound(string path, ESound sndId)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			m_SoundPlayer.LoadClip(path, sndId.ToString());
		}
	}

	private void PlaySound(ESound sndId)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, 1f);
		}
	}

	private void PlaySound(ESound sndId, float volume)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, volume, false);
		}
	}

	private void PlaySound(ESound sndId, float volume, bool loop)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			m_SoundPlayer.PlaySound(sndId, volume, loop);
		}
	}

	private void PlaySound(ERabbidSound sndId)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, 1f);
		}
	}

	private void PlaySound(ERabbidSound sndId, float volume)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, volume, false);
		}
	}

	private void PlaySound(ERabbidSound sndId, float volume, bool loop)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			m_SoundPlayer.PlaySound(sndId, volume, loop);
		}
	}

	private void PlaySound(EBwahSound sndId)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, 1f);
		}
	}

	private void PlaySound(EBwahSound sndId, float volume)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			PlaySound(sndId, volume, false);
		}
	}

	private void PlaySound(EBwahSound sndId, float volume, bool loop)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */m_SoundPlayer != null)
		{
			m_SoundPlayer.PlaySound(sndId, volume, loop);
		}
	}

	private void StopSound()
	{
		if (m_SoundPlayer != null)
		{
			m_SoundPlayer.Stop();
		}
		base.GetComponent<AudioSource>().Stop();
		//AudioPlayerBinding.stop(false);
	}

	private void PauseSound(bool b)
	{
		if (b)
		{
			if (++m_PauseSoundCounter == 1)
			{
				if (m_SoundPlayer != null)
				{
					m_SoundPlayer.Pause(true);
				}
				//AudioPlayerBinding.pause();
				base.GetComponent<AudioSource>().Pause();
				Utility.Log(ELog.Sound, "Pause");
			}
		}
		else if (m_PauseSoundCounter > 0 && --m_PauseSoundCounter == 0)
		{
			if (m_SoundPlayer != null)
			{
				m_SoundPlayer.Pause(false);
			}
			//AudioPlayerBinding.play();
			base.GetComponent<AudioSource>().Play();
			Utility.Log(ELog.Sound, "Resume");
		}
	}

	private void PlayIdleSound(ESound sndId)
	{
		if (GlobalVariables.SoundEnabled != 0 && /*!AudioPlayerManager.s_IsDeviceMuted && */!IsPaused())
		{
			LoadSound("Idle/", sndId);
			PlaySound(sndId);
		}
	}

	private void ClearAudioClips()
	{
		if (m_SoundPlayer != null)
		{
			m_SoundPlayer.ClearClips();
		}
	}

	public static ESound GetSoundEnum(string str)
	{
		for (ESound eSound = ESound.IZI_Poke_hurt1; eSound < ESound.Count; eSound++)
		{
			if (str.Equals(eSound.ToString()))
			{
				return eSound;
			}
		}
		return ESound.Count;
	}

	public static ESound GetIdleSoundEnum(string str)
	{
		for (ESound eSound = ESound.IZI_Poke_hurt1; eSound < ESound.Idle_Count; eSound++)
		{
			if (str.Equals(eSound.ToString()))
			{
				return eSound;
			}
		}
		return ESound.Count;
	}

	private bool PlayCallBack(string cb)
	{
		if (m_SoundTableCallback.Contains(cb))
		{
			return false;
		}
		m_SoundTableCallback.Add(cb, 0);
		return true;
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
			m_TextContent = m_CommonSkin.GetStyle("BigTextContent");
		}
		else
		{
			m_PriceTextStyle = "PriceText";
			m_TextContent = m_CommonSkin.GetStyle("TextContent");
		}
		if (Utility.IsFourthGenAppleDevice())
		{
			m_TextContent = m_CommonSkin.GetStyle("HugeTextContent");
		}
	}

	private void StoreGUI()
	{
		if (m_Store)
		{
			m_FixedWidth = m_TextContent.fixedWidth;
			m_TextContent.fixedWidth = m_StoreDescriptionRect.width - 45f;
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
					m_BuyPack = EPack.Count;
					PlayExternalAudioClip(BackSound);
					ShowStore(false);
					Pause(false);
				}
			}
			EProductID productIDEnumWithStoreProductID = Utility.GetProductIDEnumWithStoreProductID(PLACEHOLDERS.str /*m_ProductList[0].productIdentifier*/);
			string text = productIDEnumWithStoreProductID.ToString();
			GUI.Label(m_StoreTitleRect, Localization.GetLocalizedText(text + "_Title"), "BlackTextTitle");
			GUI.skin = m_OptionsSkin;
			GUILayout.BeginArea(m_StoreDescriptionRect);
			m_DescriptionScrollPos = GUILayout.BeginScrollView(m_DescriptionScrollPos);
			GUILayout.Label(Localization.GetLocalizedText(text + "_Describe"), m_TextContent);
			GUILayout.EndScrollView();
			GUILayout.EndArea();
			GUI.skin = m_CommonSkin;
			if (productIDEnumWithStoreProductID < EProductID.AvailableCount)
			{
				if (Utility.IsUnlockedProduct(productIDEnumWithStoreProductID))
				{
					GUI.Label(m_StoreBuyRect, Localization.GetLocalizedText(ELoc.PurchaseInstalled), "TextTitle");
				}
				else
				{
					LoadMoneySymbol(PLACEHOLDERS.str /*m_ProductList[0].currencySymbol*/);
					GUI.Label(m_StorePriceRect, PLACEHOLDERS.str /*m_ProductList[0].price*/, m_PriceTextStyle);
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
			m_TextContent.fixedWidth = m_FixedWidth;
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
		m_ScrollState = EScrollState.Idle;
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Active = !b;
		}
		m_Store = b;
		if (b)
		{
			SetPlaneTexture("GUI/DLC_buy");
			DataMining.Increment(MainMenuScript.EMenu.Store.ToString());
		}
		else
		{
			ClearPlaneTexture();
		}
		Pause(b);
	}

	private void ShowMessage()
	{
		switch (m_Message)
		{
		case EMessage.EmptyStore:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.EmptyStore)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.ProductListRequestFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.ProductListRequestFailed)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseFailed)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseFailedClientInvalid:
			if (GUIUtils.DrawError("Client is Invalid"))
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
		case EMessage.NetNotReachable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.YouNeedInternet)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseDisable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseDisable)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseSuccess:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseSucceed)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.ImgSizeError:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.ImgSizeError)))
			{
				SetMessage(EMessage.None);
			}
			break;
		}
		if (m_Message == EMessage.None && !m_Store)
		{
			Pause(false);
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
			}
			m_CurrencySymbol = symbol;
		}
	}

	private void StartStoreKit()
	{
	}

	private void OnDisableStoreKit()
	{
	}

	private void Request(EProductID product)
	{
		if (!Utility.IsAvailableProduct(product))
		{
			Utility.Log(ELog.Errors, "Product not available");
			return;
		}
		switch (product)
		{
		case EProductID.historypack:
			m_BuyPack = EPack.History;
			break;
		case EProductID.moustachpack:
			m_BuyPack = EPack.Moustach;
			break;
		}
		Pause(true);
		StopAnim();
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
}
