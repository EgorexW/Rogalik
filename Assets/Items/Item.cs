using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] public Sprite sprite;

    public virtual bool Use(GameObject user){
        return false;
    }
}
