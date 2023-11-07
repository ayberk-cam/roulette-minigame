using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardView : View
{
    [Header("Elements")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemAmountText;
    [SerializeField] Image itemImage;

    [Header("Parents")]
    [SerializeField] GameObject panelParent;

    private readonly float bounceMultiplier = 1.05f;

    public override void Initialize()
    {
        GameEventsHandler.SetRewardEvent += SetReward;
        ResetReward();
    }

    private void OnDestroy()
    {
        GameEventsHandler.SetRewardEvent -= SetReward;
    }

    public void BounceFeel()
    {
        FeelManager.Instance.BounceFeel(panelParent, bounceMultiplier, 0.1f);
    }

    public void SetReward(IRewardable rewardable)
    {
        SetItemName(rewardable.ItemName);
        SetItemAmount(rewardable.Amount);
        SetItemImage(rewardable.ItemSprite);
    }

    private void SetItemName(string itemName)
    {
        itemNameText.text = itemName;
    }

    private void SetItemAmount(int itemAmount)
    {
        itemAmountText.text = "x"  + itemAmount.ToString();
    }

    private void SetItemImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

    public void ResetReward()
    {
        itemNameText.text = "";
        itemAmountText.text = "";
        itemImage.sprite = null;
    }
}
