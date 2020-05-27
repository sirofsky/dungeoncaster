using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyBehavior : MonoBehaviour
{

    float[] healthValue = { 10, 20, 30 };
    public Sprite[] enemySprites;


    public GameObject boom;
    public GameObject powerUp;
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

    public void dexPowerup()
    //DEX MODIFIER POWER UP
    {

        StartCoroutine(dexTimer());

    }

    IEnumerator dexTimer() //keeps the dex modifier on a short timer 

    {
        Debug.Log("dex Power up start");
        dexModifier = 10;
        yield return new WaitForSeconds(20);
        dexModifier = 0;
   
        Debug.Log("dex Power up done");
    }


    // Update is called once per frame
    void Update()
    {
        //enemyPosition = new Vector3(transform.position.x + (Random.Range(-2, 2)), 0, transform.position.z + (Random.Range(-2, 2)));

       
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ball" || col.gameObject.tag == "multiball")
        {
            //when the ball hits the enemy, the enemy calculates how much damage is applied to it


            rollValue = Random.Range(1, 21) + dexModifier;
            Debug.Log(rollValue);

            health = health - rollValue;

            score.IncrementScore(rollValue);

            if (health <= 0)
            {

                score.IncrementScore(healthValue[rand]);

                if(Random.Range(1,21) > 12) //frequency of power ups spawning
                {
                    Instantiate(powerUp, transform.position, Quaternion.Euler(90, 0, 0));
;                }
           
                Destroy(gameObject, 0.1f); //delayed destroy so the ball bounces off the enemy before destroying


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
            int criticalBonus = Random.Range(10, 20);
            Debug.Log("boom");
            health = health - 10;
            bool isCriticalHit = false;
            damagePopup.Create(transform.position, criticalBonus, isCriticalHit);
            score.IncrementScore(criticalBonus);

        }

    }

}

