using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsGenerator : MonoBehaviour
{

    List<GameObject> rooms;
    [SerializeField] int roomsCount;

    [SerializeField] int roomLenght;
    [SerializeField] LayerMask blockLayerMask;
    [SerializeField] LayerMask roomLayerMask;

    [SerializeField] int explosiveRoomChance;
    [SerializeField] int explosiveRoomSpredingChance;

    [SerializeField] int barrelsMin;
    [SerializeField] int barrelsMax;
    [SerializeField] GameObject barrel;

    void Update(){
        rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        if (rooms.Count != roomsCount){
            return;
        }
        AssignEvents();
        ExecuteEvents();
        gameObject.GetComponent<EventsGenerator>().enabled = false;
    }

    void AssignEvents(){
        Vector3[] directions = {new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(1, 0, 0), new Vector3(-1, 0, 0)};
        foreach (GameObject room in rooms){
            // Debug.Log(room);
            if (room.GetComponent<Room>().type == RoomTemplateType.Enter || room.GetComponent<Room>().type == RoomTemplateType.Exit){
                continue;
            }
            if (Random.Range(1, 1000) <= explosiveRoomChance){
                room.GetComponent<Room>().eventType = EventType.Explosive;
                foreach (Vector3 dir in directions){
                    Collider2D collider = Physics2D.OverlapCircle(room.transform.position + (roomLenght / 2 * dir), 0.2f, blockLayerMask);
                    // Debug.Log(transform.position + (roomLenght / 2 * dir));
                    // Debug.Log(collider);
                    if (collider == null){
                        Collider2D nextCollider = Physics2D.OverlapCircle(room.transform.position + (roomLenght * dir), 0.2f, roomLayerMask);
                        // Debug.Log(nextCollider);
                        if (nextCollider == null){
                            continue;
                        }
                        GameObject nextRoom = nextCollider.gameObject;
                        if (nextRoom.GetComponent<Room>().type == RoomTemplateType.Enter || nextRoom.GetComponent<Room>().type == RoomTemplateType.Exit){
                            continue;
                        }   
                        if (Random.Range(1, 1000) <= explosiveRoomSpredingChance){
                            // Debug.Log(nextRoom);
                            nextRoom.GetComponent<Room>().eventType = EventType.Explosive;
                        }
                    }
                }
            }
        }
    }

    void ExecuteEvents(){
        foreach (GameObject room in rooms){
            switch (room.GetComponent<Room>().eventType){
                case EventType.Explosive:
                    ExplosiveEvent(room);
                    break;
            }
        }
    }

    void ExplosiveEvent(GameObject room){
        int barrels = Random.Range(barrelsMin, barrelsMax);
        int barrelsPlaced = 0;
        List<Vector2> avaliableLocations = new List<Vector2>();
        List<Vector2> locations = new List<Vector2>();
        float x = room.transform.position.x - roomLenght/2;
        float y = room.transform.position.y - roomLenght/2;
        while(y <= room.transform.position.y + roomLenght/2){
            locations.Add(new Vector2(x, y));
            x ++;
            if (x > room.transform.position.x + roomLenght/2)
            {
                x = room.transform.position.x - roomLenght/2;
                y ++;
            }
        }
        foreach (Vector2 location in locations)
        {   
            if(Physics2D.OverlapCircle(location, 0.1f) == null){
                avaliableLocations.Add(location);
            }
        }
        while (barrelsPlaced < barrels)
        {
            Vector2 location = avaliableLocations[Random.Range(0, avaliableLocations.Count)];
            Instantiate(barrel, new Vector3(location.x, location.y, 0), Quaternion.identity);
            barrelsPlaced ++;
        }
        
    }
}
