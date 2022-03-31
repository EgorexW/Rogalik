using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public bool moved;
    public int onTurn;  //can move in given turn
    [SerializeField] protected int health;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    protected TurnController turnController;

    protected void Register() {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
        turnController = controllers[0].GetComponent<TurnController>();
        turnController.RegisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }

    public virtual void Damage(int damage){
        health -= damage;
        if (health <= 0){
            Die();
        }
    }

    public virtual void ApplyStatusEffect(StatusEffect effect){
        statusEffects.Add(effect);
    }

    protected virtual void Die(){
        Destroy(gameObject);
        turnController.UnregisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }
}
