using System;
using UnityEngine;

namespace Gamekit2D
{
    public class SceneControllerWrapper : MonoBehaviour
    {
        public void RestartZone (bool resetHealth)
        {
            SceneController.RestartZone (resetHealth);
        }

        public void TransitionToScene (TransitionPoint transitionPoint)
        {
            SceneController.TransitionToScene (transitionPoint);
        }

        public void TransitionToSceneCustom(string sceneName)
        {
            TransitionPoint trans = new TransitionPoint();
            trans.newSceneName = sceneName;
            trans.requiresInventoryCheck = false;
            trans.resetInputValuesOnTransition = false;
            trans.transitionType = TransitionPoint.TransitionType.DifferentZone;
            trans.transitionWhen = TransitionPoint.TransitionWhen.ExternalCall;

            SceneController.TransitionToScene(trans);
        }

        public void RestartZoneWithDelay(float delay)
        {
            SceneController.RestartZoneWithDelay (delay, false);
        }

        public void RestartZoneWithDelayAndHealthReset (float delay)
        {
            SceneController.RestartZoneWithDelay (delay, true);
        }

		public void TransitionAfterGameOver(string sceneName){
			SceneController.TransitionAfterGameOver (sceneName);

		}
    }
}