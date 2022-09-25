using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    static EnemyCollection enemyCollection = new EnemyCollection();
    private WorldGridMap pathFind;
    public LayerMask moveLayer;
    
    void Start(){
        GameObject[] paths = GameObject.FindGameObjectsWithTag("WorldGridMap");
        pathFind = paths[0].GetComponent<WorldGridMap>();
    }

    public void Move(GameObject target){
        Debug.Log(target);
        Move(target.transform.position);
    }

    public void Move(Vector3 target){
        List<Vector3> path = pathFind.FindPath(transform.position, target);
        if (path != null && path.Count > 1){
            Vector3 toMove = path[1];
            if (Physics2D.OverlapCircle(toMove, 0.1f, moveLayer) == null){
                Vector2 dir = new Vector2(toMove.x - transform.position.x, toMove.y - transform.position.y);
                transform.position = toMove;
                transform.rotation = enemyCollection.Rotate(dir);
            }
        }
    }

    public void Idle(){

    }
}
