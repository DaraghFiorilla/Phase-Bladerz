using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private int maxHealth = 100;
    public int playerHealth;
    [SerializeField]private GameObject attackAreaObject;
    private Collider2D attackArea;
    private SpriteRenderer areaDisplay;
    private PlayerMovement playerMovement;
    [SerializeField] private float attackCooldown = 0.5f;
    private bool canAttack = true;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHealth;
        playerHealth = maxHealth;
        healthSlider.value = playerHealth;
        attackArea = attackAreaObject.GetComponent<Collider2D>();
        areaDisplay = attackAreaObject.GetComponent<SpriteRenderer>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        attackArea.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.playerOne)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canAttack)
            {
                StartCoroutine(Attack(attackCooldown));
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightControl) && canAttack)
            {
                StartCoroutine(Attack(attackCooldown));
            }
        }
    }

    IEnumerator Attack(float cooldownSeconds)
    {
        animator.SetTrigger("attacking");
        canAttack = false;
        areaDisplay.enabled = true;
        attackArea.enabled = true;
        Debug.Log(attackArea.enabled);
        yield return new WaitForSeconds(cooldownSeconds);
        areaDisplay.enabled = false;
        attackArea.enabled = false;
        canAttack = true;
        Debug.Log("Can attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damaging"))
        {
            DamagePlayer(10);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerHealth -= damage;
        healthSlider.value = playerHealth;
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
