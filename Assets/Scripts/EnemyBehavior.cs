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
    [SerializeField] private int experienceToGive;
    [SerializeField] private float pushTime;
    private Vector3 direction;
    [SerializeField] private GameObject destroyEffect;
    private float pushCounter;




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

            if (pushCounter > 0)
            {
                pushCounter -= Time.deltaTime;
                if (speed > 0)
                {
                    speed = -speed;
                }
                if (pushCounter <= 0)
                {
                    speed = Mathf.Abs(speed);
                }
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
        DamageNumberController.Instance.CreateNumber(1, transform.position);
        pushCounter = pushTime;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerController.Instance.GetExperience(experienceToGive);
        }
    }
}
