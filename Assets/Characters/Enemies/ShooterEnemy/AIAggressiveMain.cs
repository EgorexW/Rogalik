using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggressiveMain : AIMain
{

    private GameObject target;

    public override bool PlayTurn(){
        bool moved = false;
        if (!base.PlayTurn()){
            return false;
        }
        target = targeting.GetTarget();
        if (target){
            moved = true;
            if (!attack.Attack(target)){
                movement.Move(target);
            }
        }
        else
        {
            movement.Idle();
        }
        return moved;
    }

    protected override void Die(){
        attack.DropWeapon();
        base.Die();
    }
}
