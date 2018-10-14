using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager: MonoBehaviour
{
    public List<GameObject> ToHideOnFailure = new List<GameObject>();

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
        ObjectSpawner audioPlayer = GameObject.Find("RythmnSpawner").GetComponent<ObjectSpawner>();
        audioPlayer.Fail();
        //audioPlayer.Stop();
        CreateBlackBackground();
        GameObject toSpawn = Resources.Load<GameObject>(FailureWindowPath);
        Window = GameObject.Instantiate(toSpawn, WindowCanvas);
    }

    public void SaveScore()
    {
        string toSaveScore = LoadMusicButton.Index.ToString() + LoadMusicButton.SongProperties[2];
        string toSaveCleared = LoadMusicButton.Index.ToString() + LoadMusicButton.SongProperties[4];
        string currentCleared = MusicManager.GetSong(toSaveCleared);
        if(currentCleared == null || currentCleared.Equals(""))
        {
            currentCleared = "0";
        }

        MusicManager.addSong(toSaveCleared, (int.Parse(currentCleared) +1).ToString());
        MusicManager.addSong(toSaveScore, GetScore().ToString());
    }

    public void SaveAttempts()
    {
        string toSaveAttempts = LoadMusicButton.Index.ToString() + LoadMusicButton.SongProperties[3];
        string currentAttempts = MusicManager.GetSong(toSaveAttempts);

        MusicManager.addSong(toSaveAttempts, (int.Parse(currentAttempts) +1).ToString());
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