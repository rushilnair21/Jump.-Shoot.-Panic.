using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Collectibles")]
    public int coins = 0;

    [Header("Ammo")]
    public int maxAmmo = 8;
    public int ammo = 8;
    public int coinsPerRefill = 5;
    public int ammoRefillAmount = 4;
    private int coinsSinceRefill = 0;

    [Header("Scroll Speed")]
    public float baseScrollSpeed = 2f;
    public float speedIncreasePer10Coins = 0.5f;

    public float ScrollSpeed
    {
        get
        {
            int tens = coins / 10;
            return baseScrollSpeed + tens * speedIncreasePer10Coins;
        }
    }

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        coinsSinceRefill += amount;

        while (coinsSinceRefill >= coinsPerRefill)
        {
            coinsSinceRefill -= coinsPerRefill;
            ammo = Mathf.Clamp(ammo + ammoRefillAmount, 0, maxAmmo);
        }
    }

    public bool TrySpendAmmo(int amount)
    {
        if (ammo < amount) return false;
        ammo -= amount;
        return true;
    }
}