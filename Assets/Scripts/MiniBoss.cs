using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : FollowingEnemy
{
    public GameObject teleporter;

    new void Awake()
    {
        base.Awake();
        weapon = transform.Find("EnemyWeapon").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        teleporter.SetActive(false);
        health = 2;
    }

    void Update()
    {
        if (IsDead())
        {
            teleporter.SetActive(true);
        }
    }
}
