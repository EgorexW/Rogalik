using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    public virtual bool Attack(GameObject target){
        return true;
    }

    public virtual void DropWeapon(){

    }
}
