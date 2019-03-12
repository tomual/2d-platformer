using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
{
    float h = 0.5f;
    float flipTimer = 0;
    float flipTimeout = 1;
    bool facingRight = true;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        animator.SetBool("Moving", true);
        float moveForce = 200f;
        float maxSpeed = 0.5f;

        if (h * enemyRigidbody.velocity.x < maxSpeed)
        {
            enemyRigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if (enemyRigidbody.velocity.x > maxSpeed)
        {
            enemyRigidbody.velocity = new Vector2(Mathf.Sign(enemyRigidbody.velocity.x) * maxSpeed, enemyRigidbody.velocity.y);
        }
        
    }

    void UpdateDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x > transform.position.x)
        {
            if (Time.time - flipTimer > flipTimeout)
            {
                h = 0.5f;
                flipTimer = Time.time;
                if (facingRight)
                {
                    Flip();
                }
            }
        } else if (player.transform.position.x < transform.position.x)
        {
            if (Time.time - flipTimer > flipTimeout)
            {
                h = -0.5f;
                flipTimer = Time.time;
                if (!facingRight)
                {
                    Flip();
                }
            }
        } else
        {
            flipTimer = Time.time;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
