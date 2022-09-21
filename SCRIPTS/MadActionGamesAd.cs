using System;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System.Collections;
using Sirenix.OdinInspector;

public class MadActionGamesAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public static MadActionGamesAd Instance;
    [Title("Bools")]
    public bool isTestAds;
    public bool OneBannerAtA_Time;
    [Title("Ids")]
    public string admob_AppID;
    public string admob_InterID;
    public string admob_BannerID;
    public string admob_LargeBannerID;

    private bool isInternetAvailable = false;
    private bool isAdsInitialized = false;
    private bool isUnityAdsReady = false;


    public BannerView smallBannerView;
    public BannerView largeBannerView;
    public GoogleMobileAds.Api.InterstitialAd interstitial;

    public string unityId;
    public string placementId = "video";
    public string staticPlacementId = "Android_Static";

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

        isUnityAdsReady = false;

        if (isTestAds)
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
            InitUnityAds();
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
            Advertisement.Initialize(unityId, isTestAds);

            Invoke("LoadUnityAd", 2);

            Debug.Log("InitUnityAds CALLED");
        }
    }

    public void LoadUnityAd()
    {
           Advertisement.Load(placementId, this);
            Debug.Log("LoadUnityAd CALLED");
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


                        if (isUnityAdsReady)
                        {
                               Advertisement.Show(placementId, this);
                        }
                        else
                        {
                               Advertisement.Load(placementId);
                        }

                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

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
                        if (isUnityAdsReady)
                        {
                              Advertisement.Show(placementId, this);
                        }
                        else if (interstitial.IsLoaded())
                        {
                            ShowAdmobInterstitial();
                             Advertisement.Load(placementId);
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

    #region UNITY_ADS_CALLBACKS

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Unity Ad LOADED");
        isUnityAdsReady = true;

       
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        isUnityAdsReady = false;
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        //throw new NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //throw new NotImplementedException();
    }
    #endregion
}
