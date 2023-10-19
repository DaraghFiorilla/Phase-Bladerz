using System.Linq;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [Tooltip("The time between weapon spawns in seconds")]public float timeBetweenSpawns;
    [Tooltip("The maximum amount of weapons that can be on the screen at any time")]public int maxWeapons;
    private float timer;
    [SerializeField] private GameObject weaponPrefab;
    [Tooltip("The number of currently spawned weapons")]public int activeWeaponCount;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (activeWeaponCount < 0)
            {
                activeWeaponCount = 0;
            }
            if (activeWeaponCount < maxWeapons)
            {
                SpawnNewWeapon();
            }
            timer = timeBetweenSpawns;
        }
    }

    public Vector2 spawnAreaCenter;
    public Vector2 spawnAreaSize;

    private void OnDrawGizmos() // This draws the area where weapons can spawn in the scene window
    {
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawCube(spawnAreaCenter, spawnAreaSize);
    }

    private void SpawnNewWeapon()
    {
        Vector2 pos = spawnAreaCenter + new Vector2(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2));
        Instantiate(weaponPrefab, pos, Quaternion.identity);
        activeWeaponCount++;
    }
}
