using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamekit2D
{


    public class StageSelectionCanvasManager : MonoBehaviour
    {

        public int numberOfLevels;
        public Canvas stageSelectionCanvas;

        public RectTransform buttonsPositionReference;

        public GameObject buttonsParentContainer;

        List<GameObject> buttonParents = new List<GameObject>();

        public GameObject stageButtonDimensionPrefab;

        public GameObject stageButtonStar0Prefab;
        public GameObject stageButtonStar1Prefab;
        public GameObject stageButtonStar2Prefab;
        public GameObject stageButtonStar3Prefab;
        public GameObject stageButtonDisabledPrefab;

        public List<Button> lockImages;

        private GameObject stageButtonPrefab;

        private GameObject[] stageButtons;



        public Text txtTotalStars;

        // Use this for initialization
        void Start()
        {
            foreach (Transform child in buttonsParentContainer.transform)
            {
                buttonParents.Add(child.gameObject);
            }

            txtTotalStars.text = PlayerPrefs.GetInt("TotalStars").ToString();

            DrawStageButtons(numberOfLevels);

            LockLevels();
        }

        private void DrawStageButtons(int numberOfLevels)
        {
            //Delete last child of canvas (text i used to show that gui buttons will get drawned at run time)
            int canvasChildCount = stageSelectionCanvas.gameObject.transform.childCount;
            stageSelectionCanvas.gameObject.transform.GetChild(canvasChildCount - 1).gameObject.SetActive(false);

            //Get the scale factor
            float canvasScaleFactor = stageSelectionCanvas.scaleFactor;

            //Get dimensions of the viewport. In our case, the canvas.
            // Viewport and button dimensions will be Delta size dimesions (without scaling)

            //Vector2 viewPortDimensions = new Vector2(stageSelectionCanvas.GetComponent<RectTransform>().rect.width * canvasScaleFactor,stageSelectionCanvas.GetComponent<RectTransform>().rect.height * canvasScaleFactor);
            Vector2 buttonDimensions = new Vector2(stageButtonDimensionPrefab.GetComponent<RectTransform>().rect.width * canvasScaleFactor, stageButtonDimensionPrefab.GetComponent<RectTransform>().rect.height * canvasScaleFactor);

            //New 20180526. Get panel dimensions and use that to draw buttons in panel
            Vector2 panelDimensions = new Vector2(buttonsPositionReference.rect.width * canvasScaleFactor, buttonsPositionReference.rect.height * canvasScaleFactor);




            //Get all 4 positions of canvas in case we need them. Will probably only use Top Left point for reference
            //Vector2 canvasTopLeft = new Vector2(stageSelectionCanvas.transform.position.x - (viewPortDimensions.x / 2), stageSelectionCanvas.transform.position.y + (viewPortDimensions.y / 2));
            //Vector2 canvasTopRight = new Vector2(stageSelectionCanvas.transform.position.x + (viewPortDimensions.x / 2), stageSelectionCanvas.transform.position.y + (viewPortDimensions.y / 2));
            //Vector2 canvasBottomLeft = new Vector2(stageSelectionCanvas.transform.position.x - (viewPortDimensions.x / 2), stageSelectionCanvas.transform.position.y - (viewPortDimensions.y / 2));
            //Vector2 canvasBottomRight = new Vector2(stageSelectionCanvas.transform.position.x + (viewPortDimensions.x / 2), stageSelectionCanvas.transform.position.y - (viewPortDimensions.y / 2));


            //New 20180526. Get panel top left position
            List<Vector2> panelsTopLeftList = new List<Vector2>();
            foreach (GameObject thePanel in buttonParents)
            {
                Vector2 panelTopLeft = new Vector2(thePanel.transform.position.x - ((buttonsPositionReference.rect.width * canvasScaleFactor) / 2), thePanel.transform.position.y + ((buttonsPositionReference.rect.height * canvasScaleFactor) / 4));
                panelsTopLeftList.Add(panelTopLeft);
            }


            //int buttonLimitHorizontal = (int)(viewPortDimensions.x / (buttonDimensions.x * 1.5));

            //New 20180526
            int buttonLimitHorizontalPanel = (int)(panelDimensions.x / (buttonDimensions.x * 1.5));



            RectTransform previousStageButtonTransform = null;

            for (int i = 1; i <= numberOfLevels; ++i)
            {
                if (i > PlayerPrefs.GetInt("UnlockedScenes"))
                {
                    stageButtonPrefab = stageButtonDisabledPrefab;
                }
                else
                {
                    if (PlayerPrefs.HasKey("Stars_" + i))
                    {
                        switch (PlayerPrefs.GetInt("Stars_" + i))
                        {
                            case 1:
                                stageButtonPrefab = stageButtonStar1Prefab;
                                break;
                            case 2:
                                stageButtonPrefab = stageButtonStar2Prefab;
                                break;
                            case 3:
                                stageButtonPrefab = stageButtonStar3Prefab;
                                break;
                        }
                    }
                    else
                    {
                        stageButtonPrefab = stageButtonStar0Prefab;
                    }
                }

                GameObject stageButton = Instantiate(stageButtonPrefab) as GameObject;
                //stageButton.transform.SetParent(stageSelectionCanvas.transform, false);

                //New 20180526

                stageButton.transform.SetParent(buttonParents[(int)((i - 1) / 5)].transform, false);

                //switch (i)
                //{
                //    case 1 - 5:
                //        stageButton.transform.SetParent(buttonParents[0].transform, false);
                //        break;
                //}



                //Old approach. To use this, switch the for loop to start from numberOfLevels going to 0
                //stageButton.transform.position = new Vector2(canvasTopLeft.x + ((buttonDimensions.x / 2) * i) + (buttonDimensions.x * (i - 1)),
                //    canvasTopLeft.y - ((buttonDimensions.y * 2) * Mathf.Ceil(i / buttonLimitHorizontal)));

                //New Approach
                //New 20180526. Replace canvasTopLeft with panelTopLeft and buttonLimitHorizontal with buttonLimitHorizontalPanel
                //If its the first item set manually position
                if (i % 5 == 1)
                {
                    stageButton.transform.position = new Vector2(panelsTopLeftList[(int)((i - 1) / 5)].x + buttonDimensions.x,
                                                                panelsTopLeftList[(int)((i - 1) / 5)].y - (buttonDimensions.y * 0.7f));
                }
                else
                {
                    stageButton.transform.position = new Vector2(previousStageButtonTransform.position.x + (buttonDimensions.x * 1.5f), previousStageButtonTransform.position.y);
                }

                //Flag to add extra Y and reset X position whenever screen width is at limit. Making modulo = 1 because we do not want to change before the 4th is drawn.
                if (/*i != 1 && */ i % buttonLimitHorizontalPanel == 1 && i != 1)
                {
                    stageButton.transform.position = new Vector2(panelsTopLeftList[(int)((i - 1) / 5)].x + buttonDimensions.x, stageButton.transform.position.y);
                }
                //-(buttonDimensions.y * 1.5f)

                //Set up the button settings
                stageButton.name = "Level_" + i;
                stageButton.GetComponentInChildren<Text>().text = i.ToString();
                stageButton.GetComponent<Button>().onClick.AddListener(() => ButtonClickedEvent(stageButton.name));

                previousStageButtonTransform = stageButton.GetComponent<RectTransform>();

            }

            for (int x = 0; x < lockImages.Count; ++x)
            {
                lockImages[x].transform.SetSiblingIndex(6);
                //lockImages[x].onClick.AddListener(() => ButtonUnlockedEvent(lockImages[x]));
                lockImages[x].GetComponentInChildren<Text>().text = "Reach " + ((x+1) * 12 + PlayerPrefs.GetInt("GroupStagesUnlocked")).ToString() + " stars to unlock me!";


                if (x + 1 < PlayerPrefs.GetInt("GroupStagesUnlocked"))
                {
                    lockImages[x].gameObject.SetActive(false);
                }
            }
        }

        private void ButtonClickedEvent(string buttonText)
        {
            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom(buttonText);
        }

        public void ButtonUnlockedEvent(Button button)
        {
            if (PlayerPrefs.GetInt("TotalStars") >= (PlayerPrefs.GetInt("GroupStagesUnlocked")) * 12 + PlayerPrefs.GetInt("GroupStagesUnlocked"))
            {
                PlayerPrefs.SetInt("GroupStagesUnlocked", PlayerPrefs.GetInt("GroupStagesUnlocked") + 1);
                button.gameObject.SetActive(false);
            }
        }

        private void LockLevels()
        {
            stageButtons = GameObject.FindGameObjectsWithTag("StageButton");

            //Since UI is dynamically drawing the buttons in correct order, we do not need to sort them
            //Array.Sort(stageButtons, (x, y) => (Int32.Parse(x.GetComponentInChildren<Text>().text)).CompareTo(Int32.Parse(y.GetComponentInChildren<Text>().text)));


            //Since all buttons get drawned as interactable, lock upwards instead of unlock.
            for (int i = PlayerPrefs.GetInt("UnlockedScenes"); i < stageButtons.Length; ++i)
            {
                stageButtons[i].GetComponent<Button>().interactable = false;


            }
        }




        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);

        }
    }
}