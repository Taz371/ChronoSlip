using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject RayCast;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

    public float direction = -4;
    private Vector3 rotationConstant = new Vector3(0, 180, 0);

    private PlayerMovementScript playerMovement;
    public GameManagerScript gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0, 0, 0);

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && playerMovement.isMoving)
        {
            rb.linearVelocity = new Vector2(direction, rb.linearVelocity.y);
        }
        else if (playerMovement.isMoving && !isGrounded)
        {
            rb.linearVelocity = new Vector2(direction, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.TakeDamage(10f);
        }
        else if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.eulerAngles += rotationConstant;
            direction = direction * -1;
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
