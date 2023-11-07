using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameMode GameMode;
    private int Width;
    private int Height;

    private bool isSpining = false;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }

        GameMode = StartManager.Instance.GameMode;
        Width = StartManager.Instance.Width;
        Height = StartManager.Instance.Height;
    }

    private void Start()
    {
        AddressableManager.Instance.LoadItemPrefab();
    }

    private void OnEnable()
    {
        GameEventsHandler.GameStartEvent += StartGame;
    }

    private void OnDisable()
    {
        GameEventsHandler.GameStartEvent -= StartGame;
    }

    public void StartGame()
    {
        if(GameMode == GameMode.Circle)
        {
            GameEventsHandler.BoardCreateCircleEventHandler(Width, Height);
        }
        else
        {
            GameEventsHandler.BoardCreateSnakeEventHandler(Width, Height);
        }
    }

    public void EndGame()
    {
        GameEventsHandler.SpinControlEventHandler(false);
        SceneEventsHandler.SceneLoaderEventHandler("GameScene");
    }

    public void CheckBoard(GameObject item, int location)
    {
        if(GameMode == GameMode.Circle)
        {
            BoardManager.Instance.DeleteNode(item);
            SaveManager.Instance.SaveWallet();

            if(BoardManager.Instance.LinkedList.Size == 0)
            {
                EndGame();
            }
            else
            {
                GameEventsHandler.SpinControlEventHandler(true);
                SetSpinCondition(false);
            }
        }
        else
        {
            var list = new List<int>();
            var row = location / Width;
            if(row % 2 == 0)
            {
                for(int i = 0; i < Width; i++)
                {
                    var deletedLocation = i + row * Width;
                    list.Add(deletedLocation);
                }
            }
            else
            {
                for(int i = Width - 1; i >= 0; i--)
                {
                    var deletedLocation = i + row * Width;
                    list.Add(deletedLocation);
                }
            }

            GameEventsHandler.RemoveItemsEventHandler(list);
            SetSpinCondition(false);
        }
    }

    public void SetSpinCondition(bool condition)
    {
        isSpining = condition;
    }

    public bool GetSpinCondition()
    {
        return isSpining;
    }
}
