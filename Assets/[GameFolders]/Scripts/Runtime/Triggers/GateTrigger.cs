using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateTrigger : MonoBehaviour, IInteractable
{
    #region Params
    public bool canBeInteract { get; private set; }
    public GameObject leftDoor;
    public GameObject rightDoor;
    const float GATE_OPEN_TIME = 1;
    #endregion
    #region Methods
    private void Start()
    {
        canBeInteract = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponent<Interactor>();
        if (interactor != null && canBeInteract)
        {
            interactor.EnterInteractableArea(this);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Interactor interactor = other.GetComponent<Interactor>();
        if (interactor != null)
        {
            interactor.ExitInteractableArea(this);

        }
    }
    public void Interact()
    {
        if (!canBeInteract)
            return;
        GameManager.Instance.CompeleteStage(true);
        FeedbackPanel.OnFeedbackClose.Invoke();
        leftDoor.transform.DORotate(new Vector3(0f, 90f, 0f), GATE_OPEN_TIME);
        rightDoor.transform.DORotate(new Vector3(0f, -90f, 0f), GATE_OPEN_TIME);
        
    }
    #endregion
}