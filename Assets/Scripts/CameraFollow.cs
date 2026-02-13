using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float scrollSpeed = 2f;
    void Update()
    {
        if (GameManager.I == null || GameManager.I.isGameOver) return;

        float s = GameManager.I.ScrollSpeed;
        transform.Translate(Vector3.right * s * Time.deltaTime);
    }
}
