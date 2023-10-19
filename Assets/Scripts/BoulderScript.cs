using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    [SerializeField] private float boulderSpeed;
    [SerializeField] private int boulderDamage;
    private PlayerCombatV2 playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - boulderSpeed);

        if (gameObject.transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat = other.gameObject.GetComponent<PlayerCombatV2>();
            playerCombat.DamagePlayer(gameObject, boulderDamage);
            Destroy(gameObject);
        }
    }
}
