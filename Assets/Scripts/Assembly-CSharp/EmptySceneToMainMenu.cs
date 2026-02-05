using UnityEngine;

public class EmptySceneToMainMenu : MonoBehaviour
{
	public GUISkin m_CommonSkin;

	private void Start()
	{
		DataMining.OnLevelStart();
		Utility.CreateMenuBackground();
		Utility.ShowActivityView(false);
	}

	private void OnDisable()
	{
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
		Application.LoadLevel("MainMenu");
	}

	private void OnGUI()
	{
		if ((bool)m_CommonSkin)
		{
			GUI.skin = m_CommonSkin;
		}
		else
		{
			Utility.Log(ELog.Errors, "Main Menu: Skin not found");
		}
		GUIUtils.DrawLoadingText();
	}
}
