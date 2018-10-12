using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{

    public class SwitchesController : MonoBehaviour
    {

        public GameObject player;
        public List<GameObject> switches = new List<GameObject>();

        public GameObject doorToOpen;
        public GameObject platformToMove;

        private List<bool> switchesFlags = new List<bool>();

        private string doorOpenAnimation;
        private string doorCloseAnimation;

        private bool shouldOpen = false;
        private bool alreadyOpened = false;
        private bool alreadyMoving = false;
        private bool animationPlaying = false;

        private int pressedCount;

        // Use this for initialization
        void Start()
        {
            pressedCount = 0;


            foreach (GameObject item in switches)
            {
                switchesFlags.Add(false);
            }

            if (doorToOpen.name.StartsWith("Door"))
            {
                doorOpenAnimation = "DoorOpening";
                doorCloseAnimation = "DoorClosing";
            }
            else if (doorToOpen.name.StartsWith("LargeDoor"))
            {
                doorOpenAnimation = "LargeDoorOpen";
                doorCloseAnimation = "LargeDoorClose";
            }



        }

        // Update is called once per frame
        void Update()
        {

            pressedCount = 0;

            for (int i = 0; i < switchesFlags.Count; ++i)
            {
                if (switches[i].GetComponent<InteractOnTrigger2D>().isEnabled)
                {
                    switchesFlags[i] = true;
                }
                else
                {
                    switchesFlags[i] = false;
                }
            }

            foreach (bool item in switchesFlags)
            {
                if (item == true)
                {
                    pressedCount += 1;
                }
            }

            if (doorToOpen)
            {
                HandleDoor();
            }

            if (platformToMove)
            {
                HandlePlatform();
            }

        }


        void HandlePlatform()
        {
            if (!alreadyMoving && pressedCount == switches.Count)
            {
                platformToMove.GetComponent<MovingPlatform>().StartMoving();
                alreadyMoving = true;
                //player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);
            }


            if (pressedCount != switches.Count)
            {
                platformToMove.GetComponent<MovingPlatform>().StopMoving();

                //platformToMove.GetComponent<MovingPlatform>().StopAllCoroutines();
                alreadyMoving = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0f);

            }
         

        }

        void HandleDoor()
        {
            if (!alreadyOpened && pressedCount == switchesFlags.Count)
            {
                if (!(doorToOpen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
                {
                    doorToOpen.GetComponent<Animator>().Play(doorOpenAnimation);
                    alreadyOpened = true;
                }

            }


            if (alreadyOpened && pressedCount != switchesFlags.Count)
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