using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] TextMeshPro textMesh;

    [SerializeField] float moveYSpeed = 20f;
    [SerializeField] float disappearSpeed = 3f;
    [SerializeField] float disappearTimer = 1f;

    float disappearTimer;
    Color textColor;

    public void Setup(int damage){
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
    }

    void Update(){
        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer <= 0){
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0){
                Destroy(gameObject);
            }
        }
    }

}
