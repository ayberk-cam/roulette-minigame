using System;

public static class AddressablesEventHandler
{
    public static event Action WalletPopupUnitLoaderEvent;
    public static void WalletPopupUnitLoaderEventHandler()
    {
        WalletPopupUnitLoaderEvent?.Invoke();
    }
}
