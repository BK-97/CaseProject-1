using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    #region Params
    bool canDamage;
    public int damage;
    #endregion
    #region Methods
    private void Start()
    {
        canDamage = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagableObject = collision.gameObject.GetComponent<IDamagable>();
        if (damagableObject != null&&canDamage)
        {
            damagableObject.TakeDamage(damage);
            StartCoroutine(WaitDamageCO());
        }
    }
    IEnumerator WaitDamageCO()
    {
        canDamage = false;
        yield return new WaitForSeconds(1);
        canDamage = true;
    }
    #endregion

}
