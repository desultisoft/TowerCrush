using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;
    public TowerDetector tDetector;

    [Header("Reload")]
    public float reloadSpeed = 1;
    public float maxReloadTime = 3;
    public float currentReloadTimer { protected set; get; }
    [Header("Range")]
    public float range = 3;

    public int TotalValue { get; set; }

    public IEnumerator buffCooldown(float duration, float amount)
    {
        reloadSpeed = Mathf.Clamp(reloadSpeed + amount, 0.1f, 3);
        yield return new WaitForSeconds(duration);
        reloadSpeed = Mathf.Clamp(reloadSpeed - amount, 0.1f, 3);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
