using System;

[Serializable]
public abstract class Buff
{
    public abstract void Apply();
}

[Serializable]
public abstract class Buff<DataType> : Buff
{
    public DataType data;
    public Enemy targetForBuff;
}

