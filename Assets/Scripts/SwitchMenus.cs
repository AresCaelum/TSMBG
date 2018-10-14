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
    // Use this for initialization

    private void Awake()
    {
     
           LoadMusicMenu(1);
    }
    void Start () {
        Play.GetComponent<Button>().onClick.AddListener(delegate{LoadMusicMenu(1);});
        Exit.GetComponent<Button>().onClick.AddListener(OnExit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadMusicMenu(int index)
    {
        
            Canvis.SetActive(false);
            Canvis1.SetActive(true);
    }
    private void OnExit()
    {
        Application.Quit();
    }



}
