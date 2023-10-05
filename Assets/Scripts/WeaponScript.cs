using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private SpawnWeapon weaponManager;
    private WeaponType weaponType;
    // Start is called before the first frame update
    void Start()
    {
        weaponManager = FindObjectOfType<SpawnWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("GameObject " + other.name + " has entered trigger of " + gameObject.name);
        if (Input.GetKeyDown(KeyCode.LeftShift) && other.name == "Player 1" || Input.GetKeyDown(KeyCode.RightControl) && other.name == "Player 2")
        {
            Debug.Log("Weapon triggered");
            Destroy(gameObject.transform.parent.gameObject);
            weaponManager.activeWeaponCount--;
        }
    }

    public enum WeaponType
    {
        Sword,
        Boomerang,
        Axe
    }
}
