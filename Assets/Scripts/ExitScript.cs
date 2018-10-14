using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {
    public GameObject Exit;
    // Use this for initialization
    void Start () {
        Exit.GetComponent<Button>().onClick.AddListener(OnExit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnExit()
    {
        Application.Quit();
    }
}
