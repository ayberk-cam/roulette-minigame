using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartView : View
{
    [Header("Elements")]
    [SerializeField] private Image loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    public override void Initialize()
    {
        loadingBar.fillAmount = 0f;
        loadingText.text = "Loading...";
    }

    public Image GetLoadingBar()
    {
        return loadingBar;
    }

    public TextMeshProUGUI GetLoadingText()
    {
        return loadingText;
    }
}
