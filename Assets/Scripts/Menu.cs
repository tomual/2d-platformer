using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartClick()
    {
        SceneManager.LoadScene("World");
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
