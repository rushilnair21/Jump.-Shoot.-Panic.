using UnityEngine;

public class DestroyBehindCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float destroyBehindDistance = 25f;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        if (transform.position.x < cameraTransform.position.x - destroyBehindDistance)
        {
            Destroy(gameObject);
        }
    }
}