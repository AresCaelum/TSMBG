using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySong : MonoBehaviour {
    public AudioClip clipToPlay;
    public Slider difficultySlider;
    public AdControls adControl;

    public void PlayExample()
    {
        if (AudioHolder.CanPlay())
        {
            AudioHolder.UseLife();
            WindowManager.SaveStats = false;
            ObjectSpawner.ProjectileSpawnRate = 3 - Mathf.RoundToInt(difficultySlider.value);
            AudioHolder.instance.SetAudioClip(clipToPlay);
            AudioHolder.songName = clipToPlay.name;
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
