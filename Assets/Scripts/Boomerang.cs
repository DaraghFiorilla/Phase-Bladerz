using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private PlayerCombatV2 parentCombat;
    [SerializeField] private float boomerangOffset;
    private Transform parentObject;
    private PlayerCombatV2 enemyCombat;
    [SerializeField] private int boomerangDamage;
    private Vector3 startingPos, targetPos;
    private bool targetReached;

    // Start is called before the first frame update
    void Start()
    {
        parentCombat = GetComponentInParent<PlayerCombatV2>();
        parentObject = gameObject.transform.parent;
        gameObject.transform.position = parentObject.transform.position;
        startingPos = gameObject.transform.position;
        if (parentCombat.isFacingRight)
        {
            targetPos = new Vector3(startingPos.x + boomerangOffset, startingPos.y);
        }
        else
        {
            targetPos = new Vector3(startingPos.x - boomerangOffset, startingPos.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != targetPos && !targetReached)
        {
            Debug.Log("gameObject.transform.position != targetPos");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 0.01f);
        }
        else if (gameObject.transform.position == targetPos && !targetReached)
        {
            Debug.Log("gameObject.transform.position == targetPos");
            targetReached = true;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startingPos, 0.01f);
        }
        else if (/*gameObject.transform.position != targetPos &&*/ targetReached)
        {
            Debug.Log("gameObject.transform.position != targetPos && targetReached");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startingPos, 0.01f);
        }
        else if (gameObject.transform.position == startingPos && targetReached)
        {
            Debug.Log("transform.position == startingPos && targetReached");
            parentCombat.weaponCharge--;
            Destroy(gameObject);
        }
        gameObject.transform.Rotate(0, 0, Time.deltaTime * 500);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject != parentObject)
        {
            enemyCombat = other.GetComponent<PlayerCombatV2>();
            enemyCombat.DamagePlayer(gameObject, boomerangDamage);
        }
    }
}
