using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyDetector : MonoBehaviour
{
    private CircleCollider2D col;

    public Enemy targetEnemy;
    private Enemy tempEnemy;

    void OnTriggerStay2D(Collider2D col)
    {
        if (!targetEnemy)
        {
            targetEnemy = col.GetComponent<Enemy>();
        }
            
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.TryGetComponent(out tempEnemy))
        {
            if (tempEnemy == targetEnemy)
                targetEnemy = null;
        }
    }
    private void Update()
    {
        if (targetEnemy && !targetEnemy.healthController.isAlive)
            targetEnemy = null;
    }

}
