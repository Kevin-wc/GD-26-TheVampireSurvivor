using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Camera position based on player and offset
            transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, -10f);
        }
    }
}