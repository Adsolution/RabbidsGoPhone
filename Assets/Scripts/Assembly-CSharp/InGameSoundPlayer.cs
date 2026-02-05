using System.Collections;
using UnityEngine;

public class InGameSoundPlayer : MonoBehaviour
{
	public AudioClip[] m_Clips;

	public AudioClip[] m_RabbidClips = new AudioClip[9];

	public AudioClip[] m_BwahClips = new AudioClip[16];

	private AudioSource[] m_Audio;

	private bool m_PlaySounds = true;

	private int m_MaximumSimultaneousSounds;

	private Hashtable m_ClipsHashTable = new Hashtable();

	private void Start()
	{
		m_PlaySounds = PlayerPrefs.GetInt("_sound_enable") == 1;
		m_Audio = base.gameObject.GetComponents<AudioSource>();
		if (m_MaximumSimultaneousSounds != 0)
		{
			m_MaximumSimultaneousSounds = 0;
		}
		Utility.Log(ELog.Info, "AudioSource Component Count: " + m_Audio.Length);
		Utility.Log(ELog.Info, "Play Sounds: " + m_PlaySounds);
	}

	private void OnApplicationPause(bool pause)
	{
		Pause(pause);
	}

	public void PlaySound(InGameScript.ESound snd, float volume, bool loop)
	{
		PlaySound(snd.ToString(), volume, loop);
	}

	public void PlaySound(string sndName, float volume, bool loop)
	{
		if (m_PlaySounds)
		{
			AudioClip clip = GetClip(sndName);
			if (PlayClip(clip, volume, loop))
			{
				Utility.Log(ELog.Sound, string.Empty + sndName + " - " + clip.length);
			}
			else
			{
				Utility.Log(ELog.Errors, "Sound - Failed to play sound (" + sndName + ")");
			}
		}
	}

	public void Stop()
	{
		base.GetComponent<AudioSource>().Stop();
		if (m_Audio != null && m_PlaySounds)
		{
			int i = 0;
			for (int num = m_Audio.Length; i < num; i++)
			{
				if (m_Audio[i].isPlaying)
				{
					m_Audio[i].Stop();
				}
				m_Audio[i].clip = null;
			}
		}
		Utility.Log(ELog.Sound, "Stop");
	}

	public void Pause(bool pauseResume)
	{
		if (m_Audio == null || !m_PlaySounds)
		{
			return;
		}
		int i = 0;
		int num = m_Audio.Length;
		if (pauseResume)
		{
			base.GetComponent<AudioSource>().Pause();
			for (; i < num; i++)
			{
				m_Audio[i].Pause();
			}
		}
		else
		{
			base.GetComponent<AudioSource>().Play();
			for (; i < num; i++)
			{
				m_Audio[i].Play();
			}
		}
	}

	public void ClearClips()
	{
		Utility.Log(ELog.Sound, "Clear Audio Clips: " + m_ClipsHashTable.Count);
		if (m_ClipsHashTable.Count > 0)
		{
			m_ClipsHashTable.Clear();
		}
	}

	public void LoadClip(string path, string sndName)
	{
		if (m_PlaySounds && !m_ClipsHashTable.Contains(sndName))
		{
			AudioClip audioClip = null;
			audioClip = Utility.LoadResource<AudioClip>("SFX/" + path + sndName);
			if (audioClip != null)
			{
				m_ClipsHashTable.Add(sndName, audioClip);
			}
		}
	}

	private AudioClip GetClip(string snd)
	{
		if (m_ClipsHashTable.Contains(snd))
		{
			return (AudioClip)m_ClipsHashTable[snd];
		}
		InGameScript.ESound idleSoundEnum = InGameScript.GetIdleSoundEnum(snd);
		if ((int)idleSoundEnum < m_Clips.Length)
		{
			return m_Clips[(int)idleSoundEnum];
		}
		return null;
	}

	private bool PlayClip(AudioClip clip, float volume, bool loop)
	{
		if (!m_PlaySounds)
		{
			return true;
		}
		if (clip == null)
		{
			Utility.Log(ELog.Errors, "Failed to play sound ??? - clip is null");
			return false;
		}
		if (m_Audio != null)
		{
			int i = 0;
			int num = m_Audio.Length;
			if (num <= 0)
			{
				Utility.Log(ELog.Errors, "There is not enough AudioSource component");
				return false;
			}
			for (; i < num && m_Audio[i].clip != null; i++)
			{
			}
			if (i < num)
			{
				if (loop)
				{
					m_Audio[i].volume = volume;
					m_Audio[i].loop = loop;
					m_Audio[i].clip = clip;
					m_Audio[i].Play();
				}
				else
				{
					m_Audio[i].PlayOneShot(clip, volume);
				}
				if (i + 1 > m_MaximumSimultaneousSounds)
				{
					m_MaximumSimultaneousSounds = i + 1;
					Utility.Log(ELog.Info, "MaximumSimultaneousSounds: " + m_MaximumSimultaneousSounds);
				}
			}
			else if (loop)
			{
				m_Audio[0].volume = volume;
				m_Audio[0].loop = loop;
				m_Audio[0].clip = clip;
				m_Audio[0].Play();
			}
			else
			{
				m_Audio[0].PlayOneShot(clip, volume);
			}
			return true;
		}
		return false;
	}

	public AudioClip GetClip(InGameScript.ERabbidSound snd)
	{
		if ((int)snd >= m_RabbidClips.Length || !m_RabbidClips[(int)snd])
		{
			return null;
		}
		return m_RabbidClips[(int)snd];
	}

	public AudioClip GetClip(InGameScript.EBwahSound snd)
	{
		if ((int)snd >= m_BwahClips.Length || !m_BwahClips[(int)snd])
		{
			return null;
		}
		return m_BwahClips[(int)snd];
	}

	public void PlaySound(InGameScript.ERabbidSound snd, float volume, bool loop)
	{
		if (m_PlaySounds && snd < InGameScript.ERabbidSound.Count)
		{
			AudioClip clip = GetClip(snd);
			if (PlayClip(clip, volume, loop))
			{
				Utility.Log(ELog.Sound, string.Empty + snd.ToString() + " (" + (int)snd + ") - " + clip.length);
			}
			else
			{
				Utility.Log(ELog.Errors, "Sound - Failed to play sound (" + snd.ToString() + ")");
			}
		}
	}

	public void PlaySound(InGameScript.EBwahSound snd, float volume, bool loop)
	{
		if (m_PlaySounds && snd < InGameScript.EBwahSound.Count)
		{
			AudioClip clip = GetClip(snd);
			if (PlayClip(clip, volume, loop))
			{
				Utility.Log(ELog.Sound, string.Empty + snd.ToString() + " (" + (int)snd + ") - " + clip.length);
			}
			else
			{
				Utility.Log(ELog.Errors, "Sound - Failed to play sound (" + snd.ToString() + ")");
			}
		}
	}
}
