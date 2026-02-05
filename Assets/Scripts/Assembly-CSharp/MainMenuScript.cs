using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using TMPro;
using UnityEngine;

public class MainMenuScript : MonoBehaviour, SmallXmlParser.IContentHandler
{
	public enum EState
	{
		intro = 0,
		loop = 1
	}

	public enum EMenu
	{
		InZePhone = 0,
		OutWorld = 1,
		Photo = 2,
		Option = 3,
		Music = 4,
		Quit = 5,
		Customization = 6,
		Store = 7,
		Count = 8
	}

	public enum EMessage
	{
		NetworkConnectionRequired = 0,
		NetNotReachable = 1,
		None = 2
	}

	public enum EAudioSource
	{
		BG = 0,
		FX = 1,
		Count = 2
	}

	public enum EView
	{
		Achievement = 0,
		Leaderbord = 1,
		Count = 2
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

	public GameObject m_Menu;

	public TextMeshPro m_TextInZePhone;

	public TextMeshPro m_TextOutWorld;

	public TextMeshPro m_TextPhotoRoom;

	public TextMeshPro m_TextMusic;

	public TextMeshPro m_TextOptions;

	public TextMeshPro m_TextQuit;

	public GameObject m_NewVideoBackground;

	public GameObject m_Logo;

	private Transform m_PhotoRoomNode;

	private AudioSource[] m_Audio;

	public AudioClip m_MenuStartUpSound;

	public AudioClip m_MenuSongSound;

	public AudioClip m_MenuHudValidSound;

	public GUISkin m_CommonSkin;

	public GUISkin m_MainMenuSkin;

	private bool m_IsLoadingGame;

	private EState m_State;

	private int m_ReviewLaunchCount = 2;

	private int m_HoursBetweenReviewPrompts = 48;

	private EMessage m_Message = EMessage.None;

	private EMenu m_MenuBegan = EMenu.Count;

	private EView m_View = EView.Count;

	private EView m_NextView = EView.Count;

	private string m_NextSceneName = string.Empty;

	private EMenu m_NextMenu = EMenu.Count;

	private int m_FrameCountBeforeNextScene = -1;

	private bool m_ClearActivityView = true;

	private bool m_ShowUrgentNews = true;

	private Texture m_SingThumbnail;

	private Texture m_DanceThumbnail;

	private GUIStyle m_TextButton;

	private bool m_CrossPromoInfoBadge;

	private bool m_CrossPromoActivity;

	private int m_CrossPromoFrameCount = 10;

	private static float s_VideoLoadingTimeLimit = 25f;

	private int m_NewVideoIndex = -1;

	private List<SVideo> m_Videos = new List<SVideo>();

	private SVideo m_CurrentVideo;

	private ECheck m_Check;

	private int m_CurrentThumbnail;

	private EThumbnail m_UsingThumbnail = EThumbnail.Thumbnail2;

	private Texture2D m_NewTexture_120x90;

	private Texture m_NewVideoThumbnail;

	private void Awake()
	{
		AllInput.ActivateAutoRotateFrame(true);
		AllInput.ActivateAutoRotateScreen(true, true);
	}

	private void Start()
	{
		GlobalVariables.SoundEnabled = PlayerPrefs.GetInt("_sound_enable");
		DataMining.OnLevelStart();
		//AudioPlayerBinding.checkMuteSwitch();
		Utility.CreateMenuBackground();
		if (m_TextInZePhone != null)
		{
			m_TextInZePhone.text = Localization.GetLocalizedText(ELoc.StartGame);
		}
		if (m_TextOutWorld != null)
		{
			m_TextOutWorld.text = Localization.GetLocalizedText(ELoc.OutWorld);
		}
		if (m_TextMusic != null)
		{
			m_TextMusic.text = Localization.GetLocalizedText(ELoc.Music);
		}
		if (m_TextOptions != null)
		{
			m_TextOptions.text = Localization.GetLocalizedText(ELoc.Options);
		}
		if (m_TextQuit != null)
		{
			m_TextQuit.text = Localization.GetLocalizedText(ELoc.Quit);
		}
		if (m_TextPhotoRoom != null)
		{
			m_TextPhotoRoom.text = Localization.GetLocalizedText(ELoc.PhotoRoom);
			m_PhotoRoomNode = m_TextPhotoRoom.transform.parent;
			if (m_PhotoRoomNode == null)
			{
				Utility.Log(ELog.Errors, "MainMenuScript::m_PhotoRoomNode has no parent");
			}
		}
		if (m_NewVideoBackground != null)
		{
			m_NewVideoBackground.active = false;
		}
		m_Audio = base.gameObject.GetComponents<AudioSource>();
		if (m_Audio.Length < 2)
		{
			Utility.Log(ELog.Errors, "not enough audio source component : current " + m_Audio.Length + " need " + 2);
		}
		PlayAudioClip(m_MenuStartUpSound, base.transform.position, 1f, EAudioSource.BG);
		m_Menu.GetComponent<Animation>()["loop"].wrapMode = WrapMode.Loop;
		m_Menu.GetComponent<Animation>().Play("intro");
		SystemLanguage language = Localization.GetLanguage();
		string text = Utility.GetTexPath() + "GUI/Logo";
		text = ((language != SystemLanguage.French) ? (text + "All") : (text + "Fr"));
		m_Logo.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(text);
		m_SingThumbnail = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/Sing");
		m_DanceThumbnail = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/Dance");
		Utility.ClearActivityView();
		StartVideoChecker();
		if (!Utility.IsHD())
		{
			m_TextButton = m_CommonSkin.GetStyle("TextButtonSmall");
		}
		else if (Utility.IsFourthGenAppleDevice())
		{
			m_TextButton = m_CommonSkin.GetStyle("HugeTextButtonLarge");
		}
		else
		{
			m_TextButton = m_CommonSkin.GetStyle("TextButtonLarge");
		}
		if (GlobalVariables.s_CrossPromo)
		{
			//CrossPromoBinding.ReloadWebNews();
			int num = PlayerPrefs.GetInt("UrgentNewsCpt", 0);
			if (++num == 4)
			{
				num = 0;
				//CrossPromoBinding.ReloadUrgentNews();
			}
			PlayerPrefs.SetInt("UrgentNewsCpt", num);
		}
	}

	private void OnDisable()
	{
		DisableVideoChecker();
		if (m_ClearActivityView)
		{
			Utility.ClearActivityView();
		}
	}

	private void FixedUpdate()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			m_FrameCountBeforeNextScene--;
			if (m_FrameCountBeforeNextScene == 0)
			{
				Localization.GenerateWaitText();
				LoadLevel(m_NextSceneName, m_NextMenu);
			}
		}
	}

	private void Update()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			return;
		}
		switch (m_State)
		{
		case EState.intro:
			if (!m_Menu.GetComponent<Animation>().isPlaying)
			{
				m_State = EState.loop;
				m_Menu.GetComponent<Animation>().CrossFade("loop");
				OnEndMenuAnimation();
			}
			break;
		}
		if (m_ShowUrgentNews && AllInput.IsInternetReachable() && m_CrossPromoFrameCount >= 0)
		{
			m_CrossPromoFrameCount--;
			if (m_CrossPromoFrameCount < 0)
			{
				if (GlobalVariables.s_CrossPromo)
				{
					//CrossPromoBinding.ShowUrgentNews();
				}
				m_ShowUrgentNews = false;
			}
			return;
		}
		if (m_NextView != EView.Count)
		{
			m_NextView = EView.Count;
		}
		if (m_View == EView.Count && !m_Audio[0].isPlaying && GlobalVariables.SoundEnabled == 1)
		{
			m_Audio[0].clip = m_MenuSongSound;
			m_Audio[0].Play();
			m_Audio[0].loop = true;
		}
		if (m_Message == EMessage.None && !Utility.InputStoppedByActivity())
		{
			SelectMenuItem();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (m_Message != EMessage.None)
			{
				m_Message = EMessage.None;
			}
			else
			{
				Application.Quit();
			}
		}
	}

	private void LateUpdate()
	{
		if (m_PhotoRoomNode != null)
		{
			m_PhotoRoomNode.localEulerAngles = Vector3.zero;
		}
	}

	private void SelectMenuItem()
	{
		if (AllInput.GetTouchCount() != 1)
		{
			return;
		}
		if (AllInput.GetState(0) == AllInput.EState.Began)
		{
			Ray ray = base.GetComponent<Camera>().ScreenPointToRay(AllInput.GetRaycastPosition(0));
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (m_TextInZePhone != null && hitInfo.transform == m_TextInZePhone.transform)
				{
					m_MenuBegan = EMenu.InZePhone;
				}
				else if (m_TextMusic != null && hitInfo.transform == m_TextMusic.transform)
				{
					m_MenuBegan = EMenu.Music;
				}
				else if (m_TextOptions != null && hitInfo.transform == m_TextOptions.transform)
				{
					m_MenuBegan = EMenu.Option;
				}
				else if (m_TextPhotoRoom != null && hitInfo.transform == m_TextPhotoRoom.transform)
				{
					m_MenuBegan = EMenu.Photo;
				}
				else if (m_TextOutWorld != null && hitInfo.transform == m_TextOutWorld.transform)
				{
					m_MenuBegan = EMenu.OutWorld;
				}
				else if (m_TextQuit != null && hitInfo.transform == m_TextQuit.transform)
				{
					m_MenuBegan = EMenu.Quit;
				}
				else
				{
					m_MenuBegan = EMenu.Count;
				}
			}
			else
			{
				m_MenuBegan = EMenu.Count;
			}
		}
		else
		{
			if (AllInput.GetState(0) != AllInput.EState.Leave)
			{
				return;
			}
			Ray ray2 = base.GetComponent<Camera>().ScreenPointToRay(AllInput.GetRaycastPosition(0));
			RaycastHit hitInfo2;
			if (!Physics.Raycast(ray2, out hitInfo2))
			{
				return;
			}
			if (m_TextInZePhone != null && hitInfo2.transform == m_TextInZePhone.transform)
			{
				GlobalVariables.s_PhotoFromMainMenu = false;
				if (m_MenuBegan == EMenu.InZePhone)
				{
					SelectMenuItem("InGame", EMenu.InZePhone);
				}
			}
			else if (m_TextOutWorld != null && hitInfo2.transform == m_TextOutWorld.transform)
			{
				if (m_MenuBegan == EMenu.OutWorld)
				{
					SelectMenuItem("OutWorld2D", EMenu.OutWorld);
				}
			}
			else if (m_TextPhotoRoom != null && hitInfo2.transform == m_TextPhotoRoom.transform)
			{
				GlobalVariables.s_PhotoFromMainMenu = true;
				if (m_MenuBegan == EMenu.Photo)
				{
					SelectMenuItem("PhotoRoom", EMenu.Photo);
				}
			}
			else if (m_TextMusic != null && hitInfo2.transform == m_TextMusic.transform)
			{
				if (m_MenuBegan == EMenu.Music)
				{
					GamePlayerPrefs.SetString("Goto", "Dance");
					SelectMenuItem("InGame", EMenu.InZePhone);
				}
			}
			else if (m_TextOptions != null && hitInfo2.transform == m_TextOptions.transform)
			{
				if (m_MenuBegan == EMenu.Option)
				{
					SelectMenuItem("Options", EMenu.Option);
				}
			}
			else if (m_TextQuit != null && hitInfo2.transform == m_TextQuit.transform)
			{
				Utility.Log(ELog.Info, "Quit");
				Application.Quit();
			}
		}
	}

	private void OnGUI()
	{
		if (m_FrameCountBeforeNextScene > 0)
		{
			return;
		}
		if ((bool)m_CommonSkin)
		{
			GUI.skin = m_CommonSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "Main Menu: Skin not found");
		}
		if (m_IsLoadingGame)
		{
			GUIUtils.DrawLoadingText();
			return;
		}
		bool considerInputs = m_Message == EMessage.None;
		GUI.skin = m_MainMenuSkin;
		DrawNewVideo(considerInputs);
		DrawRedLynx(considerInputs);
		if (m_Message != EMessage.None)
		{
			ShowMessage();
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
			return;
		}
		EView view = m_View;
		if (view == EView.Achievement || view == EView.Leaderbord)
		{
			PlayAudioSource(EAudioSource.BG);
		}
		m_View = EView.Count;
		AllInput.ActivateAutoRotateFrame(false);
		AllInput.ActivateAutoRotateScreen(false, true);
		if (GlobalVariables.s_CrossPromo)
		{
			//CrossPromoBinding.ReloadWebNews();
		}
	}

	private void PlayAudioClip(AudioClip clip, Vector3 position, float volume, EAudioSource source)
	{
		if (GlobalVariables.SoundEnabled == 1)
		{
			m_Audio[(int)source].Stop();
			m_Audio[(int)source].clip = clip;
			m_Audio[(int)source].time = 0f;
			m_Audio[(int)source].Play();
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

	private void SelectMenuItem(string sceneName, EMenu menu)
	{
		m_FrameCountBeforeNextScene = 10;
		m_NextSceneName = sceneName;
		m_NextMenu = menu;
		Utility.ShowActivityView(true);
		if (menu == EMenu.InZePhone)
		{
			m_ClearActivityView = false;
		}
		if (m_NewVideoBackground != null)
		{
			m_NewVideoBackground.active = false;
		}
		if (GlobalVariables.s_CrossPromo && m_CrossPromoInfoBadge && m_CrossPromoActivity)
		{
			//CrossPromoBinding.ToggleInfoBadge();
			m_CrossPromoActivity = false;
		}
	}

	private void LoadLevel(string sceneName, EMenu menu)
	{
		//AudioPlayerBinding.checkMuteSwitch();
		if (!m_IsLoadingGame)
		{
			Utility.Log(ELog.Info, "Menu choice: " + sceneName);
			DataMining.Increment(menu.ToString());
			if (m_Menu != null)
			{
				Utility.ShowRecursivly(m_Menu.transform, false);
			}
			m_IsLoadingGame = true;
			if (m_MenuHudValidSound != null && base.transform != null)
			{
				PlayAudioClip(m_MenuHudValidSound, base.transform.position, 1f, EAudioSource.FX);
			}
			m_Audio[0].loop = false;
			Utility.FreeMem(true);
			Application.LoadLevel(sceneName);
			Utility.FreeMem(true);
		}
	}

	private void OnEndMenuAnimation()
	{
		RegisterForRemoteNotifications();
	}

	private void ShowMessage()
	{
		GUI.skin = m_CommonSkin;
		if (m_Message == EMessage.None)
		{
			return;
		}
		switch (m_Message)
		{
		case EMessage.NetworkConnectionRequired:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.NetworkConnectionRequired)))
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

	private void DrawNewVideo(bool considerInputs)
	{
		float top = 10f;
		if (m_NewVideoIndex == -1 || !(m_NewVideoThumbnail != null))
		{
			return;
		}
		Rect position = Utility.NewRect(Utility.RefWidth / 2f + (float)GlobalVariables.BACK_BUTTON_WIDTH + 30f, top, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT);
		if (GUI.Button(position, m_NewVideoThumbnail) && considerInputs)
		{
			if (AllInput.IsInternetReachable())
			{
				GamePlayerPrefs.SetInt("GotoVideo", m_NewVideoIndex);
				SelectMenuItem("OutWorld2D", EMenu.OutWorld);
			}
			else
			{
				SetMessage(EMessage.NetNotReachable);
			}
		}
	}

	private void DrawDanceAndSing(bool considerInputs)
	{
		float top = Utility.RefHeight / 2f - 60f;
		Rect position = Utility.NewRect(Utility.RefWidth / 2f + (float)GlobalVariables.BACK_BUTTON_WIDTH + 30f, top, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT);
		if (GUI.Button(position, m_SingThumbnail) && considerInputs)
		{
			GamePlayerPrefs.SetString("Goto", "Sing");
			SelectMenuItem("InGame", EMenu.InZePhone);
		}
		position = Utility.NewRect(Utility.RefWidth / 2f - 2f * (float)GlobalVariables.BACK_BUTTON_WIDTH - 30f, top, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT);
		if (GUI.Button(position, m_DanceThumbnail) && considerInputs)
		{
			GamePlayerPrefs.SetString("Goto", "Dance");
			SelectMenuItem("InGame", EMenu.InZePhone);
		}
	}

	private Rect CreateRedLynxRect(int i, int eltCount, float width, float height, float hMargin, float vMargin)
	{
		float top = 100f - height - vMargin;
		float left = ((i != 0) ? (100f - width - hMargin) : hMargin);
		return Utility.NewRectP(left, top, width, height);
	}

	private void DrawRedLynx(bool considerInputs)
	{
		if (!GlobalVariables.s_CrossPromo)
		{
			return;
		}
		GUI.skin = m_CommonSkin;
		if (GUI.Button(CreateRedLynxRect(0, 2, 20f, 12f, 3f, 1f), "Web\nNews", m_TextButton) && considerInputs)
		{
			if (AllInput.IsInternetReachable())
			{
				//CrossPromoBinding.ShowWebNews();
			}
			else
			{
				SetMessage(EMessage.NetNotReachable);
			}
		}
		if (m_CrossPromoInfoBadge && GUI.Button(CreateRedLynxRect(1, 2, 20f, 12f, 3f, 1f), "Info\nBadge", m_TextButton) && considerInputs)
		{
			if (AllInput.IsInternetReachable())
			{
				//CrossPromoBinding.ToggleInfoBadge();
				m_CrossPromoActivity = !m_CrossPromoActivity;
			}
			else
			{
				SetMessage(EMessage.NetNotReachable);
			}
		}
	}

	private void RegisterForRemoteNotifications()
	{
		//EtceteraBinding.setUrbanAirshipCredentials(GlobalVariables.UA_APP_KEY, GlobalVariables.UA_APP_SECRET);
		//EtceteraBinding.registerForRemoteNotifcations((RemoteNotificationType)7);
	}

	private void AskForReview()
	{
		string appPackageName = "com.exkee.rabbids";
		//EtceteraAndroid.askForReview(m_ReviewLaunchCount, 1, m_HoursBetweenReviewPrompts, Localization.GetLocalizedText(ELoc.ReviewTitle), Localization.GetLocalizedText(ELoc.ReviewMessage), appPackageName, false);
	}

	private void StartVideoChecker()
	{
		string text = Localization.GetLanguage().ToString();
		m_NewTexture_120x90 = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "GUI/" + text + "/new_120x90");
		if (AllInput.IsInternetReachable())
		{
			LoadVideos();
		}
	}

	private void DisableVideoChecker()
	{
		Utility.StopWWW(this);
	}

	private void LoadVideos()
	{
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

	private void OnEndCallRSS(WWW www, object[] list)
	{
		SmallXmlParser smallXmlParser = new SmallXmlParser();
		smallXmlParser.Parse(new StringReader(www.text), this);
		for (int i = 0; i < m_Videos.Count; i++)
		{
			string thumbnailUrl = m_Videos[i].ThumbnailUrl;
			if (AchievementTable.IsGoodyNew(AchievementTable.EGoody.YoutubeVideo, thumbnailUrl))
			{
				m_NewVideoIndex = i;
				if (AllInput.IsInternetReachable())
				{
					Utility.CallWWW(this, m_Videos[i].ThumbnailUrl, OnEndCallThumbnail, OnCallThumbnailError, m_Videos[i].ThumbnailUrl);
				}
				break;
			}
		}
	}

	private void OnEndLoadVideos(params object[] list)
	{
		Utility.Log(ELog.Errors, "MainMenu :: Load Video failed...");
	}

	private void OnEndCallThumbnail(WWW www, object[] list)
	{
		m_NewVideoThumbnail = Utility.BlendTextures(m_NewTexture_120x90, www.texture);
		if (m_NewVideoThumbnail == null)
		{
			Utility.Log(ELog.Errors, "Unable to blend New Texture with: " + list[0]);
		}
		if (m_NewVideoBackground != null)
		{
			m_NewVideoBackground.active = true;
		}
	}

	private void OnCallThumbnailError(object[] list)
	{
		Utility.Log(ELog.Errors, "Thumbnail donwload failed... " + list[0]);
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
			if (string.IsNullOrEmpty(m_CurrentVideo.Url))
			{
				Utility.Log(ELog.Errors, "Youtube : Video url not fount");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.ThumbnailUrlBig))
			{
				Utility.Log(ELog.Errors, "Youtube : ThumbnailBig url not found");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.Tag))
			{
				Utility.Log(ELog.Errors, "Youtube : Video tag not found");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.Title))
			{
				Utility.Log(ELog.Errors, "Youtube : Video title not found");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.Desc))
			{
				Utility.Log(ELog.Errors, "Youtube : Video description not found");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.Date))
			{
				Utility.Log(ELog.Errors, "Youtube : Video date not found");
			}
			else if (m_CurrentVideo.Duration == -1)
			{
				Utility.Log(ELog.Errors, "Youtube : Video duration not found");
			}
			else if (string.IsNullOrEmpty(m_CurrentVideo.ThumbnailUrl))
			{
				Utility.Log(ELog.Errors, "Youtube : Thumbnail url not found");
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
