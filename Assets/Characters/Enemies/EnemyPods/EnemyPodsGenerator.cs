using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPodsGenerator : MonoBehaviour
{
    List<GameObject> rooms;
    List<GameObject> roomsMain = new List<GameObject>();

    [SerializeField] int roomsNr;
    [SerializeField] List<EnemyPods> enemyPodsPerLevel;
    [SerializeField] GameObject chest;
    [SerializeField] int podsMin;
    [SerializeField] float mainPathDistribiution = 0.4f;
    [SerializeField] int podsMax;
    [SerializeField] int chestMin;
    [SerializeField] int chestMax;
    [SerializeField] int roomSize;
    [SerializeField] int spawnOffset;
    [SerializeField] float podsIncresePerLevel;

    [SerializeField] GameObject enemyPodSpawner;

    // void Update(){
    //     rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
    //     if (rooms.Count != roomsNr){
    //         return;
    //     }
    //     foreach (GameObject room in rooms)
    //     {
    //         // Debug.Log(room);
    //         if (room.GetComponent<Room>().mainPath){
    //             roomsMain.Add(room);
    //             // Debug.Log(room);
    //         }
    //     }
    //     GeneratePods();
    //     gameObject.GetComponent<EnemyPodsGenerator>().enabled = false;
    // }

    public void GeneratePods(List<GameObject> rooms){
        foreach (GameObject room in rooms)
        {
            // Debug.Log(room);
            if (room.GetComponent<Room>().mainPath){
                roomsMain.Add(room);
                // Debug.Log(room);
            }
        }
        int level = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level;
        int podsIncrese = (int) Mathf.Floor(podsIncresePerLevel * level);
        List<GameObject> toUse = new List<GameObject>();
        int podsNr = Random.Range(podsMin + podsIncrese, podsMax + podsIncrese);
        int mainPods = (int) Mathf.Floor(podsNr * mainPathDistribiution);
        while (toUse.Count < podsNr){
            // Debug.Log(mainPods);
            GameObject room = null;
            if (mainPods > 0){
                room = roomsMain[Random.Range(0, roomsMain.Count - 1)];
            } else {
                room = rooms[Random.Range(0, rooms.Count - 1)];
            }
            if (room.GetComponent<Room>().type == RoomTemplateType.Enter || room.GetComponent<Room>().type == RoomTemplateType.Closed){
                rooms.Remove(room);
                roomsMain.Remove(room);
            }
            else{
                rooms.Remove(room);
                roomsMain.Remove(room);
                toUse.Add(room);
                mainPods --;
            }
            if (rooms.Count <= 0){
                podsNr = toUse.Count;
            }
        }
        EnemyPods enemyPods = enemyPodsPerLevel[enemyPodsPerLevel.Count - 1];
        if (enemyPodsPerLevel.Count > level){
            enemyPods = enemyPodsPerLevel[level - 1];            
        }
        foreach(GameObject room in toUse) {
            List<Vector2> avaliableLocations = new List<Vector2>();
            List<Vector2> locations = new List<Vector2>();
            float x = room.transform.position.x - roomSize/2 + spawnOffset;
            float y = room.transform.position.y - roomSize/2 + spawnOffset;
            while(y <= room.transform.position.y + roomSize/2 - spawnOffset){
                // Debug.Log(x + ":" + y);
                locations.Add(new Vector2(x, y));
                x ++;
                if (x > room.transform.position.x + roomSize/2 - spawnOffset)
                {
                    x = room.transform.position.x - roomSize/2 + spawnOffset;
                    y ++;
                }
            }
            // Debug.Log(locations.Count);
            foreach (Vector2 locationTmp in locations)
            {   
                if(Physics2D.OverlapCircle(locationTmp, 0.1f) == null){
                    avaliableLocations.Add(locationTmp);
                }
            }
            Vector2 location = avaliableLocations[Random.Range(0, avaliableLocations.Count - 1)];
            avaliableLocations.Remove(location);
            GameObject pod = Instantiate(enemyPodSpawner, location, Quaternion.identity);
            EnemyPod enemyPod = enemyPods.enemyPods[Random.Range(0, enemyPods.enemyPods.Count - 1)];
            pod.GetComponent<EnemyPodSpawner>().Spawn(enemyPod);
            int chests = Random.Range(chestMin, chestMax);
            while(chests > 0){
                location = avaliableLocations[Random.Range(0, avaliableLocations.Count - 1)];
                avaliableLocations.Remove(location);
                Instantiate(chest, location, Quaternion.identity);
                chests --;
            }
        }
    }
    
}
