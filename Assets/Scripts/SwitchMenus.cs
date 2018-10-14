using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenus : MonoBehaviour {

    public GameObject Canvis;
    public GameObject Canvis1;
    public GameObject Play;
    public GameObject Exit;
    public GameObject Options;

    public GameObject AdCanvas;
	// Use this for initialization
	void Start () {
        Play.GetComponent<Button>().onClick.AddListener(LoadMusicMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadMusicMenu()
    {
        Canvis.SetActive(false);
        Canvis1.SetActive(true);
    }


}
