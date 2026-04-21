using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{

    private Weapon assignedWeapon;

    public void ActivateButton(Weapon weapon)
    {
        assignedWeapon = weapon;
    }
    public void SelectUpgrade()
    {
        UIController.Instance.LevelUpPanelClose();
    }
}
