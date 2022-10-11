using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : Weapon
{

    [SerializeField] int shieldGain;

    public override GameObject Fire(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        if (ammo <= 0){
            return null;
        }
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, fireLayerMask);
        if (collider.gameObject.GetComponent<CharacterObject>().isPlayer) 
            collider.gameObject.GetComponent<PlayerInput>().GiveShield(shieldGain);
        return base.Fire(dir);
    }
}
