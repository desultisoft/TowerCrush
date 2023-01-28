using UnityEngine;

public class PiercingBullet : Bullet
{
    private Vector3 Dir;
    public override void SetDir(Vector3 dir)
    {
        Dir = Vector3.Normalize(dir);
        Destroy(gameObject, 3);
    }
    public void Update()
    {
        transform.position += Dir*speed*Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy theEnemy = collision.GetComponent<Enemy>();
        if(theEnemy)
            theEnemy.healthController.TakeDamage(damage);
    }
}
