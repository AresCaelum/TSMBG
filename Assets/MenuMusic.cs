using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MenuMusic : MonoBehaviour {
    private AudioSource mySource;
    private void Awake()
    {
        mySource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    IEnumerator Start () {
        AudioHolder.SongFinishedLoading += ChangeClipAndPlay;
        yield return new WaitForSeconds(1f);
        AudioHolder.instance.LoadAudio(PlayerPrefs.GetString(AudioHolder.lastPlayed, ""));
	}

    private void OnDestroy()
    {
        AudioHolder.SongFinishedLoading -= ChangeClipAndPlay;
    }

    void ChangeClipAndPlay()
    {
        if (mySource.isPlaying)
            mySource.Stop();

        mySource.clip = AudioHolder.instance.GetAudioClip();
        mySource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
