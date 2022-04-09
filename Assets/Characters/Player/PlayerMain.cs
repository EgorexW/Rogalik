using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public Vector2 dir;

    public static PlayerMain instance;

    [SerializeField] PlayerInput pInp;
    [SerializeField] PlayerInventory pInv;
    [SerializeField] PlayerMovement pM;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            instance.transform.position = transform.position;
            instance.pInp.Start();
            instance.pInv.Start();
            instance.pM.Start();
            Destroy(gameObject);
        } else {
            transform.parent = null;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
