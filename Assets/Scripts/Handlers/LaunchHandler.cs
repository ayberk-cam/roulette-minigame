using UnityEngine;

public class LaunchHandler : MonoBehaviour
{
    private void OnEnable()
    {
        SceneEventsHandler.SaveLoadedEvent += InitialLauncher;
    }

    private void OnDisable()
    {
        SceneEventsHandler.SaveLoadedEvent -= InitialLauncher;
    }

    private void InitialLauncher()
    {
        SaveManager.Instance.LoadWallet();
        SceneEventsHandler.LaunchEventHandler();
    }
}
