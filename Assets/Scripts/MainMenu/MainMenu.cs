using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject startPanel;
    public GameObject mainPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
        startPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OpenStartMenu()
    {
        mainPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void CloseStartMenu()
    {
        startPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ExitGame()
    {
        print("Exiting game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
