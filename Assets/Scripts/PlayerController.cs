using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;
    [SerializeField] bool isFacingRight = true;

    [SerializeField] int doubleJump;
    [SerializeField] bool isDoubleJump;

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
        
        if (Input.GetButtonDown("Jump") && isDoubleJump)
        {
            if (IsGrounded() || doubleJump < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump ++;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            doubleJump = 0;
            isDoubleJump = true;

        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            isDoubleJump = false;
        }
    }
}
