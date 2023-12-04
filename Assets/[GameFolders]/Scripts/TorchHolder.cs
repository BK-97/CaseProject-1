using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject torchFire;
    private void OnEnable()
    {
        TorchManager.OnTorchOn.AddListener(()=> torchFire.SetActive(true));
        TorchManager.OnTorchOff.AddListener(()=> torchFire.SetActive(false));
    }
    private void OnDisable()
    {
        TorchManager.OnTorchOn.RemoveListener(() => torchFire.SetActive(true));
        TorchManager.OnTorchOff.RemoveListener(() => torchFire.SetActive(false));
    }
}
