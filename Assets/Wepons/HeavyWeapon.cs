using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWeapon : Weapon
{

    [SerializeField] StatusEffect shotingStatusEffect;

    public override string Fire(Vector2 dir, int damageMod = 0){
        if (ammo <= 0){
            return "Miss";
        }
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.1f, fireLayerMask);
        collider.gameObject.GetComponent<CharacterObject>().ApplyStatusEffect(shotingStatusEffect);
        return base.Fire(dir);
    }
}
