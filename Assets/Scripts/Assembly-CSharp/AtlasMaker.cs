using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AtlasMaker : MonoBehaviour
{
	public struct STexture
	{
		public Texture2D texture;

		public Vector2 offset;

		public int atlas;
	}

	public struct SNode
	{
		public SNode[] children;

		public Rect rect;

		public int texture;

		public bool full;
	}

	public struct SIndex : IComparer<SIndex>
	{
		public int texInd;

		public int width;

		public int height;

		public string name;

		public int Compare(SIndex x, SIndex y)
		{
			return x.name.CompareTo(y.name);
		}
	}

	public class TextureSorter : IComparer<TextureSorter>
	{
		private Texture2D m_Texture;

		public Texture2D Texture
		{
			get
			{
				return m_Texture;
			}
			set
			{
				m_Texture = value;
			}
		}

		public TextureSorter()
		{
		}

		public TextureSorter(Texture2D tex)
		{
			m_Texture = tex;
		}

		public int Compare(TextureSorter a, TextureSorter b)
		{
			if (a.m_Texture == null || b.m_Texture == null)
			{
				return 0;
			}
			return a.m_Texture.name.CompareTo(b.m_Texture.name);
		}

		public static IComparer<TextureSorter> TextureSorterHelper()
		{
			return new TextureSorter();
		}
	}

	public static int s_SizeMax = 2048;

	public static int s_Padding = 0;

	public static int s_MarginNibbling = 0;

	public static int s_PaddingHalfSize = s_Padding / 2;

	public static TextureFormat s_Format = TextureFormat.ARGB32;

	public static string s_TexPathOut = string.Empty;

	public static string s_ScriptPathOut = string.Empty;

	public static string s_FileName = "FileName";

	public static string s_Class = "SpriteData";

	public static bool s_UsePackTexture = true;

	private static Texture2D[] SortTextures(Object[] texs)
	{
		List<TextureSorter> list = new List<TextureSorter>();
		foreach (Object obj in texs)
		{
			list.Add(new TextureSorter((Texture2D)obj));
		}
		list.Sort(TextureSorter.TextureSorterHelper());
		List<Texture2D> list2 = new List<Texture2D>();
		foreach (TextureSorter item in list)
		{
			list2.Add(item.Texture);
		}
		return list2.ToArray();
	}

	public static void MakeAtlas(Object[] texs)
	{
		Texture2D[] texs2 = SortTextures(texs);
		if (s_UsePackTexture)
		{
			MakeAtlasPackTexture(texs2);
		}
		else
		{
			MakeAtlasManual(texs2);
		}
	}

	public static void MakeAtlasManual(Texture2D[] texs)
	{
		STexture[] textures = new STexture[texs.Length];
		for (int i = 0; i < texs.Length; i++)
		{
			textures[i].texture = texs[i];
			textures[i].offset = default(Vector2);
			textures[i].atlas = -1;
		}
		Process(ref textures, true);
	}

	private static int SupOrEgalPow2(int n)
	{
		int num = n & (n - 1);
		if (num == 0)
		{
			return (n == 0) ? 1 : n;
		}
		do
		{
			n = num;
			num &= num - 1;
		}
		while (num != 0);
		return n << 1;
	}

	private static Texture2D[] Process(ref STexture[] textures, bool export)
	{
		List<SIndex> list = new List<SIndex>(textures.Length);
		for (int i = 0; i < textures.Length; i++)
		{
			SIndex item = new SIndex
			{
				texInd = i,
				width = textures[i].texture.width,
				height = textures[i].texture.height,
				name = textures[i].texture.name
			};
			if (s_Padding > 0 && Mathf.Min(item.width, item.height) < s_SizeMax)
			{
				item.width += s_Padding;
				item.height += s_Padding;
			}
			list.Add(item);
		}
		list.Sort(default(SIndex));
		if (textures.Length <= 0)
		{
			Utility.Log("AtlasMaker::Process : no texture selected");
			return null;
		}
		List<SNode> atlas = new List<SNode>();
		CreateAtlas(ref atlas, Mathf.Max(list[0].width, list[0].height));
		for (int i = 0; i < textures.Length; i++)
		{
			int j;
			SNode node;
			for (j = 0; j < atlas.Count; j++)
			{
				node = atlas[j];
				if (InsertTexture(ref node, list[i], ref textures))
				{
					atlas[j] = node;
					break;
				}
			}
			if (j < atlas.Count)
			{
				continue;
			}
			SNode grandParent = default(SNode);
			if (GrowAtlas(atlas[atlas.Count - 1], out grandParent))
			{
				if (!InsertTexture(ref grandParent, list[i], ref textures))
				{
					Utility.Log(ELog.Errors, "Error in InsertTexture");
				}
				atlas[atlas.Count - 1] = grandParent;
				continue;
			}
			CreateAtlas(ref atlas, Mathf.Max(list[i].width, list[i].height));
			node = atlas[j];
			if (InsertTexture(ref node, list[i], ref textures))
			{
				atlas[j] = node;
			}
			else
			{
				Utility.Log(ELog.Errors, "Error in InsertTexture");
			}
		}
		if (export && s_Format != TextureFormat.ARGB32 && s_Format != TextureFormat.RGB24)
		{
			s_Format = TextureFormat.ARGB32;
		}
		bool flag = s_Format == TextureFormat.DXT1 || s_Format == TextureFormat.DXT5;
		TextureFormat format = ((!flag) ? s_Format : TextureFormat.ARGB32);
		Texture2D[] atlas2 = new Texture2D[atlas.Count];
		for (int i = 0; i < atlas.Count; i++)
		{
			atlas2[i] = new Texture2D(Mathf.FloorToInt(atlas[i].rect.width), Mathf.FloorToInt(atlas[i].rect.height), format, false);
			WriteInAtlas(ref atlas2, i, atlas[i], ref textures);
			if (flag)
			{
				atlas2[i].Compress(false);
			}
			atlas2[i].Apply();
		}
		if (export)
		{
			ExportManual(ref atlas2, ref atlas, ref textures);
		}
		atlas2[0].filterMode = FilterMode.Point;
		return atlas2;
	}

	private static void CreateAtlas(ref List<SNode> atlas, int size)
	{
		int num = SupOrEgalPow2(size);
		SNode item = new SNode
		{
			rect = new Rect(0f, 0f, num, num),
			children = null,
			texture = -1,
			full = false
		};
		atlas.Add(item);
	}

	private static bool InsertTexture(ref SNode node, SIndex index, ref STexture[] textures)
	{
		if (node.full || node.rect.width < (float)textures[index.texInd].texture.width || node.rect.height < (float)textures[index.texInd].texture.height || node.texture >= 0)
		{
			return false;
		}
		if (node.children != null)
		{
			for (int i = 0; i < 2; i++)
			{
				if (InsertTexture(ref node.children[i], index, ref textures))
				{
					if (i == 2 && node.children[i].full)
					{
						node.full = true;
					}
					return true;
				}
			}
			return false;
		}
		if (node.rect.height > (float)index.height)
		{
			SplitHeight(ref node, index.height);
			if (InsertTexture(ref node.children[0], index, ref textures))
			{
				return true;
			}
			Utility.Log(ELog.Errors, "Error in InsertTexture");
		}
		if (node.rect.width > (float)index.width)
		{
			SplitWidth(ref node, index.width);
			if (InsertTexture(ref node.children[0], index, ref textures))
			{
				return true;
			}
			Utility.Log(ELog.Errors, "Error in InsertTexture");
		}
		node.texture = index.texInd;
		node.full = true;
		if (s_Padding > 0 && Mathf.Min(index.width, index.height) < s_SizeMax)
		{
			node.rect = new Rect(node.rect.x + (float)s_PaddingHalfSize, node.rect.y + (float)s_PaddingHalfSize, node.rect.width - (float)s_PaddingHalfSize, node.rect.height - (float)s_PaddingHalfSize);
		}
		return true;
	}

	private static bool GrowAtlas(SNode node, out SNode grandParent)
	{
		if (node.rect.width >= (float)s_SizeMax)
		{
			grandParent.rect = default(Rect);
			grandParent.children = null;
			grandParent.texture = -1;
			grandParent.full = false;
			return false;
		}
		float num = node.rect.width * 2f;
		SNode sNode = new SNode
		{
			rect = new Rect(node.rect.width, 0f, num - node.rect.width, node.rect.height),
			children = null,
			texture = -1,
			full = false
		};
		SNode sNode2 = new SNode
		{
			rect = new Rect(0f, 0f, num, node.rect.height),
			children = new SNode[2]
		};
		sNode2.children[0] = node;
		sNode2.children[1] = sNode;
		sNode2.texture = -1;
		sNode2.full = false;
		SNode sNode3 = new SNode
		{
			rect = new Rect(0f, node.rect.height, num, num - node.rect.height),
			children = null,
			texture = -1,
			full = false
		};
		grandParent.rect = new Rect(0f, 0f, num, num);
		grandParent.children = new SNode[2];
		grandParent.children[0] = sNode2;
		grandParent.children[1] = sNode3;
		grandParent.texture = -1;
		grandParent.full = false;
		return true;
	}

	private static void SplitHeight(ref SNode node, int size)
	{
		SNode sNode = new SNode
		{
			rect = new Rect(node.rect.x, node.rect.y, node.rect.width, size),
			children = null,
			texture = -1,
			full = false
		};
		SNode sNode2 = new SNode
		{
			rect = new Rect(node.rect.x, node.rect.y + (float)size, node.rect.width, node.rect.height - (float)size),
			children = null,
			texture = -1,
			full = false
		};
		node.children = new SNode[2];
		node.children[0] = sNode;
		node.children[1] = sNode2;
	}

	private static void SplitWidth(ref SNode node, int size)
	{
		SNode sNode = new SNode
		{
			rect = new Rect(node.rect.x, node.rect.y, size, node.rect.height),
			children = null,
			texture = -1,
			full = false
		};
		SNode sNode2 = new SNode
		{
			rect = new Rect(node.rect.x + (float)size, node.rect.y, node.rect.width - (float)size, node.rect.height),
			children = null,
			texture = -1,
			full = false
		};
		node.children = new SNode[2];
		node.children[0] = sNode;
		node.children[1] = sNode2;
	}

	private static void WriteInAtlas(ref Texture2D[] atlas, int atlasInd, SNode node, ref STexture[] textures)
	{
		if (node.children != null)
		{
			for (int i = 0; i < 2; i++)
			{
				WriteInAtlas(ref atlas, atlasInd, node.children[i], ref textures);
			}
		}
		else
		{
			if (node.texture < 0)
			{
				return;
			}
			Texture2D texture = textures[node.texture].texture;
			bool flag = s_Padding > 0 && Mathf.Min(texture.width, texture.height) < s_SizeMax;
			textures[node.texture].atlas = atlasInd;
			textures[node.texture].offset = new Vector2(node.rect.x, node.rect.y);
			if (flag)
			{
			}
			int num = Mathf.FloorToInt(textures[node.texture].offset.x);
			int num2 = Mathf.FloorToInt(textures[node.texture].offset.y);
			Color[] pixels = texture.GetPixels();
			atlas[atlasInd].SetPixels(num, num2, texture.width, texture.height, pixels);
			if (flag)
			{
				Color color = new Color(0f, 0f, 0f, 0f);
				for (int j = -1; j < texture.width + 1; j++)
				{
					atlas[atlasInd].SetPixel(num + j, num2 - 1, color);
					atlas[atlasInd].SetPixel(num + j, num2 - 2, color);
					atlas[atlasInd].SetPixel(num + j, num2 + texture.height, color);
					atlas[atlasInd].SetPixel(num + j, num2 + texture.height + 1, color);
				}
				for (int k = -1; k < texture.height + 1; k++)
				{
					atlas[atlasInd].SetPixel(num - 1, num2 + k, color);
					atlas[atlasInd].SetPixel(num - 2, num2 + k, color);
					atlas[atlasInd].SetPixel(num + texture.width, num2 + k, color);
					atlas[atlasInd].SetPixel(num + 1 + texture.width, num2 + k, color);
				}
			}
		}
	}

	private static void ExportManual(ref Texture2D[] atlas, ref List<SNode> nodeAtlas, ref STexture[] textures)
	{
		for (int i = 0; i < atlas.Length; i++)
		{
			int num = i + 1;
			string text = string.Format("{0:00}", num);
			if (atlas.Length == 1)
			{
				text = string.Empty;
			}
			string path = Application.dataPath + "/" + s_TexPathOut + "/" + s_FileName + text + ".png";
			ExportTexture(atlas[i], path);
			path = Application.dataPath + "/" + s_ScriptPathOut + "/" + s_FileName + text + ".cs";
			FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(stream);
			sw.Write("public class " + s_FileName + text + "\n{\n");
			ExportNode(ref sw, nodeAtlas[i], ref textures, atlas[i].width);
			sw.Write("}");
			sw.Close();
		}
	}

	private static void ExportNode(ref StreamWriter sw, SNode node, ref STexture[] textures, float size)
	{
		if (node.children != null)
		{
			for (int i = 0; i < 2; i++)
			{
				ExportNode(ref sw, node.children[i], ref textures, size);
			}
		}
		else if (node.texture >= 0)
		{
			Texture2D texture = textures[node.texture].texture;
			string text = texture.name;
			text = text.Replace('-', '_');
			string text2 = string.Format("{0:0.000}f", node.rect.x / size);
			string text3 = string.Format("{0:0.000}f", node.rect.y / size);
			string text4 = string.Format("{0:0.000}f", (float)texture.width / size);
			string text5 = string.Format("{0:0.000}f", (float)texture.height / size);
			sw.Write("\tpublic static " + s_Class + " " + text + " = new " + s_Class + "(" + text2 + ", " + text3 + ", " + text4 + ", " + text5 + ");\n");
		}
	}

	public static void MakeAtlasPackTexture(Texture2D[] texs)
	{
		Texture2D texture2D = new Texture2D(s_SizeMax, s_SizeMax);
		Rect[] rects = texture2D.PackTextures(texs, s_Padding, s_SizeMax);
		if (rects != null && rects.Length > 0 && rects.Length == texs.Length)
		{
			string path = Application.dataPath + "/" + s_TexPathOut + "/" + s_FileName + ".png";
			ExportTexture(texture2D, path);
			NibbleMargin(ref rects, (float)s_MarginNibbling / (float)texture2D.width, (float)s_MarginNibbling / (float)texture2D.height);
			path = Application.dataPath + "/" + s_ScriptPathOut + "/" + s_FileName + ".cs";
			FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(stream);
			sw.Write("public class " + s_FileName + "\n{\n");
			ExportRects(ref sw, ref rects, ref texs);
			sw.Write("}");
			sw.Close();
		}
		else
		{
			Utility.Log(ELog.Errors, "MakeAtlasPackTexture fails: " + rects.Length + " / " + texs.Length);
		}
	}

	private static void NibbleMargin(ref Rect[] rects, float xPad, float yPad)
	{
		for (int i = 0; i < rects.Length; i++)
		{
			rects[i] = new Rect(rects[i].xMin + xPad, rects[i].yMin + yPad, rects[i].width - xPad * 2f, rects[i].height - yPad * 2f);
		}
	}

	private static void ExportRects(ref StreamWriter sw, ref Rect[] rects, ref Texture2D[] texs)
	{
		for (int i = 0; i < rects.Length; i++)
		{
			string text = "\tpublic static " + s_Class + " " + texs[i].name + " = new " + s_Class + "(";
			text = text + string.Format("{0:0.00000}f", rects[i].x) + ", ";
			text = text + string.Format("{0:0.00000}f", rects[i].y) + ", ";
			text = text + string.Format("{0:0.00000}f", rects[i].width) + ", ";
			text = text + string.Format("{0:0.00000}f", rects[i].height) + ");\n";
			sw.Write(text);
		}
	}

	public static void ExportTexture(Texture2D tex, string path)
	{
		byte[] bytes = tex.EncodeToPNG();
		ExportBytes(bytes, path);
		if (path.Contains("/HD/"))
		{
			path = path.Replace("/HD/", "/LD/");
			ExportBytes(bytes, path);
		}
	}

	public static void ExportBytes(byte[] bytes, string path)
	{
		FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		binaryWriter.Write(bytes);
		binaryWriter.Close();
		fileStream.Close();
	}
}
