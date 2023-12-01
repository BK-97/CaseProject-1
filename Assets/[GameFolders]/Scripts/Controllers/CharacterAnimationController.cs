using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region Params
    public Animator animator;
    #endregion
    #region Methods
    private void OnEnable()
    {
        CharacterHealthController.OnCharacterDie.AddListener(DeathAnim);
    }
    private void OnDisable()
    {
        CharacterHealthController.OnCharacterDie.RemoveListener(DeathAnim);
    }
    public void SetSpeed(float currentSpeed, float maxSpeed)
    {
        float normalizedSpeed = currentSpeed / maxSpeed;
        animator.SetFloat("MoveSpeed", normalizedSpeed);
        if (Mathf.Approximately(currentSpeed,0))
            animator.applyRootMotion = true;
        else
            animator.applyRootMotion = false;
    }
    public void HitAnim()
    {
        animator.applyRootMotion = false;
        animator.SetTrigger("Hit");

    }
    private void DeathAnim()
    {
        animator.Rebind();
        animator.applyRootMotion = true;
        animator.SetTrigger("Die");
    }
    #endregion
}
