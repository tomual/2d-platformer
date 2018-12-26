using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy {
    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 1f;             // The fastest the player can travel in the x axis.
    float h = 1;
    public bool facingRight = false;

    void Start () {
		
	}
	
	void Update () {

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float myXPosition = gameObject.transform.position.x;
        float playerXPosition = player.transform.position.x;

        if (myXPosition > playerXPosition)
        {
            h = -1;
            if (facingRight)
            {
                Flip();
            }
        } else
        {
            h = 1;
            if (!facingRight)
            {
                Flip();
            }
        }

        if (isPlaying("slash"))
        {
            gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }

    }

    private void FixedUpdate()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            if (!isPlaying("attack_enemy"))
            {
                anim.SetTrigger("Attack");
            }
            
        }

        rb.mass = 10;
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
