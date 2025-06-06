using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject RayCast;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    public Collider2D playerCollider;

    public float direction = -4;
    private Vector3 rotationConstant = new Vector3(0, 180, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            Debug.Log("Player entered the trigger!");
        }
        else
        {
            transform.eulerAngles += rotationConstant;
            direction = direction * -1;
            Debug.Log("Wall entered the trigger!");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
