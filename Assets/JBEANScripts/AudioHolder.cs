using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{

    public static AudioHolder instance;
    [SerializeField]
    private AudioClip currentClip;
    private static string testLoad = "D:/Downloads/kimi.mp3";

    public AudioClip GetAudioClip()
    {
        return currentClip;
    }

    public void LoadAudio(string location)
    {
        WWW clipAddress = new WWW(location);
        currentClip = clipAddress.GetAudioClip(false, false);
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

    public void TestLoad()
    {
        LoadAudio(testLoad);
    }

    // Use this for initialization
    void Start()
    {
        if (instance != null)
            return;
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
