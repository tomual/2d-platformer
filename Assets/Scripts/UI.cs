using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private GameObject deathScreen;
    private bool fadeDeathScreen;
    private float deathScreenAlpha;

    void Awake()
    {
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeDeathScreen)
        {
            deathScreenAlpha += 0.02f;
            deathScreen.GetComponent<CanvasGroup>().alpha = deathScreenAlpha;
            if (deathScreenAlpha >= 1)
            {
                fadeDeathScreen = false;
            }
        }
    }
    
    public void ShowDeathScreen()
    {
        fadeDeathScreen = true;
        deathScreen.SetActive(true);
    }

    public void RestartClick()
    {
        SceneManager.LoadScene("World1");
    }
}
