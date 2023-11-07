using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Repositories/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] List<ItemUnit> list = new();

    private Dictionary<string, ItemUnit> dict;

    private void List2Dict()
    {
        dict = new();

        foreach (var unit in list)
        {
            if (!dict.ContainsKey(unit.ItemName))
            {
                dict.Add(unit.ItemName, unit);
            }
            else
            {
                Debug.LogWarning("Item with " + unit.ItemName + " is duplicated");
            }
        }
    }

    public ItemUnit GetUnit(string itemName)
    {
        ItemUnit unit = null;

        if (dict == null)
        {
            List2Dict();
        }

        if (dict.ContainsKey(itemName))
        {
            unit = dict[itemName];
        }

        return unit;
    }

    public ItemUnit GetRandomUnit()
    {
        return list[Random.Range(0, list.Count())];
    }

    public void AddToList(ItemUnit item)
    {
        list.Add(item);
        List2Dict();
    }

    public List<ItemUnit> GetList()
    {
        return list;
    }

    public void ClearList()
    {
        list.Clear();
    }
}
