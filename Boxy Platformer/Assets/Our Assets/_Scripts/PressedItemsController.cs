using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gamekit2D
{

    public class PressedItemsController : MonoBehaviour {

        //public string doorOpenAnimation;
        //public string doorCloseAnimation;
        public List<GameObject> pressurePads = new List<GameObject>();
        public GameObject doorToOpen;

        private List<bool> pressurePadFlags = new List<bool>();

        private bool shouldOpen = false;
        private bool alreadyOpened = false;
        private bool animationPlaying = false;

        private string doorOpenAnimation;
        private string doorCloseAnimation;

        private int pressedCount;

        // Use this for initialization
        void Start() {
            pressedCount = 0;

            //TSQL similar to LIKE Door% in order to get all instances of same type of door e.g Door, Door (1) etc.
            //Could also have strings as public and check if null etc but aint nobody got time for dat!
            if(doorToOpen.name.StartsWith("Door"))
            {
                doorOpenAnimation = "DoorOpening";
                doorCloseAnimation = "DoorClosing";
            }
            else if (doorToOpen.name.StartsWith("LargeDoor"))
            {
                doorOpenAnimation = "LargeDoorOpen";
                doorCloseAnimation = "LargeDoorClose";
            }


            foreach(GameObject item in pressurePads)
            {
                pressurePadFlags.Add(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

            pressedCount = 0;

            for(int i =0; i < pressurePads.Count; ++ i)
            {
                if (pressurePads[i].GetComponent<PressurePad>().isPressed)
                {
                    pressurePadFlags[i] = true;
                }
                else
                {
                    pressurePadFlags[i] = false;
                }
            }

            foreach(bool item in pressurePadFlags)
            {
                if(item == true)
                {
                    pressedCount += 1;
                }
            }

            if (!alreadyOpened && pressedCount == pressurePadFlags.Count)
            {
                if (!(doorToOpen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
                {
                    doorToOpen.GetComponent<Animator>().Play(doorOpenAnimation);
                    alreadyOpened = true;
                }
            
            }


            if (alreadyOpened && pressedCount != pressurePadFlags.Count)
            {
                if (!(doorToOpen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
                {
                    doorToOpen.GetComponent<Animator>().Play(doorCloseAnimation);
                    alreadyOpened = false;       
                }
            }

        }
    }

}