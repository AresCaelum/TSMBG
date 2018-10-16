using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public enum SongSavedProperties { Score, Failed, Cleared, _LAST_ }
public class SongButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI SongText;

    string FiledPath;
    public string SongPath;
    public string SongName;

    public static Image SelectedButton = null;

    public void SetSettings(string path)
    {
        SongPath = path;
        SongName = Path.GetFileNameWithoutExtension(SongPath);
        FiledPath = "file:///" + SongPath;
        SongText.text = SongName;
    }

    public void LoadAudio()
    {
    }

    public void ShowDescription()
    {
        if (SelectedButton != null)
            SelectedButton.color = Color.white;

        SelectedButton = GetComponent<Image>();
        AudioManager.LoadClip(SongPath);
        GetComponent<Image>().color = Color.yellow;
        int score = PlayerPrefs.GetInt(SongName + SongSavedProperties.Score.ToString(), 0);
        int failed = PlayerPrefs.GetInt(SongName + SongSavedProperties.Failed.ToString(), 0);
        int cleared = PlayerPrefs.GetInt(SongName + SongSavedProperties.Cleared.ToString(), 0);

        MusicList.Instance.Labels[(int)PropertiesToFill.Name].text = SongName;
        MusicList.Instance.Labels[(int)PropertiesToFill.Cleared].text = cleared.ToString();
        MusicList.Instance.Labels[(int)PropertiesToFill.Attempts].text = failed.ToString();
        MusicList.Instance.Labels[(int)PropertiesToFill.Score].text = score.ToString();

        float lengthInSeconds = AudioManager.GetClip().length;
        int minutes = Mathf.FloorToInt(lengthInSeconds / 60F);
        int seconds = Mathf.FloorToInt(lengthInSeconds - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        MusicList.Instance.Labels[(int)PropertiesToFill.Length].text = niceTime;
    }
}
