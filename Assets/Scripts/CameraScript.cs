using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 SelectedWorldPosition;
    float mDelta = 25; // Pixels. The width border at the edge in which the movement work
    public float mSpeed = 20.0f; // Scale. Speed of the movement
    public Vector2 minPosition;
    public Vector2 maxPosition;

    public void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        SelectedWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= Screen.width - mDelta && SelectedWorldPosition.x < maxPosition.x)
        {
            // Move the camera
            transform.position += transform.right * Time.deltaTime * mSpeed;
        }
        else if (mousePos.x <= 0 + mDelta && SelectedWorldPosition.x > minPosition.x)
        {
            // Move the camera
            transform.position -= transform.right * Time.deltaTime * mSpeed;
        }

        if (mousePos.y >= Screen.height - mDelta && SelectedWorldPosition.y < maxPosition.y)
        {
            // Move the camera
            transform.position += transform.up * Time.deltaTime * mSpeed;
        }
        else if (mousePos.y <= 0 + mDelta && SelectedWorldPosition.y > minPosition.y)
        {
            // Move the camera
            transform.position -= transform.up * Time.deltaTime * mSpeed;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.Lerp(minPosition, maxPosition, 0.5f), maxPosition-minPosition);
    }

}
