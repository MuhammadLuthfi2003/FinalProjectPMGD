using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript Instance;
    public bool isPaused = false;
    public GameObject pausePanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //Shows the Pause Menu
    public void ShowPauseMenu()
    {
        SFXPlayer.Instance.PlayOpenWindowSFX();
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    //Hides the Pause Menu
    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        SFXPlayer.Instance.PlayCloseWindowSFX();
        isPaused = false;
        pausePanel.SetActive(false);
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
    }

    //Restarts Game
    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    //Go Back to Main Menu
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            HidePauseMenu();
          
        }
        else
        {
            ShowPauseMenu();

        }
    }
}
