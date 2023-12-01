using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterHealthController : MonoBehaviour
{
    private float currentHealth;
    public float _maxHealth;
    bool canTakeDamage;
    public static UnityEvent OnCharacterDie = new UnityEvent();

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
        }
    }
    public void Die()
    {
        OnCharacterDie.Invoke();
        GameManager.Instance.CompeleteStage(false);
    }

}
