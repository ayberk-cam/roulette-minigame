using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeelManager : Singleton<FeelManager>
{
    [Header("Start Scene")]
    private Image loadingBar;
    private TextMeshProUGUI loadingText;

    #region Start Feels

    public IEnumerator RevealTextFeel()
    {
        loadingText = ViewManager.Instance.GetView<StartView>().GetLoadingText();

        var originalString = loadingText.text;
        loadingText.text = "";

        var numCharsRevealed = 0;
        while (numCharsRevealed < originalString.Length)
        {
            ++numCharsRevealed;
            loadingText.text = originalString.Substring(0, numCharsRevealed);

            yield return new WaitForSeconds(0.07f);
        }

        loadingText.text = originalString;
    }

    public IEnumerator LoadingBarFillFeel()
    {
        loadingBar = ViewManager.Instance.GetView<StartView>().GetLoadingBar();

        float time = 0;
        float startValue = 0;

        while (time < 1f)
        {
            loadingBar.fillAmount = Mathf.Lerp(startValue, 1, time / 1);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        loadingBar.fillAmount = 1;
    }

    #endregion

    #region Scene Feels

    public IEnumerator FadeLoadingScreenFeel(CanvasGroup canvasGroup, float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }

    #endregion

    public void BounceFeel(GameObject obj, float bounceMultiplier, float time)
    {
        StartCoroutine(BounceFeelRoutine(obj, bounceMultiplier, time));
    }

    public IEnumerator BounceFeelRoutine(GameObject obj, float bounceMultiplier, float totalTime)
    {
        var bounceScale = obj.transform.localScale * bounceMultiplier;
        var targetScale = obj.transform.localScale;

        float time = 0f;
        float timer = totalTime / 2f;

        while (time <= timer)
        {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, bounceScale, time / timer);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        obj.transform.localScale = bounceScale;

        time = 0f;
        timer = totalTime / 2f;

        while (time <= timer)
        {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, targetScale, time / timer);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }

    public IEnumerator ItemLightedRoutine(GameObject glow)
    {
        for(int i = 0; i < 5; i++)
        {
            glow.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            glow.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
