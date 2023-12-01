using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateTrigger : MonoBehaviour, IInteractable
{
    #region Events
    public static UnityEvent OnUseGate = new UnityEvent();
    #endregion
    #region Params
    public bool canBeInteract { get; private set; }
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
        OnUseGate.Invoke();
    }
    #endregion
}