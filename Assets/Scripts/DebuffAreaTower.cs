using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAreaTower : AreaTower
{
    [Header("Debuff")]
    public BuffFactory appliedDebuff;
    public override void AffectEnemy(Enemy affectedEnemy)
    {
        if (affectedEnemy && affectedEnemy.healthController.isAlive)
        {
            affectedEnemy.healthController.TakeDamage(damage);
            appliedDebuff.GetBuff(affectedEnemy).Apply();
        }
    }
}
