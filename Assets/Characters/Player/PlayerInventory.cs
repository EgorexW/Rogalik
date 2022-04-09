using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject firstWeapon;
    [SerializeField] GameObject secondWeapon;

    GameObject WeaponUI1;
    GameObject WeaponUI2;

    [SerializeField] LayerMask itemLayer;

    int dropNr = 1;

    public void Start(){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GameController");
        WeaponUI1 = gameObjects[0].GetComponent<UIManager>().WeaponUI1;
        WeaponUI2 = gameObjects[0].GetComponent<UIManager>().WeaponUI2;
        UpdateInventoryUI();
    }

    public void SwitchWeapon(){
        GameObject tmpFirstWeapon = firstWeapon;
        firstWeapon = secondWeapon;
        secondWeapon = tmpFirstWeapon; 
        UpdateInventoryUI();
    }
    
    public void UpdateInventoryUI(){
        WeaponUI1.GetComponent<WeaponUI>().SetWeaponUI(firstWeapon.GetComponent<Weapon>().sprite, firstWeapon.name, firstWeapon.GetComponent<Weapon>().ammo);
        WeaponUI2.GetComponent<WeaponUI>().SetWeaponUI(secondWeapon.GetComponent<Weapon>().sprite, secondWeapon.name, secondWeapon.GetComponent<Weapon>().ammo);
    }

    public void PickUp(){
        GameObject item = Physics2D.OverlapCircle(transform.position, 0.1f, itemLayer).gameObject;
        Debug.Log(item);
        if (item.tag == "Weapon"){
            Drop(firstWeapon);
            item.transform.parent = transform;
            item.GetComponent<SpriteRenderer>().enabled = false;
            item.GetComponent<BoxCollider2D>().enabled = false;
            firstWeapon = item;
        }
        UpdateInventoryUI();
    }

    public GameObject GetWeapon(){
        return firstWeapon;
    }

    void Drop(GameObject item){
        item.transform.parent = transform.parent;
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.GetComponent<BoxCollider2D>().enabled = true;
        item.transform.rotation = Quaternion.identity;
        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, dropNr);
        dropNr ++;
    }
}
