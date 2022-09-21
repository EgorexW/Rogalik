using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIcon : MonoBehaviour
{
    static Vector3 offsetValue = new Vector3(0.3f, -0.3f, 0);

    [SerializeField] Sprite sharpenedSprite;
    [SerializeField] Sprite protectedSprite;


    [SerializeField] LayerMask iconsLayer;
    StatusEffect statusEffect;
    Transform parent;
    bool offset;
    
    public static GameObject Create(Transform parent, bool offset, StatusEffect statusEffect){
        Vector3 position = parent.position;
        if (offset) position += offsetValue;
        GameObject statusIcon = Instantiate(GameAssets.i.statusIcon, position, Quaternion.identity);
        statusIcon.GetComponent<StatusIcon>().Setup(statusEffect, parent, offset);
        return statusIcon;
    }

    public void Setup(StatusEffect tmp_statusEffect, Transform parentTmp, bool offsetTmp){
        parentTmp.GetComponent<StatusIconPlugin>().Register(this);
        statusEffect = tmp_statusEffect;
        if (statusEffect == StatusEffect.Sharpened){
            GetComponent<SpriteRenderer>().sprite = sharpenedSprite;
        } else if (statusEffect == StatusEffect.Protected){
            GetComponent<SpriteRenderer>().sprite = protectedSprite;
        }

        parent = parentTmp;
        offset = offsetTmp;
    }

    public void RunUpdate(int index){
        if(parent == null || !parent.GetComponent<CharacterObject>().statusEffects.Contains(statusEffect)){
            parent.GetComponent<StatusIconPlugin>().Unregister(this);
            Destroy(gameObject);
            return;
        }
        Vector3 offsetTmp = offsetValue;
        offsetTmp.x -= transform.localScale.x * index;
        transform.position = parent.position + offsetTmp;
    }

}
