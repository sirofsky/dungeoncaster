using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontrol : MonoBehaviour
{

    public Vector3 currentVelocity; 
    public Vector3 ballVelocity; 

    public float startVelocity; //starting speed of ball
    public float maxVelocity; //maximum speed limit



    // Start is called before the first frame update
    void Start()
    {

        ballVelocity = new Vector3(startVelocity, 0, -startVelocity); //starting velocity of ball
        GetComponent<Rigidbody>().velocity = ballVelocity;
    }


    // Update is called once per frame
    void Update()
    {
        //sets maximum speed ball can travel at

        currentVelocity = GetComponent<Rigidbody>().velocity;
        if (currentVelocity.z > maxVelocity)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, maxVelocity);
        }

        if (currentVelocity.z < -maxVelocity)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, -maxVelocity);
        }

        if(currentVelocity.x > maxVelocity)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(maxVelocity, 0, currentVelocity.z);
        }

        if (currentVelocity.x < -maxVelocity)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-maxVelocity, 0, currentVelocity.z);
        }

        //we don't want things to ever stop

        if(currentVelocity.z == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(currentVelocity.x, 0, maxVelocity/2);
        }

        if (currentVelocity.x == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(maxVelocity/2, 0, currentVelocity.z);
        }






        //makes dice rotate

        transform.Rotate(1, 2, 3);
    }


    public void largerBall()
    {

        StartCoroutine(largeTimer());
    }

    IEnumerator largeTimer()
    {

        gameObject.transform.localScale = new Vector3(.7f, .7f, .7f);
        yield return new WaitForSeconds(10);
        gameObject.transform.localScale = new Vector3(.35f, .35f, .35f);
    }


    void OnTriggerEnter(Collider collider)
    {
        //ball gets destroyed if out of frame

        if (collider.tag == "Respawn")
        {
            Destroy(gameObject, .1f);
        }
    }
}
