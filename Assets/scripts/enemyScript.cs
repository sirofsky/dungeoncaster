using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyScript1 : MonoBehaviour
{

    float[] healthValue = { 10, 20, 30 };
    public Sprite[] enemySprites;
    

    public GameObject boom;
    public float health; //health value for each enemy
    public int rollValue; //current dice value
    public int dexModifier;

    private Vector3 enemyPosition;

    public scoreManager score;

    private int rand;


    // Start is called before the first frame update
    void Start()
    {
        //randomly assigning each enemy a different sprite and associated health level
        rand = Random.Range(0, enemySprites.Length);
        GetComponent<SpriteRenderer>().sprite = enemySprites[rand];
        health = healthValue[rand];

        dexModifier = 0;

        score = GameObject.FindWithTag("score").GetComponent<scoreManager>();


    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = new Vector3(transform.position.x + (Random.Range(-2, 2)), 0, transform.position.z + (Random.Range(-2, 2)));
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "ball")
        {
            //when the ball hits the enemy, the enemy calculates how much damage is applied to it

            rollValue = Random.Range(1, 21) + dexModifier;
            Debug.Log(rollValue);

            health = health - rollValue;

            if (health <= 0)
            {

                score.IncrementScore(healthValue[rand]);
                Destroy(gameObject, 0.1f);
            

            }

            if (rollValue == 20f)
            {
                bool isCriticalHit = true;
                damagePopup.Create(transform.position, rollValue, isCriticalHit);

                Instantiate(boom, transform.position, Quaternion.identity);

       

            }
            else
            {
                bool isCriticalHit = false;
                damagePopup.Create(transform.position, rollValue, isCriticalHit);
            }
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "blast")
        {
            Debug.Log("boom");
            health = health - 10;
            bool isCriticalHit = false;
            damagePopup.Create(transform.position, Random.Range(10,20), isCriticalHit);
                
        }

    }

}

