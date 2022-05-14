using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
{
    public int damage;
    public bool crit;

    
    public Damage(int dmg, bool tmp_crit)
    {
        damage = dmg;
        crit = tmp_crit;
    }
}
