using UnityEngine;

public class GUIRetractableScroller : GUIScroller
{
	public enum EState
	{
		Idle = 0,
		StartButton = 1,
		Hiding = 2,
		Hided = 3,
		Showing = 4,
		Count = 5
	}

	private Rect m_ButtonPos;

	private bool m_AutoRetractable = true;

	private EState m_State;

	private Rect m_InteractiveButton = default(Rect);

	private static float s_AppendageHeight = 28f;

	private static float s_WaitTimeBeforeShowing = 0.5f;

	private static float s_ShowTime = 0.1f;

	public bool IsAutoRetractable()
	{
		return m_AutoRetractable;
	}

	public void SetAutoRetractable(bool b)
	{
		m_AutoRetractable = b;
	}

	public EState GetState()
	{
		return m_State;
	}

	public override bool IsActive()
	{
		return base.IsActive() && m_State != EState.Hiding && m_State != EState.Showing && m_State != EState.Hided;
	}

	public override void Hide()
	{
		if (IsActive())
		{
			m_State = EState.Hiding;
		}
	}

	public override void ForceHide()
	{
		m_State = EState.Hided;
	}

	public override bool IsInteract(int touchID)
	{
		if (!m_Visible)
		{
			return false;
		}
		if (base.IsInteract(touchID))
		{
			return true;
		}
		return m_InteractiveButton.Contains(GetGUIPosition(touchID));
	}

	public override void Initialize(int ID, float posY, float space, Texture2D[] textures, bool selection, int selected)
	{
		base.Initialize(ID, posY, space, textures, selection, selected);
		float num = Utility.NewY(s_AppendageHeight);
		m_WindowPos.height += num;
		m_ViewPos.y += num;
		m_YSelection += s_AppendageHeight;
		m_CurrentWindowPos = m_WindowPos;
		m_CurrentWindowPos.y = Utility.NewY(Utility.RefHeight - s_AppendageHeight);
		m_InteractPos = Utility.NewRect(0f, Utility.RefHeight, Utility.RefWidth, Utility.RefHeight - posY);
		m_ButtonPos = Utility.NewRect(Utility.RefWidth * 0.5f - 35f, 0f, 70f, s_AppendageHeight);
		m_CurrentWaitTime = s_WaitTimeBeforeShowing;
		m_State = EState.Hided;
	}

	public override void Resize(Texture2D[] textures, int lastEntry)
	{
		if (textures != null && (m_Textures == null || m_Textures.Length < textures.Length))
		{
			m_CurrentWaitTime = -1f;
			m_State = EState.Showing;
		}
		base.Resize(textures, lastEntry);
	}

	public override void Reset()
	{
		base.Reset();
		m_CurrentWaitTime = s_WaitTimeBeforeShowing;
		m_State = EState.Hided;
		m_CurrentWindowPos = m_WindowPos;
		m_CurrentWindowPos.y = Utility.NewY(Utility.RefHeight - s_AppendageHeight);
		Utility.Log(ELog.GUI, "Reset() - m_State = EState.Hided");
	}

	public override void Update()
	{
		base.Update();
		switch (m_State)
		{
		case EState.Hiding:
		{
			float num3 = m_CurrentWindowPos.y - m_InteractPos.y;
			float num4 = Utility.NewY(Utility.RefHeight - s_AppendageHeight);
			m_CurrentWindowPos.y += (num4 - m_WindowPos.y) * Time.deltaTime / s_ShowTime;
			if (m_CurrentWindowPos.y > num4)
			{
				m_CurrentWindowPos.y = num4;
				m_State = EState.Hided;
			}
			m_InteractPos.y = m_CurrentWindowPos.y - num3;
			break;
		}
		case EState.Showing:
		{
			float num = m_CurrentWindowPos.y - m_InteractPos.y;
			float num2 = Utility.NewY(Utility.RefHeight - s_AppendageHeight);
			m_CurrentWindowPos.y += (m_WindowPos.y - num2) * Time.deltaTime / s_ShowTime;
			if (m_CurrentWindowPos.y < m_WindowPos.y)
			{
				m_CurrentWindowPos.y = m_WindowPos.y;
				m_State = EState.Idle;
			}
			m_InteractPos.y = m_CurrentWindowPos.y - num;
			break;
		}
		case EState.Hided:
			if (m_CurrentWaitTime >= 0f)
			{
				m_CurrentWaitTime -= Time.deltaTime;
				if (m_CurrentWaitTime < 0f)
				{
					m_State = EState.Showing;
				}
			}
			break;
		}
		m_InteractiveRect = new Rect(m_CurrentWindowPos.xMin, m_CurrentWindowPos.yMin + m_ButtonPos.height * 0.6666f, m_CurrentWindowPos.width, m_CurrentWindowPos.height - m_ButtonPos.height * 0.6666f);
		m_InteractiveButton = m_ButtonPos;
		m_InteractiveButton.yMin += m_CurrentWindowPos.yMin;
		m_InteractiveButton.yMax += m_CurrentWindowPos.yMin;
	}

	protected override void BeginDrawGroup()
	{
		if (m_State == EState.Hided || m_State == EState.Showing)
		{
			GUI.BeginGroup(m_CurrentWindowPos, string.Empty, "ScrollViewBgRetractable");
		}
		else
		{
			GUI.BeginGroup(m_CurrentWindowPos, string.Empty, "ScrollViewBgRetractableInvert");
		}
	}

	protected override void DrawScroller(bool fake)
	{
		if (m_Textures == null)
		{
			return;
		}
		if (m_State != EState.Hided)
		{
			base.DrawScroller(fake);
		}
		if (fake || m_State == EState.Hiding || m_State == EState.Showing)
		{
			return;
		}
		GUIStyle style = new GUIStyle();
		if (GUI.Button(m_ButtonPos, string.Empty, style))
		{
			if (m_State == EState.Hided)
			{
				m_State = EState.Showing;
			}
			else
			{
				m_State = EState.Hiding;
			}
		}
	}

	public override void OnScrollItemPressed(int itemIdx)
	{
		base.OnScrollItemPressed(itemIdx);
		if (m_AutoRetractable)
		{
			m_State = EState.Hiding;
		}
	}

	protected override void UpdateState()
	{
		int touchCount = AllInput.GetTouchCount();
		bool flag = false;
		bool flag2 = false;
		Rect buttonPos = m_ButtonPos;
		buttonPos.y = m_CurrentWindowPos.y;
		base.UpdateState();
		for (int i = 0; i < touchCount; i++)
		{
			if (AllInput.GetState(i) == AllInput.EState.Began)
			{
				Vector3 gUIPosition = GetGUIPosition(i);
				if (buttonPos.Contains(gUIPosition))
				{
					m_State = EState.StartButton;
				}
				if (IsInteract(i))
				{
					flag2 = true;
				}
				else
				{
					flag = true;
				}
			}
		}
		if (!flag2 && flag && m_AutoRetractable)
		{
			m_State = EState.Hiding;
		}
	}

	protected override bool IsInteractState()
	{
		return base.IsInteractState() || m_State == EState.StartButton;
	}
}
