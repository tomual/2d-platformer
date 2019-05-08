using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private bool disabled = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!disabled)
        {
            Vector3 cameraPosition = transform.position;
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            cameraPosition.x = playerPosition.x;
            transform.position = cameraPosition;
        }
    }

    public void SetCameraStop(bool disabled)
    {
        this.disabled = disabled;
    }
}
