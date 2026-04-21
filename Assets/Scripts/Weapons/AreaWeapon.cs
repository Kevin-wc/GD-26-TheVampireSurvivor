using UnityEngine;
using Unity.VisualScripting;

public class AreaWeapon : Weapon
{
    [SerializeField] private GameObject prefab;
    private float spawnCounter;



    // Update is called once per frame
    void Update()
    {
        if (!unlocked) return;

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;
            Vector3 spawnPos = transform.position + new Vector3(0f, -0.57285f, 0f);
            Instantiate(prefab, spawnPos, transform.rotation, transform);
        }
    }

}
