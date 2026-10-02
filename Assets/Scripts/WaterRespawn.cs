using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RespawnFromWater(respawnPoint);
        }
    }
}