using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    static EnemyCollection enemyCollection = new EnemyCollection();
    [SerializeField]
    protected GameObject weapon;

    public virtual bool Attack(GameObject target){
        if (enemyCollection.straitLineCheck(transform.position, target.transform.position) != Vector2.zero){
            Vector2 ray = enemyCollection.straitLineCheck(target.transform.position, transform.position).normalized;
            // Debug.Log(ray.normalized);
            if (weapon.GetComponent<Weapon>().FirePreview(ray.normalized) == target){
                weapon.GetComponent<Weapon>().Fire(ray.normalized);
                transform.rotation = enemyCollection.Rotate(ray.normalized);
                return true;
            }
            else if (weapon.GetComponent<Weapon>().ammo <= 0){
                weapon.GetComponent<Weapon>().ammo += 1;
                return true;
            }
        }
        return false;
    }
    
    public void DropWeapon(){
        if (weapon == null){
            return;
        }
        weapon.transform.parent = transform.parent;
        weapon.GetComponent<SpriteRenderer>().enabled = true;
        weapon.GetComponent<BoxCollider2D>().enabled = true;
        weapon.transform.rotation = Quaternion.identity;
    }
}
