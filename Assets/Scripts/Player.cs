﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerRigidbody;
    private bool facingRight = true;
    private Transform groundCheck;
    private bool jump;
    private GameObject weapon;
    float lastKnockback;
    int health;
    bool invincible;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        weapon = transform.Find("Weapon").gameObject;
        health = 10;
        lastKnockback = -2;
        invincible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1") && !IsPlaying("attack"))
        {
            animator.SetTrigger("Attack");
        }

        if (IsInvincible())
        {
            Blink();
        }

        weapon.SetActive(IsPlaying("attack"));
    }

    private void FixedUpdate()
    {
        float maxSpeed = 2;
        float moveForce = 300;
        float jumpForce = 1000f;

        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h));
        animator.SetBool("Grounded", IsGrounded());

        if (h * playerRigidbody.velocity.x < maxSpeed)
        {
            playerRigidbody.AddForce(Vector2.right * h * moveForce);
        }

        if (h == 0)
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        }

        if (Mathf.Abs(playerRigidbody.velocity.x) > maxSpeed )
        {
            playerRigidbody.velocity = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * maxSpeed, playerRigidbody.velocity.y);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }

        if (h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            animator.SetTrigger("Jump");
            playerRigidbody.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    bool IsPlaying(string name)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(name) && animatorStateInfo.normalizedTime < 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyWeapon" && !IsInvincible())
        {
            TakeDamage(collision.gameObject, 1);
        }
    }

    void TakeDamage(GameObject source, int damage)
    {
        Debug.Log("TakeDamage");

        float thrustY = 350;
        float thrustX = 550;
        float enemyXPosition = source.transform.position.x;
        lastKnockback = Time.time;
        invincible = true;

        if (IsGrounded())
        {
            if (transform.position.x < enemyXPosition)
            {
                playerRigidbody.AddForce(new Vector2(-thrustX, thrustY));
            }
            else
            {
                playerRigidbody.AddForce(new Vector2(thrustX, thrustY));
            }
        }
        health = health - damage;
    }

    bool IsInvincible()
    {
        return invincible;
    }

    private void Blink()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        string time = Time.time.ToString("0.0");
        char milliseconds = time.ToCharArray()[time.Length - 1];
        bool beTransparent = int.Parse(milliseconds.ToString()) % 3 == 0;

        if (beTransparent)
        {
            spriteRenderer.color = new Color(255, 255, 255, 0.5f);

        }
        else
        {
            spriteRenderer.color = new Color(255, 255, 255, 1);
        }

        if (Time.time - lastKnockback > 1)
        {
            spriteRenderer.color = new Color(255, 255, 255, 1);
            invincible = false;
        }

    }
}
