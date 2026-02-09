using UnityEngine;

public class ObstacleKill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        // For now: restart immediately (matches your existing pattern)
        FindObjectOfType<GameOverOnExit>()?.SendMessage("RestartScene", SendMessageOptions.DontRequireReceiver);

        // If you don't want to alter GameOverOnExit yet, use:
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
