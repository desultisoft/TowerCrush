using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HomingBullet : Bullet
{
    //Move toward our target once per frame!
    void Update()
    {
        if (!TargetEnemy || !TargetEnemy.healthController.isAlive)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = TargetEnemy.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized*distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        TargetEnemy.healthController.TakeDamage(damage);
        Destroy(gameObject);
    }

    public override void SetTarget(Enemy theEnemy)
    {
        TargetEnemy = theEnemy;
    }
}
