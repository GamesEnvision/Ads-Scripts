using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdmobRewardedAds : MonoBehaviour
{
    public  MadActionGamesAd MadActionGamesAd;



    public RewarderAds RewarderAds;
    [HideInInspector]
    public RewardedAd rewardedAd;


    public void Start()
    {
        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        string adUnitId;

        if (MadActionGamesAd.Instance.TestMode)
        {

            adUnitId = "ca-app-pub-3940256099942544/5224354917"; //rewarded Test ID
        }
        else
        {
            adUnitId = MadActionGamesAd.admob_RewardedID;

        }

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        if (MadActionGamesAd.isDebugLog)
        {
            MadActionGamesAd.debugLogText.text += "CreateAndLoadRewardedAd()";
        }
    }

    public void HandleRewardedAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        if (MadActionGamesAd.isDebugLog)
        {
            MadActionGamesAd.debugLogText.text += "HandleRewardedAdLoaded event received";
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        if (MadActionGamesAd.isDebugLog)
        {
            MadActionGamesAd.debugLogText.text += "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message;
        }
    }

    public void HandleRewardedAdOpening(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        if (!this.rewardedAd.IsLoaded())
        {
            CreateAndLoadRewardedAd();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        CreateAndLoadRewardedAd();
        RewarderAds.AdFinished();
    }

    public void ShowAdmobRewarded()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
            if (MadActionGamesAd.isDebugLog)
            {
                MadActionGamesAd.debugLogText.text += " WAS  LOADED ";
            }
        }

    }

    public void ShowAdmobRewarded_NoCheck()
    {
        this.rewardedAd.Show();
        if (MadActionGamesAd.isDebugLog)
        {
            MadActionGamesAd.debugLogText.text += " WAS  LOADED ";
        }

    }
}