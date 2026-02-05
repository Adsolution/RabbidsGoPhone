using UnityEngine;

public class GamePlayerPrefs : MonoBehaviour
{
	private static int s_DeletionCounter;

	private static int s_CreationCounter;

	public static void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
	}

	public static int GetInt(string key)
	{
		return PlayerPrefs.GetInt(key);
	}

	public static void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
	}

	public static void SetString(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
	}

	public static void DeleteKey(string key)
	{
		if (PlayerPrefs.HasKey(key))
		{
			PlayerPrefs.DeleteKey(key);
			s_DeletionCounter++;
		}
	}

	public static void DeleteAll()
	{
		s_DeletionCounter = 0;
		for (Rabbid.ECostume eCostume = Rabbid.ECostume.Naked; eCostume < Rabbid.ECostume.Count; eCostume++)
		{
			string key = AchievementTable.EGoody.Costume.ToString() + "_" + eCostume.ToString() + "_NoLongerNew";
			DeleteKey(key);
		}
		for (Rabbid.EEnvironment eEnvironment = Rabbid.EEnvironment.Standard; eEnvironment < Rabbid.EEnvironment.Count; eEnvironment++)
		{
			string key = AchievementTable.EGoody.Environment.ToString() + "_" + eEnvironment.ToString() + "_NoLongerNew";
			DeleteKey(key);
		}
		for (PhotoRoom.EAREighty eAREighty = PhotoRoom.EAREighty.BTS_AW3D_RAB08_0002_Resized_PhotoMontage; eAREighty < PhotoRoom.EAREighty.Count; eAREighty++)
		{
			string key = AchievementTable.EGoody.AREighty.ToString() + "_" + eAREighty.ToString() + "_NoLongerNew";
			DeleteKey(key);
		}
		for (PhotoRoom.EARPose eARPose = PhotoRoom.EARPose.pose01; eARPose < PhotoRoom.EARPose.Count; eARPose++)
		{
			string key = AchievementTable.EGoody.ARPose.ToString() + "_" + eARPose.ToString() + "_NoLongerNew";
			DeleteKey(key);
		}
		for (OutWorld2D.EWallpaper eWallpaper = OutWorld2D.EWallpaper.Wallpaper1; eWallpaper < OutWorld2D.EWallpaper.Count; eWallpaper++)
		{
			string key = AchievementTable.EGoody.Wallpaper.ToString() + "_" + eWallpaper.ToString() + "_NoLongerNew";
			DeleteKey(key);
		}
		for (InGameScript.EState eState = InGameScript.EState.ClownBox; eState < InGameScript.EState.Count; eState++)
		{
			string key = AchievementTable.EGoody.MiniGame.ToString() + "_" + eState.ToString() + "_NoLongerNew";
			DeleteKey(key);
			key = eState.ToString() + "_Locked";
			DeleteKey(key);
			key = eState.ToString() + "EndedActionCount";
			DeleteKey(key);
			for (int i = 0; i < 4; i++)
			{
				key = eState.ToString() + "_Action_" + i + "_Locked";
				DeleteKey(key);
			}
		}
		DeleteKey("_number_of_moves");
		DeleteKey("_number_of_ended_moves");
		DeleteKey("_number_of_mini_game_moves");
		DeleteKey("_number_of_ended_mini_game_moves");
		DeleteKey("_total_number_of_moves");
		DeleteKey("_total_number_of_ended_moves");
		DeleteKey("upgrade");
		DeleteKey("_new_wallpaper");
		DeleteKey("_new_environment");
		DeleteKey("CostumeName");
		DeleteKey("EnvironmentName");
		DeleteKey("InGameTime");
		DeleteKey("blow");
		DeleteKey("yell");
		DeleteKey("poke_gift");
		DeleteKey("steam");
		DeleteKey("dance");
		DeleteKey("ear");
		DeleteKey("string");
		DeleteKey("tickle");
		DeleteKey("turn");
		DeleteKey("burp");
		DeleteKey("rotate");
		DeleteKey("rotate_down");
		DeleteKey("wallbounce_screen");
		DeleteKey("wallbounce_vertical");
		DeleteKey("wallbounce_horizontal");
		DeleteKey("poke_body");
		DeleteKey("poke_eye");
		DeleteKey("run");
		DeleteKey("_init_new_move");
		Utility.Log(ELog.Info, "DeleteAll is over, deleted: " + s_DeletionCounter);
	}

	public static void DisplayPlayerPrefsStats()
	{
		Utility.Log(ELog.Info, "s_CreationCounter: " + s_CreationCounter);
	}
}
