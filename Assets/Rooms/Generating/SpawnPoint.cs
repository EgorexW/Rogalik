using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] objects;
    
    public bool rotate;

    float[] rotations = {0, 90, 180, 270};

    void Start()
    {
        int objectNr = Random.Range(0, objects.Length);
        Quaternion rotation = Quaternion.identity;
        if (rotate){
            rotation = Quaternion.Euler(0, 0, rotations[Random.Range(0, rotations.Length - 1)]);
        }
        Instantiate(objects[objectNr], transform.position, rotation).transform.parent = transform.parent;
        Destroy(gameObject);
    }
}