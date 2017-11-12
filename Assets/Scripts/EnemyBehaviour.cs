using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour {

    enum EnemyState
    {
        IDLE,
        CHASING,
        ATTACKING
    }

    enum LazerState
    {
        PREPARE,
        CHARGING,
        FIRING
    }

    EnemyState state;
    LazerState lazerState;
    float timer;
    public float movementSpeed;
    public float detectionDistance;
    Vector2 firingDirection;
    GameObject lazer;

	// Use this for initialization
	void Start () {
        state = EnemyState.IDLE;
        lazerState = LazerState.PREPARE;
        timer = 0.0f;
        firingDirection = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 distanceBetween3D = (GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position);
        Vector2 distanceBetween = new Vector2(distanceBetween3D.x, distanceBetween3D.y);

        if (state == EnemyState.IDLE)
        {
            if(distanceBetween.magnitude <= detectionDistance)
            {
                state = EnemyState.CHASING;
                timer = 0.0f;
            }
        }
        else if(state == EnemyState.CHASING)
        {
            if (distanceBetween.magnitude >= detectionDistance)
            {
                state = EnemyState.IDLE;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
                if(timer >= 4.0f)
                {
                    state = EnemyState.ATTACKING;
                    timer = 0.0f;
                    firingDirection = distanceBetween.normalized;
                }
                else
                {
                    distanceBetween3D.z = 0.0f;
                    gameObject.transform.position += distanceBetween3D.normalized * movementSpeed * Time.deltaTime;
                    Vector3 forward = Vector3.Cross(gameObject.transform.up, new Vector3(0.0f, 0.0f, 1.0f));
                    Vector2 forward2D = new Vector2(forward.x, forward.y);
                    gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -Vector2.Angle(forward2D, distanceBetween));
                }
            }
        }
        else if(state == EnemyState.ATTACKING)
        {
            UpdateLazerMesh(timer);
            if(distanceBetween.magnitude >= detectionDistance || timer >= 4.0f)
            {
                state = EnemyState.IDLE;
                timer = 0.0f;
                Destroy(lazer);
                lazerState = LazerState.PREPARE;
            }
            else
            {
                timer += Time.deltaTime;
                if(timer >= 2.0f)
                {
                    FireLazer();
                }
            }
        }
	}

    void UpdateLazerMesh(float time)
    {
        if(lazerState == LazerState.PREPARE)
        {
            lazer = Instantiate(Resources.Load("Lazer"), gameObject.transform, false) as GameObject;
            Color c = lazer.GetComponentInChildren<Renderer>().material.color;
            c.a = 0.5f;
            lazer.GetComponentInChildren<Renderer>().material.color = c;
            lazerState = LazerState.CHARGING;
        }
        else if(lazerState == LazerState.CHARGING)
        {
            Vector3 lScale = lazer.transform.localScale;
            lScale.y -= Time.deltaTime / 2.0f;
            lazer.transform.localScale = lScale;
            if(time >= 2.0f)
            {
                lazerState = LazerState.FIRING;
                Color c = lazer.GetComponentInChildren<Renderer>().material.color;
                c.a = 1.0f;
                lazer.GetComponentInChildren<Renderer>().material.color = c;
            }
        }
        else if(lazerState == LazerState.FIRING)
        {
            
            Vector3 lScale = lazer.transform.localScale;
            if(lScale.y >= 1.0f)
            {
                lScale.y -= Time.deltaTime / 0.5f;
            }
            else
            {
                lScale.y += Time.deltaTime / 0.1f;
            }
            lazer.transform.localScale = lScale;
        }
    }

    void FireLazer()
    {
        RaycastHit2D[] lazerHits = Physics2D.CircleCastAll(gameObject.transform.position, 1.0f, firingDirection);
        foreach(RaycastHit2D lazerHit in lazerHits)
        {
            if(lazerHit.collider.gameObject.tag == "Player")
            {
                GameObject player = lazerHit.collider.gameObject;
                if(player.GetComponent<PlayerAttributes>().Blobs <= 0)
                {
                    //game over
                    SceneManager.LoadScene("gameOver");
                }
            }
            else if(lazerHit.collider.gameObject.tag == "Blob" && lazerHit.collider.gameObject.GetComponent<BlobBehaviour>().IsAttached())
            {
                lazerHit.collider.gameObject.GetComponent<BlobBehaviour>().Detach();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>().DecrementBlob();
            }
        }
    }
}
