using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    private bool allowedToMove = false;

    void Update()
    {
        if (isDead())
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (allowedToMove)
        {
        }
    }

    public void enableBoss(bool enable)
    {
        allowedToMove = enable;
    }
}
