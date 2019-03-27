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
    public string destination;

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
                SceneManager.LoadScene(destination);
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
