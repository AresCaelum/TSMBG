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
        
        string Name = MusicManager.NumberofSongs().ToString() + LoadMusicButton.SongProperties[0];
        string songLocation = SelectFile.newPath;

        string length = MusicManager.NumberofSongs().ToString() + LoadMusicButton.SongProperties[1];
        string songLength = (0).ToString();
        if (AudioManager.GetClip() != null)
        {
            songLength = AudioManager.GetClip().length.ToString();
        }

        string score = MusicManager.NumberofSongs().ToString() + LoadMusicButton.SongProperties[2];
        string songScore = "0";

        string Attempts = MusicManager.NumberofSongs().ToString() + LoadMusicButton.SongProperties[3];
        string songAttempts = "0";

        string Cleared = MusicManager.NumberofSongs().ToString() + LoadMusicButton.SongProperties[4];
        string songCleared = "0";

        MusicManager.addSong(Name, songLocation);
        MusicManager.addSong(score, songScore);
        MusicManager.addSong(length, songLength);
        MusicManager.addSong(Attempts, songAttempts);
        MusicManager.addSong(Cleared, songCleared);
        increment++;
        MusicManager.IncrementNumberofSongs();

    }
}
