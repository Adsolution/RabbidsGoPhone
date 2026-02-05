using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mono.Xml;
using UnityEngine;

public class Localization : SmallXmlParser.IContentHandler
{
	private string[] m_LocalizedTexts = new string[163];

	private Hashtable m_CharTable = new Hashtable();

	private static Localization s_Localization = null;

	private static SystemLanguage s_Language = SystemLanguage.English;

	private static Hashtable s_EnumTable = new Hashtable();

	private static string s_WaitText = string.Empty;

	public Localization()
	{
		s_Language = GetLanguageInternal();
		string text = "Localization/" + s_Language;
		s_EnumTable.Clear();
		for (ELoc eLoc = ELoc.Yes; eLoc < ELoc.Count; eLoc++)
		{
			s_EnumTable[eLoc.ToString()] = eLoc;
			m_LocalizedTexts[(int)eLoc] = "Undefined";
		}
		TextAsset textAsset = Utility.LoadResource<TextAsset>(text);
		if (textAsset == null)
		{
			textAsset = Utility.LoadResource<TextAsset>("Localization/" + SystemLanguage.English);
			if (textAsset == null)
			{
				Utility.Log(ELog.Errors, "Unable to find: " + text + " neither english");
				return;
			}
		}
		else
		{
			Utility.Log(ELog.Info, "Current language correctly loaded: " + text);
		}
		SmallXmlParser smallXmlParser = new SmallXmlParser();
		smallXmlParser.Parse(new StringReader(textAsset.text), this);
		FormatTexts();
	}

	private string GetLocalizedTextIntern(ELoc textId)
	{
		if (m_LocalizedTexts != null)
		{
			return m_LocalizedTexts[(int)textId];
		}
		return string.Empty;
	}

	private static ELoc GetLocEnum(string str)
	{
		if (str != null && s_EnumTable.Contains(str))
		{
			return (ELoc)(int)s_EnumTable[str];
		}
		return ELoc.Count;
	}

	private void FormatTexts()
	{
		bool flag = false;
		SystemLanguage systemLanguage = s_Language;
		if (systemLanguage == SystemLanguage.Japanese || systemLanguage == SystemLanguage.Korean || systemLanguage == SystemLanguage.Chinese)
		{
			flag = true;
		}
		if (flag && LoadUniqueChars())
		{
			TransformStrings();
		}
	}

	private void SaveUniqueChars()
	{
		List<char> list = new List<char>();
		string text = string.Empty;
		int num = 0;
		int i;
		for (i = 0; i < 163; i++)
		{
			string text2 = m_LocalizedTexts[i];
			foreach (char c in text2)
			{
				if (c > 'ÿ' && !list.Contains(c))
				{
					list.Add(c);
					if (c > num)
					{
						num = c;
					}
				}
			}
		}
		Utility.Log(ELog.Info, "maxIdx: " + num + " - c: " + (char)num);
		Utility.Log(ELog.Info, "uniqueCharCount: " + list.Count);
		list.Sort();
		i = 256;
		foreach (char item in list)
		{
			text = text + item + "|";
			text = text + (int)item + "|";
			string text3 = text;
			int num2 = item;
			text = text3 + "h" + num2.ToString("X") + "|";
			text = text + i + "|";
			text = text + "h" + i.ToString("X");
			text += "\n";
			i++;
		}
		Utility.SaveFile(Application.dataPath + "/Resources/Localization", s_Language.ToString() + "CharTable.txt", Encoding.UTF8.GetBytes(text));
	}

	private bool LoadUniqueChars()
	{
		string text = Application.dataPath + "/Resources/Localization/";
		text = text + s_Language.ToString() + "CharTable.txt";
		byte[] array = Utility.LoadFile(text);
		if (array == null)
		{
			Utility.Log(ELog.Errors, "Unable to load: " + text);
			return false;
		}
		string text2 = Encoding.UTF8.GetString(array);
		string[] array2 = text2.Split('\n');
		string[] array3 = array2;
		foreach (string text3 in array3)
		{
			if (text3.Length > 3)
			{
				string[] array4 = text3.Split('|');
				if (array4.Length > 3)
				{
					int num = int.Parse(array4[1]);
					int num2 = int.Parse(array4[3]);
					m_CharTable.Add(num, num2);
				}
			}
		}
		return true;
	}

	private void TransformStrings()
	{
		for (int i = 0; i < 163; i++)
		{
			string text = string.Empty;
			for (int j = 0; j < m_LocalizedTexts[i].Length; j++)
			{
				char c = m_LocalizedTexts[i][j];
				if (c > 'ÿ')
				{
					int num = (int)m_CharTable[(int)c];
					text += (char)num;
				}
				else
				{
					text += c;
				}
			}
			m_LocalizedTexts[i] = text;
		}
	}

	private SystemLanguage GetLanguageInternal()
	{
		return Application.systemLanguage;
	}

	public static SystemLanguage GetLanguage()
	{
		return s_Language;
	}

	public static string GetLocalizedText(string textId)
	{
		ELoc locEnum = GetLocEnum(textId);
		if (locEnum != ELoc.Count)
		{
			return GetLocalizedText(locEnum);
		}
		Utility.Log(ELog.Errors, "Unknown text ID : " + textId);
		return string.Empty;
	}

	public static string GetLocalizedText(ELoc textId)
	{
		if (s_Localization == null)
		{
			s_Localization = new Localization();
		}
		return s_Localization.GetLocalizedTextIntern(textId);
	}

	public static void GenerateWaitText()
	{
		string text = ELoc.PleaseWait.ToString();
		text = text + "Alt" + Random.Range(1, 10);
		if (s_Localization == null)
		{
			s_Localization = new Localization();
		}
		s_WaitText = GetLocalizedText(text);
		if (s_WaitText == string.Empty)
		{
			s_WaitText = GetLocalizedText(ELoc.PleaseWait);
		}
	}

	public static string GetWaitText()
	{
		return s_WaitText;
	}

	public void OnStartParsing(SmallXmlParser parser)
	{
	}

	public void OnEndParsing(SmallXmlParser parser)
	{
	}

	public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
	{
		if (name == "Text")
		{
			string value = attrs.GetValue(1);
			ELoc locEnum = GetLocEnum(attrs.GetValue(0));
			if (locEnum != ELoc.Count && value != null)
			{
				value = value.Replace("…", "...");
				value = value.Replace("§ ", "\n");
				value = value.Replace("§", "\n");
				value = value.Replace("’", "'");
				m_LocalizedTexts[(int)locEnum] = value;
			}
			else if (value == null)
			{
				Utility.Log(ELog.Errors, "Some elements were not readable due to a Null pointer");
			}
			else if (locEnum == ELoc.Count)
			{
				Utility.Log(ELog.Errors, "txt == ELoc.Count -> " + attrs.GetValue(0));
			}
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
