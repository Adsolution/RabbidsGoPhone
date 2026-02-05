using UnityEngine;

public class VideoPlayerAndroid : MonoBehaviour
{
	public static VideoPlayerAndroid s_Instance;

	public static VideoPlayerAndroid Instance
	{
		get
		{
			if (s_Instance == null)
			{
				Utility.Log(ELog.Errors, "VideoPlayerAndroid:: Call of instance before awake()");
			}
			return s_Instance;
		}
	}

	private void Awake()
	{
		s_Instance = this;
	}

	private void Start()
	{
		AndroidJNI.AttachCurrentThread();
	}

	private void Update()
	{
	}

	public void playMovie(string uri, bool autoPlay)
	{
		using (new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaClass("com.exkee.Plugins.VideoPlayer"))
			{
				androidJavaObject.CallStatic("PlayMovie", uri, autoPlay);
			}
		}
	}

	public void playMovie(string uri)
	{
		using (new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaClass("com.exkee.Plugins.VideoPlayer"))
			{
				androidJavaObject.CallStatic("PlayMovie", uri);
			}
		}
	}

	public bool isPlaying()
	{
		using (new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaClass("com.exkee.Plugins.VideoPlayer"))
			{
				return androidJavaObject.CallStatic<bool>("IsPlaying", new object[0]);
			}
		}
	}

	public void stopMovie()
	{
		using (new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaClass("comm.exkee.Rabbids.VideoPlayer"))
			{
				androidJavaObject.CallStatic("StopMovie");
			}
		}
	}
}
