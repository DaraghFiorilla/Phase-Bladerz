using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLava : MonoBehaviour
{
    [SerializeField] private GameObject lavaPrefab;
    [SerializeField] private float lavaInterval;
    [SerializeField] private float warningTime;
    [SerializeField] private float activeTime;
    private float timer;
    private bool lavaActive;
    private LavaScript lavaScript;

    // Start is called before the first frame update
    void Start()
    {
        timer = lavaInterval;
        /*warningTimer = warningTime;
        activeTimer = activeTime;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!lavaActive && timer > 0)
        {
            timer -= Time.deltaTime;
            /*warningTimer = warningTime;
            activeTimer = activeTime;*/
        }
        else if (!lavaActive && timer < 0)
        {
            StartCoroutine(Lava());
        }
        /*else if (warningActive && !lavaActive)
        {
            timer = lavaInterval;
            warningTimer -= Time.deltaTime;
            activeTimer = activeTime;
        }
        else if (!warningActive && lavaActive)
        {
            timer = lavaInterval;
            warningTimer = warningTime;
            activeTimer -= Time.deltaTime;
        }
        else
        {
            // PANIC
        }*/
    }

    IEnumerator Lava()
    {
        lavaActive = true;
        timer = lavaInterval;
        GameObject lavaObject = Instantiate(lavaPrefab);
        lavaScript = lavaObject.GetComponent<LavaScript>();
        lavaScript.isWarning = true;
        yield return new WaitForSeconds(warningTime);
        lavaScript.isWarning = false;
        yield return new WaitForSeconds(activeTime);
        Destroy(lavaObject);
    }
}
