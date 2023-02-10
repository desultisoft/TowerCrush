using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : AreaTower
{
    public Animator anim;
    public FreezingDebuffFactory appliedDebuff;

    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if(affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            //slowAmount = affectedEnemy.pathController.currentSpeed - (affectedEnemy.pathController.currentSpeed * (slowPercent / 100));
            //affectedEnemy.StartCoroutine(affectedEnemy.pathController.Slow(duration, slowAmount));
            affectedEnemy.healthController.TakeDamage(damage);
            appliedDebuff.GetBuff(affectedEnemy).Apply();
        }
    }

    public override void visualEffect()
    {
        anim.SetTrigger("Effect");
    }
}
