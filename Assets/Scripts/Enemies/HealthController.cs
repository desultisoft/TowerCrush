using System;
using UnityEngine;

//A Re-Usable health Controller class that follows an observer pattern and is good for composition
public class HealthController 
{
    private float health;
    private float maxHealth;
    public bool isAlive => (health > 0);
    public event Action onTakeDamage = delegate { };
    public event Action<bool> onLifeStatusChange = delegate { };
    public event Action<float> onHealthPercentReached = delegate { };

    public HealthController(float totalHealth)
    {
        maxHealth = totalHealth;
        Reset();
        
    }

    public void Reset()
    {
        health = maxHealth;
        onLifeStatusChange.Invoke(true);
    }

    public void TakeDamage(float amountDamage)
    {
        if(isAlive)
        {
            onTakeDamage.Invoke();
            health = Mathf.Clamp(health - amountDamage, 0, maxHealth);
            onHealthPercentReached.Invoke(health / maxHealth);
            if (health == 0)
            {
                health = -1;
                onLifeStatusChange.Invoke(false);
            }
        }
    }
}
