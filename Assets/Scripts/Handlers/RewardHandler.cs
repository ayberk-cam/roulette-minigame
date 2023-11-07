using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsHandler.ShowRewardEvent += ShowReward;
        GameEventsHandler.GiveRewardEvent += GiveReward;
    }

    private void OnDisable()
    {
        GameEventsHandler.ShowRewardEvent -= ShowReward;
        GameEventsHandler.GiveRewardEvent -= GiveReward;
    }

    public void ShowReward(GameObject item)
    {
        GameEventsHandler.SetRewardEventHandler(item.GetComponent<IRewardable>());
        ViewManager.Instance.ShowPopUp<RewardView>();
        ViewManager.Instance.GetView<RewardView>().BounceFeel();
        StartCoroutine(CloseRoutine(item));
    }

    private IEnumerator CloseRoutine(GameObject item)
    {
        yield return new WaitForSeconds(1.5f);
        CloseReward();
        yield return new WaitForSeconds(0.1f);
        ClosePopUp(item);
    }

    public void CloseReward()
    {
        ViewManager.Instance.GetView<RewardView>().BounceFeel();
    }

    public void ClosePopUp(GameObject item)
    {
        ViewManager.Instance.GetView<RewardView>().Hide();
        GameEventsHandler.RemainingItemCheckerEventHandler(item);
    }

    public void GiveReward(GameObject item)
    {
        item.GetComponent<IRewardable>().GiveReward();
    }
}
