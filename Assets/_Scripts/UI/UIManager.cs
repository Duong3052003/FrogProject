using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool isInGame=true;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject Finish;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject SettingScreen;
    [SerializeField] private GameObject MenuGameScreen;
    [SerializeField] private GameObject MenuGameArrow;
    [SerializeField] private GameObject SelectLevelScreen;
    [SerializeField] private TextMeshProUGUI GodModeText;
    [SerializeField] private Animator transitionAnimator;
    private SaveManager saveManager;

    public static UIManager instance { get; private set; }
    public static bool GodMode=false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("1 UI Manager thoi");
        }

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
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (SelectLevelScreen.activeInHierarchy)
                {
                    SelectLevelScreen.SetActive(false);
                    MenuGameScreen.SetActive(true);
                }
                if (SettingScreen.activeInHierarchy)
                {
                    SettingScreen.SetActive(false);
                    MenuGameArrow.SetActive(true);
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
            SaveManager.instance.SaveGame();
        }
    }

    public void Restart()
    {
        StartCoroutine(LoadRestart());
    }
    private IEnumerator LoadRestart()
    {
        Time.timeScale = 1;
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        transitionAnimator.SetTrigger("Start");
    }


    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    private IEnumerator LoadLevel()
    {
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        else
        {
            MainMenu();
        }
        transitionAnimator.SetTrigger("Start");
    }

    public void MainMenu()
    {
        StartCoroutine(LoadMenu());
    }
    private IEnumerator LoadMenu()
    {
        Time.timeScale = 1;
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
        transitionAnimator.SetTrigger("Start");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SelectLevels()
    {
        if (MenuGameScreen.activeInHierarchy)
        {
            SelectLevelScreen.SetActive(true);
            MenuGameScreen.SetActive(false);
        }
        else if (SettingScreen.activeInHierarchy)
        {
            MenuGameScreen.SetActive(true);
            SelectLevelScreen.SetActive(false);
        }
    }

    public void SelectLevel(int level)
    {
        SceneManager.LoadScene($"_Scenes/Level/Level_{level}");
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
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

    public void ActiveGodMode()
    {
        GodMode = !GodMode;
        if (GodMode)
        {
            GodModeText.color = new Color32(40, 68, 217, 255);
        }
        else
        {
            GodModeText.color = Color.white;
        }
    }
}
