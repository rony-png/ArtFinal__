using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle player movement
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Activate animation with the "F" button
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("ActivateAnimation");
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
        animator.SetBool("IsGrounded", isGrounded);
    }
}
