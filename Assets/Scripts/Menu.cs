using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartClick()
    {
        SceneManager.LoadScene("World1");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
