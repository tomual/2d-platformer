using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer sprite;
    public Material defaultMaterial;
    public Material whiteMaterial;
    public float lastTookDamage = 0;
    private float spriteAlpha = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        defaultMaterial = sprite.material;
        whiteMaterial = (Material) AssetDatabase.LoadAssetAtPath("Assets/Sprites/WhiteMaterial.mat", typeof(Material));
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isDead())
        {
            Die();
        }
    }

    private void LateUpdate()
    {
        if (Time.time - lastTookDamage >= 0.1f)
        {
            sprite.material = defaultMaterial;
        }
    }

    public virtual void TakeDamage()
    {
        if (Time.time - lastTookDamage >= 0.1f)
        {
            --health;
            sprite.material = whiteMaterial;
            lastTookDamage = Time.time;
            if (isDead())
            {
                anim.SetTrigger("Dead");
            }
        }
    }

    public bool isDead()
    {
        return health == 0;
    }

    public void Die()
    {
        Debug.Log("Die");
        Debug.Log(health);
        float r = GetComponent<SpriteRenderer>().color.r;
        float g = GetComponent<SpriteRenderer>().color.g;
        float b = GetComponent<SpriteRenderer>().color.b;
        GetComponent<SpriteRenderer>().color = new Color(r, g, b, spriteAlpha);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (Time.time - lastTookDamage >= 2)
        {
            spriteAlpha -= 0.01f;
            if (spriteAlpha <= 0)
            {
                Destroy(gameObject);
            }
            rb.mass = 10000;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDead())
        {
            if (col.gameObject.name == "Weapon")
            {
                TakeDamage();
            }
        }
    }

    public bool isPlaying(string stateName)
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
}
