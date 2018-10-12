using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Gamekit2D;


public class AdMobManager : MonoBehaviour {

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;
    private RewardBasedVideoAd rewardExtraTimeBasedVideo;
    private InterstitialAd interstitial;

    private bool videoShown;
    private bool coinReward;
    private object threadLock = new object(); //To lock thread to avoid issues of deadlocks

    protected static AdMobManager s_Instance;

    public static AdMobManager Instance
    {
        get
        {
            if (s_Instance != null)
                return s_Instance;

            s_Instance = FindObjectOfType<AdMobManager>();

            if (s_Instance != null)
                return s_Instance;

            Create();

            return s_Instance;
        }
    }

    public static void Create()
    {
        AdMobManager controllerPrefab = Resources.Load<AdMobManager>("AdMobManager");
        s_Instance = Instantiate(controllerPrefab);
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
			return;
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        //initialize admob
#if UNITY_ANDROID
        string appId = "ca-app-pub-4239652787266003~8303320687";
#else
        string appId = "Unexpected Platform";
#endif

        MobileAds.Initialize(appId);


        this.RequestBanner();

        this.RequestInterstitial();

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance; //Based on Google API, Reward Based Video is a Singleton and therefore you can not use multiple Ap IDs / Instances.
        //Therefore we can not have 2x different objects for different type of adds. Instead, using flags et cetera.

        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed; //Event to pre-load new video after last one has played.
        rewardBasedVideo.OnAdCompleted += HandleRewardBasedVideoCompleted;

        this.RequestRewardBasedVideo();
    }


    private void Update()
    {

        lock (threadLock)
        {
            if (videoShown)
            {
                videoShown = false;

                if (coinReward)
                {
                    PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 50);

                    //TODO: Update coins text.
                    GameObject.FindGameObjectWithTag("TotalCoinsText").GetComponent<Text>().text = PlayerPrefs.GetInt("TotalCoins").ToString();

                }
                else
                {
                    //add extra time
                  
                    TimerController timerControllerScript = GameObject.Find("HealthCanvas").GetComponent<TimerController>();
                    float TotalTimeOptimized3StarsInSeconds = GameObject.Find("HealthCanvas").GetComponent<TimerController>().TotalTimeOptimized3StarsInSeconds;
                    timerControllerScript.TotalTimeInSeconds = (int)(TotalTimeOptimized3StarsInSeconds * 0.3);
                    timerControllerScript.watchVideoPanel.SetActive(false);
                    timerControllerScript.gameOver = false;
                    Time.timeScale = 1f;
                }
            }
        }
    }




    private void RequestBanner()
    {
        //WARNING: Do not use our own Ad Unit ID. It's against Admob policy. The sample used below, is a test ad Unit Id provided by the Google Adds SDK.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4239652787266003/9438272523";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    private void RequestInterstitial()
    {

        //WARNING: Do not use our own Ad Unit ID. It's against Admob policy. The sample used below, is a test ad Unit Id provided by the Google Adds SDK.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4239652787266003/8915528344";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += HandleInterstitialAddClosed;
    }


    private void HandleInterstitialAddClosed(object sender, EventArgs args)
    {
        Time.timeScale = 1f;

        //Will probably always show this on retry. In case we change logic, change the part where we load screen as well
        SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom(SceneManager.GetActiveScene().name);

        this.RequestInterstitial();
    }

    private void RequestRewardBasedVideo()
    {
        //WARNING: Do not use our own Ad Unit ID. It's against Admob policy. The sample used below, is a test ad Unit Id provided by the Google Adds SDK.
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-4239652787266003/3414340527";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    private void HandleRewardBasedVideoCompleted(object sender, EventArgs args)
    {
        //We can't perform changes here because it's running on a different thread (That's how the SDK is implemented.
        //Therefore we just add a variable flag and use the Update function to check if there is a reward.
        //lock is only for safety. Just to ensure we won't have both threads trying to access the same resources leading to error. (Unlikely since code in main thread is not heavy)
        lock (threadLock) videoShown = true;

    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();

    }


    public void PlayRewardVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            coinReward = true;

            GooglePlayManager.Instance.ViewAdsIncrementalAchievment();

        }
        //else
        //{
        //    //TODO: Add a message to let the user know he should try in a while since the add has not been loaded
        //}
    }

    public void PlayRewardVideoTime()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            coinReward = false;

            GooglePlayManager.Instance.ViewAdsIncrementalAchievment();
        }
        //else
        //{
        //    //TODO: Add a message to let the user know he should try in a while since the add has not been loaded
        //}
    }

    public void PlayInterstitialVideo()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();

            GooglePlayManager.Instance.ViewAdsIncrementalAchievment();
        }
    }

}
