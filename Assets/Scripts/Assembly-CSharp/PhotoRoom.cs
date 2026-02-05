using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotoRoom : Rabbid
{
	public enum EARPose
	{
		pose01 = 0,
		pose00 = 1,
		pose04 = 2,
		pose06 = 3,
		pose07 = 4,
		pose08 = 5,
		pose09 = 6,
		pose10 = 7,
		pose11 = 8,
		pose12 = 9,
		pose13 = 10,
		pose14 = 11,
		pose15 = 12,
		pose17 = 13,
		pose18 = 14,
		pose20 = 15,
		pose22 = 16,
		pose24 = 17,
		pose25 = 18,
		pose26 = 19,
		pose27 = 20,
		pose28 = 21,
		pose30 = 22,
		pose32 = 23,
		Count = 24
	}

	public enum EAREighty
	{
		BTS_AW3D_RAB08_0002_Resized_PhotoMontage = 0,
		Count = 1
	}

	public enum EBackMode
	{
		CameraBack = 0,
		CameraFront = 1,
		Photo = 2
	}

	public enum EFrontMode
	{
		Pose = 0,
		Eighties = 1
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

	private const float m_AutoHideTime = 2f;

	public AudioClip BackSound;

	public GUISkin m_ScrollerSkin;

	public GUISkin m_InGameSkin;

	public GUISkin m_OptionsSkin;

	private Texture2D m_NewTexture_110x128;

	private static int s_StartOrientation;

	private static int s_LastValidOrientation;

	private static int s_TmpLastValidOrientation;

	private GameObject m_IntroLoadingScreen;

	private GameObject m_OutroLoadingScreen;

	private int m_FixedUpdateFrameCount;

	private int m_UpdateFrameCount;

	private string m_NextSceneName = string.Empty;

	private int m_FrameCountBeforeNextScene = -1;

	private bool m_ImagePicked;

	private bool m_CanUsePicker = true;

	private Fonctionality m_Fonctionality;

	private FonctionalityItem m_FonctionalityRoot;

	private MeshMerger m_FonctionalityMerger;

	private Fader m_Flash;

	private float m_FlashDuration = 0.1f;

	private bool m_GoToInGame;

	private bool m_GoToMainMenu;

	private int m_BackPressed = -1;

	private bool m_DisplayLoadPictureText;

	private Vector3 m_LoadPictureOffset = new Vector3(10f, 0f, 0f);

	private Camera m_Camera_3D;

	private Camera m_Camera_2D;

	private GameObject m_Plane;

	private Quaternion m_PlaneRotation = Quaternion.identity;

	private Quaternion m_PlaneRotationDephase = Quaternion.identity;

	private bool m_IsPlaneDephase;

	private GameObject m_RabbidShadow;

	private static int s_ReverseChangeFrameCount;

	public AudioClip PhotoSound;

	public AudioClip ValidSound;

	private Texture2D m_MenuLastPhoto;

	private GameObject m_PlaneCapture;

	private GameObject m_PlaneEighties;

	private EBackMode m_BackMode;

	private EFrontMode m_FrontMode;

	private Vector3 m_BackCameraScale = Vector3.zero;

	private Vector3 m_FrontCameraScale = Vector3.zero;

	private Vector3 m_PhotoScale = Vector3.zero;

	private Vector3 m_PhotoScale90 = Vector3.zero;

	private Quaternion m_CameraRotation = Quaternion.identity;

	private Quaternion m_CameraRotationDephase = Quaternion.identity;

	private Quaternion m_PhotoRotation = Quaternion.identity;

	private Quaternion m_PhotoRotation90 = Quaternion.identity;

	private int m_LastTouchCount;

	private float m_RefAngle;

	private Vector3 m_TouchWorldPos = Vector3.zero;

	private float m_RefScale = 1f;

	private float m_MaxScale = 5f;

	private float m_MinScale = 0.5f;

	private Rect m_TransRect = new Rect(-2f, -3f, 4f, 6f);

	private Vector3 m_DefaultPosition = Vector3.zero;

	private Vector3 m_DefaultScale = Vector3.zero;

	private Vector3 m_HidePosition = Vector3.zero;

	private Vector3 m_PhotoPos = new Vector3(0f, 0f, 5.4f);

	private bool m_TakePhoto;

	private Texture2D m_Screenshot;

	private string m_PhotoPath = string.Empty;

	private string m_MiniPhotoPath = string.Empty;

	private int m_PhotoNumber;

	private float m_MediaPickerTime = 2f;

	private float m_Y;

	private float m_Z;

	private bool m_CanMakePayments;

	private GUIScroller m_Scroller;

	private List<Texture2D> m_AllThumbnails = new List<Texture2D>();

	private bool m_DrawScroller = true;

	private int m_ARIndex;

	private float m_AutoHideTimer = -1f;

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

	public override void Start()
	{
		Utility.ShowActivityView(false);
		base.Start();
		Screen.sleepTimeout = 0;
		s_StartOrientation = (int)Utility.LastDeviceOrientation;
		if (s_StartOrientation != 2)
		{
			s_StartOrientation = 1;
		}
		s_LastValidOrientation = s_StartOrientation;
		m_NewTexture_110x128 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/" + Localization.GetLanguage().ToString() + "/new_110x128");
		if (m_NewTexture_110x128 == null)
		{
			Utility.Log(ELog.Errors, "m_NewTexture_110x128 == null");
		}
		m_IntroLoadingScreen = Utility.CreateLoadingScreen();
		if (m_IntroLoadingScreen == null)
		{
			Utility.Log(ELog.Errors, "m_IntroLoadingScreen == null");
		}
		SwapCostume(Rabbid.GetCostumeEnum(PlayerPrefs.GetString("CostumeName")));
		for (EARPose eARPose = EARPose.pose01; eARPose < EARPose.Count; eARPose++)
		{
			AddPonctualAnimationClip("BunnyARPoses/", eARPose.ToString());
		}
		StartEtcetera();
		StartPlugins();
		StartHelper();
		StartHUD();
		StartStore();
		StartStoreKit();
		StartScroller();
		StartPhoto();
		StartWithOrientation();
		RenderSettings.fog = false;
	}

	private void StartWithOrientation()
	{
		if (ReverseScreen())
		{
			if (m_IntroLoadingScreen != null)
			{
				m_IntroLoadingScreen.transform.localEulerAngles = new Vector3(270f, 180f, 0f);
			}
			SetR(new Vector3(0f, 0f, 180f));
		}
		else
		{
			if (m_IntroLoadingScreen != null)
			{
				m_IntroLoadingScreen.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			SetR(new Vector3(0f, 0f, 0f));
		}
	}

	public override void Update()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			return;
		}
		base.Update();
		if (m_IntroLoadingScreen != null)
		{
			StartWithOrientation();
			if (++m_UpdateFrameCount >= 50 && m_FixedUpdateFrameCount >= 50)
			{
				Object.DestroyImmediate(m_IntroLoadingScreen);
				m_IntroLoadingScreen = null;
				Utility.FreeMem();
				m_Fonctionality.Active = true;
				m_Fonctionality.Next(m_FonctionalityRoot.GetChild(FonctionalityItem.EType.AR));
				m_Fonctionality.SwitchRoot();
				ActivateScroller();
				Utility.ClearActivityView();
				if (AllInput.IsCaptureAvailable())
				{
					AllInput.StopCameraCapture();
					AllInput.StartCameraCapture(m_BackMode == EBackMode.CameraFront, m_PlaneCapture.GetComponent<Renderer>().material);
				}
			}
		}
		else
		{
			UpdateScrolling();
			UpdateStore();
			UpdateHUD();
			UpdatePhoto();
			UpdateHelper();
		}
	}

	private void FixedUpdate()
	{
		m_FixedUpdateFrameCount++;
		if (m_FrameCountBeforeNextScene > 0)
		{
			m_FrameCountBeforeNextScene--;
			if (m_FrameCountBeforeNextScene == 0)
			{
				Localization.GenerateWaitText();
				Application.LoadLevel(m_NextSceneName);
			}
		}
		else if (m_BackPressed >= 0)
		{
			m_BackPressed--;
		}
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
			ReverseScreen();
		}
		else if (!m_CanUsePicker)
		{
			Utility.Log(ELog.Info, "OnApplicationPause");
			m_CanUsePicker = true;
			m_MediaPickerTime = 0f;
		}
	}

	private void OnDisable()
	{
		OnDisableScroller();
		OnDisableHUD();
		OnDisableStoreKit();
		OnDisableEtcetera();
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
		if (!m_Store)
		{
			HUDGUI();
			if (m_IntroLoadingScreen != null || m_OutroLoadingScreen != null)
			{
				GUIUtils.DrawLoadingText();
			}
			else if (!m_DisplayLoadPictureText)
			{
				PhotoGUI();
				ScrollerGUI();
			}
		}
	}

	private void GoToInGame()
	{
		if (!m_GoToMainMenu && !m_GoToInGame)
		{
			Utility.ShowActivityView(false);
			m_GoToInGame = true;
			m_Fonctionality.Active = false;
			m_OutroLoadingScreen = Utility.CreateLoadingScreen();
			if (m_OutroLoadingScreen == null)
			{
				Utility.Log(ELog.Errors, "m_OutroLoadingScreen == null");
			}
			HideRabbid();
		}
	}

	private void GoToMainMenu()
	{
		if (!m_GoToMainMenu && !m_GoToInGame)
		{
			Utility.ShowActivityView(false);
			m_GoToMainMenu = true;
			m_Fonctionality.Active = false;
			m_OutroLoadingScreen = Utility.CreateLoadingScreen();
			if (m_OutroLoadingScreen == null)
			{
				Utility.Log(ELog.Errors, "m_OutroLoadingScreen == null");
			}
			HideRabbid();
		}
	}

	private void StartEtcetera()
	{
		/*EtceteraManager.imagePickerChoseImage += OnPickerChooseImage;
		EtceteraManager.imagePickerCancelled += OnPickerCancelled;
		EtceteraManager.dismissView += OnDismissView;
		EtceteraManager.releasedView += OnReleaseView;
		EtceteraAndroidManager.albumChooserSucceededEvent += OnPickerChooseImageDroid;
		EtceteraAndroidManager.albumChooserCancelledEvent += OnPickerCancelled;
		EtceteraAndroidManager.photoChooserSucceededEvent += OnPickerChooseImageDroid;
		EtceteraAndroidManager.photoChooserCancelledEvent += OnPickerCancelled;*/
	}

	private void OnDisableEtcetera()
	{
		/*EtceteraManager.imagePickerChoseImage -= OnPickerChooseImage;
		EtceteraManager.imagePickerCancelled -= OnPickerCancelled;
		EtceteraManager.dismissView -= OnDismissView;
		EtceteraManager.releasedView -= OnReleaseView;
		EtceteraAndroidManager.albumChooserSucceededEvent -= OnPickerChooseImageDroid;
		EtceteraAndroidManager.albumChooserCancelledEvent -= OnPickerCancelled;
		EtceteraAndroidManager.photoChooserSucceededEvent -= OnPickerChooseImageDroid;
		EtceteraAndroidManager.photoChooserCancelledEvent -= OnPickerCancelled;*/
	}

	private void OnDismissView()
	{
		Utility.ShowActivityView(true);
		if (!m_ImagePicked)
		{
			Utility.Log(ELog.Info, "OnDismissView");
		}
	}

	private void OnReleaseView()
	{
		Utility.Log(ELog.Info, "OnReleaseView");
		m_CanUsePicker = true;
		m_MediaPickerTime = 0f;
	}

	private void OnPickerChooseImageDroid(string text, Texture2D tex)
	{
		Utility.Log(ELog.Info, "OnPickerChooseImage");
		if (text == "size_error")
		{
			Utility.Log(ELog.Info, "size_error");
			OnPickerCancelled();
			SetMessage(EMessage.ImgSizeError);
		}
		else if (tex != null)
		{
			m_ImagePicked = true;
			if (m_PlaneCapture != null)
			{
				m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = null;
				Utility.FreeMem(true);
			}
			m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = tex;
			SwitchBackMode(EBackMode.Photo, true);
			m_TakePhoto = true;
			AllInput.StopCameraCapture();
		}
		else
		{
			if (m_PlaneCapture != null)
			{
				m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = null;
				Utility.FreeMem(true);
			}
			m_ImagePicked = true;
			Utility.CallWWW(this, "file://" + text, OnCallPickerSuccess, OnCallPickerError);
		}
		OnEndImagePicker();
	}

	private void OnPickerChooseImage(string text)
	{
		Utility.Log(ELog.Info, "OnPickerChooseImage");
		if (text == "size_error")
		{
			Utility.Log(ELog.Info, "size_error");
			OnPickerCancelled();
			SetMessage(EMessage.ImgSizeError);
			return;
		}
		if (m_PlaneCapture != null)
		{
			m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = null;
			Utility.FreeMem(true);
		}
		m_ImagePicked = true;
		Utility.CallWWW(this, "file://" + text, OnCallPickerSuccess, OnCallPickerError);
	}

	private void OnPickerChooseImage(Texture2D tex)
	{
		Utility.Log(ELog.Info, "OnPickerChooseImage");
		if (tex != null)
		{
			m_ImagePicked = true;
			if (m_PlaneCapture != null)
			{
				m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = null;
				Utility.FreeMem(true);
			}
			m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = tex;
			SwitchBackMode(EBackMode.Photo, true);
			m_TakePhoto = true;
		}
		OnEndImagePicker();
	}

	private void OnPickerCancelled()
	{
		Utility.Log(ELog.Info, "OnPickerCancelled");
		m_ImagePicked = true;
		OnErrorImagePicker();
	}

	private void ShowImagePicker()
	{
		if (m_CanUsePicker && !(m_MediaPickerTime < 2f))
		{
			Utility.Log(ELog.Info, "ShowImagePicker");
			m_CanUsePicker = false;
			m_ImagePicked = false;
			AllInput.PauseCameraCapture();
			Utility.FreeMem(true);
			PromptForPhoto();
		}
	}

	private void OnCallPickerSuccess(WWW www, object[] list)
	{
		Utility.Log(ELog.Info, "OnCallPickerSuccess");
		Texture2D texture = www.texture;
		m_PlaneCapture.GetComponent<Renderer>().material.mainTexture = texture;
		SwitchBackMode(EBackMode.Photo, true);
		m_TakePhoto = true;
		texture = null;
		AllInput.StopCameraCapture();
		OnEndImagePicker();
	}

	private void OnCallPickerError(object[] list)
	{
		Utility.Log(ELog.Info, "OnCallPickerError");
		OnErrorImagePicker();
	}

	private void OnErrorImagePicker()
	{
		Utility.Log(ELog.Info, "OnErrorImagePicker");
		if (m_BackMode != EBackMode.Photo && !m_TakePhoto)
		{
			AllInput.ResumeCameraCapture();
		}
		OnEndImagePicker();
	}

	private void OnEndImagePicker()
	{
		Utility.Log(ELog.Info, "OnEndImagePicker");
		Utility.HideActivityView(true);
		Utility.FreeMem();
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
		Fader.Shown += OnFaderShown;
		Fader.Hided += OnFaderHided;
		Fonctionality.RootPressed += OnFonctionalityRootPressed;
		Fonctionality.ItemPressed += OnFonctionalityItemPressed;
		Fonctionality.Closed += OnFonctionalityClosed;
		m_DisplayLoadPictureText = !AllInput.IsCaptureAvailable();
		Utility.Log(ELog.Device, "AllInput.IsCaptureAvailable: " + AllInput.IsCaptureAvailable());
		Utility.Log(ELog.Device, "m_DisplayLoadPictureText: " + m_DisplayLoadPictureText);
		if (m_DisplayLoadPictureText)
		{
			m_FonctionalityMerger.transform.localPosition += m_LoadPictureOffset;
			m_FonctionalityRoot.transform.localPosition += m_LoadPictureOffset;
			m_FonctionalityMerger.MergeMeshes();
		}
	}

	private void UpdateHUD()
	{
		if (m_FonctionalityMerger != null && m_Fonctionality.ResfreshMeshMerger())
		{
			m_FonctionalityMerger.MergeMeshes();
			m_Fonctionality.SetMergeMeshesFinished();
		}
		if (m_GoToInGame)
		{
			if (GlobalVariables.SoundEnabled == 1)
			{
				base.GetComponent<AudioSource>().Stop();
				base.GetComponent<AudioSource>().clip = BackSound;
				base.GetComponent<AudioSource>().Play();
				base.GetComponent<AudioSource>().loop = false;
			}
			StopAnim();
			SetNextLevel("InGame", true);
		}
		else if (m_GoToMainMenu)
		{
			if (GlobalVariables.SoundEnabled == 1)
			{
				base.GetComponent<AudioSource>().Stop();
				base.GetComponent<AudioSource>().clip = BackSound;
				base.GetComponent<AudioSource>().Play();
				base.GetComponent<AudioSource>().loop = false;
			}
			StopAnim();
			SetNextLevel("MainMenu", true);
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
		if (!(m_IntroLoadingScreen == null) || !(m_OutroLoadingScreen == null))
		{
			return;
		}
		if (Utility.InputStoppedByActivity())
		{
			GUIUtils.DrawFictiveBackButton();
		}
		else if (GUIUtils.DrawBackButton() && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			if (GlobalVariables.s_PhotoFromMainMenu)
			{
				GoToMainMenu();
			}
			else
			{
				GoToInGame();
			}
		}
		if (m_DisplayLoadPictureText && GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.NoCamera)))
		{
			m_DisplayLoadPictureText = false;
			m_FonctionalityRoot.transform.localPosition -= m_LoadPictureOffset;
			m_FonctionalityMerger.transform.localPosition -= m_LoadPictureOffset;
			m_FonctionalityMerger.MergeMeshes();
		}
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
		if (type == FonctionalityItem.EType.ARRoot)
		{
			m_Fonctionality.Next(m_FonctionalityRoot.GetChild(FonctionalityItem.EType.AR));
			m_Fonctionality.SwitchRoot();
		}
	}

	private void OnFonctionalityRootPressed(int ID, FonctionalityItem.EType type)
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

	private void OnFonctionalityItemPressed(int ID, FonctionalityItem.EType item, FonctionalityItem.EType root)
	{
		m_Fonctionality.RefreshMerger();
		switch (item)
		{
		case FonctionalityItem.EType.AR:
			break;
		case FonctionalityItem.EType.Back:
			break;
		case FonctionalityItem.EType.OpenLibrary:
			ShowImagePicker();
			break;
		case FonctionalityItem.EType.SwitchCamera:
			m_TakePhoto = false;
			if (m_BackMode != EBackMode.CameraBack)
			{
				SwitchBackMode(EBackMode.CameraBack);
			}
			else
			{
				SwitchBackMode(EBackMode.CameraFront);
			}
			break;
		case FonctionalityItem.EType.LastPicture:
		case FonctionalityItem.EType.LastScreenshot:
			GamePlayerPrefs.SetInt("GotoPhoto", m_PhotoNumber - 1);
			DataMining.Increment(MainMenuScript.EMenu.OutWorld.ToString());
			SetNextLevel("OutWorld2D", false);
			break;
		case FonctionalityItem.EType.ShadowOn:
			ActivateShadow(false);
			m_Fonctionality.SetType(FonctionalityItem.EType.ShadowOff, item);
			break;
		case FonctionalityItem.EType.ShadowOff:
			ActivateShadow(true);
			m_Fonctionality.SetType(FonctionalityItem.EType.ShadowOn, item);
			break;
		case FonctionalityItem.EType.Valid:
			switch (root)
			{
			case FonctionalityItem.EType.ARRoot:
				ValidatePhoto();
				break;
			case FonctionalityItem.EType.PauseRoot:
				PlayExternalAudioClip(PhotoSound);
				ValidatePhoto();
				break;
			}
			break;
		case FonctionalityItem.EType.ARRoot:
		case FonctionalityItem.EType.MusicOn:
		case FonctionalityItem.EType.MusicOff:
		case FonctionalityItem.EType.Pause:
		case FonctionalityItem.EType.PauseRoot:
		case FonctionalityItem.EType.Screenshot:
		case FonctionalityItem.EType.Progress:
			break;
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

	private void StartHelper()
	{
		FindCamera();
		FindShadow();
		m_Plane = Utility.LoadGameObject("GUI/PlaneDLC", m_Camera_2D.transform);
		Utility.SetLayerRecursivly(m_Plane.transform, 11);
		m_Plane.active = false;
		m_PlaneRotation = Quaternion.Euler(90f, 180f, 0f);
		m_PlaneRotationDephase = Quaternion.Euler(270f, 0f, 0f);
	}

	private void UpdateHelper()
	{
		UpdatePlaneOrientation();
	}

	private void HelperGUI()
	{
	}

	public static bool ReverseScreen()
	{
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

	private void FindCamera()
	{
		GameObject gameObject = GameObject.Find("Main Camera");
		if (gameObject == null)
		{
			gameObject = GameObject.Find("Camera_3D");
		}
		if (gameObject != null)
		{
			m_Camera_3D = gameObject.GetComponent<Camera>();
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

	private void FindShadow()
	{
		Transform transform = base.transform.Find("Shadows");
		if (transform != null)
		{
			m_RabbidShadow = transform.gameObject;
			if (m_RabbidShadow == null)
			{
				Utility.Log(ELog.Errors, "Shadows not found");
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Shadows root not found");
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

	private void ActivateShadow(bool b)
	{
		if (m_RabbidShadow != null)
		{
			m_RabbidShadow.SetActiveRecursively(b);
		}
	}

	private void StartPhoto()
	{
		m_PlaneCapture = Utility.LoadGameObject("Misc/PlaneCapture", base.transform);
		m_PlaneEighties = Utility.LoadGameObject("Misc/PlaneEighties", base.transform);
		SetAsCameraSon(m_PlaneCapture);
		SetAsCameraSon(m_PlaneEighties);
		ActivatePhotoObjects(false);
		string text = Utility.GetPersistentDataPath() + "/";
		m_PhotoPath = text + GlobalVariables.s_PhotoPath + "/";
		m_MiniPhotoPath = text + GlobalVariables.s_MiniPhotoPath + "/";
		if (Utility.GetResolution() == EResolution.IPad)
		{
			Vector3 localScale = m_PlaneCapture.transform.localScale;
			localScale.z = -18f;
			m_PlaneCapture.transform.localScale = localScale;
		}
		m_CameraRotation = Quaternion.Euler(0f, 270f, 90f);
		m_CameraRotationDephase = Quaternion.Euler(0f, 90f, 270f);
		m_FrontCameraScale = (m_BackCameraScale = m_PlaneCapture.transform.localScale);
		m_FrontCameraScale.z *= -1f;
		m_BackCameraScale.z *= -1f;
		m_FrontCameraScale.x *= -1f;
		m_PhotoScale = new Vector3(m_FrontCameraScale.z, m_FrontCameraScale.y, m_FrontCameraScale.x);
		m_PhotoRotation = Quaternion.Euler(270f, 0f, 0f);
		m_PhotoScale90 = new Vector3(m_FrontCameraScale.x, m_FrontCameraScale.y, m_FrontCameraScale.z);
		m_PhotoRotation90 = Quaternion.Euler(0f, 90f, 270f);
		m_HidePosition = -m_PhotoPos;
		StartPhotoMode();
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

	private void StartPhotoMode()
	{
		if (m_PlaneCapture != null)
		{
			m_PlaneCapture.SetActiveRecursively(true);
		}
		RecreateARPosesThumbnailsBuffer();
		SwitchBackMode(EBackMode.CameraBack);
		m_ARIndex = 1;
		StopAnim();
		if (AllInput.IsCaptureAvailable())
		{
			AllInput.StartCameraCapture(m_BackMode == EBackMode.CameraFront, m_PlaneCapture.GetComponent<Renderer>().material);
		}
		else
		{
			SwitchBackMode(EBackMode.Photo);
			m_PlaneCapture.transform.localScale = m_PhotoScale;
			m_PlaneCapture.transform.localEulerAngles = m_PhotoRotation.eulerAngles;
		}
		if (m_Fonctionality != null)
		{
			m_Fonctionality.Wait = false;
		}
		SelectARPose(true, false);
		if (m_Scroller != null)
		{
			m_Scroller.SetVisible(false);
		}
	}

	private void UpdatePhoto()
	{
		m_MediaPickerTime += Time.deltaTime;
		if (m_Scroller == null)
		{
			return;
		}
		GUIRetractableScroller gUIRetractableScroller = (GUIRetractableScroller)m_Scroller;
		if (!Utility.InputStoppedByActivity())
		{
			if (m_FrontMode != EFrontMode.Eighties)
			{
				base.Update();
				UpdateRabbid();
			}
			gUIRetractableScroller.Update();
		}
		else if (gUIRetractableScroller.GetState() != GUIRetractableScroller.EState.Hided)
		{
			gUIRetractableScroller.Update();
		}
	}

	private void PhotoGUI()
	{
		if (AllInput.IsCaptureAvailable())
		{
			float num = 55f;
			float height = 55f;
			GUISkin skin = GUI.skin;
			GUI.skin = m_InGameSkin;
			GUIStyle style = (m_TakePhoto ? ((GUIStyle)"TakePhotoButtonGreyed") : ((GUIStyle)"TakePhotoButton"));
			Rect position = Utility.NewRect(Utility.RefWidth - num, Utility.RefHeight * 0.67f, num, height);
			if (GUI.Button(position, string.Empty, style) && !Utility.InputStoppedByActivity())
			{
				PlayExternalAudioClip(PhotoSound);
				TakePhoto();
			}
			GUI.skin = skin;
		}
	}

	public static EARPose GetARPoseEnum(string str)
	{
		for (EARPose eARPose = EARPose.pose01; eARPose < EARPose.Count; eARPose++)
		{
			if (str.Equals(eARPose.ToString()))
			{
				return eARPose;
			}
		}
		return EARPose.Count;
	}

	public static EAREighty GetAREightyEnum(string str)
	{
		for (EAREighty eAREighty = EAREighty.BTS_AW3D_RAB08_0002_Resized_PhotoMontage; eAREighty < EAREighty.Count; eAREighty++)
		{
			if (str.Equals(eAREighty.ToString()))
			{
				return eAREighty;
			}
		}
		return EAREighty.Count;
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

	private void ActivatePhotoObjects(bool b)
	{
		if (m_PlaneCapture != null)
		{
			m_PlaneCapture.SetActiveRecursively(b);
		}
		if (m_PlaneEighties != null)
		{
			m_PlaneEighties.SetActiveRecursively(b);
		}
	}

	private void SwitchBackMode(EBackMode mode)
	{
		SwitchBackMode(mode, false);
	}

	private void SwitchBackMode(EBackMode mode, bool landscape)
	{
		switch (mode)
		{
		case EBackMode.CameraBack:
			AllInput.ModifyCameraCapture(false);
			AllInput.ResumeCameraCapture();
			m_PlaneCapture.transform.localScale = m_BackCameraScale;
			if (DephaseRabbid())
			{
				m_PlaneCapture.transform.localEulerAngles = m_CameraRotationDephase.eulerAngles;
			}
			else
			{
				m_PlaneCapture.transform.localEulerAngles = m_CameraRotation.eulerAngles;
			}
			break;
		case EBackMode.CameraFront:
			AllInput.ModifyCameraCapture(true);
			AllInput.ResumeCameraCapture();
			m_PlaneCapture.transform.localScale = m_FrontCameraScale;
			if (DephaseRabbid())
			{
				m_PlaneCapture.transform.localEulerAngles = m_CameraRotationDephase.eulerAngles;
			}
			else
			{
				m_PlaneCapture.transform.localEulerAngles = m_CameraRotation.eulerAngles;
			}
			break;
		case EBackMode.Photo:
			AllInput.PauseCameraCapture();
			Utility.LogRed("landscape: " + landscape);
			if (landscape)
			{
				Vector3 photoScale = m_PhotoScale90;
				photoScale.z *= -1f;
				m_PlaneCapture.transform.localScale = photoScale;
				m_PlaneCapture.transform.localEulerAngles = m_PhotoRotation90.eulerAngles;
			}
			else
			{
				Vector3 photoScale2 = m_PhotoScale;
				photoScale2.z *= -1f;
				m_PlaneCapture.transform.localScale = photoScale2;
				m_PlaneCapture.transform.localEulerAngles = m_PhotoRotation.eulerAngles;
			}
			break;
		}
		m_BackMode = mode;
	}

	private void SwitchFrontMode(EFrontMode mode)
	{
		if (m_FrontMode == mode)
		{
			return;
		}
		switch (mode)
		{
		case EFrontMode.Pose:
			if (m_PlaneEighties != null)
			{
				m_PlaneEighties.active = false;
				m_PlaneEighties.GetComponent<Renderer>().material.mainTexture = null;
				Utility.FreeMem();
			}
			SetTS(m_DefaultPosition, m_DefaultScale);
			break;
		case EFrontMode.Eighties:
			m_DefaultScale = base.transform.localScale;
			m_DefaultPosition = base.transform.localPosition;
			SetT(m_HidePosition);
			m_PlaneEighties.active = true;
			break;
		}
		m_FrontMode = mode;
		if (m_PlaneEighties != null)
		{
			Texture mainTexture = m_PlaneEighties.GetComponent<Renderer>().material.mainTexture;
			if (mainTexture != null)
			{
				float num = (float)mainTexture.width / (float)mainTexture.height;
				Vector3 localScale = m_PlaneEighties.transform.localScale;
				localScale.x = localScale.z * num;
				m_PlaneEighties.transform.localScale = localScale;
			}
		}
	}

	private void UpdateRabbid()
	{
		if (AllInput.GetTouchCount() <= 2 && m_LastTouchCount <= 2)
		{
			int touchCount = AllInput.GetTouchCount();
			int num = touchCount;
			Vector3 zero = Vector3.zero;
			bool flag = false;
			for (int i = 0; i < touchCount; i++)
			{
				AllInput.EState state = AllInput.GetState(i);
				if (m_Scroller != null && m_Scroller.IsInteract(i))
				{
					flag = true;
					if (state == AllInput.EState.Leave)
					{
						num--;
					}
					else
					{
						zero += AllInput.GetWorldPosition(i);
					}
					continue;
				}
				switch (state)
				{
				case AllInput.EState.Began:
					zero += AllInput.GetWorldPosition(i);
					flag = true;
					break;
				case AllInput.EState.Leave:
					flag = true;
					num--;
					break;
				default:
					zero += AllInput.GetWorldPosition(i);
					break;
				}
			}
			if (num >= 1)
			{
				zero /= (float)num;
				zero.z = 0f;
				if (num == 1 && !flag && AllInput.GetState(0) != AllInput.EState.Stationary)
				{
					Vector3 localPosition = base.transform.localPosition;
					localPosition += zero - m_TouchWorldPos;
					localPosition = (localPosition - base.transform.localPosition) * 0.0055f;
					localPosition.x *= -1f;
					localPosition += base.transform.localPosition;
					if (localPosition.x < m_TransRect.xMin)
					{
						localPosition.x = m_TransRect.xMin;
					}
					else if (localPosition.x > m_TransRect.xMax)
					{
						localPosition.x = m_TransRect.xMax;
					}
					if (localPosition.y < m_TransRect.yMin)
					{
						localPosition.y = m_TransRect.yMin;
					}
					else if (localPosition.y > m_TransRect.yMax)
					{
						localPosition.y = m_TransRect.yMax;
					}
					SetT(localPosition);
				}
				if (AllInput.GetTouchCount() == 2)
				{
					bool flag2 = AllInput.GetState(0) != AllInput.EState.Leave && AllInput.GetState(1) != AllInput.EState.Leave;
					Vector3 axis = AllInput.GetPosition(0) - AllInput.GetPosition(1);
					axis.Normalize();
					float rotation = GetRotation(axis);
					if (flag2 && m_LastTouchCount == 2)
					{
						float num2 = rotation - m_RefAngle;
						float num3 = (zero - m_TouchWorldPos).magnitude;
						if (m_TouchWorldPos.x > zero.x)
						{
							num3 *= -1f;
						}
						m_Y += num3;
						m_Z += num2;
						Quaternion identity = Quaternion.identity;
						identity *= Quaternion.AngleAxis(m_Z, Vector3.back);
						identity *= Quaternion.AngleAxis(m_Y, Vector3.up);
						base.transform.rotation = identity;
						if (m_Costume != null)
						{
							m_Costume.transform.rotation = base.transform.rotation;
						}
					}
					m_RefAngle = rotation;
					if (m_LastTouchCount == 2)
					{
						float magnitude = (AllInput.GetWorldPosition(0) - AllInput.GetWorldPosition(1)).magnitude;
						float num4 = magnitude / m_RefScale;
						Vector3 s = base.transform.localScale * num4;
						if (s.x > m_MaxScale)
						{
							s = new Vector3(m_MaxScale, m_MaxScale, m_MaxScale);
						}
						else if (s.x < m_MinScale)
						{
							s = new Vector3(m_MinScale, m_MinScale, m_MinScale);
						}
						SetS(s);
						m_RefScale = magnitude;
						if (m_RefScale < 0.01f)
						{
							m_RefScale = 0.01f;
						}
					}
					else
					{
						m_RefScale = (AllInput.GetWorldPosition(0) - AllInput.GetWorldPosition(1)).magnitude;
						if (m_RefScale < 0.01f)
						{
							m_RefScale = 0.01f;
						}
					}
				}
				m_TouchWorldPos = zero;
			}
		}
		m_LastTouchCount = AllInput.GetTouchCount();
	}

	private void QuitPhotoMode()
	{
		DataMining.Increment(MainMenuScript.EMenu.InZePhone.ToString());
	}

	private float GetRotation(Vector3 axis)
	{
		float num = Vector3.Angle(axis, new Vector3(1f, 0f, 0f));
		if (axis.y < 0f)
		{
			num *= -1f;
		}
		return num;
	}

	private void TakePhoto()
	{
		if (!m_TakePhoto)
		{
			Utility.FreeMem();
			AllInput.PauseCameraCapture();
		}
		else if (m_BackMode == EBackMode.Photo)
		{
			SwitchBackMode(EBackMode.CameraBack);
		}
		else
		{
			AllInput.ResumeCameraCapture();
		}
		m_TakePhoto = !m_TakePhoto;
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
		Utility.FreeMem();
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
				FonctionalityItem.EType eType = FonctionalityItem.EType.LastScreenshot;
				eType = FonctionalityItem.EType.LastPicture;
				m_Fonctionality.SetTexture(texture2D, eType);
				m_Fonctionality.Enable(true, eType);
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
		Utility.FreeMem();
		DataMining.OnTakePhoto(m_BackMode);
		Utility.StopInputByActivity(false);
	}

	private void SelectARPose(bool forceRecreate, bool playSound)
	{
		bool flag = false;
		if (playSound)
		{
			PlayExternalAudioClip(ValidSound);
		}
		if (m_AchievementsTable == null)
		{
			return;
		}
		int aRIndex = m_ARIndex;
		EProductID eProductID = ((aRIndex >= 1) ? m_AchievementsTable.GetProductID((EARPose)(aRIndex - 1)) : m_AchievementsTable.GetProductID((EAREighty)aRIndex));
		if (!Utility.IsUnlockedProduct(eProductID))
		{
			Request(eProductID);
			return;
		}
		if (m_PlaneEighties != null && m_PlaneEighties.GetComponent<Renderer>().material.mainTexture != null)
		{
			m_PlaneEighties.GetComponent<Renderer>().material.mainTexture = null;
			Utility.FreeMem();
		}
		if (aRIndex < 1)
		{
			string text = ((EAREighty)aRIndex).ToString();
			string resPath = "ARPics/AREighties/" + text;
			Texture2D mainTexture = Utility.LoadTextureResource<Texture2D>(resPath);
			if (m_PlaneEighties != null)
			{
				m_PlaneEighties.GetComponent<Renderer>().material.mainTexture = mainTexture;
			}
			SwitchFrontMode(EFrontMode.Eighties);
			if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.AREighty, text))
			{
				flag = true;
			}
			else
			{
				Utility.FreeMem(true);
			}
		}
		else
		{
			string anim = ((EARPose)(aRIndex - 1)).ToString();
			SwitchFrontMode(EFrontMode.Pose);
			PlayAnim(anim, 0f);
			if (AchievementTable.MarkGoodyKey(AchievementTable.EGoody.ARPose, anim))
			{
				flag = true;
			}
		}
		if (flag || forceRecreate)
		{
			RecreateARPosesThumbnailsElement(m_ARIndex);
			if (m_Scroller != null)
			{
				m_Scroller.Resize(m_AllThumbnails.ToArray(), m_ARIndex);
			}
		}
	}

	private void StartPlugins()
	{
		StartBilling();
		if (!m_CanMakePayments)
		{
		}
	}

	private void OnEnable()
	{
		RegisterIAB();
		RegisterStoreKit();
	}

	private void OnDisablePlugins()
	{
		UnRegisterStoreKit();
		UnRegisterIAB();
	}

	private void PromptForPhoto()
	{
		Utility.Log(ELog.Plugin, "PromptForPhoto - Start");
		//EtceteraAndroid.promptForPictureFromAlbum(Screen.width, Screen.height, "albumImage.jpg");
		Utility.Log(ELog.Plugin, "PromptForPhoto - End");
	}

	private void StartBilling()
	{
		//IABAndroid.startCheckBillingAvailableRequest();
	}

	private bool CanMakePayments()
	{
		Utility.Log(ELog.Plugin, "CanMakePayments ?");
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

	private void StartScroller()
	{
		GUIScroller.ScrollItemPressed += OnScrollItemPressed;
		m_PhotoPath = Utility.GetPersistentDataPath() + "/" + GlobalVariables.s_PhotoPath + "/";
		m_MiniPhotoPath = Utility.GetPersistentDataPath() + "/" + GlobalVariables.s_MiniPhotoPath + "/";
		GUIUtils.AddScroller(0, 0f, m_AllThumbnails.ToArray(), true, 0);
		((GUIRetractableScroller)GUIUtils.GetScroller(0)).SetAutoRetractable(false);
		m_Scroller = GUIUtils.GetScroller(0);
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
	}

	private void OnScrollItemPressed(int scrollViewID, int itemIdx)
	{
		m_ARIndex = itemIdx;
		SelectARPose(false, true);
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

	private void RecreateARPosesThumbnailsBuffer()
	{
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
		m_AllThumbnails.Clear();
		for (EAREighty eAREighty = EAREighty.BTS_AW3D_RAB08_0002_Resized_PhotoMontage; eAREighty < EAREighty.Count; eAREighty++)
		{
			bool flag = false;
			bool flag2 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			EProductID productID = m_AchievementsTable.GetProductID(eAREighty);
			ePack = AchievementTable.GetPackFromProductID(productID);
			bool flag3 = Utility.IsUnlockedProduct(productID);
			string text = "ARPics/AREighties/Thumbnails";
			if (!flag3)
			{
				flag2 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag = AchievementTable.IsGoodyNew(AchievementTable.EGoody.AREighty, eAREighty.ToString());
			}
			text = text + "/" + eAREighty;
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>(text);
			if (texture2D != null)
			{
				if (flag)
				{
					texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
				}
				if (flag2)
				{
					texture2D = Utility.AddColor(texture2D, clr);
				}
				m_AllThumbnails.Add(texture2D);
			}
			else
			{
				Utility.Log(ELog.Errors, "Failed to load: " + text);
			}
		}
		ePack = EPack.Count;
		for (EARPose eARPose = EARPose.pose01; eARPose < EARPose.Count; eARPose++)
		{
			bool flag = false;
			bool flag2 = false;
			Color clr = Color.white;
			EPack ePack2 = ePack;
			EProductID productID = m_AchievementsTable.GetProductID(eARPose);
			ePack = AchievementTable.GetPackFromProductID(productID);
			bool flag3 = Utility.IsUnlockedProduct(productID);
			if (array[(int)ePack].StartIndex == -1)
			{
				array[(int)ePack].StartIndex = m_AllThumbnails.Count;
			}
			if (ePack2 != ePack)
			{
				EPack ePack3 = ePack2;
				if (ePack3 == EPack.Count)
				{
					ePack3 = EPack.Interactions;
				}
				array[(int)ePack3].EndIndex = m_AllThumbnails.Count - 1;
				if (!Utility.IsUnlockedProduct(AchievementTable.GetProductIDFromPack(ePack3)))
				{
					int eltCount = array[(int)ePack3].EndIndex - array[(int)ePack3].StartIndex;
					array[(int)ePack3].Texture = AchievementTable.LoadPackTexture(ePack3, eltCount);
				}
			}
			string text = "ARPics/ARPoses/Thumbnails";
			if (!flag3)
			{
				flag2 = false;
				text += "Locked";
				clr = AchievementTable.GetPackColor(ePack);
			}
			else
			{
				flag = AchievementTable.IsGoodyNew(AchievementTable.EGoody.ARPose, eARPose.ToString());
			}
			text = text + "/" + eARPose;
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>(text);
			if (texture2D != null)
			{
				if (flag)
				{
					texture2D = Utility.BlendTextures(m_NewTexture_110x128, texture2D);
				}
				if (flag2)
				{
					texture2D = Utility.AddColor(texture2D, clr);
				}
				m_AllThumbnails.Add(texture2D);
			}
			else
			{
				Utility.Log(ELog.Errors, "Failed to load: " + text);
			}
		}
		if (ePack != EPack.Count)
		{
			array[(int)ePack].EndIndex = m_AllThumbnails.Count - 1;
			if (!Utility.IsUnlockedProduct(AchievementTable.GetProductIDFromPack(ePack)))
			{
				int eltCount2 = array[(int)ePack].EndIndex - array[(int)ePack].StartIndex;
				array[(int)ePack].Texture = AchievementTable.LoadPackTexture(ePack, eltCount2);
			}
		}
		GUIUtils.GetScroller(0).Resize(m_AllThumbnails.ToArray(), 0);
		GUIUtils.GetScroller(0).SetPacks(array);
	}

	private void RecreateARPosesThumbnailsElement(int itemIdx)
	{
		if (itemIdx < 1)
		{
			Texture2D texture2D = Utility.LoadTextureResource<Texture2D>("ARPics/AREighties/Thumbnails/" + (EAREighty)itemIdx);
			if (texture2D != null)
			{
				m_AllThumbnails[itemIdx] = texture2D;
			}
			return;
		}
		itemIdx--;
		Texture2D texture2D2 = Utility.LoadTextureResource<Texture2D>("ARPics/ARPoses/Thumbnails/" + (EARPose)itemIdx);
		if (texture2D2 != null)
		{
			m_AllThumbnails[itemIdx + 1] = texture2D2;
		}
	}

	private void ActivateScroller()
	{
		m_Scroller = GUIUtils.GetScroller(0);
		if (m_Scroller != null && m_AllThumbnails != null)
		{
			m_Scroller.SetVisible(m_AllThumbnails.Count != 0);
		}
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
					PlayExternalAudioClip(BackSound);
					ShowStore(false);
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

	private void PlayExternalAudioClip(AudioClip clip)
	{
		if (/*!AudioPlayerManager.s_IsDeviceMuted && */GlobalVariables.SoundEnabled != 0)
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
				symbol = string.Empty;
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

	private void UnlockPack(EProductID productID)
	{
		Utility.UnlockProduct(productID);
		if (m_AchievementsTable != null)
		{
			m_AchievementsTable.UnlockPack();
		}
		GlobalVariables.RecomputeMoveCount();
		RecreateARPosesThumbnailsBuffer();
		if (m_Scroller != null)
		{
			m_Scroller.Resize(m_AllThumbnails.ToArray(), m_ARIndex);
		}
		ActivateScroller();
	}
}
