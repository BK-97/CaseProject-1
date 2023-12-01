using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterHealthController : MonoBehaviour,IDamagable
{
    #region Params
    public float _maxHealth;

    private float currentHealth;
    private bool canTakeDamage;
    #endregion
    #region Events
    public static UnityEvent OnCharacterDie = new UnityEvent();
    public static UnityEvent OnCharacterTakeDamage = new UnityEvent();
    #endregion
    #region Methods

    void Start()
    {
        currentHealth = _maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;
        if (currentHealth - damage <= 0)
            Die();
        else
        {
            currentHealth -= damage;
            OnCharacterTakeDamage.Invoke();
        }
    }
    public void Die()
    {
        OnCharacterDie.Invoke();
        GameManager.Instance.CompeleteStage(false);
    }
    #endregion

}
