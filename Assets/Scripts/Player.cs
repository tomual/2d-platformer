using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    public bool jump = false;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        float maxSpeed = 2;
        float moveForce = 300;
        float jumpForce = 1000f;

        animator.SetFloat("Speed", Mathf.Abs(h));

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        }

        if (h == 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        }
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            animator.SetTrigger("Jump");

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
