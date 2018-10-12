using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject CreditsPanel;

    public Animator CreditsAnimator;
    private AnimationClip CreditsAnimation;

    public float secondsToWaitAfterCreditsAnimation;

    private GameObject dustParticles;


    private void Start()
    {
        dustParticles = GameObject.Find("DustCloud");
    }

    public void OpenCredits()
    {
        MainPanel.SetActive(false);
        CreditsPanel.SetActive(true);

        dustParticles.SetActive(false);

        CreditsAnimator.Play("Credits Animation");


        StartCoroutine(CloseCredits());

    }

    IEnumerator CloseCredits()
    {
        CreditsAnimation = CreditsAnimator.runtimeAnimatorController.animationClips[0];
        yield return new WaitForSeconds(CreditsAnimation.length + secondsToWaitAfterCreditsAnimation);

        MainPanel.SetActive(true);
        dustParticles.SetActive(true);
        CreditsPanel.SetActive(false);

    }



    


}
