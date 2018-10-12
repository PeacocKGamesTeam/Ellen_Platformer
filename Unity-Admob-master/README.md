Admob Unity Plugin
==============================

Admob Unity Plugin provides a way to integrate admob ads in Unity3D Game and u3d app.
You can use it for Unity iOS and Android App with the same c# or js code.    


## Admob Unity3d Plugin Readme Contents

1. [Admob Unity Plugin Description](#admob-unity-plugin-description)
2. [Unity Admob Plugin Features](#unity-admob-plugin-features)
3. [Downloads Admob Unity Plugin](#downloads-admob-unity-plugin)
4. [Installation Admob Unity](#installation-admob-unity)
5. [Unity Plugin Wiki and Documentation](#unity-plugin-wiki-and-documentation)
6. [Quick Start](#quick-start)
7. [Unity Admob Demo Usage](#unity-admob-demo-usage)
8. [Important Tips](#important-tips)
9. [Screenshots](#screenshots)
10. [License](#license)

## Admob Unity Plugin Description
The Google Mobile Ads SDK is the latest generation in Google mobile advertising featuring refined ad formats and streamlined APIs for access to mobile ad networks and advertising solutions. The SDK enables Unity mobile app developers to maximize their monetization in native mobile apps.

This repository contains the source code for the Google Mobile Ads Unity plugin. This plugin enables Unity developers to easily serve Google Mobile Ads on Android and iOS apps without having to write Java or Objective-C code. The plugin provides a C# interface for requesting ads that is used by C# scripts in your Unity project.

## Unity Admob Plugin Features
Platforms supported in one plugin :
- [x] Android, via SDK v11.0.4 (part of Google Play service platform)
- [x] iOS, via SDK v7.28.0
- [x] Support all native events
- [x] AdRequest targeting methods,such as children target,test mode
- [x] Not need change Android package name
- [x] Very simple API

Ad Types:
- [x] Banner(All Banner Type and Custom banner sizes)
- [x] Interstitial (text, picture, video)
- [x] Rewarded Video 
- [x] Advanced Native Ad



## Downloads Admob Unity Plugin
AdmobPluginRes/GoogleMobileAds.framework and admob_unity_plugin.unitypackage is reqired <br/>
Download those files from Admob Unity3d Plugin Project Home https://github.com/unity-plugins/Unity-Admob <br/>
or Download all the Unity admob plugin project https://github.com/unity-plugins/Unity-Admob/archive/master.zip<br/>

## Installation Admob Unity
1. Open your project in the Unity editor.
2. Navigate to **Assets -> Import Package -> Custom Package**.
3. Select the AdmobUnityPlugin.unitypackage file.
4. Import all of the files for the plugins by selecting **Import**. Make sure
   to check for any conflicts with files.

## Unity Plugin Wiki and Documentation
* [Document](https://github.com/unity-plugins/Unity-Admob/wiki/Admob-Unity-Plugin-Document)
* [API](https://github.com/unity-plugins/Unity-Admob/wiki/Admob-Unity-Plugin-API)
* [Tutorial](https://github.com/unity-plugins/Unity-Admob/wiki/How-to-Use-Admob-Plugin-for-Unity)

## Quick Start
#### 1.Init Admob Unity Plugin 
Create A C# script ,drag the script to a object on scene , add the follow code in the script file
```
    using admob;
    Admob.Instance().initAdmob("admob banner id", "admob interstitial id");//admob id with format ca-app-pub-279xxxxxxxx/xxxxxxxx
    //Admob.Instance().initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");

```
#### 2.Add Admob Banner in Unity App 
Here is the minimal code needed to show admob banner.
```
    Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);

```

The AdPosition class specifies where to place the banner. AdSize specifies witch size banner to show

#### 3.Remove Banner 
By default, banners are visible. To temporarily hide a banner, call:
```
    Admob.Instance().removeBanner();
```

#### 4.How to integrate Interstitial into Unity 3d app?

Here is the minimal  code to create an interstitial.
```
    Admob.Instance().loadInterstitial(); 
```
Unlike banners, interstitials need to be explicitly shown. At an appropriate
stopping point in your app, check that the interstitail is ready before
showing it:
```
    if (Admob.Instance().isInterstitialReady()) {
      Admob.Instance().showInterstitial();
    }
```
#### 5.Custom Admob Banner Ad Sizes
In addition to constants on _AdSize_, you can also create a custom size:
```
    //Create a 250x250 banner.
    AdSize adSize = new AdSize(250, 250);
    Admob.Instance().showBannerAbsolute(adSize,0,30);
```
#### 6.Admob test Ads and children app
If you want to test the ads or the your app with children target,you can set with admob unity plugin easy
```
    Admob.Instance().setTesting(true);
    Admob.Instance().setForChildren(true);
    string[] keywords = { "game","crash","male game"};
     Admob.Instance().setKeywords(keywords);
```
#### 7.Ad Events
Both _Banner_ and _Interstitial_ contain the same ad events that you can
register for. 
Here we'll demonstrate setting ad events on a interstitial,and show interstitial when load success:
```
    Admob.Instance().interstitialEventHandler += onInterstitialEvent;
    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
```
You only need to register for the events you care about.

#### 8.How to integrate Admob Rewarded Video to Unity3d app?

Here is the minimal  code to create an admob video.
```
    Admob.Instance().loadRewardedVideo("ca-app-pub-3940256099942544/1712485313"); 
```
Simular with interstitial,video need to be explicitly shown at an appropriate
stopping point in your app, check that the video is ready before
showing it:
```
    if (Admob.Instance().isRewardedVideoReady()) {
      Admob.Instance().showRewardedVideo();
    }
```


#### 9.Show Admob Native Advanced Ad in IOS and Android App 
Here is the minimal code needed to show admob banner.
This is implemented with Admob Native Advanced as AdMob announced stop the express format ads ,so you must update this sdk if you are using old native banner.
Just update sdk and replace the admob Express Ad ID with  Advanced Ad ID
```
    Admob.Instance().showNativeBannerRelative(new AdSize(360,100), AdPosition.BOTTOM_CENTER, 0,"ca-app-pub-3940256099942544/2934735716");

```


## Unity Admob Demo Usage
1. import AdmobUnityPlugin.unitypackage to your Unity project
2. copy admobdemo.cs from AdmobPluginRes to your unity project/assets dic
3. attach admobdemo.cs to the main camera
4. edit  admob id  in admobdemo.cs
5. build and run this in your device

## Important Tips
1. Add **GoogleMobileAds.framework**. to Xcode Project
2. Add the following framework to Xcode project
```
    AdSupport.framework,EventKit.framework,EventKitUI.framework,CoreTelephony.framework,StoreKit.framework,MessageUI.framework
```
3. Attach admob to Object on scene,init admob before call admob fun

## Screenshots
![ScreenShot](https://raw.githubusercontent.com/unity-plugins/Unity-Admob/master/doc/android_banner.jpg) 
![ScreenShot](https://raw.githubusercontent.com/unity-plugins/Unity-Admob/master/doc/android_full.jpg) 

## License
[Apache 2.0 License](http://www.apache.org/licenses/LICENSE-2.0.html)


## Change Log
1.Support iphoneX 
2.Implement Native Banner with Admob  Advanced Native Ad 
3.Update sdk


