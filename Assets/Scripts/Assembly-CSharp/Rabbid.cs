using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbid : MonoBehaviour
{
	public enum ECostume
	{
		Naked = 0,
		String = 1,
		Astronaut = 2,
		Cleopatra = 3,
		Cowboy = 4,
		Hippy = 5,
		Knight = 6,
		Bow = 7,
		GhettoBlaster = 8,
		Nunchaku = 9,
		RugbyBall = 10,
		SausagePolice = 11,
		Count = 12
	}

	public enum EEnvironment
	{
		Standard = 0,
		Boy = 1,
		Girl = 2,
		Astronaut = 3,
		Hippy = 4,
		Knight = 5,
		Cowboy = 6,
		Cleopatra = 7,
		Count = 8
	}

	public enum ERabbid
	{
		InGame = 0,
		Customize = 1,
		Count = 2
	}

	public string currentAnim = "None";

	public float currentAnimLength;

	public float currentAnimTime;

	protected string m_CurrentAnimation = "None";

	protected float m_CurrentAnimTime;

	protected float m_CurrentAnimLength;

	protected bool m_CurrentAnimLoop;

	protected WrapMode m_CurrentAnimWrapMode;

	protected List<string> m_IdleAnims = new List<string>();

	protected List<AnimationClip> m_BaseAnimationClips = new List<AnimationClip>();

	protected List<string> m_AdditionalMiniGameAnims = new List<string>();

	protected List<string> m_AdditionalAnimationClips = new List<string>();

	protected List<string> m_ToBeRemovedClips = new List<string>();

	protected float m_RemoveTimer;

	protected AchievementTable m_AchievementsTable;

	protected MiniGamesThematics m_ThematicsTable;

	protected GameObject m_Costume;

	protected ECostume m_CurrentCostume;

	private bool m_CostumeResyncAnim;

	protected Renderer m_CostumeRenderer;

	protected Renderer m_Renderer;

	protected Material m_SharedMaterial;

	protected EEnvironment m_CurrentEnvironment = EEnvironment.Count;

	protected Renderer m_EnvironmentRenderer;

	protected GameObject m_Light;

	protected bool m_ResetXScale;

	private GameObject m_Eyes;

	private bool[] m_DisplayEyes = new bool[12];

	public GUISkin m_CommonSkin;

	protected Hashtable m_MiniGamesHashtable = new Hashtable();

	protected Hashtable m_MiniGamesHashtableRevert = new Hashtable();

	protected List<GameObject> m_MiniGamesObjects = new List<GameObject>();

	private bool m_AnimFirstFrame;

	private static bool s_LogAnimation = true;

	protected Hashtable m_TimeAnimTable;

	protected bool animIsPlaying
	{
		get
		{
			if (m_AnimFirstFrame)
			{
				return true;
			}
			switch (m_CurrentAnimWrapMode)
			{
			case WrapMode.Loop:
				return true;
			case WrapMode.PingPong:
				return m_CurrentAnimTime < m_CurrentAnimLength * 2f;
			default:
				if (m_CurrentAnimTime > m_CurrentAnimLength)
				{
					return false;
				}
				return base.GetComponent<Animation>().isPlaying;
			}
		}
	}

	public virtual void Start()
	{
		InstanciateAnimationClips();
		InitTimeAnimTable();
		m_AchievementsTable = new AchievementTable();
		m_ThematicsTable = new MiniGamesThematics();
		Transform transform = base.transform.Find("body_low01");
		if (transform != null)
		{
			m_Renderer = transform.gameObject.GetComponent<SkinnedMeshRenderer>();
			if (m_Renderer != null)
			{
				m_SharedMaterial = (Material)Object.Instantiate(m_Renderer.material);
				m_Renderer.sharedMaterial = m_SharedMaterial;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unable to find any skinned mesh renderer 'body_low01'");
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Unable to find 'body_low01' and its skinned mesh renderer");
		}
		transform = base.transform.Find("B_mainly_dummy");
		if (transform != null)
		{
			Renderer[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].sharedMaterial = m_SharedMaterial;
				}
			}
		}
		m_Eyes = GameObject.Find("B_bunny eye father");
		if (m_Eyes == null)
		{
			Utility.Log(ELog.Errors, "Unable to find 'B_bunny eye father'");
		}
		for (int i = 0; i < 12; i++)
		{
			m_DisplayEyes[i] = true;
		}
		m_Light = GameObject.Find("Lights");
		if (m_Light == null)
		{
			Utility.Log(ELog.Errors, "Unable to find 'Lights'");
		}
	}

	public virtual void Update()
	{
		if (m_AnimFirstFrame)
		{
			m_AnimFirstFrame = false;
		}
		if (m_CostumeResyncAnim)
		{
			SyncCostumeAnim();
			m_CostumeResyncAnim = false;
		}
		m_RemoveTimer += Time.deltaTime;
		m_CurrentAnimTime += Time.deltaTime;
		currentAnimTime = m_CurrentAnimTime;
	}

	public static ERabbid GetRabbidEnum(string str)
	{
		for (ERabbid eRabbid = ERabbid.InGame; eRabbid < ERabbid.Count; eRabbid++)
		{
			if (str.Equals(eRabbid.ToString()))
			{
				return eRabbid;
			}
		}
		return ERabbid.Count;
	}

	public static ECostume GetCostumeEnum(string str)
	{
		if (str == string.Empty)
		{
			return ECostume.Naked;
		}
		for (ECostume eCostume = ECostume.Naked; eCostume < ECostume.Count; eCostume++)
		{
			if (str.Equals(eCostume.ToString()))
			{
				return eCostume;
			}
		}
		return ECostume.Count;
	}

	public static EEnvironment GetEnvironmentEnum(string str)
	{
		if (str == string.Empty)
		{
			return EEnvironment.Standard;
		}
		for (EEnvironment eEnvironment = EEnvironment.Standard; eEnvironment < EEnvironment.Count; eEnvironment++)
		{
			if (str.Equals(eEnvironment.ToString()))
			{
				return eEnvironment;
			}
		}
		return EEnvironment.Count;
	}

	protected void SetXScale(float scale)
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x = scale;
		base.transform.localScale = localScale;
		if (m_Costume != null)
		{
			localScale = m_Costume.transform.localScale;
			localScale.x = scale;
			m_Costume.transform.localScale = localScale;
		}
		if (m_Light != null)
		{
			localScale = m_Light.transform.localScale;
			localScale.x = scale;
			m_Light.transform.localScale = localScale;
		}
	}

	protected void SetActive(bool b)
	{
		base.gameObject.SetActiveRecursively(b);
		if (m_Costume != null)
		{
			m_Costume.SetActiveRecursively(b);
		}
	}

	protected void SetT(Vector3 pos)
	{
		base.transform.localPosition = pos;
		if (m_Costume != null)
		{
			m_Costume.transform.localPosition = pos;
		}
	}

	protected void SetR(Vector3 euler)
	{
		base.transform.eulerAngles = euler;
		if (m_Costume != null)
		{
			m_Costume.transform.eulerAngles = euler;
		}
	}

	protected void SetR(Quaternion quat)
	{
		base.transform.rotation = quat;
		if (m_Costume != null)
		{
			m_Costume.transform.rotation = quat;
		}
	}

	protected void SetS(Vector3 scale)
	{
		base.transform.localScale = scale;
		if (m_Costume != null)
		{
			m_Costume.transform.localScale = scale;
		}
	}

	protected void SetTR(Vector3 pos, Vector3 euler)
	{
		base.transform.localPosition = pos;
		base.transform.eulerAngles = euler;
		if (m_Costume != null)
		{
			m_Costume.transform.localPosition = pos;
			m_Costume.transform.eulerAngles = euler;
		}
	}

	protected void SetTS(Vector3 pos, Vector3 scale)
	{
		base.transform.localPosition = pos;
		base.transform.localScale = scale;
		if (m_Costume != null)
		{
			m_Costume.transform.localPosition = pos;
			m_Costume.transform.localScale = scale;
		}
	}

	protected void SetTRS(Vector3 pos, Vector3 euler, Vector3 scale)
	{
		base.transform.localPosition = pos;
		base.transform.localEulerAngles = euler;
		base.transform.localScale = scale;
		if (m_Costume != null)
		{
			m_Costume.transform.localPosition = pos;
			m_Costume.transform.localEulerAngles = euler;
			m_Costume.transform.localScale = scale;
		}
	}

	protected void SetTRS(Vector3 pos, Quaternion rot, Vector3 scale)
	{
		base.transform.localScale = scale;
		base.transform.localRotation = rot;
		base.transform.localPosition = pos;
		if (m_Costume != null)
		{
			m_Costume.transform.localScale = scale;
			m_Costume.transform.localRotation = rot;
			m_Costume.transform.localPosition = pos;
		}
	}

	protected bool AnimIsPlaying(string animName)
	{
		if (m_CurrentAnimation == animName && m_AnimFirstFrame)
		{
			return true;
		}
		return base.GetComponent<Animation>().IsPlaying(animName);
	}

	public void PlayAnim(string anim)
	{
		AnimationState animationState = base.GetComponent<Animation>()[anim];
		if (animationState != null)
		{
			bool flag = animIsPlaying;
			currentAnim = anim;
			m_CurrentAnimation = anim;
			m_CurrentAnimTime = 0f;
			m_CurrentAnimWrapMode = animationState.wrapMode;
			m_CurrentAnimLength = ((m_TimeAnimTable == null) ? animationState.length : ((float)m_TimeAnimTable[anim]));
			currentAnimTime = 0f;
			currentAnimLength = m_CurrentAnimLength;
			if (flag)
			{
				base.GetComponent<Animation>().CrossFade(anim);
				if (m_Costume != null)
				{
					m_Costume.GetComponent<Animation>().CrossFade(anim);
				}
			}
			else
			{
				base.GetComponent<Animation>().Play(anim);
				if (m_Costume != null)
				{
					m_Costume.GetComponent<Animation>().Play(anim);
				}
			}
			foreach (GameObject miniGamesObject in m_MiniGamesObjects)
			{
				PlayAnim(anim, miniGamesObject);
			}
			if (s_LogAnimation)
			{
				Utility.Log(ELog.Animation, m_CurrentAnimation + " (" + m_CurrentAnimLength + ") - Cross fading: " + flag);
			}
			if (m_ResetXScale)
			{
				SetXScale(1f);
				m_ResetXScale = false;
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Animation '" + anim + "' doesn't exists");
		}
		m_AnimFirstFrame = true;
	}

	public void PlayAnim(string anim, GameObject go)
	{
		if (go == null || go.GetComponent<Animation>() == null)
		{
			return;
		}
		AnimationState animationState = go.GetComponent<Animation>()[anim];
		if (animationState != null)
		{
			if (go.GetComponent<Animation>().isPlaying)
			{
				go.GetComponent<Animation>().CrossFade(anim);
			}
			else
			{
				go.GetComponent<Animation>().Play(anim);
			}
		}
		else
		{
			Utility.Log(ELog.Errors, "Animation '" + anim + "' doesn't exists on " + go.name);
		}
	}

	public void PlayAnim(string anim, float speed)
	{
		if (base.GetComponent<Animation>() == null)
		{
			return;
		}
		AnimationState animationState = base.GetComponent<Animation>()[anim];
		if (animationState != null)
		{
			base.GetComponent<Animation>()[anim].speed = speed;
			if (m_Costume != null)
			{
				m_Costume.GetComponent<Animation>()[anim].speed = speed;
			}
			PlayAnim(anim);
		}
		else
		{
			Utility.Log(ELog.Errors, "Animation '" + anim + "' doesn't exists");
		}
	}

	public void StopAnim()
	{
		Utility.Log(ELog.Animation, "Stop Anim (" + m_CurrentAnimation + ")");
		base.GetComponent<Animation>().Stop();
		if (m_Costume != null)
		{
			m_Costume.GetComponent<Animation>().Stop();
		}
		foreach (GameObject miniGamesObject in m_MiniGamesObjects)
		{
			StopAnim(miniGamesObject);
		}
	}

	public void StopAnim(GameObject go)
	{
		if (go != null && go.GetComponent<Animation>() != null)
		{
			go.GetComponent<Animation>().Stop();
		}
	}

	public void RewindAnim()
	{
		base.GetComponent<Animation>().Rewind();
		if (m_Costume != null)
		{
			m_Costume.GetComponent<Animation>().Rewind();
		}
		foreach (GameObject miniGamesObject in m_MiniGamesObjects)
		{
			RewindAnim(miniGamesObject);
		}
	}

	public void RewindAnim(string anim)
	{
		base.GetComponent<Animation>().Rewind(anim);
		if (m_Costume != null)
		{
			m_Costume.GetComponent<Animation>().Rewind(anim);
		}
		foreach (GameObject miniGamesObject in m_MiniGamesObjects)
		{
			RewindAnim(anim, miniGamesObject);
		}
	}

	public void RewindAnim(GameObject go)
	{
		if (go != null && go.GetComponent<Animation>() != null)
		{
			go.GetComponent<Animation>().Rewind();
		}
	}

	public void RewindAnim(string anim, GameObject go)
	{
		if (go != null && go.GetComponent<Animation>() != null)
		{
			go.GetComponent<Animation>().Rewind(anim);
		}
	}

	public void SetAnimTime(string anim, float time)
	{
		if (base.GetComponent<Animation>() == null)
		{
			return;
		}
		AnimationState animationState = base.GetComponent<Animation>()[anim];
		if (animationState != null)
		{
			m_CurrentAnimTime = time;
			base.GetComponent<Animation>()[anim].time = time;
			if (m_Costume != null && m_Costume.GetComponent<Animation>() != null && m_Costume.GetComponent<Animation>()[anim] != null)
			{
				m_Costume.GetComponent<Animation>()[anim].time = time;
			}
			{
				foreach (GameObject miniGamesObject in m_MiniGamesObjects)
				{
					SetAnimTime(anim, time, miniGamesObject);
				}
				return;
			}
		}
		Utility.Log(ELog.Errors, "Animation '" + anim + "' doesn't exists");
	}

	public void SetAnimTime(string anim, float time, GameObject go)
	{
		if (go != null && go.GetComponent<Animation>() != null && go.GetComponent<Animation>()[anim] != null)
		{
			go.GetComponent<Animation>()[anim].time = time;
		}
	}

	public void SetWrapMode(string anim, WrapMode mode)
	{
		base.GetComponent<Animation>()[anim].wrapMode = mode;
		if (m_Costume != null)
		{
			m_Costume.GetComponent<Animation>()[anim].wrapMode = mode;
		}
	}

	protected void LoadIdleAnimationSet(ERabbid rabbid)
	{
		IdleAnimationTable idleAnimationTable = new IdleAnimationTable();
		idleAnimationTable.LoadTable(m_IdleAnims, rabbid, m_CurrentCostume);
		idleAnimationTable = null;
		Utility.FreeMem();
	}

	protected bool AddAnimationClip(string path, string animName, bool miniGame)
	{
		if (m_ToBeRemovedClips.Contains(animName))
		{
			m_ToBeRemovedClips.Remove(animName);
			if (miniGame)
			{
				if (!m_AdditionalMiniGameAnims.Contains(animName))
				{
					m_AdditionalMiniGameAnims.Add(animName);
				}
				else
				{
					Utility.Log(ELog.Errors, "Trying to add twice the same element in mini game list (to be removed): " + animName);
				}
			}
			else if (!m_AdditionalAnimationClips.Contains(animName))
			{
				m_AdditionalAnimationClips.Add(animName);
			}
			else
			{
				Utility.Log(ELog.Errors, "Trying to add twice the same element in list (to be removed): " + animName);
			}
			return false;
		}
		bool flag = false;
		if (miniGame)
		{
			if (!m_AdditionalMiniGameAnims.Contains(animName))
			{
				m_AdditionalMiniGameAnims.Add(animName);
				flag = true;
			}
		}
		else if (!m_AdditionalAnimationClips.Contains(animName))
		{
			m_AdditionalAnimationClips.Add(animName);
			flag = true;
		}
		if (flag)
		{
			AnimationClip animationClip = Utility.LoadResource<AnimationClip>("Animations/" + path + animName);
			if (!miniGame)
			{
				animationClip = (AnimationClip)Object.Instantiate(animationClip);
			}
			if (animationClip != null)
			{
				base.GetComponent<Animation>().AddClip(animationClip, animName);
				if (m_Costume != null)
				{
					m_Costume.GetComponent<Animation>().AddClip(animationClip, animName);
				}
			}
		}
		return true;
	}

	private AnimationClip LoadClip(string path)
	{
		return Utility.LoadResource<AnimationClip>(path);
	}

	protected bool AddPonctualAnimationClip(string path, string animName)
	{
		if (m_ToBeRemovedClips.Contains(animName))
		{
			m_ToBeRemovedClips.Remove(animName);
			return false;
		}
		AnimationClip animationClip = Utility.LoadResource<AnimationClip>("Animations/" + path + animName);
		if (animationClip != null)
		{
			base.GetComponent<Animation>().AddClip(animationClip, animName);
			if (m_Costume != null)
			{
				m_Costume.GetComponent<Animation>().AddClip(animationClip, animName);
			}
		}
		return true;
	}

	protected bool AddAnimationClip(string path, string animName)
	{
		return AddAnimationClip(path, animName, true);
	}

	protected bool RemoveAnimationClip(string animName)
	{
		bool flag = true;
		flag = !AnimIsPlaying(animName);
		if (flag)
		{
			if (base.GetComponent<Animation>()[animName] != null)
			{
				base.GetComponent<Animation>().RemoveClip(animName);
				if (m_Costume != null)
				{
					m_Costume.GetComponent<Animation>().RemoveClip(animName);
				}
			}
			else
			{
				Utility.Log(ELog.Errors, "Unable to remove '" + animName + "' because it doesn't exist");
			}
		}
		else
		{
			if (!m_ToBeRemovedClips.Contains(animName))
			{
				m_ToBeRemovedClips.Add(animName);
				m_RemoveTimer = 0f;
			}
			if (s_LogAnimation)
			{
				Utility.Log(ELog.Animation, "ClearAdditionalAnimationClips - Sparing: " + animName);
			}
		}
		return flag;
	}

	protected void ClearAnimationClipsToBeRemoved()
	{
		if (m_ToBeRemovedClips.Count <= 0)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < m_ToBeRemovedClips.Count; i++)
		{
			if (!flag)
			{
				break;
			}
			if (m_CurrentAnimation == m_ToBeRemovedClips[i])
			{
				flag = false;
			}
			else if (AnimIsPlaying(m_ToBeRemovedClips[i]))
			{
				flag = false;
			}
		}
		if (flag)
		{
			ClearAdditionalAnimationClips(m_ToBeRemovedClips);
		}
	}

	protected void ClearAdditionalAnimationClips()
	{
	}

	protected void ClearAdditionalAnimationClips(bool miniGame)
	{
		if (miniGame)
		{
			ClearAdditionalAnimationClips(m_AdditionalMiniGameAnims);
		}
		else
		{
			ClearAdditionalAnimationClips(m_AdditionalAnimationClips);
		}
	}

	private void ClearAdditionalAnimationClips(List<string> lst)
	{
		int num = 0;
		for (int i = 0; i < lst.Count; i++)
		{
			if (!RemoveAnimationClip(lst[i]))
			{
				num++;
			}
		}
		lst.Clear();
		Utility.FreeMem(true);
		if (s_LogAnimation)
		{
			if (lst == m_ToBeRemovedClips)
			{
				Utility.Log(ELog.Animation, "ClearAdditionalAnimationClips - Clearing m_ToBeRemovedClips " + num);
			}
			else if (lst == m_AdditionalMiniGameAnims)
			{
				Utility.Log(ELog.Animation, "ClearAdditionalAnimationClips - Clearing m_AdditionalMiniGameAnims " + num);
			}
			else if (lst == m_AdditionalAnimationClips)
			{
				Utility.Log(ELog.Animation, "ClearAdditionalAnimationClips - Clearing m_AdditionalAnimationClips " + num);
			}
		}
	}

	public void InstanciateAnimationClips()
	{
		m_BaseAnimationClips.Clear();
		IEnumerator enumerator = base.GetComponent<Animation>().GetEnumerator();
		enumerator.Reset();
		while (enumerator.MoveNext())
		{
			AnimationState animationState = (AnimationState)enumerator.Current;
			AnimationClip clip = base.GetComponent<Animation>().GetClip(animationState.name);
			m_BaseAnimationClips.Add(clip);
		}
	}

	protected void ReleaseCostume()
	{
		if (m_Costume != null)
		{
			Object.DestroyImmediate(m_Costume);
		}
		m_Costume = null;
		m_CostumeRenderer = null;
		if (m_SharedMaterial != null)
		{
			m_SharedMaterial.mainTexture = null;
		}
		if (m_Eyes != null)
		{
			m_Eyes.SetActiveRecursively(true);
		}
		Utility.FreeMem();
	}

	protected void SwapCostume(ECostume cst)
	{
		Utility.Log(ELog.Info, "SwapCostume: " + cst);
		ReleaseCostume();
		if (cst == ECostume.Count)
		{
			return;
		}
		bool flag = false;
		Texture texture = Utility.LoadResource<Texture>(Utility.GetTexPath() + "Costumes/Skins/Bunny" + cst);
		if (texture != null)
		{
			if (m_SharedMaterial != null)
			{
				m_SharedMaterial.mainTexture = texture;
				flag = true;
			}
			else
			{
				texture = null;
			}
		}
		GameObject gameObject = Utility.LoadResource<GameObject>("Costumes/" + cst);
		if (gameObject != null)
		{
			m_Costume = (GameObject)Object.Instantiate(gameObject, base.transform.position, base.transform.rotation);
			if (m_Costume != null)
			{
				m_Costume.AddComponent<FakeInGameScript>();
				for (int i = 0; i < m_BaseAnimationClips.Count; i++)
				{
					m_Costume.GetComponent<Animation>().AddClip(m_BaseAnimationClips[i], m_BaseAnimationClips[i].name);
				}
				flag = true;
				m_Costume.transform.parent = base.transform.parent;
				m_Costume.transform.localScale = base.transform.localScale;
				m_Costume.transform.localRotation = base.transform.localRotation;
				m_Costume.transform.localPosition = base.transform.localPosition;
				Utility.SetLayerRecursivly(m_Costume.transform, 9);
				m_CostumeRenderer = m_Costume.GetComponentInChildren<SkinnedMeshRenderer>();
				if (m_CostumeRenderer == null)
				{
					Utility.Log(ELog.Errors, "No Renderer is found on Costume");
				}
				else
				{
					m_CostumeRenderer.material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "Costumes/Textures/Costume" + cst);
				}
				m_CostumeResyncAnim = true;
			}
		}
		if (flag)
		{
			m_CurrentCostume = cst;
		}
	}

	protected void SyncCostumeAnim()
	{
		if (!(m_Costume == null) && animIsPlaying)
		{
			string currentAnimation = m_CurrentAnimation;
			m_Costume.GetComponent<Animation>()[currentAnimation].time = base.GetComponent<Animation>()[currentAnimation].time;
			m_Costume.GetComponent<Animation>().Play(currentAnimation);
		}
	}

	protected void ActivateEyes(bool b)
	{
		if (m_Eyes != null)
		{
			m_Eyes.SetActiveRecursively(b);
		}
	}

	protected void CreateEnvironment()
	{
		GameObject gameObject = GameObject.Find("Environment");
		if (gameObject != null)
		{
			m_EnvironmentRenderer = gameObject.GetComponentInChildren<Renderer>();
			Utility.SetLayerRecursivly(gameObject.transform, 1);
		}
	}

	protected void ReleaseEnvironment()
	{
		m_EnvironmentRenderer.material.mainTexture = null;
		Utility.FreeMem();
	}

	protected void SwapEnvironment(EEnvironment env)
	{
		Utility.Log(ELog.Info, "SwapEnvironment: " + env);
		ReleaseEnvironment();
		if (env != EEnvironment.Count)
		{
			m_CurrentEnvironment = env;
			m_EnvironmentRenderer.material.mainTexture = Utility.LoadResource<Texture2D>(Utility.GetTexPath() + "Environments/" + env);
		}
	}

	protected void HideRabbid()
	{
		int childCount = base.transform.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = base.transform.GetChild(i);
		}
		for (int i = 0; i < childCount; i++)
		{
			array[i].gameObject.SetActiveRecursively(false);
		}
		if (m_Costume != null)
		{
			m_Costume.SetActiveRecursively(false);
		}
	}

	private void InitTimeAnimTable()
	{
		m_TimeAnimTable = new Hashtable();
		m_TimeAnimTable.Add("standby_night", 10f);
		m_TimeAnimTable.Add("poke1", 2.166667f);
		m_TimeAnimTable.Add("poke2", 1.833334f);
		m_TimeAnimTable.Add("pose00", 15.93333f);
		m_TimeAnimTable.Add("pose01", 15.93333f);
		m_TimeAnimTable.Add("pose02", 15.93333f);
		m_TimeAnimTable.Add("pose03", 15.93333f);
		m_TimeAnimTable.Add("pose04", 15.93333f);
		m_TimeAnimTable.Add("pose05", 15.93333f);
		m_TimeAnimTable.Add("pose06", 15.93333f);
		m_TimeAnimTable.Add("pose07", 15.93333f);
		m_TimeAnimTable.Add("pose08", 15.93333f);
		m_TimeAnimTable.Add("pose09", 15.93333f);
		m_TimeAnimTable.Add("pose10", 15.93333f);
		m_TimeAnimTable.Add("pose11", 15.93333f);
		m_TimeAnimTable.Add("pose12", 15.93333f);
		m_TimeAnimTable.Add("pose13", 15.93333f);
		m_TimeAnimTable.Add("pose14", 15.93333f);
		m_TimeAnimTable.Add("pose15", 15.93333f);
		m_TimeAnimTable.Add("pose16", 15.93333f);
		m_TimeAnimTable.Add("pose17", 15.93333f);
		m_TimeAnimTable.Add("pose18", 15.93333f);
		m_TimeAnimTable.Add("pose19", 15.93333f);
		m_TimeAnimTable.Add("pose20", 15.93333f);
		m_TimeAnimTable.Add("pose21", 15.96667f);
		m_TimeAnimTable.Add("pose22", 15.96667f);
		m_TimeAnimTable.Add("pose23", 15.96667f);
		m_TimeAnimTable.Add("pose24", 15.96667f);
		m_TimeAnimTable.Add("pose25", 15.96667f);
		m_TimeAnimTable.Add("pose26", 15.96667f);
		m_TimeAnimTable.Add("pose27", 15.96667f);
		m_TimeAnimTable.Add("pose28", 15.96667f);
		m_TimeAnimTable.Add("pose29", 15.96667f);
		m_TimeAnimTable.Add("pose30", 15.96667f);
		m_TimeAnimTable.Add("pose31", 15.96667f);
		m_TimeAnimTable.Add("pose32", 15.96667f);
		m_TimeAnimTable.Add("steam", 5.333333f);
		m_TimeAnimTable.Add("gift01", 18.23333f);
		m_TimeAnimTable.Add("gift01_bis", 18.23333f);
		m_TimeAnimTable.Add("gift02", 16.33333f);
		m_TimeAnimTable.Add("gift02_bis", 16.33333f);
		m_TimeAnimTable.Add("Toilet_Paper_Jugglin", 8.333334f);
		m_TimeAnimTable.Add("Toilet_Paper_LoopIdle", 5.1f);
		m_TimeAnimTable.Add("Toilet_Paper_Reading", 11.3f);
		m_TimeAnimTable.Add("Toilet_Paper_Start", 3.3f);
		m_TimeAnimTable.Add("Toilet_Paper_Tast", 9.133334f);
		m_TimeAnimTable.Add("Guitar_Belly", 6.666667f);
		m_TimeAnimTable.Add("Guitar_Gag2", 9.500001f);
		m_TimeAnimTable.Add("Guitar_Gag3", 5.1f);
		m_TimeAnimTable.Add("Guitar_LoopIdle", 4f);
		m_TimeAnimTable.Add("Guitar_Start", 3f);
		m_TimeAnimTable.Add("JetPack_LoopIdle", 4f);
		m_TimeAnimTable.Add("JetPack_Push_End", 3.266667f);
		m_TimeAnimTable.Add("JetPack_Push_Loop1", 1.433334f);
		m_TimeAnimTable.Add("JetPack_Push_Loop2", 1.433334f);
		m_TimeAnimTable.Add("JetPack_Start", 2.6f);
		m_TimeAnimTable.Add("JetPack_Whirl_end", 2.866667f);
		m_TimeAnimTable.Add("JetPack_Whirl_Loop", 0.8f);
		m_TimeAnimTable.Add("JetPack_Whirl_start", 0.1f);
		m_TimeAnimTable.Add("Micro_Lick", 3.966667f);
		m_TimeAnimTable.Add("Micro_LoopIdle", 4.133334f);
		m_TimeAnimTable.Add("Micro_Shock", 9.3f);
		m_TimeAnimTable.Add("Micro_SingASong", 8.666667f);
		m_TimeAnimTable.Add("Micro_Start", 1.5f);
		m_TimeAnimTable.Add("Phone_Bomb", 10f);
		m_TimeAnimTable.Add("Phone_Green", 5.666667f);
		m_TimeAnimTable.Add("Phone_IdleLoop", 5f);
		m_TimeAnimTable.Add("Phone_Start", 3.233334f);
		m_TimeAnimTable.Add("Phone_Stunned", 4.5f);
		m_TimeAnimTable.Add("Piranha_Bite", 8.166667f);
		m_TimeAnimTable.Add("Piranha_HeadInTank", 5.166667f);
		m_TimeAnimTable.Add("Piranha_IdleLoop", 4f);
		m_TimeAnimTable.Add("Piranha_Jump", 1.8f);
		m_TimeAnimTable.Add("Piranha_Scale", 4.400001f);
		m_TimeAnimTable.Add("Piranha_Start", 3f);
		m_TimeAnimTable.Add("PopGun_Butt", 5f);
		m_TimeAnimTable.Add("PopGun_Eye", 8.1f);
		m_TimeAnimTable.Add("PopGun_LoopIdle", 5.866667f);
		m_TimeAnimTable.Add("PopGun_Shoot", 6.666667f);
		m_TimeAnimTable.Add("PopGun_Start", 1.9f);
		m_TimeAnimTable.Add("Shield_Faces", 7.666667f);
		m_TimeAnimTable.Add("Shield_LoopIdle", 5.166667f);
		m_TimeAnimTable.Add("Shield_Start", 2.166667f);
		m_TimeAnimTable.Add("Shield_Surf", 5.333333f);
		m_TimeAnimTable.Add("Shield_Turtle", 5.066667f);
		m_TimeAnimTable.Add("Swatter_BwaahEnd", 7.5f);
		m_TimeAnimTable.Add("Swatter_HappyEnd", 4.1f);
		m_TimeAnimTable.Add("Swatter_LoopIdle", 4f);
		m_TimeAnimTable.Add("Swatter_Start", 3.333333f);
		m_TimeAnimTable.Add("WCBrush_EarBrushing", 6f);
		m_TimeAnimTable.Add("WCBrush_EyeLashBrushing", 5f);
		m_TimeAnimTable.Add("WCBrush_LoopIdle", 2.333333f);
		m_TimeAnimTable.Add("WCBrush_ShowerBrush", 6.666667f);
		m_TimeAnimTable.Add("WCBrush_Start", 3.2f);
		m_TimeAnimTable.Add("WCBrush_TeethBrushing", 5f);
		m_TimeAnimTable.Add("Bow_Start", 4.9f);
		m_TimeAnimTable.Add("Bow_Idle", 4.95f);
		m_TimeAnimTable.Add("Bow_Action1", 10f);
		m_TimeAnimTable.Add("Bow_Action2", 7.5f);
		m_TimeAnimTable.Add("GhettoBlaster_Start", 4f);
		m_TimeAnimTable.Add("GhettoBlaster_Idle", 7.033f);
		m_TimeAnimTable.Add("GhettoBlaster_Action_1", 10f);
		m_TimeAnimTable.Add("GhettoBlaster_Action_2", 10.13f);
		m_TimeAnimTable.Add("Nunchaku_Start", 2.16f);
		m_TimeAnimTable.Add("Nunchaku_Idle", 6.666f);
		m_TimeAnimTable.Add("Nunchaku_Action1", 11.2f);
		m_TimeAnimTable.Add("Nunchaku_Action2", 10f);
		m_TimeAnimTable.Add("RugbyBall_Start", 5f);
		m_TimeAnimTable.Add("RugbyBall_Idle", 5f);
		m_TimeAnimTable.Add("RugbyBall_Action1", 10f);
		m_TimeAnimTable.Add("RugbyBall_Action2", 10f);
		m_TimeAnimTable.Add("SausagePolice_Start", 3.66f);
		m_TimeAnimTable.Add("SausagePolice_Idle", 5f);
		m_TimeAnimTable.Add("SausagePolice_Action_1", 8.83f);
		m_TimeAnimTable.Add("SausagePolice_Action_2", 10.83f);
		m_TimeAnimTable.Add("BellTap", 3.066667f);
		m_TimeAnimTable.Add("ComePlay", 4.533334f);
		m_TimeAnimTable.Add("HideSeek", 3.9f);
		m_TimeAnimTable.Add("IchyAss", 3.633333f);
		m_TimeAnimTable.Add("Idle_Dance", 2.666667f);
		m_TimeAnimTable.Add("Idle_jump", 1.9f);
		m_TimeAnimTable.Add("Idle_Look", 5f);
		m_TimeAnimTable.Add("Idle_Scratch", 5.333333f);
		m_TimeAnimTable.Add("Idle_Taunt", 2.166667f);
		m_TimeAnimTable.Add("Idle_Turn", 2.666667f);
		m_TimeAnimTable.Add("Idle_YawnFart", 8.333334f);
		m_TimeAnimTable.Add("knock", 5.933333f);
		m_TimeAnimTable.Add("stand_middle", 10f);
		m_TimeAnimTable.Add("stand_part01", 6.033334f);
		m_TimeAnimTable.Add("stand_part02", 10f);
		m_TimeAnimTable.Add("standby_crazy", 10.66667f);
		m_TimeAnimTable.Add("standby_cri_de_pres", 20.16667f);
		m_TimeAnimTable.Add("Tired", 2f);
		m_TimeAnimTable.Add("TurnInPlace", 3f);
		m_TimeAnimTable.Add("Yarning", 2.4f);
		m_TimeAnimTable.Add("blow_getup", 5.633334f);
		m_TimeAnimTable.Add("blow_loop", 1.1f);
		m_TimeAnimTable.Add("blow_start", 1.566667f);
		m_TimeAnimTable.Add("burp", 6.666667f);
		m_TimeAnimTable.Add("choke", 8.8f);
		m_TimeAnimTable.Add("get up", 2.666667f);
		m_TimeAnimTable.Add("screen_frontbouncing", 0.8333334f);
		m_TimeAnimTable.Add("wall bounce down", 0.3f);
		m_TimeAnimTable.Add("dance_all", 37f);
		m_TimeAnimTable.Add("ears1", 1.633333f);
		m_TimeAnimTable.Add("ears2", 1.166667f);
		m_TimeAnimTable.Add("look_down", 1.133333f);
		m_TimeAnimTable.Add("screen_stand_up", 3.766667f);
		m_TimeAnimTable.Add("step_back", 1.766667f);
		m_TimeAnimTable.Add("basic_sign", 8.666667f);
		m_TimeAnimTable.Add("screen_rotate", 2f);
		m_TimeAnimTable.Add("run_acceleration", 2.566667f);
		m_TimeAnimTable.Add("run_deceleration", 4.166667f);
		m_TimeAnimTable.Add("run_running_fast", 2.733334f);
		m_TimeAnimTable.Add("string_left", 1f);
		m_TimeAnimTable.Add("string_right", 13f / 15f);
		m_TimeAnimTable.Add("string_up", 0.8333335f);
		m_TimeAnimTable.Add("tickle_end", 1.3f);
		m_TimeAnimTable.Add("tickle_loop", 1.2f);
		m_TimeAnimTable.Add("tickle_start", 1.166667f);
		m_TimeAnimTable.Add("turn", 5.5f);
		m_TimeAnimTable.Add("fall left", 2.733334f);
		m_TimeAnimTable.Add("fall_back", 3.4f);
		m_TimeAnimTable.Add("fall_bouncing_right", 4.733334f);
		m_TimeAnimTable.Add("screen_frontfalling", 3.066667f);
		m_TimeAnimTable.Add("start wall bounce left", 0.3333333f);
		m_TimeAnimTable.Add("start wall bounce up", 0.3f);
		m_TimeAnimTable.Add("start_bounce_back", 2f / 3f);
		m_TimeAnimTable.Add("wall bounce left", 0.4000001f);
		m_TimeAnimTable.Add("wall bounce right", 0.3666667f);
		m_TimeAnimTable.Add("wall bounce up", 0.3333334f);
		m_TimeAnimTable.Add("yell", 7.033334f);
	}
}
