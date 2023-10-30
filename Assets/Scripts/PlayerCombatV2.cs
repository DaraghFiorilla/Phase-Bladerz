using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;


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
    [SerializeField] private float knockbackDelay;
     [SerializeField] private AudioSource HitSoundEffect;
    //public bool canAttack;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float radius;

    [Header("Weapon Variables:")]
    public bool weaponEquipped;
    [SerializeField] private int maxWeaponCharge;
    private int weaponCharge;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private Slider weaponChargeSlider;
    public bool isInWeaponTrigger;
    public GameObject weapon;

    [Header("Other:")]
    private Rigidbody2D rb;
    private PlayerMovementV2 playerMovement;
    private Animator animator;
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
        weaponChargeSlider.maxValue = maxWeaponCharge;
        weaponChargeSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponEquipped && weaponCharge == 0)
        {
            weaponChargeSlider.enabled = false;
            RemoveWeapon();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!playerMovement.attacking)
            {
                if (isInWeaponTrigger) // pick up weapon
                {
                    weapon.GetComponent<WeaponScript>().EquipWeapon();
                }
                else // attack
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }

    public void SetWeapon(WeaponType weaponType)
    {
        weaponCharge = maxWeaponCharge;
        weaponChargeSlider.value = weaponCharge;
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
        weaponChargeSlider.enabled = true;
    }

    private void RemoveWeapon()
    {
        weaponEquipped = false;
        weaponCharge = 0;
        attackPower = 4;
        attackCooldown = 0.4f;
        weaponText.text = "Active weapon =  NONE";
        weaponChargeSlider.value = weaponCharge;
        weaponChargeSlider.enabled = false; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 255, 0.5f);
        Vector2 position = raycastOrigin == null ? Vector2.zero : raycastOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        Debug.Log("Detect colliders run");
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(raycastOrigin.position, radius))
        {
            if (collider.CompareTag("Player") && collider.gameObject != gameObject)
            {
                Debug.Log("Player hit");
                HitSoundEffect.Play();
                collider.gameObject.GetComponent<PlayerCombatV2>().DamagePlayer(gameObject, attackPower);
                weaponCharge--;
                weaponChargeSlider.value = weaponCharge;
            }
        }
    }

    public void DamagePlayer(GameObject sender, int damage)
    {
        playerHealth -= damage;
        healthSlider.value = playerHealth;
        if (playerHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        StopAllCoroutines();
        playerMovement.knockbackActive = true;
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(KnockbackReset());
    }

    private IEnumerator KnockbackReset()
    {
        yield return new WaitForSeconds(knockbackDelay);
        rb.velocity = Vector2.zero;
        playerMovement.knockbackActive = false;
    }
}
