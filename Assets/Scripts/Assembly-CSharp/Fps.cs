using UnityEngine;

public class Fps : MonoBehaviour
{
	private float m_UpdateInterval = 0.5f;

	private float m_Accumulator;

	private float m_FrameCount;

	private float m_GUIAccumulator;

	private float m_GUICount;

	private float m_TimeLeft;

	private TextMesh m_TextBox;

	private bool m_Enabled;

	private void Start()
	{
		m_TextBox = GetComponent<TextMesh>();
		m_TimeLeft = m_UpdateInterval;
		m_TextBox.GetComponent<Renderer>().material.color = Color.white;
	}

	private void OnGUI()
	{
		if (m_Enabled)
		{
			m_GUIAccumulator += Time.timeScale / Time.deltaTime;
			m_GUICount += 1f;
			GUI.contentColor = new Color(1f, 1f, 1f, 1f);
			GUI.Label(Utility.NewRect(20f, 340f, 300f, 120f), "Score: " + GlobalVariables.s_Score);
		}
	}

	private void Update()
	{
		if (!m_Enabled && !Utility.IsCheater())
		{
			return;
		}
		bool flag = false;
		if (Input.GetKeyDown(KeyCode.F))
		{
			flag = true;
		}
		else if (AllInput.GetTouchCount() == 4)
		{
			for (int i = 0; i < AllInput.GetTouchCount(); i++)
			{
				if (AllInput.GetState(i) == AllInput.EState.Began)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			m_Enabled = !m_Enabled;
			m_TextBox.GetComponent<Renderer>().enabled = m_Enabled;
			m_Accumulator = 0f;
			m_FrameCount = 0f;
			m_GUIAccumulator = 0f;
			m_GUICount = 0f;
			m_TimeLeft = 0f;
		}
		if (m_Enabled)
		{
			m_TimeLeft -= Time.deltaTime;
			m_Accumulator += Time.timeScale / Time.deltaTime;
			m_FrameCount += 1f;
			if (m_TimeLeft <= 0f)
			{
				m_TextBox.text = string.Empty + (m_Accumulator / m_FrameCount).ToString("f2") + " Fps";
				TextMesh textBox = m_TextBox;
				textBox.text = textBox.text + "\n" + (m_GUIAccumulator / m_GUICount).ToString("f2") + " GUIps";
				TextMesh textBox2 = m_TextBox;
				textBox2.text = textBox2.text + "\n" + GlobalVariables.s_AveragePower;
				TextMesh textBox3 = m_TextBox;
				string text = textBox3.text;
				textBox3.text = text + "\n" + SystemInfo.systemMemorySize + " o";
				TextMesh textBox4 = m_TextBox;
				text = textBox4.text;
				textBox4.text = text + "\n" + SystemInfo.systemMemorySize / 1024 + " ko";
				TextMesh textBox5 = m_TextBox;
				text = textBox5.text;
				textBox5.text = text + "\n" + SystemInfo.systemMemorySize / 1048576 + " mo";
				m_TimeLeft = m_UpdateInterval;
				m_Accumulator = 0f;
				m_FrameCount = 0f;
				m_GUIAccumulator = 0f;
				m_GUICount = 0f;
			}
		}
	}
}
