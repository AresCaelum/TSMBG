using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager: MonoBehaviour
{
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

    GameObject SpawnedBlackBackground = null;
    GameObject SpawnedWindow = null;

    void CreateBlackBackground()
    {
        GameObject toSpawn = Resources.Load<GameObject>(BlackBackgroundPath);
        GameObject blackBackground = GameObject.Instantiate(toSpawn, WindowCanvas);
    }

    public void CreateCompleteWindow()
    {
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
        CreateBlackBackground();
        GameObject toSpawn = Resources.Load<GameObject>(FailureWindowPath);
        WindowComplete window = GameObject.Instantiate(toSpawn, WindowCanvas).GetComponent<WindowComplete>();
    }

    public void SaveScore()
    {
        string toSaveScore = LoadMusicButton.Index.ToString() + LoadMusicButton.SongProperties[2];
        string toSaveCleared = LoadMusicButton.Index.ToString() + LoadMusicButton.SongProperties[4];
        string currentCleared = MusicManager.GetSong(toSaveCleared);

        MusicManager.addSong(toSaveCleared, (int.Parse(currentCleared) +1).ToString());
        MusicManager.addSong(toSaveScore, GetScore().ToString());
    }

    int GetScore()
    {
        AudioSource player = GameObject.Find("RythmnSpawner").GetComponent<AudioSource>();
        int score = (int)player.clip.length;

        return score;
    }
}