using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkBehaviour : MonoBehaviour {
    public static string uri = "http://hacksussex.herokuapp.com/api";
    public static PlayerAttributes playerAttributes;
    float timer;

    [System.Serializable]
    class PartialPlayer
    {
        public string __v;
        public string _id;
        public string name;
        public int score;
        public Attributes attributes;
    }
    [System.Serializable]
    class Attributes
    {
        public string _id;
        public int amountOfBlobs;
        public float upwardThrust;
        public float leftThrust;
        public float rightThrust;
        public float downwardThrust;
        public float anticlockwiseTorque;
        public float clockwiseTorque;
        public System.DateTime timeStamp;
    }

	// Use this for initialization
	void Start () {
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();

        WWWForm form = new WWWForm();
        form.AddField("score", 0);

        WWW www = new WWW(uri, form);
        while (!www.isDone)
        {

        }
        PartialPlayer partialPlayer = JsonUtility.FromJson<PartialPlayer>(www.text);
        playerAttributes.ID = partialPlayer._id;
        playerAttributes.Name = partialPlayer.name;
    }
	
	// Update is called once per frame
	void Update () {
		if(timer >= 2.0f)
        {
            PartialPlayer partialPlayer = new PartialPlayer();
            partialPlayer._id = playerAttributes.ID;
            partialPlayer.name = playerAttributes.Name;
            partialPlayer.score = playerAttributes.Score;
            partialPlayer.attributes = new Attributes();
            partialPlayer.attributes.amountOfBlobs = playerAttributes.Blobs;            
            partialPlayer.attributes.anticlockwiseTorque = (int)playerAttributes.AnticlockwiseTorque;
            partialPlayer.attributes.clockwiseTorque = (int)playerAttributes.ClockwiseTorque;
            partialPlayer.attributes.downwardThrust = (int)playerAttributes.DownwardThrust;
            partialPlayer.attributes.leftThrust = (int)playerAttributes.LeftThrust;
            partialPlayer.attributes.rightThrust= (int)playerAttributes.RightThrust;
            partialPlayer.attributes.upwardThrust= (int)playerAttributes.UpwardThrust;
            partialPlayer.attributes.timeStamp= System.DateTime.Now;

            string json = JsonUtility.ToJson(partialPlayer);
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);

            //Dictionary<string, string> headers = new Dictionary<string, string>();
            //headers.Add("Content-Type", "application/json");
            //headers.Add("X-HTTP-Method-Override", "PUT");
            //WWW www = new WWW(uri, myData, headers);
            StartCoroutine(putRequest(uri, myData));
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
	}

    IEnumerator putRequest(string url, byte[] data)
    {
        UnityWebRequest uwr = UnityWebRequest.Put(uri, data);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("err");
        }
        else
        {
            Debug.Log("yay");
        }
    }
}
