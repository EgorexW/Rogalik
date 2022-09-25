using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMain : CharacterObject
{
    private GameObject target;
    [SerializeField] protected AIMovement movement;
    [SerializeField] protected AITargeting targeting;
    [SerializeField] protected AIAttack attack;

    [SerializeField]
    protected int shieldDrop;
    [SerializeField]
    protected int shieldRange;

    void Start(){
        Register();
        // StatusIcon.Create(transform, true, StatusEffect.Sharpened);
    }
    protected virtual void Update(){
        // Debug.Log("Current turn= " + turnController.currentTurn);
        if (turnController.GetCurrentTurn() == onTurn){
            if (moved == false){
                if (StatusEffects.StartTurnCheck(gameObject)){
                    return;
                }
                target = targeting.GetTarget();
                if (target){
                    if (!attack.Attack(target)){
                        movement.Move(target);
                    }
                }
                else
                {
                    movement.Idle();
                }
                moved = true;
            }
        }
    }

    protected override void Die(){
        foreach(GameObject character in GameObject.FindGameObjectsWithTag("Character")){
            if (character.GetComponent<CharacterObject>().isPlayer && Vector3.Distance(transform.position, character.transform.position) <= shieldRange){
                character.GetComponent<PlayerInput>().GiveShield(shieldDrop);       
            }
        }
        if (attack !=  null){
            attack.DropWeapon();
        }
        base.Die();
    }
}
