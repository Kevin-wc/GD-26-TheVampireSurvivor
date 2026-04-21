using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    public int experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> playerLevels;

    public Weapon activeWeapon;


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
        UIController.Instance.UpdateExperienceSlider();

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 5));
        }
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

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;
        UIController.Instance.UpdateExperienceSlider();
        if (experience >= playerLevels[currentLevel - 1])
        {
            LevelUp();
        }
    }

    public void UpgradeMoveSpeed()
    {
        speed += 0.5f;
    }

    public void UpgradeHealth()
    {
        playerMaxHealth += 5;
        playerCurrentHealth = playerMaxHealth;

        if (playerCurrentHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        UIController.Instance.UpdateHealthSlider();
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel - 1];
        currentLevel++;
        UIController.Instance.UpdateExperienceSlider();
        UIController.Instance.LevelUpPanelOpen();
    }
}
