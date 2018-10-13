using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadMusicButton : MonoBehaviour
{
    static public event Action LoadMusic = delegate { };
    public GameObject Panel;
    public GameObject Panel3;
    // public Button StatButton;
    public int Index;
    public GameObject Upbutton;
    public GameObject Downbutton;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    private String[] Names = { "Song_name", "Song_length", "Song_score", "Attempts" };


    public static int increment = 0;
    //List<>
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
    }

    void Update()
    {
        UpdateMusic();
    }

    private void NewMethod()
    {

    }

    // Update is called once per frame
    public void UpdateMusic()
    {
        for (int i = 0; i < 4; i++)
        {

            // Debug.Log(MusicManager.GetSong((increment + i).ToString()) + (increment + i).ToString());
            string yousuck = MusicManager.GetSong((increment + i).ToString() + "Song_name");
            Panel.transform.GetChild(i).GetComponentInChildren<Text>().text = yousuck;

        }

    }

    void IncrementList()
    {
        int newposition = increment + 1;
        Debug.Log(MusicManager.NumberofSongs().ToString());
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

        for (int i = 0; i < 4; i++)
        {
            string yousuck = MusicManager.GetSong((ID).ToString() + Names[i]);
            Panel3.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = yousuck;

            //newposition++;

        }

    }
    //  onb

    public void Enter()
    {
        PlayerPrefs.GetString("Things", "0000");
    }

    private void OnDisable()
    {
        MusicManager.Exit();
        Debug.Log("I FEL IN HERE");
    }

    public void SelectSong(int index)
    {
        Debug.Log(MusicManager.GetSong((index).ToString() + "Song_name") + " - " + index);
        AudioHolder.instance.LoadAudio(MusicManager.GetSong((index).ToString() + "Song_name"));
        LoadMusic.Invoke();
        MusicManager.addSong((index).ToString() + "Song_length", AudioHolder.instance.GetAudioClip().name.ToString());
    }

}