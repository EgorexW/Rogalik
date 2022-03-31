using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public int roomType;
    public List<Vector3> connections;

    private Quaternion rotation;

    // 0 for coridors, 1 for turn, 2 for 3-ways, 3 for 4-ways, 4 for dead ends, 5 for start, 6 for exit, 7 for closed;
    public GameObject[] rooms;

    void Update()
    {
        GenerateRoom();
        Destroy(gameObject);
    }

    void GenerateRoom()
    {
        if (roomType == 0)
        {
            GenerateClosed();
            return;
        }
        if (roomType == 2)
        {
            GenerateDoors(5);
            return;
        }
        if (roomType == 3)
        {
            GenerateDoors(6);
            return;
        }
        if (connections.Count == 2)
        {
            if (connections.Contains(Vector3.right) && connections.Contains(Vector3.left) ||
            connections.Contains(Vector3.up) && connections.Contains(Vector3.down))
            {
                GenerateCorridor();
                return;
            }
            GenerateTurn();
            return;
        }
        if (connections.Count == 1)
        {
            GenerateDeadEnd();
            return;
        }
        if (connections.Count == 3)
        {
            Generate3Ways();
            return;
        }
        if (connections.Count == 4)
        {
            Generate4Ways();
            return;
        }
    }

    void GenerateCorridor()
    {
        if (connections.Contains(Vector3.right))
        {
            if (Random.Range(0, 2) == 0)
            {
                rotation.eulerAngles = new Vector3(0, 0, 180);
                Instantiate(rooms[0], transform.position, rotation);
                return;
            }
            Instantiate(rooms[0], transform.position, rotation);
            return;
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                rotation.eulerAngles = new Vector3(0, 0, 270);
                Instantiate(rooms[0], transform.position, rotation);
                return;
            }
            rotation.eulerAngles = new Vector3(0, 0, 90);
            Instantiate(rooms[0], transform.position, rotation);
            return;
        }
    }

    void GenerateTurn()
    {
        if (connections.Contains(Vector3.right))
        {
            if (connections.Contains(Vector3.up))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 180);
                    Instantiate(rooms[1], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 270);
                Instantiate(rooms[1], transform.position, rotation);
                return;

            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 90);
                    Instantiate(rooms[1], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 0);
                Instantiate(rooms[1], transform.position, rotation);
                return;
            }
        }
        else
        {
            if (connections.Contains(Vector3.up))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 270);
                    Instantiate(rooms[1], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(180, 0, 0);
                Instantiate(rooms[1], transform.position, rotation);
                return;

            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 0);
                    Instantiate(rooms[1], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 90);
                Instantiate(rooms[1], transform.position, rotation);
                return;
            }
        }
    }

    void GenerateDeadEnd()
    {
        if (connections.Contains(Vector3.right))
        {
            if (Random.Range(0, 2) == 0)
            {
                rotation.eulerAngles = new Vector3(0, 0, 180);
                Instantiate(rooms[4], transform.position, rotation);
                return;
            }
            rotation.eulerAngles = new Vector3(0, 180, 0);
            Instantiate(rooms[4], transform.position, rotation);
            return;
        }
        if (connections.Contains(Vector3.down))
        {
            if (Random.Range(0, 2) == 0)
            {
                rotation.eulerAngles = new Vector3(0, 0, 90);
                Instantiate(rooms[4], transform.position, rotation);
                return;
            }
            rotation.eulerAngles = new Vector3(180, 0, 270);
            Instantiate(rooms[4], transform.position, rotation);
            return;
        }
        if (connections.Contains(Vector3.left))
        {
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(rooms[4], transform.position, rotation);
                return;
            }
            rotation.eulerAngles = new Vector3(180, 0, 0);
            Instantiate(rooms[4], transform.position, rotation);
            return;
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                rotation.eulerAngles = new Vector3(0, 0, 270);
                Instantiate(rooms[4], transform.position, rotation);
                return;
            }
            rotation.eulerAngles = new Vector3(180, 0, 90);
            Instantiate(rooms[4], transform.position, rotation);
            return;
        }
    }

    void Generate3Ways()
    {
        if (connections.Contains(Vector3.left) && connections.Contains(Vector3.right))
        {
            if (connections.Contains(Vector3.up))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 180);
                    Instantiate(rooms[2], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(180, 0, 0);
                Instantiate(rooms[2], transform.position, rotation);
                return;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(rooms[2], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 0);
                Instantiate(rooms[2], transform.position, rotation);
                return;
            }
        }
        else
        {
            if (connections.Contains(Vector3.left))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 270);
                    Instantiate(rooms[2], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 90);
                Instantiate(rooms[2], transform.position, rotation);
                return;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 90);
                    Instantiate(rooms[2], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 270);
                Instantiate(rooms[2], transform.position, rotation);
                return;
            }
        }
    }

    void Generate4Ways()
    {
        int x = 0;
        int y = 0;
        int z;
        int[] rotations = {0, 90, 180, 270};
        if (Random.Range(0, 2) == 0)
        {
            x = 180;
        }
        if (Random.Range(0, 2) == 0)
        {
            y = 180;
        }
        z = rotations[Random.Range(0, 4)];
        rotation.eulerAngles = new Vector3(x, y, z);
        Instantiate(rooms[3], transform.position, rotation);
        return;
    }

    void GenerateClosed()
    {
        int x = 0;
        int y = 0;
        int z;
        int[] rotations = {0, 90, 180, 270};
        if (Random.Range(0, 2) == 0)
        {
            x = 180;
        }
        if (Random.Range(0, 2) == 0)
        {
            y = 180;
        }
        z = rotations[Random.Range(0, 4)];
        rotation.eulerAngles = new Vector3(x, y, z);
        Instantiate(rooms[7], transform.position, rotation);
        return;
    }

    void GenerateDoors(int nr)
    {
        {
            if (connections.Contains(Vector3.right))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 180);
                    Instantiate(rooms[nr], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(0, 180, 0);
                Instantiate(rooms[nr], transform.position, rotation);
                return;
            }
            if (connections.Contains(Vector3.down))
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 90);
                    Instantiate(rooms[nr], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(180, 0, 270);
                Instantiate(rooms[nr], transform.position, rotation);
                return;
            }
            if (connections.Contains(Vector3.left))
            {
                if (Random.Range(0, 2) == 0)
                {
                    Instantiate(rooms[nr], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(180, 0, 0);
                Instantiate(rooms[nr], transform.position, rotation);
                return;
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    rotation.eulerAngles = new Vector3(0, 0, 270);
                    Instantiate(rooms[nr], transform.position, rotation);
                    return;
                }
                rotation.eulerAngles = new Vector3(180, 0, 90);
                Instantiate(rooms[nr], transform.position, rotation);
                return;
            }
        }
    }
}