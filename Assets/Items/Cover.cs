using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : Item
{

    [SerializeField] bool useMove;

    [SerializeField] StatusEffect status;

    [SerializeField] int nrOfEffects = 1;

    public override bool Use(GameObject user){
        for (int i = 0; i < nrOfEffects; i++){
            user.GetComponent<CharacterObject>().ApplyStatusEffect(status);
        }
        return useMove;
    }
}
