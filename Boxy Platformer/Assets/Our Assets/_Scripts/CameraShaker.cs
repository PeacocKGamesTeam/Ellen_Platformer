using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    //DUE TO 2D KIT RESTRICTIONS, INSTEAD OF DRAGGING ACTUAL CAMERA IN 'CAMERA' 
    //SCRIPT VARIABLE, USE CAMERA CHILD OBJECT OF PLAYER CHARACTER
    public float power = 2.0f;
    private float duration;
    private Transform camera;
    public float slowDownAmount = 1.0f;

    private bool shouldShake = false;
    Vector3 startPosition;
    float initialDuration;

    // Use this for initialization
    void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("CameraTransform").transform;
        startPosition = camera.localPosition;
        
        if (this.gameObject.name.StartsWith("Door"))
        {

            duration = this.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        }
        else if (this.gameObject.name.StartsWith("LargeDoor"))
        {
            duration = this.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        }

        initialDuration = duration;
    }

    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                camera.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                duration = initialDuration;
                camera.localPosition = startPosition;
                shouldShake = false;
            }
        }
    }

    // Update is called once per frame
    public void ShakeScreen()
    {
        shouldShake = true;
    }
}
