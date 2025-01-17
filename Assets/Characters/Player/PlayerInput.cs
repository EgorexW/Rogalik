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

    public bool rotated;
    bool startTurnCheck = false;

    public void Start() {
        Register();
        pM = GetComponent<PlayerMovement>();
        pI = GetComponent<PlayerInventory>();
        pA = GetComponent<PlayerAttack>();
        StatsUI = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>().StatsUI;
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
        StatsUI.GetComponent<StatsUI>().UpdateStatsShieldUI(shield);
    }

    public override bool PlayTurn()
    {
        if (!startTurnCheck){
            bool moved = StatusEffects.StartTurnCheck(gameObject);
            pA.UpdateTargetingUI();
            if (moved){
                return moved;
            }   
            rotated = false;
            startTurnCheck = true;
        }
        bool didSomething = true;
        bool moveMade = false;
        bool shift = Input.GetButton("Shift");
        if (Input.GetAxisRaw("Vertical") != 0) {
            moveMade = pM.Movement(Input.GetAxisRaw("Vertical"), true, shift);
        } else if (Input.GetAxisRaw("Horizontal") != 0) {
            moveMade = pM.Movement(Input.GetAxisRaw("Horizontal"), false, shift);
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
        } else {
            didSomething = false;
        }

        if (didSomething){
            pA.UpdateTargetingUI();
        }
        if (moveMade) {
            startTurnCheck = false;
        }
        if (Input.GetButtonDown("Reload Scene")){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadLevel(true);
        }
        return moveMade;
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

    public override void Heal(int healValue, HealType healType){
        base.Heal(healValue, healType);
        StatsUI.GetComponent<StatsUI>().UpdateStatsHealthUI(health);
    }
}
