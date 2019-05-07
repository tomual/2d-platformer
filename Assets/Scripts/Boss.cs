using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private float lastAction = 0;
    private System.Random random;
    public Rigidbody2D fallingDanger;
    public Rigidbody2D minion;
    Rigidbody2D minionInstance;
    new void Awake()
    {
        base.Awake();
        random = new System.Random();
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        health = 1;
    }

    private void FixedUpdate()
    {
        if (AllowedToMove())
        {
            UpdateWeapon();
        }
        else
        {
            return;
        }

        if (Time.time - lastAction > 2)
            {
                lastAction = Time.time;
                int mode = random.Next(1, 4);
            
                switch (mode)
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        if (MinionCount() == 0)
                        {
                            Summon();
                            break;
                        }
                        Attack();
                        break;
                    case 3:
                        Stomp();
                        break;
                }
        }
    }

    public void Summon()
    {
        animator.SetTrigger("Summon");
        Vector3 position = new Vector3(transform.position.x - 10f, transform.position.y + 4f, transform.position.z);
        minionInstance = Instantiate(minion, position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;

        Debug.Log("Summon");
    }

    public void Stomp()
    {
        animator.SetTrigger("Stomp");
        Debug.Log("Stomp");
        for (int i = 0; i < 4; i++)
        {
            float distance = Random.Range(1.5f, 3.0f);
            float x = i * distance + 1.5f;
            Vector3 position = new Vector3(transform.position.x - x, transform.position.y + 5f, transform.position.z);
            Rigidbody2D fallingDangerInstance = Instantiate(fallingDanger, position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
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

    int MinionCount()
    {
        return FindObjectsOfType<FollowingEnemy>().Length;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        attackStart = Time.time;
    }
}
