using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager: MonoBehaviour
{
    public List<GameObject> ToHideOnFailure = new List<GameObject>();
    public static bool SaveStats = true;
    Transform windoCanvas;
    public Transform WindowCanvas
    {
        get
        {
            if(windoCanvas == null)
            {
                windoCanvas = GameObject.Find("PlayerControlsCanvas").transform;
            }
            return windoCanvas;
        }
    }
    public static WindowManager Instance = null;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    string BlackBackgroundPath = "Window_BlackBackground";
    string CompleteWindowPath = "Window_StageComplete";
    string FailureWindowPath = "Window_StageFailed";

    GameObject BlackBackground;
    GameObject Window;

    bool Failure = false;

    void CreateBlackBackground()
    {
        GameObject toSpawn = Resources.Load<GameObject>(BlackBackgroundPath);
        BlackBackground = GameObject.Instantiate(toSpawn, WindowCanvas);
    }

    public void CreateCompleteWindow()
    {
        if (Failure)
            return;

        CreateBlackBackground();
        GameObject toSpawn = Resources.Load<GameObject>(CompleteWindowPath);
        WindowComplete window = GameObject.Instantiate(toSpawn, WindowCanvas).GetComponent<WindowComplete>();
        if(window != null)
        {
            window.SetScore(GetScore());
            SaveScore();
        }
    }

    public void CreateFailureWindow()
    {
        Failure = true;
        SaveAttempts();
        ObjectSpawner audioPlayer = GameObject.Find("RythmnSpawner").GetComponent<ObjectSpawner>();
        audioPlayer.Fail();
        //audioPlayer.Stop();
        CreateBlackBackground();
        GameObject toSpawn = Resources.Load<GameObject>(FailureWindowPath);
        Window = GameObject.Instantiate(toSpawn, WindowCanvas);
    }

    public void SaveScore()
    {
        if (SaveStats)
        {
            int toSaveScore = PlayerPrefs.GetInt(AudioManager.SongName + SongSavedProperties.Score.ToString(), 0);
            int toSaveCleared =  PlayerPrefs.GetInt(AudioManager.SongName + SongSavedProperties.Cleared.ToString(), 0);

            PlayerPrefs.SetInt(AudioManager.SongName + SongSavedProperties.Score.ToString(), toSaveScore);
            PlayerPrefs.SetInt(AudioManager.SongName + SongSavedProperties.Cleared.ToString(), toSaveCleared + 1);
        }
    }

    public void SaveAttempts()
    {
        if (SaveStats)
        {
            int toSaveAttempts = PlayerPrefs.GetInt(AudioManager.SongName + SongSavedProperties.Failed.ToString(), 0);
            PlayerPrefs.SetInt(AudioManager.SongName + SongSavedProperties.Failed.ToString(), toSaveAttempts + 1);
        }
    }

    public void HideOnFilure()
    {
        Destroy(Window);
        Destroy(BlackBackground);
        foreach(GameObject obj in ToHideOnFailure)
        {
            obj.SetActive(false);
        }
    }

    int GetScore()
    {
        AudioSource player = GameObject.Find("RythmnSpawner").GetComponent<AudioSource>();
        int score = (int)(player.clip.length * ((float)Player.Health / (float)Player.MaxHealth)) * 100;

        return score;
    }
}