using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    private Dictionary<int, Vector3> itemPositions;

    public static BoardManager Instance;

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

        itemPositions = new();
    }

    public CircularLinkedList LinkedList;

    private void OnEnable()
    {
        GameEventsHandler.RemainingItemCheckerEvent += CheckRemainingItems;
    }

    private void OnDisable()
    {
        GameEventsHandler.RemainingItemCheckerEvent -= CheckRemainingItems;
    }

    public List<GameObject> GetAllItems()
    {
        var list = new List<GameObject>();

        if (LinkedList.Head == null)
        {
            return list;
        }
        SingleNode tempNode = LinkedList.Head;

        for (int i = 0; i < LinkedList.Size; i++)
        {
            list.Add(tempNode.Value);
            tempNode = tempNode.Next;
        }

        return list;
    }

    public GameObject GetRandomReward(GameObject lastItem)
    {
        var location = Random.Range(0, LinkedList.Size);
        var rewardable = LinkedList.GetNodeFromLocationInCircularLL(location);

        if(lastItem == null)
        {
            return rewardable;
        }
        else
        {
            if (rewardable == lastItem)
            {
                return GetRandomReward(lastItem);
            }
            else
            {
                return rewardable;
            }
        }
    }

    public void CheckRemainingItems(GameObject item)
    {
        if (LinkedList.Size > 0)
        {
            var node = LinkedList.GetNodeInCircularLL(item);
            var location = LinkedList.GetNodeLocationInCircularLL(node);

            GameManager.Instance.CheckBoard(item, location);
        }
        else
        {
            GameManager.Instance.EndGame();
        }
    }

    public void AddItemToList(Vector3 position, GameObject item)
    {
        if(LinkedList.Size == 0)
        {
            LinkedList.CreateCircularLL(item);
        }
        else
        {
            LinkedList.AddNodeToEndCircularLL(item);
        }

        AddItemPosition(position, item);
    }

    public void DeleteNode(GameObject item)
    {
        var node = LinkedList.GetNodeInCircularLL(item);
        var location = LinkedList.GetNodeLocationInCircularLL(node);
        LinkedList.DeleteNodeInCircularLL(location);
    }

    public void AddItemPosition(Vector3 position, GameObject item)
    {
        var node = LinkedList.GetNodeInCircularLL(item);
        var location = LinkedList.GetNodeLocationInCircularLL(node);
        itemPositions.Add(location, position);
    }

    public Vector3 GetPosition(int location)
    {
        return itemPositions[location];
    }
}
