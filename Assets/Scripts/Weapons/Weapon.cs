using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public bool unlocked;
    public List<WeaponStats> stats;

    public void LevelUp()
    {
        if (!unlocked)
        {
            unlocked = true;
            return;

        }

        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
        }
    }

    public bool isMaxLevel()
    {
        return unlocked && weaponLevel >= stats.Count - 1;
    }
}

[System.Serializable]
public class WeaponStats
{
    public float cooldown;
    public float duration;
    public float damage;
    public float range;
    public float speed;
}
