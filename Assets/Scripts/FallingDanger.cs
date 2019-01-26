using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDanger : MonoBehaviour {
    
    public GameObject explosion;

    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Instantiate the explosion and destroy the rocket.
        Debug.Log(col.name);
        OnExplode();
        Destroy(gameObject);
    }
}
