using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISupportMain : AIMain
{

    Vector3 targetPos;

    protected override void Update()
    {
        if (turnController.GetCurrentTurn() == onTurn){
            if (moved == false){
                if (StatusEffects.StartTurnCheck(gameObject)){
                    return;
                }
                targetPos = targeting.GetTargetPos();
                if (targetPos != transform.position){
                    movement.Move(targetPos);
                }
                attack.Attack(null);
                moved = true;
            }
        }
    }
}
