﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour {

    public float speed = 200.0F;
    public float airmodifier;
    public float maxSlope = 60.0f;
    public bool grounded = false;
    public bool groundedLastFrame = false;
    public bool jumpLastFrame = false;
    public float jumpVelocity = 200.0f;
    public float dimension_ygap;
    public float dimension_xgap;
    public GameObject shadowplayer;
    public Rigidbody rb;
    public GameObject warpSensor;
    private warpSensor sensor;
    public Vector3 originalAirSpeed;
    public float airChangeLimit;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;


    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        sensor = warpSensor.GetComponent<warpSensor>();
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        if (grounded)
        {
            //transform.Translate(straffe, 0, translation);
            rb.velocity = transform.TransformDirection(new Vector3(straffe, rb.velocity.y, translation));
            //d1_rb.velocity = new Vector3(straffe, 0, translation);
            
            if (!groundedLastFrame)
            {
                jumpLastFrame = false;
            }
            groundedLastFrame = true;
        } else
        {
            //if we have a vertical velocity and aren't touching the ground and we didn't jump, remove the vertical velocity
            if(rb.velocity.y > 0 && groundedLastFrame && !jumpLastFrame)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            //if we won't move too fast, apply a small force to our movement in air
	    Vector3 currentSpeed = rb.velocity;
        rb.AddRelativeForce(new Vector3(straffe * airmodifier, 0, translation * airmodifier), ForceMode.Acceleration);
	    if(Mathf.Abs(rb.velocity.x - originalAirSpeed.x) > airChangeLimit)
	    {
		    rb.velocity = new Vector3(originalAirSpeed.x, rb.velocity.y, rb.velocity.z);
	    }
	    if(Mathf.Abs(rb.velocity.z - originalAirSpeed.z) > airChangeLimit)
	    {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, originalAirSpeed.z);
            }


            groundedLastFrame = false;
        }


        //set the location of the object in other dimension
        if (GetComponent<CapsuleCollider>().enabled)
        {
            shadowplayer.transform.position = new Vector3(transform.position.x + dimension_xgap, transform.position.y + dimension_ygap+0.1f, transform.position.z);
            //if the warp sensor is active we have fallen through the floor and should be bounced back above ground
            if (!sensor.canwarp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            }
        }

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetButtonDown("Jump") && grounded) {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity);
            jumpLastFrame = true;
	    originalAirSpeed = rb.velocity;
        }
    }

    void OnCollisionStay(Collision collision) {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            {
                grounded = true;
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        grounded = false;
    }

}
