using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] CanvasGroup canvasGroup;

    private void OnEnable()
    {
        SceneEventsHandler.SceneLoaderEvent += LoadScene;
    }

    private void OnDisable()
    {
        SceneEventsHandler.SceneLoaderEvent -= LoadScene;
    }

    public void LoadScene(string sceneToLoad)
    {
        StartCoroutine(StartLoad(sceneToLoad));
    }

    IEnumerator StartLoad(string sceneToLoad)
    {
        canvasGroup.gameObject.SetActive(true);
        yield return StartCoroutine(FeelManager.Instance.FadeLoadingScreenFeel(canvasGroup,1, 0.25f));
        Addressables.LoadSceneAsync(sceneToLoad,LoadSceneMode.Single,true).Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning(sceneToLoad + "scene cannot loaded!");
            }
        };
        yield return StartCoroutine(FeelManager.Instance.FadeLoadingScreenFeel(canvasGroup,0, 0.25f));
        canvasGroup.gameObject.SetActive(false);
    }
}
