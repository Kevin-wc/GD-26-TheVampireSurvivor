using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text ExperienceText;
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    public GameObject pausePanel;
    [SerializeField] private TMP_Text timerText;

    public LevelUpButton[] levelUpButtons;

    public GameObject shadowButton;
    public GameObject bloodButton;

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
    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = PlayerController.Instance.playerMaxHealth;
        playerHealthSlider.value = PlayerController.Instance.playerCurrentHealth;
        healthText.text = playerHealthSlider.value + " / " + playerHealthSlider.maxValue;
    }
    public void UpdateExperienceSlider()
    {
        playerExperienceSlider.maxValue = PlayerController.Instance.playerLevels[PlayerController.Instance.currentLevel - 1];
        playerExperienceSlider.value = PlayerController.Instance.experience;
        ExperienceText.text = playerExperienceSlider.value + " / " + playerExperienceSlider.maxValue;
    }

    public void UpdateTimer(float timer)
    {
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);
        timerText.text = min + ":" + sec.ToString("00");
    }

    public void LevelUpPanelOpen()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        if (shadowButton != null) shadowButton.SetActive(true);
        if (bloodButton != null) bloodButton.SetActive(true);

        if (LevelUpManager.Instance != null)
        {
            if (LevelUpManager.Instance.shadowWeapon != null &&
                LevelUpManager.Instance.shadowWeapon.isMaxLevel())
            {
                if (shadowButton != null) shadowButton.SetActive(false);
            }

            if (LevelUpManager.Instance.bloodWeapon != null &&
                LevelUpManager.Instance.bloodWeapon.isMaxLevel())
            {
                if (bloodButton != null) bloodButton.SetActive(false);
            }
        }
    }
    public void LevelUpPanelClose()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
