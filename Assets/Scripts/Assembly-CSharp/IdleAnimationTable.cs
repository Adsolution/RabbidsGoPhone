using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using UnityEngine;

public class IdleAnimationTable : SmallXmlParser.IContentHandler
{
	private TextAsset m_IdleAnimationFile;

	private Rabbid.ERabbid m_CurrentRabbid = Rabbid.ERabbid.Count;

	private Rabbid.ERabbid m_RabbidToParse = Rabbid.ERabbid.Count;

	private Rabbid.ECostume m_CurrentCostume = Rabbid.ECostume.Count;

	private Rabbid.ECostume m_CostumeToParse = Rabbid.ECostume.Count;

	private List<string> m_IdleAnims;

	public IdleAnimationTable()
	{
		m_IdleAnimationFile = Utility.LoadResource<TextAsset>("Config/IdleAnimationTable");
		if (m_IdleAnimationFile != null)
		{
			Utility.Log(ELog.Info, "IdleAnimationTable correctly loaded");
		}
	}

	public void LoadTable(List<string> idleAnims, Rabbid.ERabbid rabbid, Rabbid.ECostume cst)
	{
		if (idleAnims != null && rabbid != Rabbid.ERabbid.Count && cst != Rabbid.ECostume.Count)
		{
			SmallXmlParser smallXmlParser = new SmallXmlParser();
			m_RabbidToParse = rabbid;
			m_CostumeToParse = cst;
			m_IdleAnims = idleAnims;
			m_IdleAnims.Clear();
			smallXmlParser.Parse(new StringReader(m_IdleAnimationFile.text), this);
		}
	}

	public void OnStartParsing(SmallXmlParser parser)
	{
	}

	public void OnEndParsing(SmallXmlParser parser)
	{
	}

	public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
	{
		switch (name)
		{
		case "Rabbid":
			m_CurrentRabbid = Rabbid.GetRabbidEnum(attrs.GetValue(0));
			if (m_CurrentRabbid == Rabbid.ERabbid.Count)
			{
				Utility.Log(ELog.Errors, "IdleAnimation::Invalid rabbid -> " + attrs.GetValue(0));
			}
			break;
		case "Costume":
			m_CurrentCostume = Rabbid.GetCostumeEnum(attrs.GetValue(0));
			if (m_CurrentCostume == Rabbid.ECostume.Count)
			{
				Utility.Log(ELog.Errors, "IdleAnimation::Invalid rabbid -> " + attrs.GetValue(0));
			}
			break;
		case "Anim":
			if ((m_CostumeToParse == m_CurrentCostume || m_CurrentCostume == Rabbid.ECostume.Naked) && m_RabbidToParse == m_CurrentRabbid)
			{
				m_IdleAnims.Add(attrs.GetValue(0));
			}
			break;
		}
	}

	public void OnEndElement(string name)
	{
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
