using UnityEngine;

public class PlaneSwitcher : MonoBehaviour
{
	public enum States
	{
		WaitFinger = 0,
		TrackFinger = 1,
		MovePics = 2
	}

	public GameObject m_Plane;

	public Texture2D[] m_Textures;

	private GameObject[] m_Planes;

	private int m_Count;

	private float m_CurrentX;

	private float m_GoalX;

	private int m_Goal;

	private float m_BaseFingerPos;

	private float m_Offset;

	private States m_State;

	private void Start()
	{
		m_Count = m_Textures.Length;
		m_Offset = 10f * m_Plane.transform.localScale.z;
		m_Planes = new GameObject[m_Count];
		for (int i = 0; i < m_Count; i++)
		{
			Vector3 position = m_Plane.transform.position;
			position.x = (float)i * m_Offset;
			m_Planes[i] = (GameObject)Object.Instantiate(m_Plane, position, m_Plane.transform.rotation);
			m_Planes[i].name = "Picture_" + i;
			m_Planes[i].GetComponent<Renderer>().material.SetTexture("_MainTex", m_Textures[i]);
		}
		m_Plane.active = false;
	}

	private void Update()
	{
		switch (m_State)
		{
		case States.WaitFinger:
			if (AllInput.GetTouchCount() == 1)
			{
				m_BaseFingerPos = AllInput.GetXPosition(0);
				m_State = States.TrackFinger;
			}
			break;
		case States.TrackFinger:
			if (AllInput.GetTouchCount() == 1)
			{
				float xPosition = AllInput.GetXPosition(0);
				float num = m_BaseFingerPos - xPosition;
				if (num > 100f && m_Goal < m_Count - 1)
				{
					m_Goal++;
					m_GoalX = (float)m_Goal * (0f - m_Offset);
					m_State = States.MovePics;
				}
				if (num < -100f && m_Goal > 0)
				{
					m_Goal--;
					m_GoalX = (float)m_Goal * (0f - m_Offset);
					m_State = States.MovePics;
				}
			}
			else
			{
				m_State = States.WaitFinger;
			}
			break;
		case States.MovePics:
		{
			for (int i = 0; i < m_Count; i++)
			{
				Vector3 position = m_Plane.transform.position;
				position.x = m_Offset * (float)i + m_CurrentX;
				m_Planes[i].transform.position = position;
			}
			if (m_CurrentX == m_GoalX)
			{
				m_State = States.WaitFinger;
				break;
			}
			m_CurrentX = Mathf.Lerp(m_CurrentX, m_GoalX, Time.deltaTime * 5f);
			if (Mathf.Abs(m_CurrentX - m_GoalX) < 1f)
			{
				m_CurrentX = m_GoalX;
			}
			break;
		}
		}
	}

	public void Show(bool b)
	{
		for (int i = 0; i < m_Count; i++)
		{
			m_Planes[i].active = b;
		}
	}
}
