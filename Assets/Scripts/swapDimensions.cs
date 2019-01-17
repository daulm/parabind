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
    public GameObject warpSensor1;
    public GameObject warpSensor2;
    private warpSensor sensor1;
    private warpSensor sensor2;
    private Vector3 oldPosition;
    public float shrinkScale;
    private Vector3 shrunkSize;
    private Vector3 fullSize;
    private bool shouldResize1 = false;
    private bool shouldResize2 = false;
    private float startResize1 = 0;
    private float startResize2 = 0;
    public float resizeTime;
    public float liftHeight = 0.8f;


    // Use this for initialization
    void Start () {
        //disable camera, collider, and movement of d2
        player2.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = false;
        //transform.Find("Player_d2").gameObject.GetComponent<CapsuleCollider>().layer = 8;
        //transform.Find("Player_d2").gameObject.GetComponent<characterControllerD2>().enabled = false;
        player2.GetComponent<CharacterController>().enabled = false;
        //Physics.IgnoreLayerCollision(0, 9, true);

        sensor1 = warpSensor1.GetComponent<warpSensor>();
        sensor2 = warpSensor2.GetComponent<warpSensor>();
        //player2.GetComponent<Rigidbody>().Sleep();

        fullSize = player1.transform.localScale;
        shrunkSize = player1.transform.localScale - new Vector3(shrinkScale, shrinkScale, shrinkScale);

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Warp"))
        {
            warpsuccess = false;
        }

        if (Input.GetButtonUp("Warp"))
        {
            warpsuccess = true;
        }

        if(Input.GetButton("Warp"))
        {
            if (dim1 && !warpsuccess && CanWarp())
            {
                //save current location of player
                //oldPosition = player1.transform.position;
                // move to dimension 2
                //player1.transform.position = player2.transform.position;
                //player2.transform.position = oldPosition;

                // invert the gap between players
                //player1.GetComponent<characterControl>().dimension_xgap *= -1;
                //player1.GetComponent<characterControl>().dimension_ygap *= -1;

                //shrink player model
                player2.transform.localScale = shrunkSize;
                player2.transform.position += new Vector3(0, liftHeight, 0);
                shouldResize2 = true;
                startResize2 = Time.time;

                // move to dimension 2
                player2.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = true;
                player2.GetComponent<characterControl>().isActive = true;
                //Physics.IgnoreLayerCollision(0, 9, false);
                //player2.GetComponent<CapsuleCollider>().enabled = true;
                player2.GetComponent<CharacterController>().enabled = true;


                // disable dimension 1
                player1.transform.Find("Camera_d1").gameObject.GetComponent<Camera>().enabled = false;
                player1.GetComponent<characterControl>().isActive = false;
                //Physics.IgnoreLayerCollision(0, 8, true);
                //player1.GetComponent<CapsuleCollider>().enabled = false;
                player1.GetComponent<CharacterController>().enabled = false;


                // give velocity to player in dim1
                //transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().velocity = transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().velocity;

                //turn off goggles
                goggle1.GetComponent<Renderer>().enabled = false;
                goggle2.GetComponent<Renderer>().enabled = false;

                dim1 = false;
                warpsuccess = true;

            }
            else if(!dim1 && !warpsuccess && CanWarp())
            {
                //save current location of player
                //oldPosition = transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().position;
                // move to dimension 1
                //transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().position = transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().position;
                //transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().position = oldPosition;

                //shrink player model
                player1.transform.localScale = shrunkSize;
                player1.transform.position += new Vector3(0, liftHeight, 0);
                shouldResize1 = true;
                startResize1 = Time.time;

                // move to dimension 1
                player1.transform.Find("Camera_d1").gameObject.GetComponent<Camera>().enabled = true;
                player1.GetComponent<characterControl>().isActive = true;
                //Physics.IgnoreLayerCollision(0, 8, false);
                //player1.GetComponent<CapsuleCollider>().enabled = true;
                player1.GetComponent<CharacterController>().enabled = true;


                // disable dimension 2
                player2.transform.Find("Camera_d2").gameObject.GetComponent<Camera>().enabled = false;
                player2.GetComponent<characterControl>().isActive = false;
                //Physics.IgnoreLayerCollision(0, 9, true);
                //player2.GetComponent<CapsuleCollider>().enabled = false;
                player2.GetComponent<CharacterController>().enabled = false;


                // give velocity to player in dim1
                //transform.Find("Player_d1").gameObject.GetComponent<Rigidbody>().velocity = transform.Find("Player_d2").gameObject.GetComponent<Rigidbody>().velocity;

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

            if(dim1 && CanWarp())
            {
                goggle1.GetComponent<Renderer>().enabled = true;
            } else if(!dim1 && CanWarp())
            {
                goggle2.GetComponent<Renderer>().enabled = true;
            }

            if(!CanWarp())
            {
                goggle1.GetComponent<Renderer>().enabled = false;
                goggle2.GetComponent<Renderer>().enabled = false;
            }

        }

        if (Input.GetButtonUp("Fire2"))
        {
            goggle1.GetComponent<Renderer>().enabled = false;
            goggle2.GetComponent<Renderer>().enabled = false;
        }

        if (shouldResize1)
        {
            if(Time.time - startResize1 > resizeTime)
            {
                shouldResize1 = false;
                player1.transform.localScale = fullSize;
            }
            else
            {
                player1.transform.localScale = Resize(shrunkSize, fullSize, startResize1, resizeTime);
            }
        }

        if (shouldResize2)
        {
            if (Time.time - startResize2 > resizeTime)
            {
                shouldResize2 = false;
                player1.transform.localScale = fullSize;
            }
            else
            {
                player2.transform.localScale = Resize(shrunkSize, fullSize, startResize2, resizeTime);
            }
        }


    }

    private bool CanWarp()
    {

        //check to see if the sensor is active in the other dimension
        if(dim1 && sensor2.canwarp)
        {
            return true;
        }
        if(!dim1 && sensor1.canwarp)
        {
            return true;
        }

        return false;
    }

    public Vector3 Resize(Vector3 startSize, Vector3 endSize, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(startSize, endSize, percentageComplete);

        return result;
    }
}
