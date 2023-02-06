using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public StatusBar prefab;
    public RectTransform parent;

    public void OnEnable() => EventManager.instance.onEnemySpawn += SetupBuffBar;
    public void OnDisable() => EventManager.instance.onEnemySpawn -= SetupBuffBar;

    public void HandleStatusGain()
    {

    }

    private void SetupBuffBar(Enemy obj)
    {
        StatusBar bar = Instantiate(prefab);
        bar.transform.SetParent(parent);

        if (bar.TryGetComponent(out FollowTarget follow))
        {
            follow.SetTarget(obj.transform);
        }

        //obj.healthController.onStatusChange += bar.HandleStatusChange;
    }
}
