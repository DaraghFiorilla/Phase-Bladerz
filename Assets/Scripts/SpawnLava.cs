using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLava : MonoBehaviour
{
    [SerializeField] private GameObject lavaPrefab;
    [SerializeField] private float lavaInterval;
    [SerializeField] private float warningTime;
    private float timer, warningTimer;

    // Start is called before the first frame update
    void Start()
    {
        timer = lavaInterval;
        warningTimer = warningTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            
        }

        timer -= Time.deltaTime;
    }
}
