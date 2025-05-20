using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public Boolean isGameActive;
    public int enemykilled;
    public GameObject WinningScreen;
    
    public void Start()
    {
        enemykilled = 0;
        isGameActive = true;
    }

    public void Update()
    {
        if (isGameActive == false)
        {
            Time.timeScale = 0f;
        }
        if (enemykilled == 5)
        {
            Winning();
        }
    }   

    public void Winning()
    {
        WinningScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameActive = true;
    }
}
