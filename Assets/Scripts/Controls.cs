﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float movementSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector2(0.0f, movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector2(-movementSpeed, 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector2(0.0f, -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector2(movementSpeed, 0.0f);
        }
        gameObject.transform.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime;
	}
}