using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D
{

    public class TimerController : MonoBehaviour
    {

        public Text txtTimer;
        public float TotalTimeOptimized3StarsInSeconds; //Total time for 3 stars
        public int Percentage2Stars;
        public int Percentage1Stars;

		public ScreenFader screenFader;

        public Text txtExtraTime;
        public Text txtExtraTimeWithCoins;

        public int CoinsForExtraTime;
        public GameObject watchVideoPanel;

        [HideInInspector]
        public float TotalTimeInSeconds;
        public Button pauseButton;
        public Color newColor;

        public Button GiveCoinsButton;
        //private HandleDoorTrigger doorTriggerScript;
        //private PanelController panelControllerScript;

        private int minutes;
        private int seconds;

        public bool gameOver;

        private List<bool> buttonsActive = new List<bool>();
        private List<Button> buttonsPowerUps = new List<Button>();


        private void Awake()
        {
            gameOver = false;
            //doorTriggerScript = GameObject.Find("LargeDoor").GetComponent<HandleDoorTrigger>();
            //panelControllerScript = GameObject.Find("SceneController").GetComponent<PanelController>();
        }

        // Use this for initialization
        void Start()
        {
            Time.timeScale = 1;
            TotalTimeInSeconds = TotalTimeOptimized3StarsInSeconds + (TotalTimeOptimized3StarsInSeconds * Percentage2Stars / 100) + (TotalTimeOptimized3StarsInSeconds * Percentage1Stars / 100);

            minutes = (int)(TotalTimeInSeconds % 3600) / 60;
            seconds = (int)(TotalTimeInSeconds % 3600) % 60;

            txtTimer.text = minutes.ToString() + ":" + seconds.ToString();

            txtExtraTime.text = "Play adv to get extra " + ((int)(TotalTimeOptimized3StarsInSeconds * 0.3)).ToString() + " seconds";
            txtExtraTimeWithCoins.text = "Give " + CoinsForExtraTime.ToString() + " to get extra " + ((int)(TotalTimeOptimized3StarsInSeconds * 0.3)).ToString() + " seconds";
        }

        // Update is called once per frame
        void Update()
        {
            if (TotalTimeInSeconds > 0)
            {
                TotalTimeInSeconds -= Time.deltaTime;
                minutes = (int)(TotalTimeInSeconds % 3600) / 60;
                seconds = (int)(TotalTimeInSeconds % 3600) % 60;

                txtTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                txtTimer.text = "00:00";

                if (!gameOver)
                {
                    gameOver = true;
                    int ExtraTimeChance = PlayerPrefs.GetInt("ExtraTimeChance");
                    
                    if (ExtraTimeChance <= 0)
                    {
                        PlayerPrefs.SetInt("ExtraTimeChance", 1);
                        this.GameOverOrExtraSeconds();
                    }
                    else
                    {
                        PlayerCharacter.PlayerInstance.maxSpeed = 0f;

                        PlayerPrefs.SetInt("ExtraTimeChance", ExtraTimeChance - 1);

                        SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionAfterGameOver("StageSelection");
                    }
                }
            }

            if (seconds == 10 & minutes == 0)
            {
                txtTimer.fontStyle = FontStyle.Bold;
                txtTimer.color = newColor;
            }

            if(seconds <= 2 & minutes == 0)
            {
                pauseButton.interactable = false;
            }
            else
            {
                pauseButton.interactable = true;
            }
        }

        private void GameOverOrExtraSeconds()
        {
            Time.timeScale = 0;

            if (PlayerPrefs.GetInt("TotalCoins") < CoinsForExtraTime)
            {
                
                GiveCoinsButton.interactable = false;
            }
            watchVideoPanel.GetComponent<AnimateUIObject>().AnimateElement();
            pauseButton.interactable = false;

            buttonsActive = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsActive;
            buttonsPowerUps = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsPowerUps;


            for (int i = 0; i < buttonsActive.Count; ++i)
            {

                buttonsPowerUps[i].interactable = false;
            }
        }
    }
}