using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponType : ScriptableObject
{
    public int idealRange;
    public int maxRange;

    public List<int> critDisMods;

    public int GetCritDisMod(int dis){
        return critDisMods[dis];
    }
}
