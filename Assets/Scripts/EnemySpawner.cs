using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    Vector2 playerOldPosition;
    public GameObject enemyPrefab;

    // Use this for initialization
    void Start () {
        playerOldPosition = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = new Vector2(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        if ((playerPos - playerOldPosition).magnitude > 10.0f)
        {
            //discern the position to spawn around
            Vector2 baseSpawnLocation = playerPos + (playerPos - playerOldPosition).normalized * 40;
            //find a place to spawn a blob
            Vector2 delta = Random.insideUnitCircle * 10;

            Instantiate(enemyPrefab, baseSpawnLocation + delta, Quaternion.identity);
            playerOldPosition = playerPos;
        }
    }
}
