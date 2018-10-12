using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamekit2D
{
    public class StageCanvasManager : MonoBehaviour
    {

        private GameObject star1;
        private GameObject star2;
        private GameObject star3;

        public GameObject watchVideoPanel;

        public List<bool> buttonsActive = new List<bool>();
        public List<Button> buttonsPowerUps = new List<Button>();

        public Text txtTotalCoins;


        public void Start()
        {
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();

            for (int i = 0; i < buttonsActive.Count; ++i)
            {
                if (PlayerPrefs.GetInt("TotalCoins") < PowerUps.Instance.PowerUpCoins)
                {
                    buttonsPowerUps[i].interactable = false;
                }

                buttonsPowerUps[i].gameObject.SetActive(buttonsActive[i]);
            }

        }

        //UnityAdsManager showad;

        public void PlayAdvExtraTime()
        {
            //sto animation
            GameObject.Find("HealthCanvas/WatchVideoPanel/Image").GetComponent<ExtraTimeAnimation>().StopAnimator();

            //unity ads
            AdMobManager.Instance.PlayRewardVideoTime();
        }

        public void GiveCoinsForExtraTime()
        {
            int CoinsForExtraTime = GameObject.Find("HealthCanvas").GetComponent<TimerController>().CoinsForExtraTime;

            

            //add extra time
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - CoinsForExtraTime);
            TimerController timerControllerScript = GameObject.Find("HealthCanvas").GetComponent<TimerController>();
            float TotalTimeOptimized3StarsInSeconds = GameObject.Find("HealthCanvas").GetComponent<TimerController>().TotalTimeOptimized3StarsInSeconds;
            timerControllerScript.TotalTimeInSeconds = (int)(TotalTimeOptimized3StarsInSeconds * 0.3);
            timerControllerScript.watchVideoPanel.SetActive(false);
            timerControllerScript.gameOver = false;

            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();

            Time.timeScale = 1f;


        }

        public void Retry()
        {
            //unity ads
            PlayerCharacter.PlayerInstance.maxSpeed = 0f;
            AdMobManager.Instance.PlayInterstitialVideo();
            Time.timeScale = 1f;
            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom(SceneManager.GetActiveScene().name);
            
        }

        public void Exit()
        {
            PlayerCharacter.PlayerInstance.maxSpeed = 0f;
            Time.timeScale = 1f;
            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom("StageSelection");
        }

        public void Next()
        {
            Time.timeScale = 1f;
            int WinStars;

            GameObject persistentPreferenceObject = GameObject.FindGameObjectWithTag("PersistentPrefs");

            star1 = GameObject.FindGameObjectWithTag("Star1");
            star2 = GameObject.FindGameObjectWithTag("Star2");
            star3 = GameObject.FindGameObjectWithTag("Star3");

            if (star3 != null)
            {
                WinStars = 3;
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 30);
               
                
            }
            else if (star2 != null)
            {
                WinStars = 2;
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 20);
                
                
            }
            else if (star1 != null)
            {
                WinStars = 1;
                PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + 10);
                
            }
            else
            {
                WinStars = 0;
                
            }
            
            persistentPreferenceObject.GetComponent<PlayerPrefsManager>().SaveCurrentLevelWin(WinStars);

            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom("StageSelection");

        }

        public void skipExtraTime()
        {
            Time.timeScale = 1f;

            PlayerCharacter.PlayerInstance.maxSpeed = 0f;

            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionAfterGameOver("StageSelection");

            watchVideoPanel.GetComponent<AnimateUIObject>().ReverseAnimateElement();
        }

        public void SuperJumpPowerUp()
        {
            PowerUps.Instance.GainSuperJump();
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - PowerUps.Instance.PowerUpCoins);
            //TODO: Update coins text.
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();

            StartCoroutine(WaitPowerUpCoroutine());
        }

        public void SuperSpeedPowerUp()
        {
            PowerUps.Instance.GainSuperSpeed();
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - PowerUps.Instance.PowerUpCoins);
            //TODO: Update coins text.
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();

            StartCoroutine(WaitPowerUpCoroutine());
        }

        public void InvinsibilityPowerUp()
        {
            PowerUps.Instance.GainInvinsibility();
            PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") - PowerUps.Instance.PowerUpCoins);
            //TODO: Update coins text.
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();

            StartCoroutine(WaitPowerUpCoroutine());
        }

        IEnumerator WaitPowerUpCoroutine()
        {
            for (int i = 0; i < buttonsActive.Count; ++i)
            {
                buttonsPowerUps[i].interactable = false;
            }

            yield return new WaitForSeconds(PowerUps.Instance.PowerUpDuration);

            if (PlayerPrefs.GetInt("TotalCoins") < PowerUps.Instance.PowerUpCoins)
            {
                for (int i = 0; i < buttonsActive.Count; ++i)
                {
                    buttonsPowerUps[i].interactable = false;
                }
            }
            else
            {
                for (int i = 0; i < buttonsActive.Count; ++i)
                {
                    buttonsPowerUps[i].interactable = true;
                }
            }
        }

    }
}