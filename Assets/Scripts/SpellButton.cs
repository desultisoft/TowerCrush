using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    private SpellTower tower;
    public Image icon;
    public Outline outline;
    public Image timer;
    private Color baseColor;
    private Color color;
    public void Start()
    {
        baseColor = timer.color;
    }

    public void Use()
    {
        if (tower)
        {
            tower.CastSpell();
        }
    }

    public void SetTower(SpellTower added)
    {
        tower = added;
        if (tower != null)
        {
            icon.sprite = tower.data.icon;
        }
        else
        {
            icon.sprite = null;
        }
    }

    public void Update()
    {
        if (tower)
        {
            if(tower is ClickerTower c)
            {
                if(c.duration > 0)
                {
                    color = new Color(1,0,0,0.5f);
                    timer.color = color;

                    timer.fillAmount = c.duration / c.maxDuration;
                }
                else
                {
                    timer.color = baseColor;
                    timer.fillAmount = tower.currentReloadTimer / tower.maxReloadTime;
                }
            }
            else
            {
                timer.fillAmount = tower.currentReloadTimer / tower.maxReloadTime;
            }
        }
        else
        {
            timer.fillAmount = 1;
        }
        
    }
}