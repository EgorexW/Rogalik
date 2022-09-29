using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawn : SpawnPoint
{
    [HideInInspector] public bool mainPath;

        void Start()
    {
        int objectNr = Random.Range(0, objects.Length);
        GameObject spawnedObject = Instantiate(objects[objectNr], transform.position, transform.rotation, transform.parent);
        spawnedObject.GetComponent<Room>().mainPath = mainPath;
        Destroy(gameObject);
    }
}
