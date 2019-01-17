using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossEnabler : MonoBehaviour {

    public MiniBoss miniBoss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            miniBoss.enableBoss(true);
        }
    }
}
