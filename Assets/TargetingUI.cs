using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetingUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI critChanceText;
    [SerializeField] TextMeshProUGUI idealRangeText;

    public void UpdateCritChance(int critChance){
        critChanceText.text = "Crit Chance: " + critChance + "%";
    }

    public void UpdateIdealRange(int idealRange){
        idealRangeText.text = "Ideal Range: " + idealRange;
    }
}
