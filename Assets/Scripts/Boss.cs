using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    private bool allowedToMove = false;
    private float attackStart = 0;
    private float lastAction = 0;
    private GameObject weapon;
    private System.Random random;
    public Rigidbody2D minion;
    UIController uiController;

    void Start()
    {
        weapon = gameObject.transform.GetChild(0).gameObject;
        random = new System.Random();
        uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    void Update()
    {
        if (!isDead())
        {
            UpdateWeapon();

            if (Time.time - lastAction > 2)
            {
                int mode = random.Next(1, 4);

                switch(mode)
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        Stomp();
                        break;
                    case 3:
                        if (lastTookDamage != 0 && MinionCount() == 0)
                        {
                            Summon();
                        } else
                        {
                            Attack();
                        }
                        break;
                }
                lastAction = Time.time;
            }
        }
        if (isDead())
        {
            Die();
            uiController.ShowEndScreen();
        }
    }

    private void Attack()
    {
        attackStart = Time.time;
        anim.SetTrigger("Attack");
    }

    private void Summon()
    {
        Vector3 position = new Vector3(transform.position.x - 5f, transform.position.y + 1f, transform.position.z);
        Rigidbody2D minionInstance = Instantiate(minion, position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
    }

    private int MinionCount()
    {
        return FindObjectsOfType<FollowingEnemy>().Length;
    }

    private void Stomp()
    {
        Debug.Log("Stomp");
    }

    private void FixedUpdate()
    {
        if (allowedToMove)
        {
        }
    }

    public void enableBoss(bool enable)
    {
        allowedToMove = enable;
    }

    public void UpdateWeapon()
    {
        if (isPlaying("attack_enemy") && Time.time - attackStart > 0.5)
        {
            rb.mass = 10000;
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
    }

    public void Swipe()
    {
        if (!isDead())
        {
            if (!isPlaying("attack_enemy"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                attackStart = Time.time;
                anim.SetTrigger("Attack");
            }
        }
    }
}
