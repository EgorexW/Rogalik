using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPodsGenerator : MonoBehaviour
{
    List<GameObject> rooms;

    [SerializeField] int roomsNr;
    [SerializeField] GameObject[] enemyPods;
    [SerializeField] int podsMin;
    [SerializeField] int podsMax;
    [SerializeField] int roomSize;
    [SerializeField] int spawnOffset;

    void Update(){
        rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        if (rooms.Count != roomsNr){
            return;
        }
        GeneratePods();
        gameObject.GetComponent<EnemyPodsGenerator>().enabled = false;
    }

    void GeneratePods(){
        List<GameObject> toUse = new List<GameObject>();
        int podsNr = Random.Range(podsMin, podsMax);
        while (toUse.Count < podsNr){
            GameObject room = rooms[Random.Range(0, rooms.Count - 1)];
            if (room.GetComponent<Room>().type == RoomTemplateType.Enter || room.GetComponent<Room>().type == RoomTemplateType.Closed){
                rooms.Remove(room);
            }
            else{
                rooms.Remove(room);
                toUse.Add(room);
            }
        }
        foreach(GameObject room in toUse) {
            List<Vector2> avaliableLocations = new List<Vector2>();
            List<Vector2> locations = new List<Vector2>();
            float x = room.transform.position.x - roomSize/2 + 0.5f + spawnOffset;
            float y = room.transform.position.y - roomSize/2 + 0.5f + spawnOffset;
            while(y <= room.transform.position.y + roomSize/2 - spawnOffset){
                // Debug.Log(x + ":" + y);
                locations.Add(new Vector2(x, y));
                x ++;
                if (x > room.transform.position.x + roomSize/2 - spawnOffset)
                {
                    x = room.transform.position.x - roomSize/2 + 0.5f + spawnOffset;
                    y ++;
                }
            }
            // Debug.Log(locations.Count);
            foreach (Vector2 location in locations)
            {   
                if(Physics2D.OverlapCircle(location, 0.1f) == null){
                    avaliableLocations.Add(location);
                }
            }
            // Debug.Log(avaliableLocations.Count);
            Instantiate(enemyPods[Random.Range(0, enemyPods.Length - 1)], avaliableLocations[Random.Range(0, avaliableLocations.Count - 1)], Quaternion.identity);
        }
    }
    
}
