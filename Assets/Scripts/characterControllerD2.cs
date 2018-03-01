using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControllerD2 : MonoBehaviour
{

    public float speed = 10.0F;
    public float maxSlope = 60.0f;
    public bool grounded = false;
    public float jumpVelocity = 200.0f;
    public float dimension_gap = -99.5f;
    public GameObject playerd1;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        //set the location of the object in D1
        if (GetComponent<CapsuleCollider>().enabled)
        {
            playerd1.transform.position = new Vector3(transform.position.x, transform.position.y + dimension_gap, transform.position.z);
        }
        

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            {
                grounded = true;
            }
        }
    }

    void OnCollisionExit()
    {
        grounded = false;
    }

}
