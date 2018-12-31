using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.


    public float moveForce = 300;          // Amount of force added to move the player left and right.
    public float maxSpeed = 2;             // The fastest the player can travel in the x axis.
    public AudioClip[] jumpClips;           // Array of clips for when the player jumps.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    public AudioClip[] taunts;              // Array of clips for when the player taunts.
    public float tauntProbability = 50f;    // Chance of a taunt happening.
    public float tauntDelay = 1f;           // Delay for when the taunt should happen.


    private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;          // Whether or not the player is grounded.
    private Animator anim;                  // Reference to the player's animator component.

    public Rigidbody2D rb;
    public Rigidbody2D rocket;
    public float speed = 20f;				// The speed the rocket will fire at.

    private GameObject weapon;

    float lastKnockback = -2;
    bool invincible = false;


    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }


    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        anim.SetBool("Grounded", grounded);
        if (Input.GetButtonDown("Jump") && grounded && !isBeingKnockedBack())
        {
            jump = true;
        }

        // If the fire button is pressed...
		if(Input.GetButtonDown("Fire1"))
		{
			anim.SetTrigger("Shoot");

            // If the player is facing right...
            if (facingRight)
			{
                Vector3 position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
				Rigidbody2D bulletInstance = Instantiate(rocket, position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			}
			else
            {
                Vector3 position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                Rigidbody2D bulletInstance = Instantiate(rocket, position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			}
        }

        // Fire1 = Shoot fireball
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");

            // If the player is facing right...
            if (facingRight)
            {
                Vector3 position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                Rigidbody2D bulletInstance = Instantiate(rocket, position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(speed, 0);
            }
            else
            {
                Vector3 position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                Rigidbody2D bulletInstance = Instantiate(rocket, position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-speed, 0);
            }
        }

        // Fire2 = Slash
        if (Input.GetButtonDown("Fire2") && !isPlaying("slash"))
        {
            anim.SetTrigger("Slash");
        }

        if (isPlaying("slash"))
        {
            weapon.SetActive(true);
        } else
        {
            weapon.SetActive(false);
        }

        if (invincible)
        {
            blink();
        }
    }


    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        {
            // ... add a force to the player.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        }

        if (h == 0 && !isBeingKnockedBack())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        {
            // ... set the player's velocity to the maxSpeed in the x axis.
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

        // If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            anim.SetTrigger("Jump");

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    bool isPlaying(string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
        
    //    float thrustY = 350;
    //    float thrustX = 550;

    //    if (collision.gameObject.name == "Enemy" && !isBeingKnockedBack())
    //    {
    //        float enemyXPosition = collision.gameObject.transform.position.x;
    //        lastKnockback = Time.time;

    //        //rb.AddForce(transform.up * thrustY);
    //        if (transform.position.x > enemyXPosition)
    //        {
    //            rb.AddForce(new Vector2(thrustX, thrustY));
    //        } else
    //        {
    //            rb.AddForce(new Vector2(-thrustX, thrustY));
    //        }
    //        Debug.Log(Time.time);
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        float thrustY = 350;
        float thrustX = 550;

        if (collision.gameObject.name == "EnemyWeapon" && !isBeingKnockedBack() && !invincible)
        {
            float enemyXPosition = collision.gameObject.transform.position.x;
            lastKnockback = Time.time;
            invincible = true;

            //rb.AddForce(transform.up * thrustY);
            if (transform.position.x > enemyXPosition)
            {
                rb.AddForce(new Vector2(-thrustX, thrustY));
            }
            else 
            {
                rb.AddForce(new Vector2(thrustX, thrustY));
            }
        }
    }

    private bool isBeingKnockedBack()
    {
        return Time.time - lastKnockback < 0.3;
    }

    private void blink()
    {
        string time = Time.time.ToString("0.0");
        char milliseconds = time.ToCharArray()[time.Length - 1];
        bool beTransparent = int.Parse(milliseconds.ToString()) % 3 == 0;

        if (beTransparent)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);

        } else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }

        if (Time.time - lastKnockback > 1)
        {
            invincible = false;
        }

    }
}
