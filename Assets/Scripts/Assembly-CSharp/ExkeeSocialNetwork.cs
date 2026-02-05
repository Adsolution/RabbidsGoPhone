using System;
using UnityEngine;

public class ExkeeSocialNetwork : MonoBehaviour
{
	private static ExkeeSocialNetwork s_Instance;

	private object m_ObjectToPostFacebook;

	public static event Action OnFacebookPostSucceededEvent;

	public static event Action OnFacebookPostFailedEvent;

	private void Awake()
	{
		s_Instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		//FacebookAndroid.init(GlobalVariables.FB_APP_ID);
		RegisterSocialNetworking();
	}

	public static void PostOnFacebook(object o)
	{
		if (s_Instance == null)
		{
			Utility.Log(ELog.Errors, "PostOnFacebook failed, instance is null");
			return;
		}
		s_Instance.m_ObjectToPostFacebook = o;
		if (s_Instance.FacebookIsLoggedIn())
		{
			Utility.Log(ELog.Plugin, "PostOnFacebook: ProcessFacebookPost");
			s_Instance.ProcessFacebookPost();
		}
		else
		{
			Utility.Log(ELog.Plugin, "PostOnFacebook: FacebookLogIn");
			s_Instance.FacebookLogIn();
		}
	}

	private void RegisterSocialNetworking()
	{
		/*FacebookManager.loginSucceededEvent += OnFacebookLogInSucceeded;
		FacebookManager.loginFailedEvent += OnFacebookLogInFailed;*/
	}

	private void UnregisterSocialNetworking()
	{
		/*FacebookManager.loginSucceededEvent -= OnFacebookLogInSucceeded;
		FacebookManager.loginFailedEvent -= OnFacebookLogInFailed;*/
	}

	private bool FacebookIsLoggedIn()
	{
		return true /*FacebookAndroid.isSessionValid()*/;
	}

	private void FacebookLogIn()
	{
		if (!FacebookIsLoggedIn())
		{
			Utility.Log(ELog.Plugin, "FacebookLogin :: Session is not valid");
			//FacebookAndroid.loginWithRequestedPermissions(new string[3] { "publish_stream", "email", "user_birthday" });
		}
	}

	private void ProcessFacebookPost()
	{
		Utility.ShowActivityView(true);
		if (m_ObjectToPostFacebook.GetType() == typeof(Texture2D))
		{
			Utility.Log(ELog.Plugin, "ProcessFacebookPost: doEncoding");
			//JPGEncoder jPGEncoder = new JPGEncoder((Texture2D)m_ObjectToPostFacebook, 75f);
			//jPGEncoder.doEncoding();
			//byte[] bytes = jPGEncoder.GetBytes();
			Utility.Log(ELog.Plugin, "ProcessFacebookPost: postImage");
			//Facebook.instance.postImage(bytes, string.Empty, FacebookPostCompletionHandler);
			Utility.Log(ELog.Plugin, "ProcessFacebookPost: done");
		}
		else
		{
			object[] array = new object[1] { m_ObjectToPostFacebook };
			Utility.Log(ELog.Plugin, "ProcessFacebookPost: postImage");
			//Facebook.instance.postMessage((string)array[0], FacebookPostCompletionHandler);
			Utility.Log(ELog.Plugin, "ProcessFacebookPost: done");
		}
		m_ObjectToPostFacebook = null;
		Utility.FreeMem(true);
	}

	private void FacebookPostCompletionHandler(string error, object result)
	{
		Utility.Log(ELog.Plugin, "FacebookPostCompletionHandler: " + error + " / " + result);
		if (error != null)
		{
			OnFacebookPostFailed(error);
		}
		else
		{
			OnFacebookPostSucceeded(result);
		}
	}

	private void OnFacebookLogInSucceeded()
	{
		Utility.Log(ELog.Plugin, "OnFacebookLogInSucceeded");
		ProcessFacebookPost();
	}

	private void OnFacebookLogInFailed(string error)
	{
		Utility.Log(ELog.Errors, "OnFacebookLogInFailed: " + error);
		m_ObjectToPostFacebook = null;
	}

	private void OnFacebookPostSucceeded(object result)
	{
		Utility.Log(ELog.Plugin, "OnFacebookPostSucceeded: " + result);
		if (ExkeeSocialNetwork.OnFacebookPostSucceededEvent != null)
		{
			ExkeeSocialNetwork.OnFacebookPostSucceededEvent();
		}
		OnEndFacebookPost();
	}

	private void OnFacebookPostFailed(string error)
	{
		Utility.Log(ELog.Errors, "OnFacebookPostFailed: " + error);
		if (ExkeeSocialNetwork.OnFacebookPostFailedEvent != null)
		{
			ExkeeSocialNetwork.OnFacebookPostFailedEvent();
		}
		OnEndFacebookPost();
	}

	private void OnEndFacebookPost()
	{
		Utility.Log(ELog.Plugin, "OnEndFacebookPost");
		Utility.HideActivityView(true);
		AllInput.ActivateAutoRotateFrame(false);
	}
}
