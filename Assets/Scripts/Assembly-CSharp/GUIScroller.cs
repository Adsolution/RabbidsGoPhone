using UnityEngine;

public class GUIScroller
{
	public enum ESlide
	{
		Idle = 0,
		StartTouch = 1,
		TouchSlide = 2,
		AutoSlide = 3,
		ScrollSlide = 4,
		StartSelect = 5,
		SelectSlide = 6,
		Count = 7
	}

	public struct SPack
	{
		public Texture2D Texture;

		public EPack Pack;

		public int StartIndex;

		public int EndIndex;
	}

	public delegate void ScrollViewHandler(int scrollViewID, int itemIdx);

	private int m_ID = -1;

	protected bool m_Visible = true;

	protected Rect m_WindowPos;

	protected Rect m_CurrentWindowPos;

	protected Rect m_ViewPos;

	protected Rect m_InteractPos;

	private Rect m_View;

	private Vector2 m_ScrollPos;

	protected Rect m_InteractiveRect = default(Rect);

	private float m_Delta;

	private float m_Space;

	private int m_Selected;

	private bool m_Selection;

	protected ESlide m_SlideState = ESlide.Count;

	protected float m_CurrentWaitTime;

	protected Texture2D[] m_Textures;

	protected float m_YSelection;

	private int m_FingerID = -1;

	private float m_PosGoTo;

	private SPack[] m_Packs;

	private GUIStyle m_TextContent;

	protected static float s_PackBorderX = 6f;

	protected static float s_PackBorderY = 8f;

	private static float s_ScrollHeight = 20f;

	private static float s_ItemXOffset = 200f;

	protected static float s_ItemYOffset = 18f;

	private static float s_TranslateTime = 0.1f;

	private static float s_DeltaThreshold = 0.1f;

	private static float s_DeltaFactor = 0.9f;

	private static float s_ThumbHeight = 60f;

	private static float s_SelectWidth = 49f;

	private static float s_SelectHeight = 66f;

	public static event ScrollViewHandler ScrollItemPressed;

	public int GetID()
	{
		return m_ID;
	}

	public virtual bool IsInteract(int touchID)
	{
		if (!m_Visible)
		{
			return false;
		}
		Vector3 gUIPosition = GetGUIPosition(touchID);
		return m_InteractiveRect.Contains(gUIPosition);
	}

	public virtual bool IsActive()
	{
		return true;
	}

	public void SetScrollPos(int selected, bool smooth)
	{
		float num = Utility.NewX(s_ItemXOffset);
		if (m_View.width < 2f * num + m_ViewPos.width)
		{
			m_PosGoTo = (m_View.width - m_ViewPos.width) * 0.5f;
		}
		else
		{
			float num2 = m_ViewPos.width * 0.5f;
			float num3 = num + num2;
			float num4 = m_View.width - num3;
			float num5 = Utility.NewX(s_ItemXOffset + m_Space * 0.5f);
			for (int i = 0; i < m_Textures.Length && i != selected; i++)
			{
				num5 += Utility.NewX(ComputeItemWidth(i) + m_Space);
			}
			num5 += Utility.NewX(ComputeItemWidth(selected) * 0.5f);
			if (num5 < num3)
			{
				m_PosGoTo = num;
			}
			else if (num5 > num4)
			{
				m_PosGoTo = m_View.width - num - m_ViewPos.width;
			}
			else
			{
				m_PosGoTo = num5 - num2;
			}
		}
		m_Selected = selected;
		if (smooth)
		{
			m_SlideState = ESlide.StartSelect;
		}
		else
		{
			m_ScrollPos.x = m_PosGoTo;
		}
	}

	public virtual void Hide()
	{
	}

	public virtual void ForceHide()
	{
	}

	public void SetVisible(bool b)
	{
		m_Visible = b;
	}

	public bool IsVisible()
	{
		return m_Visible;
	}

	public void SetTextContent(GUIStyle textContent)
	{
		m_TextContent = textContent;
	}

	public virtual void Initialize(int ID, float posY, float space, Texture2D[] textures, bool selection, int selected)
	{
		float num = 2f * s_ItemYOffset;
		m_ID = ID;
		m_Space = space;
		m_Textures = textures;
		m_Selection = selection;
		float width = ComputeViewWidth();
		m_WindowPos = Utility.NewRect(0f, posY, Utility.RefWidth, s_ThumbHeight + num + s_ScrollHeight);
		m_ViewPos = Utility.NewRect(0f, s_ItemYOffset, Utility.RefWidth, s_ThumbHeight + s_ScrollHeight);
		m_View = Utility.NewRect(0f, s_ItemYOffset, width, s_ThumbHeight);
		m_InteractPos = Utility.NewRect(0f, posY + s_ItemYOffset, Utility.RefWidth, s_ThumbHeight + s_ScrollHeight);
		m_ScrollPos = Vector2.zero;
		m_Delta = 0f;
		m_CurrentWindowPos = m_WindowPos;
		m_CurrentWaitTime = -1f;
		m_SlideState = ESlide.Idle;
		m_YSelection = s_ItemYOffset - (s_SelectHeight - s_ThumbHeight) * 0.5f;
		m_InteractiveRect = m_CurrentWindowPos;
		SetScrollPos(selected, false);
	}

	public void Draw()
	{
		Draw(false);
	}

	public void Draw(bool fake)
	{
		if (m_Visible)
		{
			BeginDrawGroup();
			DrawScroller(fake);
			GUI.EndGroup();
		}
	}

	public virtual void Update()
	{
		if (Utility.InputStoppedByActivity())
		{
			return;
		}
		if (IsActive())
		{
			UpdateState();
		}
		switch (m_SlideState)
		{
		case ESlide.TouchSlide:
			if (m_Delta < 0f - s_DeltaThreshold || m_Delta > s_DeltaThreshold)
			{
				m_ScrollPos.x -= m_Delta;
				m_ScrollPos.x = Mathf.Clamp(m_ScrollPos.x, m_View.xMin, m_View.xMax - (float)Screen.width);
				m_Delta *= s_DeltaFactor;
			}
			break;
		case ESlide.AutoSlide:
		{
			m_ScrollPos.x -= m_Delta;
			m_ScrollPos.x = Mathf.Clamp(m_ScrollPos.x, m_View.xMin, m_View.xMax - (float)Screen.width);
			float num = Utility.NewX(s_ItemXOffset);
			float num2 = m_ViewPos.width * 0.5f;
			if (m_View.width < 2f * num + m_ViewPos.width)
			{
				ScrollPosGoTo(m_View.width * 0.5f - num2);
				break;
			}
			float num3 = num;
			float num4 = m_View.width - num3 - m_ViewPos.width;
			if (m_ScrollPos.x < num3)
			{
				ScrollPosGoTo(num3);
				break;
			}
			if (m_ScrollPos.x > num4)
			{
				ScrollPosGoTo(num4);
				break;
			}
			if (m_Delta < 0f - s_DeltaThreshold || m_Delta > s_DeltaThreshold)
			{
				m_Delta *= s_DeltaFactor;
				break;
			}
			m_Delta = 0f;
			m_SlideState = ESlide.Idle;
			break;
		}
		case ESlide.SelectSlide:
			ScrollPosGoTo(m_PosGoTo);
			m_ScrollPos.x -= m_Delta;
			m_ScrollPos.x = Mathf.Clamp(m_ScrollPos.x, m_View.xMin, m_View.xMax - (float)Screen.width);
			if (m_Delta > 0f - s_DeltaThreshold && m_Delta < s_DeltaThreshold)
			{
				m_Delta = 0f;
				m_SlideState = ESlide.Idle;
			}
			break;
		case ESlide.ScrollSlide:
		case ESlide.StartSelect:
			break;
		}
	}

	public virtual void Resize(Texture2D[] textures, int lastEntry)
	{
		m_Textures = textures;
		float width = ComputeViewWidth();
		m_View = Utility.NewRect(0f, s_ItemYOffset, width, s_ThumbHeight);
		if (lastEntry != -1)
		{
			SetScrollPos(lastEntry, IsActive());
		}
	}

	public void SetPacks(SPack[] packs)
	{
		m_Packs = packs;
	}

	public virtual void Reset()
	{
	}

	public virtual void Clear()
	{
		m_Textures = null;
	}

	public virtual void OnScrollItemPressed(int itemIdx)
	{
		SetScrollPos(itemIdx, true);
		if (GUIScroller.ScrollItemPressed != null)
		{
			GUIScroller.ScrollItemPressed(m_ID, itemIdx);
		}
	}

	protected virtual void BeginDrawGroup()
	{
		GUI.BeginGroup(m_CurrentWindowPos, string.Empty, "ScrollViewBg");
	}

	protected virtual void DrawScroller(bool fake)
	{
		if (m_Textures == null)
		{
			return;
		}
		GUIStyle gUIStyle = new GUIStyle();
		float num = s_ItemXOffset + m_Space * 0.5f;
		float num2 = -1f;
		float num3 = 0f;
		float num4 = 0f;
		bool flag = false;
		int num5 = m_Textures.Length;
		int num6 = 0;
		float num7 = s_ThumbHeight * 0.25f;
		int num8 = 0;
		bool flag2 = false;
		float num9 = 0f;
		float num10 = 0f;
		Vector2 vector = GUI.BeginScrollView(m_ViewPos, m_ScrollPos, m_View);
		if (vector != m_ScrollPos && AllInput.GetTouchCount() > 0)
		{
			m_SlideState = ESlide.ScrollSlide;
		}
		m_ScrollPos = vector;
		for (int i = 0; i < num5; i++)
		{
			if (m_Packs != null && num6 < 6)
			{
				num8 = 0;
				while (num6 < 6 && m_Packs[num6].EndIndex < i)
				{
					num6++;
					flag2 = false;
				}
				if (num6 < 6 && m_Packs[num6].Texture != null)
				{
					if (m_Packs[num6].StartIndex == i)
					{
						num8 = 1;
						num += num7;
					}
					else if (m_Packs[num6].EndIndex == i)
					{
						num8 = 2;
					}
				}
			}
			num3 = ComputeItemWidth(i);
			float num11 = Mathf.Max(num3, s_SelectWidth);
			float num12 = Utility.NewX(num) - m_ScrollPos.x;
			float num13 = Utility.NewX(num + num11) - m_ScrollPos.x;
			switch (num8)
			{
			case 1:
				num9 = num;
				break;
			case 2:
				num10 = num + num11;
				break;
			}
			if (num12 < m_ViewPos.width && num13 > m_ViewPos.x)
			{
				if (m_Selection && i == m_Selected)
				{
					num2 = num;
					num4 = num3;
					flag = true;
				}
				gUIStyle.normal.background = m_Textures[i];
				if (!fake)
				{
					if (GUI.Button(Utility.NewRect(num, s_ItemYOffset, num3, s_ThumbHeight), string.Empty, gUIStyle) && IsActive() && m_SlideState != ESlide.TouchSlide)
					{
						OnScrollItemPressed(i);
						num5 = m_Textures.Length;
					}
				}
				else
				{
					GUI.Label(Utility.NewRect(num, s_ItemYOffset, num3, s_ThumbHeight), string.Empty, gUIStyle);
				}
				if (!flag2)
				{
					flag2 = true;
				}
			}
			if (flag2 && num8 == 2)
			{
				float left = num9 - s_PackBorderX;
				float top = s_ItemYOffset - s_PackBorderY;
				float num14 = num10 - num9 + s_PackBorderX * 2f;
				float num15 = s_ThumbHeight + s_PackBorderY * 2f;
				int eltCount = m_Packs[num6].EndIndex - m_Packs[num6].StartIndex;
				gUIStyle.normal.background = m_Packs[num6].Texture;
				GUI.Label(Utility.NewRect(left, top, num14, num15), string.Empty, gUIStyle);
				DrawTitle(left, top, num14, num15, (EPack)num6, eltCount);
			}
			num += num3 + m_Space;
		}
		GUI.EndScrollView();
		if (flag)
		{
			gUIStyle.normal.background = null;
			num2 -= (s_SelectWidth - num4) * 0.5f;
			Rect position = Utility.NewRect(num2, m_YSelection, s_SelectWidth, s_SelectHeight);
			position.x -= m_ScrollPos.x;
			GUI.Label(position, string.Empty, "ScrollViewSelect");
		}
	}

	protected virtual void UpdateState()
	{
		int touchCount = AllInput.GetTouchCount();
		if (m_SlideState == ESlide.StartTouch)
		{
			int touchByFinger = AllInput.GetTouchByFinger(m_FingerID);
			if (touchByFinger >= 0)
			{
				if (AllInput.GetState(touchByFinger) == AllInput.EState.Moved)
				{
					m_SlideState = ESlide.TouchSlide;
				}
				else if (AllInput.GetState(touchByFinger) == AllInput.EState.Leave)
				{
					m_SlideState = ESlide.AutoSlide;
				}
			}
		}
		else if (m_SlideState == ESlide.TouchSlide)
		{
			int touchByFinger2 = AllInput.GetTouchByFinger(m_FingerID);
			if (touchByFinger2 >= 0)
			{
				m_Delta = AllInput.GetGUIDelta(touchByFinger2).x;
				if (InGameScript.ReverseScreen() && Application.loadedLevelName == "InGame")
				{
					m_Delta *= -1f;
				}
				if (AllInput.GetState(touchByFinger2) == AllInput.EState.Leave)
				{
					m_SlideState = ESlide.AutoSlide;
				}
			}
			else
			{
				m_Delta = 0f;
			}
		}
		else if (m_SlideState == ESlide.ScrollSlide)
		{
			for (int i = 0; i < touchCount; i++)
			{
				if (AllInput.GetState(i) == AllInput.EState.Leave)
				{
					m_SlideState = ESlide.AutoSlide;
					break;
				}
			}
		}
		else if (m_SlideState == ESlide.StartSelect)
		{
			m_SlideState = ESlide.SelectSlide;
		}
		else if (touchCount == 1 && AllInput.GetState(0) == AllInput.EState.Began)
		{
			Vector3 gUIPosition = GetGUIPosition(0);
			if (m_InteractPos.Contains(gUIPosition))
			{
				m_FingerID = AllInput.GetFingerID(0);
				m_SlideState = ESlide.StartTouch;
			}
		}
	}

	protected virtual bool IsInteractState()
	{
		return m_SlideState == ESlide.StartTouch || m_SlideState == ESlide.TouchSlide || m_SlideState == ESlide.ScrollSlide || m_SlideState == ESlide.StartSelect;
	}

	protected void DrawTitle(float left, float top, float hSize, float vSize, EPack pack, int eltCount)
	{
		eltCount = AchievementTable.ApproxEltCount(eltCount);
		if (eltCount == 5)
		{
			DrawTitle_5(left, top, hSize, vSize, pack);
		}
		else
		{
			DrawTitle_20(left, top, hSize, vSize, pack);
		}
	}

	protected void DrawTitle_5(float left, float top, float hSize, float vSize, EPack pack)
	{
		string localizedText = Localization.GetLocalizedText(AchievementTable.GetTitleFromPack(pack));
		TextAnchor alignment = m_TextContent.alignment;
		float fixedWidth = m_TextContent.fixedWidth;
		float num = 45f;
		float num2 = 72f;
		float num3 = 50f;
		float num4 = 20f;
		float left2 = hSize * num * 0.01f + left;
		float top2 = vSize * num2 * 0.01f + top;
		float width = hSize * num3 * 0.01f;
		float height = vSize * num4 * 0.01f;
		m_TextContent.alignment = TextAnchor.MiddleCenter;
		m_TextContent.fixedWidth = 0f;
		GUI.Label(Utility.NewRect(left2, top2, width, height), localizedText, m_TextContent);
		m_TextContent.alignment = alignment;
		m_TextContent.fixedWidth = fixedWidth;
	}

	protected void DrawTitle_20(float left, float top, float hSize, float vSize, EPack pack)
	{
		string localizedText = Localization.GetLocalizedText(AchievementTable.GetTitleFromPack(pack));
		TextAnchor alignment = m_TextContent.alignment;
		float fixedWidth = m_TextContent.fixedWidth;
		float num = 15f;
		float num2 = 72f;
		float num3 = 15f;
		float num4 = 20f;
		float top2 = vSize * num2 * 0.01f + top;
		float width = hSize * num3 * 0.01f;
		float height = vSize * num4 * 0.01f;
		m_TextContent.alignment = TextAnchor.MiddleCenter;
		m_TextContent.fixedWidth = 0f;
		while (num < 100f)
		{
			float left2 = hSize * num * 0.01f + left;
			num += 34f;
			GUI.Label(Utility.NewRect(left2, top2, width, height), localizedText, m_TextContent);
		}
		m_TextContent.alignment = alignment;
		m_TextContent.fixedWidth = fixedWidth;
	}

	private int FindNextPackIndex(int packIndex)
	{
		packIndex++;
		if (m_Packs != null && packIndex < m_Packs.Length)
		{
			while (m_Packs[packIndex].StartIndex == -1 && packIndex < m_Packs.Length)
			{
				packIndex++;
			}
		}
		return packIndex;
	}

	private void ScrollPosGoTo(float scrollPos)
	{
		float num = m_ScrollPos.x - scrollPos;
		m_Delta = s_DeltaFactor * Time.deltaTime * num / s_TranslateTime;
		if ((m_ScrollPos.x < scrollPos && m_ScrollPos.x - m_Delta > scrollPos) || (m_ScrollPos.x > scrollPos && m_ScrollPos.x - m_Delta < scrollPos))
		{
			m_ScrollPos.x = scrollPos;
			m_Delta = 0f;
			m_SlideState = ESlide.Idle;
		}
	}

	private float ComputeViewWidth()
	{
		float num = 0f;
		if (m_Textures != null)
		{
			float num2 = m_Textures.Length;
			if (num2 > 0f)
			{
				num += 2f * s_ItemXOffset;
				for (int i = 0; i < m_Textures.Length; i++)
				{
					num += m_Space + ComputeItemWidth(i);
				}
			}
		}
		return num * 1.015f;
	}

	private float ComputeItemWidth(int idx)
	{
		if (idx < 0)
		{
			Utility.Log(ELog.Errors, "ComputeItemWidth: idx == " + idx);
			return s_ThumbHeight;
		}
		if (m_Textures == null)
		{
			Utility.Log(ELog.Errors, "ComputeItemWidth: m_Textures == null");
			return s_ThumbHeight;
		}
		if (idx >= m_Textures.Length)
		{
			Utility.Log(ELog.Errors, "ComputeItemWidth: idx == " + idx + " / " + m_Textures.Length);
			return s_ThumbHeight;
		}
		return s_ThumbHeight * (float)m_Textures[idx].width / (float)m_Textures[idx].height;
	}

	private bool IsTouchOn(int touchID)
	{
		Vector3 gUIPosition = GetGUIPosition(touchID);
		return m_InteractPos.Contains(gUIPosition);
	}

	protected Vector3 GetGUIPosition(int touchID)
	{
		Vector3 gUIPosition = AllInput.GetGUIPosition(touchID);
		if (InGameScript.ReverseScreen() && Application.loadedLevelName == "InGame")
		{
			gUIPosition.x = (float)Screen.width - gUIPosition.x;
			gUIPosition.y = (float)Screen.height - gUIPosition.y;
		}
		return gUIPosition;
	}
}
