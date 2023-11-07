using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class Item : MonoBehaviour, IRewardable, IFrameable
{
    public string ItemName { get; set; }
    public int Amount { get; set; }
    public Sprite ItemSprite { get; set; }
    public GameObject Glow { get; set; }
    public GameObject SelectedFrame { get; set; }
    public GameObject SelectedSprite { get; set; }
    public GameObject RewardedFrame { get; set; }
    public GameObject ItemSpriteRenderer { get; set; }

    [SerializeField] SpriteRenderer itemSpriteRenderer;
    [SerializeField] GameObject glowObject;
    [SerializeField] GameObject selectedFrameObject;
    [SerializeField] GameObject selectedSpriteObject;
    [SerializeField] GameObject rewardedFrameObject;

    private void Awake()
    {
        Glow = glowObject;
        SelectedFrame = selectedFrameObject;
        SelectedSprite = selectedSpriteObject;
        RewardedFrame = rewardedFrameObject;
        ItemSpriteRenderer = itemSpriteRenderer.gameObject;
    }

    public void GiveReward()
    {
        var unit = AddressableManager.Instance.walletSO.GetUnit(this.ItemName);
        unit.ItemAmount += this.Amount;
        GameEventsHandler.ShowRewardEventHandler(gameObject);
    }

    public void SetReward(ItemUnit itemUnit, int amount)
    {
        ItemName = itemUnit.ItemName;
        Amount = amount;
        ItemSprite = itemUnit.ItemSprite;

        SetSprite();
    }

    public void ResetReward()
    {
        SetSelectedFrame(false);
        SetSelectedSprite(false);
        SetRewardedFrame(false);
        SetItemSprite(true);
    }

    public void SetSprite()
    {
        itemSpriteRenderer.sprite = ItemSprite;
    }

    public void MakeSelected()
    {
        StartCoroutine(FrameSelectedRoutine());
    }

    public void SetSelectedFrame(bool condition)
    {
        SelectedFrame.SetActive(condition);
    }

    public void SetSelectedSprite(bool condition)
    {
        SelectedSprite.SetActive(condition);
    }

    public void SetItemSprite(bool condition)
    {
        ItemSpriteRenderer.SetActive(condition);
    }

    public void SetRewardedFrame(bool condition)
    {
        RewardedFrame.SetActive(condition);
    }

    private IEnumerator FrameSelectedRoutine()
    {
        yield return StartCoroutine(FeelManager.Instance.ItemLightedRoutine(Glow));
        SetSelectedFrame(true);
        yield return new WaitForSeconds(0.2f);
        SetSelectedSprite(true);
        yield return StartCoroutine(FeelManager.Instance.BounceFeelRoutine(SelectedSprite, 1.25f, 0.2f));
        GameEventsHandler.FlyRewardEventHandler(gameObject);
        yield return new WaitForSeconds(1f);
        SetRewardedFrame(true);
        SetItemSprite(false);
    }

    public void MakeLighted(float time)
    {
        StartCoroutine(MakeLightedRoutine(time));
    }

    private IEnumerator MakeLightedRoutine(float time)
    {
        Glow.SetActive(true);
        yield return new WaitForSeconds(time);
        Glow.SetActive(false);
    }
}
