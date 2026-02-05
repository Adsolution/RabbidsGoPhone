using UnityEngine;

public class FogDisabler : MonoBehaviour
{
	private bool m_RevertFogState;

	private void OnPreRender()
	{
		m_RevertFogState = RenderSettings.fog;
		RenderSettings.fog = false;
	}

	private void OnPostRender()
	{
		RenderSettings.fog = m_RevertFogState;
	}
}
