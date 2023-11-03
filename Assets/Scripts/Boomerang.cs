using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public PlayerCombatV2 parentCombat;
    [SerializeField] private float boomerangOffset;
    public Transform parentObject;
    private PlayerCombatV2 enemyCombat;
    [SerializeField] private int boomerangDamage;
    private Vector3 startingPos, targetPos;
    private bool targetReached;
    //private float timer = 5;

    void Start()
    {
        
    }

    public void ManualStart()
    {
        //parentCombat = GetComponentInParent<PlayerCombatV2>();
        //parentObject = gameObject.transform.parent;
        parentCombat.boomerangActive = true;
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

    void Update()
    {
        //timer -= Time.deltaTime;
        if (gameObject.transform.position != targetPos && !targetReached)
        {
            //Debug.Log("gameObject.transform.position != targetPos");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 0.02f);
        }
        else if (gameObject.transform.position == targetPos && !targetReached)
        {
            //Debug.Log("gameObject.transform.position == targetPos");
            targetReached = true;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startingPos, 0.02f);
        }
        else if (gameObject.transform.position != targetPos && targetReached)
        {
            //Debug.Log("gameObject.transform.position != targetPos && targetReached");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, startingPos, 0.02f);
        }

        if (gameObject.transform.position == startingPos && targetReached)
        {
            //Debug.Log("transform.position == startingPos && targetReached");
            parentCombat.weaponCharge--;
            parentCombat.weaponChargeSlider.value = parentCombat.weaponCharge;
            parentCombat.boomerangActive = false;
            Destroy(gameObject);
        }
        gameObject.transform.Rotate(0, 0, Time.deltaTime * 500);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.transform != parentObject.transform)
        {
            enemyCombat = other.GetComponent<PlayerCombatV2>();
            enemyCombat.DamagePlayer(gameObject, boomerangDamage);
        }
    }
}
