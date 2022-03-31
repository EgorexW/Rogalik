using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridMap : MonoBehaviour
{

    private Pathfinding pathFind;
    public int xWorldSize;
    public int yWorldSize;
    public LayerMask layerMask;

    void Start()
    {
        pathFind = new Pathfinding(xWorldSize + 1, yWorldSize + 1);
    }

    void Update()
    {
        for (int x = 0; x <= xWorldSize; x++) {
            for (int y = 0; y <= yWorldSize; y++) {
                Collider2D circle = Physics2D.OverlapCircle(new Vector2(x + 0.5f, y + 0.5f), 0.1f, layerMask);
                if (circle == null){
                    pathFind.GetNode(x, y).SetIsWalkable(true);
                }
                else{
                    pathFind.GetNode(x, y).SetIsWalkable(false);
                }
            }
        }
    }
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition){
        return pathFind.FindPath(startWorldPosition, endWorldPosition);
    }
}
