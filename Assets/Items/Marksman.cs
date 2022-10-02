using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marksman : Item
{

    [SerializeField] StatusEffect statusEffect;

    public override void OnEquip(GameObject user){
        user.GetComponent<CharacterObject>().ApplyStatusEffect(statusEffect);
    }

    public override void OnDrop(GameObject user){
        user.GetComponent<CharacterObject>().RemoveStatusEffect(statusEffect);
    }
}
