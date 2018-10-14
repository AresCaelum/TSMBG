using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour {

    // Use this for initialization
    public Button Exit;
    private void Awake()
    {
        Exit = GetComponent<Button>();
    }
    void Start () {
        Exit.onClick.AddListener(ExitMenu);
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    void ExitMenu()
    {
       FileBrowser.CloseScreen();
    }
}
