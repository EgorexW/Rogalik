using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISupportMain : AIMain
{

    Vector3 targetPos;

    public override bool PlayTurn()
    {
        bool moved = false;
        if (!base.PlayTurn()){
            return false;
        }
        targetPos = targeting.GetTargetPos();
        if (targetPos != transform.position){
            movement.Move(targetPos);
            moved = true;
        }
        attack.Attack(null);
        return moved;
    }
}
