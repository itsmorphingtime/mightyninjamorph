using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCreator : MonoBehaviour {

    public GameObject blobPrefab;
    Vector2 playerOldPosition;

	// Use this for initialization
	void Start () {
        playerOldPosition = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerPos = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        if ((playerPos - playerOldPosition).magnitude > 10.0f)
        {
            //discern the position to spawn around
            Vector2 baseSpawnAttemptLocation = playerPos + (playerPos - playerOldPosition).normalized * 40;
            //find a place to spawn a blob
            for(int i = 0; i < 100; i++)
            {
                Vector2 deltaAttempt = Random.insideUnitCircle * 10;
                Collider2D nothingThere = Physics2D.OverlapCircle(baseSpawnAttemptLocation + deltaAttempt, 1.0f);
                if (nothingThere == null)
                {
                    GameObject newBlob = Instantiate(blobPrefab, baseSpawnAttemptLocation + deltaAttempt, Quaternion.identity) as GameObject;
                    int rand = Random.Range(1, 6);
                    switch (rand)
                    {
                        case 1:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LeftThrust");
                            break;
                        case 2:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("rightThrust");
                            break;
                        case 3:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("upwardThrust");
                            break;
                        case 4:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("downwardThrust");
                            break;
                        case 5:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("anticlockwiseTorque");
                            break;
                        case 6:
                            newBlob.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("clockwiseTorque");
                            break;

                    }
                    
                    break;
                }
            }
            playerOldPosition = playerPos;
        }
	}
}
