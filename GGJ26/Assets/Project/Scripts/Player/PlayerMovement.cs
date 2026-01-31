using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed = 5f;
    private float jumpForce = 7f;
    private float moveX;
    public static int facingDirection;
    public bool isSwinging;
    public bool doubleJump;
    
    

    public bool canClimb;
    private static Rigidbody2D rb;
    public bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrounded();
        handleRotation();
        if (isSwinging)
        {
            return;}
        canClimb = Physics2D.Raycast(transform.position, Vector2.down, 0f, LayerMask.GetMask("Climbable"));
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.linearVelocityY = moveSpeed;
                
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.linearVelocityY = -moveSpeed;
            }
        }

        // Movement
        moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Jump
        if ((isGrounded|| doubleJump )&& Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            doubleJump = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.7f);
            }
        }
        
    }

    private void CheckGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        } else {isGrounded = false;}
    }

    private void handleRotation()
    {
        if (moveX > 0)
        {
            facingDirection = 1;
        }

        if (moveX < 0)
        {
            facingDirection = -1;
        }
    }

    public static Rigidbody2D sendRB()
    {
        return rb;
    }
}
