using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private float shotCounter;

    void Update()
    {
        if (!unlocked) return;

        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            shotCounter = stats[weaponLevel].cooldown;
            FireAtClosestEnemy();
        }
    }

    void FireAtClosestEnemy()
    {
        EnemyBehavior closestEnemy = FindClosestEnemy();

        if (closestEnemy == null) return;

        Vector3 spawnPosition;

        if (firePoint != null)
        {
            spawnPosition = firePoint.position;
        }
        else
        {
            spawnPosition = transform.position;
        }

        GameObject bullet = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        ProjectileWeaponBullet bulletScript = bullet.GetComponent<ProjectileWeaponBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(closestEnemy.transform, this);
        }
        else
        {
            Debug.LogError("ProjectileWeaponBullet is missing on the projectile prefab.");
        }
    }

    EnemyBehavior FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyBehavior closest = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, enemies[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemies[i].GetComponent<EnemyBehavior>();
            }
        }

        return closest;
    }
}