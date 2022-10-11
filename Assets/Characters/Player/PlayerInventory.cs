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
    public TargetingUI TargetingUI;

    [SerializeField] LayerMask itemLayer;

    [SerializeField] Sprite noneSprite;
    [SerializeField] string noneName;

    int dropNr = 1;

    public void Start(){
        GameObject gameObjects = GameObject.FindGameObjectWithTag("GameController");
        WeaponUI1 = gameObjects.GetComponent<UIManager>().WeaponUI1;
        WeaponUI2 = gameObjects.GetComponent<UIManager>().WeaponUI2;
        ItemUI = gameObjects.GetComponent<UIManager>().ItemsUI;
        TargetingUI = gameObjects.GetComponent<UIManager>().TargetingUI.GetComponent<TargetingUI>();
        UpdateInventoryUI();
    }

    public void SwitchWeapon(){
        GameObject tmpFirstWeapon = firstWeapon;
        firstWeapon = secondWeapon;
        secondWeapon = tmpFirstWeapon; 
        UpdateInventoryUI();
    }
    
    public void UpdateInventoryUI(){
        if (firstWeapon != null){
            WeaponUI1.GetComponent<WeaponUI>().SetWeaponUI(firstWeapon.GetComponent<Weapon>().sprite, firstWeapon.GetComponent<Weapon>().displayName, firstWeapon.GetComponent<Weapon>().ammo);
            TargetingUI.UpdateIdealRange(firstWeapon.GetComponent<Weapon>().weaponType.idealRange);
        } else {
            WeaponUI1.GetComponent<WeaponUI>().SetWeaponUI(noneSprite, noneName, 0);
            TargetingUI.UpdateIdealRange(0);
        }
        if (secondWeapon != null){
            WeaponUI2.GetComponent<WeaponUI>().SetWeaponUI(secondWeapon.GetComponent<Weapon>().sprite, secondWeapon.GetComponent<Weapon>().displayName, secondWeapon.GetComponent<Weapon>().ammo);
        } else {
            WeaponUI2.GetComponent<WeaponUI>().SetWeaponUI(noneSprite, noneName, 0);
        }
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
        if (Physics2D.OverlapCircle(transform.position, 0.1f, itemLayer) == null){
            return;
        }
        GameObject item = Physics2D.OverlapCircle(transform.position, 0.1f, itemLayer).gameObject;
        item.transform.parent = transform;
        item.GetComponent<SpriteRenderer>().enabled = false;
        item.GetComponent<BoxCollider2D>().enabled = false;
        if (item.tag == "Weapon"){
            if (firstWeapon != null){
                Drop(firstWeapon);
            }
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
            item.GetComponent<Item>().OnEquip(gameObject);
        }
        UpdateInventoryUI();
    }

    public GameObject GetWeapon(){
        return firstWeapon;
    }

    void Drop(GameObject item){
        if (item.tag == "item"){
            item.GetComponent<Item>().OnDrop(gameObject);
        }
        item.transform.SetParent(null);
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.GetComponent<BoxCollider2D>().enabled = true;
        item.transform.rotation = Quaternion.identity;
        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, dropNr);
        SceneManager.MoveGameObjectToScene(item, SceneManager.GetActiveScene());
        dropNr ++;
    }

    public bool UseItem(int itemNr){
        bool moveMade = false;
        GameObject item = items[itemNr];
        if (item == null){
            return false;
        }
        moveMade = item.GetComponent<Item>().Use(gameObject);
        if (item.GetComponent<Item>().DestroyOnUse()){
            items[itemNr] = null;
            UpdateInventoryUI();
        }
        return moveMade;
    }
}
