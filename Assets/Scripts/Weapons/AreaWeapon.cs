using UnityEngine;
using Unity.VisualScripting;

public class AreaWeapon : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private float spawnCounter;

    public float cooldown = 5f;
    public float duration = 3f;
    public float damage = 1f;
    public float range = 0.7f;

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = cooldown;
            Vector3 spawnPos = transform.position + new Vector3(0f, -0.57285f, 0f);
            Instantiate(prefab, spawnPos, transform.rotation, transform);
        }
    }
}
