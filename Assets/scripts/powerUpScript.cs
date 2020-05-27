using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class powerUpScript : MonoBehaviour
{


    public Sprite[] powerSprites;
    private int rand;

    public scoreManager score;
    public ballManager multiball;
    public ballcontrol bigball;
    public enemyBehavior dexPower;
  

    GameObject[] enemies;
    GameObject enemy;
    GameObject boss;

    GameObject[] balls;
    GameObject anyball;


    // Start is called before the first frame update
    void Start()
    {
        //randomly assigning each powerup a different sprite
        rand = Random.Range(0, powerSprites.Length);
 
        GetComponent<SpriteRenderer>().sprite = powerSprites[rand];

        //connecting with the scripts that will execute the power ups
        multiball = GameObject.FindWithTag("Respawn").GetComponent<ballManager>();

        
        

    }

    // Update is called once per frame
    void Update()
    {
        //slowly move towards the bottom of the screen
        transform.position += new Vector3(0, 0, Random.Range(1,5) * -.2f * Time.deltaTime); 
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ball" || collider.gameObject.tag == "multiball")
        {
            //when the ball hits the powerup, the powerup activates

            if (rand == 0) //DEX MODIFIER POWER UP

                //find all the enemies so they know to increase their dex modifier
            {
                enemies = GameObject.FindGameObjectsWithTag("enemy");

                foreach(GameObject enemy in enemies)
                {
                    dexPower = enemy.GetComponent<enemyBehavior>();
                    

                    dexPower.dexPowerup();
                    

                }
            }

            if (rand == 1) //LARGER BALL

                //find all the balls, no matter what they're tagged
            {
                string[] allballs = { "ball", "multiball" };

                foreach (string tag in allballs)
                {
                    GameObject[] balls = GameObject.FindGameObjectsWithTag(tag);

                    foreach(GameObject anyball in balls)
                    {
                        bigball = anyball.GetComponent<ballcontrol>();

                        bigball.largerBall();
                    }
                }

                   
            }

            if (rand == 2) //MULTIBALL
            {
                multiball.Multiball();
            }



            Destroy(gameObject, 0f); //destroy the powerup immediately so the ball doesn't bounce off of it



        }

        if(collider.gameObject.tag == "Respawn")
        {
            Destroy(gameObject, 0f);
        }

    }



}

