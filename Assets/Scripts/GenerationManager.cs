using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{

    [SerializeField] LevelGeneration levelGeneration;
    [SerializeField] EventsGenerator eventsGenerator;
    [SerializeField] EnemyPodsGenerator enemyPodsGenerator;

    void Start(){
        StartCoroutine(Generate());
    }

    IEnumerator Generate(){
        List<GameObject> rooms = levelGeneration.GenerateRooms();
        SpawnRooms(rooms);
        yield return null;
        rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        eventsGenerator.AssignEvents(rooms);
        enemyPodsGenerator.GeneratePods(rooms);
        eventsGenerator.ExecuteEvents(rooms);
    }

    void SpawnRooms(List<GameObject> rooms){
        foreach (GameObject room in rooms)
        {
            room.GetComponent<RoomType>().Generate();
        }
    }
}
