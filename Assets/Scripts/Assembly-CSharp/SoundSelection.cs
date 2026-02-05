using UnityEngine;

public class SoundSelection : MonoBehaviour
{
	public static string s_SoundEnableKey = "_sound_enable";

	public GUISkin m_CommonSkin;

	private void Start()
	{
		DataMining.OnLevelStart();
		Utility.CreateMenuBackground();
	}

	private void Update()
	{
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
		switch (GUIUtils.AskQuestion(Localization.GetLocalizedText(ELoc.EnableSound)))
		{
		case GUIUtils.EAnswer.No:
			SetMusic(0);
			break;
		case GUIUtils.EAnswer.Yes:
			SetMusic(1);
			break;
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

	private void SetMusic(int v)
	{
		GlobalVariables.SoundEnabled = v;
		GamePlayerPrefs.SetInt(s_SoundEnableKey, v);
		Application.LoadLevel("MainMenu");
	}
}
