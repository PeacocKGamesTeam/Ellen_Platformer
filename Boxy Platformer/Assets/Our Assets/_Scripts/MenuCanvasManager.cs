using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D
{
    public class MenuCanvasManager : MonoBehaviour
    {

        public Text txtTotalCoins;

        // Use this for initialization
        void Start()
        {
            txtTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        }

    }
}