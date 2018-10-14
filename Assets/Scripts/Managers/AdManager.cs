using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public static class AdManager
{
    static bool initialized = false;

    private static UnityAction RewardFunction;
    private static UnityAction SkipFunction;
    private static UnityAction FailFunction;

    public static string rewardedVideo = "rewardedVideo";

    public static void Initialize(string UnityID, bool testMode = true)
    {
        if (initialized)
            return;
        initialized = true;
        Advertisement.Initialize(UnityID, testMode);
    }

    static public void clearAllRewardfunctions()
    {
        RewardFunction = null;
        SkipFunction = null;
        FailFunction = null;
    }

    static public void SetZoneReward(ShowResult result, UnityAction function)
    {
        switch (result)
        {
            case ShowResult.Failed:
                FailFunction = function;
                break;
            case ShowResult.Skipped:
                SkipFunction = function;
                break;
            case ShowResult.Finished:
                RewardFunction = function;
                break;
        }
    }

    public static void PlayAd(string zone)
    {
        if (!initialized)
        {
            Debug.LogError("JBEDAdManager::PlayRewardedAd - Cannot play an ad without first initializing the manager. JBEDAdManager.Initialize(\"ID From Unity\") ");
            return;
        }

        if (Advertisement.IsReady(zone))
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleEvent;
            Advertisement.Show(zone, options);
        }
        else
        {
            Debug.LogWarning("Zone: " + zone + "\tisn't ready.");
        }

    }

    private static void HandleEvent(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                DoCallBack(FailFunction);
                break;
            case ShowResult.Skipped:
                DoCallBack(SkipFunction);
                break;
            case ShowResult.Finished:
                DoCallBack(RewardFunction);
                break;
        }
    }
    private static void DoCallBack(UnityAction callMethod)
    {
        if (callMethod != null)
        {
            callMethod.Invoke();
        }
    }
}
