using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    #region Params
    private IInteractable currentInteractable;
    private bool canInteract = true;
    #endregion
    #region Methods

    private void OnEnable()
    {
        InputManager.OnInteractInput.AddListener(InteractWithInteractable);
    }
    private void OnDisable()
    {
        InputManager.OnInteractInput.RemoveListener(InteractWithInteractable);
    }
    public void EnterInteractableArea(IInteractable interactable)
    {
        currentInteractable = interactable;
        string feedbackString = "PRESS E FOR INTERACT";
        FeedbackPanel.OnFeedbackOpen.Invoke(feedbackString);
    }
    public void ExitInteractableArea(IInteractable interactable)
    {
        currentInteractable = null;
        FeedbackPanel.OnFeedbackClose.Invoke();

    }
    private void InteractWithInteractable()
    {
        if (!canInteract)
            return;
        if (currentInteractable == null)
            return;
        currentInteractable.Interact();
    }
    #endregion
}