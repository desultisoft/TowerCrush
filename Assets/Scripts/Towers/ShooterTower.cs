using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShooterTower : Tower
{
    public GameObject Turret;
    public Animator anim;
    public EnemyDetector eDetector;

    public void Rotate()
    {
        Vector3 difference = eDetector.targetEnemy.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Turret.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
    }

    public void Update()
    {
        currentReloadTimer -= cooldownSpeed*Time.deltaTime;

        if (eDetector.targetEnemy)
        {
            Rotate();
            if (currentReloadTimer <= 0)
            {
                currentReloadTimer = maxReloadTime;
                anim.SetTrigger("Shoot");
            }
        }
    }


}
