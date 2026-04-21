using UnityEngine;

public class ProjectileWeaponBullet : MonoBehaviour
{
    private Transform target;
    private ProjectileWeapon weapon;

    public void SetTarget(Transform newTarget, ProjectileWeapon newWeapon)
    {
        target = newTarget;
        weapon = newWeapon;

        Destroy(gameObject, weapon.stats[weapon.weaponLevel].duration);
    }

    void Update()
    {
        if (weapon == null)
        {
            Destroy(gameObject);
            return;
        }

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            weapon.stats[weapon.weaponLevel].speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = collision.GetComponent<EnemyBehavior>();

            if (enemy != null)
            {
                enemy.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            }

            Destroy(gameObject);
        }
    }
}