using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCreation : MonoBehaviour {

    Vector2 oldPosition = new Vector2();
    Vector2 newPosition = new Vector2();
    Vector2 resulting = new Vector2();

    bool check = true;

    public Transform prefab;

    

    // Use this for initialization
    void Start () {
        //Instantiate(prefab, new Vector2(newPosition.x + resulting.x, newPosition.y + resulting.y), Quaternion.identity);
        
    }
	
	// Update is called once per frame
	void Update () {
        bool trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().getIsMoving();
   
        if (trigger && check == true)
        {
            oldPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            check = false;
        }

        newPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        resulting = new Vector2(newPosition.x - oldPosition.x, newPosition.y - oldPosition.y);

        if (resulting.magnitude > 3.0 && check == false)
        {
            check = true;
            float val = Random.value;
            Instantiate(prefab, new Vector2(newPosition.x + resulting.x, newPosition.y + resulting.y), Quaternion.identity);
        }
    }
}
