using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : CharacterObject
{

    [SerializeField] List<GameObject> lootTable = new List<GameObject>();

    protected override void Die(){
        Loot();
    }

    public override void ApplyStatusEffect(StatusEffect status){
        return;
    }

    void Loot(){
        GameObject loot = Instantiate(lootTable[0], transform.position, Quaternion.identity);
        loot.GetComponent<SpriteRenderer>().enabled = true;
        loot.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
