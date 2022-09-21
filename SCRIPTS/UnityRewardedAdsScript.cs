using UnityEngine;
using UnityEngine.Advertisements;
public class UnityRewardedAdsScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public RewarderAds rewarderAds;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId = null;

    [HideInInspector]
    public bool UnityRewardedLoaded = false;

    void Awake()
    {
        UnityRewardedLoaded = false;
        _adUnitId = _androidAdUnitId;
        Invoke("LoadAd", 3);
    }

    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        UnityRewardedLoaded = true;
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            rewarderAds.AdFinished();
            Debug.Log("Unity Ads Rewarded Ad Completed");
            Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        UnityRewardedLoaded = false;

    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

}
