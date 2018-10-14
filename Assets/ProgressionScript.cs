using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionScript : MonoBehaviour {

    public Image ProgressionBar;
    public AudioSource Audio;
   
	// Use this for initialization
	void Start () {
        ProgressionBar.fillAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
       ProgressionBar.fillAmount = Audio.time /AudioHolder.instance.GetAudioClip().length;
    }
}
