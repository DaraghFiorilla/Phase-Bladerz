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
    [SerializeField] private int attackPower;
    private bool canAttack = true;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator animator;
    public bool weaponEquipped;
    [SerializeField] private int maxWeaponCharge;
    private int weaponCharge;

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

    IEnumerator Attack(float cooldownSeconds)
    {
        animator.SetTrigger("attacking");
        canAttack = false;
        areaDisplay.enabled = true;
        attackArea.enabled = true;
        //Debug.Log(attackArea.enabled);
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
            DamagePlayer(attackPower);
            if (other.GetComponentInParent<PlayerCombat>().weaponCharge != 0)
            {
                other.GetComponentInParent<PlayerCombat>().weaponCharge--;
            }
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
        }
    }

    private void RemoveWeapon()
    {
        weaponEquipped = false;
        weaponCharge = 0;
        attackPower = 4;
        attackCooldown = 0.4f;
    }
}
