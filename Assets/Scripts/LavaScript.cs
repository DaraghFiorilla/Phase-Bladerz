using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] private int lavaDamage;
    private PlayerCombatV2 playerCombat;
    //[SerializeField] private float timeBetweenFlash;
    [SerializeField] private GameObject warningObject;
    [SerializeField] private GameObject lavaObject;
    public bool isWarning;
    private Collider2D boxCollider;

    private void Start()
    {
        boxCollider = lavaObject.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (isWarning)
        {
            warningObject.SetActive(true);
            boxCollider.enabled = false;
        }
        else
        {
            warningObject.SetActive(false);
            lavaObject.SetActive(true);
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat = other.GetComponent<PlayerCombatV2>();
            playerCombat.DamagePlayer(gameObject, lavaDamage);
        }
    }
}
