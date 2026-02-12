using TMPro;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text speedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.I == null) return;

        coinsText.text = "Coins: " + GameManager.I.coins;
        speedText.text = "Speed: " + GameManager.I.ScrollSpeed.ToString("0.0");
    }
}
