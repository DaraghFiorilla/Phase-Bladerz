using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float strength;
    public bool startShaking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startShaking)
        {
            startShaking = false;
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        Vector2 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPos + Random.insideUnitCircle * strength;
            yield return null;
        }

        transform.position = startPos;
    }
}
