using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public bool moved;
    public int onTurn;  //can move in given turn
    [SerializeField] protected int health;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public int resistance;

    protected TurnController turnController;

    protected void Register() {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
        turnController = controllers[0].GetComponent<TurnController>();
        turnController.RegisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }

    public virtual void Damage(int damage){
        DamagePopup.Create(transform.position, damage, statusEffects.Contains(StatusEffect.Crit));
        statusEffects.RemoveAll(status => status == StatusEffect.Crit);
        health -= damage;
        if (health <= 0){
            Die();
        }
    }

    public virtual void ApplyStatusEffect(StatusEffect status){
        statusEffects.Add(status);
        StatusEffects.GetStatusCheck(gameObject, status);
    }

    protected virtual void Die(){
        Destroy(gameObject);
        turnController.UnregisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }
}
