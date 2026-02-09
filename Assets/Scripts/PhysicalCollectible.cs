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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
