using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewarderAds : MonoBehaviour
{
    #region Instance
    public static RewarderAds instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GoogleMobileAdsDemoScript GoogleMobileAdsDemoScript;
    public UnityRewardedAdsScript UnityRewardedAdsScript;


    public void ViewRewarded_Admob_Unity()
    {
        if (GoogleMobileAdsDemoScript.rewardedAd.IsLoaded())
        {
            GoogleMobileAdsDemoScript.ShowAdmobRewarded_NoCheck();
        }
        else
        {
            if (UnityRewardedAdsScript.UnityRewardedLoaded)
            {
                UnityRewardedAdsScript.ShowRewardedAd();
            }
        }
    }
    public int option = 0;

    public void AdFinished()
    {
        Debug.Log("AdFinished CALLED");
        if (option == 1) // watch video for coins Ad
        {
            MainMenuUI.instance._Get100Coins();
        }
        if (option == 2) // watch video for coins Ad
        {
            GameplayControl.instance._SkipLevel();
         
        }
        if (option == 3) // watch video for coins Ad
        {
            GameplayControl.instance._Get2XCoins();
        }
        if (option == 4) // watch video for coins Ad
        {

        }
        if (option == 5) // watch video for coins Ad
        {
            RewardedInApps.instance._GoldPackage1();
        }
        if (option == 6) // watch video for coins Ad
        {
            RewardedInApps.instance._GoldPackage2();

        }
        if (option == 7) // watch video for coins Ad
        {
            RewardedInApps.instance._GoldPackage3();

        }
        if (option == 8) // watch video for coins Ad
        {
            RewardedInApps.instance._UnlockAllLevels();

        }
        if (option == 9) // watch video for coins Ad
        {
            RewardedInApps.instance._UnlockAllGuns();

        }
        if (option == 10) // watch video for coins Ad
        {
            RewardedInApps.instance._UnlockAllGame();

        }
        if (option == 11) // watch video for coins Ad
        {
            GameplayControl.instance._SkipLevel();

        }
    }
}
