using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using Assets.Scripts.Shared;
using Helpers;

public class AdManager : MonoBehaviour
{

    #region IDs

    //Android
    private string UnitIdBannerAndroid = "ca-app-pub-7786369819729095/8957373964";
    private string UnitIdInterstitialAndroid = "ca-app-pub-7786369819729095/4609635962";
    private string AppIDAndroid = "ca-app-pub-7786369819729095~7480640765";

    //iOS
    private string UnitIdBannerIOS = "ca-app-pub-7786369819729095/2297993454";
    private string UnitIdInterstitialIOS = "ca-app-pub-7786369819729095/9165819983";
    private string AppIDIOS = "ca-app-pub-7786369819729095~2239365272";
    #endregion

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
        }
        else
        {
            Destroy(this);
        }
    }

    private string getAppID()
    {
        return GameObjectMethods.isPlatformiOS() ? AppIDIOS : AppIDAndroid;
    }

    private string getBannerID()
    {
        return GameObjectMethods.isPlatformiOS() ? UnitIdBannerIOS : UnitIdBannerAndroid;
    }

    private string getInterstitialID()
    {
        return GameObjectMethods.isPlatformiOS() ? UnitIdInterstitialIOS : UnitIdInterstitialAndroid;
    }

    public void RequestBanner()
    {
        //AdSize adSize = new AdSize(Screen.width,50);
        bannerView = new BannerView(getBannerID(), AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        DebugLog("Loading Banner Ad");
        //bannerView.OnAdLoaded += HandleOnAdLoaded;
        //bannerView.OnAdClosed += HandleOnAdClosed;
    }

    public void CloseBanner()
    {
        DebugLog("Closing Banner Ad");
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void RequestInterstitial()
    {
        if (GameObjectMethods.isPlatformiOS())
        {
            return;
        }

        interstitial = new InterstitialAd(getInterstitialID());
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

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
        if (scene.name == Strings.SCENE_MAIN_MENU)
        {
            CloseBanner();
        }
        else
        {
            RequestBanner();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        LoadAdArea laa = FindObjectOfType<LoadAdArea>();
        if (laa != null)
        {
            laa.Load();
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        LoadAdArea laa = FindObjectOfType<LoadAdArea>();
        if (laa != null)
        {
            laa.Unload();
        }
    }

    public void DebugLog(string msg)
    {
        Debug.Log("AdManager: " + msg);
    }

}
