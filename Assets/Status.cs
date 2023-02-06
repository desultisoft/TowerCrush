using UnityEngine;

public abstract class Status : ScriptableObject
{
    public float duration;
    public Sprite image;

    public abstract void OnStatusStart(Enemy e);
    public abstract void OnStatusEnd(Enemy e);
    public virtual void Tick(Enemy e)
    {
        if(duration > 0)
        {
            duration -= 0.1f;
            if(duration <= 0)
            {
                OnStatusEnd(e);
            }
        }
    }
}
