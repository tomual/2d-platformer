﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {

    private GameObject fadeScreen;
    private bool startFadeOut = false;
    private bool fadeOutInProgress = false;
    private float fadeScreenAlpha = 0;

    // Use this for initialization
    void Start ()
    {
        fadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");
    }
	
	// Update is called once per frame
	void Update () {
        if (startFadeOut)
        {
            if (!fadeOutInProgress)
            {
                SceneManager.LoadScene("World");
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
        Debug.Log(collision.gameObject.name);
        startFadeOut = true;
        fadeOutInProgress = true;
    }
}