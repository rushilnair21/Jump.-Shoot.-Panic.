using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform cameraTransform;

    [Header("References")]
    public Transform roadTop;

    [Header("Prefabs")]
    public GameObject[] obstaclePrefabs; // block, fire
    public GameObject floorPrefab;

    [Header("Spawn Settings")]
    public float obstacleAheadDistance = 25f; 
    public float floorAheadDistance = 100f;
    public float groundY = -3.4f;
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

        nextSpawnX = cameraTransform.position.x + obstacleAheadDistance;
        nextFloorX = cameraTransform.position.x - 20f;
    }

    void Update()
    {
        
        if (cameraTransform == null) return;
        float camX = cameraTransform.position.x;

        while(nextFloorX < camX + floorAheadDistance)
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

        while (nextSpawnX < camX + obstacleAheadDistance)
        {
            SpawnOne(nextSpawnX);
            nextSpawnX += Random.Range(curMinGap, curMaxGap);
        } 

    }

    void SpawnOne(float x)
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        if (prefab == null) return;

        float y = GetGroundedY(prefab);
        Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
    }

    float GetGroundedY(GameObject prefab)
    {
        // If roadTop is assigned, place obstacle so bottom touches road top
        if (roadTop != null)
        {
            float roadTopY = roadTop.position.y;

            // Figure out half-height using collider first (best), fallback to sprite
            float halfH = 0.5f;

            var col = prefab.GetComponent<Collider2D>();
            if (col != null) halfH = col.bounds.extents.y;

            var sr = prefab.GetComponent<SpriteRenderer>();
            if (sr != null) halfH = Mathf.Max(halfH, sr.bounds.extents.y);

            return roadTopY + halfH;
        }

        // fallback if roadTop not set
        return groundY;
    }
}