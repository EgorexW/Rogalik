using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject firstWeapon;
    [SerializeField] GameObject secondWeapon;

    [SerializeField] GameObject[] items;

    GameObject WeaponUI1;
    GameObject WeaponUI2;
    GameObject ItemUI;

    [SerializeField] LayerMask itemLayer;

    int dropNr = 1;

    public void Start(){
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GameController");
        WeaponUI1 = gameObjects[0].GetComponent<UIManager>().WeaponUI1;
        WeaponUI2 = gameObjects[0].GetComponent<UIManager>().WeaponUI2;
        ItemUI = gameObjects[0].GetComponent<UIManager>().ItemsUI;
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
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null){
                ItemUI.transform.GetChild(i).GetComponent<Image>().enabled = true;
                ItemUI.transform.GetChild(i).GetComponent<Image>().sprite = items[i].GetComponent<Item>().sprite;
            } else {
                ItemUI.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
        }
    }

    public void PickUp(){
        GameObject item = Physics2D.OverlapCircle(transform.position, 0.1f, itemLayer).gameObject;
        item.transform.parent = transform;
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;
        if (item.tag == "Weapon"){
            Drop(firstWeapon);
            firstWeapon = item;
        } else if(item.tag == "Item"){
            bool added = false;
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i] == null)
                {
                    items[i] = item;
                    added = true;
                    break;
                }
            }
            if (!added){
                Drop(items[0]);
                for (int i = 1; i < items.Length; i++){
                    items[i - 1] = items[i];
                }
                items[items.Length - 1] = item;

            }
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
        SceneManager.MoveGameObjectToScene(item, SceneManager.GetActiveScene());
        dropNr ++;
    }

    public bool UseItem(int itemNr){
        bool moveMade = false;
        if (items[itemNr] == null){
            return false;
        }
        moveMade = items[itemNr].GetComponent<Item>().Use(gameObject);
        items[itemNr] = null;
        UpdateInventoryUI();
        return moveMade;
    }
}
