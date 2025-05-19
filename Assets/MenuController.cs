using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;

public class mainMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool IsPaused;
    void Start()
    {
        IsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Debug.Log("Resume");
                pauseMenuUI.SetActive(false);
                Resume();
            }
            else
            {
                Debug.Log("Pause");
                pauseMenuUI.SetActive(true);
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu") ;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1) ;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

