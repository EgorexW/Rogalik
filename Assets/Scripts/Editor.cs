using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Editor : MonoBehaviour
{
    void OnGUI()
    {
        // GUI.Label (new Rect (10, 10, 100, 20), "Hello World!");
        Debug.Log("GUI");
        if(Event.current.isKey)
        {
            KeyCode key = Event.current.keyCode;
            Debug.Log(key);
            if (Application.isPlaying && key == KeyCode.F8){
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LoadLevel(true);
            }
        }
    }
}
