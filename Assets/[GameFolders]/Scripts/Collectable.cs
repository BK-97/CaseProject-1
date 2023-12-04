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
        Destroy(gameObject);
    }

    public void Initialize()
    {
        roundAround = GetComponent<RoundAround>();
        roundAround.StartTurning();
    }
}
