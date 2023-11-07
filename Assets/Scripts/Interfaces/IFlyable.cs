using UnityEngine;
using UnityEngine.UI;

public interface IFlyable
{
    Image ItemImage { get; set; }
    RectTransform RectTransform { get; set; }

    void SetItemSprite(Sprite sprite);
    void SetPosition(Vector3 position);
    void FlyOnRange(Vector3 destination);
    void FlyToPosition(Vector3 destination);
}
