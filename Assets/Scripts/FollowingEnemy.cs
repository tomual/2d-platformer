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
    }
	
	void Update () {

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
        } else
        {
            h = 0.5f;
            if (!facingRight)
            {
                Flip();
            }
        }

        if (isPlaying("attack_enemy") && Time.time - attackStart > 0.5)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        if (!isPlaying("attack_enemy"))
        {
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            }

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            rb.mass = 1.4f;
        }
        else
        {
            rb.mass = 10000;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            if (!isPlaying("attack_enemy"))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                attackStart = Time.time;
                anim.SetTrigger("Attack");
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
}
