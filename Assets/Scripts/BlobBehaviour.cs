using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehaviour : MonoBehaviour
{
    //when canBeAttached, can attach
    //when released, can not attach for timer < 1 second

    float timer;
    bool canBeAttached;
    bool attached;

    // Use this for initialization
    void Start()
    {
        timer = 1.0f;
        canBeAttached = true;
        attached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 1.0f)
        {
            timer += Time.deltaTime;
            if(timer >= 1.0f)
            {
                canBeAttached = true;
            }
        }
    }

    public bool CanBeAttached()
    {
        return canBeAttached;
    }

    public void Attach(GameObject g)
    {
        gameObject.transform.parent = g.transform;
        attached = true;
    }

    public void Detach()
    {
        attached = false;
        canBeAttached = false;
        timer = 0.0f;
        gameObject.transform.parent = null;
    }

    public bool IsAttached()
    {
        return attached;
    }
}