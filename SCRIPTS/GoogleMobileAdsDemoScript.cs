using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    public string AdUnitId = "ca-app-pub-9468300227416279/5045391667"; //Orignal ID
    public bool isDebugLog;

    public RewarderAds RewarderAds;
    public Text Admob_DebugText;
    [HideInInspector]
    public RewardedAd rewardedAd;


    public void Start()
    {
        CreateAndLoadRewardedAd();
    }

    public void CreateAndLoadRewardedAd()
    {
        string adUnitId;

        if (MadActionGamesAd.Instance.isTestAds)
        {

            adUnitId = "ca-app-pub-3940256099942544/5224354917"; //rewarded Test ID
        }
        else
        {
            adUnitId = AdUnitId;

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
        if (isDebugLog)
        {
            Admob_DebugText.text += "CreateAndLoadRewardedAd()";
        }
    }

    public void HandleRewardedAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        if (isDebugLog)
        {
            Admob_DebugText.text += "HandleRewardedAdLoaded event received";
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        if (isDebugLog)
        {
            Admob_DebugText.text += "HandleRewardedAdFailedToLoad event received with message: "
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
            if (isDebugLog)
            {
                Admob_DebugText.text += " WAS  LOADED ";
            }
        }

    }

    public void ShowAdmobRewarded_NoCheck()
    {
        this.rewardedAd.Show();
        if (isDebugLog)
        {
            Admob_DebugText.text += " WAS  LOADED ";
        }

    }
}