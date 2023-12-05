using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour,ICollectable
{
    private RoundAround roundAround;
    private void Start()
    {
        Initialize();
    }
    public void Collect(ICollector collector)
    {
        collector.Collect(this);
        Demolish();
    }

    public void Demolish()
    {
        roundAround.EndTurning();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelRestart.AddListener(Initialize);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelRestart.RemoveListener(Initialize);
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        roundAround = GetComponent<RoundAround>();
        roundAround.StartTurning();

    }
}
