using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIcon : MonoBehaviour
{
    static Vector3 offsetValue = new Vector3(0.3f, -0.3f, 0);

    [SerializeField] Sprite sharpenedSprite;

    Transform parent;
    bool offset;
    
    public static GameObject Create(Transform parent, bool offset, StatusEffect statusEffect){
        Vector3 position = parent.position;
        if (offset) position += offsetValue;
        GameObject statusIcon = Instantiate(GameAssets.i.statusIcon, position, Quaternion.identity);
        statusIcon.GetComponent<StatusIcon>().Setup(statusEffect, parent, offset);
        return statusIcon;
    }

    public void Setup(StatusEffect statusEffect, Transform parentTmp, bool offsetTmp){
        if (statusEffect == StatusEffect.Sharpened){
            GetComponent<SpriteRenderer>().sprite = sharpenedSprite;
        }

        parent = parentTmp;
        offset = offsetTmp;
    }

    void LateUpdate(){
        if(parent == null){
            Destroy(gameObject);
            return;
        }
        transform.position = parent.position + offsetValue;
    }

}
