using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUnit
{
    [SerializeField]
    private string itemName;

    public string ItemName
    {
        get
        {
            return itemName;
        }
        set
        {
            itemName = value;
        }
    }

    [SerializeField]
    private Sprite itemSprite;

    public Sprite ItemSprite
    {
        get
        {
            return itemSprite;
        }
        set
        {
            itemSprite = value;
        }
    }
}
