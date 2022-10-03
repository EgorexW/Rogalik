using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    public static GameObject Summon(GameObject subject, Vector3 pos){
        subject = Instantiate(subject, pos, Quaternion.identity);
        return subject;
    }
}
