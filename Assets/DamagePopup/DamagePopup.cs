using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    public static DamagePopup Create(Vector3 position, int damage, bool isCrit){
        GameObject damagePopup = Instantiate(GameAssets.i.damagePopup, position, Quaternion.identity);
        damagePopup.GetComponent<DamagePopup>().Setup(damage, isCrit);

        return damagePopup.GetComponent<DamagePopup>();
    }

    [SerializeField] TextMeshPro textMesh;

    [SerializeField] Vector3 moveVector;
    [SerializeField] float disappearSpeed = 3f;
    [SerializeField] float disappearTimer = 1f;
    [SerializeField] int critFontSize = 45;
    [SerializeField] Color critColor;

    Color textColor;

    public void Setup(int damage, bool isCrit){
        textMesh.SetText(damage.ToString());
        if (isCrit){
            textMesh.fontSize = critFontSize;
            textMesh.color = critColor;
        }
        textColor = textMesh.color;
        moveVector.x = Random.Range(-moveVector.x, moveVector.x);
    }

    void Update(){
        transform.position += moveVector * Time.deltaTime;

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
