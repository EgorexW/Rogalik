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
        PI.GetWeapon().GetComponent<Weapon>().Fire(PM.dir);
        PI.UpdateInventoryUI();
    }
}