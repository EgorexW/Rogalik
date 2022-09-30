using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackMelee : AIAttack
{

    [SerializeField] Damage dmg;

    public override bool Attack(GameObject target){
        if (Vector2.Distance(transform.position, target.transform.position) <= 1){
            target.GetComponent<CharacterObject>().Damage(dmg);
            return true;
        }
        return false;
    }
}
