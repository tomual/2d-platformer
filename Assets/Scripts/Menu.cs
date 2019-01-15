using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    private GameObject pauseScreen;

    void Awake()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
    }

    public void StartClick()
    {
        SceneManager.LoadScene("World2");
    }

    public void QuitClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    public void ResumeClick()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
