using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{

    public int onTurn;  //can move in given turn

    public bool isPlayer = false;

    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth = 0;

    [SerializeField] protected Team team;

    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    [SerializeField] protected int resistance;

    protected bool getsStunned = true;

    public void Awake(){
        if (maxHealth == 0){
            maxHealth = health;
        }
        gameObject.AddComponent<StatusIconPlugin>();
    }

    public virtual bool PlayTurn(){
        if (StatusEffects.StartTurnCheck(gameObject)){
            return false;
        }
        return true;
    }

    protected TurnController turnController;

    protected void Register() {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        turnController = controller.GetComponent<TurnController>();
        turnController.RegisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this, isPlayer);
    }

    public virtual Damage Damage(Damage dmg){
        dmg = StatusEffects.OnHit(gameObject, dmg);
        DamagePopup.Create(transform.position, dmg.damage, dmg.crit);
        health -= dmg.damage;
        if (dmg.crit && getsStunned){
            ApplyStatusEffect(StatusEffect.Stunned);
        }
        if (health <= 0){
            Die();
        }
        return dmg;
    }

    public virtual void Heal(int healValue, HealType healType){
        StatusEffects.Heal(gameObject, healType);
        health = Mathf.Min(health + healValue, maxHealth);
    }

    public virtual void ApplyStatusEffect(StatusEffect status){
        StatusEffects.GetStatusCheck(gameObject, status);
    }

    protected virtual void Die(){
        Destroy(gameObject);
        turnController.UnregisterObjectInTurn(GetComponent<CharacterObject>().onTurn, this);
    }

    public FriendOrFoe CheckIfFriendOrFoe(GameObject character){
        return team.GetFriendOrFoe(character.GetComponent<CharacterObject>().GetTeam());
    }

    public Team GetTeam(){
        return team;
    }

    public int GetResistance(){
        return resistance;
    }
}
