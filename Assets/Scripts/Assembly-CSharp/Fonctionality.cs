using System.Collections.Generic;
using UnityEngine;

public class Fonctionality : MonoBehaviour
{
	public enum EState
	{
		Closing = 0,
		Closed = 1,
		Opening = 2,
		Opened = 3
	}

	public struct SUpdateItem
	{
		public FonctionalityItem Item;

		public bool Enable;

		public Texture2D Texture;

		public FonctionalityItem.EType Type;
	}

	public delegate void Handler(int fonctionnalityID, FonctionalityItem.EType type);

	public delegate void Handler2(int fonctionnalityID, FonctionalityItem.EType type1, FonctionalityItem.EType type2);

	private static float s_OpeningTime = 0.2f;

	private static float s_BaseSpace = 1f;

	private static float s_ItemSpace = 8.5f;

	private static float s_ChildScale = 0.8f;

	private static float s_DepthValue = 0.1f;

	private static Vector3 s_IPhonePos = new Vector3(12f, 21f, 20f);

	private static Vector3 s_IPadPos = new Vector3(14.5f, 21f, 20f);

	private FonctionalityItem m_RootItem;

	private FonctionalityItem m_CurrentItem;

	private FonctionalityItem m_NextItem;

	private List<SUpdateItem> m_ToUpdateItemList = new List<SUpdateItem>();

	private EState m_State = EState.Closed;

	private int m_ID = -1;

	private float m_CurrentTime = -1f;

	private Vector3 m_RootPosition = Vector3.zero;

	private Vector3 m_RootScale = Vector3.one;

	private bool m_Wait;

	private bool m_Active;

	private bool m_Locked;

	private bool m_RefreshMerger;

	public EState State
	{
		set
		{
			m_State = value;
		}
	}

	public bool Wait
	{
		set
		{
			m_Wait = value;
			AutoOpen();
		}
	}

	public bool Lock
	{
		get
		{
			return m_Locked;
		}
		set
		{
			m_Locked = value;
		}
	}

	public bool Active
	{
		set
		{
			m_Active = value;
			m_CurrentItem.Active = m_Active;
			m_RefreshMerger = true;
			if (!m_Active || m_State != EState.Closed)
			{
				FonctionalityItem[] children = m_CurrentItem.Children;
				foreach (FonctionalityItem fonctionalityItem in children)
				{
					fonctionalityItem.Active = m_Active;
				}
			}
		}
	}

	public static event Handler RootPressed;

	public static event Handler2 ItemPressed;

	public static event Handler Opened;

	public static event Handler Closed;

	public static event Handler2 RootChange;

	public void Enable(bool b, FonctionalityItem.EType type)
	{
		FonctionalityItem fonctionalityItem = m_RootItem.LookingFor(type);
		if (fonctionalityItem != null && fonctionalityItem.Enable != b)
		{
			if (m_State != EState.Closed && m_CurrentItem.Contains(fonctionalityItem))
			{
				SUpdateItem item = default(SUpdateItem);
				item.Item = fonctionalityItem;
				item.Enable = true;
				item.Texture = null;
				item.Type = FonctionalityItem.EType.None;
				m_ToUpdateItemList.Add(item);
				Close(true);
			}
			else
			{
				fonctionalityItem.Enable = b;
			}
		}
	}

	public void SetTexture(Texture2D texture, FonctionalityItem.EType type)
	{
		FonctionalityItem fonctionalityItem = m_RootItem.LookingFor(type);
		if (fonctionalityItem != null)
		{
			fonctionalityItem.Texture = texture;
		}
	}

	public void SetType(FonctionalityItem.EType newType, FonctionalityItem.EType oldType)
	{
		m_RefreshMerger = true;
		FonctionalityItem fonctionalityItem = m_RootItem.LookingFor(oldType);
		if (fonctionalityItem != null)
		{
			fonctionalityItem.Type = newType;
		}
	}

	public bool IsRoot()
	{
		return m_CurrentItem == m_RootItem;
	}

	public void RefreshMerger()
	{
		m_RefreshMerger = true;
	}

	public void Update()
	{
		switch (m_State)
		{
		case EState.Closing:
			UpdateClosing();
			break;
		case EState.Opening:
			UpdateOpening();
			break;
		default:
			UpdateInteractor();
			break;
		}
	}

	public void Open(bool animated)
	{
		if (m_State == EState.Opened)
		{
			return;
		}
		Vector3 scale = new Vector3(s_ChildScale, s_ChildScale, s_ChildScale);
		FonctionalityItem[] children = m_CurrentItem.Children;
		int num = 0;
		foreach (FonctionalityItem fonctionalityItem in children)
		{
			if (fonctionalityItem.Enable)
			{
				num++;
				fonctionalityItem.Active = true;
			}
			fonctionalityItem.Scale = scale;
		}
		if (animated && num > 0)
		{
			State = EState.Opening;
			m_CurrentTime = s_OpeningTime;
		}
		else
		{
			ComputeChildrenOpeningPosition(0f);
			OnOpen();
		}
	}

	public void Close(bool animated)
	{
		if (m_State == EState.Closed)
		{
			return;
		}
		FonctionalityItem[] children = m_CurrentItem.Children;
		int num = 0;
		for (int i = 0; i < children.Length; i++)
		{
			if (children[i].Enable)
			{
				num++;
			}
		}
		if (animated && num > 0)
		{
			State = EState.Closing;
			m_CurrentTime = s_OpeningTime;
		}
		else
		{
			ComputeChildrenClosingPosition(0f);
			OnClose();
		}
	}

	public void Next(FonctionalityItem item)
	{
		m_NextItem = item;
		Close(true);
	}

	public bool ResfreshMeshMerger()
	{
		return m_State == EState.Closing || m_State == EState.Opening || m_RefreshMerger;
	}

	public void SetMergeMeshesFinished()
	{
		m_RefreshMerger = false;
	}

	private void UpdateOpening()
	{
		if (m_CurrentTime > 0f)
		{
			m_CurrentTime -= Time.deltaTime;
			if (m_CurrentTime <= 0f)
			{
				m_CurrentTime = 0f;
				ComputeChildrenOpeningPosition(m_CurrentTime);
				OnOpen();
				m_RefreshMerger = true;
			}
			else
			{
				ComputeChildrenOpeningPosition(m_CurrentTime);
			}
		}
	}

	private void UpdateClosing()
	{
		if (m_CurrentTime > 0f)
		{
			m_CurrentTime -= Time.deltaTime;
			if (m_CurrentTime <= 0f)
			{
				m_CurrentTime = 0f;
				ComputeChildrenClosingPosition(m_CurrentTime);
				OnClose();
				m_RefreshMerger = true;
			}
			else
			{
				ComputeChildrenClosingPosition(m_CurrentTime);
			}
		}
	}

	private void OnOpen()
	{
		State = EState.Opened;
		if (m_ToUpdateItemList.Count > 0)
		{
			m_ToUpdateItemList.Clear();
		}
		else if (Fonctionality.Opened != null)
		{
			Fonctionality.Opened(m_ID, m_CurrentItem.Type);
		}
	}

	private void OnClose()
	{
		State = EState.Closed;
		FonctionalityItem[] children = m_CurrentItem.Children;
		foreach (FonctionalityItem fonctionalityItem in children)
		{
			fonctionalityItem.Active = false;
		}
		int count = m_ToUpdateItemList.Count;
		if (Fonctionality.Closed != null && count == 0 && m_Active)
		{
			Fonctionality.Closed(m_ID, m_CurrentItem.Type);
		}
		bool flag = false;
		if (m_NextItem != null)
		{
			flag = true;
			SwitchRoot();
		}
		foreach (SUpdateItem toUpdateItem in m_ToUpdateItemList)
		{
			flag = true;
			if (toUpdateItem.Enable)
			{
				toUpdateItem.Item.Enable = !toUpdateItem.Item.Enable;
			}
			if (toUpdateItem.Texture != null)
			{
				toUpdateItem.Item.Texture = toUpdateItem.Texture;
			}
			if (toUpdateItem.Type != FonctionalityItem.EType.None)
			{
				toUpdateItem.Item.Type = toUpdateItem.Type;
			}
		}
		if (flag)
		{
			AutoOpen();
		}
	}

	private void AutoOpen()
	{
		if (!m_Wait && (!IsRoot() || m_ToUpdateItemList.Count > 0))
		{
			Open(true);
		}
	}

	private void UpdateInteractor()
	{
		if (m_Locked || Utility.InputStoppedByActivity() || !(base.GetComponent<Camera>() != null) || AllInput.GetTouchCount() != 1 || AllInput.GetState(0) != AllInput.EState.Began)
		{
			return;
		}
		Ray ray = base.GetComponent<Camera>().ScreenPointToRay(AllInput.GetRaycastPosition(0));
		RaycastHit hitInfo;
		if (!(m_CurrentItem != null) || !Physics.Raycast(ray, out hitInfo))
		{
			return;
		}
		if (hitInfo.transform == m_CurrentItem.transform)
		{
			OnHitRootItem();
		}
		else
		{
			if (m_State != EState.Opened || !(m_CurrentItem != null) || m_CurrentItem.Children == null)
			{
				return;
			}
			FonctionalityItem[] children = m_CurrentItem.Children;
			if (children == null)
			{
				return;
			}
			foreach (FonctionalityItem fonctionalityItem in children)
			{
				if (hitInfo.transform == fonctionalityItem.transform)
				{
					OnHitChildItem(fonctionalityItem);
					break;
				}
			}
		}
	}

	public void OnHitRootItem()
	{
		if (m_Locked)
		{
			return;
		}
		m_RefreshMerger = true;
		if (Fonctionality.RootPressed != null)
		{
			Fonctionality.RootPressed(m_ID, m_CurrentItem.Type);
		}
		switch (m_State)
		{
		case EState.Closed:
			Open(true);
			break;
		case EState.Opened:
		{
			FonctionalityItem parent = m_CurrentItem.Parent;
			if (parent == null)
			{
				Close(true);
			}
			else
			{
				Next(parent);
			}
			break;
		}
		case EState.Opening:
			break;
		}
	}

	private void OnHitChildItem(FonctionalityItem item)
	{
		if (!m_Locked)
		{
			m_RefreshMerger = true;
			if (Fonctionality.ItemPressed != null)
			{
				Fonctionality.ItemPressed(m_ID, item.Type, m_CurrentItem.Type);
			}
			FonctionalityItem[] children = item.Children;
			if (children != null && children.Length != 0)
			{
				Next(item);
			}
		}
	}

	public void Initialize(FonctionalityItem item)
	{
		int width = Screen.width;
		int height = Screen.height;
		float num = 1024f / (float)Mathf.Max(width, height);
		float num2 = (float)Mathf.Min(width, height) * num;
		num2 -= 768f;
		Vector3 vector = s_IPadPos - s_IPhonePos;
		Vector3 vector2 = vector * num2 / 86f;
		Vector3 localPosition = item.transform.localPosition;
		localPosition += vector2;
		item.transform.localPosition = localPosition;
		m_RootItem = (m_CurrentItem = item);
		m_CurrentItem.Type = item.RootType;
		m_RootPosition = m_CurrentItem.Position;
		m_RootScale = m_CurrentItem.Scale;
		m_CurrentItem.Initialize();
	}

	public void Destroy(FonctionalityItem.EType type)
	{
		FonctionalityItem fonctionalityItem = m_RootItem.LookingFor(type);
		if (!(fonctionalityItem != null))
		{
			return;
		}
		FonctionalityItem parent = fonctionalityItem.Parent;
		Object.DestroyImmediate(fonctionalityItem.gameObject);
		if (!(parent != null))
		{
			return;
		}
		int childCount = parent.transform.childCount;
		if (childCount > 0)
		{
			FonctionalityItem[] array = new FonctionalityItem[childCount];
			for (int i = 0; i < childCount; i++)
			{
				array[i] = parent.transform.GetChild(i).GetComponent<FonctionalityItem>();
			}
			parent.Children = array;
		}
	}

	public void SwitchRoot()
	{
		if (m_CurrentItem != m_NextItem && m_NextItem != null)
		{
			if (m_Active)
			{
				m_CurrentItem.Active = false;
				m_NextItem.Active = true;
			}
			FonctionalityItem.EType type = m_CurrentItem.Type;
			FonctionalityItem.EType rootType = m_NextItem.RootType;
			m_NextItem.Type = rootType;
			m_CurrentItem.Type = m_CurrentItem.ItemType;
			m_CurrentItem = m_NextItem;
			if (IsRoot())
			{
				m_CurrentItem.Position = m_RootPosition;
				m_CurrentItem.Scale = m_RootScale;
			}
			else
			{
				m_CurrentItem.Position = Vector3.zero;
				m_CurrentItem.Scale = Vector3.one;
			}
			if (Fonctionality.RootChange != null)
			{
				Fonctionality.RootChange(m_ID, type, rootType);
			}
		}
		m_NextItem = null;
	}

	private void ComputeChildrenOpeningPosition(float time)
	{
		FonctionalityItem[] children = m_CurrentItem.Children;
		int num = 0;
		foreach (FonctionalityItem fonctionalityItem in children)
		{
			if (fonctionalityItem.Enable)
			{
				num++;
				float num2 = s_BaseSpace + (float)num * s_ItemSpace;
				Vector3 zero = Vector3.zero;
				zero.y = (float)(-num) * s_DepthValue;
				zero.z = num2 - time * num2 / s_OpeningTime;
				fonctionalityItem.Position = zero;
			}
		}
	}

	private void ComputeChildrenClosingPosition(float time)
	{
		FonctionalityItem[] children = m_CurrentItem.Children;
		int num = 0;
		foreach (FonctionalityItem fonctionalityItem in children)
		{
			if (fonctionalityItem.Enable)
			{
				num++;
				float num2 = s_BaseSpace + (float)num * s_ItemSpace;
				Vector3 zero = Vector3.zero;
				zero.y = (float)(-num) * s_DepthValue;
				zero.z = time * num2 / s_OpeningTime;
				fonctionalityItem.Position = zero;
			}
		}
	}
}
