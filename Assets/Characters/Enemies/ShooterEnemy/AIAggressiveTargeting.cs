using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggressiveTargeting : AITargeting
{
    public override GameObject GetTarget(){
        List<GameObject> characters = new List<GameObject>();
        characters.AddRange(GameObject.FindGameObjectsWithTag("Character"));
        float lowDistance = Mathf.Infinity;
        GameObject bestTarget = null;
        foreach (GameObject character in characters)
        {
            if (GetComponent<AIMain>().CheckIfFriendOrFoe(character) != FriendOrFoe.Foe){
                continue;
            }
            float x = transform.position.x - character.transform.position.x;
            float y = transform.position.y - character.transform.position.y;
            float distance = Mathf.Abs(x) + Mathf.Abs(y);
            // Debug.Log(distance);
            if (distance < lowDistance && distance < seeRange){
                lowDistance = distance;
                bestTarget = character;
            }
        }
        // Debug.Log(bestTarget);
        return bestTarget;
    }
}
