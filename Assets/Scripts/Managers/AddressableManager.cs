using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : Singleton<AddressableManager>
{
    [SerializeField] private AssetLabelReference walletAssetLabelReference;
    [SerializeField] private AssetLabelReference itemAssetLabelReference;
    [SerializeField] private AssetLabelReference walletPopupUnitAssetLabelReference;
    [SerializeField] private AssetLabelReference itemPrefabAssetLabelReference;
    [SerializeField] private AssetLabelReference flyingItemPrefabAssetLabelReference;
    [HideInInspector] public WalletSO walletSO;
    [HideInInspector] public ItemSO itemSO;
    [HideInInspector] public WalletPopupUnit walletPopupUnit;
    [HideInInspector] public GameObject itemPrefab;
    [HideInInspector] public GameObject flyingItemPrefab;

    private void Start()
    {
        LoadItemSO();
    }

    public void LoadWalletSO()
    {
        Addressables.LoadAssetAsync<WalletSO>(walletAssetLabelReference).Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                walletSO = asyncOperationHandle.Result;
                SceneEventsHandler.SaveLoadedEventHandler();
            }
            else
            {
                Debug.LogWarning("Wallet SO failed to load");
            }
        };
    }

    public void LoadItemSO()
    {
        Addressables.LoadAssetAsync<ItemSO>(itemAssetLabelReference).Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                itemSO = asyncOperationHandle.Result;
                LoadWalletSO();
            }
            else
            {
                Debug.LogWarning("Item SO failed to load");
            }
        };
    }

    public void LoadWalletUnit()
    {
        if(walletPopupUnit != null)
        {
            AddressablesEventHandler.WalletPopupUnitLoaderEventHandler();
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(walletPopupUnitAssetLabelReference).Completed += (asyncOperationHandle) =>
            {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    walletPopupUnit = asyncOperationHandle.Result.GetComponent<WalletPopupUnit>();

                    AddressablesEventHandler.WalletPopupUnitLoaderEventHandler();
                }
                else
                {
                    Debug.LogWarning("Wallet Unit failed to load");
                }
            };
        }
    }

    public void LoadItemPrefab()
    {
        if(itemPrefab != null)
        {
            GameEventsHandler.GameStartEventHandler();
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(itemPrefabAssetLabelReference).Completed += (asyncOperationHandle) =>
            {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    itemPrefab = asyncOperationHandle.Result;

                    GameEventsHandler.GameStartEventHandler();
                }
                else
                {
                    Debug.LogWarning("Item Prefab failed to load");
                }
            };
        }
    }

    public void LoadFlyingItemPrefab()
    {
        if(flyingItemPrefab != null)
        {
            GameEventsHandler.FlyingItemPoolCreateEventHandler();
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(flyingItemPrefabAssetLabelReference).Completed += (asyncOperationHandle) =>
            {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    flyingItemPrefab = asyncOperationHandle.Result;

                    GameEventsHandler.FlyingItemPoolCreateEventHandler();
                }
                else
                {
                    Debug.LogWarning("Item Prefab failed to load");
                }
            };
        }
    }
}
