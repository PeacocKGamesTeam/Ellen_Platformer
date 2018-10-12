using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GooglePlayManager : MonoBehaviour {

    public static GooglePlayManager Instance;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }

    }

    public void UnlockAchievement()
    {

        GooglePlay.UnlockAchievement(GPGSIds.achievement_incremental_achievement_view_ads);
    }


    public void ShowAchievements()
    {

        GooglePlay.ShowAchievementUI();
    }

    public void ShowLeaderboards()
    {

        GooglePlay.ShowLeaderboardUI();
    }



    public void ViewAdsIncrementalAchievment()
    {

        // Only do achievements if the user is signed in
        if (Social.localUser.authenticated)
        {
            //START CODE TO COPY FOR ACHIEVEMENT TRACKING
            PlayGamesPlatform.Instance.IncrementAchievement(
                  GPGSIds.achievement_incremental_achievement_view_ads,
                  1,
                  (bool success) =>
                  {
                      Debug.Log("Incremental Achievement view ads: " +
                         success);
                  });

        }
    }

    public void View5WinsIncrementalAchievment()
    {

        // Only do achievements if the user is signed in
        if (Social.localUser.authenticated)
        {
            //START CODE TO COPY FOR ACHIEVEMENT TRACKING
            PlayGamesPlatform.Instance.IncrementAchievement(
                  GPGSIds.achievement_incremental_achievement_5_wins,
                  1,
                  (bool success) =>
                  {
                      Debug.Log("Incremental Achievement 5 wins: " +
                         success);
                  });

        }
    }

    public void View10WinsIncrementalAchievment()
    {

        // Only do achievements if the user is signed in
        if (Social.localUser.authenticated)
        {
            //START CODE TO COPY FOR ACHIEVEMENT TRACKING
            PlayGamesPlatform.Instance.IncrementAchievement(
                  GPGSIds.achievement_incremental_achievement_10_wins,
                  1,
                  (bool success) =>
                  {
                      Debug.Log("Incremental Achievement 10 wins: " +
                         success);
                  });

        }
    }

    public void View15WinsIncrementalAchievment()
    {

        // Only do achievements if the user is signed in
        if (Social.localUser.authenticated)
        {
            //START CODE TO COPY FOR ACHIEVEMENT TRACKING
            PlayGamesPlatform.Instance.IncrementAchievement(
                  GPGSIds.achievement_incremental_achievement_15_wins,
                  1,
                  (bool success) =>
                  {
                      Debug.Log("Incremental Achievement 15 wins: " +
                         success);
                  });

        }
    }

    public void View20WinsIncrementalAchievment()
    {

        // Only do achievements if the user is signed in
        if (Social.localUser.authenticated)
        {
            //START CODE TO COPY FOR ACHIEVEMENT TRACKING
            PlayGamesPlatform.Instance.IncrementAchievement(
                  GPGSIds.achievement_incremental_achievement_20_wins,
                  1,
                  (bool success) =>
                  {
                      Debug.Log("Incremental Achievement 20 wins: " +
                         success);
                  });

        }
    }


    public void ViewWins3LevelsInARowIncrementalAchievment()
    {

        if(PlayerPrefs.GetInt("WinsInARow") == 3)
        { 

            // Only do achievements if the user is signed in
            if (Social.localUser.authenticated)
            {
                //START CODE TO COPY FOR ACHIEVEMENT TRACKING
                PlayGamesPlatform.Instance.UnlockAchievement(
                      GPGSIds.achievement_normal_achievement_win_3_levels_in_a_row,
                      (bool success) =>
                      {
                          Debug.Log("Normal Achievement 3 levels in a row: " +
                             success);
                      });

            }
        }
    }

    public void ViewWins5LevelsInARowIncrementalAchievment()
    {

        if (PlayerPrefs.GetInt("WinsInARow") == 3)
        {

            // Only do achievements if the user is signed in
            if (Social.localUser.authenticated)
            {
                //START CODE TO COPY FOR ACHIEVEMENT TRACKING
                PlayGamesPlatform.Instance.UnlockAchievement(
                      GPGSIds.achievement_normal_achievement_win_5_levels_in_a_row,
                      (bool success) =>
                      {
                          Debug.Log("Normal Achievement Win 5 levels in a row: " +
                             success);
                      });

            }
        }
    }


    


}
