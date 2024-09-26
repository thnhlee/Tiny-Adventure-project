using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("AnhDuy/UIFade")]

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1.0f;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeRoutine(float delay)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, delay))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, delay, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }

    }

}
