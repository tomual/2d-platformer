using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    public Rigidbody2D enemyRigidbody;
    public Animator animator;

    public void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public bool IsPlaying(string name)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animatorStateInfo.IsName(name) && animatorStateInfo.normalizedTime < 1.0f;
    }
}
