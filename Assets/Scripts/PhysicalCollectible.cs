using UnityEngine;

public class PhysicalCollectible : MonoBehaviour
{
    public int coinValue = 1;

    private static int internalCoinCounter = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Trigger hit by: " + other.name);

        if (!other.CompareTag("Player")) return;
        if (GameManager.I == null) return;

        GameManager.I.AddCoin(coinValue);

        internalCoinCounter++;
        if (internalCoinCounter >=2)
        {
            GameManager.I.ammo +=1;
            internalCoinCounter = 0;
            UnityEngine.Debug.Log("Ammo Refilled");
        }
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
