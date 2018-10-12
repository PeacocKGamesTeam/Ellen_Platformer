using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{

    public class StartupLoadingController : MonoBehaviour {


        // Use this for initialization
        void Start()
        {
            SceneController.Instance.GetComponent<SceneControllerWrapper>().TransitionToSceneCustom("Menu");
        }
    }

}