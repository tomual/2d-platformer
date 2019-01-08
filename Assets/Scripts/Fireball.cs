﻿using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public GameObject explosion;        // Prefab of explosion effect.


    void Start()
    {
        // Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        Destroy(gameObject, 2);
    }


    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        // Instantiate the explosion and destroy the rocket.
        OnExplode();
        Destroy(gameObject);
    }
}
