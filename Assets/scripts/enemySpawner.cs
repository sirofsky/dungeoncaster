using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    Vector3[] enemyPos; //declaring array of spaces the enemy can spawn

    public GameObject enemy; //identifying the enemy prefab

    public int length; //number of enemies per row
    public int height; //number of rows per level

    public float spacing; //distance from one enemy to another


    // Start is called before the first frame update
    void Start()
    {
            for (int j = 0; j < length; j++) {  //how many columns wide

            

            for (int i = 0; i < height; i++) //how many rows tall
            { 
                enemyPos = new Vector3[height];
                enemyPos[i] = new Vector3(transform.position.x - ((spacing * length) - spacing)/2 + (j * spacing), 0, transform.position.z - ((spacing * height) - spacing)/2 + (i * spacing));
                //some math to help make the enemies spawn centered no matter how many spawn

                //drop those enemies on the board in an array
                Instantiate(enemy, enemyPos[i], Quaternion.Euler(90,0,0));

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
