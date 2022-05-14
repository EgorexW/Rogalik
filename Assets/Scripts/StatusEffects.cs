using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Stunned,
    Sharpened,
    Protected
}

public static class StatusEffects
{
    public static bool StartTurnCheck(GameObject gameObject){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        bool moved = false;
        if (statusEffects.Contains(StatusEffect.Stunned)){
            statusEffects.RemoveAll(status => status == StatusEffect.Stunned);
            gameObject.GetComponent<CharacterObject>().moved = true;
            moved = true;
        }
        return moved;
    }

    public static void GetStatusCheck(GameObject gameObject, StatusEffect status){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        switch (status)
        {
            case StatusEffect.Sharpened:
                StatusIcon.Create(gameObject.transform, true, StatusEffect.Sharpened);
                break;
            case StatusEffect.Protected:
                StatusIcon.Create(gameObject.transform, true, StatusEffect.Protected);
                break;
        }
    }

    public static Damage OnHit(GameObject gameObject, Damage dmg){
        if (dmg.crit){
            if (gameObject.GetComponent<CharacterObject>().statusEffects.Contains(StatusEffect.Sharpened)){
                dmg = new Damage(9999, false);
            } 
            gameObject.GetComponent<CharacterObject>().ApplyStatusEffect(StatusEffect.Stunned);
        }
        if (gameObject.GetComponent<CharacterObject>().statusEffects.Contains(StatusEffect.Protected)){
            dmg = new Damage(0, false);
        } 
        return dmg;
    }
}
