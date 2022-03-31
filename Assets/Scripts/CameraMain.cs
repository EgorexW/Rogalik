using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{

    private GameObject player;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if(player != null)
            {
                transform.parent = player.transform;

                transform.position = player.transform.position;

                transform.position += new Vector3(0, 0, -10);
            }
        }
    }
}
