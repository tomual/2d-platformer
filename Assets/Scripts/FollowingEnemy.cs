using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
{
    float h = 0.5f;
    float flipTimer = 0;
    float flipTimeout = 1;
    bool facingRight = true;
    GameObject weapon;
    GameObject player;

    new void Awake()
    {
        base.Awake();
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        health = 2;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (AllowedToMove())
        {
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
        weapon.SetActive(IsPlaying("attack"));
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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        enemyRigidbody.velocity = new Vector2(0, enemyRigidbody.velocity.y);
    }
}
