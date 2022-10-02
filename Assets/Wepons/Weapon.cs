using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponDamageMod
{
    public WeaponDamageMod(int damageMod = 0, int critChanceMod = 0, int critDamageMod = 0){
        this.damageMod = damageMod;
        this.critChanceMod = critChanceMod;
        this.critDamageMod = critDamageMod;
    }

    public int damageMod;
    public int critChanceMod;
    public int critDamageMod;
}
public class Weapon : MonoBehaviour
{
    public string displayName;

    public int fireRange;
    [SerializeField] int damage;
    [SerializeField] int critChance;

    [SerializeField] protected LayerMask fireLayerMask;

    public int ammo;
    public Sprite sprite;
    public WeaponTypes weaponType;
    
    public virtual string Fire(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        if (ammo <= 0){
            return "Miss";
        }
        int dmg = damage;
        RaycastHit2D raycats = Physics2D.Raycast(transform.position, dir, fireRange, fireLayerMask);
        ammo --;
        if (raycats.transform == null){
            return "Miss";
        }
        if (raycats.transform.tag == "Player" || raycats.transform.tag == "Character"){
            bool crit = false;
            if (Random.Range(1, 100) <= critChance - raycats.transform.GetComponent<CharacterObject>().GetResistance() + weaponDamageMod.critChanceMod){
                crit = true;
                dmg += weaponDamageMod.critDamageMod;
            }
            dmg += weaponDamageMod.damageMod;
            raycats.transform.GetComponent<CharacterObject>().Damage(new Damage(dmg, crit));
            if (raycats.transform == null){
                return "Kill";
            }
            return "Hit";
        }
        return "Miss";
    }

    public GameObject FirePreview(Vector2 dir){
        if (ammo <= 0){
            return null;
        }
        RaycastHit2D raycats = Physics2D.Raycast(transform.position, dir, fireRange, fireLayerMask);
        Debug.DrawRay(transform.position, dir * fireRange, Color.red, 100);
        // Debug.Log(raycats.transform);
        if (raycats.transform != null){
            // Debug.Log(raycats.transform.gameObject);
            return raycats.transform.gameObject;
        }
        return null;
    }
}
