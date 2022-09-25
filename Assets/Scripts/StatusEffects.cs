using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Stunned,
    Sharpened,
    Protected,
    Shielded
}

public static class StatusEffects
{
    public static bool StartTurnCheck(GameObject gameObject){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        bool moved = false;
        if (statusEffects.Contains(StatusEffect.Stunned)){
            statusEffects.Remove(StatusEffect.Stunned);
            moved = true;
        }
        return moved;
    }

    public static void Heal(GameObject gameObject, HealType healType){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        switch (healType){
            case HealType.Medkit:
            statusEffects.Remove(StatusEffect.Sharpened);
            break;
        }
    }

    public static void GetStatusCheck(GameObject gameObject, StatusEffect status){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        switch (status)
        {
            case StatusEffect.Sharpened:
                if (!statusEffects.Contains(status)){
                    StatusIcon.Create(gameObject.transform, true, status);
                    statusEffects.Add(status);
                }
                break;
            case StatusEffect.Protected:
                if (!statusEffects.Contains(status)){
                    StatusIcon.Create(gameObject.transform, true, status);
                    statusEffects.Add(status);
                }
                break;
            case StatusEffect.Shielded:
                if (!statusEffects.Contains(status)){
                    StatusIcon.Create(gameObject.transform, true, status);
                }
                statusEffects.Add(status);
                break;
            case StatusEffect.Stunned:
                if (!statusEffects.Contains(status)){
                    statusEffects.Add(status);
                }
                break;
        }
    }

    public static Damage OnHit(GameObject gameObject, Damage dmg){
        if (dmg.crit){
            if (gameObject.GetComponent<CharacterObject>().statusEffects.Contains(StatusEffect.Sharpened)){
                dmg = new Damage(dmg.damage + 10, true);
            } 
        }
        if (gameObject.GetComponent<CharacterObject>().statusEffects.Contains(StatusEffect.Protected)){
            dmg = new Damage(0, false);
            gameObject.GetComponent<CharacterObject>().statusEffects.Remove(StatusEffect.Protected);
        } 
        else if(gameObject.GetComponent<CharacterObject>().statusEffects.Contains(StatusEffect.Shielded)){
            dmg = new Damage(dmg.damage - 1, dmg.crit);
            gameObject.GetComponent<CharacterObject>().statusEffects.Remove(StatusEffect.Shielded);
        }
        return dmg;
    }
}
