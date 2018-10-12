using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamekit2D
{
    public class SceneLoader : MonoBehaviour
    {
        public SceneControllerWrapper sceneController;

        public void LoadScene(string SceneName)
        {
            sceneController.TransitionToSceneCustom(SceneName);
        }
    }
}