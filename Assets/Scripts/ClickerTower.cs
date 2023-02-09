using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ClickerTower : SpellTower
{
    public float maxDuration;
    public float duration { get; private set; }
    public bool isActive { get; private set; }

    public override void Spell()
    {
        duration = maxDuration;
    }

    public override void CastSpell()
    {
        if (currentReloadTimer <= 0 && duration <= 0)
        {
            Spell();
        }
    }

    public override void Update()
    {
        

        //Spell is active
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            //See if we are casting and do the onclick for this tower.
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnClick();
            }

            //If we have hit the end of the duration begin the cooldown.
            if (duration <= 0)
            {
                currentReloadTimer = maxReloadTime;
            }
        }
        else
        {
            currentReloadTimer -= reloadSpeed*Time.deltaTime;
        }

        

    }

    public abstract void OnClick();
}