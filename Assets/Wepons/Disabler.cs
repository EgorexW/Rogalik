using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : Weapon
{

    [SerializeField] StatusEffect status;

    public override GameObject Fire(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        GameObject target = base.Fire(dir, weaponDamageMod);
        // Debug.Log(target);
        if (target != null){
            target.GetComponent<CharacterObject>().ApplyStatusEffect(status);
        }
        return target;
    }
}
