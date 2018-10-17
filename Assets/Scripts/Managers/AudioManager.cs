using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioManager{

    private static AudioClip loadedClip;
    private static string url = "";
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

    public static void ClearClip()
    {
        loadedClip = null;
        url = "";
        songName = "";
    }

    public static AudioClip LoadClip(string path)
    {
        if (path.Equals("") || path == Url)
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
