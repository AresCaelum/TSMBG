using System;
using System.Collections;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{

    public static AudioHolder instance;
    public static String lastPlayed = "LastPlayed";
    private String audioLocation = "";
    [SerializeField]
    private AudioClip currentClip;
    public static event Action SongFinishedLoading = delegate { };

    private int Lives = 3;

    public static bool CanPlay()
    {
        return instance.Lives > 0;
    }

    public static void ResetLives()
    {
        instance.Lives = 3;
    }

    public static int GetLives()
    {
        return instance.Lives;
    }

    public static void UseLife()
    {
        instance.Lives--;
    }

    private Coroutine loadRoutine;
    public AudioClip GetAudioClip()
    {
        return currentClip;
    }

    public void LoadAudio(string location)
    {
        if(location.Equals(""))
        {
            return;
        }
        if (loadRoutine != null)
            StopCoroutine(loadRoutine);

        WWW clipAddress = new WWW(location);
        currentClip = clipAddress.GetAudioClip(false, false);
        if (currentClip != null)
        {
            audioLocation = location;
            PlayerPrefs.SetString(lastPlayed, location);
            PlayerPrefs.Save();
            loadRoutine = StartCoroutine(MusicStatus());
        }
    }

    IEnumerator MusicStatus()
    {
        while (currentClip.loadState != AudioDataLoadState.Loaded)
        {
            yield return null;
        }
        SongFinishedLoading.Invoke();
        loadRoutine = null;
    }

    public bool IsFinishedLoading()
    {
        if (currentClip == null)
        {
            return false;
        }
        return currentClip.loadState == AudioDataLoadState.Loaded;
    }

    public AudioDataLoadState GetStatus()
    {
        if (currentClip == null)
        {
            return AudioDataLoadState.Failed;
        }

        return currentClip.loadState;
    }

    // Use this for initialization
    void Start()
    {
        if (instance != null)
            return;
        AdManager.Initialize("2850450");
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SelectFile.UsedFile += HandleLoad;

    }

    void HandleLoad()
    {
        LoadAudio(SelectFile.newPath);
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

}
