using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFrameable
{
    GameObject Glow { get; set; }
    GameObject SelectedFrame { get; set; }
    GameObject SelectedSprite { get; set; }
    GameObject RewardedFrame { get; set; }
    GameObject ItemSpriteRenderer { get; set; }

    void MakeSelected();
    void MakeLighted(float time);
    void SetSelectedFrame(bool condition);
    void SetSelectedSprite(bool condition);
    void SetRewardedFrame(bool condition);
    void SetItemSprite(bool condition);
    void ResetReward();
}
