using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PoolerBase : MonoBehaviour
{
    [HideInInspector]
    public GameObject itemPrefab;

    public ObjectPooler<GameObject> objectPool;

    public Transform parent;

    public virtual void CreatePool(int amount)
    {
        objectPool = new ObjectPooler<GameObject>(ItemFactoryMethod, TurnOnItem, TurnOffItem, amount, true);
    }

    public virtual GameObject ItemFactoryMethod()
    {
        return Instantiate(itemPrefab, parent);
    }

    public virtual void TurnOnItem(GameObject item)
    {
        item.SetActive(true);
    }

    public virtual void TurnOffItem(GameObject item)
    {
        item.SetActive(false);
    }

    public virtual GameObject GetItem()
    {
        return objectPool.GetObject();
    }

    public virtual void ReturnItem(GameObject item)
    {
        objectPool.ReturnObject(item);
    }
}
