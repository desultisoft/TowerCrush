using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerTower : AreaTower
{
    public Animator anim;
    [Header("Debuff")]
    public int maxExpose = 3;
    public float addedExpose = 0.1f;
    public int duration = 2;

    public override void visualEffect()
    {
        anim.SetBool("Effect", true);
    }
    public override void NoEnemyFound()
    {
        anim.SetBool("Effect", false);
    }
    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if (affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            //affectedEnemy.StartCoroutine(affectedEnemy.Expose(maxExpose, addedExpose , duration));
            affectedEnemy.healthController.TakeDamage(damage);
        }
    }
}
