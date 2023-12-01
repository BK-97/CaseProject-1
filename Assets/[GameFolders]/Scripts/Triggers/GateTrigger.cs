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
    public void Interact()
    {
        if (!canBeInteract)
            return;
        GameManager.Instance.CompeleteStage(true);

        leftDoor.transform.DORotate(new Vector3(0f, 90f, 0f), GATE_OPEN_TIME);
        rightDoor.transform.DORotate(new Vector3(0f, -90f, 0f), GATE_OPEN_TIME);
        
    }
    #endregion
}