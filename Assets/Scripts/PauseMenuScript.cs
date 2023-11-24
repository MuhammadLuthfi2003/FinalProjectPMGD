using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript Instance;

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
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        gameObject.SetActive(true);
    }

    //Hides the Pause Menu
    public void HidePauseMenu()
    {
        gameObject.SetActive(false);
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
    }

    //Restarts Game
    public void RestartGame()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    //Go Back to Main Menu
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
