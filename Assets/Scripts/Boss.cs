using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    new void Awake()
    {
        base.Awake();
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        health = 2;
    }

    private void FixedUpdate()
    {
        if (AllowedToMove())
        {
            UpdateWeapon();
            if (!IsPlaying("attack"))
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < 3)
                {
                    Attack();
                }
            }
        }
        else
        {
            animator.SetBool("Moving", false);
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

    void Attack()
    {
        animator.SetTrigger("Attack");
        attackStart = Time.time;
    }
}
