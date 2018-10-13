using UnityEngine;

public static class MusicManager
{

    // private int key;
    // Use this for initialization
    private static int Numberofsongs = 0;
    static public void Start()
    {
        Numberofsongs = PlayerPrefs.GetInt("Numberofsongs", 0);
        Debug.Log(PlayerPrefs.GetString("Numberofsongs"));
    }

    // Update is called once per frame
    static void Update()
    {
        Debug.Log(Numberofsongs.ToString());
    }

    public static void addSong(string song, string value)
    {

        PlayerPrefs.SetString(song, value);
        GetSong(song);

    }

    public static string GetSong(string songname)
    {
        return PlayerPrefs.GetString(songname);
    }

    public static int NumberofSongs()
    {
        Numberofsongs = PlayerPrefs.GetInt("Numberofsongs", 0);
        return Numberofsongs;
    }
    public static void IncrementNumberofSongs()
    {
        Numberofsongs++;
        PlayerPrefs.SetInt("Numberofsongs", Numberofsongs);
        PlayerPrefs.Save();
    }

    static public void Exit()
    {
        Debug.Log(PlayerPrefs.GetString("Numberofsongs"));
        PlayerPrefs.SetInt("Numberofsongs", Numberofsongs);
        PlayerPrefs.Save();
    }


}
