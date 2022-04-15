using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightedValue<T>
{
    public T value;
    public int chance;

    public static T GetWeightedRandom(List<WeightedValue<T>> weights){
        T output = default(T);
        
        int totalWeight = 0;
        foreach (var entry in weights)
        {
            totalWeight += entry.chance;
        }

        int roll = UnityEngine.Random.Range(1, totalWeight);

        int processedWeight = 0;
        foreach (var entry in weights)
        {
            processedWeight += entry.chance;
            if(roll <= processedWeight){
                output = entry.value;
                break;
            }
        }

        return output;
    }
}
