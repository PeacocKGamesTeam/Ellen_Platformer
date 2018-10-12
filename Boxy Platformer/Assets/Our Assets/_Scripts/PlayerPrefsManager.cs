using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerPrefsManager : MonoBehaviour
{

    public static PlayerPrefsManager Instance;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        InitialPrefs();

        //PlayerPrefs.SetInt("TotalCoins", 30000);
        //PlayerPrefs.SetInt("UnlockedScenes", 30);
        //PlayerPrefs.SetInt("TotalStars", 100);

    }

    // Use this for initialization
    public void InitialPrefs()
    {

        if (!PlayerPrefs.HasKey("UnlockedScenes"))
        {
            PlayerPrefs.SetInt("UnlockedScenes", 1);
        }
        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
        }
        if (!PlayerPrefs.HasKey("TotalStars"))
        {
            PlayerPrefs.SetInt("TotalStars", 0);
        }
        if (!PlayerPrefs.HasKey("ExtraTimeChance"))
        {
            PlayerPrefs.SetInt("ExtraTimeChance", 1);
        }
        if (!PlayerPrefs.HasKey("GroupStagesUnlocked"))
        {
            PlayerPrefs.SetInt("GroupStagesUnlocked", 1);
        }
        if (!PlayerPrefs.HasKey("WinsInARow"))
        {
            PlayerPrefs.SetInt("WinsInARow", 0);
        }
    }

    public void SaveCurrentLevelWin(int Stars)
    {
        string currentScene;
        int currentSceneNo;

        currentScene = SceneManager.GetActiveScene().name;

        currentSceneNo = Convert.ToInt32(currentScene.Substring(currentScene.IndexOf("_")+1, currentScene.Length - Convert.ToInt32(currentScene.IndexOf("_") + 1)));

        if (currentSceneNo == PlayerPrefs.GetInt("UnlockedScenes"))
        {
            PlayerPrefs.SetInt("UnlockedScenes", PlayerPrefs.GetInt("UnlockedScenes") + 1);
            GooglePlay.IncrementAchievement(GPGSIds.leaderboard_high_scores, 10);
            GooglePlayManager.Instance.View5WinsIncrementalAchievment();
            GooglePlayManager.Instance.View10WinsIncrementalAchievment();
            GooglePlayManager.Instance.View15WinsIncrementalAchievment();
            GooglePlayManager.Instance.View20WinsIncrementalAchievment();

            PlayerPrefs.SetInt("WinsInARow", PlayerPrefs.GetInt("WinsInARow") + 1);

            GooglePlayManager.Instance.ViewWins3LevelsInARowIncrementalAchievment();
            GooglePlayManager.Instance.ViewWins5LevelsInARowIncrementalAchievment();
        }

 
        if(PlayerPrefs.HasKey("Stars_" + currentSceneNo.ToString()))
        {
            if (PlayerPrefs.GetInt("Stars_" + currentSceneNo.ToString()) < Stars)
            {
                PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars") + Stars - PlayerPrefs.GetInt("Stars_" + currentSceneNo.ToString()));
                PlayerPrefs.SetInt("Stars_" + currentSceneNo.ToString(), Stars);
                
            }
        }
        else
        {
            PlayerPrefs.SetInt("Stars_" + currentSceneNo.ToString(), Stars);
            PlayerPrefs.SetInt("TotalStars", PlayerPrefs.GetInt("TotalStars") + Stars);
        }

        GooglePlay.AddScoreToLeaderboard(GPGSIds.leaderboard_high_scores, PlayerPrefs.GetInt("TotalStars") * 10);

    }



}
