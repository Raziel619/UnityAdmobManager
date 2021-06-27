using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{

    #region IDs
    //Android
    [SerializeField]
    private string unitIdBannerAndroid = "";
    [SerializeField]
    private string unitIdInterstitialAndroid = "";

    //iOS
    [SerializeField]
    private string unitIdBannerIOS = "";
    [SerializeField]
    private string unitIdInterstitialIOS = "";

    //Sample admob IDs
    private string sampleIDBanner = "ca-app-pub-3940256099942544/6300978111";
    private string sampleIDInterstitial = "ca-app-pub-3940256099942544/1033173712";
    #endregion

    //Space between variables 
    [Space(10)]

    [SerializeField]
    private bool useSampleAds;
    [SerializeField]
    private bool printDebugMessages = true;
    [SerializeField]
    private AdPosition bannerPosition = AdPosition.Bottom;
    [SerializeField]
    private string[] bannerSceneNames;

    private InterstitialAd interstitial;
    private BannerView bannerView;

    public static AdManager Instance;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DebugLog("Started");
            DontDestroyOnLoad(this);
            MobileAds.Initialize(initStatus => { });
            RequestInterstitial();
            SceneManager.sceneLoaded += OnSceneChanged;
            LoadBannerBySceneName(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(this);
        }
    }

    #region Get ID Functions

    private string getBannerID()
    {
        return useSampleAds ? sampleIDBanner : isPlatformiOS() ? unitIdBannerIOS : unitIdBannerAndroid;
    }

    private string getInterstitialID()
    {
        return useSampleAds ? sampleIDInterstitial : isPlatformiOS() ? unitIdInterstitialIOS : unitIdInterstitialAndroid;
    }

    #endregion

    #region Banner Functions

    public void RequestBanner()
    {
        bannerView = new BannerView(getBannerID(), AdSize.SmartBanner, bannerPosition);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        DebugLog("Loading Banner Ad");
    }

    public void CloseBanner()
    {
        DebugLog("Closing Banner Ad");
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    #endregion

    #region Interstitial Functions

    public void RequestInterstitial()
    {

        interstitial = new InterstitialAd(getInterstitialID());
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            RequestInterstitial();
        }
    }

    #endregion

    #region Overloads

    public void OnDestroy()
    {
        DebugLog("OnDestroy");
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        CloseBanner();

    }

    public void OnSceneChanged(Scene scene, LoadSceneMode mod)
    {
        LoadBannerBySceneName(scene.name);

    }

    /*public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
    }*/

    #endregion

    #region Helper Methods

    public void LoadBannerBySceneName(String name)
    {
        if (Array.Exists(bannerSceneNames, element => element == name))
        {
            RequestBanner();
        }
        else
        {
            CloseBanner();
        }
    }

    public void DebugLog(string msg)
    {
        if (printDebugMessages)
        {
            Debug.Log("AdManager: " + msg);
        }
        
    }

    public static bool isPlatformiOS()
    {
        return Application.platform == RuntimePlatform.OSXEditor
            || Application.platform == RuntimePlatform.OSXPlayer
            || Application.platform == RuntimePlatform.IPhonePlayer;
    }

    #endregion

}
