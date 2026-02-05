using UnityEngine;

public class FonctionalityItem : MonoBehaviour
{
	public enum EType
	{
		InGameMenuClose = 0,
		InGameMenuOpen = 1,
		CustomizeCostume = 2,
		CustomizeCostumeRoot = 3,
		CustomizeEnvironment = 4,
		CustomizeEnvironmentRoot = 5,
		AR = 6,
		ARRoot = 7,
		OpenLibrary = 8,
		SwitchCamera = 9,
		LastPicture = 10,
		MusicOn = 11,
		MusicOff = 12,
		Valid = 13,
		Back = 14,
		Pause = 15,
		PauseRoot = 16,
		Screenshot = 17,
		LastScreenshot = 18,
		Progress = 19,
		ShadowOn = 20,
		ShadowOff = 21,
		Sing = 22,
		Count = 23,
		None = -1
	}

	private FonctionalityItem[] m_Children;

	private FonctionalityItem m_Parent;

	private EType m_Type = EType.None;

	public EType m_RootType = EType.None;

	public EType m_ItemType = EType.None;

	private bool m_Enable = true;

	public bool m_Atlas = true;

	public FonctionalityItem[] Children
	{
		get
		{
			return m_Children;
		}
		set
		{
			m_Children = value;
		}
	}

	public FonctionalityItem Parent
	{
		get
		{
			return m_Parent;
		}
	}

	public EType Type
	{
		get
		{
			return m_Type;
		}
		set
		{
			if (m_Type != value && value != EType.None)
			{
				m_Type = value;
				ModifyUVs();
			}
		}
	}

	public EType RootType
	{
		get
		{
			return m_RootType;
		}
	}

	public EType ItemType
	{
		get
		{
			return m_ItemType;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return (Texture2D)base.GetComponent<Renderer>().material.mainTexture;
		}
		set
		{
			base.GetComponent<Renderer>().material.mainTexture = value;
		}
	}

	public bool Active
	{
		get
		{
			return base.gameObject.active;
		}
		set
		{
			if (m_Enable)
			{
				base.gameObject.active = value;
			}
		}
	}

	public bool Enable
	{
		get
		{
			return m_Enable;
		}
		set
		{
			m_Enable = value;
		}
	}

	public Vector3 Position
	{
		get
		{
			return base.transform.localPosition;
		}
		set
		{
			base.transform.localPosition = value;
		}
	}

	public Vector3 Scale
	{
		get
		{
			return base.transform.localScale;
		}
		set
		{
			base.transform.localScale = value;
		}
	}

	public FonctionalityItem GetChild(EType type)
	{
		if (type == Type)
		{
			return this;
		}
		FonctionalityItem[] children = Children;
		if (children != null)
		{
			for (int i = 0; i < children.Length; i++)
			{
				FonctionalityItem child = children[i].GetChild(type);
				if (child != null)
				{
					return child;
				}
			}
		}
		return null;
	}

	public bool Contains(FonctionalityItem item)
	{
		for (int i = 0; i < m_Children.Length; i++)
		{
			if (m_Children[i] == item)
			{
				return true;
			}
		}
		return false;
	}

	public void Initialize()
	{
		Transform parent = base.transform.parent;
		if (parent != null)
		{
			m_Parent = parent.GetComponent<FonctionalityItem>();
		}
		int childCount = base.transform.childCount;
		if (childCount > 0)
		{
			m_Children = new FonctionalityItem[childCount];
			for (int i = 0; i < childCount; i++)
			{
				m_Children[i] = base.transform.GetChild(i).GetComponent<FonctionalityItem>();
				m_Children[i].Type = m_Children[i].ItemType;
				m_Children[i].Initialize();
			}
		}
		ModifyUVs();
	}

	private void ModifyUVs()
	{
		if (m_Atlas)
		{
			SpriteData spriteDataWithSpriteSheet = SpriteData.GetSpriteDataWithSpriteSheet(typeof(GUIAtlas), m_Type.ToString());
			if (spriteDataWithSpriteSheet != null)
			{
				Mesh mesh = GetComponent<MeshFilter>().mesh;
				spriteDataWithSpriteSheet.ModifyUV(mesh);
			}
		}
	}

	public FonctionalityItem LookingFor(EType type)
	{
		if (m_Type == type)
		{
			return this;
		}
		FonctionalityItem fonctionalityItem = null;
		FonctionalityItem[] children = Children;
		if (children != null)
		{
			for (int i = 0; i < children.Length; i++)
			{
				if (!(fonctionalityItem == null))
				{
					break;
				}
				FonctionalityItem fonctionalityItem2 = children[i];
				fonctionalityItem = fonctionalityItem2.LookingFor(type);
			}
		}
		return fonctionalityItem;
	}
}
