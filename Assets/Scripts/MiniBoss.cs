using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : FollowingEnemy {

    private bool allowedToMove = false;

    void Update () {
		if (!isDead() && allowedToMove)
        {
            UpdateDirection();
            UpdateWeapon();
        }
        if (isDead())
        {
            weapon.SetActive(false);
            anim.SetBool("Moving", false);
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (allowedToMove)
        {
            Move();
        }
    }

    public void enableBoss(bool enable)
    {
        allowedToMove = enable;
    }
}
