using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class FeedbackPanel : PanelBase
{
    public static StringEvent OnFeedbackOpen = new StringEvent();
    public static UnityEvent OnFeedbackClose = new UnityEvent();
    public TextMeshProUGUI feedbackText;
    private void OnEnable()
    {
        OnFeedbackOpen.AddListener(GiveFeedback);
        OnFeedbackClose.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        OnFeedbackOpen.RemoveListener(GiveFeedback);
        OnFeedbackClose.RemoveListener(HidePanel);
    }
    private void GiveFeedback(string feedbackString)
    {
        ShowPanel();
        string upperFeedback = feedbackString.ToUpper();
        feedbackText.text = upperFeedback;
    }
}
public class StringEvent : UnityEvent<string> { }