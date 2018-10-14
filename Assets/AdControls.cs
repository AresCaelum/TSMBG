using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdControls : MonoBehaviour {
    public GameObject AdScreen;
    internal void ActivateAdScreen()
    {
        AdScreen.SetActive(true);
    }

    public void PlayAd()
    {
        AdManager.PlayAd(AdManager.rewardedVideo);
    }
}
