using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : FollowingEnemy {

    private bool allowedToMove = false;

    void Start()
    {
    }

    void Update () {
		if (!isDead() && allowedToMove)
        {
            UpdateDirection();
            UpdateWeapon();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void enableBoss(bool enable)
    {
        allowedToMove = enable;
    }
}
