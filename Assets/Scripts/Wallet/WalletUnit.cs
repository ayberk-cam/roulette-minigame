using UnityEngine;

[System.Serializable]
public class WalletUnit : WalletBase
{
    [SerializeField]
    private Sprite itemSprite;

    public Sprite ItemSprite
    {
        get
        {
            return itemSprite;
        }
        set
        {
            itemSprite = value;
        }
    }

}
