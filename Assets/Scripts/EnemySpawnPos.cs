using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPos : MonoBehaviour {

    [SerializeField] public GameObject SpawnWhenDead;

    public Vector2 GetSpawnWhenDeadPosition()
    {
        return SpawnWhenDead.transform.position;
    }

    public string GetSpawnWhenDeadString()
    {
        return SpawnWhenDead.tag;
    }



}
