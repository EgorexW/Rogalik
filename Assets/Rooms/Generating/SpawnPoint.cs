using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] objects;
    
    void Start()
    {
        int objectNr = Random.Range(0, objects.Length);
        Instantiate(objects[objectNr], transform.position, Quaternion.identity).transform.parent = transform.parent;
        Destroy(gameObject);
    }
}