using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public bool moved;
    public int onTurn;  //can move in given turn
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 0;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public int resistance;

    public void Awake(){
        if (maxHealth == 0){
            maxHealth = health;
        }
    }

    protected TurnController turnController;

    protected void Register() {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        turnController = controller.GetComponent<TurnController>();
        turnController.RegisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }

    public virtual Damage Damage(Damage dmg){
        dmg = StatusEffects.OnHit(gameObject, dmg);
        DamagePopup.Create(transform.position, dmg.damage, dmg.crit);
        health -= dmg.damage;
        if (dmg.crit){
            ApplyStatusEffect(StatusEffect.Stunned);
        }
        if (health <= 0){
            Die();
        }
        return dmg;
    }

    public virtual void Heal(int healValue){
        health = Mathf.Min(health + healValue, maxHealth);
    }

    public virtual void ApplyStatusEffect(StatusEffect status){
        StatusEffects.GetStatusCheck(gameObject, status);
    }

    protected virtual void Die(){
        Destroy(gameObject);
        turnController.UnregisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }
}
