using UnityEngine;

public class AngelDetector : MonoBehaviour 
{
    public Angel target;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out target);
    }
}