using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponDamageMod
{
    public WeaponDamageMod(int damageMod = 0, int critChanceMod = 1, int critDamageMod = 0){
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
    
    public virtual GameObject Fire(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        if (ammo <= 0){
            return null;
        }
        int dmg = damage;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, fireRange, fireLayerMask);
        ammo --;
        if (raycast.transform == null){
            return null;
        }
        if (raycast.transform.tag == "Player" || raycast.transform.tag == "Character"){
            bool crit = false;
            if (Random.Range(1, 100) <= critChance - raycast.transform.GetComponent<CharacterObject>().GetResistance() + weaponDamageMod.critChanceMod){
                crit = true;
                dmg += weaponDamageMod.critDamageMod;
            }
            dmg += weaponDamageMod.damageMod;
            raycast.transform.GetComponent<CharacterObject>().Damage(new Damage(dmg, crit));
            return raycast.transform.gameObject;
        }
        return null;
    }

    public GameObject FirePreview(Vector2 dir){
        if (ammo <= 0){
            return null;
        }
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, fireRange, fireLayerMask);
        Debug.DrawRay(transform.position, dir * fireRange, Color.red, 100);
        // Debug.Log(raycast.transform);
        if (raycast.transform != null){
            // Debug.Log(raycast.transform.gameObject);
            return raycast.transform.gameObject;
        }
        return null;
    }
}
