using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    PlayerMain PM;
    [SerializeField]
    PlayerInventory PI;

    public void UseWeapon(){
        WeaponDamageMod damageMod = StatusEffects.GetDamageMod(gameObject);
        if (GetComponent<PlayerInput>().rotated){
            damageMod.damageMod -= 1;
        }
        if (PI.GetWeapon() != null){
            PI.GetWeapon().GetComponent<Weapon>().Fire(PM.dir, damageMod);
        }
        PI.UpdateInventoryUI();
    }
}
