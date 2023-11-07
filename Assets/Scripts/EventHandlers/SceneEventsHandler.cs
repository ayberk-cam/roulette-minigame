using System;

public static class SceneEventsHandler
{
    public static event Action<string> SceneLoaderEvent;
    public static void SceneLoaderEventHandler(string loadedScene)
    {
        SceneLoaderEvent?.Invoke(loadedScene);
    }

    public static event Action LaunchEvent;
    public static void LaunchEventHandler()
    {
        LaunchEvent?.Invoke();
    }

    public static event Action SaveLoadedEvent;
    public static void SaveLoadedEventHandler()
    {
        SaveLoadedEvent?.Invoke();
    }
}
