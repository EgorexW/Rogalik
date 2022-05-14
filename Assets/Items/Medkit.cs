using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Item
{

    [SerializeField] int healValue;
    [SerializeField] bool useMove;

    public override bool Use(GameObject user){
        user.GetComponent<CharacterObject>().Heal(healValue);
        return useMove;
    }
}
