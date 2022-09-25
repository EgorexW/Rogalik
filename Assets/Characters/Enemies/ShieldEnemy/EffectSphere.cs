using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSphere : AIAttack
{

    [SerializeField] float sphereRange;
    [SerializeField] StatusEffect effect;

    public override bool Attack(GameObject target)
    {
        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (GetComponent<CharacterObject>().CheckIfFriendOrFoe(character) != FriendOrFoe.Friend){
                continue;
            }
            if (Vector2.Distance(transform.position, character.transform.position) > sphereRange){
                continue;
            }
            if (character.GetComponent<CharacterObject>().statusEffects.Contains(effect)){
                continue;
            }
            character.GetComponent<CharacterObject>().ApplyStatusEffect(effect);
        }
        return true;
    }
}
