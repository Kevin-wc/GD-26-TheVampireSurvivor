using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    public static LevelUpManager Instance;

    public Weapon shadowWeapon;
    public Weapon bloodWeapon;

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

    public void UpgradeMoveSpeed()
    {
        PlayerController.Instance.UpgradeMoveSpeed();
        UIController.Instance.LevelUpPanelClose();
    }

    public void UpgradeHealth()
    {
        PlayerController.Instance.UpgradeHealth();
        UIController.Instance.LevelUpPanelClose();
    }

    public void UpgradeShadowAttack()
    {
        shadowWeapon.LevelUp();
        UIController.Instance.LevelUpPanelClose();
    }
    public void UpgradeBloodAttack()
    {
        bloodWeapon.LevelUp();
        UIController.Instance.LevelUpPanelClose();
    }


}