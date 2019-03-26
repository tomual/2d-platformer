using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    bool startFadeOut;
    float fadeScreenAlpha;
    bool fadeOutInProgress;
    GameObject fadeScreen;

    private void Start()
    {
        startFadeOut = false;
        fadeOutInProgress = false;
        fadeScreenAlpha = 0;
        fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
    }

    void Update()
    {
        if (startFadeOut)
        {
            Debug.Log(fadeScreenAlpha);
            if (!fadeOutInProgress)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    case "World1":
                        SceneManager.LoadScene("World2");
                        break;
                    case "World2":
                        SceneManager.LoadScene("World3");
                        break;
                    case "World3":
                        SceneManager.LoadScene("World4");
                        break;
                    default:
                        SceneManager.LoadScene("World1");
                        break;
                }
            }
            else
            {
                fadeScreenAlpha += 0.02f;
                fadeScreen.GetComponent<CanvasGroup>().alpha = fadeScreenAlpha;
                if (fadeScreenAlpha >= 1)
                {
                    fadeOutInProgress = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            startFadeOut = true;
            fadeOutInProgress = true;
        }
    }
}
