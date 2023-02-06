using System;

[Serializable]
public abstract class Buff
{
    public abstract void Apply();
    public Buff() { }
}

[Serializable]
public abstract class Buff<DataType> : Buff
{
    public DataType data;
    public Enemy target;
    public Buff() { }
}

