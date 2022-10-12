using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;

    [SerializeField] Sprite critSprite;

    public void Sprite(bool crit){
        Sprite sprite = defaultSprite;
        if (crit){
            sprite = critSprite;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
