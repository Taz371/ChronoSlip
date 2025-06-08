using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private PlayerMovementScript playerMovement;
    public Image healthBar;

    public Canvas canvas;
    public Camera camera;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = camera.transform.position;
        pos.z = -10;
        camera.transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        playerMovement.playerHealth -= damage;
        healthBar.fillAmount = playerMovement.playerHealth / 100f;
    }

    public void Heal(float healingAmount)
    {
        if(playerMovement.playerHealth <= 90)
        {
            playerMovement.playerHealth += healingAmount;
            playerMovement.playerHealth = Mathf.Clamp(playerMovement.playerHealth, 0, 100);

            healthBar.fillAmount = playerMovement.playerHealth / 100f;
        }
        else
        {
            Debug.Log("Nothing to Heal");
        }
    }
}
