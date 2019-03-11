using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
{
    float h = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {

        UpdateDirection();
    }

    private void FixedUpdate()
    {
        float moveForce = 200f;
        float maxSpeed = 0.5f;

        if (h * rigidbody.velocity.x < maxSpeed)
        {
            rigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if (rigidbody.velocity.x > maxSpeed)
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }
        
    }

    void UpdateDirection()
    {
        Debug.Log(h);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x > transform.position.x)
        {
            h = 0.5f;
        } else
        {
            h = -0.5f;
        }
    }
}
