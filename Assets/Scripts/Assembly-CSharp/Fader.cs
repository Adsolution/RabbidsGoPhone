using UnityEngine;

public class Fader : MonoBehaviour
{
	public enum EState
	{
		Hided = 0,
		FadeIn = 1,
		Shown = 2,
		FadeOut = 3
	}

	public delegate void Handler();

	private const float m_DeltaTimeMax = 0.01f;

	private Material m_DefaultMaterial;

	private float m_CurrentTime;

	private EState m_State;

	private float m_Duration;

	public static event Handler Shown;

	public static event Handler Hided;

	public EState GetState()
	{
		return m_State;
	}

	public void UseSpecificMaterial(Material material)
	{
		base.GetComponent<Renderer>().sharedMaterial = material;
	}

	public void UnUseSpecificMaterial()
	{
		base.GetComponent<Renderer>().sharedMaterial = m_DefaultMaterial;
	}

	private void Start()
	{
		m_DefaultMaterial = base.GetComponent<Renderer>().sharedMaterial;
		SetHided();
	}

	private void Update()
	{
		if (!(m_CurrentTime >= 0f))
		{
			return;
		}
		float num = Time.deltaTime / Time.timeScale;
		m_CurrentTime -= num;
		if (m_CurrentTime < 0f)
		{
			if (m_State == EState.FadeIn)
			{
				SetShown();
				if (Fader.Shown != null)
				{
					Fader.Shown();
				}
			}
			else if (m_State == EState.FadeOut)
			{
				SetHided();
				if (Fader.Hided != null)
				{
					Fader.Hided();
				}
			}
		}
		else
		{
			Color color = base.GetComponent<Renderer>().sharedMaterial.color;
			if (m_State == EState.FadeIn)
			{
				color.a = 1f - m_CurrentTime / m_Duration;
			}
			else if (m_State == EState.FadeOut)
			{
				color.a = m_CurrentTime / m_Duration;
			}
			base.GetComponent<Renderer>().sharedMaterial.color = color;
		}
	}

	public void FadeIn(float duration)
	{
		if (base.GetComponent<Renderer>().sharedMaterial != null)
		{
			Color color = base.GetComponent<Renderer>().sharedMaterial.color;
			m_State = EState.FadeIn;
			m_Duration = duration;
			m_CurrentTime = m_Duration * (1f - color.a);
			base.gameObject.active = true;
		}
	}

	public void FadeOut(float duration)
	{
		if (base.GetComponent<Renderer>().sharedMaterial != null)
		{
			Color color = base.GetComponent<Renderer>().sharedMaterial.color;
			m_State = EState.FadeOut;
			m_Duration = duration;
			m_CurrentTime = m_Duration * color.a;
		}
		else
		{
			m_State = EState.Hided;
			base.gameObject.active = false;
		}
	}

	private void SetHided()
	{
		m_State = EState.Hided;
		Color color = base.GetComponent<Renderer>().sharedMaterial.color;
		color.a = 0f;
		base.GetComponent<Renderer>().sharedMaterial.color = color;
		base.gameObject.active = false;
	}

	private void SetShown()
	{
		m_State = EState.Shown;
		Color color = base.GetComponent<Renderer>().sharedMaterial.color;
		color.a = 1f;
		base.GetComponent<Renderer>().sharedMaterial.color = color;
	}
}
