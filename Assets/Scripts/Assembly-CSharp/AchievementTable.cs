using System;
using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using UnityEngine;

public class AchievementTable : SmallXmlParser.IContentHandler
{
	public enum EGoody
	{
		MiniGame = 0,
		Costume = 1,
		Environment = 2,
		ARPose = 3,
		AREighty = 4,
		ARPicture = 5,
		Wallpaper = 6,
		YoutubeVideo = 7,
		Stripe = 8,
		Pack = 9,
		Achievement = 10,
		Count = 11
	}

	private TextAsset m_AchievementFile;

	private int[] m_ActionCount = new int[19];

	private EProductID[] m_ProductDependency = new EProductID[19];

	private List<InGameScript.EState>[] m_MiniGamesPerPack = new List<InGameScript.EState>[2];

	private EProductID[] m_Costumes = new EProductID[12];

	private EProductID[] m_Environments = new EProductID[8];

	private EProductID[] m_Wallpapers = new EProductID[34];

	private EProductID[] m_ARPoses = new EProductID[24];

	private EProductID[] m_AREighties = new EProductID[1];

	private EProductID[] m_Stripes = new EProductID[8];

	private List<Rabbid.ECostume> m_VisibleCostumes = new List<Rabbid.ECostume>();

	private List<Rabbid.EEnvironment> m_VisibleEnvironments = new List<Rabbid.EEnvironment>();

	private List<OutWorld2D.EWallpaper> m_VisibleWallpapers = new List<OutWorld2D.EWallpaper>();

	private List<PhotoRoom.EARPose> m_VisibleARPoses = new List<PhotoRoom.EARPose>();

	private List<PhotoRoom.EAREighty> m_VisibleAREighties = new List<PhotoRoom.EAREighty>();

	private List<OutWorld2D.EStripe> m_VisibleStripes = new List<OutWorld2D.EStripe>();

	private List<Rabbid.ECostume> m_JustUnlockedCostumes = new List<Rabbid.ECostume>();

	private List<Rabbid.EEnvironment> m_JustUnlockedEnvironments = new List<Rabbid.EEnvironment>();

	private List<OutWorld2D.EWallpaper> m_JustUnlockedWallpapers = new List<OutWorld2D.EWallpaper>();

	private List<PhotoRoom.EARPose> m_JustUnlockedARPoses = new List<PhotoRoom.EARPose>();

	private List<PhotoRoom.EAREighty> m_JustUnlockedAREighties = new List<PhotoRoom.EAREighty>();

	private List<OutWorld2D.EStripe> m_JustUnlockedStripes = new List<OutWorld2D.EStripe>();

	private bool m_FirstPass = true;

	private bool m_IsParsingBaseUnlock;

	private InGameScript.EState m_CurrentState = InGameScript.EState.Count;

	private int m_CurrentAction = -1;

	private bool m_IsCurrentActionUnlocked;

	private EProductID m_CurrentPack = EProductID.None;

	private bool m_ConsiderPack;

	private InGameScript.EState m_StateToUnlock = InGameScript.EState.Count;

	private int m_ActionToUnlock = -1;

	public AchievementTable()
	{
		m_AchievementFile = Utility.LoadResource<TextAsset>("Config/AchievementTable");
		if (m_AchievementFile == null)
		{
			Utility.Log(ELog.Errors, "Unable to find: Config/AchievementTable.xml");
		}
		else
		{
			Utility.Log(ELog.Info, "Achievement Table correctly loaded");
		}
		for (InGameScript.EState eState = InGameScript.EState.ClownBox; eState < InGameScript.EState.Count; eState++)
		{
			m_ProductDependency[InGameScript.GetMiniGameIndex(eState)] = EProductID.Free;
		}
		for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.AvailableCount; eProductID++)
		{
			m_MiniGamesPerPack[(int)eProductID] = new List<InGameScript.EState>();
		}
		LoadTable();
	}

	public static bool IsGoodyNew(EGoody goody, string name)
	{
		return !PlayerPrefs.HasKey(goody.ToString() + "_" + name + "_NoLongerNew");
	}

	public static bool MarkGoodyKey(EGoody goody, string name)
	{
		if (IsGoodyNew(goody, name))
		{
			GamePlayerPrefs.SetInt(goody.ToString() + "_" + name + "_NoLongerNew", 1);
			return true;
		}
		return false;
	}

	public int GetActionCount(InGameScript.EState state)
	{
		return m_ActionCount[InGameScript.GetMiniGameIndex(state)];
	}

	public bool IsMiniGameAvailable(InGameScript.EState state)
	{
		EProductID eProductID = m_ProductDependency[InGameScript.GetMiniGameIndex(state)];
		if (eProductID != EProductID.Free && !IsProductFree(eProductID))
		{
			return Utility.IsUnlockedProduct(eProductID);
		}
		return true;
	}

	public bool IsProductDependent(InGameScript.EState state)
	{
		EProductID eProductID = m_ProductDependency[InGameScript.GetMiniGameIndex(state)];
		return eProductID < EProductID.AvailableCount && !IsProductFree(eProductID);
	}

	public static bool IsProductFree(EProductID pid)
	{
		bool result = false;
		if (pid == EProductID.historypack || pid == EProductID.moustachpack)
		{
			result = true;
		}
		return result;
	}

	public void UnlockEnvironment(Rabbid.EEnvironment env)
	{
		m_Environments[(int)env] = EProductID.Free;
	}

	public void UnlockWallpaper(OutWorld2D.EWallpaper wallpaper)
	{
		m_Wallpapers[(int)wallpaper] = EProductID.Free;
	}

	public void UnlockStateAction(InGameScript.EState state, int actionID)
	{
		m_CurrentState = InGameScript.EState.Count;
		m_CurrentAction = -1;
		m_StateToUnlock = state;
		m_ActionToUnlock = actionID;
		m_IsParsingBaseUnlock = false;
		m_JustUnlockedCostumes.Clear();
		m_JustUnlockedWallpapers.Clear();
		m_JustUnlockedARPoses.Clear();
		m_JustUnlockedAREighties.Clear();
		m_JustUnlockedEnvironments.Clear();
		m_JustUnlockedStripes.Clear();
		Parse();
		RegenerateLists();
	}

	public void UnlockPack()
	{
		m_JustUnlockedCostumes.Clear();
		m_JustUnlockedWallpapers.Clear();
		m_JustUnlockedARPoses.Clear();
		m_JustUnlockedAREighties.Clear();
		m_JustUnlockedEnvironments.Clear();
		m_JustUnlockedStripes.Clear();
		m_ConsiderPack = false;
		m_CurrentPack = EProductID.None;
		Parse();
		RegenerateLists();
		Utility.FreeMem();
	}

	public EProductID GetProductID(Rabbid.ECostume costume)
	{
		return m_Costumes[(int)costume];
	}

	public EProductID GetProductID(Rabbid.EEnvironment environment)
	{
		return m_Environments[(int)environment];
	}

	public EProductID GetProductID(PhotoRoom.EARPose pose)
	{
		return m_ARPoses[(int)pose];
	}

	public EProductID GetProductID(PhotoRoom.EAREighty eighty)
	{
		return m_AREighties[(int)eighty];
	}

	public EProductID GetProductID(OutWorld2D.EWallpaper wallpaper)
	{
		return m_Wallpapers[(int)wallpaper];
	}

	public EProductID GetProductID(OutWorld2D.EStripe stripe)
	{
		return m_Stripes[(int)stripe];
	}

	public List<Rabbid.ECostume> GetCostumeList()
	{
		m_VisibleCostumes.Clear();
		for (int i = 0; i < 12; i++)
		{
			if (m_Costumes[i] != EProductID.None)
			{
				m_VisibleCostumes.Add((Rabbid.ECostume)i);
			}
		}
		return m_VisibleCostumes;
	}

	public List<Rabbid.EEnvironment> GetEnvironmentList()
	{
		m_VisibleEnvironments.Clear();
		for (int i = 0; i < 8; i++)
		{
			if (m_Environments[i] != EProductID.None)
			{
				m_VisibleEnvironments.Add((Rabbid.EEnvironment)i);
			}
		}
		return m_VisibleEnvironments;
	}

	public List<PhotoRoom.EARPose> GetARPoseList()
	{
		m_VisibleARPoses.Clear();
		for (int i = 0; i < 24; i++)
		{
			if (m_ARPoses[i] != EProductID.None)
			{
				m_VisibleARPoses.Add((PhotoRoom.EARPose)i);
			}
		}
		return m_VisibleARPoses;
	}

	public List<PhotoRoom.EAREighty> GetAREightyList()
	{
		m_VisibleAREighties.Clear();
		for (int i = 0; i < 1; i++)
		{
			if (m_AREighties[i] != EProductID.None)
			{
				m_VisibleAREighties.Add((PhotoRoom.EAREighty)i);
			}
		}
		return m_VisibleAREighties;
	}

	public List<OutWorld2D.EWallpaper> GetWallpaperList()
	{
		m_VisibleWallpapers.Clear();
		for (int i = 0; i < 34; i++)
		{
			if (m_Wallpapers[i] != EProductID.None)
			{
				m_VisibleWallpapers.Add((OutWorld2D.EWallpaper)i);
			}
		}
		return m_VisibleWallpapers;
	}

	public List<OutWorld2D.EStripe> GetStripeList()
	{
		m_VisibleStripes.Clear();
		for (int i = 0; i < 8; i++)
		{
			if (m_Stripes[i] != EProductID.None)
			{
				m_VisibleStripes.Add((OutWorld2D.EStripe)i);
			}
		}
		return m_VisibleStripes;
	}

	public List<Rabbid.ECostume> GetJustUnlockedCostumes()
	{
		return m_JustUnlockedCostumes;
	}

	public List<Rabbid.EEnvironment> GetJustUnlockedEnvironments()
	{
		return m_JustUnlockedEnvironments;
	}

	public List<OutWorld2D.EWallpaper> GetJustUnlockedWallpapers()
	{
		return m_JustUnlockedWallpapers;
	}

	public List<PhotoRoom.EARPose> GetJustUnlockedARPoses()
	{
		return m_JustUnlockedARPoses;
	}

	public List<PhotoRoom.EAREighty> GetJustUnlockedAREighties()
	{
		return m_JustUnlockedAREighties;
	}

	public List<OutWorld2D.EStripe> GetJustUnlockedStripes()
	{
		return m_JustUnlockedStripes;
	}

	public List<InGameScript.EState> GetPackMiniGame(EProductID pack)
	{
		return m_MiniGamesPerPack[(int)pack];
	}

	public EProductID GetMiniGamePack(InGameScript.EState state)
	{
		for (EProductID eProductID = EProductID.historypack; eProductID < EProductID.AvailableCount; eProductID++)
		{
			if (m_MiniGamesPerPack[(int)eProductID].Contains(state))
			{
				return eProductID;
			}
		}
		return EProductID.Free;
	}

	public static EProductID GetProductIDFromPack(EPack pack)
	{
		EProductID result = EProductID.Free;
		switch (pack)
		{
		case EPack.History:
			result = EProductID.historypack;
			break;
		case EPack.Moustach:
			result = EProductID.moustachpack;
			break;
		}
		return result;
	}

	public static EPack GetPackFromProductID(EProductID product)
	{
		EPack result = EPack.Count;
		switch (product)
		{
		case EProductID.historypack:
			result = EPack.History;
			break;
		case EProductID.moustachpack:
			result = EPack.Moustach;
			break;
		case EProductID.Free:
			result = EPack.Base;
			break;
		}
		return result;
	}

	public static ELoc GetTitleFromPack(EPack pack)
	{
		ELoc result = ELoc.Count;
		switch (pack)
		{
		case EPack.History:
			result = ELoc.historypack_Title;
			break;
		case EPack.Moustach:
			result = ELoc.moustachpack_Title;
			break;
		}
		return result;
	}

	public static Color GetPackColor(EPack pack)
	{
		Color result = Color.white;
		switch (pack)
		{
		case EPack.Base:
			result = Color.blue;
			break;
		case EPack.XK:
			result = Color.green;
			break;
		case EPack.History:
			result = Color.green / 2f + Color.red;
			break;
		case EPack.Moustach:
			result = Color.red;
			break;
		}
		return result;
	}

	public static int ApproxEltCount(int eltCount)
	{
		if (eltCount <= 10)
		{
			return 5;
		}
		return 20;
	}

	public static Texture2D LoadPackTexture(EPack pack, int eltCount)
	{
		Texture2D texture2D = null;
		eltCount = ApproxEltCount(eltCount);
		texture2D = Utility.LoadTextureResource<Texture2D>("PackThumbnails/PackThumbnail_" + eltCount);
		if (texture2D != null)
		{
			texture2D = Utility.AddColor(texture2D, GetPackColor(pack));
		}
		return texture2D;
	}

	private void Parse()
	{
		SmallXmlParser smallXmlParser = new SmallXmlParser();
		smallXmlParser.Parse(new StringReader(m_AchievementFile.text), this);
	}

	public void LoadTable()
	{
		for (int i = 0; i < 19; i++)
		{
			m_ActionCount[i] = 0;
		}
		for (int i = 0; i < 12; i++)
		{
			m_Costumes[i] = EProductID.None;
		}
		for (int i = 0; i < 8; i++)
		{
			m_Environments[i] = EProductID.None;
		}
		for (int i = 0; i < 34; i++)
		{
			m_Wallpapers[i] = EProductID.None;
		}
		for (int i = 0; i < 24; i++)
		{
			m_ARPoses[i] = EProductID.None;
		}
		for (int i = 0; i < 1; i++)
		{
			m_AREighties[i] = EProductID.None;
		}
		for (int i = 0; i < 8; i++)
		{
			m_Stripes[i] = EProductID.None;
		}
		if (PlayerPrefs.HasKey("_new_wallpaper"))
		{
			int num = PlayerPrefs.GetInt("_new_wallpaper");
			if (num > 0)
			{
				UnlockWallpaper(OutWorld2D.EWallpaper.Wallpaper1);
			}
			if (num > 1)
			{
				UnlockWallpaper(OutWorld2D.EWallpaper.Wallpaper2);
			}
			if (num > 2)
			{
				UnlockWallpaper(OutWorld2D.EWallpaper.Wallpaper3);
			}
			if (num > 3)
			{
				UnlockWallpaper(OutWorld2D.EWallpaper.Wallpaper5);
			}
		}
		if (PlayerPrefs.HasKey("_new_environment"))
		{
			int num2 = PlayerPrefs.GetInt("_new_environment");
			if (num2 > 0)
			{
				UnlockEnvironment(Rabbid.EEnvironment.Boy);
			}
			if (num2 > 1)
			{
				UnlockEnvironment(Rabbid.EEnvironment.Girl);
			}
		}
		m_FirstPass = true;
		Parse();
		m_FirstPass = false;
		m_JustUnlockedCostumes.Clear();
		m_JustUnlockedEnvironments.Clear();
		m_JustUnlockedWallpapers.Clear();
		m_JustUnlockedARPoses.Clear();
		m_JustUnlockedAREighties.Clear();
		m_JustUnlockedStripes.Clear();
		RegenerateLists();
		Utility.FreeMem();
	}

	private void FirstPass(string name, SmallXmlParser.IAttrList attrs)
	{
		switch (name)
		{
		case "BaseUnlock":
			m_IsParsingBaseUnlock = true;
			m_CurrentPack = EProductID.Free;
			return;
		case "Pack":
			m_ConsiderPack = false;
			m_CurrentPack = Utility.GetProductIDEnum(attrs.GetValue(0));
			if (m_CurrentPack != EProductID.None)
			{
				m_ConsiderPack = Utility.IsUnlockedProduct(m_CurrentPack);
			}
			else
			{
				Utility.Log(ELog.Errors, "m_CurrentPack == EProductID.None: " + attrs.GetValue(0));
			}
			return;
		case "MiniGame":
			m_IsParsingBaseUnlock = false;
			m_IsCurrentActionUnlocked = false;
			m_CurrentState = InGameScript.GetStateEnum(attrs.GetValue(0));
			if (m_CurrentState != InGameScript.EState.Count)
			{
				int miniGameIndex = InGameScript.GetMiniGameIndex(m_CurrentState);
				m_ActionCount[miniGameIndex] = Convert.ToInt32(attrs.GetValue(1));
				if (m_CurrentPack < EProductID.AvailableCount)
				{
					m_ProductDependency[miniGameIndex] = m_CurrentPack;
					m_MiniGamesPerPack[(int)m_CurrentPack].Add(m_CurrentState);
				}
			}
			else
			{
				Utility.Log(ELog.Errors, "AchievementTable::Invalid state -> " + attrs.GetValue(0));
			}
			return;
		case "Action":
		{
			m_CurrentAction = Convert.ToInt32(attrs.GetValue(0));
			string key = m_CurrentState.ToString() + "_Action_" + m_CurrentAction + "_Locked";
			if (PlayerPrefs.HasKey(key))
			{
				m_IsCurrentActionUnlocked = PlayerPrefs.GetInt(key) == 0;
			}
			return;
		}
		}
		if (!m_IsCurrentActionUnlocked && !m_IsParsingBaseUnlock && !m_ConsiderPack)
		{
			return;
		}
		switch (name)
		{
		case "Costume":
		{
			Rabbid.ECostume costumeEnum = Rabbid.GetCostumeEnum(attrs.GetValue(0));
			if (costumeEnum != Rabbid.ECostume.Count)
			{
				m_Costumes[(int)costumeEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced Rabbid.ECostume Enum: " + attrs.GetValue(0));
			}
			break;
		}
		case "Environment":
		{
			Rabbid.EEnvironment environmentEnum = Rabbid.GetEnvironmentEnum(attrs.GetValue(0));
			if (environmentEnum != Rabbid.EEnvironment.Count)
			{
				m_Environments[(int)environmentEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced Rabbid.EEnvironment Enum: " + attrs.GetValue(0));
			}
			break;
		}
		case "Wallpaper":
		{
			OutWorld2D.EWallpaper wallpaperEnum = OutWorld2D.GetWallpaperEnum(attrs.GetValue(0));
			if (wallpaperEnum != OutWorld2D.EWallpaper.Count)
			{
				m_Wallpapers[(int)wallpaperEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced OutWorld2D.EWallpaper Enum: " + attrs.GetValue(0));
			}
			break;
		}
		case "ARPose":
		{
			PhotoRoom.EARPose aRPoseEnum = PhotoRoom.GetARPoseEnum(attrs.GetValue(0));
			if (aRPoseEnum != PhotoRoom.EARPose.Count)
			{
				m_ARPoses[(int)aRPoseEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced InGameScript.EARPose Enum: " + attrs.GetValue(0));
			}
			break;
		}
		case "AREighty":
		{
			PhotoRoom.EAREighty aREightyEnum = PhotoRoom.GetAREightyEnum(attrs.GetValue(0));
			if (aREightyEnum != PhotoRoom.EAREighty.Count)
			{
				m_AREighties[(int)aREightyEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced InGameScript.EAREighty Enum: " + attrs.GetValue(0));
			}
			break;
		}
		case "Stripe":
		{
			OutWorld2D.EStripe stripeEnum = OutWorld2D.GetStripeEnum(attrs.GetValue(0));
			if (stripeEnum != OutWorld2D.EStripe.Count)
			{
				m_Stripes[(int)stripeEnum] = m_CurrentPack;
			}
			else
			{
				Utility.Log(ELog.Errors, "Unreferenced OutWorld2D.EStripe Enum: " + attrs.GetValue(0));
			}
			break;
		}
		}
	}

	private void SecondPass(string name, SmallXmlParser.IAttrList attrs)
	{
		switch (name)
		{
		case "BaseUnlock":
			m_IsParsingBaseUnlock = true;
			m_CurrentPack = EProductID.Free;
			return;
		case "Pack":
			m_ConsiderPack = false;
			m_CurrentPack = Utility.GetProductIDEnum(attrs.GetValue(0));
			if (m_CurrentPack != EProductID.None)
			{
				m_ConsiderPack = Utility.IsUnlockedProduct(m_CurrentPack);
			}
			else
			{
				Utility.Log(ELog.Errors, "m_CurrentPack == EProductID.None: " + attrs.GetValue(0));
			}
			return;
		case "MiniGame":
			m_IsParsingBaseUnlock = false;
			m_IsCurrentActionUnlocked = false;
			m_CurrentState = InGameScript.GetStateEnum(attrs.GetValue(0));
			return;
		case "Action":
			m_CurrentAction = Convert.ToInt32(attrs.GetValue(0));
			return;
		}
		if ((m_IsParsingBaseUnlock || m_CurrentState != m_StateToUnlock || m_CurrentAction != m_ActionToUnlock) && !m_ConsiderPack)
		{
			return;
		}
		switch (name)
		{
		case "Costume":
		{
			Rabbid.ECostume costumeEnum = Rabbid.GetCostumeEnum(attrs.GetValue(0));
			if (costumeEnum != Rabbid.ECostume.Count && m_Costumes[(int)costumeEnum] == EProductID.None)
			{
				m_JustUnlockedCostumes.Add(costumeEnum);
				m_Costumes[(int)costumeEnum] = m_CurrentPack;
			}
			break;
		}
		case "Environment":
		{
			Rabbid.EEnvironment environmentEnum = Rabbid.GetEnvironmentEnum(attrs.GetValue(0));
			if (environmentEnum != Rabbid.EEnvironment.Count && m_Environments[(int)environmentEnum] == EProductID.None)
			{
				m_JustUnlockedEnvironments.Add(environmentEnum);
				m_Environments[(int)environmentEnum] = m_CurrentPack;
			}
			break;
		}
		case "Wallpaper":
		{
			OutWorld2D.EWallpaper wallpaperEnum = OutWorld2D.GetWallpaperEnum(attrs.GetValue(0));
			if (wallpaperEnum != OutWorld2D.EWallpaper.Count && m_Wallpapers[(int)wallpaperEnum] == EProductID.None)
			{
				m_JustUnlockedWallpapers.Add(wallpaperEnum);
				m_Wallpapers[(int)wallpaperEnum] = m_CurrentPack;
			}
			break;
		}
		case "ARPose":
		{
			PhotoRoom.EARPose aRPoseEnum = PhotoRoom.GetARPoseEnum(attrs.GetValue(0));
			if (aRPoseEnum != PhotoRoom.EARPose.Count && m_ARPoses[(int)aRPoseEnum] == EProductID.None)
			{
				m_JustUnlockedARPoses.Add(aRPoseEnum);
				m_ARPoses[(int)aRPoseEnum] = m_CurrentPack;
			}
			break;
		}
		case "AREighty":
		{
			PhotoRoom.EAREighty aREightyEnum = PhotoRoom.GetAREightyEnum(attrs.GetValue(0));
			if (aREightyEnum != PhotoRoom.EAREighty.Count && m_AREighties[(int)aREightyEnum] == EProductID.None)
			{
				m_JustUnlockedAREighties.Add(aREightyEnum);
				m_AREighties[(int)aREightyEnum] = m_CurrentPack;
			}
			break;
		}
		case "Stripe":
		{
			OutWorld2D.EStripe stripeEnum = OutWorld2D.GetStripeEnum(attrs.GetValue(0));
			if (stripeEnum != OutWorld2D.EStripe.Count && m_Stripes[(int)stripeEnum] == EProductID.None)
			{
				m_JustUnlockedStripes.Add(stripeEnum);
				m_Stripes[(int)stripeEnum] = m_CurrentPack;
			}
			break;
		}
		}
	}

	private void RegenerateLists()
	{
		GetWallpaperList();
		GetCostumeList();
		GetEnvironmentList();
		GetARPoseList();
		GetAREightyList();
		GetStripeList();
	}

	public void OnStartParsing(SmallXmlParser parser)
	{
	}

	public void OnEndParsing(SmallXmlParser parser)
	{
	}

	public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
	{
		if (m_FirstPass)
		{
			FirstPass(name, attrs);
		}
		else
		{
			SecondPass(name, attrs);
		}
	}

	public void OnEndElement(string name)
	{
		if (name == "BaseUnlock" || name == "Pack")
		{
			m_ConsiderPack = false;
			m_CurrentPack = EProductID.Free;
		}
	}

	public void OnProcessingInstruction(string name, string text)
	{
	}

	public void OnChars(string text)
	{
	}

	public void OnIgnorableWhitespace(string text)
	{
	}
}
