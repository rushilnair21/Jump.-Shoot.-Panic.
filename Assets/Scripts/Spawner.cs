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
    public float obstacleSpawnY = -2.5f;
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

        while (nextSpawnX < camX + obstacleAheadDistance)
        {
            SpawnOne(nextSpawnX);
            nextSpawnX += Random.Range(minGap, maxGap);
        } 

    }

    void SpawnOne(float x)
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        GameObject selectedPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        if (selectedPrefab != null)
        {
            Instantiate(selectedPrefab, new Vector3(x, obstacleSpawnY, 0f), Quaternion.identity);
        }
    }

}