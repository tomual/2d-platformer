using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private GameObject pauseScreen;
    private GameObject deathScreen;
    private GameObject fadeScreen;
    private GameObject endScreen;

    private bool fadeDeathScreen = false;
    private float deathScreenAlpha = 0;
    private bool fadeInInProgress = true;
    private float fadeScreenAlpha = 1;
    private bool fadeEndScreen = false;
    private float endScreenAlpha = 0;
    private float endStart;

    void Start()
    {
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        pauseScreen.SetActive(false);
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        deathScreen.SetActive(false);
        endScreen = GameObject.FindGameObjectWithTag("EndScreen");
        endScreen.SetActive(false);
        fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
        fadeScreen.GetComponent<CanvasGroup>().alpha = fadeScreenAlpha;
    }

    void Update()
    {
        if (fadeInInProgress)
        {
            fadeScreenAlpha -= 0.02f;
            fadeScreen.GetComponent<CanvasGroup>().alpha = fadeScreenAlpha;
            if (fadeScreenAlpha <= 0)
            {
                fadeInInProgress = false;
            }
        }

        if (Input.GetKeyUp("escape"))
        {
            if(pauseScreen.activeInHierarchy)
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
                pauseScreen.GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
                pauseScreen.GetComponent<CanvasGroup>().alpha = 1;
            }
        }

        if (fadeDeathScreen)
        {
            deathScreenAlpha += 0.02f;
            deathScreen.GetComponent<CanvasGroup>().alpha = deathScreenAlpha;
            if (deathScreenAlpha >= 1)
            {
                fadeDeathScreen = false;
            }
        }

        if (fadeEndScreen)
        {
            if (Time.time - endStart > 2)
            {
                endScreenAlpha += 0.02f;
                endScreen.GetComponent<CanvasGroup>().alpha = endScreenAlpha;
                if (endScreenAlpha >= 1)
                {
                    fadeEndScreen = false;
                }
            }
        }
    }

    public void ShowDeathScreen()
    {
        fadeDeathScreen = true;
        deathScreen.SetActive(true);
    }

    public void ShowEndScreen()
    {
        endStart = Time.time;
        fadeEndScreen = true;
        endScreen.SetActive(true);
    }
}
