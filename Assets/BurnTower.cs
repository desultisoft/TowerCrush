using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTower : AreaTower
{
    public Animator anim;
    public BurningDebuffFactory appliedDebuff;
    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if (affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            appliedDebuff.GetBuff(affectedEnemy).Apply();
            affectedEnemy.healthController.TakeDamage(damage);
        }
    }

    public override void visualEffect()
    {
        anim.SetTrigger("Effect");
    }
}
