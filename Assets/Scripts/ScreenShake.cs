using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool startShake;
    [SerializeField] float duration;
    [SerializeField] float strength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startShake)
        {
            startShake = false;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = startPos + Random.insideUnitSphere * strength;
            if (transform.position.z >= -1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
            elapsedTime+= Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }
}
