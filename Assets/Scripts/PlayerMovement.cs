using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Settings & Configuration
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public GameObject shootEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Horizontal Movement Logic
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        //Ground Detection 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //Stomping Logic
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -25f);
            UnityEngine.Debug.Log("Stomp activated!");
        }

        //Laser Sight Cleanup
        LineRenderer lr = GetComponent<LineRenderer>();
        if (lr != null && lr.positionCount > 0) 
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position); 
        }
    }
}