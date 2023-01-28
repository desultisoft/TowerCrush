using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Enemy TargetEnemy;
    public float speed;
    [HideInInspector]
    public int damage;

    public virtual void SetTarget(Enemy targetEnemy) { }
    public virtual void SetDir(Vector3 dir) { }
}
