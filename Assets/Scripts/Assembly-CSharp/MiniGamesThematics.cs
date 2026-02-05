using System.Collections.Generic;
using System.IO;
using Mono.Xml;
using UnityEngine;

public class MiniGamesThematics : SmallXmlParser.IContentHandler
{
	private TextAsset m_ThematicsFile;

	private List<InGameScript.EState>[] m_Thematics;

	private Rabbid.ECostume m_ParsingCostume = Rabbid.ECostume.Count;

	public MiniGamesThematics()
	{
		m_ThematicsFile = Utility.LoadResource<TextAsset>("Config/MiniGamesThematics");
		m_Thematics = new List<InGameScript.EState>[12];
		for (int i = 0; i < 12; i++)
		{
			m_Thematics[i] = new List<InGameScript.EState>();
		}
		Parse();
		Utility.FreeMem();
	}

	public bool IsMiniGameAvailable(Rabbid.ECostume cst, InGameScript.EState state)
	{
		return m_Thematics[(int)cst].Contains(state);
	}

	private void Parse()
	{
		SmallXmlParser smallXmlParser = new SmallXmlParser();
		smallXmlParser.Parse(new StringReader(m_ThematicsFile.text), this);
	}

	public void OnStartParsing(SmallXmlParser parser)
	{
	}

	public void OnEndParsing(SmallXmlParser parser)
	{
	}

	public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
	{
		if (name == "Costume")
		{
			m_ParsingCostume = Rabbid.GetCostumeEnum(attrs.GetValue(0));
		}
		else
		{
			if (!(name == "MiniGame") || m_ParsingCostume == Rabbid.ECostume.Count)
			{
				return;
			}
			InGameScript.EState stateEnum = InGameScript.GetStateEnum(attrs.GetValue(0));
			if (stateEnum == InGameScript.EState.Count)
			{
				return;
			}
			if (m_ParsingCostume == Rabbid.ECostume.Naked)
			{
				for (int i = 0; i < 12; i++)
				{
					m_Thematics[i].Add(stateEnum);
				}
			}
			else
			{
				m_Thematics[(int)m_ParsingCostume].Add(stateEnum);
			}
		}
	}

	public void OnEndElement(string name)
	{
		if (name == "Costume")
		{
			m_ParsingCostume = Rabbid.ECostume.Count;
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
