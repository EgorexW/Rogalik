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

public struct FirePreviewReturn
{
    public FirePreviewReturn(int damage = 0, int critChance = 0, GameObject target = null){
        this.damage = damage;
        this.critChance = critChance;
        this.target = target;
    }

    public int damage;
    public int critChance;
    public GameObject target;
}
public class Weapon : MonoBehaviour
{
    public string displayName;
    [SerializeField] int damage;
    [SerializeField] int critChance;

    [SerializeField] protected LayerMask fireLayerMask;

    public int ammo;
    public Sprite sprite;
    [SerializeField] bool useCritDisMod = true;
    public WeaponType weaponType;
    
    public virtual GameObject Fire(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        if (ammo <= 0){
            return null;
        }
        int dmg = damage;
        int critChanceTmp = critChance;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, weaponType.maxRange, fireLayerMask);
        // Debug.Log(weaponDamageMod.critChanceMod);
        ammo --;
        if (raycast.transform == null){
            return null;
        }
        if (raycast.transform.tag == "Player" || raycast.transform.tag == "Character"){
            int dis = (int)raycast.distance;
            if (useCritDisMod){
                critChanceTmp += weaponType.GetCritDisMod(dis);
            }
            critChanceTmp += weaponDamageMod.critChanceMod;
            bool crit = false;
            critChanceTmp -= raycast.transform.GetComponent<CharacterObject>().GetResistance();
            while (critChanceTmp > 100){
                // Debug.Log("While");
                if (!crit){
                    crit = true;
                } else {
                    dmg += 1;
                }
                critChanceTmp -= 100;
            }
            if (Random.Range(1, 100) <= critChanceTmp){
                if (!crit){
                    crit = true;
                } else {
                    dmg += 1;
                }
            }
            if (crit){
                dmg += weaponDamageMod.critDamageMod;
            }
            dmg += weaponDamageMod.damageMod;
            raycast.transform.GetComponent<CharacterObject>().Damage(new Damage(dmg, crit));
            return raycast.transform.gameObject;
        }
        return null;
    }

    public FirePreviewReturn FirePreview(Vector2 dir, WeaponDamageMod weaponDamageMod = new WeaponDamageMod()){
        FirePreviewReturn defaultReturn = new FirePreviewReturn(damage, critChance, null);
        if (ammo <= 0){
            return defaultReturn;
        }
        int dmg = damage;
        int critChanceTmp = critChance;
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, weaponType.maxRange, fireLayerMask);
        if (raycast.transform == null){
            return defaultReturn;
        }
        if (raycast.transform.tag == "Player" || raycast.transform.tag == "Character"){
            int dis = (int)raycast.distance;
            if (useCritDisMod){
                critChanceTmp += weaponType.GetCritDisMod(dis);
            }
            critChanceTmp += weaponDamageMod.critChanceMod;
            critChanceTmp -= raycast.transform.GetComponent<CharacterObject>().GetResistance();
            while(critChance >= 200){
                dmg ++;
                critChance -= 100;
            }
            if (critChance >= 100){
                dmg += weaponDamageMod.critDamageMod;
            }
            dmg += weaponDamageMod.damageMod;
            return new FirePreviewReturn(dmg, critChanceTmp, raycast.transform.gameObject);
        }
        return defaultReturn;
    }
}
