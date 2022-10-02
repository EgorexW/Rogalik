using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public Sprite sprite;

    [SerializeField] protected ItemType type;

    [SerializeField] protected bool destroyOnUse;
    
    public virtual bool Use(GameObject user){
        return false;
    }

    public virtual void OnEquip(GameObject user){
        
    }

    public virtual void OnDrop(GameObject user){

    }

    public virtual bool DestroyOnUse(){
        if (destroyOnUse){
            Destroy(gameObject);
        }
        return destroyOnUse;
    }
}
