using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMain : CharacterObject
{
    private GameObject target;
    [SerializeField] AIMovement movement;
    [SerializeField] AITargeting targeting;
    [SerializeField] AIAttack attack;

    [SerializeField]
    int shieldDrop;
    [SerializeField]
    int shieldRange;

    void Start(){
        Register();
        // StatusIcon.Create(transform, true, StatusEffect.Sharpened);
    }
    void Update(){
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
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= shieldRange){
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>().GiveShield(shieldDrop);
        }
        attack.DropWeapon();
        base.Die();
    }
}
