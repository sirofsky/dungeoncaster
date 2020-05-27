using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddlemover : MonoBehaviour
{

    public KeyCode moveL;
    public KeyCode moveR;

    public float speed; //speed of paddle

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(moveL)){
        GetComponent<Rigidbody>().velocity = new Vector3 (-speed, 0, 0);
        }
        if(Input.GetKeyDown(moveR))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
        }
        if (Input.GetKeyUp(moveL) || Input.GetKeyUp(moveR))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}
