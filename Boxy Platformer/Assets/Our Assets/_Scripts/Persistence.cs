using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gamekit2D
{


    public class Persistence : MonoBehaviour {
        public Button playRewardVideoBtn;

        void Start()
        {
            if (gameObject.name == "PlayRewardedVideo")
            {
                playRewardVideoBtn.onClick.AddListener(delegate ()
                {
                    PlayRewardsVideo();
                });
            }
        }


        public void PlayRewardsVideo()
        {
            AdMobManager.Instance.PlayRewardVideo();

        }

    }
}
