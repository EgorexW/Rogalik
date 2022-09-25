using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITargeting : MonoBehaviour
{
    [SerializeField] protected LayerMask charLayerMask;
    [SerializeField] protected float seeRange;

    public virtual GameObject GetTarget(){
        return null;
    }

    public virtual Vector3 GetTargetPos(){
        return GetTarget().transform.position;
    }
}

