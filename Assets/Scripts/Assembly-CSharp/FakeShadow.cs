using UnityEngine;

public class FakeShadow : MonoBehaviour
{
	public GameObject m_Anchor;

	private void Start()
	{
	}

	private void Update()
	{
		SetTR();
	}

	public void SetTR()
	{
		if (!(m_Anchor == null))
		{
			Vector3 zero = Vector3.zero;
			Vector3 position = m_Anchor.transform.position;
			float num = ((!InGameScript.DephaseRabbid()) ? 1f : (-1f));
			switch ((DeviceOrientation)InGameScript.s_TargetOrientation)
			{
			default:
				return;
			case DeviceOrientation.LandscapeLeft:
				position.x = 0.92f * num;
				zero.z = 90f;
				break;
			case DeviceOrientation.LandscapeRight:
				position.x = -0.92f * num;
				zero.z = -90f;
				break;
			case DeviceOrientation.Portrait:
				position.y = -0.92f * num;
				break;
			case DeviceOrientation.PortraitUpsideDown:
				position.y = 0.92f * num;
				zero.z = 180f;
				break;
			}
			if (InGameScript.DephaseRabbid())
			{
				zero.z += 180f;
			}
			base.transform.position = position;
			base.transform.rotation = Quaternion.Euler(zero);
		}
	}
}
