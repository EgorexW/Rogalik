using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Stunned,
    Sharpened,
    Crit
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
            case StatusEffect.Crit:
                if (statusEffects.Contains(StatusEffect.Sharpened)){
                    gameObject.GetComponent<CharacterObject>().Damage(99999);
                }
                break;
            case StatusEffect.Sharpened:
                StatusIcon.Create(gameObject.transform, true, StatusEffect.Sharpened);
                break;
        }
    }
}
