using UnityEngine;

public class PhysicalCollectible : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger hit by: " + other.name);

        if (!other.CompareTag("Player")) return;
        if (GameManager.I == null) return;

        GameManager.I.AddCoin(coinValue);

        Destroy(gameObject);
    }
}