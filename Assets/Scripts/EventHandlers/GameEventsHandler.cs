using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHandler
{
    public static event Action<IRewardable> SetRewardEvent;
    public static void SetRewardEventHandler(IRewardable rewardable)
    {
        SetRewardEvent?.Invoke(rewardable);
    }

    public static event Action<GameObject> ShowRewardEvent;
    public static void ShowRewardEventHandler(GameObject item)
    {
        ShowRewardEvent?.Invoke(item);
    }

    public static event Action<GameObject> RemainingItemCheckerEvent;
    public static void RemainingItemCheckerEventHandler(GameObject item)
    {
        RemainingItemCheckerEvent?.Invoke(item);
    }

    public static event Action<int, int> BoardCreateCircleEvent;
    public static void BoardCreateCircleEventHandler(int width, int height)
    {
        BoardCreateCircleEvent?.Invoke(width,height);
    }

    public static event Action<int, int> BoardCreateSnakeEvent;
    public static void BoardCreateSnakeEventHandler(int width, int height)
    {
        BoardCreateSnakeEvent?.Invoke(width, height);
    }

    public static event Action<GameObject> GiveRewardEvent;
    public static void GiveRewardEventHandler(GameObject item)
    {
        GiveRewardEvent?.Invoke(item);
    }

    public static event Action<GameObject> FlyRewardEvent;
    public static void FlyRewardEventHandler(GameObject selectedItem)
    {
        FlyRewardEvent?.Invoke(selectedItem);
    }

    public static event Action SelectRewardEvent;
    public static void SelectRewardEventHandler()
    {
        SelectRewardEvent?.Invoke();
    }

    public static event Action<GameObject> ReturnFlyingItemEvent;
    public static void ReturnFlyingItemEventHandler(GameObject flyingItem)
    {
        ReturnFlyingItemEvent?.Invoke(flyingItem);
    }

    public static event Action GameStartEvent;
    public static void GameStartEventHandler()
    {
        GameStartEvent?.Invoke();
    }

    public static event Action FlyingItemPoolCreateEvent;
    public static void FlyingItemPoolCreateEventHandler()
    {
        FlyingItemPoolCreateEvent?.Invoke();
    }

    public static event Action<GameObject> ReturnItemEvent;
    public static void ReturnItemEventHandler(GameObject item)
    {
        ReturnItemEvent?.Invoke(item);
    }

    public static event Action<Vector3, int> AddItemEvent;
    public static void AddItemEventHandler(Vector3 position, int location)
    {
        AddItemEvent?.Invoke(position, location);
    }

    public static event Action<List<int>> RemoveItemsEvent;
    public static void RemoveItemsEventHandler(List<int> list)
    {
        RemoveItemsEvent?.Invoke(list);
    }

    public static event Action<bool> SpinControlEvent;
    public static void SpinControlEventHandler(bool condition)
    {
        SpinControlEvent?.Invoke(condition);
    }
}
