using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementScript : MonoBehaviour
{                                                                                                                                                                                                                                                                                                                      
    public float gravityStrength = 9.81f;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;

    public float normalGravity = 0;
    public float increasedObjectGravity = 3f;

    private Rigidbody2D rb;

    public bool isMoving;

    public float playerHealth = 100;
    public float movement = 0;

    public GameObject bulletPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetGravityForOthers(normalGravity);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }

        movement = Input.GetAxisRaw("Horizontal");

        if (movement == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<Animator>().Play("Run");
            isMoving = true;
}
        else if (movement == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            GetComponent<Animator>().Play("Run");
            isMoving = true;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<Animator>().Play("Idle");
            isMoving = false;
        }

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Set horizontal velocity only
        rb.linearVelocity = new Vector2(movement * currentSpeed, rb.linearVelocity.y);

        // Jumping � grounded check using vertical velocity
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Update other objects' gravity based on movement
        if (isMoving)
        {
            SetGravityForOthers(increasedObjectGravity);
        }
        else
        {
            SetGravityForOthers(normalGravity);
        }

        if (playerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravityStrength);
    }

    void SetGravityForOthers(float gravityValue)
    {
        //Debug.Log(gravityValue);
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("AffectedByGravity");

        if (gravityValue != 0)
        {
            foreach (GameObject obj in allObjects)
            {
                // Avoid changing gravity of player accidentally
                if (obj != gameObject)
                {
                    Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                    if (objRb != null)
                    {
                        objRb.gravityScale = gravityValue;
                    }
                }
            }
        }
        else
        {
            foreach (GameObject obj in allObjects)
            {
                // Avoid changing gravity of player accidentally
                if (obj != gameObject)
                {
                    Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
                    if (objRb != null)
                    {
                        objRb.gravityScale = gravityValue;
                        objRb.linearVelocity = new Vector2(0, 0);
                    }
                }
            }
        }
    }
}