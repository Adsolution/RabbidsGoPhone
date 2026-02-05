using System.Collections.Generic;
using UnityEngine;

public class GUIUtils : MonoBehaviour
{
	public enum EAnswer
	{
		Yes = 0,
		No = 1,
		Count = 2
	}

	private static List<GUIScroller> s_Scrollers = new List<GUIScroller>();

	public static void DrawLoadingText()
	{
		float num = 0.5f;
		GUI.Label(Utility.NewRect(0f, Utility.RefHeight * (0.95f - num), Utility.RefWidth, Utility.RefHeight * num), Localization.GetWaitText() + "\n", "BottomTextTitle");
	}

	public static void DrawText(string txt)
	{
		GUI.Label(Utility.NewRect(0f, Utility.RefHeight * 0.8f, Utility.RefWidth, Utility.RefHeight / 5f + 40f), txt, "LoadingTitle");
	}

	public static bool DrawBackButton()
	{
		return GUI.Button(Utility.NewRect(10f, 5f, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "BackButton") || Input.GetKeyDown(KeyCode.Escape);
	}

	public static void DrawFictiveBackButton()
	{
		GUI.Label(Utility.NewRect(10f, 5f, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "BackButton");
	}

	public static void DrawFictiveButton(string buttonName)
	{
		GUI.Label(Utility.NewRect(Utility.RefWidth - 74f, 95f, 64f, 64f), string.Empty, buttonName);
	}

	public static void DrawFictiveButton(Texture2D texture)
	{
		GUI.Label(Utility.NewRect(Utility.RefWidth - 74f, 95f, 64f, 64f), texture, new GUIStyle());
	}

	public static bool DrawPrevButton()
	{
		return DrawPrevButton(0f);
	}

	public static bool DrawNextButton()
	{
		return DrawNextButton(0f);
	}

	public static bool DrawPrevButton(float verticalOffset)
	{
		return GUI.Button(Utility.NewRect(0f, Utility.RefHeight - 69f - verticalOffset, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "PrevButton");
	}

	public static bool DrawNextButton(float verticalOffset)
	{
		return GUI.Button(Utility.NewRect(Utility.RefWidth - 64f, Utility.RefHeight - 69f - verticalOffset, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "NextButton");
	}

	public static void DrawFictivePrevButton(float verticalOffset)
	{
		GUI.Label(Utility.NewRect(0f, Utility.RefHeight - 69f - verticalOffset, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "PrevButton");
	}

	public static void DrawFictiveNextButton(float verticalOffset)
	{
		GUI.Label(Utility.NewRect(Utility.RefWidth - 64f, Utility.RefHeight - 69f - verticalOffset, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "NextButton");
	}

	public static bool DrawError(string txt)
	{
		float num = 10f;
		GUIStyle style = GUI.skin.GetStyle("TextErrorMessage");
		GUI.Label(Utility.NewRect(0f, 150f, Utility.RefWidth, 170f), string.Empty, "BlackBackground");
		GUI.Label(Utility.NewRect(num, 200f, Utility.RefWidth - num, 0f), txt, style);
		return GUI.Button(Utility.NewRect((Utility.RefWidth - (float)GlobalVariables.BACK_BUTTON_WIDTH) * 0.5f, 250f, GlobalVariables.BACK_BUTTON_WIDTH, GlobalVariables.BACK_BUTTON_HEIGHT), string.Empty, "ValidButton");
	}

	public static EAnswer AskQuestion(string question)
	{
		float num = 10f;
		GUIStyle style = GUI.skin.GetStyle("TextErrorMessage");
		GUI.Label(Utility.NewRect(0f, Utility.RefHeight / 6f, Utility.RefWidth, Utility.RefHeight / 4f), string.Empty, "BlackBackground");
		GUI.Label(Utility.NewRect(num, Utility.RefHeight / 6f + Utility.RefHeight / 8f, Utility.RefWidth - num, 0f), question, style);
		EAnswer result = EAnswer.Count;
		if (GUI.Button(Utility.NewRect((Utility.RefWidth - (float)GlobalVariables.BUTTON_WIDTH) / 2f, Utility.RefHeight * 0.4f + (float)GlobalVariables.BUTTON_HEIGHT * 0.4f, GlobalVariables.BUTTON_WIDTH, GlobalVariables.BUTTON_HEIGHT), Localization.GetLocalizedText(ELoc.Yes)))
		{
			result = EAnswer.Yes;
		}
		if (GUI.Button(Utility.NewRect((Utility.RefWidth - (float)GlobalVariables.BUTTON_WIDTH) / 2f, Utility.RefHeight * 0.4f + (float)GlobalVariables.BUTTON_HEIGHT * 1.25f, GlobalVariables.BUTTON_WIDTH, GlobalVariables.BUTTON_HEIGHT), Localization.GetLocalizedText(ELoc.No)))
		{
			result = EAnswer.No;
		}
		return result;
	}

	public static GUIScroller AddScroller(int ID, float space, Texture2D[] textures, bool selection, int selected)
	{
		return AddScroller(ID, Utility.RefHeight - 125f, space, textures, selection, selected, true);
	}

	public static GUIScroller AddScroller(int ID, float space, Texture2D[] textures, bool selection, int selected, bool retractable)
	{
		return AddScroller(ID, Utility.RefHeight - 125f, space, textures, selection, selected, retractable);
	}

	public static GUIScroller AddScroller(int ID, float posY, float space, Texture2D[] textures, bool selection, int selected, bool retractable)
	{
		AllInput.ComputeRatios();
		GUIScroller scroller = GetScroller(ID);
		if (scroller != null)
		{
			s_Scrollers.Remove(scroller);
		}
		scroller = ((!retractable) ? new GUIScroller() : new GUIRetractableScroller());
		scroller.Initialize(ID, posY, space, textures, selection, selected);
		s_Scrollers.Add(scroller);
		return scroller;
	}

	public static void RemoveScroller(int ID)
	{
		int i;
		for (i = 0; i < s_Scrollers.Count && s_Scrollers[i].GetID() != ID; i++)
		{
		}
		if (i < s_Scrollers.Count)
		{
			s_Scrollers.RemoveAt(i);
		}
	}

	public static GUIScroller GetScroller(int ID)
	{
		int i;
		for (i = 0; i < s_Scrollers.Count && s_Scrollers[i].GetID() != ID; i++)
		{
		}
		if (i < s_Scrollers.Count)
		{
			return s_Scrollers[i];
		}
		return null;
	}

	public static void SetScrollerVisibility(int ID, bool b)
	{
		int i;
		for (i = 0; i < s_Scrollers.Count && s_Scrollers[i].GetID() != ID; i++)
		{
		}
		if (i < s_Scrollers.Count)
		{
			s_Scrollers[i].SetVisible(b);
		}
	}

	public static void SetTextContent(GUIStyle textContent)
	{
		for (int i = 0; i < s_Scrollers.Count; i++)
		{
			if (i < s_Scrollers.Count)
			{
				s_Scrollers[i].SetTextContent(textContent);
			}
		}
		if (textContent == null)
		{
			Utility.Log(ELog.Errors, "SetTextContent is null");
		}
	}
}
