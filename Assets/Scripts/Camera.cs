using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        cameraPosition.x = playerPosition.x;
        transform.position = cameraPosition;
    }
}
