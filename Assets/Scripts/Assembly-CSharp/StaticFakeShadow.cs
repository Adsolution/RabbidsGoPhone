using UnityEngine;

public class StaticFakeShadow : MonoBehaviour
{
	public GameObject m_Anchor;

	private void Start()
	{
		base.GetComponent<Renderer>().material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "Misc/FakeShadow");
	}

	private void Update()
	{
		SetTR();
	}

	private void SetTR()
	{
		if (!(m_Anchor == null))
		{
			float y = base.transform.localPosition.y;
			base.transform.position = m_Anchor.transform.position;
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y = y;
			base.transform.localPosition = localPosition;
		}
	}
}
