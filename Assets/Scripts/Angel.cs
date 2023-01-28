using UnityEngine;
using UnityEngine.Events;
public class Angel : MonoBehaviour, IDamageable
{
    public Health health;
    public Animator anim;

    public void Damage(int attackPower)
    {
        health.TakeDamage(attackPower);

        if (health.health <= 0)
        {
            anim.SetTrigger("Die");
            GameManager.instance.GameOver();
        }
    }
}
