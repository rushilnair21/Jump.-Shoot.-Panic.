using UnityEngine;

public class GhostMode : MonoBehaviour
{
    [Header("Input")]
    public KeyCode toggleKey = KeyCode.G;

    [Header("Cost")]
    public float coinsPerSecond = 2f;

    [Header("Layers")]
    public string playerLayerName = "Player";
    public string obstacleLayerName = "Obstacle";

    [Header("Visual Feedback")]
    public SpriteRenderer sr;
    public float ghostAlpha = 0.45f;

    private bool isGhost = false;
    private float spendAccumulator = 0f;

    private int playerLayer;
    private int obstacleLayer;

    void Start()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        playerLayer = LayerMask.NameToLayer(playerLayerName);
        obstacleLayer = LayerMask.NameToLayer(obstacleLayerName);

        if (playerLayer == -1)
            Debug.LogError("Player layer not found: " + playerLayerName);

        if (obstacleLayer == -1)
            Debug.LogError("Obstacle layer not found: " + obstacleLayerName);

        SetGhost(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleGhost();
        }

        if (isGhost)
        {
            DrainCoins();
        }
    }

    public void ToggleGhost()
    {
        if (isGhost)
        {
            SetGhost(false);
            return;
        }

        // Only enable if player has coins
        if (GameManager.I == null) return;
        if (GameManager.I.coins <= 0) return;

        SetGhost(true);
    }

    private void SetGhost(bool on)
    {
        isGhost = on;
        spendAccumulator = 0f;

        // Ignore collision between Player and Obstacle while ghost is active
        if (playerLayer != -1 && obstacleLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, on);
        }

        // Visual feedback (semi-transparent)
        if (sr != null)
        {
            Color c = sr.color;
            c.a = on ? ghostAlpha : 1f;
            sr.color = c;
        }
    }

    private void DrainCoins()
    {
        if (GameManager.I == null)
        {
            SetGhost(false);
            return;
        }

        spendAccumulator += Time.deltaTime * coinsPerSecond;

        int toSpend = Mathf.FloorToInt(spendAccumulator);
        if (toSpend <= 0) return;

        if (GameManager.I.coins >= toSpend)
        {
            GameManager.I.coins -= toSpend;
            spendAccumulator -= toSpend;
        }
        else
        {
            // Not enough coins left --> exit ghost mode
            SetGhost(false);
        }
    }

    private void OnDisable()
    {
        // Safety: if script or object gets disabled while ghost is ON,
        // restore collisions so future runs are not "stuck ghost".
        if (playerLayer != -1 && obstacleLayer != -1)
            Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
    }
}