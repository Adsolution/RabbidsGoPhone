using System;
using System.Reflection;
using UnityEngine;

public class SpriteData
{
	public Vector2 m_UVPos;

	public Vector2 m_UVSize;

	public Vector2 UVPos
	{
		get
		{
			return m_UVPos;
		}
	}

	public Vector2 UVSize
	{
		get
		{
			return m_UVSize;
		}
	}

	public SpriteData(float xp, float yp, float xs, float ys)
	{
		m_UVPos = new Vector2(xp, yp);
		m_UVSize = new Vector2(xs, ys);
	}

	public static SpriteData GetSpriteDataWithSpriteSheet(Type typeOfSpriteSheet, string spriteDataName)
	{
		FieldInfo field = typeOfSpriteSheet.GetField(spriteDataName);
		if (field == null)
		{
			Utility.Log(ELog.Errors, "fi == null for " + spriteDataName);
			return null;
		}
		return (SpriteData)field.GetValue(null);
	}

	public void ModifyUV(Mesh mesh)
	{
		Vector3 min = mesh.bounds.min;
		Vector3 max = mesh.bounds.max;
		Vector2[] uv = mesh.uv;
		for (int i = 0; i < uv.Length; i++)
		{
			float num = 1f - (mesh.vertices[i].x - min.x) / (max.x - min.x);
			float num2 = 1f - (mesh.vertices[i].y - min.y) / (max.y - min.y);
			Vector2 vector = new Vector2
			{
				x = num * m_UVSize.x,
				y = num2 * m_UVSize.y
			};
			uv[i] = m_UVPos + vector;
		}
		mesh.uv = uv;
	}
}
