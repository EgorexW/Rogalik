using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AITargeting : MonoBehaviour
{
    public LayerMask charLayerMask;
    public float seeRange;

    public GameObject GetTarget(){
        List<GameObject> targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        float lowDistance = Mathf.Infinity;
        GameObject bestTarget = null;
        foreach (GameObject target in targets)
        {
            float x = transform.position.x - target.transform.position.x;
            float y = transform.position.y - target.transform.position.y;
            float distance = (float)Math.Sqrt(x*x + y*y);
            if (distance < lowDistance && distance < seeRange){
                lowDistance = distance;
                bestTarget = target;
            }
        }
        return bestTarget;
    }
}
