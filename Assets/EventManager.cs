using System;
using UnityEngine;

public class Status
{

}

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void enemyAction(Enemy e);
    public event enemyAction onEnemyDie;
    public event enemyAction onEnemySpawn;

    public delegate void enemyBuffChange(Enemy e, Status s, bool isActive);
    public event enemyBuffChange onEnemyStatusChange;

    public void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            Debug.LogError("Two Event Managers in Scene.");
        }
        else
        {
            instance = this;
        }
    }

    

    public void OnEnemyDie(Enemy e)
    {
        if (onEnemyDie != null)
        {
            onEnemyDie.Invoke(e);
        }
    }

    public void OnEnemySpawn(Enemy e)
    {
        if (onEnemyDie != null)
        {
            onEnemySpawn.Invoke(e);
        }
    }
}
