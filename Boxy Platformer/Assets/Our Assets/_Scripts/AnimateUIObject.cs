using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateUIObject : MonoBehaviour
{

    [Space(10)]
    [Header("Bools")]
    public bool animateOnStart = false;

    [Space(10)]
    [Header("Timing")]
    public float delay = 0f;
    public float effectTime = 1f;

    [Space(10)]
    [Header("Normal Animation")]
    public Vector3 startScale;
    public Vector3 startPos;
    public AnimationCurve scaleCurve;
    public AnimationCurve posCurve;

    [Space(10)]
    [Header("Reverse Animation")]
    public Vector3 reverseStartScale;
    public Vector3 reverseStartPos;
    public AnimationCurve reverseScaleCurve;
    public AnimationCurve reversePosCurve;


    private Vector3 endScale;
    private Vector3 endPos;

    private Vector3 reverseEndScale;
    private Vector3 reverseEndPos;

    private bool reverseAnimationFinished;

    private bool animationShouldPlayOnNextClick;
    private bool reverseAnimationShouldPlayOnNextClick;


    private void Awake()
    {
        if (animateOnStart)
        {
            animationShouldPlayOnNextClick = false;
            endScale = transform.localScale;
            endPos = transform.localPosition;
            StartCoroutine(Animation());
        }
        else
        {
            animationShouldPlayOnNextClick = true;
            reverseAnimationShouldPlayOnNextClick = false;
        }
    }

    IEnumerator Animation()
    {
        transform.localPosition = startPos;
        transform.localScale = startScale;

        yield return new WaitForSecondsRealtime(delay);

        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);
            Vector3 tempScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(perc));
            Vector3 tempPos = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            yield return null;
        } while (perc < 1);
        transform.localScale = endScale;
        transform.localPosition = endPos;
        yield return null;

        reverseAnimationShouldPlayOnNextClick = true;
    }


    IEnumerator ReverseAnimation()
    {
        transform.localPosition = reverseStartPos;
        transform.localScale = reverseStartScale;

        yield return new WaitForSecondsRealtime(delay);

        float time = 0;
        float perc = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time / effectTime);
            Vector3 tempScale = Vector3.LerpUnclamped(reverseStartScale, reverseEndScale, reverseScaleCurve.Evaluate(perc));
            Vector3 tempPos = Vector3.LerpUnclamped(reverseStartPos, reverseEndPos, reversePosCurve.Evaluate(perc));
            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            yield return null;
        } while (perc < 1);
        transform.localScale = reverseEndScale;
        transform.localPosition = reverseEndPos;
        yield return null;

        reverseAnimationFinished = true;

        animationShouldPlayOnNextClick = true;
    }




    public void AnimateElement()
    {
        if (animationShouldPlayOnNextClick)
        {
            animationShouldPlayOnNextClick = false;
            gameObject.SetActive(true);

            endScale = transform.localScale;
            endPos = transform.localPosition;

            StartCoroutine(Animation());
        }
    }




    public void ReverseAnimateElement()
    {
        if (reverseAnimationShouldPlayOnNextClick)
        {
            reverseAnimationShouldPlayOnNextClick = false;
            reverseEndPos = startPos;
            reverseEndScale = startScale;


            StartCoroutine(ReverseAnimation());

        }
    }



    private void Update()
    {
        if (reverseAnimationFinished)
        {
            reverseAnimationFinished = false;
            gameObject.SetActive(false);
            gameObject.transform.localScale = endScale;
            gameObject.transform.localPosition = endPos;
        }
    }

}
