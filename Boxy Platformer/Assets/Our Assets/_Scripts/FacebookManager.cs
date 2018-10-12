using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

    public Text txtTotalCoins;

    private void Awake()
    {

        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);

        }
        else {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }




    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    

    public void Share()
    {
        FB.ShareLink(
            contentTitle:"Boxy Platformer",
            contentURL:new System.Uri("http://AndroidStore.boxyplatformer.link"),
            contentDescription: "Boxy Platformer Awesome Game Downolad Link.",
            callback:OnShare
            );
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink error:" + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log("Error:" + result.PostId);
        }
        else
        {
            Debug.Log("Succeed");
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 50);

            //TODO: Update coins text.
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();


        }

    }
}
