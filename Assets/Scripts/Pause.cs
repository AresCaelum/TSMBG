
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject Background;
    public GameObject Title;
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public GameObject ReturnToSongMenu;
    public AudioSource theMusic;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        //Debug.Log("I FELL IN HERE");
        // PauseButton.SetActive(true);
        Background.SetActive(false);
        Title.SetActive(false);
        ResumeButton.SetActive(false);
        ReturnToSongMenu.SetActive(false);
        PauseButton.GetComponent<Button>().onClick.AddListener(PauseTime);
        ResumeButton.GetComponent<Button>().onClick.AddListener(ResumeTime);
        ReturnToSongMenu.GetComponent<Button>().onClick.AddListener(ReturnToSongscreen);
    }


    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {

    }


    void PauseTime()
    {
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
        ReturnToSongMenu.SetActive(true);
        Background.SetActive(true);
        Title.SetActive(true);
        Time.timeScale = 0;
        theMusic.Pause();

    }

    void ResumeTime()
    {
        Time.timeScale = 1;
        ResumeButton.SetActive(false);
        ReturnToSongMenu.SetActive(false);
        Background.SetActive(false);
        Title.SetActive(false);
        PauseButton.SetActive(true);
        theMusic.UnPause();
    }

    void ReturnToSongscreen()
    {

        SceneManager.LoadScene(0);
        PauseButton.SetActive(false);
        ResumeButton.SetActive(false);
        ReturnToSongMenu.SetActive(false);
    }


    static public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}