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

    [SerializeField] GameObject startingChest;

    public void AssignEvents(List<GameObject> rooms){
        Vector3[] directions = {new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(1, 0, 0), new Vector3(-1, 0, 0)};
        foreach (GameObject room in rooms){
            // Debug.Log(room); 
            if (room.GetComponent<Room>().type == RoomTemplateType.Enter){
                if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().startLevel){
                    room.GetComponent<Room>().eventType = EventType.StartingChests;
                }
                continue;
            }
            if (room.GetComponent<Room>().type == RoomTemplateType.Exit){
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

    public void ExecuteEvents(List<GameObject> rooms){
        // Debug.Log(rooms.Count);
        foreach (GameObject room in rooms){
            // Debug.Log(room.GetComponent<Room>().type);
            // if (room.GetComponent<Room>().type == RoomTemplateType.Enter){
            //     Debug.Log(room.GetComponent<Room>().eventType);
            // }
            switch (room.GetComponent<Room>().eventType){
                case EventType.Explosive:
                    ExplosiveEvent(room);
                    break;
                case EventType.StartingChests:
                    StartingChestsEvent(room);
                    break;
            }
        }
    }

    void StartingChestsEvent(GameObject room){
        // Debug.Log("OK");
        List<Vector2> avaliableLocations = new List<Vector2>();
        List<Vector2> locations = new List<Vector2>();
        float x = room.transform.position.x - roomLenght/2 + 6;
        float y = room.transform.position.y - roomLenght/2 + 6;
        while(y <= room.transform.position.y + roomLenght/2 - 6){
            locations.Add(new Vector2(x, y));
            x ++;
            if (x > room.transform.position.x + roomLenght/2 - 6)
            {
                x = room.transform.position.x - roomLenght/2 + 6;
                y ++;
            }
        }
        foreach (Vector2 location in locations)
        {   
            if(Physics2D.OverlapCircle(location, 0.1f) == null){
                avaliableLocations.Add(location);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            Vector2 location = avaliableLocations[Random.Range(0, avaliableLocations.Count)];
            avaliableLocations.Remove(location);
            // Debug.Log("Hwo");
            Instantiate(startingChest, new Vector3(location.x, location.y, 0), Quaternion.identity);
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
            avaliableLocations.Remove(location);
            Instantiate(barrel, new Vector3(location.x, location.y, 0), Quaternion.identity);
            barrelsPlaced ++;
        }
        
    }
}
