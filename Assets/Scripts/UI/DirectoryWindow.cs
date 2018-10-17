using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DirectoryWindow : MonoBehaviour
{
    [SerializeField]
    GameObject ToSpawn;
    [SerializeField]
    Transform ContentsBox;
    [SerializeField]
    TextMeshProUGUI currentDirectoryText;
    [SerializeField]
    Button BackButton;
    [SerializeField]
    Transform MusicContent;
    [SerializeField]
    GameObject SongButton;

    List<DirectoryButton> CurrentDirectories = new List<DirectoryButton>();
    public static List<string> MusicPaths = new List<string>();
    public static string MusicFolderPath;

    int currentDirectoryCount = 0;

    private void Awake()
    {
        if (MusicFolderPath.Length == 0)
            MusicFolderPath = Application.persistentDataPath;
        currentDirectoryText.text = MusicFolderPath;
        Refresh();
    }

    private void OnEnable()
    {
        currentDirectoryText.text = MusicFolderPath;
    }

    private void Refresh()
    {
        currentDirectoryText.text = MusicFolderPath;
        for (int i = CurrentDirectories.Count - 1; i >= 0; i--)
        {
            Destroy(CurrentDirectories[i].gameObject);
        }
        CurrentDirectories.Clear();

        DirectoryInfo dir = new DirectoryInfo(MusicFolderPath);
        DirectoryInfo[] info = dir.GetDirectories();

        foreach (DirectoryInfo file in info)
        {
            AddNewButton(file);
        }
    }

    private void AddNewButton(DirectoryInfo file)
    {
        DirectoryButton button = Instantiate(ToSpawn, ContentsBox).GetComponent<DirectoryButton>();
        button.FolderName = Path.GetFileName(file.FullName);
        button.Directory = file.FullName;
        button.Initialize();
        string dir = file.FullName;
        button.GetComponent<Button>().onClick.AddListener(() => { MusicFolderPath = dir; Refresh(); });
        CurrentDirectories.Add(button);
    }

    public void Back_Button()
    {
        DirectoryInfo dir = Directory.GetParent(MusicFolderPath);
        MusicFolderPath = dir.FullName;
        Refresh();
    }

    public void Cancel_Button()
    {
        gameObject.SetActive(false);
    }

    public void Accept_Button()
    {
        AudioManager.ClearClip();
        LoadSongsInPath(MusicFolderPath, SongButton, MusicContent);
        gameObject.SetActive(false);
    }

    public static void LoadSongsInPath(string path, GameObject objToSpawn, Transform parentOfObjToSpawn)
    {
        string LastPlayed = PlayerPrefs.GetString("LastPlayedSong", "").Replace("file:///", "");
        Debug.Log(LastPlayed);
        List<Transform> buttonsToClear = new List<Transform>();
        foreach (Transform child in parentOfObjToSpawn)
        {
            buttonsToClear.Add(child);
        }
        for (int i = buttonsToClear.Count - 1; i >= 0; i--)
        {
            Destroy(buttonsToClear[i].gameObject);
        }

        MusicFolderPath = path;
        PlayerPrefs.SetString("MusicFolderPath", MusicFolderPath);

        MusicPaths.Clear();

        DirectoryInfo dir = new DirectoryInfo(MusicFolderPath);
        FileInfo[] MP3 = dir.GetFiles("*.mp3");
        FileInfo[] WAV = dir.GetFiles("*.wav");
        FileInfo[] OGG = dir.GetFiles("*.ogg");
        bool firstLoaded = true;
        foreach (FileInfo song in MP3)
        {
            MusicPaths.Add(song.FullName);
            SongButton button = Instantiate(objToSpawn, parentOfObjToSpawn).GetComponent<SongButton>();
            button.SetSettings(song.FullName);
            if (song.FullName == LastPlayed)
            {
                button.PreviouslyLoaded = true;
            }
            if (firstLoaded && button.PreviouslyLoaded == false)
            {
                firstLoaded = false;
                button.FirstToLoad = true;
            }
        }

        foreach (FileInfo song in WAV)
        {
            MusicPaths.Add(song.FullName);
            SongButton button = Instantiate(objToSpawn, parentOfObjToSpawn).GetComponent<SongButton>();
            button.SetSettings(song.FullName);
            if (song.FullName == LastPlayed)
            {
                button.PreviouslyLoaded = true;
            }
        }

        foreach (FileInfo song in OGG)
        {
            MusicPaths.Add(song.FullName);
            SongButton button = Instantiate(objToSpawn, parentOfObjToSpawn).GetComponent<SongButton>();
            button.SetSettings(song.FullName);
            if (song.FullName == LastPlayed)
            {
                button.PreviouslyLoaded = true;
            }
        }
    }
}