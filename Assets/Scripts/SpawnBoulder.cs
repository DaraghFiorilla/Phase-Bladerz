using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoulder : MonoBehaviour
{
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private float boulderInterval;
    [Tooltip("Delay between multiple boulders spawning")][SerializeField] private float boulderDelay;
    [SerializeField] private int numToSpawn;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = boulderInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            SpawnBoulders(numToSpawn);
            timer = boulderInterval;
        }
        timer -= Time.deltaTime;
    }

    void SpawnBoulders(int numberToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            Debug.Log("Spawning boulder");
            Vector2 pos = spawnAreaCenter + new Vector2(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2));
            Instantiate(boulderPrefab, pos, Quaternion.identity);
            StartCoroutine(Wait(boulderDelay));
        }
    }

    IEnumerator Wait(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
    }

    public Vector2 spawnAreaCenter, spawnAreaSize;

    private void OnDrawGizmos() // This draws the area where boulders can spawn in the scene window
    {
        Gizmos.color = new Color(0, 255, 0, 100);
        Gizmos.DrawCube(spawnAreaCenter, spawnAreaSize);
    }
}
