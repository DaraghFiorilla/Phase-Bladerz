using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLava : MonoBehaviour
{                                                                           // Put this script on player?
    //[SerializeField] private GameObject[] playerObjects;
    [Tooltip("How often the script will check the player's position to see if they haven't moved enough")][SerializeField] private float checkInterval;
    [Tooltip("How far the player can be from their last check position before lava spawns")][SerializeField] private float checkDistance;
    [SerializeField] private GameObject lavaPrefab;
    private float timer;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //playerObjects = GameObject.FindGameObjectsWithTag("Player");
        timer = checkInterval;
        this.lastPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            CheckLastPos();
        }

        timer -= Time.deltaTime;
    }
    
    void CheckLastPos()
    {
        float distance = Vector2.Distance(this.lastPos, gameObject.transform.position);
        //Debug.Log("Distance = " + distance);
        if (distance > checkDistance)
        {
            //Debug.Log("Spawning lava");
        }

        lastPos = gameObject.transform.position;
        timer = checkInterval;
    }
}
