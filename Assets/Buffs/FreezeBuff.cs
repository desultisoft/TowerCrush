using System.Collections;
using UnityEngine;

public class FreezeBuff : Buff<FreezeBuffData>
{

    public override void Apply()
    {
        targetForBuff.StartCoroutine(FreezeRoutine());
    }

    public IEnumerator FreezeRoutine()
    {
        targetForBuff.rend.material.EnableKeyword("OVERLAY_ON");
        targetForBuff.rend.material.SetTexture("_OverlayTex", data.freezeTexture);

        targetForBuff.anim.speed = 0;
        targetForBuff.pathController.ChangeSpeed(-100);

        float startTime = Time.time;
        while (Time.time - startTime <= data.duration)
        {
            yield return new WaitForSeconds(data.tickTime);
        }

        targetForBuff.pathController.ChangeSpeed(100);
        targetForBuff.anim.speed = 1;

        targetForBuff.rend.material.DisableKeyword("OVERLAY_ON");
    }
}

