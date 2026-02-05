using UnityEngine;

public class SplashScreen : MonoBehaviour
{
	public enum ESplash
	{
		Ubisoft = 0,
		Count = 1
	}

	public Texture2D[] m_Splashes = new Texture2D[1];

	public GUISkin m_CommonSkin;

	private float m_SplashTime = 3.5f;

	private ESplash m_CurrentSplash;

	private float m_Time;

	private Rect m_Rect = default(Rect);

	private Rect m_LegalLineRect = default(Rect);

	private string m_LegalLine = string.Empty;

	private GUIStyle m_BlackTextContent;

	private int m_LastTouchCount;

	private int m_CheatStep;

	private int[] m_CheatCode = new int[7] { 1, 2, 3, 4, 3, 2, 1 };

	private bool m_LoadLevel;

	private void Awake()
	{
		AllInput.ActivateAutoRotateFrame(false);
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = true;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
	}

	private void Start()
	{
		GlobalVariables.ClearPlayerPrefs(false);
		DataMining.OnLevelStart();
		//IABAndroid.init("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlg98dmascsLxJ+jsX170aVsLThx1aXhj0sgRnyE04zPbKEzDBeuDOG1X0tHvYILct+LyTlIuIcD/6Ax4H9W2dJddmlXQ9sw20+F5RBpvbualVEC1PeFfalDocFQJHIAlIHAR9AaqLu/H9ggpV85Mkya0YecKj1Qu0TRR6Vf8RCdYIwH/FhaIUVukLPOGhcK49cugpEW7YgZIkQajos+KFXSnoJnBSBgU+c1OTJu79BAjB9dQ3J2ItvhIrPkOW/d5TZuTfeuwtbEAGLkbxrMBrOAWWnGeWAb1I3PmxwOcTZ7XYMwMQJZCLYOS0EdC3fur7lu99bSGcWG/p6icNjf1wQIDAQAB");
		ComputeRect();
		InitHDLD();
		InitDistrib();
		float num = 5f;
		m_LegalLineRect = Utility.NewRect(num, Utility.RefHeight - 80f, Utility.RefWidth - 2f * num, 80f);
		m_BlackTextContent = m_CommonSkin.GetStyle("BlackTextContent");
		m_BlackTextContent.fixedWidth = m_LegalLineRect.width;
		m_BlackTextContent.fontSize = 30;
		if (GlobalVariables.s_CrossPromo)
		{
			//CrossPromoBinding.ReloadWebNews();
			//CrossPromoBinding.ReloadUrgentNews();
		}
	}

	private void Update()
	{
		if (m_LoadLevel)
		{
			m_BlackTextContent.fixedWidth = 0f;
			Localization.GenerateWaitText();
			Application.LoadLevel("SoundSelection");
			return;
		}
		m_Time += Time.deltaTime;
		if (!(m_Time > m_SplashTime))
		{
			return;
		}
		m_Time = 0f;
		m_CurrentSplash++;
		if (m_CurrentSplash == ESplash.Count)
		{
			m_LoadLevel = true;
			Utility.CreateMenuBackground();
			Utility.Log(ELog.Info, "Load SoundSelection");
			GameObject gameObject = GameObject.Find("Main Camera");
			if (gameObject != null)
			{
				gameObject.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f, 0f);
			}
		}
		else
		{
			ComputeRect();
		}
	}

	private void OnGUI()
	{
		if (m_CurrentSplash != ESplash.Count)
		{
			GUIStyle gUIStyle = new GUIStyle();
			ComputeRect();
			gUIStyle.normal.background = m_Splashes[(int)m_CurrentSplash];
			GUI.Label(m_Rect, string.Empty, gUIStyle);
			GUI.skin = m_CommonSkin;

            GUI.Label(m_LegalLineRect, m_LegalLine, m_BlackTextContent);
		}
	}

	private void ComputeRect()
	{
		Texture2D texture2D = m_Splashes[(int)m_CurrentSplash];
		float num = 0f;
		float num2 = 0f;
		float num3 = texture2D.width;
		float num4 = texture2D.height;
		if ((texture2D.width >= Screen.width || texture2D.height >= Screen.height) && texture2D.width != 0 && texture2D.height != 0 && Screen.height != 0)
		{
			float num5 = (float)texture2D.width / (float)texture2D.height;
			float num6 = (float)Screen.width / (float)Screen.height;
			if (num5 < num6)
			{
				float num7 = (float)Screen.height / (float)texture2D.height;
				num3 *= num7;
				num4 *= num7;
			}
			else
			{
				float num8 = (float)Screen.width / (float)texture2D.width;
				num3 *= num8;
				num4 *= num8;
			}
		}
		num = ((float)Screen.height - num4) / 2f;
		num2 = ((float)Screen.width - num3) / 2f;
		if (Utility.IsCheater())
		{
			num2 += num3 / 4f;
			num += num4 / 4f;
			num3 /= 2f;
			num4 /= 2f;
			m_Rect = new Rect(num2, num, num3, num4);
		}
		else
		{
			m_Rect = new Rect(num2, num, num3, num4);
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
		}
	}

	private void CheckCheat()
	{
		if (Utility.IsCheater())
		{
			return;
		}
		Utility.ActivateCheats();
		int touchCount = AllInput.GetTouchCount();
		if (m_LastTouchCount != touchCount)
		{
			if (touchCount == m_CheatCode[m_CheatStep])
			{
				if (++m_CheatStep == m_CheatCode.Length)
				{
					Utility.ActivateCheats();
				}
			}
			else
			{
				m_CheatStep = 0;
			}
		}
		else if (Input.GetKeyDown(KeyCode.T))
		{
			Utility.ActivateCheats();
		}
		m_LastTouchCount = AllInput.GetTouchCount();
	}

	private void InitHDLD()
	{
		bool hD = Mathf.Min(Screen.width, Screen.height) >= 410;
		Utility.SetHD(hD);
		Utility.Log(ELog.Device, "Game is launched HD: " + Utility.IsHD());
		if (Localization.GetLanguage() == SystemLanguage.French)
		{
			if (Utility.IsUniversal())
			{
				m_LegalLine = "© 2012 Ubisoft Entertainment. Tous droits réservés. Lapins Crétins, Ubisoft et le logo Ubisoft sont des marques déposées appartenant à Ubisoft Entertainment.";
			}
			else
			{
				m_LegalLine = "© 2009-2012 Ubisoft Entertainment. Tous droits réservés. Lapins Crétins, Ubisoft et le logo Ubisoft sont des marques déposées appartenant à Ubisoft Entertainment.";
			}
		}
		else if (Utility.IsUniversal())
		{
			m_LegalLine = "© 2012 Ubisoft Entertainment. All Rights Reserved. Rabbids, Ubisoft and the Ubisoft logo are trademarks of Ubisoft Entertainment in the U.S. and/or other countries.";
		}
		else
		{
			m_LegalLine = "© 2009-2012 Ubisoft Entertainment. All Rights Reserved. Rabbids, Ubisoft and the Ubisoft logo are trademarks of Ubisoft Entertainment in the U.S. and/or other countries.";
		}
	}

	private void InitDistrib()
	{
		TextAsset textAsset = (TextAsset)Resources.Load("iDistrib");
		if (textAsset != null)
		{
			Utility.SetUniversal(!textAsset.text.Contains("iPhone"));
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to read iDistrib");
		}
		Utility.Log(ELog.Device, "IsUniversal: " + Utility.IsUniversal());
	}
}
