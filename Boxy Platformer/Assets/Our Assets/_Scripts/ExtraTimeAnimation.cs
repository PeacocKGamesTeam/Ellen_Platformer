using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{
    public class ExtraTimeAnimation : MonoBehaviour
    {

        public void TimeExpiredGameOver()
        {
            Time.timeScale = 1;
            PlayerCharacter.PlayerInstance.OnDie(true);
        }

        public void StopAnimator()
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}