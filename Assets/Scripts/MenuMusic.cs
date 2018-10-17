using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MenuMusic : MonoBehaviour
{
    private AudioSource mySource;
    private void Awake()
    {
        mySource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.LoadClip(PlayerPrefs.GetString("LastPlayedSong", ""));
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("LastPlayedSong", AudioManager.Url);
    }

    void ChangeClipAndPlay()
    {
        if (mySource.isPlaying)
            mySource.Stop();

        mySource.clip = AudioManager.GetClip();
        mySource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioManager.GetClip() != mySource.clip)
        {
            ChangeClipAndPlay();
        }
    }
}
