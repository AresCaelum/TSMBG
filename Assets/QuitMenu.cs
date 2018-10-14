using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour {

    // Use this for initialization
    public GameObject Exit;
	void Start () {
        Exit.GetComponent<Button>().onClick.AddListener(ExitMenu);
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    void ExitMenu()
    {
       FileBrowser.CloseScreen();
    }
}
