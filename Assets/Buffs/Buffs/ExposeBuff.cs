using System.Collections;
using UnityEngine;

public class ExposeBuff : Buff<ExposeBuffData>
{

    public override void Apply()
    {
        targetForBuff.StartCoroutine(ExposeRoutine());
    }

    public IEnumerator ExposeRoutine()
    {
        targetForBuff.rend.material.EnableKeyword("NEGATIVE_ON");
        targetForBuff.healthController.damageMultiplier = data.exposeAmount;

        float startTime = Time.time;
        while (Time.time - startTime <= data.duration)
        {
            yield return new WaitForSeconds(data.tickTime);
        }
        targetForBuff.rend.material.DisableKeyword("NEGATIVE_ON");
        targetForBuff.healthController.damageMultiplier = 1f;

    }
}

