using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaTower : Tower
{
    public int damage = 18;
    public float effectTime = 1f;
    public float checkTime = 0.1f;
    public float maxCheckTime = 1f;
    public int maxEnemiesHit = 25;
    public LayerMask enemyMask;

    private Enemy targetEnemy;
    private Collider2D[] hitEnemies;
    private int hitCount;
    

    public void Start()
    {
        hitEnemies = new Collider2D[maxEnemiesHit];
    }

    public virtual void Update()
    {
        checkTime -= Time.deltaTime;
        currentReloadTimer -= Time.deltaTime * reloadSpeed;
        
        if(checkTime <= 0 && currentReloadTimer <=0)
        {
            hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, hitEnemies, enemyMask);
            if (hitCount > 0)
            {
                visualEffect();

                currentReloadTimer = maxReloadTime;
                
                for (int i = 0; i < hitEnemies.Length; i++)
                {
                    if (hitEnemies[i] == null) continue;
                    targetEnemy = hitEnemies[i].GetComponent<Enemy>();
                    if (targetEnemy)
                    {
                        AffectEnemy(targetEnemy);
                    }
                }
            }
            else
            {
                NoEnemyFound();
            }
            
            checkTime = maxCheckTime;
        }
    }
    public abstract void visualEffect();
    public virtual void NoEnemyFound() { }
    public virtual void AffectEnemy(Enemy affectedEnemy)
    {
        affectedEnemy.healthController.TakeDamage(damage);
    }
}
