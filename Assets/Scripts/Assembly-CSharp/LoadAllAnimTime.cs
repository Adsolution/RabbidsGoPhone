using System.Collections.Generic;
using UnityEngine;

public class LoadAllAnimTime : MonoBehaviour
{
	public string m_PathAnimDirectory = "/Resources/Animations";

	public List<string> m_AnimsName = new List<string>();

	private void Start()
	{
		GameObject gameObject = GameObject.Find("InGameRabbid");
		Rabbid component = gameObject.GetComponent<Rabbid>();
		string text = string.Empty;
		string[] array = new string[3] { "standby_night", "poke1", "poke2" };
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			float length = component.GetComponent<Animation>().GetClip(text2).length;
			string text3 = text;
			text = text3 + "m_TimeAnimTable.Add(\"" + text2 + "\", " + length + "f);\n";
		}
		text += "\n//Dynamic anim \n";
		Object[] array3 = Resources.LoadAll("Animations", typeof(AnimationClip));
		Debug.Log("animClipSize = " + array3.Length);
		Object[] array4 = array3;
		for (int j = 0; j < array4.Length; j++)
		{
			AnimationClip animationClip = (AnimationClip)array4[j];
			string text3 = text;
			text = text3 + "m_TimeAnimTable.Add(\"" + animationClip.name + "\", " + animationClip.length + "f);\n";
		}
		Debug.Log(text);
	}
}
