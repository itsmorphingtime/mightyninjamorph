using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    public string ID { get { return id; } set { id = value; } }
    string id;
    public string Name { get { return playerName; } set { playerName = value; } }
    string playerName;
    public int Score { get { return (int)Mathf.Floor(scoreFloat); } }
    int amountOfBlobs;
    public int Blobs { get { return amountOfBlobs; } }
    float scoreFloat;
    float upwardThrust;
    public float UpwardThrust { get { return upwardThrust; } }
    float leftThrust;
    public float LeftThrust { get { return leftThrust; } }
    float rightThrust;
    public float RightThrust { get { return rightThrust; } }
    float downwardThrust;
    public float DownwardThrust { get { return downwardThrust; } }
    float anticlockwiseTorque;
    public float AnticlockwiseTorque { get { return anticlockwiseTorque; } }
    float clockwiseTorque;
    public float ClockwiseTorque { get { return clockwiseTorque; } }


    // Use this for initialization
    void Start () {
        upwardThrust = 0.0f;
        leftThrust = 0.0f;
        rightThrust = 0.0f;
        downwardThrust = 0.0f;
        anticlockwiseTorque = 0.0f;
        clockwiseTorque = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        scoreFloat += amountOfBlobs * Time.deltaTime;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(rightThrust - leftThrust, upwardThrust - downwardThrust));
        gameObject.GetComponent<Rigidbody2D>().AddTorque(anticlockwiseTorque - clockwiseTorque);


        GameObject.FindGameObjectWithTag("UI").GetComponent<Text>().text = "Score: " + Score + System.Environment.NewLine +
            "Blobs: " + Blobs + System.Environment.NewLine +
            "Upward Thrust: " + upwardThrust + System.Environment.NewLine +
            "Left Thrust: " + leftThrust + System.Environment.NewLine +
            "Right Thrust: " + rightThrust + System.Environment.NewLine +
            "Downward Thrust: " + downwardThrust + System.Environment.NewLine +
            "Anticlockwise Torque: " + anticlockwiseTorque + System.Environment.NewLine +
            "Clockwise Torque: " + clockwiseTorque;
    }

    public void IncrementBlob()
    {
        amountOfBlobs++;
        
    }

    public void DecrementBlob()
    {
        amountOfBlobs--;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blob")
        {
            if (collision.gameObject.GetComponent<BlobBehaviour>().CanBeAttached())
            {
                collision.gameObject.GetComponent<BlobBehaviour>().Attach(gameObject);
                gameObject.GetComponent<PlayerAttributes>().IncrementBlob();
                switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
                {
                    case "leftThrust":
                        leftThrust += 1.0f;
                        break;
                    case "rightThrust":
                        rightThrust += 1.0f;
                        break;
                    case "upwardThrust":
                        upwardThrust += 1.0f;
                        break;
                    case "downwardThrust":
                        downwardThrust += 1.0f;
                        break;
                    case "anticlockwiseTorque":
                        anticlockwiseTorque += 1.0f;
                        break;
                    case "clockwiseTorque":
                        clockwiseTorque += 1.0f;
                        break;
                }
            }
        }
    }
}
