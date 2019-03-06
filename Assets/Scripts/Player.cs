using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody;
    bool facingRight = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float maxSpeed = 2;
        float moveForce = 300;

        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));

        if (h * rigidbody.velocity.x < maxSpeed)
        {
            rigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if (h == 0)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }

        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed )
        {
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }

        if (h < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
