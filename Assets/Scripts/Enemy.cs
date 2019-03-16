using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    public Rigidbody2D enemyRigidbody;
    public Animator animator;
    public SpriteRenderer sprite;
    public Material defaultMaterial;
    public Material whiteMaterial;
    public float lastTookDamage;

    public void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
        whiteMaterial = Resources.Load<Material>("Materials/WhiteMaterial");
        health = 3;
        lastTookDamage = 0;
    }

    private void LateUpdate()
    {
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
            Debug.Log(health);
        }
    }
}
