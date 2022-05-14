using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : Item
{

    [SerializeField] bool useMove;

    public override bool Use(GameObject user){
        user.GetComponent<CharacterObject>().ApplyStatusEffect(StatusEffect.Protected);
        return useMove;
    }
}
