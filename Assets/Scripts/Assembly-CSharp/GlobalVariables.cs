using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
	public static int SoundEnabled = 1;

	public static int BUTTON_WIDTH = 217;

	public static int BUTTON_HEIGHT = 68;

	public static int BACK_BUTTON_WIDTH = 64;

	public static int BACK_BUTTON_HEIGHT = 64;

	public static bool FAKE_YELL = false;

	public static bool FAKE_BLOW = false;

	public static bool FAKE_BURP = false;

	public static bool FAKE_HWALLBOUNCE = false;

	public static bool FAKE_VWALLBOUNCE = false;

	public static bool FAKE_SWALLBOUNCE = false;

	public static string s_PhotoPath = "Photo";

	public static string s_MiniPhotoPath = "MiniPhoto";

	public static string s_BackgroundPath = "Background";

	public static string YOUTUBE_UK = "https://gdata.youtube.com/feeds/api/playlists/4D637B9BA9A3CE87";

	public static string YOUTUBE_FR = "https://gdata.youtube.com/feeds/api/playlists/4D637B9BA9A3CE87";

	public static string YOUTUBE_DE = "https://gdata.youtube.com/feeds/api/playlists/4D637B9BA9A3CE87";

	public static string YOUTUBE_IT = "https://gdata.youtube.com/feeds/api/playlists/4D637B9BA9A3CE87";

	public static string YOUTUBE_ES = "https://gdata.youtube.com/feeds/api/playlists/4D637B9BA9A3CE87";

	public static string DM_SERVER_URL = "http://rabbidsgophone.com/zKn2ErNS_push.php";

	public static float s_AveragePower = -168f;

	public static int s_Score = 0;

	public static int s_InGameTime = 1;

	public static float s_TimeAccumulator = 0f;

	private static int s_RefreshTime = 3;

	private static int s_TotalMoveCount = 3;

	private static bool s_UpdateTimer = true;

	public static string GC_LEADERBOARD_ID_LD = "com.ubisoft.rgpa.Leaderboard";

	public static string GC_LEADERBOARD_ID_HD = "com.ubisoft.rgh.hd.Leaderboard";

	public static string GC_ACHIEVEMENT_ID_PREFIX_LD = "com.ubisoft.rgpa.achievement";

	public static string GC_ACHIEVEMENT_ID_PREFIX_HD = "com.ubisoft.rgh.hd.achievement";

	public static bool OF_AVAILABLE = false;

	public static int OF_LEADERBOARD_ID = 856326;

	public static int[] s_OFAchievementIds = new int[35]
	{
		1163382, 1165742, 1165752, 1165762, 1165772, 1165782, 1165792, 0, 1165802, 1165812,
		1165822, 1165832, 1165842, 0, 1165852, 1165862, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0
	};

	public static string APPLE_PRODUCT_ID_PREFIX_LD = "rgpa.";

	public static string APPLE_PRODUCT_ID_PREFIX_HD = "rgh.hd.";

	public static string APPLE_APP_ID = "458043086";

	public static string UA_APP_KEY = "li1q_3auQamY5kcOa2_q6Q";

	public static string UA_APP_SECRET = "9qecxJ1gSBihBiwGiGspxw";

	public static string FB_APP_ID = "108986462528480";

	public static string FB_RABBIDS_PAGE_FR = "lapinscretins";

	public static string FB_RABBIDS_PAGE_UK = "ravingrabbids";

	public static string FB_RABBIDS_PAGE_ES = "RabbidsLocos";

	public static string FB_RABBIDS_PAGE_IT = "rabbidsitalia";

	public static bool FB_FILTERED = true;

	public static string FB_RABBIDS_FILTER_FR = "14431251223";

	public static string FB_RABBIDS_FILTER_UK = "11083361227";

	public static string FB_RABBIDS_FILTER_ES = "87681403030";

	public static string FB_RABBIDS_FILTER_IT = "158897857472927";

	private static int s_TotalEndedMoves = -1;

	public static bool s_PhotoFromMainMenu = false;

	public static bool s_CrossPromo = false;

	private static int GetMiniGameMoveCount()
	{
		int num = 0;
		AchievementTable achievementTable = new AchievementTable();
		for (InGameScript.EState eState = InGameScript.EState.ClownBox; eState < InGameScript.EState.Count; eState++)
		{
			if (achievementTable.IsMiniGameAvailable(eState))
			{
				num += achievementTable.GetActionCount(eState);
			}
		}
		return num;
	}

	public static void ClearPlayerPrefs(bool deleteAll)
	{
		if (deleteAll)
		{
			GamePlayerPrefs.DeleteAll();
			Utility.Log(ELog.Initialize, "GamePlayerPrefs.DeleteAll");
		}
		if (!PlayerPrefs.HasKey("_init_new_move"))
		{
			int num = 13;
			int num2 = 0;
			GamePlayerPrefs.SetInt("_init_new_move", 1);
			GamePlayerPrefs.SetInt("run", 1);
			GamePlayerPrefs.SetInt("poke_eye", 1);
			GamePlayerPrefs.SetInt("poke_body", 1);
			GamePlayerPrefs.SetInt("wallbounce_horizontal", 1);
			GamePlayerPrefs.SetInt("wallbounce_vertical", 1);
			GamePlayerPrefs.SetInt("wallbounce_screen", 1);
			GamePlayerPrefs.SetInt("rotate_down", 1);
			GamePlayerPrefs.SetInt("rotate", 1);
			GamePlayerPrefs.SetInt("burp", 1);
			GamePlayerPrefs.SetInt("turn", 1);
			GamePlayerPrefs.SetInt("tickle", 1);
			GamePlayerPrefs.SetInt("ear", 1);
			GamePlayerPrefs.SetInt("dance", 1);
			GamePlayerPrefs.SetInt("string", 1);
			GamePlayerPrefs.SetInt("steam", 1);
			GamePlayerPrefs.SetInt("poke_gift", 1);
			GamePlayerPrefs.SetInt("_number_of_moves", num);
			if (AllInput.IsMicroAvailable())
			{
				num += 2;
				GamePlayerPrefs.SetInt("yell", 1);
				GamePlayerPrefs.SetInt("blow", 1);
			}
			num2 += GetMiniGameMoveCount();
			GamePlayerPrefs.SetInt("_number_of_ended_moves", 0);
			GamePlayerPrefs.SetInt("_number_of_ended_mini_game_moves", 0);
			GamePlayerPrefs.SetInt("_total_number_of_ended_moves", 0);
			GamePlayerPrefs.SetInt("_number_of_moves", num);
			RecomputeMoveCount();
			GamePlayerPrefs.SetInt("_new_wallpaper", 0);
			GamePlayerPrefs.SetInt("_new_environment", 0);
			GamePlayerPrefs.SetString("CostumeName", string.Empty);
			GamePlayerPrefs.SetString("EnvironmentName", string.Empty);
			Utility.Log(ELog.Info, "Key creation");
		}
		else if (!PlayerPrefs.HasKey("upgrade"))
		{
			int num3 = 13;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			num4 += ((!PlayerPrefs.HasKey("run")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("poke_eye")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("poke_body")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("wallbounce_horizontal")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("wallbounce_vertical")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("wallbounce_screen")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("rotate_down")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("rotate")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("burp")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("turn")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("tickle")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("ear")) ? 1 : 0);
			num4 += ((!PlayerPrefs.HasKey("dance")) ? 1 : 0);
			if (PlayerPrefs.HasKey("string"))
			{
				PlayerPrefs.DeleteKey("string");
			}
			num6 += ((!PlayerPrefs.HasKey("steam")) ? 1 : 0);
			num6 += ((!PlayerPrefs.HasKey("poke_gift")) ? 1 : 0);
			if (AllInput.IsMicroAvailable())
			{
				num3 += 2;
				num4 += ((!PlayerPrefs.HasKey("yell")) ? 1 : 0);
				num4 += ((!PlayerPrefs.HasKey("blow")) ? 1 : 0);
			}
			num5 += GetMiniGameMoveCount();
			GamePlayerPrefs.SetInt("_number_of_ended_moves", num4);
			GamePlayerPrefs.SetInt("_number_of_ended_mini_game_moves", num6);
			GamePlayerPrefs.SetInt("_total_number_of_ended_moves", num4);
			GamePlayerPrefs.SetInt("_number_of_moves", num3);
			RecomputeMoveCount();
			Utility.Log(ELog.Info, "Key upgrade");
		}
		InitScoringTimer();
	}

	public static void RecomputeMoveCount()
	{
		int miniGameMoveCount = GetMiniGameMoveCount();
		int num = GamePlayerPrefs.GetInt("_number_of_moves");
		GamePlayerPrefs.SetInt("_number_of_mini_game_moves", miniGameMoveCount);
		GamePlayerPrefs.SetInt("_total_number_of_moves", num + miniGameMoveCount);
	}

	public static void InitScoringTimer()
	{
		if (PlayerPrefs.HasKey("InGameTime"))
		{
			s_InGameTime = PlayerPrefs.GetInt("InGameTime");
		}
		else
		{
			GamePlayerPrefs.SetInt("InGameTime", s_InGameTime);
		}
		s_TotalMoveCount = PlayerPrefs.GetInt("_total_number_of_moves");
		ComputeScore(false);
	}

	public static void UpdateScoringTimer(float time)
	{
		if (s_UpdateTimer)
		{
			s_TimeAccumulator += time;
			if (s_TimeAccumulator > (float)s_RefreshTime)
			{
				s_InGameTime += s_RefreshTime;
				s_TimeAccumulator -= s_RefreshTime;
				ComputeScore(false);
			}
		}
	}

	public static void ComputeScore(bool submit)
	{
		if (s_UpdateTimer)
		{
			if (s_TotalEndedMoves == -1)
			{
				ResetEndedMoveCount();
			}
			s_UpdateTimer = s_TotalMoveCount != s_TotalEndedMoves;
			int num = s_TotalEndedMoves;
			int num2 = (int)s_TimeAccumulator;
			s_InGameTime += num2;
			s_TimeAccumulator -= num2;
			num *= num * num;
			s_Score = 60000 * num / s_InGameTime;
			if (submit)
			{
				GamePlayerPrefs.SetInt("InGameTime", s_InGameTime);
			}
		}
	}

	public static void ResetEndedMoveCount()
	{
		if (PlayerPrefs.HasKey("_total_number_of_ended_moves"))
		{
			s_TotalEndedMoves = PlayerPrefs.GetInt("_total_number_of_ended_moves");
			return;
		}
		s_TotalEndedMoves = 0;
		PlayerPrefs.SetInt("_total_number_of_ended_moves", s_TotalEndedMoves);
	}
}
