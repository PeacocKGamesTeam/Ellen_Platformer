using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D
{

    public class HandleDoorTrigger : MonoBehaviour
    {
        public Button pauseButton;

        public Text txtCoinsCalculation;
        public Text txtCoinsTotal;

        private BoxCollider2D colliderDoor;
        private GameObject finishedPanel;

        private GameObject star1;
        private GameObject star2;
        private GameObject star3;

        private TimerController TimerObject;
        private float RemainingTimeInSeconds;


        private float TotalTimeOptimized3StarsInSeconds;
        private int Percentage2Stars;
        private int Percentage1Stars;

        private List<bool> buttonsActive = new List<bool>();
        private List<Button> buttonsPowerUps = new List<Button>();

        // Use this for initialization
        void Start()
        {
            finishedPanel = GameObject.FindGameObjectWithTag("FinishedPanel");

            star1 = GameObject.FindGameObjectWithTag("Star1");
            star2 = GameObject.FindGameObjectWithTag("Star2");
            star3 = GameObject.FindGameObjectWithTag("Star3");

            finishedPanel.SetActive(false);

            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);

            colliderDoor = GetComponent<BoxCollider2D>();

            TimerObject = GameObject.Find("HealthCanvas").GetComponent<TimerController>();

            TotalTimeOptimized3StarsInSeconds = TimerObject.TotalTimeOptimized3StarsInSeconds;
            Percentage2Stars = TimerObject.Percentage2Stars;
            Percentage1Stars = TimerObject.Percentage1Stars;
            
            
        }

        private void Update()
        {
            RemainingTimeInSeconds = TimerObject.TotalTimeInSeconds;
        }

        public void EnableTrigger()
        {
            colliderDoor.enabled = true;
        }

        public void DisableTrigger()
        {
            colliderDoor.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Ellen")
            {
                Time.timeScale = 0;
                StarsWin();

                finishedPanel.GetComponent<AnimateUIObject>().AnimateElement();

                pauseButton.interactable = false;
                buttonsActive = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsActive;
                buttonsPowerUps = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsPowerUps;


                for (int i = 0; i < buttonsActive.Count; ++i)
                {

                    buttonsPowerUps[i].interactable = false;
                }
            }

        }

        private void StarsWin()
        {
            float TotalTime2StarsInSeconds = (TotalTimeOptimized3StarsInSeconds * Percentage2Stars / 100);
            float TotalTime1StarsInSeconds = (TotalTimeOptimized3StarsInSeconds * Percentage1Stars / 100);


            if ((int)RemainingTimeInSeconds > 0 & (int)RemainingTimeInSeconds <= (int)TotalTime1StarsInSeconds)
            {
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);

                txtCoinsCalculation.text = "+10";

                txtCoinsTotal.text = (PlayerPrefs.GetInt("TotalCoins") + 10).ToString();
            }
            else if ((int)RemainingTimeInSeconds > (int)TotalTime1StarsInSeconds & (int)RemainingTimeInSeconds <= (int)TotalTime1StarsInSeconds + (int)TotalTime2StarsInSeconds)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);

                txtCoinsCalculation.text = "+20";

                txtCoinsTotal.text = (PlayerPrefs.GetInt("TotalCoins") + 20).ToString();
            }
            else
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);

                txtCoinsCalculation.text = "+30";

                txtCoinsTotal.text = (PlayerPrefs.GetInt("TotalCoins") + 30).ToString();
                
            }
        }


    }
}