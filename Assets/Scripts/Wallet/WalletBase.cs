using UnityEngine;

[System.Serializable]
public class WalletBase
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemAmount;

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

    public int ItemAmount
    {
        get
        {
            return itemAmount;
        }
        set
        {
            itemAmount = value;
        }
    }
}
