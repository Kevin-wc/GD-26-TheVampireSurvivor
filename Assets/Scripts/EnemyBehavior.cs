using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Component References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    public bool spriteFacesRight = false;

    [Header("Enemy Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    private Vector3 direction;
    [SerializeField] private GameObject destroyEffect;




    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.gameObject.activeSelf)
        {
            bool isPlayerLookingRight = PlayerController.Instance.transform.position.x > transform.position.x;

            if (isPlayerLookingRight)
            {
                spriteRenderer.flipX = !spriteFacesRight;

            }
            else
            {
                spriteRenderer.flipX = spriteFacesRight;

            }

            direction = (PlayerController.Instance.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }
}
