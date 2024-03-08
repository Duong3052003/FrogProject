using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool isInGame=true;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject Finish;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject SettingScreen;
    [SerializeField] private GameObject MenuGameArrow;

    private void Awake()
    {
        if (isInGame)
        {
            PauseScreen.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInGame)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (PauseScreen.activeInHierarchy)
                {
                    PauseGame(false);
                }
                else
                {
                    PauseGame(true);
                }

                if (SettingScreen.activeInHierarchy)
                {
                    SettingScreen.SetActive(false);
                }
            }
        }
    }

    public void GameOver()
    {
        if(GameOverScreen != null)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void Win()
    {
        if (Finish != null)
        {
            Finish.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
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

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene($"_Scenes/Level_{level}");
    }

    public void Shop()
    {

    }
    
    public void PauseGame(bool status)
    {
        if (status==true)
        {
            Time.timeScale = 0;
        }
        if (status == false)
        {
            Time.timeScale = 1;
        }

        PauseScreen.SetActive(status);
    }

    public void Setting()
    {
        if (isInGame)
        {
            if (PauseScreen.activeInHierarchy)
            {
                SettingScreen.SetActive(true);
                PauseScreen.SetActive(false);
            }
            else if (SettingScreen.activeInHierarchy)
            {
                PauseScreen.SetActive(true);
                SettingScreen.SetActive(false);
            }
        }
        else
        {
            if (MenuGameArrow.activeInHierarchy)
            {
                SettingScreen.SetActive(true);
                MenuGameArrow.SetActive(false);
            }
            else if (SettingScreen.activeInHierarchy)
            {
                MenuGameArrow.SetActive(true);
                SettingScreen.SetActive(false);
            }
        }
    }

    public void SoundVolume()
    {
        SoundManager.Instance.ChangedSound(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.Instance.ChangedMusic(0.2f);
    }
}
