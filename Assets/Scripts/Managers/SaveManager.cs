using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveManager : Singleton<SaveManager>
{  
    private readonly string WalletSaveFileName = "/Wallet";
    private string WalletSavePath;

    protected override void Awake()
    {
        base.Awake();
        WalletSavePath = Application.persistentDataPath + WalletSaveFileName;
    }

    public void LoadWallet()
    {
        MatchSOs();

        WalletSaveObject walletSaveObj = SaveController.LoadObject<WalletSaveObject>(WalletSavePath, out bool fileExists);

        if (fileExists)
        {
            var list = AddressableManager.Instance.walletSO.GetList();

            foreach (var item in list)
            {
                var saveObj = GetWalletSave(item.ItemName, walletSaveObj.items);

                if(saveObj != null)
                {
                    item.ItemAmount = saveObj.ItemAmount;
                }
            }
        }

        SaveWallet();
    }

    public WalletBase GetWalletSave(string itemName, List<WalletBase> list)
    {
        foreach(var item in list)
        {
            if(item.ItemName == itemName)
            {
                return item;
            }
        }

        return null;
    }

    public void MatchSOs()
    {
        var items = AddressableManager.Instance.itemSO.GetList();

        AddressableManager.Instance.walletSO.ClearList();

        foreach(var item in items)
        {
            var walletUnit = new WalletUnit
            {
                ItemName = item.ItemName,
                ItemSprite = item.ItemSprite
            };

            AddressableManager.Instance.walletSO.AddToList(walletUnit);
        }
    }

    public void SaveWallet()
    {
        WalletSaveObject walletSaveObj = new()
        {
            items = new()
        };

        var list = AddressableManager.Instance.walletSO.GetList();

        foreach (var item in list)
        {
            var unit = new WalletBase
            {
                ItemName = item.ItemName,
                ItemAmount = item.ItemAmount
            };
            walletSaveObj.items.Add(unit);
        }

        SaveController.SaveObject(walletSaveObj, WalletSavePath);
    }
}
