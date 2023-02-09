using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void enemyAction(Enemy e);
    public event enemyAction onEnemyDie = delegate { };
    public event enemyAction onEnemySpawn = delegate { };

    public delegate void enemyBuffChange(Enemy e, Status s, bool isActive);
    public event enemyBuffChange onEnemyStatusChange = delegate { };

    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            Debug.LogError("Two Event Managers in Scene.");
        }
        else
        {
            Debug.Log("Setting up EventManager instance");
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
