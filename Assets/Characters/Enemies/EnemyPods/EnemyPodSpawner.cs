using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPodSpawner : MonoBehaviour
{
    public void Spawn(EnemyPod enemyPod){
        int size = enemyPod.enemies.Count;
        List<Vector2> avaliableLocations = new List<Vector2>();
        List<Vector2> locations = new List<Vector2>();
        float x = transform.position.x - (size - 1)/2;
        float y = transform.position.y - (size - 1)/2;
        while(y <= transform.position.y + (size - 1)/2){
            // Debug.Log(x + ":" + y);
            locations.Add(new Vector2(x, y));
            x ++;
            if (x > transform.position.x + (size - 1)/2)
            {
                x = transform.position.x - (size - 1)/2;
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
        foreach(GameObject enemy in enemyPod.enemies){
            Vector2 location = avaliableLocations[Random.Range(0, avaliableLocations.Count - 1)];
            GameObject newEnemy = Instantiate(enemy, transform);
            newEnemy.transform.position = location;
            avaliableLocations.Remove(location);
        }
    }
}
