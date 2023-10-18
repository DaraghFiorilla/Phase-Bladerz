using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCombatV2 : MonoBehaviour
{
    [Header("Health Variables:")]
    private int maxHealth = 100;
    public int playerHealth;
    [SerializeField] private Slider healthSlider;

    [Header("Attack Variables:")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackPower;
    [SerializeField] private float knockbackStrength;
    public bool canAttack;

    [Header("Weapon Variables:")]
    public bool weaponEquipped;
    [SerializeField] private int maxWeaponCharge;
    private int weaponCharge;
    [SerializeField] private TextMeshProUGUI weaponText;
    public bool isInWeaponTrigger;
    public GameObject weapon;

    [Header("Other:")]
    private Rigidbody2D rb;
    private PlayerMovementV2 playerMovement;
    public Animator animator;
    [SerializeField] private InputActionReference attack;

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
        playerMovement = gameObject.GetComponent<PlayerMovementV2>();
        attackPower = 4;
        attackCooldown = 0.5f;
        weaponText.text = "Active weapon =  NONE";
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponEquipped && weaponCharge == 0)
        {
            RemoveWeapon();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isInWeaponTrigger)
            {
                weapon.GetComponent<WeaponScript>().EquipWeapon();
            }
            else
            {
                Debug.Log("Attacking");
                animator.SetTrigger("attack");
            }
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
