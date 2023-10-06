using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpawnWeapon weaponManager;
    private WeaponScriptType weaponScriptType;
    private bool playerInTrigger;
    private GameObject playerObject;
    private PlayerCombat playerCombat;

    private enum WeaponScriptType
    {
        Katana,
        Boomerang,
        Axe,
        Scythe
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = FindObjectOfType<SpawnWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerObject.name == "Player 1" || Input.GetKeyDown(KeyCode.RightControl) && playerObject.name == "Player 2")
            {
                Debug.Log("Weapon triggered");
                Destroy(gameObject.transform.parent.gameObject);
                weaponManager.activeWeaponCount--;
                switch (weaponScriptType)
                {
                    case WeaponScriptType.Katana:
                        {
                            playerCombat.SetWeapon(PlayerCombat.WeaponType.Katana);
                            break;
                        }
                    case WeaponScriptType.Boomerang:
                        {
                            playerCombat.SetWeapon(PlayerCombat.WeaponType.Boomerang);
                            break;
                        }
                    case WeaponScriptType.Axe:
                        {
                            playerCombat.SetWeapon(PlayerCombat.WeaponType.Axe);
                            break;
                        }
                    case WeaponScriptType.Scythe:
                        {
                            playerCombat.SetWeapon(PlayerCombat.WeaponType.Scythe);
                            break;
                        }
                    default:
                        {
                            // PANIC
                            break;
                        }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            playerObject = other.gameObject;
            playerCombat = other.GetComponent<PlayerCombat>();
        }
        Debug.Log("GameObject " + other.name + " has entered trigger of " + gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInTrigger = false;
        playerObject = null;
        playerCombat = null;
    }

}
