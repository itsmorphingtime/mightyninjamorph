using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float movementSpeed;
    public Rigidbody2D rb;
    bool isMoving;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}    
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 movement = new Vector2();
        isMoving = false;
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector2(0.0f, movementSpeed);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector2(-movementSpeed, 0.0f);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector2(0.0f, -movementSpeed);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector2(movementSpeed, 0.0f);
            isMoving = true;
        }

        rb.AddForce(new Vector2(movement.x, movement.y));
	}

    public bool getIsMoving() { return isMoving; }

    
}