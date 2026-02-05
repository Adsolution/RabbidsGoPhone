using UnityEngine;

public class InGameScreenOrientationScript : MonoBehaviour
{
	private int m_LastDeviceOrientation;

	private Transform m_Fonctionality;

	private Fonctionality m_FonctionalityScript;

	private bool m_ChangeOrientationWithoutSuccess;

	private static int s_GravityBlocker = 50;

	private static int s_StartOrientation;

	private static int s_LastValidOrientation;

	private static int s_TmpLastValidOrientation;

	private static int s_ReverseChangeFrameCount;

	private static bool s_LastReverseState;

	private static bool s_ReverseBeforePausing;

	private void Start()
	{
		s_StartOrientation = (int)Utility.LastDeviceOrientation;
		s_LastValidOrientation = s_StartOrientation;
		s_TmpLastValidOrientation = s_StartOrientation;
		s_LastReverseState = ReverseScreen();
	}

	private void Update()
	{
		s_GravityBlocker++;
	}

	private void LateUpdate()
	{
		bool flag = ReverseScreen();
		if (flag != s_LastReverseState)
		{
			ApplyPortraitOrPortraitUpsideDown(flag);
		}
		s_LastReverseState = flag;
		if (!m_ChangeOrientationWithoutSuccess)
		{
		}
	}

	private void ApplyPortraitOrPortraitUpsideDown(bool upsideDown)
	{
		if (!m_Fonctionality)
		{
			GameObject gameObject = GameObject.Find("Camera_2D/Fonctionality");
			if (!gameObject)
			{
				m_ChangeOrientationWithoutSuccess = true;
				return;
			}
			m_Fonctionality = gameObject.transform;
			m_FonctionalityScript = m_Fonctionality.parent.GetComponent<Fonctionality>();
		}
		if (upsideDown)
		{
			ApplyPortraitUpsideDown();
		}
		else
		{
			ApplyPortrait();
		}
		if (m_FonctionalityScript != null)
		{
			m_FonctionalityScript.RefreshMerger();
		}
		m_ChangeOrientationWithoutSuccess = false;
	}

	private void ApplyPortraitUpsideDown()
	{
		m_Fonctionality.rotation = Quaternion.Euler(0f, 0f, 180f);
	}

	private void ApplyPortrait()
	{
		m_Fonctionality.rotation = Quaternion.identity;
	}

	public static bool ReverseScreen()
	{
		if (s_GravityBlocker < 20)
		{
			return s_ReverseBeforePausing;
		}
		switch (AllInput.GetDeviceOrientation())
		{
		case DeviceOrientation.PortraitUpsideDown:
			s_TmpLastValidOrientation = 2;
			break;
		case DeviceOrientation.Portrait:
			s_TmpLastValidOrientation = 1;
			break;
		}
		if (s_TmpLastValidOrientation != s_LastValidOrientation)
		{
			if (++s_ReverseChangeFrameCount > 10)
			{
				s_LastValidOrientation = s_TmpLastValidOrientation;
				s_ReverseChangeFrameCount = 0;
			}
		}
		else
		{
			s_ReverseChangeFrameCount = 0;
		}
		return s_StartOrientation != s_LastValidOrientation;
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			s_ReverseBeforePausing = ReverseScreen();
		}
		s_GravityBlocker = 0;
	}

	private void OnApplicationQuit()
	{
		s_GravityBlocker = 0;
	}
}
