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
        int damageMod = 0;
        if (GetComponent<PlayerInput>().rotated){
            damageMod -= 1;
        }
        PI.GetWeapon().GetComponent<Weapon>().Fire(PM.dir, damageMod);
        PI.UpdateInventoryUI();
    }
}
