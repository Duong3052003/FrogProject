using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    public void GameOver()
    {
        if(gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying=false;
    }
    public void SelectLevel()
    {

    }
    public void Shop()
    {

    }
}
