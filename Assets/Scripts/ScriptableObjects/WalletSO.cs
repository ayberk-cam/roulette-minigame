using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WalletSO", menuName = "Repositories/WalletSO")]
public class WalletSO : ScriptableObject
{
    [SerializeField] List<WalletUnit> list = new();

    private Dictionary<string, WalletUnit> dict;

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

    public WalletUnit GetUnit(string itemName)
    {
        WalletUnit unit = null;

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

    public WalletUnit GetRandomUnit()
    {
        return list[Random.Range(0, list.Count())];
    }

    public void AddToList(WalletUnit item)
    {
        list.Add(item);
        List2Dict();
    }

    public List<WalletUnit> GetList()
    {
        return list;
    }

    public void ClearList()
    {
        list.Clear();
    }
}
