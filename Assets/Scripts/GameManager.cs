using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Collectibles")]
    public int coins = 0;

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
    }

    public void AddCoin(int amount)
    {
        coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount)
            return false;

        coins -= amount;
        return true;
    }
}