using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private int movementDir;
    public float maxOffset;
    public float movementSpeed;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
        movementDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < startPos.x - maxOffset)
        {
            movementDir = 1;
        }
        else if (gameObject.transform.position.x > startPos.x + maxOffset)
        {
            movementDir = -1;
        }

        gameObject.transform.position = new Vector2(movementDir * movementSpeed, 0);
    }
}
