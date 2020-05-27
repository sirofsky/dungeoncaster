using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ballManager : MonoBehaviour
{

    public int lives; //how many lives the player has left 

    public Image[] hearts; //prefab image
    public GameObject ball; //prefab ball
    public GameObject multiball; //prefab multiball
    
    public bool ballInPlay; //is the game currently happening or are we waiting to start again? 

    GameObject[] balls; //array of all the balls in play, whether they're multiballs or normal balls
    public int ballsInPlay; //count of all the balls in play

    public Text announceText; //textbox for giving different instructions


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        ballInPlay = false;
        announceText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (!ballInPlay && lives > 0)
        {

            announceText.text = "press space to roll dice";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                announceText.text = "";
                Instantiate(ball, new Vector3(-4.5f, 0f, -4.5f), Quaternion.identity);
                ballInPlay = true;
            }

        }


        //setting up hearts on the playfield. Possibility of adding extra lives as a powerup in the future
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < lives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        //keeping track of how many balls are on the playfield at once
        string[] allballs = { "ball", "multiball" };

        foreach (string tag in allballs)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag(tag);

            Debug.Log("balls in play" + balls.Length);
            ballsInPlay = balls.Length;
        }



        //end of game, when there are no more lives
        if (lives == 0)
        {
            announceText.text = "game over...press space to restart";

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                   

            }
        }


    }



    //multiball powerup
    public void Multiball()
    {

        Instantiate(multiball, new Vector3(-4.5f, 0f, -4.5f), Quaternion.identity); //a multiball has entered the chat

    }
    
    //what happens when a ball hits the deathWall
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ball" || collider.gameObject.tag == "multiball")
        {
         
            if (ballsInPlay == 0) //if there are no more balls counted in play...
            {

                Debug.Log("deadball");
                lives = lives - 1;

                ballInPlay = false;
            }


        }

    }

    
}
