using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public int bulletSpeed = 10;

    private PlayerMovementScript playerMovement;
    private float directionp;

    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.movement == 0)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            rb.linearVelocity = bulletSpeed * direction;
        }
    }
}
