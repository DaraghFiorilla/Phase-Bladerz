using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using static PlayerCombat;

public class PlayerCombat : MonoBehaviour
{
    private int maxHealth = 100;
    public int playerHealth;
    [SerializeField]private GameObject attackAreaObject;
    private Collider2D attackArea;
    private SpriteRenderer areaDisplay;
    private PlayerMovement playerMovement;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackPower;
    private bool canAttack = true;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    public bool weaponEquipped;
    [SerializeField] private int maxWeaponCharge;
    private int weaponCharge;
    [SerializeField] private float knockbackStrength;
    private Rigidbody2D rb;
    private GameObject knockbackAttacker;
    private bool knockingback;
    [SerializeField] private TextMeshProUGUI weaponText;

    public enum WeaponType
    {
        Katana,
        Boomerang,
        Axe,
        Scythe
    }

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
        attackPower = 4;
        attackCooldown = 0.5f;
        weaponText.text = "Active weapon =  NONE";
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
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
        if (weaponEquipped && weaponCharge == 0)
        {
            RemoveWeapon();
        }
    }

    private void FixedUpdate()
    {
        if (knockingback)
        {
            Vector2 knockbackDirection = (gameObject.transform.position - knockbackAttacker.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
            knockingback = false;
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
        //Debug.Log("Can attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damaging"))
        {
            DamagePlayer(attackPower, other.transform.parent.gameObject);
            if (other.GetComponentInParent<PlayerCombat>().weaponCharge != 0)
            {
                other.GetComponentInParent<PlayerCombat>().weaponCharge--;
            }
        }
    }

    public void DamagePlayer(int damage, GameObject sender)
    {
        playerHealth -= damage;
        healthSlider.value = playerHealth;
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        knockbackAttacker = sender;
        knockingback = true;
        //Vector2 knockbackDirection = (gameObject.transform.position - sender.transform.position).normalized;
        //rb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
    }

    public void SetWeapon(WeaponType weaponType)
    {
        weaponCharge = maxWeaponCharge;
        weaponEquipped = true;
        Debug.Log(weaponType);

        switch (weaponType)
        {
            case WeaponType.Katana:
                {
                    attackPower = 8;
                    attackCooldown = 0.7f;
                    break;
                }
            case WeaponType.Boomerang:
                {
                    attackPower = 6;
                    attackCooldown = 1f;
                    break;
                }
            case WeaponType.Axe:
                {
                    attackPower = 10;
                    attackCooldown = 1f;
                    break;
                }
            case WeaponType.Scythe:
                {
                    attackPower = 7;
                    attackCooldown = 0.6f;
                    break;
                }

            default:
                {
                    attackPower = 4;
                    attackCooldown = 0.5f;
                    break;
                }

        }
        weaponText.text = "Active weapon =  " + weaponType.ToString().ToUpper();
    }

    private void RemoveWeapon()
    {
        weaponEquipped = false;
        weaponCharge = 0;
        attackPower = 4;
        attackCooldown = 0.4f;
        weaponText.text = "Active weapon =  NONE";
    }
}
