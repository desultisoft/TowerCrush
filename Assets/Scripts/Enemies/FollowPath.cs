using System.Collections;
using UnityEngine;


public class FollowPath
{
    private Transform toMove;
    private Vector3 targetPosition
    {
        get
        {
            if (currentPathNumber < Path.instance.pathPositions.Count)
            {
                return Path.instance.pathPositions[currentPathNumber];
            }
            else
            {
                return Vector3.zero;
            }
        }
    }


    private float distanceForNextPathPiece = 0.2f;
    private int currentPathNumber = 0;
    private Vector3 direction;

    private float maxSpeed;
    private float currentSpeed;

    public FollowPath(Transform toMove, float speed)
    {
        this.toMove = toMove;
        maxSpeed = speed;

        Reset();
    }

    public void Reset()
    {
        currentSpeed = maxSpeed;
        currentPathNumber = 0;
    }

    public void ChangeSpeed(float speedChange)
    {
        currentSpeed = currentSpeed + speedChange; 
    }

    public void Tick()
    {
        if (currentSpeed <= 0) return;

        if (targetPosition == null)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = (targetPosition - toMove.position);
            if (direction.magnitude <= distanceForNextPathPiece)
            {
                currentPathNumber = Mathf.Clamp(currentPathNumber + 1, 0, Path.instance.pathPositions.Count - 1);
            }
        }

        toMove.position += direction.normalized * currentSpeed * Time.deltaTime;
    }
}
