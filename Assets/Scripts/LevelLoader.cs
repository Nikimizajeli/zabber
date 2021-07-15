using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      // dyrektywa dodająca przestrzeń nazw od zarządzania scenami

public class LevelLoader : MonoBehaviour
{
    private int currentSceneIndex;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
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
