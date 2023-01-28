using System.Collections;
using UnityEngine;

public class TimeBullet : Bullet
{
    public float buffAmount = 0.1f;
    public float duration;
    private Vector3 Dir;
    public override void SetTarget(Enemy enemy)
    {
        
        if (enemy)
        {
            Destroy(gameObject, 5);
            Dir = Vector3.Normalize(enemy.transform.position - transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void Update()
    {
        transform.position += Dir * speed * Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy theEnemy = collision.GetComponent<Enemy>();
        if (theEnemy)
        {
            //theEnemy.pathController.StartCoroutine(theEnemy.pathController.Slow(duration, buffAmount));
            theEnemy.healthController.TakeDamage(damage);
        }
            

        TowerDetector towerDetector = collision.GetComponent<TowerDetector>();
        if (towerDetector)
        {
            towerDetector.owner.StartCoroutine(towerDetector.owner.buffCooldown(duration, buffAmount));
        }
            
    }
}
