using System.Collections;
using UnityEngine;

public class BurnBuff : Buff<BurnBuffData>
{
    public override void Apply()
    {
        targetForBuff.StartCoroutine(BurnCoroutine());
    }

    public IEnumerator BurnCoroutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= data.duration)
        {
            yield return new WaitForSeconds(data.tickTime);
            targetForBuff.healthController.TakeDamage(data.damagePerTick);
        }
    }
}

