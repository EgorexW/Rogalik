using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    Stunned,
    Sharpened,
    Protected,
    Shielded,
    Marksman,
    Armored,
    Aim,
    Braced,
    Disabled
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
            statusEffects.Remove(StatusEffect.Disabled);
            break;
        }
    }

    public static void GetStatusCheck(GameObject gameObject, StatusEffect status){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        if (statusEffects.Contains(StatusEffect.Disabled)){
            if (status == StatusEffect.Stunned){
                statusEffects.Add(status);
            }
            return;
        }
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
            case StatusEffect.Marksman:
                if (!statusEffects.Contains(status)){
                    statusEffects.Add(status);
                }
                break;
            case StatusEffect.Armored:
                if (!statusEffects.Contains(status)){
                    statusEffects.Add(status);
                }
                break;
            case StatusEffect.Aim:
                if (!statusEffects.Contains(status)){
                    StatusIcon.Create(gameObject.transform, true, status);
                }
                statusEffects.Add(status);
                break;
            case StatusEffect.Braced:
                if (!statusEffects.Contains(status)){
                    StatusIcon.Create(gameObject.transform, true, status);
                    statusEffects.Add(status);
                }
                break;
            case StatusEffect.Disabled:
                StatusIcon.Create(gameObject.transform, true, status);
                statusEffects.Clear();
                statusEffects.Add(status);
                break;
        }
    }

    public static WeaponDamageMod GetDamageMod(GameObject gameObject){
        WeaponDamageMod weaponDamageMod = new WeaponDamageMod();
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        if (statusEffects.Contains(StatusEffect.Marksman)){
            weaponDamageMod.critDamageMod += 1;
        }
        if (statusEffects.Contains(StatusEffect.Aim)){
            weaponDamageMod.critChanceMod += 80;
            statusEffects.Remove(StatusEffect.Aim);
        }
        if (statusEffects.Contains(StatusEffect.Braced)){
            weaponDamageMod.damageMod += 2;
        }
        return weaponDamageMod;
    }

    public static MoveProperties OnMove(GameObject gameObject, MoveProperties moveP = new MoveProperties()){
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        if (statusEffects.Contains(StatusEffect.Braced)){
            statusEffects.Remove(StatusEffect.Braced);
        }
        return moveP;
    }

    public static int GetResistanceMod(GameObject gameObject){
        int resistanceMod = 0;
        List<StatusEffect> statusEffects = gameObject.GetComponent<CharacterObject>().statusEffects;
        if (statusEffects.Contains(StatusEffect.Armored)){
            resistanceMod += 30;
        }
        return resistanceMod;
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
