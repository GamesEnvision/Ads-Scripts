using System;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class MadActionGamesAd : MonoBehaviour
{


    public static MadActionGamesAd Instance;
    [Title("BOOLS")]
    public bool TestMode;
    public bool isDebugLog = false;
    public bool OneBannerAtA_Time;
    [Title("ADMOB IDs")]
    public string admob_AppID;
    public string admob_InterID;
    public string admob_BannerID;
    public string admob_LargeBannerID;
    public string admob_RewardedID = "ca-app-pub-9468300227416279/5045391667"; //Orignal ID

    [Title("UNITY ADS")]
    public UnityAdsInitializer UnityAdsInitializer;
    public UnityInterstitialAds UnityInterstitialAd;
    public UnityRewardedAds UnityRewardedAds;
    public string UnityAdID;

    private bool isInternetAvailable = false;
    private bool isAdsInitialized = false;

    //public AdsInitializer UnityAdsInitializer;
    //public InterstitialAdExample UnityInterstitialAd;
    public BannerView smallBannerView;
    public BannerView largeBannerView;
    public GoogleMobileAds.Api.InterstitialAd interstitial;

    [Title("DEBUG TEXT")]
    public Text debugLogText;



    #region ADS_INITIALIZATION

    void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }


        if (TestMode)
        {
            admob_AppID = "ca-app-pub-3940256099942544~3347511713"; ;
            admob_InterID = "ca-app-pub-3940256099942544/1033173712"; ;
            admob_BannerID = "ca-app-pub-3940256099942544/6300978111"; ;
            admob_LargeBannerID = "ca-app-pub-3940256099942544/6300978111"; ;
        }
        else
        {
        }
    }

    void Start()
    {



        StartCoroutine("StartGame");


    }

    IEnumerator StartGame()
    {
        yield return new WaitForEndOfFrame();


        if (IsInternetConnection())
        {
            InitializeAds();
        }
        else
            isAdsInitialized = false;
    }


    public void InitializeAds()
    {
        try
        {
            //  InitUnityAds();
            MobileAds.Initialize(admob_AppID);

            RequestingBanner();
            RequestingMediumBanner();
            RequestInterstitial();
            isAdsInitialized = true;
        }
        catch (Exception e)
        {

        }
    }

    public void InitUnityAds()
    {
        if (SystemInfo.systemMemorySize > 1024)
        {

            //Advertisement.Initialize(unityId, isTestAds);
            //UnityAdsInitializer.InitializeAds();

            Debug.Log("InitUnityAds CALLED");
        }
    }



    public bool CheckInitialization()
    {
        if (isAdsInitialized)
        {
            isAdsInitialized = true;
            return isAdsInitialized;
        }
        else
        {
            isAdsInitialized = false;
            InitializeAds();
            return false;
        }
    }

    public bool IsInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            isInternetAvailable = true;
        }
        else
            isInternetAvailable = false;

        return isInternetAvailable;
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void RequestingBanner()
    {
        RequestBanner();
    }

    public void RequestingMediumBanner()
    {
        RequestLargeBanner();
    }

    private void RequestBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            try
            {
                if (this.smallBannerView != null)
                    this.smallBannerView.Destroy();
                if (this.smallBannerView == null)
                {
                    this.smallBannerView = new BannerView(admob_BannerID, GoogleMobileAds.Api.AdSize.Banner, GoogleMobileAds.Api.AdPosition.Top);
                    this.smallBannerView.LoadAd(this.CreateAdRequest());
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    private void RequestLargeBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            try
            {
                if (this.largeBannerView != null)
                    this.largeBannerView.Destroy();

                if (this.largeBannerView == null)
                {
                    this.largeBannerView = new BannerView(admob_LargeBannerID, GoogleMobileAds.Api.AdSize.MediumRectangle, GoogleMobileAds.Api.AdPosition.BottomLeft);
                    this.largeBannerView.OnAdLoaded += this.HandleLargeBannerAdLoaded;
                    this.largeBannerView.LoadAd(this.CreateAdRequest());
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public void HandleLargeBannerAdLoaded(object sender, EventArgs args)
    {
        this.largeBannerView.Hide();
    }

    public void RequestInterstitial()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            try
            {
                this.interstitial = new GoogleMobileAds.Api.InterstitialAd(admob_InterID);
                this.interstitial.LoadAd(this.CreateAdRequest());
            }
            catch (Exception e)
            {

            }
        }
    }

    #endregion

    public void ShowSmallAdmobBanner()
    {

        if (OneBannerAtA_Time)
        {
            HideLargeAdmobBanner();
        }

        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        this.smallBannerView.Show();
                        if (SmallBannerCanvas.instance)
                        {
                            SmallBannerCanvas.instance.AdaptiveSmallBannerBG.SetActive(true);
                        }


                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }

    public void ShowLargeAdmobBanner()
    {

        if (OneBannerAtA_Time)
        {
            HideSmallBanner();
        }

        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        this.largeBannerView.Show();
                        LargeBannerCanvas.instance.LargeBannerBG.SetActive(true);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }

    public void HideSmallBanner()
    {

        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            try
            {
                if (CheckInitialization())
                {
                    this.smallBannerView.Hide();
                    if (SmallBannerCanvas.instance)
                    {
                        SmallBannerCanvas.instance.AdaptiveSmallBannerBG.SetActive(false);
                    }

                }
            }
            catch (Exception e)
            {

            }
        }
    }

    public void HideLargeAdmobBanner()
    {

        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            try
            {
                if (CheckInitialization())
                {
                    this.largeBannerView.Hide();
                    LargeBannerCanvas.instance.LargeBannerBG.SetActive(false);

                }

            }
            catch (Exception e)
            {

            }
        }
    }

    public void ChangeSmallBannerPosition(int pos)
    {
        switch (pos)
        {
            case 1:
                this.smallBannerView.SetPosition(AdPosition.TopRight);
                break;
            case 2:
                this.smallBannerView.SetPosition(AdPosition.BottomRight);
                break;
            case 3:
                this.smallBannerView.SetPosition(AdPosition.Top);
                break;
            case 4:
                this.smallBannerView.SetPosition(AdPosition.Bottom);
                break;
            case 5:
                this.smallBannerView.SetPosition(AdPosition.BottomLeft);
                break;
        }
        this.smallBannerView.Show();

    }

    public void ShowAdmobInterstitial()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        if (this.interstitial.IsLoaded())
                        {
                            this.interstitial.Show();
                            RequestInterstitial();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }

    public void Show_Unity()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {


                        if (UnityInterstitialAd.isAdLoadedUnity)
                        {
                            UnityInterstitialAd.ShowAd();
                        }
                       
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

    }

    public void Show_Unity_NoCondition()
    {

        //UnityInterstitialAd.ShowAd();


    }
    public void Load_Unity_NoCondition()
    {
        Debug.Log("Load_Unity_NoCondition CALLED");
        //UnityInterstitialAd.LoadAd();
    }



    public void Unity_Admob()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    try
                    {
                        if (UnityInterstitialAd.isAdLoadedUnity)
                        {
                            UnityInterstitialAd.ShowAd();

                        }
                        else if (interstitial.IsLoaded())
                        {
                            ShowAdmobInterstitial();

                        }

                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }


    public void Admob_Unity()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {

                    try
                    {
                        if (interstitial.IsLoaded())
                        {
                            ShowAdmobInterstitial();
                        }
                        else
                        {
                            Show_Unity();
                        }



                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }


}
