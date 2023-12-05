using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RoundAround : MonoBehaviour
{
    Tween turnAround;
    public void StartTurning()
    {
        turnAround = transform.DORotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    public void EndTurning()
    {
        turnAround.Kill();
    }
}
