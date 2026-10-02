using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private float nextDamageTime;

    public void DealDamage()
    {
        if (Time.time < nextDamageTime) return;

        nextDamageTime = Time.time + 2f;
        GameManager.Instance.TakeDamage();
    }
}