using UnityEngine;
using UnityEngine.SceneManagement;      // dyrektywa dodająca przestrzeń nazw od zarządzania scenami

public class LevelLoader : MonoBehaviour
{
    private int currentSceneIndex;

    public void LoadStartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        FindObjectOfType<GameController>().ResetGameSession();
    }

    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
