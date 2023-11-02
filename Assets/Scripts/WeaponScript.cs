using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpawnWeapon weaponManager;
    [SerializeField] private WeaponScriptType weaponScriptType;
    private bool playerInTrigger;
    private GameObject playerObject;
    private PlayerCombatV2 playerCombat;
    [Tooltip("Add sprites in order: Katana, Boomerang, Axe, Scythe")]public Sprite[] spriteList = new Sprite[4];
    private SpriteRenderer spriteRenderer;

    private enum WeaponScriptType
    {
        Katana,
        Boomerang,
        Sword,
        Scythe
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = FindObjectOfType<SpawnWeapon>();
        spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        GetRandomWeaponType();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerObject.name == "Player 1" && !playerCombat.weaponEquipped|| Input.GetKeyDown(KeyCode.RightControl) && playerObject.name == "Player 2" && !playerCombat.weaponEquipped)
            { // PROBLEM AREA ^ USING OLD INPUT SYSTEM
               
            }
        }
    }*/

    public void EquipWeapon()
    {
        //Destroy(gameObject.transform.parent.gameObject);
        weaponManager.activeWeaponCount--;
        switch (weaponScriptType)
        {
            case WeaponScriptType.Katana:
                {
                    playerCombat.SetWeapon(PlayerCombatV2.WeaponType.Katana);
                    break;
                }
            case WeaponScriptType.Boomerang:
                {
                    playerCombat.SetWeapon(PlayerCombatV2.WeaponType.Boomerang);
                    break;
                }
            case WeaponScriptType.Sword:
                {
                    playerCombat.SetWeapon(PlayerCombatV2.WeaponType.Sword);
                    break;
                }
            case WeaponScriptType.Scythe:
                {
                    playerCombat.SetWeapon(PlayerCombatV2.WeaponType.Scythe);
                    break;
                }
            default:
                {
                    // PANIC
                    break;
                }

        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void GetRandomWeaponType()
    {
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            case 1:
                {
                    weaponScriptType = WeaponScriptType.Katana;
                    spriteRenderer.sprite = spriteList[0];
                    break;
                }
            case 2:
                {
                    weaponScriptType = WeaponScriptType.Boomerang;
                    spriteRenderer.sprite = spriteList[1];
                    break;
                }
            case 3:
                {
                    weaponScriptType = WeaponScriptType.Sword;
                    spriteRenderer.sprite = spriteList[2];
                    break;
                }
            case 4:
                {
                    weaponScriptType = WeaponScriptType.Scythe;
                    spriteRenderer.sprite = spriteList[3];
                    break;
                }
            default:
                {
                    // PANIC
                    Debug.Log(this.name + " script is returning default in randomInt switch");
                    break;
                }
        }
        //weaponScriptType
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerObject = other.gameObject;
            playerCombat = other.GetComponent<PlayerCombatV2>();
            playerInTrigger = true;
            playerCombat.isInWeaponTrigger = true;
            playerCombat.weapon = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerCombat = other.gameObject.GetComponent<PlayerCombatV2>();
        playerCombat.isInWeaponTrigger = false;
        playerCombat.weapon = null;
        playerInTrigger = false;
        playerObject = null;
        playerCombat = null;
    }
}
