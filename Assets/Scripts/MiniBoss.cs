using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : FollowingEnemy
{
    new void Awake()
    {
        base.Awake();
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        health = 2;
    }

    void Update()
    {

    }
}
