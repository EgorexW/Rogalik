using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMainGhost : AIAggressiveMain
{
    new void Start(){
        getsStunned = false;
        base.Start();
    }


    public override Damage Damage(Damage dmg)
    {
        if (!dmg.crit){
            dmg = new Damage(0, false);
        }
        return base.Damage(dmg);
    }
}
