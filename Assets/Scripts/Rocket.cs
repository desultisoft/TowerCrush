using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : Bullet
{
    Rigidbody2D rb;
    public float angleChangingSpeed;
    Vector2 direction;
    public float acceleration;
    public float maxSpeed = 3;
    public GameObject explosion;
    public GameObject rocket;
    public float ExplosionRadius;
    public int maxEnemiesHit;
    public float maxOffAngle = 60f;
    public LayerMask enemyMask;
    void FixedUpdate()
    {
        if (TargetEnemy== null || !TargetEnemy.healthController.isAlive)
        {
            Destroy(gameObject);
            return;
        }

        speed = Mathf.Clamp(speed+ acceleration, 0, maxSpeed);
        
        direction = (Vector2)TargetEnemy.transform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction.normalized, transform.up).z;

        //Debug.Log("Rotating: " + rotateAmount);
        rb.angularVelocity = -angleChangingSpeed * rotateAmount;
        rb.velocity = transform.up * speed;
    }

    public void CreateExplosion()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.Sleep();
        explosion.transform.localScale *= ExplosionRadius * Random.Range(0.5f, 1f);
        rocket.SetActive(false);
        explosion.SetActive(true);

        Collider2D[] hitEnemies = new Collider2D[maxEnemiesHit];
        Physics2D.OverlapCircleNonAlloc(transform.position, ExplosionRadius, hitEnemies, enemyMask);
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i] == null) continue;
            TargetEnemy = hitEnemies[i].GetComponent<Enemy>();
            if (TargetEnemy)
            {
                TargetEnemy.healthController.TakeDamage(damage);
            }
        }

        Destroy(gameObject,0.7f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy theEnemy = collision.GetComponent<Enemy>();
        if (theEnemy)
        {
            CreateExplosion();
        }
    }

    // Use this for initialization
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void SetTarget(Enemy targetEnemy)
    {
        Destroy(gameObject, 10f);
        TargetEnemy = targetEnemy;
        if (TargetEnemy == null || !TargetEnemy.healthController.isAlive)
        {
            Destroy(gameObject);
            return;
        }
           
        

        Vector3 difference = TargetEnemy.transform.position - transform.position;
        float offset = Random.Range(2, -2) * maxOffAngle;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + offset;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
    }
}
