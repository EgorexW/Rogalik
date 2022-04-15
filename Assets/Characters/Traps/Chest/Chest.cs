using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : CharacterObject
{

    [SerializeField] List<WeightedValue<GameObject>> lootTable = new List<WeightedValue<GameObject>>();

    protected override void Die(){
        Loot();
    }

    public override void ApplyStatusEffect(StatusEffect status){
        return;
    }

    void Loot(){
        GameObject lootToSpawn = WeightedValue<GameObject>.GetWeightedRandom(lootTable);
        GameObject loot = Instantiate(lootToSpawn, transform.position, Quaternion.identity);
        loot.GetComponent<SpriteRenderer>().enabled = true;
        loot.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
