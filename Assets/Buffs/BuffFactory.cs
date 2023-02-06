using System;
using UnityEngine;

[Serializable]
public class BuffFactory<DataType, BuffType> : BuffFactory 
    where BuffType : Buff<DataType>, new()
{
    [SerializeField]
    public DataType data;

    public override Buff GetBuff(Enemy target)
    {
        return new BuffType { data = this.data, target = target };
    }
}

public abstract class BuffFactory : ScriptableObject
{
    public abstract Buff GetBuff(Enemy target);
}

