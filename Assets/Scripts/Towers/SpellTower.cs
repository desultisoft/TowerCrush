using UnityEngine;

public abstract class SpellTower : Tower
{
    public virtual void Update()
    {
        currentReloadTimer -= reloadSpeed *Time.deltaTime;
    }

    public virtual void CastSpell()
    {
        if(currentReloadTimer <= 0)
        {
            Spell();
        }
    }

    public virtual void Spell() { currentReloadTimer = maxReloadTime; }
}
