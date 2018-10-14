using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowComplete : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Score = null;

    public void SetScore(int score)
    {
        Score.text = score.ToString();
    }

    public void OnReturnClick()
    {
        Time.timeScale = 1.0f;
        PauseMenu.ReturnToMainMenu();
    }
}