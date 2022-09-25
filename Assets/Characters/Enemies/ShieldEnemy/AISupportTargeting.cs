using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISupportTargeting : AITargeting
{
    public override Vector3 GetTargetPos(){
        Vector3 pos = transform.position;
        float lowDistance = Mathf.Infinity;
        bool alone = true;
        Vector2[] dirs = {new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};

        List<Vector3> positions = new List<Vector3>();
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character")){
            if(GetComponent<CharacterObject>().CheckIfFriendOrFoe(character) != FriendOrFoe.Friend){
                continue;
            }
            if(character == gameObject){
                continue;
            }
            alone = false;
            foreach (Vector2 dir in dirs){
                float x = character.transform.position.x + dir.x;
                float y = character.transform.position.y + dir.y;
                if (transform.position == new Vector3(x, y, 0)){
                    lowDistance = 0;
                    break;
                }
                if (Physics2D.OverlapCircle(new Vector2(x, y),
                    0.1f, GetComponent<AIMovement>().moveLayer) != null){
                        continue;    
                }
                float x_diff = transform.position.x - x;
                float y_diff = transform.position.y - y;
                float distance = Mathf.Abs(x_diff) + Mathf.Abs(y_diff);
                if (distance < lowDistance && distance < seeRange){
                    lowDistance = distance;
                    pos = new Vector3(x, y, 0);
            }
            }
        }
        if (alone){
            // Do design alone behaviour
        }
        // Debug.Log("Escaped");
        return pos;
    }
}
