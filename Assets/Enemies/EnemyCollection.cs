using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyCollection
{
    public Vector2 straitLineCheck(Vector3 firstPoint, Vector3 secondPoint){
        if (Math.Abs(firstPoint.x - secondPoint.x) < 0.2|| Math.Abs(firstPoint.y - secondPoint.y) < 0.2){
            return new Vector2(firstPoint.x - secondPoint.x, firstPoint.y - secondPoint.y);
        }
        return Vector2.zero;
    }

    public Quaternion Rotate(Vector2 dir){
        if (dir == new Vector2(1, 0)){
            return Quaternion.Euler(0, 0, 0);
        }
        else if (dir == new Vector2(-1, 0)){
            return Quaternion.Euler(0, 0, 180);
        }
        else if (dir == new Vector2(0, 1)){
            return Quaternion.Euler(0, 0, 90);
        }
        else {
            return Quaternion.Euler(0, 0, 270);
        }
    }
}
