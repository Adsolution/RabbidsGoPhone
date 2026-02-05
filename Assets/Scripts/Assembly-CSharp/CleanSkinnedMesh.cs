using UnityEngine;

public class CleanSkinnedMesh : MonoBehaviour
{
	public GameObject m_ObjetToClean;

	private void Start()
	{
		if (m_ObjetToClean != null)
		{
			MeshRenderer[] componentsInChildren = m_ObjetToClean.GetComponentsInChildren<MeshRenderer>();
			MeshFilter[] componentsInChildren2 = m_ObjetToClean.GetComponentsInChildren<MeshFilter>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].transform.parent != null && !componentsInChildren[i].transform.parent.name.Equals("B_bunny eye father"))
				{
					Object.DestroyImmediate(componentsInChildren[i]);
				}
			}
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				if (componentsInChildren2[i].transform.parent != null && !componentsInChildren2[i].transform.parent.name.Equals("B_bunny eye father"))
				{
					Object.DestroyImmediate(componentsInChildren2[i]);
				}
			}
		}
		m_ObjetToClean = null;
	}

	private void Update()
	{
	}
}
