using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDanger : MonoBehaviour
{

    private float spriteAlpha;
    private bool disappear;

    public void Awake()
    {
        disappear = false;
        spriteAlpha = 1f;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // Instantiate the explosion and destroy the rocket.
        Debug.Log(col.name);
        disappear = true;
    }

    private void LateUpdate()
    {
        if (disappear)
        {
            Die();
        }
    }

    public void Die()
    {
        float r = GetComponent<SpriteRenderer>().color.r;
        float g = GetComponent<SpriteRenderer>().color.g;
        float b = GetComponent<SpriteRenderer>().color.b;
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, spriteAlpha);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CircleCollider2D>().enabled = false;
        spriteAlpha -= 0.05f;
        if (spriteAlpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
