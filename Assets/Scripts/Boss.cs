using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private float lastAction = 0;
    private System.Random random;
    new void Awake()
    {
        base.Awake();
        random = new System.Random();
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

        if (Time.time - lastAction > 2)
            {
                lastAction = Time.time;
                int mode = random.Next(1, 4);

                switch(mode)
                {
                    case 1:
                        Summon();
                        break;
                    case 2:
                        Stomp();
                        break;
                }
            }
    }

    public void Summon()
    {
        Debug.Log("Summon");
    }

    public void Stomp()
    {
        Debug.Log("Stomp");
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
