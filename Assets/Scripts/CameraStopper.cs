using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour {

    private CameraController cameraController;

	void Start () {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraController.setOnEdge(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cameraController.setOnEdge(false);
    }
}
