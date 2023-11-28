using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public static GameOverScript Instance;

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

    //Shows Game Over Screen
    public void ShowGameOver()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        SFXPlayer.Instance.PlayGameOverSFX();
        gameObject.SetActive(true);
    }

    //Restarts Game
    public void RestartGame()
    {
        GameManager.Instance.ResetScriptableValues();
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
