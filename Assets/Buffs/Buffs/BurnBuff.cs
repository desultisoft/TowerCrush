using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBuff : Buff<BurnBuffData>
{
    public static Dictionary<Enemy, int> BurningEnemies = new Dictionary<Enemy, int>();
    
    public override void Apply()
    {
        
        targetForBuff.StartCoroutine(BurnCoroutine());
    }

    public IEnumerator BurnCoroutine()
    {
        targetForBuff.rend.material.EnableKeyword("ALPHACUTOFF_ON");
        targetForBuff.rend.material.EnableKeyword("OUTBASE_ON");

        targetForBuff.rend.material.SetColor("_OutlineColor", data.burnColor);
        targetForBuff.rend.material.EnableKeyword("OUTBASE8DIR_ON");
        targetForBuff.rend.material.EnableKeyword("OUTDIST_ON");
        targetForBuff.rend.material.SetFloat("_OutlineDistortAmount", 0.1f);


        if (!BurningEnemies.ContainsKey(targetForBuff))
        {
            BurningEnemies.Add(targetForBuff, 1);
        }
        else
        {
            BurningEnemies[targetForBuff]++;
        }

        float startTime = Time.time;
        while (Time.time - startTime <= data.duration)
        {
            yield return new WaitForSeconds(data.tickTime);
            targetForBuff.healthController.TakeDamage(data.damagePerTick);
        }

        if (BurningEnemies.ContainsKey(targetForBuff))
        {
            BurningEnemies[targetForBuff]--;
        }

        //If there are no burn buffs left on the enemy.
        if(BurningEnemies[targetForBuff] <= 0)
        {
            //Remove them from the map.
            BurningEnemies.Remove(targetForBuff);

            //Tell them they aren't burning.
            targetForBuff.rend.material.DisableKeyword("ALPHACUTOFF_ON");
            targetForBuff.rend.material.DisableKeyword("OUTBASE_ON");
        }
    }
}
