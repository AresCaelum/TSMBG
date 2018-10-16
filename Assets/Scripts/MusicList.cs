using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PropertiesToFill { Name, Cleared, Attempts, Score, Length }
public class MusicList : MonoBehaviour
{
    [SerializeField]
    GameObject SelectMusicDirectory;
    [SerializeField]
    GameObject SongToSpawn;
    [SerializeField]
    Transform MusicContents;

    public static MusicList Instance;

    public List<TextMeshProUGUI> Labels = new List<TextMeshProUGUI>();

    public void Start()
    {
        if (Instance == null)
        {
            string musicPath = PlayerPrefs.GetString("MusicFolderPath", "EMPTY");
            if (musicPath.Equals("EMPTY") == false)
            {
                DirectoryWindow.MusicFolderPath = PlayerPrefs.GetString("MusicFolderPath");
                DirectoryWindow.LoadSongsInPath(musicPath, SongToSpawn, MusicContents);
            }
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ShowDirectory()
    {
        SelectMusicDirectory.SetActive(true);
    }
}
