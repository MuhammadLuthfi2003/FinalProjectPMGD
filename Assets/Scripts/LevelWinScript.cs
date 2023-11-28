using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinScript : MonoBehaviour
{
    public static LevelWinScript Instance;

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

    //Shows the Level Win screen
    public void ShowLevelWin()
    {
        SFXPlayer.Instance.PlayWinSFX();
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        gameObject.SetActive(true);
    }

    public void CalculateScore()
    {
        GameManager.Instance.score.value += GameManager.Instance.player.GetComponent<Health>().health * 10;
        GameManager.Instance.score.value += GameManager.Instance.shield.value * 30;
        GameManager.Instance.score.value += 50;
    }

    //Go back to Main Menu
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Restarts Game
    public void RestartGame()
    {
        //GameManager.Instance.kayubakar.value = GameManager.Instance.kayubakar.defaultValue;
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    //Go to Next Level
    public void ToNextLevel()
    {
        //Add code for next level transition
    }
}
