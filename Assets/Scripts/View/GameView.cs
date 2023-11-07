using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : View
{
    [Header("Buttons")]
    [SerializeField] Button spinButton;
    [SerializeField] Button walletButton;
    [SerializeField] Button quitButton;

    public override void Initialize()
    {
        SetButtons(false);

        spinButton.onClick.AddListener(Spin);
        walletButton.onClick.AddListener(OpenWallet);
        quitButton.onClick.AddListener(OpenMainScene);

        GameEventsHandler.SpinControlEvent += SetButtons;
    }

    private void OnDestroy()
    {
        GameEventsHandler.SpinControlEvent -= SetButtons;
    }

    public void Spin()
    {
        GameEventsHandler.SelectRewardEventHandler();
    }

    public void SetSpinButton(bool condition)
    {
        spinButton.gameObject.SetActive(condition);
    }

    public void OpenWallet()
    {
        if(!GameManager.Instance.GetSpinCondition())
        {
            ViewManager.Instance.GetView<WalletView>().SetUnits();
            ViewManager.Instance.ShowPopUp<WalletView>();
            ViewManager.Instance.GetView<WalletView>().BounceFeel();
        }
    }

    public void OpenMainScene()
    {
        SceneEventsHandler.SceneLoaderEventHandler("MainScene");
    }

    public Transform GetWalletTransform()
    {
        return walletButton.gameObject.transform;
    }

    private void SetButtons(bool condition)
    {
        spinButton.gameObject.SetActive(condition);
        quitButton.gameObject.SetActive(condition);
    }
}
