using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    public static Path instance;
    public Tilemap tilemap;
    private List<Vector3Int> TilePositions = new List<Vector3Int>();
    public List<Vector2> pathPositions = new List<Vector2>();

    public void Awake()
    {
        instance = this;
        pathPositions = GetPathPositions();
    }

    public void OnDrawGizmosSelected()
    {
        foreach (var position in pathPositions)
        {
            Gizmos.DrawIcon(position, "pos");
        }
    }

    public List<Vector2> GetPathPositions()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                TilePositions.Add(position);
            }
        }
        Vector3Int currentPos = TilePositions.OrderBy(x => x.x).FirstOrDefault();
        Vector3Int prevPos = Vector3Int.zero;
        TileBase currentTile = tilemap.GetTile(currentPos);
        List<Vector3Int> pathPoints = new List<Vector3Int>();
        List<Vector2> pathPoints2 = new List<Vector2>();
        for (int i = 0; i < TilePositions.Count; i++)
        {
            if (tilemap.GetTile(currentPos + Vector3Int.right) && !pathPoints.Contains(currentPos + Vector3Int.right))
            {
                currentPos = currentPos + Vector3Int.right;
            }
            else if (tilemap.GetTile(currentPos + Vector3Int.up)&&!pathPoints.Contains(currentPos + Vector3Int.up))
            {
                currentPos = currentPos + Vector3Int.up;
            }
            else if (tilemap.GetTile(currentPos + Vector3Int.down) && !pathPoints.Contains(currentPos + Vector3Int.down))
            {
                currentPos = currentPos + Vector3Int.down;
            }
            else if (tilemap.GetTile(currentPos + Vector3Int.left) && !pathPoints.Contains(currentPos + Vector3Int.left))
            {
                currentPos = currentPos + Vector3Int.left;
            }

            if (prevPos != currentPos)
            {
                Vector3 newcurrentPos = (currentPos);

                newcurrentPos += Vector3.one * 0.5f;

                pathPoints.Add(currentPos);
                pathPoints2.Add(newcurrentPos);
            }

            prevPos = currentPos;
        }
        return pathPoints2;
    }
}