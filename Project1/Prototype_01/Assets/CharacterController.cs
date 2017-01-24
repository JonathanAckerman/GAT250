using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    string up_key = "w";
    string left_key = "a";
    string down_key = "s";
    string right_key = "d";
    int moveSpeed = 10;
    Vector3 moveDir = new Vector3();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if (Input.GetKey(up_key))
        {
            moveDir += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(left_key))
        {
            moveDir += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(down_key))
        {
            moveDir += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(right_key))
        {
            moveDir += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        moveDir.Normalize();
        GetComponent<Transform>().position = moveDir;
	}
}
