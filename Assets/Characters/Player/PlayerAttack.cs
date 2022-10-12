using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    PlayerMain PM;
    [SerializeField]
    PlayerInventory PI;

    GameObject mark;
    [SerializeField] GameObject markPrefab;

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

    public void UpdateTargetingUI(){
        WeaponDamageMod damageMod = StatusEffects.GetDamageMod(gameObject, true);
        if (GetComponent<PlayerInput>().rotated){
            damageMod.damageMod -= 1;
        }
        if (PI.GetWeapon() == null){
            PI.TargetingUI.UpdateCritChance(0);
            return;
        }
        FirePreviewReturn firePreview = PI.GetWeapon().GetComponent<Weapon>().FirePreview(PM.dir, damageMod);
        PI.TargetingUI.UpdateCritChance(firePreview.critChance);
        if (firePreview.target == null){
            if (firePreview.raycast.transform != null){
                MarkTarget(firePreview.raycast.transform.position, false);
            } else{
                MarkTarget((Vector2)transform.position + PM.dir * PI.GetWeapon().GetComponent<Weapon>().weaponType.maxRange, false);
            }
        } else {
            MarkTarget(firePreview.target.transform.position);
        }
    }

    void MarkTarget(Vector2 target, bool enemy = true){
        if (mark == null){
            mark = Instantiate(markPrefab, transform.position, Quaternion.identity);
        }
        int dis = (int)Mathf.Round((target - (Vector2)transform.position).magnitude);
        if(dis == PI.GetWeapon().GetComponent<Weapon>().weaponType.idealRange){
            mark.GetComponent<Mark>().Sprite(true);
        } else {
            mark.GetComponent<Mark>().Sprite(false);
        }
        mark.transform.position = target;
    }
}
