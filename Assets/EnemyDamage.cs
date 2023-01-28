using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int _attackPower = 1;
    private bool _canAttack;

    private void OnEnable()
    {
        _canAttack = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.GetComponent<IDamageable>();
        if(hit != null && _canAttack)
        {
            hit.Damage(_attackPower);
        }
    }
}
