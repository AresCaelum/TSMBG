using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class AddSong : MonoBehaviour {

    // Use this for initialization
    public GameObject Content;
    public GameObject Button;
    public GameObject FileBrowser;
    private Button butt;
    int increment = 0;
	void Start () {
        //butt = Instantiate(Button, Content.transform).GetComponent<Button>();
        Button.GetComponent<Button>().onClick.AddListener(LoadFileBrowser);
        SelectFile.UsedFile += AddSongToManager;
        //sButton.transform.GetChild(0).GetComponent<Text>().text = "CHANGE";
        //PlayerPrefs.DeleteAll();
        MusicManager.Start();
    }
	
	// Update is called once per frame
	void Update () {
        
       
    }

    void LoadFileBrowser()
    {
        Instantiate(FileBrowser, Vector3.zero, Quaternion.identity);
    }

   void AddSongToManager()
    {
        
        
        string Name = MusicManager.NumberofSongs().ToString() + "Song_name";
        string Value1 = SelectFile.newPath;

        string score = MusicManager.NumberofSongs().ToString() + "Song_score";
        string Value2 = "0";
        

        string length = MusicManager.NumberofSongs().ToString() + "Song_length";
        string Value3 = AudioHolder.instance.GetAudioClip().length.ToString();

        string Attempts = MusicManager.NumberofSongs().ToString() + "Attempts";
        string Value4 = "0";

       
        MusicManager.addSong(Name, Value1);
        MusicManager.addSong(score, Value2);
        MusicManager.addSong(length, Value3);
        MusicManager.addSong(Attempts, Value4);
        increment++;
        MusicManager.IncrementNumberofSongs();

    }
}
