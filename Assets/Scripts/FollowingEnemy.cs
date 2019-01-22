using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy {
    private float moveForce = 220f;          // Amount of force added to move the player left and right.
    private float maxSpeed = 0.5f;             // The fastest the player can travel in the x axis.
    float h = 0.5f;
    public bool facingRight = false;
    private float attackStart = 0;
    private GameObject weapon;

    void Start () {
        weapon = gameObject.transform.GetChild(0).gameObject;
        anim.SetBool("Moving", true);
    }
	
	void Update ()
    {
        if (!isDead())
        {
            UpdateDirection();
            UpdateWeapon();
        }
        if (isDead())
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        Debug.Log("Move?");
        if (!isPlaying("attack_enemy") && !isDead())
        {
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            }

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    public void UpdateDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float myXPosition = gameObject.transform.position.x;
        float playerXPosition = player.transform.position.x;

        if (myXPosition > playerXPosition)
        {
            h = -0.5f;
            if (facingRight)
            {
                Flip();
            }
        }
        else
        {
            h = 0.5f;
            if (!facingRight)
            {
                Flip();
            }
        }

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isDead())
        {
            if (collision.gameObject.name == "Player")
            {
                if (!isPlaying("attack_enemy"))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                    attackStart = Time.time;
                    anim.SetTrigger("Attack");
                }
                rb.mass = 10000;
            }
            else
            {
                rb.mass = 1.4f;
            }
        }
    }

    void Attack()
    {

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public override void TakeDamage()
    {
        float thrustY = 350;
        --health;
        rb.AddForce(new Vector2(0, thrustY));
        sprite.material = whiteMaterial;
        lastTookDamage = Time.time;
        if (isDead())
        {
            anim.SetBool("Dead", true);
        }
    }
}
