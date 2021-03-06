﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public int health;
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public SpriteRenderer sprite;
    [HideInInspector]
    public Material defaultMaterial;
    [HideInInspector]
    public Material whiteMaterial;
    [HideInInspector]
    public float lastTookDamage;
    [HideInInspector]
    private float spriteAlpha;
    [HideInInspector]
    public float attackStart;
    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public GameObject player;

    public void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
        whiteMaterial = Resources.Load<Material>("Materials/WhiteMaterial");
        health = 3;
        lastTookDamage = 0;
        attackStart = 0;
        spriteAlpha = 1f;
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (IsDead())
        {
            Die();
        }
        if (Time.time - lastTookDamage >= 0.1f)
        {
            sprite.material = defaultMaterial;
        }
    }

    public bool IsPlaying(string name)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(name) && animatorStateInfo.normalizedTime < 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Weapon")
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {

        if (Time.time - lastTookDamage >= 0.1f)
        {
            --health;
            sprite.material = whiteMaterial;
            lastTookDamage = Time.time;
            
            if (IsDead())
            {
                animator.SetTrigger("Die");
            }
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Die()
    {
        float r = GetComponent<SpriteRenderer>().color.r;
        float g = GetComponent<SpriteRenderer>().color.g;
        float b = GetComponent<SpriteRenderer>().color.b;
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, spriteAlpha);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<BoxCollider2D>().enabled = false;
        if (Time.time - lastTookDamage >= 2)
        {
            spriteAlpha -= 0.01f;
            if (spriteAlpha <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public bool AllowedToMove()
    {
        return !IsDead();
    }
}
