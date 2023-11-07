using UnityEngine;

public interface IRewardable
{
    string ItemName { get; set; }
    int Amount { get; set; }
    Sprite ItemSprite { get; set; }

    void GiveReward();
    void SetReward(ItemUnit itemUnit,int amount);
}
