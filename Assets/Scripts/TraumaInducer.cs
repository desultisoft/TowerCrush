 using UnityEngine;
using System.Collections;

/* Example script to apply trauma to the camera or any game object */
public class TraumaInducer : MonoBehaviour 
{
    [Tooltip("Seconds to wait before trigerring the explosion particles and the trauma effect")]
    public float Delay = 1;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;

    public StressReceiver receiver;

    private IEnumerator Start()
    {
        /* Wait for the specified delay */
        yield return new WaitForSeconds(Delay);

        if(receiver == null)
            yield return null;

        float distance = Vector3.Distance(transform.position, receiver.transform.position);
        if(distance > Range)
            yield return null;

        float distance01 = Mathf.Clamp01(distance / Range);
        float stress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
        receiver.InduceStress(stress);
    }
}