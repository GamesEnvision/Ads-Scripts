using Sirenix.OdinInspector;
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

    public AdmobRewardedAds AdmobRewardedAds;
    public UnityRewardedAds UnityRewardedAds;

    public void ViewRewarded_Admob_Unity()
    {
        if (AdmobRewardedAds.rewardedAd.IsLoaded())
        {
            AdmobRewardedAds.ShowAdmobRewarded_NoCheck();
        }
        else
        {
            UnityRewardedAds.ShowUnityRewardedAd();
        }
    }
    [ReadOnly]
    public int option = 0;

    public void AdFinished()
    {
        Debug.Log("AdFinished CALLED");
        if (option == 1)
        {
            MainMenuUI.instance._Get100Coins();
        }
        if (option == 2) 
        {
            GameplayControl.instance._SkipLevel();

        }
        if (option == 3)
        {
            GameplayControl.instance._Get2XCoins();
        }
        if (option == 4) 
        {

        }
        if (option == 5) 
        {
            RewardedInApps.instance._GoldPackage1();
        }
        if (option == 6) 
        {
            RewardedInApps.instance._GoldPackage2();

        }
        if (option == 7) 
        {
            RewardedInApps.instance._GoldPackage3();

        }
        if (option == 8) 
        {
            RewardedInApps.instance._UnlockAllLevels();

        }
        if (option == 9)
        {
            RewardedInApps.instance._UnlockAllGuns();

        }
        if (option == 10) 
        {
            RewardedInApps.instance._UnlockAllGame();

        }
        if (option == 11) 
        {
            GameplayControl.instance._SkipLevel();

        }
    }
}
