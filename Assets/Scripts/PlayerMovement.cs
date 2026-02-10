using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    
    // Internal components
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    // public float autoRunSpeed = 2f;

    public GameObject shootEffect;

    void Start()
    {
        // Get the Rigidbody component from the player object
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Horizontal Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        // rb.linearVelocity = new Vector2((moveInput * moveSpeed) + autoRunSpeed, rb.linearVelocity.y);

        // 2. Ground Check (Checks if the player is touching the 'Ground' layer)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // 3. Jumping Logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}