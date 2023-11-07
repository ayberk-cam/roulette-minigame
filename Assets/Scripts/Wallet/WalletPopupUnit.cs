using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletPopupUnit : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image itemSprite;

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

    private int itemAmount;
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

    public void SetUnit(string itemName, int amount, Sprite sprite)
    {
        nameText.text = itemName;
        amountText.text = "x" + amount.ToString();
        itemSprite.sprite = sprite;

        ItemName = itemName;
        ItemAmount = amount;
    }
}
