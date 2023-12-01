using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
public class FeedbackPanel : PanelBase
{
    #region Params
    [SerializeField]
    private TextMeshProUGUI feedbackText;
    Tween textTween;
    #endregion
    #region Events
    public static StringEvent OnFeedbackOpen = new StringEvent();
    public static UnityEvent OnFeedbackClose = new UnityEvent();
    #endregion
    #region Methods
    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(()=> GiveFeedback("Click For Start!"));
        OnFeedbackOpen.AddListener(GiveFeedback);
        OnFeedbackClose.AddListener(HidePanel);
        LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        OnFeedbackOpen.RemoveListener(GiveFeedback);
        OnFeedbackClose.RemoveListener(HidePanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(() => GiveFeedback("Click For Start!"));
        LevelManager.Instance.OnLevelStart.RemoveListener(HidePanel);

    }
    private void GiveFeedback(string feedbackString)
    {
        ShowPanel();
        string upperFeedback = feedbackString;
        textTween=feedbackText.gameObject.transform.DOScale(Vector3.one * 1.1f,0.5f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        feedbackText.text = upperFeedback;
    }
    public override void HidePanel()
    {
        base.HidePanel();
        textTween.Kill();
    }
    #endregion
}
public class StringEvent : UnityEvent<string> { }