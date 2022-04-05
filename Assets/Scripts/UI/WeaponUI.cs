using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    GameObject Icon;
    GameObject Name;
    GameObject Ammo;

    void Start(){
        Icon = transform.GetChild(1).gameObject; 
        Name = transform.GetChild(2).gameObject; 
        Ammo = transform.GetChild(3).gameObject; 
    }
    
    public void SetWeaponUI(Sprite icon, string name, int ammo){
        Icon.GetComponent<Image>().sprite = icon;
        Name.GetComponent<Text>().text = name;
        Ammo.GetComponent<Text>().text = ammo.ToString();
    }
}
