using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int fireRange;
    [SerializeField] int damage;
    [SerializeField] int critChance;

    [SerializeField] LayerMask fireLayerMask;

    public int ammo;
    public Sprite sprite;
    public WeaponTypes weaponType;
    
    public string Fire(Vector2 dir){
        if (ammo <= 0){
            return "Miss";
        }
        RaycastHit2D raycats = Physics2D.Raycast(transform.position, dir, fireRange, fireLayerMask);
        ammo --;
        if (raycats.transform == null){
            return "Miss";
        }
        if (raycats.transform.tag == "Player" || raycats.transform.tag == "Character"){
            if (Random.Range(1, 100) <= critChance - raycats.transform.GetComponent<CharacterObject>().resistance){
                raycats.transform.GetComponent<CharacterObject>().ApplyStatusEffect(StatusEffect.Stunned);
                raycats.transform.GetComponent<CharacterObject>().ApplyStatusEffect(StatusEffect.Crit);
            }
            raycats.transform.GetComponent<CharacterObject>().Damage(damage);
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
