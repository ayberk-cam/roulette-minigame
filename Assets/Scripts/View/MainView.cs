using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button walletButton;

    public override void Initialize()
    {
        startButton.onClick.AddListener(OpenGameScene);
        walletButton.onClick.AddListener(OpenWallet);
    }

    public void OpenGameScene()
    {
        SceneEventsHandler.SceneLoaderEventHandler("GameScene");
    }

    public void OpenWallet()
    {
        ViewManager.Instance.GetView<WalletView>().SetUnits();
        ViewManager.Instance.ShowPopUp<WalletView>();
        ViewManager.Instance.GetView<WalletView>().BounceFeel();
    }
}
