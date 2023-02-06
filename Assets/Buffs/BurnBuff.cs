using System.Collections;
using UnityEngine;

public class BurnBuff : Buff<BurnBuffData>
{
    public override void Apply()
    {
        target.StartCoroutine(BurnCoroutine());
    }

    public IEnumerator BurnCoroutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= data.duration)
        {
            yield return new WaitForSeconds(data.tickTime);
            //target.DealDamage(data.damagePerTick);
        }
    }
}

