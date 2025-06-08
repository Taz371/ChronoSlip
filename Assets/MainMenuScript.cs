using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayTutorial()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
