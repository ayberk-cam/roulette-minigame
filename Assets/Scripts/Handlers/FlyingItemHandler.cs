using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class FlyingItemHandler : MonoBehaviour
{
    private FlyingItemPooler pooler;
    [SerializeField] RectTransform aimTransform;

    private void Awake()
    {
        pooler = gameObject.GetComponent<FlyingItemPooler>();
    }

    private void Start()
    {
        AddressableManager.Instance.LoadFlyingItemPrefab();
    }

    private void OnEnable()
    {
        GameEventsHandler.FlyRewardEvent += FlyReward;
        GameEventsHandler.ReturnFlyingItemEvent += ReturnItem;
        GameEventsHandler.FlyingItemPoolCreateEvent += CreatePool;
    }

    private void OnDisable()
    {
        GameEventsHandler.FlyRewardEvent -= FlyReward;
        GameEventsHandler.ReturnFlyingItemEvent -= ReturnItem;
        GameEventsHandler.FlyingItemPoolCreateEvent -= CreatePool;
    }

    public void CreatePool()
    {
        pooler.itemPrefab = AddressableManager.Instance.flyingItemPrefab;
        pooler.CreatePool(30);
    }

    public void FlyReward(GameObject selectedItem)
    {
        var rewardable = selectedItem.GetComponent<IRewardable>();
        StartCoroutine(FlyItems(selectedItem, rewardable.ItemSprite, rewardable.Amount, aimTransform));
    }

    IEnumerator FlyItems(GameObject selectedItem, Sprite sprite ,int amount, RectTransform destinationPos)
    {
        var startPos = CameraManager.Instance.mainCamera.WorldToScreenPoint(selectedItem.transform.position);

        List<IFlyable> list = new();

        int flyingItemAmount = amount / 10;

        if(flyingItemAmount == 0)
        {
            flyingItemAmount = 1;
        }

        for (int i = 0; i < flyingItemAmount; i++)
        {
            var item = pooler.GetItem();
            item.GetComponent<IFlyable>().SetItemSprite(sprite);
            item.GetComponent<IFlyable>().SetPosition(startPos);
            list.Add(item.GetComponent<IFlyable>());
            var random = Random.insideUnitCircle * 200f;
            Vector3 range = startPos + new Vector3(random.x, random.y, 0);
            item.GetComponent<IFlyable>().FlyOnRange(range);
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < list.Count(); i++)
        {
            list[i].FlyToPosition(destinationPos.position);
        }

        yield return new WaitForSeconds(1f);

        GameEventsHandler.GiveRewardEventHandler(selectedItem);
    }

    public void ReturnItem(GameObject flyingItem)
    {
        pooler.ReturnItem(flyingItem);
    }
}
