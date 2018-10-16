using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioManager{

    private static AudioClip loadedClip;
    private static string url = "LastPlayed";
    private static string songName = "";

    public static string Url
    {
        get
        {
            return url;
        }

        set
        {
            url = value;
        }
    }

    public static string SongName
    {
        get
        {
            return songName;
        }

        set
        {
            songName = value;
        }
    }

    public static AudioClip LoadClip(string path)
    {
        if (path.Equals(""))
        {
            return loadedClip;
        }

        Url = path;

        WWW clipAddress = new WWW(path);
        SongName = Path.GetFileNameWithoutExtension(path);
        loadedClip = clipAddress.GetAudioClip(false, true);

        return loadedClip;
    }

    public static AudioClip GetClip()
    {
        return loadedClip;
    }
}
