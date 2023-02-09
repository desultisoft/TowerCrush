using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TowerButton : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI cost;
    private Tower representedTower;

    public void Init(Tower tower)
    {
        representedTower = tower;
        image.sprite = representedTower.data.icon;
        cost.text = (tower.data.cost).ToString();
    }

    public void HandleClick()
    {
        if (TowerManager.instance.selectedTower)
        {
            TowerManager.instance.Upgrade(representedTower);
        }
        else
        {
            TowerManager.instance.SelectForBuilding(representedTower);
        }
    }
}
