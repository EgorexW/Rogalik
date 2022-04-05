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
    [SerializeField] int maxHealth;

    bool statusEffectsCheck = false;

    void Start() {
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
            }

            if (moveMade) {
                GetComponent<CharacterObject>().moved = true;
                statusEffectsCheck = false;
            }
        }
    }

    public override void Damage(int damage){
        health += Mathf.Min(shield, damage);
        shield -= Mathf.Min(shield, damage);
        base.Damage(damage);
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
        if (health <= 0){
            Die();
        }
    }

    public void GiveShield(int shieldGot){
        shield += shieldGot;
        if (shield > maxShield){
            shield = maxShield;
        }
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
    }
}
