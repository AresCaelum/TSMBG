using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TestAudioHolder : MonoBehaviour {

    AudioSource mySource;

    private void Awake()
    {
        mySource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(2f);
        AudioHolder.instance.TestLoad();
        mySource.clip = AudioHolder.instance.GetAudioClip();
    }
	
	// Update is called once per frame
	void Update () {
        if (!mySource.isPlaying && mySource.clip.loadState == AudioDataLoadState.Loaded)
            mySource.Play();
    }
}
