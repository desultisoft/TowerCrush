using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : AreaTower
{
    public Animator anim;

    [Header("Frost Slow")]
    public float slowPercent = 50f;
    public float duration = 1f;
    private float slowAmount;
    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if(affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            //slowAmount = affectedEnemy.pathController.currentSpeed - (affectedEnemy.pathController.currentSpeed * (slowPercent / 100));
            //affectedEnemy.StartCoroutine(affectedEnemy.pathController.Slow(duration, slowAmount));
            affectedEnemy.healthController.TakeDamage(damage);
        }
    }

    public override void visualEffect()
    {
        anim.SetTrigger("Effect");
    }
}
