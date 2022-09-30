using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMain : CharacterObject
{
    [SerializeField] protected AIMovement movement;
    [SerializeField] protected AITargeting targeting;
    [SerializeField] protected AIAttack attack;

    [SerializeField]
    protected int shieldDrop;
    [SerializeField]
    protected int shieldRange;

    [SerializeField] GameObject drop;

    void Start(){
        Register();
        // StatusIcon.Create(transform, true, StatusEffect.Sharpened);
    }
    public override bool PlayTurn(){
        return base.PlayTurn();
    }

    protected override void Die(){
        foreach(GameObject character in GameObject.FindGameObjectsWithTag("Character")){
            if (character.GetComponent<CharacterObject>().isPlayer && Vector3.Distance(transform.position, character.transform.position) <= shieldRange){
                character.GetComponent<PlayerInput>().GiveShield(shieldDrop);       
            }
        }
        if (drop != null){
            GameObject item = Instantiate(drop, transform.position, Quaternion.identity);
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
        base.Die();
    }
}
