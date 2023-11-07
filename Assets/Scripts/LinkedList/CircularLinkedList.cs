using System;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[System.Serializable]
public class CircularLinkedList
{
    public SingleNode Head;
    public SingleNode Tail;
    public int Size;

    public void CreateCircularLL(GameObject nodeValue)
    {
        Head = new SingleNode();
        var node = new SingleNode
        {
            Value = nodeValue
        };
        node.Next = node;
        Head = node;
        Tail = node;
        Size = 1;
    }

    public void AddNodeToEndCircularLL(GameObject nodeValue)
    {
        var newNode = new SingleNode
        {
            Value = nodeValue
        };
        Tail.Next = newNode;
        Tail = newNode;
        Tail.Next = Head;
        Size += 1;
    }

    public void InsertNewNodeInCircularLL(GameObject nodeValue, int location)
    {
        var node = new SingleNode
        {
            Value = nodeValue
        };
        if (Head == null) return;
        else if (location == 0)
        { 
            node.Next = Head;
            Head = node;
            Tail.Next = node;
        }
        else if (location >= Size)
        {
            node.Next = Head;
            Tail = node;
            Tail.Next = Head;
        }
        else
        {
            var tempNode = Head;
            int index = 0;
            while (index < location - 1)
            {
                tempNode = tempNode.Next;
                index++;
            }
            node.Next = tempNode.Next;
            tempNode.Next = node;
        }
        Size += 1;
    }

    public bool SearchNodeInCircularLL(GameObject nodeValue)
    {
        if (Head == null) return false;
        else
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < Size; i++)
            {
                if (tempNode.Value == nodeValue)
                {
                    return true;
                }
                else
                {
                    tempNode = tempNode.Next;
                }
            }
        }
        return false;
    }

    public GameObject GetNodeFromLocationInCircularLL(int location)
    {
        if (Head == null) return null;
        else
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < Size; i++)
            {
                if (i == location)
                {
                    return tempNode.Value;
                }
                else
                {
                    tempNode = tempNode.Next;
                }
            }
        }
        return null;
    }

    public SingleNode GetNodeInCircularLL(GameObject nodeValue)
    {
        if (Head == null) return null;
        else
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < Size; i++)
            {
                if (tempNode.Value == nodeValue)
                {
                    return tempNode;
                }
                else
                {
                    tempNode = tempNode.Next;
                }
            }
        }
        return null;
    }

    public int GetNodeLocationInCircularLL(SingleNode node)
    {
        if (Head == null) return -1;
        else
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < Size; i++)
            {
                if (tempNode == node)
                {
                    return i;
                }
                else
                {
                    tempNode = tempNode.Next;
                }
            }
        }
        return -1;
    }


    public void DeleteNodeInCircularLL(int location)
    {
        if (Head == null) return;
        else if (location == 0)
        {
            Head = Head.Next;
            Tail.Next = Head;
            Size -= 1;
        }
        else if (location >= Size)
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < Size - 1; i++)
            {
                tempNode = tempNode.Next;

            }
            if (tempNode == Head)
            { 
                Tail = Head = null;
                Size = 0;
                return;
            }
            tempNode.Next = Head; ;
            Tail = tempNode;
            Size -= 1;

        }
        else
        {
            SingleNode tempNode = Head;
            for (int i = 0; i < location - 1; i++)
            {
                tempNode = tempNode.Next;
            }
            tempNode.Next = tempNode.Next.Next;
            Size -= 1;
        }
    }
}
