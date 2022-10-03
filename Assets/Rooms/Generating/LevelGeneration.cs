using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] startingPoints;

    public int roomLenght;

    public bool generating = true;

    public int pathMin;
    public int pathMax;
    public int fillCount;

    public LayerMask roomLayer;

    private List<Vector3> directions = new List<Vector3>();
    private List<GameObject> rooms = new List<GameObject>();
    private List<Vector3> removedDirections = new List<Vector3>();
    private int lenght;
    private int filledRooms;


    // void Start()
    // {
    //     // GenerateRooms();
    // }

    public List<GameObject> GenerateRooms()
    {
        directions.Add(Vector3.right);
        directions.Add(Vector3.up);
        directions.Add(Vector3.down);
        directions.Add(Vector3.left);

        Generatepath();
        filledRooms = rooms.Count;

        while (filledRooms < fillCount)
        {
            directions.Clear();
            directions.Add(Vector3.right);
            directions.Add(Vector3.up);
            directions.Add(Vector3.down);
            directions.Add(Vector3.left);
            removedDirections.Clear();

            GenerateSidePaths();
        }
        return rooms;
    }

    void Generatepath()
    {
        int roomNr = Random.Range(0, startingPoints.Length);
        GameObject room = startingPoints[roomNr];
        room.GetComponent<RoomType>().roomType = 2;
        transform.position = room.transform.position;
        rooms.Add(room);


        while (generating)
        {
            Vector3 previousPosition = transform.position;
            Vector3 direction = directions[Random.Range(0, directions.Count)];
            transform.position += roomLenght * direction;
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
            if (roomDetection == null || roomDetection.gameObject.GetComponent<RoomType>().roomType != 0)
            {
                if (directions.Count <= 1){
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadLevel(true);
                    break;
                }
                directions.Remove(direction);
                removedDirections.Add(direction);
                transform.position = previousPosition;
                if (pathMin <= rooms.Count && roomDetection == null)
                {
                    break;
                }
                continue;
            }
            foreach (Vector3 dirToAdd in removedDirections)
            {
                directions.Add(dirToAdd);
            }
            removedDirections.Clear();
            room.GetComponent<RoomType>().connections.Add(direction);
            room = roomDetection.gameObject;
            room.GetComponent<RoomType>().roomType = 1;
            room.GetComponent<RoomType>().connections.Add(-direction);
            rooms.Add(room);
            if (rooms.Count >= pathMax)
            {
                break;
            }
        }
        room.GetComponent<RoomType>().roomType = 3;
        foreach (GameObject mainRoom in rooms)
        {
            mainRoom.GetComponent<RoomType>().mainPath = true;
        }
    }

    void GenerateSidePaths()
    {
        RoomType room = rooms[Random.Range(0, rooms.Count)].GetComponent<RoomType>();
        transform.position = room.gameObject.transform.position;

        if (room.connections.Count >= 4 || room.roomType == 2 || room.roomType == 3)
        {
            return;
        }

        foreach(Vector3 dir in room.connections)
        {
            directions.Remove(dir);
            removedDirections.Add(dir);
        }
        lenght = 0;
        while (generating)
        {
            Vector3 previousPosition = transform.position;
            Vector3 direction = directions[Random.Range(0, directions.Count)];
            transform.position += roomLenght * direction;
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
            if (roomDetection == null || rooms.Contains(roomDetection.gameObject))
            {
                if (directions.Count <= 1)
                {
                    break;
                }
                directions.Remove(direction);
                removedDirections.Add(direction);
                transform.position = previousPosition;
                continue;
            }
            foreach (Vector3 dirToAdd in removedDirections)
            {
                directions.Add(dirToAdd);
            }
            removedDirections.Clear();
            room.connections.Add(direction);
            room = roomDetection.gameObject.GetComponent<RoomType>();
            room.roomType = 1;
            room.connections.Add(-direction);
            rooms.Add(room.gameObject);
            lenght++;
            filledRooms++;
            if (Random.Range(lenght, 6) >= Random.Range(3, 6))
            {
                break;
            }
        }
    }
}
