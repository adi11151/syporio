using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloudSpawner : Enemy {

    public GameObject _secretCloudPosition;
    public override event Action<Enemy, string> EnemyDead;

    public override void Die()
    {
        transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        if (EnemyDead != null)
        {
            EnemyDead(this, "CloudSpawner");
        }
    }
}
