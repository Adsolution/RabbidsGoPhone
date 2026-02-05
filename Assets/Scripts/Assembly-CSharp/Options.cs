using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
	public enum EMessage
	{
		NetNotReachable = 0,
		PurchaseSuccess = 1,
		PurchaseDisable = 2,
		PurchaseFailed = 3,
		PurchaseFailedClientInvalid = 4,
		PurchaseFailedPaymentCancelled = 5,
		PurchaseFailedPaymentInvalid = 6,
		PurchaseFailedPaymentNotAllowed = 7,
		PurchaseEmpty = 8,
		PurchaseInstalled = 9,
		RestoreTransactionSucceded = 10,
		RestoreTransactionFailed = 11,
		ProductListRequestFailed = 12,
		EmptyStore = 13,
		NetworkConnectionRequired = 14,
		None = 15
	}

	public enum EState
	{
		Main = 0,
		Store = 1,
		StoreDescription = 2,
		Credits = 3,
		Count = 4
	}

	public enum EScrollState
	{
		Idle = 0,
		StartTouch = 1,
		TouchSlide = 2
	}

	public enum EAudioSource
	{
		BG = 0,
		FX = 1,
		Count = 2
	}

	public static string s_SoundEnableKey = "_sound_enable";

	public GUISkin m_CommonSkin;

	public GUISkin m_OptionsSkin;

	public AudioClip CreditsSound;

	public AudioClip gBackSound;

	private AudioSource[] m_Audio;

	private EMessage m_Message = EMessage.None;

	private int m_MusicButton;

	private EState m_State;

	private float m_SeparatorBeforeLabel = 20f;

	private float m_Separator = 20f;

	private float m_DefaultHeight = 50f;

	private float m_LabelHeight = 45f;

	private EScrollState m_ScrollState;

	private AchievementTable m_AchievementsTable;

	private GUIStyle m_TextContent;

	private string m_TextButtonLarge;

	private string m_BlackBackgroundStyle;

	private int m_BackPressed = -1;

	private Vector2 m_CreditScrollPos = Vector2.zero;

	private float m_LastTouchTime;

	private Rect m_CreditsRect;

	private string m_ExkeeCredits = string.Empty;

	private string m_UbiCredits = string.Empty;

	private float m_FixedWidth;

	private string m_DevInfos = string.Empty;

	private bool m_ResetConfirm;

	private bool m_EnableMusicConfirm;

	private bool m_DisableMusicConfirm;

	//private List<StoreKitProduct> m_ProductList;

	private int m_OldUnlockProducts;

	private bool m_CanMakePayments;

	private void Start()
	{
		DataMining.OnLevelStart();
		Utility.CreateMenuBackground();
		m_Audio = base.gameObject.GetComponents<AudioSource>();
		m_Audio[0].clip = CreditsSound;
		m_MusicButton = PlayerPrefs.GetInt(s_SoundEnableKey);
		if (m_MusicButton == 1)
		{
			m_Audio[0].Play();
			m_Audio[0].loop = true;
		}
		m_AchievementsTable = new AchievementTable();
		if (Utility.IsHD())
		{
			m_TextContent = m_CommonSkin.GetStyle("BigTextContent");
		}
		else
		{
			m_TextContent = m_CommonSkin.GetStyle("TextContent");
		}
		m_TextButtonLarge = "TextButtonLarge";
		m_BlackBackgroundStyle = "BlackBackground";
		if (Utility.IsFourthGenAppleDevice())
		{
			m_TextButtonLarge = "HugeTextButtonLarge";
			m_BlackBackgroundStyle = "HugeBlackBackground";
			m_TextContent = m_CommonSkin.GetStyle("HugeTextContent");
		}
		StartMain();
		StartStoreKit();
		StartCredits();
	}

	private void OnDisable()
	{
		StoreKitOnDisable();
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

	private void Update()
	{
		switch (m_State)
		{
		case EState.Main:
			OnUpdateMain();
			break;
		case EState.Credits:
			OnUpdateCredits();
			break;
		case EState.Store:
		case EState.StoreDescription:
			break;
		}
	}

	private void FixedUpdate()
	{
		if (m_BackPressed >= 0)
		{
			m_BackPressed--;
		}
	}

	private void PlaySound(AudioClip clip)
	{
		if (GlobalVariables.SoundEnabled == 1)
		{
			m_Audio[1].Stop();
			m_Audio[1].clip = clip;
			m_Audio[1].Play();
			m_Audio[1].loop = false;
		}
	}

	private void OnGUI()
	{
		if ((bool)m_CommonSkin)
		{
			GUI.skin = m_CommonSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "Sound Selection Screen: Skin not found");
		}
		if (m_Message == EMessage.None)
		{
			switch (m_State)
			{
			case EState.Main:
				MainGUI();
				break;
			case EState.Credits:
				CreditsGUI();
				break;
			case EState.Store:
			case EState.StoreDescription:
				break;
			}
		}
		else
		{
			ShowMessage();
		}
	}

	private void ShowMessage()
	{
		switch (m_Message)
		{
		case EMessage.NetNotReachable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.YouNeedInternet)))
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
		case EMessage.PurchaseDisable:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseDisable)))
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
		case EMessage.PurchaseEmpty:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseEmpty)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.PurchaseInstalled:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.PurchaseInstalled)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.ProductListRequestFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.ProductListRequestFailed)))
			{
				SetMessage(EMessage.None);
				SetState(EState.Main);
			}
			break;
		case EMessage.RestoreTransactionSucceded:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.RestoreTransactionSucceded)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.RestoreTransactionFailed:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.RestoreTransactionFailed)))
			{
				SetMessage(EMessage.None);
			}
			break;
		case EMessage.EmptyStore:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.EmptyStore)))
			{
				SetMessage(EMessage.None);
				SetState(EState.Main);
			}
			break;
		case EMessage.NetworkConnectionRequired:
			if (GUIUtils.DrawError(Localization.GetLocalizedText(ELoc.NetworkConnectionRequired)))
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

	private void SetState(EState state)
	{
		switch (m_State)
		{
		case EState.Main:
			OnLeaveMain();
			break;
		case EState.Credits:
			OnLeaveCredits();
			break;
		}
		m_State = state;
		switch (m_State)
		{
		case EState.Main:
			OnEnterMain();
			break;
		case EState.Credits:
			OnEnterCredits();
			break;
		case EState.Store:
		case EState.StoreDescription:
			break;
		}
	}

	private void StartCredits()
	{
		m_LastTouchTime = Time.time;
		m_CreditsRect = Utility.NewRect(50f, 140f, Utility.RefWidth - 70f, Utility.RefHeight - 225f);
		TextAsset textAsset = Utility.LoadResource<TextAsset>("Localization/CreditsExkee");
		m_ExkeeCredits = textAsset.text;
		textAsset = Utility.LoadResource<TextAsset>("Localization/CreditsUbi");
		m_UbiCredits = textAsset.text;
		GUIStyle style = m_OptionsSkin.GetStyle("verticalscrollbarthumb");
		if (Utility.IsHD())
		{
			style.fixedHeight = 80f;
			RectOffset overflow = style.overflow;
			int num = 0;
			style.overflow.right = num;
			overflow.left = num;
		}
		else
		{
			style.fixedHeight = 40f;
			RectOffset overflow2 = style.overflow;
			int num = -10;
			style.overflow.right = num;
			overflow2.left = num;
		}
		m_DevInfos = "1.0\n";
		m_DevInfos += "support@ubisoft.com\n";
		if (Localization.GetLanguage() == SystemLanguage.French)
		{
			m_DevInfos += "The Lapins Crétins, la Très Grosse Appli";
		}
		else
		{
			m_DevInfos += "Rabbids Go Phone Again";
		}
	}

	private void CreditsGUI()
	{
		float top = 5f + m_LabelHeight + m_SeparatorBeforeLabel;
		GUI.Label(Utility.NewRect(0f, top, Utility.RefWidth, m_LabelHeight), Localization.GetLocalizedText(ELoc.CreditsTitle), m_BlackBackgroundStyle);
		GUI.Label(Utility.NewRect((Utility.RefWidth - 297f) * 0.5f, 120f, 297f, 308f), string.Empty, m_BlackBackgroundStyle);
		float fixedWidth = m_TextContent.fixedWidth;
		TextAnchor alignment = m_TextContent.alignment;
		m_TextContent.fixedWidth = 0f;
		m_TextContent.alignment = TextAnchor.LowerCenter;
		GUI.Label(Utility.NewRect(0f, 430f, Utility.RefWidth, 50f), m_DevInfos, m_TextContent);
		m_TextContent.fixedWidth = fixedWidth;
		m_TextContent.alignment = alignment;
		GUI.skin = m_OptionsSkin;
		GUILayout.BeginArea(m_CreditsRect);
		m_CreditScrollPos = GUILayout.BeginScrollView(m_CreditScrollPos);
		GUILayout.BeginVertical();
		GUILayout.Label(m_ExkeeCredits, m_TextContent);
		GUILayout.Space(50f);
		GUILayout.Label(m_UbiCredits, m_TextContent);
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		GUI.skin = m_CommonSkin;
		if (GUIUtils.DrawBackButton() && m_BackPressed < 0)
		{
			m_BackPressed = 5;
			PlaySound(gBackSound);
			SetState(EState.Main);
		}
	}

	private void OnEnterCredits()
	{
		m_ScrollState = EScrollState.Idle;
		m_FixedWidth = m_TextContent.fixedWidth;
		m_TextContent.fixedWidth = m_CreditsRect.width - 45f;
	}

	private void OnUpdateCredits()
	{
		if (AllInput.GetTouchCount() == 1)
		{
			AllInput.EState state = AllInput.GetState(0);
			switch (m_ScrollState)
			{
			case EScrollState.Idle:
				if (state == AllInput.EState.Began)
				{
					Vector3 gUIPosition = AllInput.GetGUIPosition(0);
					if (m_CreditsRect.Contains(gUIPosition))
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
				m_CreditScrollPos.y -= AllInput.GetGUIDelta(0).y;
				if (state == AllInput.EState.Leave)
				{
					m_ScrollState = EScrollState.Idle;
				}
				break;
			}
			m_LastTouchTime = Time.time;
		}
		if (Time.time - m_LastTouchTime > 2f)
		{
			m_CreditScrollPos.y += 0.5f;
		}
	}

	private void OnLeaveCredits()
	{
		m_TextContent.fixedWidth = m_FixedWidth;
	}

	private void StartMain()
	{
	}

	private void MainGUI()
	{
		if (m_ResetConfirm)
		{
			switch (GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.ConfirmResetSave)))
			{
			case GUIUtils.EAnswer.Yes:
				m_ResetConfirm = false;
				GlobalVariables.ClearPlayerPrefs(true);
				DataMining.Increment(DataMining.EGeneral.ResetSave.ToString());
				break;
			case GUIUtils.EAnswer.No:
				m_ResetConfirm = false;
				break;
			}
			return;
		}
		if (m_EnableMusicConfirm)
		{
			switch (GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.ConfirmEnableMusic)))
			{
			case GUIUtils.EAnswer.Yes:
				m_EnableMusicConfirm = false;
				m_MusicButton = 1;
				m_Audio[0].Play();
				GlobalVariables.SoundEnabled = m_MusicButton;
				GamePlayerPrefs.SetInt(s_SoundEnableKey, m_MusicButton);
				break;
			case GUIUtils.EAnswer.No:
				m_EnableMusicConfirm = false;
				break;
			}
			return;
		}
		if (m_DisableMusicConfirm)
		{
			switch (GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.ConfirmDisableMusic)))
			{
			case GUIUtils.EAnswer.Yes:
				m_DisableMusicConfirm = false;
				m_MusicButton = 0;
				m_Audio[0].Pause();
				GlobalVariables.SoundEnabled = m_MusicButton;
				GamePlayerPrefs.SetInt(s_SoundEnableKey, m_MusicButton);
				break;
			case GUIUtils.EAnswer.No:
				m_DisableMusicConfirm = false;
				break;
			}
			return;
		}
		float num = GlobalVariables.BACK_BUTTON_WIDTH;
		float num2 = GlobalVariables.BACK_BUTTON_HEIGHT;
		float num3 = Utility.RefWidth * 0.5f;
		float num4 = 5f + m_LabelHeight + m_SeparatorBeforeLabel;
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
				PlaySound(gBackSound);
				Application.LoadLevel("MainMenu");
			}
		}
		GUI.Label(Utility.NewRect(0f, num4, Utility.RefWidth - 1f, m_LabelHeight), Localization.GetLocalizedText(ELoc.EnableSound), m_BlackBackgroundStyle);
		GUISkin skin = GUI.skin;
		GUI.skin = m_OptionsSkin;
		num4 += m_LabelHeight;
		if (m_MusicButton == 1)
		{
			if (Utility.InputStoppedByActivity())
			{
				GUI.Label(Utility.NewRect(num3 - num - 1f, num4, num, num2), string.Empty, "MusicOnSelect");
				GUI.Label(Utility.NewRect(num3 + 1f, num4, num, num2), string.Empty, "MusicOffUnselect");
			}
			else
			{
				if (GUI.Button(Utility.NewRect(num3 - num - 1f, num4, num, num2), string.Empty, "MusicOnSelect"))
				{
				}
				if (GUI.Button(Utility.NewRect(num3 + 1f, num4, num, num2), string.Empty, "MusicOffUnselect"))
				{
					m_DisableMusicConfirm = true;
				}
			}
		}
		else if (Utility.InputStoppedByActivity())
		{
			GUI.Label(Utility.NewRect(num3 - num - 1f, num4, num, num2), string.Empty, "MusicOnUnselect");
			GUI.Label(Utility.NewRect(num3 + 1f, num4, num, num2), string.Empty, "MusicOffSelect");
		}
		else
		{
			if (GUI.Button(Utility.NewRect(num3 - num - 1f, num4, num, num2), string.Empty, "MusicOnUnselect"))
			{
				m_EnableMusicConfirm = true;
			}
			if (!GUI.Button(Utility.NewRect(num3 + 1f, num4, num, num2), string.Empty, "MusicOffSelect"))
			{
			}
		}
		num4 += num2;
		GUI.skin = skin;
		num4 += m_Separator;
		num = 280f;
		num2 = 50f;
		float left = (Utility.RefWidth - num) * 0.5f;
		if (Utility.InputStoppedByActivity())
		{
			GUI.Label(Utility.NewRect(left, num4, num, num2), Localization.GetLocalizedText(ELoc.ResetSave), m_TextButtonLarge);
		}
		else if (GUI.Button(Utility.NewRect(left, num4, num, num2), Localization.GetLocalizedText(ELoc.ResetSave), m_TextButtonLarge))
		{
			m_ResetConfirm = true;
		}
		num4 += m_DefaultHeight;
		num = 280f;
		num2 = 50f;
		if (Utility.InputStoppedByActivity())
		{
			GUI.Label(Utility.NewRect(left, num4, num, num2), Localization.GetLocalizedText(ELoc.CreditsTitle), m_TextButtonLarge);
		}
		else if (GUI.Button(Utility.NewRect(left, num4, num, num2), Localization.GetLocalizedText(ELoc.CreditsTitle), m_TextButtonLarge))
		{
			SetState(EState.Credits);
		}
		num4 += m_DefaultHeight;
	}

	private void OnEnterMain()
	{
	}

	private void OnUpdateMain()
	{
	}

	private void OnLeaveMain()
	{
	}

	private void StartStoreKit()
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

	private void StoreKitOnDisable()
	{
		UnRegisterStoreKit();
		UnRegisterIAB();
	}

	private void RestoreCompletedTransaction()
	{
		if (!CanMakePayments())
		{
			SetMessage(EMessage.PurchaseDisable);
			return;
		}
		Utility.ShowActivityView(true);
		m_OldUnlockProducts = Utility.GetUnlockedProducts();
		RestoreCompletedTransactions();
	}

	private void RequestProductList()
	{
		/*if (m_ProductList == null)
		{
			m_ProductList = new List<StoreKitProduct>();
		}
		else
		{
			m_ProductList.Clear();
		}
		for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.UnavailableCount; eProductID++)
		{
			if (eProductID != EProductID.AvailableCount)
			{
				string text = eProductID.ToString();
				m_ProductList.Add(StoreKitProduct.productFromString(text + "|||" + text + "|||" + text + "|||0.79|||€"));
			}
		}*/
	}

	private void BuyProduct(int productID)
	{
		/*if (m_ProductList != null && productID < 2)
		{
			PurchaseProduct(m_ProductList[productID].productIdentifier);
		}*/
	}

	private void StartBilling()
	{
		//IABAndroid.startCheckBillingAvailableRequest();
	}

	private void RegisterIAB()
	{
		/*IABAndroidManager.billingSupportedEvent += OnBillingSupportedEvent;
		IABAndroidManager.purchaseSucceededEvent += OnPurchaseSucceededEvent;
		IABAndroidManager.purchaseSignatureVerifiedEvent += OnPurchaseSignatureVerifiedEvent;
		IABAndroidManager.purchaseCancelledEvent += OnPurchaseCancelledEvent;
		IABAndroidManager.purchaseRefundedEvent += OnPurchaseRefundedEvent;
		IABAndroidManager.purchaseFailedEvent += OnPurchaseFailedEvent;
		IABAndroidManager.transactionRestoreFailedEvent += OnTransactionRestoreFailedEvent;
		IABAndroidManager.transactionsRestoredEvent += OnTransactionsRestoredEvent;*/
	}

	private void UnRegisterIAB()
	{
		/*IABAndroidManager.billingSupportedEvent -= OnBillingSupportedEvent;
		IABAndroidManager.purchaseSucceededEvent -= OnPurchaseSucceededEvent;
		IABAndroidManager.purchaseSignatureVerifiedEvent -= OnPurchaseSignatureVerifiedEvent;
		IABAndroidManager.purchaseCancelledEvent -= OnPurchaseCancelledEvent;
		IABAndroidManager.purchaseRefundedEvent -= OnPurchaseRefundedEvent;
		IABAndroidManager.purchaseFailedEvent -= OnPurchaseFailedEvent;
		IABAndroidManager.transactionRestoreFailedEvent -= OnTransactionRestoreFailedEvent;
		IABAndroidManager.transactionsRestoredEvent -= OnTransactionsRestoredEvent;*/
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
		Utility.UnlockProduct(productIDEnumWithStoreProductID);
		if (m_AchievementsTable != null)
		{
			m_AchievementsTable.UnlockPack();
		}
		DataMining.Increment(DataMining.EGeneral.Purchase.ToString());
		SetMessage(EMessage.PurchaseSuccess);
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

	private void OnTransactionsRestoredEvent()
	{
		Utility.Log(ELog.Info, "OnTransactionsRestoredEvent");
		Utility.ResetUnlockedProducts();
		int unlockedProducts = Utility.GetUnlockedProducts();
		if (unlockedProducts == 0)
		{
			SetMessage(EMessage.PurchaseEmpty);
		}
		else if (m_OldUnlockProducts < unlockedProducts)
		{
			SetMessage(EMessage.RestoreTransactionSucceded);
		}
		else
		{
			SetMessage(EMessage.PurchaseInstalled);
		}
		OnRestoreTransactionEnded();
	}

	private void OnTransactionRestoreFailedEvent(string productId)
	{
		Utility.Log(ELog.Errors, "OnTransactionRestoreFailedEvent" + productId);
		SetMessage(EMessage.RestoreTransactionFailed);
		OnRestoreTransactionEnded();
	}

	private void RegisterStoreKit()
	{
		/*StoreKitManager.restoreTransactionsFinished += OnRestoreTransactionSucceded;
		StoreKitManager.restoreTransactionsFailed += OnRestoreTransactionFailed;
		StoreKitManager.productListReceived += OnProductListReceived;
		StoreKitManager.productListRequestFailed += OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful += OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled += OnPurchaseCancelled;
		StoreKitManager.purchaseFailed += OnPurchaseFailed;*/
	}

	private void UnRegisterStoreKit()
	{
		/*StoreKitManager.restoreTransactionsFinished -= OnRestoreTransactionSucceded;
		StoreKitManager.restoreTransactionsFailed -= OnRestoreTransactionFailed;
		StoreKitManager.productListReceived -= OnProductListReceived;
		StoreKitManager.productListRequestFailed -= OnProductListRequestFailed;
		StoreKitManager.purchaseSuccessful -= OnPurchaseSucceded;
		StoreKitManager.purchaseCancelled -= OnPurchaseCancelled;
		StoreKitManager.purchaseFailed -= OnPurchaseFailed;*/
	}

	private void OnRestoreTransactionSucceded()
	{
		Utility.Log(ELog.Info, "OnRestoreTransactionSucceded");
		Utility.ResetUnlockedProducts();
		int unlockedProducts = Utility.GetUnlockedProducts();
		if (unlockedProducts == 0)
		{
			Utility.Log(ELog.Info, "unlockProducts == 0");
			SetMessage(EMessage.PurchaseEmpty);
		}
		else if (m_OldUnlockProducts < unlockedProducts)
		{
			Utility.Log(ELog.Info, "m_OldUnlockProducts < unlockProducts, " + m_OldUnlockProducts + ", " + unlockedProducts);
			SetMessage(EMessage.RestoreTransactionSucceded);
		}
		else
		{
			Utility.Log(ELog.Info, "PurchaseInstalled, " + m_OldUnlockProducts + ", " + unlockedProducts);
			SetMessage(EMessage.PurchaseInstalled);
		}
		OnRestoreTransactionEnded();
	}

	private void OnRestoreTransactionFailed(string error)
	{
		Utility.Log(ELog.Errors, "OnRestoreTransactionFailed: " + error);
		SetMessage(EMessage.RestoreTransactionFailed);
		OnRestoreTransactionEnded();
	}

	/*private void OnProductListReceived(List<StoreKitProduct> productList)
	{
		Utility.Log(ELog.Errors, "OnProductListReceived: " + productList.Count);
		if (productList.Count > 0)
		{
			m_ProductList = productList;
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

	private bool CanMakePayments()
	{
		return m_CanMakePayments;
	}

	private void PurchaseProduct(string productID)
	{
		//IABAndroid.purchaseProduct(productID);
	}

	private void RestoreCompletedTransactions()
	{
		//IABAndroid.restoreTransactions();
	}
}
