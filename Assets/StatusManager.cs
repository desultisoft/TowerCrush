using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusHandler
{
    public Enemy target;
    public List<Status> status;

    public StatusHandler(Enemy target)
    {
        this.target = target;
    }

    public void Tick()
    {
        if (target.gameObject.activeInHierarchy)
        {
            foreach (Status s in status)
            {
                s.Tick(target);
            }
        }
    }
}

public class StatusManager : MonoBehaviour
{
    public StatusBar prefab;
    public RectTransform parent;
    public List<StatusHandler> StatusHandlers;
    public void Awake()
    {
        StatusHandlers = new List<StatusHandler>();
    }
    public void OnEnable()
    {
        EventManager.instance.onEnemySpawn += SetupStatus;
        EventManager.instance.onEnemyStatusChange += HandleStatusChange;
    }
    public void OnDisable()
    {
        EventManager.instance.onEnemySpawn -= SetupStatus;
        EventManager.instance.onEnemyStatusChange -= HandleStatusChange;
    }


    private void HandleStatusChange(Enemy e, Status s, bool isActive)
    {
        
    }

    public IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < StatusHandlers.Count; i++)
            {
                StatusHandlers[i].Tick();
            }
        }
    }


    private void SetupStatus(Enemy obj)
    {
        //Create a bar to represent the status's
        StatusBar bar = Instantiate(prefab);
        bar.transform.SetParent(parent);
        if (bar.TryGetComponent(out FollowTarget follow))
        {
            follow.SetTarget(obj.transform);
        }

        //Create a StatusHandler to represent the status's
        
    }
}
