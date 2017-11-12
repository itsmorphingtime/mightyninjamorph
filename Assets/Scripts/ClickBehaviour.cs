using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosVec3 = Input.mousePosition;
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(mousePosVec3).x, Camera.main.ScreenToWorldPoint(mousePosVec3).y);
            Collider2D hitCollider = Physics2D.OverlapCircle(mousePos, 1.0f);
            if (hitCollider)
            {
                if(hitCollider.gameObject.tag == "Blob")
                {
                    hitCollider.gameObject.GetComponent<BlobBehaviour>().Detach();
                    hitCollider.gameObject.transform.parent = null;
                }
            }
        }
	}
}
