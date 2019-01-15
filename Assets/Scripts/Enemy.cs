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
        --health;
        Debug.Log("Enemy Ouch");
        Debug.Log(health);
        sprite.material = whiteMaterial;
        lastTookDamage = Time.time;
        if (isDead())
        {
            Die();
        }
    }

    public bool isDead()
    {
        return health == 0;
    }

    public void Die()
    {
        anim.SetBool("Dead", true);
        rb.mass = 10000;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDead())
        {
            Debug.Log(col.gameObject.name);
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
