using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHandler : MonoBehaviour
{
    private void OnEnable()
    {
        SceneEventsHandler.LaunchEvent += StartLaunch;
    }

    private void OnDisable()
    {
        SceneEventsHandler.LaunchEvent += StartLaunch;
    }

    public void StartLaunch()
    {
        StartCoroutine(LaunchRoutine());
    }

    private IEnumerator LaunchRoutine()
    {
        yield return StartCoroutine(FeelManager.Instance.RevealTextFeel());
        yield return StartCoroutine(FeelManager.Instance.LoadingBarFillFeel());
        yield return new WaitForSecondsRealtime(0.5f);
        SceneEventsHandler.SceneLoaderEventHandler("MainScene");
    }
}
