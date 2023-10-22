using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;
    [SerializeField] bool isFacingRight = true;

    private bool doubleJump;

     private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radiusGroundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(horizontal < 0 && isFacingRight)
        {
            Quaternion rotateLeft =  Quaternion.Euler(0, 180, 0);
            transform.rotation = rotateLeft;
            isFacingRight = false;
        }
        else if( horizontal > 0 && !isFacingRight) 
        {
            Quaternion rotateRight = Quaternion.Euler(Vector3.zero);
            transform.rotation = rotateRight;
            isFacingRight = true;
        }
    }
    void Jump()
    {
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;

                Debug.Log(doubleJump);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radiusGroundCheck);
    }

}
