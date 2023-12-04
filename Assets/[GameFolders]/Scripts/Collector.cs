using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour,ICollector
{
    public void Collect(ICollectable collectable)
    {
        TorchManager.OnTorchCollected.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectable triggeredCollectable = other.GetComponent<ICollectable>();
        if (triggeredCollectable != null)
        {
            triggeredCollectable.Collect(this);
        }
    }
}
