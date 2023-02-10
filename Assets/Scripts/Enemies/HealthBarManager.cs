using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public ProgressBar prefab;
    public RectTransform parent;

    public void OnEnable() => EventManager.instance.onEnemySpawn += SetupHealthBar;
    public void OnDisable() => EventManager.instance.onEnemySpawn -= SetupHealthBar;

    private void SetupHealthBar(Enemy obj)
    {
        ProgressBar bar = Instantiate(prefab);
        bar.transform.SetParent(parent);
        if(bar.TryGetComponent(out FollowTarget follow))
        {
            follow.SetTarget(obj.transform);
        }
        
        obj.healthController.onHealthPercentReached += bar.SetProgress;
        obj.healthController.onLifeStatusChange += bar.HandleStatusChange;
    }
}
