using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingItem : MonoBehaviour, IFlyable
{
    [SerializeField] Image itemImage;
    [SerializeField] RectTransform rectTransform;

    public RectTransform RectTransform { get; set; }
    public Image ItemImage { get; set; }

    private void Awake()
    {
        RectTransform = rectTransform;
        ItemImage = itemImage;
    }

    public void SetItemSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    public void SetPosition(Vector3 position)
    {
        RectTransform.position = position;
    }

    public void FlyOnRange(Vector3 destination)
    {
        StartCoroutine(FlyItem(destination));
    }

    public void FlyToPosition(Vector3 destination)
    {
        StartCoroutine(FlyTarget(destination));
    }

    private IEnumerator FlyItem(Vector3 destination)
    {
        float time = 0;
        float timer = .2f;
        while (time <= timer)
        {
            RectTransform.position = Vector3.Lerp(RectTransform.position, destination, time / timer);
            time += Time.fixedDeltaTime;

            if (Vector3.Distance(RectTransform.position, destination) < 5f)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator FlyTarget(Vector3 destination)
    {
        float time = 0;
        float timer = 1f;
        while (time <= timer)
        {
            RectTransform.position = Vector3.Lerp(RectTransform.position, destination, time / timer);
            time += Time.fixedDeltaTime;

            if (Vector3.Distance(RectTransform.position, destination) < 5f)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        GameEventsHandler.ReturnFlyingItemEventHandler(gameObject);
    }
}
