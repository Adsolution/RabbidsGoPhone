using System.Collections.Generic;
using UnityEngine;

public class MeshMerger : MonoBehaviour
{
	public int m_MergedMeshesCount;

	private MeshFilter[] m_MeshFiltersArray;

	private List<MeshFilter> m_MeshFiltersList = new List<MeshFilter>();

	private GameObject m_OriginalMesh;

	private MeshRenderer m_MeshRenderer;

	private Material m_MeshMaterial;

	private MeshFilter m_MeshFilter;

	private Mesh m_MergedMesh;

	private void Start()
	{
		m_MergedMesh = new Mesh();
		m_MeshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		m_MeshRenderer.castShadows = false;
		m_MeshRenderer.receiveShadows = false;
		m_MeshFilter = base.gameObject.AddComponent<MeshFilter>();
		m_MeshFilter.sharedMesh = m_MergedMesh;
	}

	private void Update()
	{
	}

	public void SetMaterial(Material mat)
	{
	}

	public void SetSourceMeshesRoot(GameObject src)
	{
		m_OriginalMesh = src;
		m_MeshMaterial = src.GetComponent<Renderer>().sharedMaterial;
		base.transform.position = Vector3.zero;
	}

	public void MergeMeshes()
	{
		if (m_OriginalMesh == null || m_MeshRenderer == null || m_MergedMesh == null)
		{
			return;
		}
		if (m_MeshFiltersArray == null)
		{
			m_MeshFiltersList.Clear();
			FindMeshFilters(m_OriginalMesh.transform);
			m_MeshFiltersArray = m_MeshFiltersList.ToArray();
		}
		m_MergedMeshesCount = 0;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int num5 = m_MeshFiltersArray.Length - 1; num5 >= 0; num5--)
		{
			MeshFilter meshFilter = m_MeshFiltersArray[num5];
			if (meshFilter.gameObject.active)
			{
				if (m_MeshMaterial == null)
				{
					m_MeshMaterial = meshFilter.gameObject.GetComponent<Renderer>().sharedMaterial;
				}
				if (!(m_MeshMaterial != meshFilter.gameObject.GetComponent<Renderer>().sharedMaterial))
				{
					num += meshFilter.mesh.vertices.Length;
					num2 += meshFilter.mesh.normals.Length;
					num3 += meshFilter.mesh.triangles.Length;
					num4 += meshFilter.mesh.uv.Length;
					m_MergedMeshesCount++;
				}
			}
		}
		Vector3[] array = new Vector3[num];
		Vector3[] array2 = new Vector3[num2];
		Matrix4x4[] array3 = new Matrix4x4[m_MeshFiltersArray.Length];
		BoneWeight[] array4 = new BoneWeight[num];
		int[] array5 = new int[num3];
		Vector2[] array6 = new Vector2[num4];
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		int num10 = 0;
		for (int num5 = m_MeshFiltersArray.Length - 1; num5 >= 0; num5--)
		{
			MeshFilter meshFilter2 = m_MeshFiltersArray[num5];
			if (meshFilter2.gameObject.active && !(m_MeshMaterial != meshFilter2.gameObject.GetComponent<Renderer>().sharedMaterial))
			{
				int[] triangles = meshFilter2.mesh.triangles;
				foreach (int num11 in triangles)
				{
					array5[num8++] = num11 + num6;
				}
				array3[num10] = Matrix4x4.identity;
				Vector3[] vertices = meshFilter2.mesh.vertices;
				foreach (Vector3 position in vertices)
				{
					array4[num6].weight0 = 1f;
					array4[num6].boneIndex0 = num10;
					array[num6++] = meshFilter2.transform.TransformPoint(position);
				}
				Vector3[] normals = meshFilter2.mesh.normals;
				foreach (Vector3 vector in normals)
				{
					array2[num7++] = vector;
				}
				Vector2[] uv = meshFilter2.mesh.uv;
				foreach (Vector2 vector2 in uv)
				{
					array6[num9++] = vector2;
				}
				num10++;
				MeshRenderer component = meshFilter2.gameObject.GetComponent<MeshRenderer>();
				if ((bool)component)
				{
					component.enabled = false;
				}
			}
		}
		m_MergedMesh.Clear();
		m_MergedMesh.name = base.gameObject.name;
		m_MergedMesh.vertices = array;
		m_MergedMesh.normals = array2;
		m_MergedMesh.boneWeights = array4;
		m_MergedMesh.uv = array6;
		m_MergedMesh.triangles = array5;
		m_MergedMesh.bindposes = array3;
		base.GetComponent<Renderer>().sharedMaterial = m_MeshMaterial;
	}

	private void FindMeshFilters(Transform t)
	{
		MeshFilter component = t.GetComponent<MeshFilter>();
		if (component != null)
		{
			m_MeshFiltersList.Add(component);
		}
		for (int i = 0; i < t.childCount; i++)
		{
			FindMeshFilters(t.GetChild(i));
		}
	}
}
