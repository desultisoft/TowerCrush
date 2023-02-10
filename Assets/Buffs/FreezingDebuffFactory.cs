using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Custom/Data/Debuff/Freeze")]
public class FreezingDebuffFactory : BuffFactory<FreezeBuffData, FreezeBuff> { }