using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePanel : PanelBase
{
    [SerializeField]
    private Slider timerSlider;
    [SerializeField]
    private TextMeshProUGUI remainingTorchTM;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
        LevelManager.Instance.OnLevelFinish.RemoveListener(HidePanel);
    }
    public override void ShowPanel()
    {
        SetSlider();
        base.ShowPanel();
    }
    private void SetSlider()
    {
        timerSlider.maxValue = TorchManager.Instance.GetMaxTorchExpirationTime();
        timerSlider.value = TorchManager.Instance.GetMaxTorchExpirationTime();
    }
    private void Update()
    {
        timerSlider.value = TorchManager.Instance.currentExpirationTime;
        remainingTorchTM.text = TorchManager.Instance.CurrentTorchAmount.ToString();
    }
}
