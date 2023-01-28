using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerDataDefault")]
public class TowerData : ScriptableObject
{
    [Header("UI Data")]
    public Sprite icon;
    public List<Tower> upgrades;
    public int cost;
}