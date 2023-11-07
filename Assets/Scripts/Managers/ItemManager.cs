using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

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
    }

    public int GetTotalItemAmount(int width, int height)
    {
        int total = 0;
        total += width * 2 + (height - 2) * 2;
        return total;
    }

    public List<ItemUnit> GetItems(int width, int height)
    {
        var list = new List<ItemUnit>();

        var itemAmount = GetTotalItemAmount(width, height);

        for(int i = 0; i < itemAmount; i++)
        {
            var randomUnit = GetRandomUnit();

            var newUnit = new ItemUnit
            {
                ItemName = randomUnit.ItemName,
                ItemSprite = randomUnit.ItemSprite,
            };

            list.Add(newUnit);
        }

        return list;
    }

    public ItemUnit GetRandomUnit()
    {
        return AddressableManager.Instance.itemSO.GetRandomUnit();
    }
}
