using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform cameraTransform;

    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs; // block, fire
    public GameObject floorPrefab;

    [Header("Spawn Settings")]
    public float spawnAheadDistance = 25f;
    public float groundY = -2.5f;
    public float floorY = -3.4f;
    public float floorWidth = 25f;

    public float minGap = 4f;
    public float maxGap = 8f;

    private float nextSpawnX;
    private float nextFloorX;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        nextSpawnX = cameraTransform.position.x + spawnAheadDistance;
        nextFloorX = cameraTransform.position.x - 5f;
    }

    void Update()
    {
        
        if (cameraTransform == null) return;
        float camX = cameraTransform.position.x;

        while(nextFloorX < camX + spawnAheadDistance)
        {
            if (floorPrefab != null)
            {
                Instantiate(floorPrefab, new Vector3(nextFloorX, floorY, 0f), Quaternion.identity);
            }
            nextFloorX += floorWidth;
        }

        int c = (GameManager.I != null) ? GameManager.I.coins : 0;
        float t = Mathf.Clamp01(c / 50f); 

        float curMinGap = Mathf.Lerp(maxGap, minGap, t); 
        float curMaxGap = Mathf.Lerp(maxGap + 2f, maxGap, t);

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