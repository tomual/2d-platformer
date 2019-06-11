using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
{
    [HideInInspector]
    public float h = 0.5f;
    [HideInInspector]
    public float flipTimer = 0;
    [HideInInspector]
    public float flipTimeout = 1;
    [HideInInspector]
    public bool facingRight = true;

    new void Awake()
    {
        base.Awake();
        health = 2;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (AllowedToMove())
        {
            UpdateWeapon();
            UpdateDirection();
            if (!IsPlaying("attack"))
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < 0.8)
                {
                    Attack();
                }
                else
                {
                    Move();
                }
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void Move()
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
        if (player.transform.position.x > transform.position.x)
        {
            if (Time.time - flipTimer > flipTimeout)
            {
                h = 0.5f;
                flipTimer = Time.time;
                if (!facingRight)
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
                if (facingRight)
                {
                    Flip();
                }
            }
        } else
        {
            flipTimer = Time.time;
        }

    }

    public void UpdateWeapon()
    {
        if (IsPlaying("attack") && Time.time - attackStart > 0.5)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "FollowingEnemy" || collision.gameObject.name == "MiniBoss")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
        attackStart = Time.time;
    }
}
