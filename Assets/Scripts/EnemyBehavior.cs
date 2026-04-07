using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Component References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    [Header("Enemy Settings")]
    [SerializeField] private float speed;
    private Vector3 direction;



    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.transform.position.x >
        transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
