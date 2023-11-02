using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    [SerializeField] private int lavaDamage;
    private PlayerCombatV2 playerCombat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat = other.GetComponent<PlayerCombatV2>();
            playerCombat.DamagePlayer(gameObject, lavaDamage);
        }
    }
}
