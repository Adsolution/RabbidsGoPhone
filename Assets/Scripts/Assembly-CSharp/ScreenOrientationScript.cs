using UnityEngine;

public class ScreenOrientationScript : MonoBehaviour
{
	public bool m_NeedUpdate = true;

	private void Awake()
	{
		CheckScreenOrientation();
	}

	private void LateUpdate()
	{
		if (m_NeedUpdate)
		{
			CheckScreenOrientation();
		}
	}

	private static void CheckScreenOrientation()
	{
		if (Input.deviceOrientation == DeviceOrientation.Portrait && Screen.orientation != ScreenOrientation.Portrait)
		{
			ScreenOrientation lastScreenOrientation = (Screen.orientation = ScreenOrientation.Portrait);
			Utility.LastScreenOrientation = lastScreenOrientation;
		}
		else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown && Screen.orientation != ScreenOrientation.PortraitUpsideDown)
		{
			ScreenOrientation lastScreenOrientation = (Screen.orientation = ScreenOrientation.PortraitUpsideDown);
			Utility.LastScreenOrientation = lastScreenOrientation;
		}
		else if (Utility.LastScreenOrientation != ScreenOrientation.Unknown)
		{
			Screen.orientation = Utility.LastScreenOrientation;
		}
	}
}
