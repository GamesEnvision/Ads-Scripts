using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class UnityAdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public MadActionGamesAd madActionGamesAd;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = madActionGamesAd.UnityAdID;
        Advertisement.Initialize(_gameId, madActionGamesAd.TestMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}