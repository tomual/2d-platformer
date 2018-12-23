using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage()
    {
        Debug.Log("Ouch");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Weapon")
        {
            --health;
            Debug.Log("Ouch");
            Debug.Log(health);
            if (isDead())
            {
                Die();
            }
        }
    }

    bool isDead()
    {
        return health == 0;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
