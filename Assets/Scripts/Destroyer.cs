﻿using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
    public bool destroyOnAwake;         // Whether or not this gameobject should destroyed after a delay, on Awake.
    public float awakeDestroyDelay;     // The delay for destroying it on Awake.
    public bool findChild = false;              // Find a child game object and delete it
    public string namedChild;           // Name the child object in Inspector


    void Awake()
    {
        Debug.Log("1");
        // If the gameobject should be destroyed on awake,
        if (destroyOnAwake)
        {
            Debug.Log("2");
            if (findChild)
            {
                Debug.Log("3");
                Destroy(transform.Find(namedChild).gameObject);
            }
            else
            {
                Debug.Log("4");
                // ... destroy the gameobject after the delay.
                Destroy(gameObject, awakeDestroyDelay);
            }

        }

    }

    void DestroyChildGameObject()
    {
        Debug.Log("DestroyChildGameObject");
        // Destroy this child gameobject, this can be called from an Animation Event.
        if (transform.Find(namedChild).gameObject != null)
            Destroy(transform.Find(namedChild).gameObject);
    }

    void DisableChildGameObject()
    {
        Debug.Log("DisableChildGameObject");
        // Destroy this child gameobject, this can be called from an Animation Event.
        if (transform.Find(namedChild).gameObject.activeSelf == true)
            transform.Find(namedChild).gameObject.SetActive(false);
    }

    void DestroyGameObject()
    {
        Debug.Log("DestroyGameObject");
        // Destroy this gameobject, this can be called from an Animation Event.
        Destroy(gameObject);
    }
}
