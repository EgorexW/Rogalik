using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : CharacterObject
{

    [SerializeField] Vector3[] positions;
    [SerializeField] int damage;
    [SerializeField] StatusEffect statusEffect;

    [SerializeField] LayerMask characterLayerMask;

    protected override void Die(){
        Explode();
    }

    public override void ApplyStatusEffect(StatusEffect status){
        return;
    }

    void Explode(){
        List<GameObject> targets = new List<GameObject>();
        foreach (Vector3 pos in positions)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position + pos, 0.1f, characterLayerMask);
            if (collider != null){
                GameObject target = collider.gameObject;
                targets.Add(target); 
            }
        }
        foreach (GameObject target in targets)
        {
            target.GetComponent<CharacterObject>().ApplyStatusEffect(statusEffect);
            target.GetComponent<CharacterObject>().Damage(damage);
        }
        Destroy(gameObject);
    }
}
