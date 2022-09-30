using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMainGhost : AIAggressiveMain
{
    public override Damage Damage(Damage dmg)
    {
        if (!dmg.crit){
            dmg = new Damage(0, false);
        }
        return base.Damage(dmg);
    }
}
