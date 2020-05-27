using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossScript : MonoBehaviour { 

    public scoreManager score;
    public int rollValue; //current dice value
    public float health;

    private Renderer boss;

    public int dexModifier;

    public Text announceText; //textbox for giving different instructions

    GameObject[] balls;
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {

    health = 100;

    score = GameObject.FindWithTag("score").GetComponent<scoreManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            announceText.text = "Victory! Press space to play again";


            //remove balls from play
            string[] allballs = { "ball", "multiball" };

            foreach (string tag in allballs)
            {
                GameObject[] balls = GameObject.FindGameObjectsWithTag(tag);

                for (var i = 0; i < balls.Length; i++)
                    Destroy(balls[i]);
            }

            //hide the boss
            boss = gameObject.GetComponent<Renderer>();
            boss.enabled = false;


            //reload scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            }
        }
    }


    

    void OnCollisionEnter(Collision col)
    {
        //deduct damage from boss when hit with dice

        if (col.gameObject.tag == "ball" || col.gameObject.tag == "multiball")
        {
            rollValue = Random.Range(1, 21) + dexModifier;
            Debug.Log(rollValue);

            if (rollValue == 20f)
            {
                bool isCriticalHit = true;
                damagePopup.Create(transform.position, rollValue, isCriticalHit);

            }
            else
            {
                bool isCriticalHit = false;
                damagePopup.Create(transform.position, rollValue, isCriticalHit);
            }

            health = health - rollValue;

            score.IncrementScore(rollValue);
        }


    }
}
