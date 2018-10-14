using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour {

    Button myButton;
    public AdControls adControl;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }


    private void Start()
    {
        myButton.interactable = false;
        SelectFile.UsedFile += DisablePlayButton;
        AudioHolder.SongFinishedLoading += EnablePlayButton;
        LoadMusicButton.LoadMusic += DisablePlayButton;
    }

    private void OnDestroy()
    {
        SelectFile.UsedFile -= DisablePlayButton;
        AudioHolder.SongFinishedLoading -= EnablePlayButton;
        LoadMusicButton.LoadMusic -= DisablePlayButton;
    }

    void DisablePlayButton()
    {
        myButton.interactable = false;
    }

    void EnablePlayButton()
    {
        myButton.interactable = true;
    }

    public void StartGame()
    {
        if (AudioHolder.CanPlay())
        {
            AudioHolder.UseLife();
            SceneManager.LoadScene(1);
        }
        else
        {
            AdManager.clearAllRewardfunctions();
            AdManager.SetZoneReward(UnityEngine.Advertisements.ShowResult.Finished, AudioHolder.ResetLives);
            // Show Ad Menu...
            adControl.ActivateAdScreen();
        }
    }

}