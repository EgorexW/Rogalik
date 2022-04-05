using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField]
    GameObject Health;
    [SerializeField]
    GameObject Shield;

    public void UpdateStatsHealthUI(int health){
        // Debug.Log("Got here" + health);
        if (health < 0){
            health = 0;
        }
        Health.GetComponent<Text>().text = health.ToString(); 
    }
    public void UpdateStatsShieldUI(int shield){
        // Debug.Log("Got here" + health);
        if (shield < 0){
            shield = 0;
        }
        Shield.GetComponent<Text>().text = shield.ToString(); 
    }
}
