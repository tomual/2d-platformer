﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool facingRight = true;
    private Transform groundCheck;
    private bool jump;
    private GameObject weapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        weapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
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

        if (jump)
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(new Vector2(0f, jumpForce));
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
}
