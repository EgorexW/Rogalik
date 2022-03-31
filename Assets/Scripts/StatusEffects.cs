using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
        Stunned,
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
}
