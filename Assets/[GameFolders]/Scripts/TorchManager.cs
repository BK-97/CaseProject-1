using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorchManager : Singleton<TorchManager>
{
    const float TORCH_EXPIRATION_TIME = 5f;
    const int STARTER_TORCH_AMOUNT=5;
    private int currentTorchAmount;
    [HideInInspector]
    public float currentExpirationTime= TORCH_EXPIRATION_TIME;
    private bool isTorchUsing;

    public static UnityEvent OnTorchCollected = new UnityEvent();
    public static UnityEvent OnTorchOn = new UnityEvent();
    public static UnityEvent OnTorchOff = new UnityEvent();
    public int CurrentTorchAmount
    {
        get
        {
            return currentTorchAmount;
        }
        set
        {
            currentTorchAmount = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.CurrentTorch, currentTorchAmount);
        }
    }

    private void Start()
    {
        CurrentTorchAmount = STARTER_TORCH_AMOUNT;
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(UseTorch);
        OnTorchCollected.AddListener(AddNewTorch);
        LevelManager.Instance.OnLevelFinish.AddListener(TorchUseEnd);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(UseTorch);
        OnTorchCollected.RemoveListener(AddNewTorch);
        LevelManager.Instance.OnLevelFinish.RemoveListener(TorchUseEnd);

    }
    private void UseTorch()
    {
        if (CurrentTorchAmount > 0)
        {
            OnTorchOn.Invoke();
            isTorchUsing = true;
        }
        else
            TorchUseEnd();
    }
    private void TorchUseEnd()
    {
        isTorchUsing = false;
        OnTorchOff.Invoke();
    }
    private void AddNewTorch()
    {
        if (!isTorchUsing)
            UseTorch();
        CurrentTorchAmount++;
    }
    private void RemoveTorch()
    {
        CurrentTorchAmount--;
        if(CurrentTorchAmount==0)
            TorchUseEnd();
    }

    private void Update()
    {
        if (isTorchUsing)
        {
            currentExpirationTime -= Time.deltaTime;

            if (currentExpirationTime <= 0f)
            {
                RemoveTorch();
                if (CurrentTorchAmount > 0)
                {
                    currentExpirationTime = TORCH_EXPIRATION_TIME;
                }
            }
        }
    }
    public float GetMaxTorchExpirationTime()
    {
        return TORCH_EXPIRATION_TIME;
    }
}
