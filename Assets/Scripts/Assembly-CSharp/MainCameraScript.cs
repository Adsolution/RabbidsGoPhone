using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
	private GameObject m_TargetAnimation;

	private void Start()
	{
		m_TargetAnimation = GameObject.Find("InGameRabbid");
		if (m_TargetAnimation == null)
		{
			Utility.Log(ELog.Errors, "Unable to find 'InGameRabbid'");
		}
	}

	private void Update()
	{
		if (!(m_TargetAnimation == null))
		{
			ManageGameMove();
		}
	}

	private void ManageGameMove()
	{
		if (m_TargetAnimation.GetComponent<Animation>().IsPlaying("standby_cri_de_pres"))
		{
			if (base.transform.position.z > 2.2f)
			{
				Vector3 position = base.transform.position;
				position.z -= 0.1f;
				base.transform.position = position;
			}
			return;
		}
		if (m_TargetAnimation.GetComponent<Animation>().IsPlaying("burp") && m_TargetAnimation.GetComponent<Animation>()["burp"].time > 1.8f && m_TargetAnimation.GetComponent<Animation>()["burp"].time < 2.8f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.forward, new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 30f));
			base.transform.rotation *= quaternion;
			return;
		}
		if (base.transform.position.z < 2.4f)
		{
			Vector3 position2 = base.transform.position;
			position2.z += 0.1f;
			base.transform.position = position2;
		}
		base.transform.eulerAngles = new Vector3(0f, 180f, 0f);
	}
}
