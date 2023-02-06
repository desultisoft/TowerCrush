using System.Collections;
using UnityEngine;

public class StrengthPotionBuff : Buff<StrengthPotionBuffData>
{
    public override void Apply()
    {
        //target.AddStrength(data.strengthToAdd);
        target.StartCoroutine(UnapplicationCoroutine());
    }

    public IEnumerator UnapplicationCoroutine()
    {
        yield return new WaitForSeconds(data.duration);
        //target.RemoveStrength(data.strengthToAdd);
    }
}

