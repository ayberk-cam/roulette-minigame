using System.Collections.Generic;

[System.Serializable]
public class WalletSaveObject : SaveObject
{
    public List<WalletBase> items = new();
}
