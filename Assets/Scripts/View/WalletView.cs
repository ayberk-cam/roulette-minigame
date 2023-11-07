using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : View
{
    [Header("Buttons")]
    [SerializeField] Button closeButton;

    [Header("Parents")]
    [SerializeField] Transform unitParent;
    [SerializeField] GameObject panelParent;

    private readonly float bounceMultiplier = 1.05f;

    private WalletPopupUnit unitPrefab;

    private List<WalletPopupUnit> walletUnits = new();

    public override void Initialize()
    {
        AddressablesEventHandler.WalletPopupUnitLoaderEvent += CreateUnits;

        closeButton.onClick.AddListener(CloseView);

        AddressableManager.Instance.LoadWalletUnit();
    }

    private void OnDestroy()
    {
        foreach(var unit in walletUnits)
        {
            Destroy(unit.gameObject);
        }

        AddressablesEventHandler.WalletPopupUnitLoaderEvent -= CreateUnits;
    }

    public void CloseView()
    {
        BounceFeel();
        Invoke(nameof(Hide), 0.1f);
    }

    public void BounceFeel()
    {
        FeelManager.Instance.BounceFeel(panelParent, bounceMultiplier, 0.1f);
    }

    public void CreateUnits()
    {
        unitPrefab = AddressableManager.Instance.walletPopupUnit;

        var list = AddressableManager.Instance.walletSO.GetList();

        foreach (var item in list)
        {
            var unit = Instantiate(unitPrefab, unitParent);

            unit.SetUnit(item.ItemName, item.ItemAmount, item.ItemSprite);

            walletUnits.Add(unit);
        }
    }

    public void SetUnits()
    {
        bool hasElement = false;

        foreach(var unit in walletUnits)
        {
            var item = AddressableManager.Instance.walletSO.GetUnit(unit.ItemName);

            if(item != null)
            {
                unit.SetUnit(item.ItemName, item.ItemAmount, item.ItemSprite);

                if (unit.ItemAmount <= 0)
                {
                    unit.gameObject.SetActive(false);
                }
                else
                {
                    hasElement = true;

                    unit.gameObject.SetActive(true);
                }
            }
            else
            {
                unit.gameObject.SetActive(false);
            }
        }

        if(hasElement)
        {
            SetOrder();
        }
    }

    public void SetOrder()
    {
        var countOrdered = walletUnits.OrderBy(x => x.ItemAmount).ToList();

        countOrdered.Reverse();

        for (int i = 0; i < countOrdered.Count; i++)
        {
            countOrdered[i].gameObject.transform.SetSiblingIndex(i);
        }
    }
}
