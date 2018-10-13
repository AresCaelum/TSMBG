using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour {

    Coroutine myCoroutine;
    Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        myButton.interactable = false;
        SelectFile.UsedFile += DisablePlayButton;
        LoadMusicButton.LoadMusic += DisablePlayButton;
    }

    private void OnDestroy()
    {
        SelectFile.UsedFile -= DisablePlayButton;
        LoadMusicButton.LoadMusic -= DisablePlayButton;
    }

    void DisablePlayButton()
    {
        myButton.interactable = false;

        if (myCoroutine != null)
            StopCoroutine(myCoroutine);

        myCoroutine = StartCoroutine(WaitForAudioLoad());
    }

    IEnumerator WaitForAudioLoad()
    {
        while(AudioHolder.instance.GetStatus() != AudioDataLoadState.Loaded)
        {
            Debug.Log(AudioHolder.instance.GetStatus());
            yield return null;
        }

        myButton.interactable = true;
        myCoroutine = null;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
