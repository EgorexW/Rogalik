using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterObject
{

    PlayerMovement pM;
    PlayerInventory pI;
    PlayerAttack pA;
    GameObject StatsUI;

    [SerializeField] int shield;
    [SerializeField] int maxShield;

    bool statusEffectsCheck = false;

    public void Start() {
        Register();
        pM = GetComponent<PlayerMovement>();
        pI = GetComponent<PlayerInventory>();
        pA = GetComponent<PlayerAttack>();
        StatsUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>().StatsUI;
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
    }

    void Update()
    {
        if (turnController.GetCurrentTurn() == onTurn && !moved){
            if (!statusEffectsCheck){
                moved = StatusEffects.StartTurnCheck(gameObject);
                if (moved){
                    return;
                }   
                statusEffectsCheck = true;
            }
            bool moveMade = false;
            if (Input.GetAxisRaw("Vertical") != 0) {
                moveMade = pM.Movement(Input.GetAxisRaw("Vertical"), true, Input.GetButton("Shift"));
            } else if (Input.GetAxisRaw("Horizontal") != 0) {
                moveMade = pM.Movement(Input.GetAxisRaw("Horizontal"), false, Input.GetButton("Shift"));
            } else if (Input.GetButtonDown("Switch Weapon") == true){
                pI.SwitchWeapon();
            } else if (Input.GetButtonDown("Fire") == true){
                pA.UseWeapon();
                moveMade = true;
            } else if (Input.GetButtonDown("Pick Up") == true){
                pI.PickUp();
                moveMade = false;
            } else if (Input.GetButtonDown("Item 0") == true){
                moveMade = pI.UseItem(0);
            } else if (Input.GetButtonDown("Item 1") == true){
                moveMade = pI.UseItem(1);
            } else if (Input.GetButtonDown("Item 2") == true){
                moveMade = pI.UseItem(2);
            }

            if (moveMade) {
                GetComponent<CharacterObject>().moved = true;
                statusEffectsCheck = false;
            }
        }
        if (Input.GetButtonDown("Reload Scene")){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadLevel(true);
        }
    }

    public override Damage Damage(Damage dmg){
        dmg = StatusEffects.OnHit(gameObject, dmg);
        DamagePopup.Create(transform.position, dmg.damage, dmg.crit);
        health -= dmg.damage;
        if (dmg.crit){
            ApplyStatusEffect(StatusEffect.Stunned);
        }
        health += Mathf.Min(shield, dmg.damage);
        shield -= Mathf.Min(shield, dmg.damage);
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
        if (health <= 0){
            Die();
        }
        return dmg;
    }

    public void GiveShield(int shieldGot){
        shield += shieldGot;
        if (shield > maxShield){
            shield = maxShield;
        }
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
    }

    public override void Heal(int healValue){
        health = Mathf.Min(health + healValue, maxHealth);
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
    }
}
