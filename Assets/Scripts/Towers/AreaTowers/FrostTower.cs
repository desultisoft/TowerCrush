using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : AreaTower
{
    [Header("Debuff")]
    public FreezingDebuffFactory appliedDebuff;

    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if(affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            affectedEnemy.healthController.TakeDamage(damage);
            appliedDebuff.GetBuff(affectedEnemy).Apply();
        }
    }
}
