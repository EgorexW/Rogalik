using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int moveRange;
    [SerializeField] LayerMask blockLayer;
    private GameObject CM;
    [SerializeField]
    PlayerMain PM;

    void Start(){
        CM = GameObject.FindGameObjectWithTag("MainCamera");
        CM.transform.position = transform.position;
        CM.transform.position += new Vector3(0, 0, -10);
    }

    public bool Movement(float input, bool ver, bool onlyRotate)
    {
        float x = 0;
        float y = 0;

        if (ver) {
            y = input * moveRange;
            x = 0;
        } else {
            x = input * moveRange;
            y = 0;
        }

        if (onlyRotate){
            PM.dir = new Vector2(x, y);
            Rotate(new Vector2(x, y));
            return false;
        }

        Vector3 targetPos = transform.position + new Vector3(x, y, 0);

        Collider2D moveDetection = Physics2D.OverlapCircle(targetPos, 0.1f, blockLayer);

        if (moveDetection == null)
        {
            transform.position += new Vector3(x, y, 0);
            CM.transform.position = transform.position;
            CM.transform.position += new Vector3(0, 0, -10);
            PM.dir = new Vector2(x, y);
            Rotate(new Vector2(x, y));
            return true;
        } else if (moveDetection.tag == "Respawn"){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadLevel(false);
        }
        return false;
    }

    public void Rotate(Vector2 dir){
        if (dir == new Vector2(1, 0)){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir == new Vector2(-1, 0)){
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir == new Vector2(0, 1)){
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
    }
}
