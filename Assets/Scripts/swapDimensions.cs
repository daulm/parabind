using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapDimensions : MonoBehaviour {

    public bool dim1 = true;
    private bool warpsuccess = true;
    public GameObject goggle1;
    public GameObject goggle2;
    public GameObject player1;
    public GameObject player2;


	// Use this for initialization
	void Start () {
        //disable camera, collider, and movement of d2
        transform.Find("Player_d2").gameObject.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = false;
        transform.Find("Player_d2").gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //transform.Find("Player_d2").gameObject.GetComponent<characterControllerD2>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire3"))
        {
            warpsuccess = false;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            warpsuccess = true;
        }

        if(Input.GetButton("Fire3"))
        {
            if (dim1 && !warpsuccess)
            {         
                // enable dimension 2
                player2.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = true;
                player2.GetComponent<CapsuleCollider>().enabled = true;
                player2.GetComponent<Rigidbody>().WakeUp();


                //disable dimension 1
                player1.transform.Find("Camera_d1").gameObject.GetComponent<Camera>().enabled = false;
                player1.GetComponent<CapsuleCollider>().enabled = false;
                player1.GetComponent<Rigidbody>().Sleep();

                // give velocity to player in dim2
                transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().velocity = transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().velocity;

                //turn off goggles
                goggle1.GetComponent<Renderer>().enabled = false;
                goggle2.GetComponent<Renderer>().enabled = false;

                dim1 = false;
                warpsuccess = true;

            }
            else if(!dim1 && !warpsuccess)
            {
                // enable dimension 1
                player1.transform.Find("Camera_d1").gameObject.GetComponent<Camera>().enabled = true;
                player1.GetComponent<CapsuleCollider>().enabled = true;
                player1.GetComponent<Rigidbody>().WakeUp();

                // disable dimension 2
                transform.Find("Player_d2").gameObject.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = false;
                transform.Find("Player_d2").gameObject.GetComponent<CapsuleCollider>().enabled = false;
                player2.GetComponent<Rigidbody>().Sleep();

                // give velocity to player in dim2
                transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().velocity = transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().velocity;

                //turn off goggles
                goggle1.GetComponent<Renderer>().enabled = false;
                goggle2.GetComponent<Renderer>().enabled = false;

                dim1 = true;
                warpsuccess = true;
            }
        }

            if (Input.GetButton("Fire2"))
        {
            //show player a view into the other dimension
            if(dim1)
            {
                goggle1.GetComponent<Renderer>().enabled = true;
            } else
            {
                goggle2.GetComponent<Renderer>().enabled = true;
            }

        }

        if (Input.GetButtonUp("Fire2"))
        {
            goggle1.GetComponent<Renderer>().enabled = false;
            goggle2.GetComponent<Renderer>().enabled = false;
        }


    }

    private bool CanWarp()
    {
        //check to see if the sensor is active in the other dimension
        if(dim1 && transform.Find("Player_d2").gameObject.transform.Find("WarpSensor2").GetComponent<warpSensor>().canwarp)
        {
            return true;
        }
        if(!dim1 && transform.Find("Player_d1").gameObject.transform.Find("WarpSensor1").GetComponent<warpSensor>().canwarp)
        {
            return true;
        }

        return false;
    }
}
