using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public Transform cameraTransform;

    [Header("Prefabs")]
    public GameObject coinPrefab;

    [Header("Spawn Settings")]
    public float spawnAheadDistance = 18f;
    public float minGap = 2.5f;
    public float maxGap = 5.0f;

    [Header("Spawn Area")]
    public float minY = -1.5f;
    public float maxY = 2.5f;

    private float nextSpawnX;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        nextSpawnX = cameraTransform.position.x + spawnAheadDistance;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        float camX = cameraTransform.position.x;

        // Keep coin spawns ahead of camera
        while (nextSpawnX < camX + spawnAheadDistance)
        {
            SpawnCoin(nextSpawnX);
            nextSpawnX += Random.Range(minGap, maxGap);
        }
    }

    void SpawnCoin(float x)
    {
        if (coinPrefab == null) return;

        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(x, y, 0f);
        Instantiate(coinPrefab, pos, Quaternion.identity);
    }
}