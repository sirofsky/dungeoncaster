using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damagePopup : MonoBehaviour
{
    //create a damage popup

    public static damagePopup Create(Vector3 position, int damageAmount, bool isCriticalHit) {
        Transform damagePopupTransform = Instantiate(gameAssets.i.pfDamagePopup, position, Quaternion.Euler(90, 0, 0));

        damagePopup damagePopup = damagePopupTransform.GetComponent<damagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit) {
        textMesh.SetText(damageAmount.ToString());

        //setting up the text to fade away after a few seconds. 

        textColor = textMesh.color;
        disappearTimer = 1f;

        //for critical hits

        if (!isCriticalHit) {
            //normal hit
            textMesh.fontSize = 6;
        } else
        {
            //critical hit
            textMesh.fontSize = 10;
        }
    }

    private void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, 0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //start disappearing
            float disappearSpeed = 10f;
            textColor.a -= disappearSpeed * Time.deltaTime;

            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
