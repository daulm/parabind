using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

    Vector2 mouseLookTotal;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject character;

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLookTotal += smoothV;
        mouseLookTotal.y = Mathf.Clamp(mouseLookTotal.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLookTotal.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLookTotal.x, character.transform.up);
    }
}
