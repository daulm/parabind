using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpSensor : MonoBehaviour {

    public bool canwarp;
    public float data;
    
    

    // Use this for initialization
    void Start () {
        canwarp = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (true)
        {
            // player can't warp
            canwarp = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canwarp = true;
    }
}
