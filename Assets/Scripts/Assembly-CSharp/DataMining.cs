using System.Collections;
using UnityEngine;

public class DataMining : MonoBehaviour
{
	public enum EGeneral
	{
		StartApplication = 0,
		UnlockedSucces = 1,
		Achievements = 2,
		Leaderboard = 3,
		ResetSave = 4,
		Purchase = 5,
		FrontPhotoTaken = 6,
		BackPhotoTaken = 7,
		PhotoPostedByFB = 8,
		PhotoPostedByMail = 9,
		PhotoSaved = 10,
		WallpaperSaved = 11,
		PlayAfterCostumize = 12,
		PhotoAfterCostumize = 13,
		Count = 14
	}

	public enum ETime
	{
		ApplicationDuration = 0,
		PlayingTime = 1,
		TimeSinceLastSuccess = 2,
		Count = 3
	}

	private static string s_AddUp = "A_";

	private static string s_Replace = "R_";

	private static string s_Menu = "M_";

	private static string s_Costume = "C_";

	private static string s_Environment = "E_";

	private static string s_Video = "V_";

	private static string s_General = "G_";

	private static string s_Time = "T_";

	public static string s_Applied = "A_";

	public static string s_Seen = "S_";

	private static int s_LastPauseTime = 0;

	private static float s_LastSuccessTime = -1f;

	private static Hashtable s_Save = new Hashtable();

	private static string s_VideoIdsSave;

	private static float s_DataMiningAfterCustomeTime = 10f;

	private static float s_AfterCustomeTime = -1f;

	private void Start()
	{
		Increment(EGeneral.StartApplication.ToString());
		if (IsValid())
		{
			SendDatas();
		}
		else
		{
			Utility.CallWWW(this, GlobalVariables.DM_SERVER_URL, OnEndAskUID, null);
		}
	}

	public static bool IsValid()
	{
		return PlayerPrefs.GetInt("UID") != 0;
	}

	public static void Increment(string key)
	{
		int num = PlayerPrefs.GetInt(key);
		GamePlayerPrefs.SetInt(key, num + 1);
	}

	public static void UnlockSuccess()
	{
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		string key = ETime.TimeSinceLastSuccess.ToString();
		string key2 = EGeneral.UnlockedSucces.ToString();
		int num = PlayerPrefs.GetInt(key2);
		GamePlayerPrefs.SetInt(key, 0);
		GamePlayerPrefs.SetInt(key2, num + 1);
		s_LastSuccessTime = timeSinceLevelLoad;
	}

	public static void OnConfirmCostume(Rabbid.ECostume costume)
	{
		Increment(s_Applied + costume);
		s_AfterCustomeTime = s_DataMiningAfterCustomeTime;
	}

	public static void OnTakePhoto(PhotoRoom.EBackMode backMode)
	{
		switch (backMode)
		{
		case PhotoRoom.EBackMode.CameraFront:
			Increment(EGeneral.FrontPhotoTaken.ToString());
			break;
		case PhotoRoom.EBackMode.CameraBack:
			Increment(EGeneral.BackPhotoTaken.ToString());
			break;
		}
		if (s_AfterCustomeTime > 0f)
		{
			Increment(EGeneral.PhotoAfterCostumize.ToString());
			s_AfterCustomeTime = -1f;
		}
	}

	public static void InGameUpdate()
	{
		if (s_AfterCustomeTime > 0f)
		{
			s_AfterCustomeTime -= Time.deltaTime;
			if (s_AfterCustomeTime <= 0f)
			{
				s_AfterCustomeTime = -1f;
				Increment(EGeneral.PlayAfterCostumize.ToString());
			}
		}
	}

	public static void InGameOnDisable()
	{
		if (s_AfterCustomeTime > 0f)
		{
			s_AfterCustomeTime = -1f;
			Increment(EGeneral.PlayAfterCostumize.ToString());
		}
	}

	public static void OnLeaveGameMode()
	{
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		int num = Mathf.FloorToInt(timeSinceLevelLoad);
		string key = ETime.PlayingTime.ToString();
		int value = PlayerPrefs.GetInt(key) - s_LastPauseTime + num;
		GamePlayerPrefs.SetInt(key, value);
		key = ETime.TimeSinceLastSuccess.ToString();
		value = ((!(s_LastSuccessTime > 0f)) ? (PlayerPrefs.GetInt(key) - s_LastPauseTime + num) : Mathf.FloorToInt(timeSinceLevelLoad - s_LastSuccessTime));
		GamePlayerPrefs.SetInt(key, value);
	}

	public static void OnApplicationQuit(bool inGame)
	{
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		int num = Mathf.FloorToInt(timeSinceLevelLoad);
		string key = ETime.ApplicationDuration.ToString();
		int value = PlayerPrefs.GetInt(key) - s_LastPauseTime + Mathf.FloorToInt(Time.realtimeSinceStartup);
		GamePlayerPrefs.SetInt(key, value);
		if (inGame)
		{
			OnLeaveGameMode();
		}
		s_LastPauseTime = num;
	}

	public static void OnLevelStart()
	{
		s_LastPauseTime = 0;
		s_LastSuccessTime = -1f;
		s_AfterCustomeTime = -1f;
	}

	public static void SaveDatas()
	{
		string key = "UID";
		int num = PlayerPrefs.GetInt(key);
		s_Save.Add(key, num);
		for (int i = 0; i < 8; i++)
		{
			key = ((MainMenuScript.EMenu)i).ToString();
			num = PlayerPrefs.GetInt(key);
			if (num > 0)
			{
				s_Save.Add(key, num);
			}
		}
		for (int i = 0; i < 12; i++)
		{
			key = ((Rabbid.ECostume)i).ToString();
			num = PlayerPrefs.GetInt(s_Applied + key);
			if (num > 0)
			{
				s_Save.Add(s_Applied + key, num);
			}
			num = PlayerPrefs.GetInt(s_Seen + key);
			if (num > 0)
			{
				s_Save.Add(s_Seen + key, num);
			}
		}
		for (int i = 0; i < 8; i++)
		{
			key = ((Rabbid.EEnvironment)i).ToString();
			num = PlayerPrefs.GetInt(s_Applied + key);
			if (num > 0)
			{
				s_Save.Add(s_Applied + key, num);
			}
			num = PlayerPrefs.GetInt(s_Seen + key);
			if (num > 0)
			{
				s_Save.Add(s_Seen + key, num);
			}
		}
		s_VideoIdsSave = PlayerPrefs.GetString("VideoIds");
		char[] separator = new char[1] { '/' };
		string[] array = s_VideoIdsSave.Split(separator);
		for (int i = 0; i < array.Length; i++)
		{
			key = array[i];
			num = PlayerPrefs.GetInt(key);
			if (num > 0)
			{
				s_Save.Add(key, num);
			}
		}
		for (int i = 0; i < 14; i++)
		{
			key = ((EGeneral)i).ToString();
			num = PlayerPrefs.GetInt(key);
			if (num > 0)
			{
				s_Save.Add(key, num);
			}
		}
		for (int i = 0; i < 3; i++)
		{
			key = ((ETime)i).ToString();
			num = PlayerPrefs.GetInt(key);
			if (num > 0)
			{
				s_Save.Add(key, num);
			}
		}
	}

	public static void LoadDatas()
	{
		GamePlayerPrefs.SetString("VideoIds", s_VideoIdsSave);
		foreach (DictionaryEntry item in s_Save)
		{
			GamePlayerPrefs.SetInt((string)item.Key, (int)item.Value);
		}
		s_Save.Clear();
	}

	private static void Clear()
	{
		for (int i = 0; i < 8; i++)
		{
			PlayerPrefs.DeleteKey(((MainMenuScript.EMenu)i).ToString());
		}
		string text;
		for (int i = 0; i < 12; i++)
		{
			text = ((Rabbid.ECostume)i).ToString();
			PlayerPrefs.DeleteKey(s_Applied + text);
			PlayerPrefs.DeleteKey(s_Seen + text);
		}
		for (int i = 0; i < 8; i++)
		{
			text = ((Rabbid.EEnvironment)i).ToString();
			PlayerPrefs.DeleteKey(s_Applied + text);
			PlayerPrefs.DeleteKey(s_Seen + text);
		}
		text = "VideoIds";
		string text2 = PlayerPrefs.GetString(text);
		char[] separator = new char[1] { '/' };
		string[] array = text2.Split(separator);
		for (int i = 0; i < array.Length; i++)
		{
			PlayerPrefs.DeleteKey(array[i]);
		}
		PlayerPrefs.DeleteKey(text);
		for (int i = 0; i < 14; i++)
		{
			PlayerPrefs.DeleteKey(((EGeneral)i).ToString());
		}
		for (int i = 0; i < 3; i++)
		{
			PlayerPrefs.DeleteKey(((ETime)i).ToString());
		}
	}

	private void SendDatas()
	{
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("UID", PlayerPrefs.GetInt("UID"));
		for (int i = 0; i < 8; i++)
		{
			string text = ((MainMenuScript.EMenu)i).ToString();
			int num = PlayerPrefs.GetInt(text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Menu + text, num);
			}
		}
		for (int i = 0; i < 12; i++)
		{
			string text = ((Rabbid.ECostume)i).ToString();
			int num = PlayerPrefs.GetInt(s_Applied + text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Costume + s_Applied + text, num);
			}
			num = PlayerPrefs.GetInt(s_Seen + text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Costume + s_Seen + text, num);
			}
		}
		for (int i = 0; i < 8; i++)
		{
			string text = ((Rabbid.EEnvironment)i).ToString();
			int num = PlayerPrefs.GetInt(s_Applied + text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Environment + s_Applied + text, num);
			}
			num = PlayerPrefs.GetInt(s_Seen + text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Environment + s_Seen + text, num);
			}
		}
		string text2 = PlayerPrefs.GetString("VideoIds");
		char[] separator = new char[1] { '/' };
		string[] array = text2.Split(separator);
		foreach (string text in array)
		{
			int num = PlayerPrefs.GetInt(text);
			if (num > 0)
			{
				wWWForm.AddField(s_AddUp + s_Video + text, num);
			}
		}
		for (int i = 0; i < 14; i++)
		{
			string text = ((EGeneral)i).ToString();
			int num = PlayerPrefs.GetInt(text);
			if (num > 0)
			{
				if (i < 14)
				{
					wWWForm.AddField(s_AddUp + s_General + text, num);
				}
				else if (i < 14)
				{
					wWWForm.AddField(s_Replace + s_General + text, num);
				}
			}
		}
		for (int i = 0; i < 3; i++)
		{
			string text = ((ETime)i).ToString();
			int num = PlayerPrefs.GetInt(text);
			if (num > 0)
			{
				if (i < 2)
				{
					wWWForm.AddField(s_AddUp + s_Time + text, num);
				}
				else if (i < 3)
				{
					wWWForm.AddField(s_Replace + s_Time + text, num);
				}
			}
		}
		Utility.CallWWW(this, GlobalVariables.DM_SERVER_URL, wWWForm, OnEndSendDatas, null);
	}

	private void OnEndSendDatas(WWW www, object[] list)
	{
		string text = www.text;
		if (CheckResult(text))
		{
			ProcessResult(text);
		}
		else if (text.Contains("error"))
		{
			Utility.Log(ELog.Errors, text);
		}
		else
		{
			Clear();
		}
	}

	private void OnEndAskUID(WWW www, object[] list)
	{
		string text = www.text;
		if (CheckResult(text))
		{
			ProcessResult(text);
		}
		else
		{
			Utility.Log(ELog.Errors, text);
		}
	}

	private bool CheckResult(string result)
	{
		if (result.Length > 4)
		{
			return result.Substring(0, 4).Equals("UID:");
		}
		return false;
	}

	private void ProcessResult(string result)
	{
		if (result.Length > 4)
		{
			string s = result.Substring(4);
			GamePlayerPrefs.SetInt("UID", int.Parse(s));
		}
	}
}
