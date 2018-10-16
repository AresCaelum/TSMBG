using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class LoadMusicButton : MonoBehaviour
{
    static public event Action LoadMusic = delegate { };
    public List<TextMeshProUGUI> descriptionTexts;
    public List<TextMeshProUGUI> buttonTexts;

    public GameObject Panel;
    public GameObject Panel3;
    // public Button StatButton;
    public static int Index;
    public GameObject Upbutton;
    public GameObject Downbutton;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public static String[] SongProperties = { "Song_name", "Song_length", "Song_score", "Attempts", "Cleared" };

    public static int increment = 0;

    // Use this for initialization
    void Start()
    {
        Upbutton.GetComponent<Button>().onClick.AddListener(DecrementList);
        Downbutton.GetComponent<Button>().onClick.AddListener(IncrementList);

        Button1.GetComponent<Button>().onClick.AddListener(delegate { ShowDiscription(increment); });
        Button2.GetComponent<Button>().onClick.AddListener(delegate { ShowDiscription(increment + 1); });
        Button3.GetComponent<Button>().onClick.AddListener(delegate { ShowDiscription(increment + 2); });
        Button4.GetComponent<Button>().onClick.AddListener(delegate { ShowDiscription(increment + 3); });

        Button1.GetComponent<Button>().onClick.AddListener(delegate { SelectSong(increment); });
        Button2.GetComponent<Button>().onClick.AddListener(delegate { SelectSong(increment + 1); });
        Button3.GetComponent<Button>().onClick.AddListener(delegate { SelectSong(increment + 2); });
        Button4.GetComponent<Button>().onClick.AddListener(delegate { SelectSong(increment + 3); });
        UpdateMusic();

        AudioHolder.SongFinishedLoading += UpdateSongLength;
    }

    private void OnDestroy()
    {
        AudioHolder.SongFinishedLoading -= UpdateSongLength;
        MusicManager.Exit();
    }

    void Update()
    {
        UpdateMusic();
    }

    // Update is called once per frame
    public void UpdateMusic()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonTexts[i].text = Path.GetFileNameWithoutExtension(MusicManager.GetSong((increment + i).ToString() + SongProperties[0]));
        }

    }

    void UpdateSongLength()
    {
        MusicManager.addSong(Index.ToString() + SongProperties[1], AudioManager.GetClip().length.ToString());
        ShowDiscription(Index);
    }

    void IncrementList()
    {
        if (increment + 4 < MusicManager.NumberofSongs())
        {
            increment++;
        }
    }

    void DecrementList()
    {
        increment--;
        if (increment < 0)
            increment = 0;
    }

    void ShowDiscription(int ID)
    {
        if (ID < MusicManager.NumberofSongs())
        {
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    descriptionTexts[i].text = Path.GetFileNameWithoutExtension(MusicManager.GetSong((ID).ToString() + SongProperties[i]));
                }
                else
                {
                    descriptionTexts[i].text = MusicManager.GetSong((ID).ToString() + SongProperties[i]);
                    //Debug.Log(ID);
                }
            }
        }
    }

    public void SelectSong(int index)
    {

        if (index < MusicManager.NumberofSongs())
        {
            Index = index;
            Debug.Log(MusicManager.GetSong((index).ToString() + SongProperties[0]) + " - " + index);
            AudioManager.LoadClip(MusicManager.GetSong((index).ToString() + SongProperties[0]));
            LoadMusic.Invoke();
        }
    }
}