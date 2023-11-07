using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class BoardHandler : MonoBehaviour
{
    private readonly int minWidth = 2;
    private readonly int minHeight = 2;
    private readonly int maxWidth = 4;
    private readonly int maxHeight = 6;

    private BoardPooler boardPooler;

    private int itemAmount = 0;

    private void Awake()
    {
        boardPooler = gameObject.GetComponent<BoardPooler>();
    }

    private void OnEnable()
    {
        GameEventsHandler.BoardCreateCircleEvent += CreateBoardCircle;
        GameEventsHandler.BoardCreateSnakeEvent += CreateBoardSnake;
        GameEventsHandler.ReturnItemEvent += ReturnItem;
        GameEventsHandler.AddItemEvent += AddItem;
        GameEventsHandler.RemoveItemsEvent += RemoveItems;
    }

    private void OnDisable()
    {
        GameEventsHandler.BoardCreateCircleEvent -= CreateBoardCircle;
        GameEventsHandler.BoardCreateSnakeEvent -= CreateBoardSnake;
        GameEventsHandler.ReturnItemEvent -= ReturnItem;
        GameEventsHandler.AddItemEvent -= AddItem;
        GameEventsHandler.RemoveItemsEvent -= RemoveItems;
    }

    public void CreateBoardCircle(int width, int height)
    {
        boardPooler.itemPrefab = AddressableManager.Instance.itemPrefab;
        StartCoroutine(CreateBoardCircleRoutine(width, height));
    }

    private IEnumerator CreateBoardCircleRoutine(int width, int height)
    {
        itemAmount = ItemManager.Instance.GetTotalItemAmount(width, height);

        boardPooler.CreatePool(itemAmount);

        width = Mathf.Clamp(width, minWidth, maxWidth);
        height = Mathf.Clamp(height, minHeight, maxHeight);

        float minX = -width + 1f;
        float maxX = width - 1;

        float minY = -height + 1f;
        float maxY = height - 1f;

        for (int i = 0; i < height; i++)
        {
            var vector = new Vector3(minX, minY + (i * 2f), 0);
            SetItem(vector);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 1; i < width; i++)
        {
            var vector = new Vector3(minX + (i * 2f), maxY, 0);
            SetItem(vector);
            yield return new WaitForSeconds(0.1f);
        }

        if (width > 1)
        {
            for (int i = height - 2; i >= 0; i--)
            {
                var vector = new Vector3(maxX, minY + (i * 2f), 0);
                SetItem(vector);
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (height > 1)
        {
            for (int i = width - 2; i >= 1; i--)
            {
                var vector = new Vector3(minX + (i * 2f), minY, 0);
                SetItem(vector);
                yield return new WaitForSeconds(0.1f);
            }
        }

        GameEventsHandler.SpinControlEventHandler(true);
    }

    public void CreateBoardSnake(int width, int height)
    {
        boardPooler.itemPrefab = AddressableManager.Instance.itemPrefab;
        StartCoroutine(CreateBoardSnakeRoutine(width, height));
    }

    private IEnumerator CreateBoardSnakeRoutine(int width, int height)
    {
        itemAmount = width * height;

        boardPooler.CreatePool(itemAmount);

        width = Mathf.Clamp(width, minWidth, maxWidth);
        height = Mathf.Clamp(height, minHeight, maxHeight);

        float minX = -width + 1f;
        float maxX = width - 1;

        float minY = -height + 1f;
        float maxY = height - 1f;

        for (int i = 0; i < width; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < height; j++)
                {
                    var vector = new Vector3(minX + (j * 2f), minY + (i * 2f), 0);
                    SetItem(vector);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    var vector = new Vector3(minX + (j * 2f), minY + (i * 2f), 0);
                    SetItem(vector);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        GameEventsHandler.SpinControlEventHandler(true);
    }

    private void SetItem(Vector3 position)
    {
        var item = boardPooler.GetItem();
        item.transform.localPosition = position;
        BoardManager.Instance.AddItemToList(position, item);
        item.GetComponent<IRewardable>().SetReward(ItemManager.Instance.GetRandomUnit(), Random.Range(10, 100));
        FeelManager.Instance.BounceFeel(item, 1.05f, 0.1f);
    }

    public void ReturnItem(GameObject item)
    {
        boardPooler.ReturnItem(item);
    }

    public void AddItem(Vector3 position, int location)
    {
        var item = boardPooler.GetItem();
        item.transform.localPosition = position;
        item.GetComponent<IRewardable>().SetReward(ItemManager.Instance.GetRandomUnit(), Random.Range(10, 100));
        BoardManager.Instance.LinkedList.InsertNewNodeInCircularLL(item, location);
        FeelManager.Instance.BounceFeel(item, 1.05f, 0.1f);
    }

    public void RemoveItems(List<int> locations)
    {
        StartCoroutine(RemoveItemsRoutine(locations));
    }

    private IEnumerator RemoveItemsRoutine(List<int> locations)
    {
        foreach (var elem in locations)
        {
            var item = BoardManager.Instance.LinkedList.GetNodeFromLocationInCircularLL(elem);
            item.GetComponent<IFrameable>().ResetReward();
            ReturnItem(item);
            BoardManager.Instance.DeleteNode(item);
            yield return new WaitForSeconds(0.1f);
            AddItem(BoardManager.Instance.GetPosition(elem), elem);
            yield return new WaitForSeconds(0.1f);
        }

        SaveManager.Instance.SaveWallet();
        GameEventsHandler.SpinControlEventHandler(true);
    }
}
