using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class StormTower : ClickerTower
{
    public LayerMask enemyMask;
    public int damage;
    public int maxEnemiesHit = 25;
    private int hitCount;
    private Collider2D[] hitEnemies;
    private Vector3 WorldPosition;
    private Enemy targetEnemy;

    public void Start()
    {
        hitEnemies = new Collider2D[maxEnemiesHit];
    }

    public override void OnClick()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        WorldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        WorldPosition.x = Mathf.RoundToInt(WorldPosition.x);
        WorldPosition.y = Mathf.RoundToInt(WorldPosition.y);
        WorldPosition.z = 0;

        ObjectPooler.instance.SpawnFromPool("Lightning", WorldPosition, Quaternion.identity);

        hitCount = Physics2D.OverlapCircleNonAlloc(WorldPosition, range, hitEnemies, enemyMask);
        if (hitCount > 0)
        {

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i] == null) continue;
                targetEnemy = hitEnemies[i].GetComponent<Enemy>();
                if (targetEnemy)
                {
                    targetEnemy.healthController.TakeDamage(damage);
                }
            }
        }
    }
}
