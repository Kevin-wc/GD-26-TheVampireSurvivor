using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Player Component References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject destroyEffect;

    [Header("Player Settings")]
    [SerializeField] float speed;
    public float playerMaxHealth;
    public float playerCurrentHealth;

    private Vector2 moveInput;

    private bool isImmune;
    [SerializeField] private float immunnityDuration;
    [SerializeField] private float immunityTimer;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerCurrentHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * speed;

        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
        if (moveInput == Vector2.zero)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        }
        else
        {
            isImmune = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunnityDuration;
            playerCurrentHealth -= damage;
            UIController.Instance.UpdateHealthSlider();
            if (playerCurrentHealth <= 0)
            {
                gameObject.SetActive(false);
                Instantiate(destroyEffect, transform.position, transform.rotation);
                GameManager.Instance.GameOver();
            }
        }
    }
}
