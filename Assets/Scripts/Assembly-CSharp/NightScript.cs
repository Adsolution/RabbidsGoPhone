using UnityEngine;

public class NightScript : MonoBehaviour
{
	public GameObject eyesObject;

	public GameObject bodyObject;

	public GameObject mainlyObject;

	public GameObject environment;

	public InGameScript gameScript;

	private string m_AnimName = "Take 001";

	private float m_PauseTime;

	private void Start()
	{
		AnimationEvent animationEvent = new AnimationEvent();
		animationEvent.functionName = "PlayNightSound";
		animationEvent.time = 0f;
		base.GetComponent<Animation>()[m_AnimName].clip.AddEvent(animationEvent);
		animationEvent.functionName = "PlayWaddleSound";
		animationEvent.time = 4.45f;
		base.GetComponent<Animation>()[m_AnimName].clip.AddEvent(animationEvent);
		if (eyesObject != null)
		{
			eyesObject.SetActiveRecursively(false);
		}
		else
		{
			Utility.Log(ELog.Errors, "eyesObject == null");
		}
	}

	private void Update()
	{
		if (InGameScript.GetState() == InGameScript.EState.Night && base.GetComponent<Animation>()[m_AnimName].time >= base.GetComponent<Animation>()[m_AnimName].length - 0.1f)
		{
			SwitchOffNight();
		}
	}

	private void PlayNightSound()
	{
		if (gameScript != null)
		{
			gameScript.PlayNightSound();
		}
	}

	private void PlayWaddleSound()
	{
	}

	public void EnableNight()
	{
		gameScript.StopAnim();
		gameScript.PlayAnim("standby_night");
		eyesObject.SetActiveRecursively(true);
		bodyObject.SetActiveRecursively(false);
		mainlyObject.SetActiveRecursively(false);
		base.GetComponent<Animation>().Rewind(m_AnimName);
		base.GetComponent<Animation>()[m_AnimName].time = 4.3333335f;
		base.GetComponent<Animation>().Play(m_AnimName);
		if (environment != null)
		{
			environment.SetActiveRecursively(false);
		}
	}

	public void UpdateNight()
	{
		if (!base.GetComponent<Animation>().isPlaying)
		{
			base.GetComponent<Animation>().Rewind(m_AnimName);
			base.GetComponent<Animation>().Play(m_AnimName);
		}
	}

	public void DisableNight()
	{
		eyesObject.SetActiveRecursively(false);
		base.GetComponent<Animation>().Stop(m_AnimName);
		mainlyObject.SetActiveRecursively(true);
		bodyObject.SetActiveRecursively(true);
		if (environment != null)
		{
			environment.SetActiveRecursively(true);
		}
	}

	public void SwitchOffNight()
	{
		eyesObject.SetActiveRecursively(false);
		if (base.GetComponent<Animation>()[m_AnimName].time >= 4.45f)
		{
			gameScript.SetAnimTime("standby_night", base.GetComponent<Animation>()[m_AnimName].time - 4.45f);
		}
		else
		{
			gameScript.SetAnimTime("standby_night", 0f);
		}
		base.GetComponent<Animation>().Stop(m_AnimName);
		mainlyObject.SetActiveRecursively(true);
		bodyObject.SetActiveRecursively(true);
		if (environment != null)
		{
			environment.SetActiveRecursively(true);
		}
		InGameScript.SetState(InGameScript.EState.Idle);
	}

	public void PauseNight(bool pause)
	{
		if (pause)
		{
			m_PauseTime = base.GetComponent<Animation>()[m_AnimName].time;
			base.GetComponent<Animation>().Stop();
		}
		else
		{
			base.GetComponent<Animation>()[m_AnimName].time = m_PauseTime;
			base.GetComponent<Animation>().Play(m_AnimName);
		}
	}
}
