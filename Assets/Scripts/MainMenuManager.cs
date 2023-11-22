using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Panels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject exitConfirmationPanel;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        exitConfirmationPanel.SetActive(false);
    }

    public void openOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void openCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void openLevelSelect()
    {
        levelSelectPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void Back()
    {
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        exitConfirmationPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void openExitConfirmation()
    {
        mainMenuPanel.SetActive(false);
        exitConfirmationPanel.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("HasPlayed", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }
}
