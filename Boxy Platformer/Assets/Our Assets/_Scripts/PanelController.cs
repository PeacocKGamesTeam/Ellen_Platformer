using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D
{
    public class PanelController : MonoBehaviour
    {
        public GameObject pausePanel;

        private List<bool> buttonsActive = new List<bool>();
        private List<Button> buttonsPowerUps = new List<Button>();

        public void PanelOpen()
        {

            buttonsActive = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsActive;
            buttonsPowerUps = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsPowerUps;


            for (int i = 0; i < buttonsActive.Count; ++i)
            {
               
                    buttonsPowerUps[i].interactable = false;
            }

            Time.timeScale = 0;
        }

        public void PanelClose()
        {
            buttonsActive = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsActive;
            buttonsPowerUps = GameObject.Find("HealthCanvas").GetComponent<StageCanvasManager>().buttonsPowerUps;


            for (int i = 0; i < buttonsActive.Count; ++i)
            {
                if (PlayerPrefs.GetInt("TotalCoins") < PowerUps.Instance.PowerUpCoins)
                {
                    buttonsPowerUps[i].interactable = false;
                }
                else {
                    buttonsPowerUps[i].interactable = true;
                }
                
            }

            Time.timeScale = 1;
        }


    }
}
