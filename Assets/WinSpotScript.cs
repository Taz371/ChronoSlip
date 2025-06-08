using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSpotScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    private int buildIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Winned!");
            gameManager.Heal(10f);
            SceneManager.LoadSceneAsync(buildIndex + 1);
        }
    }
}
