using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour
{

    Button myButton;
    public Slider difficultySlide;
    public AdControls adControl;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Update()
    {
        myButton.interactable = AudioManager.GetClip() != null;
    }

    public void StartGame()
    {
        if (AudioHolder.CanPlay())
        {
            PlayerPrefs.SetString("LastPlayedSong", AudioManager.Url);
            AudioHolder.UseLife();
            WindowManager.SaveStats = true;
            ObjectSpawner.ProjectileSpawnRate = 3 - Mathf.RoundToInt(difficultySlide.value);
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