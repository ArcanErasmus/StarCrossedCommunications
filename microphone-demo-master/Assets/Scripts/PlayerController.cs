using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float Xscale;
    Vector3 velocity;

	// Use this for initialization
	void Start () {
        Xscale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("a"))
        {
            transform.localScale.x = -Xscale;
        }
        if (Input.GetKey("d"))
        {
            transform.localScale.x = Xscale;
        }
	}
}