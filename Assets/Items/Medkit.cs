using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Item
{

    [SerializeField] int healValue;
    [SerializeField] bool useMove;
    [SerializeField] HealType healType; 

    public override bool Use(GameObject user){
        user.GetComponent<CharacterObject>().Heal(healValue, healType);
        return useMove;
    }
}
