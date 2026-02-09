using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform cameraTransform;

    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs; // block, fire

    [Header("Spawn Settings")]
    public float spawnAheadDistance = 18f;
    public float groundY = -3.5f;

    public float minGap = 4f;
    public float maxGap = 8f;

    private float nextSpawnX;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        nextSpawnX = cameraTransform.position.x + spawnAheadDistance;
    }

    void Update()
    {
        int c = (GameManager.I != null) ? GameManager.I.coins : 0;

        float t = Mathf.Clamp01(c / 50f); // 0..1 by 50 coins

        float curMinGap = Mathf.Lerp(maxGap, minGap, t); // gap shrinks
        float curMaxGap = Mathf.Lerp(maxGap + 2f, maxGap, t);

        if (cameraTransform == null) return;

        float camX = cameraTransform.position.x;

        // Keep placing obstacles ahead of camera
        while (nextSpawnX < camX + spawnAheadDistance)
        {
            SpawnOne(nextSpawnX);
            nextSpawnX += Random.Range(curMinGap, curMaxGap);
        }
    }

    void SpawnOne(float x)
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        int idx = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[idx];

        Vector3 pos = new Vector3(x, groundY, 0f);
        Instantiate(prefab, pos, Quaternion.identity);
    }
}