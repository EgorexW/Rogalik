using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveProperties
{

    public bool canMove;
    public int moveDis;
    public bool useAction;
    
    public MoveProperties(bool canMove = true, int moveDis = 1, bool useAction = true){
        this.canMove = canMove;
        this.moveDis = moveDis;
        this.useAction = useAction;
    }
}
