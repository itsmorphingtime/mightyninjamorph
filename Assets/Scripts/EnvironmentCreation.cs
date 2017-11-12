using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCreation : MonoBehaviour {

    Vector2 oldPosition = new Vector2();
    Vector2 newPosition = new Vector2();
    Vector2 resulting = new Vector2();

    public Transform prefabs;
    public Transform prefabm;
    public Transform prefabl;

    const float offset = 4.0f;
    const float initial_offset = 40.0f;
    float xval;
    float yval;

    List<Vector2> obstacles = new List<Vector2>();

    /*
    void RotateVector2d(ref Vector2 v, float degrees)
    {
        float xi = v.x * Mathf.Cos(degrees * (Mathf.PI / 180)) - v.y * Mathf.Sin(degrees * (Mathf.PI / 180));
        float yi = v.x * Mathf.Sin(degrees * (Mathf.PI / 180)) + v.y * Mathf.Cos(degrees * (Mathf.PI / 180));
        v.x = xi;
        v.y = yi;
    }
    */

    void instantiateObstacles(int ioc,ref List<Vector2> obst, float xrange, float yrange)
    {
        int exit = 0;
        int counter = 0;
        int ioc_ = ioc;
        while (counter < ioc_ && exit < (ioc * 2))
        {

            xval = Random.Range(xrange - initial_offset, xrange + initial_offset);
            yval = Random.Range(yrange - initial_offset, yrange + initial_offset);

            Vector2 randomVec = new Vector2(xval, yval);

            while (obst.Count == 0)
            {
                Vector2 myPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                Vector2 mprv = new Vector2(randomVec.x - myPos.x, randomVec.y - myPos.y);
                if (mprv.magnitude > (2 * offset))
                {
                    obst.Add(randomVec);
                    Instantiate(prefabs, randomVec, Quaternion.identity);
                    counter++;
                }
            }

            float min = 100f;

            for (int i = 0; i < obst.Count; i++)
            {
                Vector2 v = new Vector2(obst[i].x - randomVec.x, obst[i].y - randomVec.y);

                if (v.magnitude < min)
                {
                    min = v.magnitude;
                }
            }

            if (min > (initial_offset / 2))
            {
                float pick = Random.Range(0f, 1f);
                if (pick < 0.3)
                {
                    obst.Add(randomVec);
                    Instantiate(prefabs, randomVec, Quaternion.identity);
                    counter++;
                }
                else if (pick > 0.3 && pick < 0.6)
                {
                    obst.Add(randomVec);
                    Instantiate(prefabm, randomVec, Quaternion.identity);
                    counter++;
                }
                else
                {
                    obst.Add(randomVec);
                    Instantiate(prefabl, randomVec, Quaternion.identity);
                    counter++;
                }
            }

            exit++;
        }
    }

    
    // Use this for initialization
    void Start () {

        float x = newPosition.x;
        float y = newPosition.y;
        instantiateObstacles(50, ref obstacles, x, y);
    }

    // Update is called once per frame
    bool check = true;
    void Update () {
        
        bool trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>().getIsMoving();
   
        if (trigger && check == true)
        {
            oldPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            check = false;
        }

        newPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        resulting = new Vector2(newPosition.x - oldPosition.x, newPosition.y - oldPosition.y);

        if (resulting.magnitude > (initial_offset / 2) && check == false)
        {
            check = true;           

            float x = newPosition.x;
            float y = newPosition.y;
            instantiateObstacles(15, ref obstacles, x, y);
        }
    }
}
