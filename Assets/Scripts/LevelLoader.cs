using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField]
    private GameObject Syporio;
    [SerializeField]
    private Enemy Cop;
    [SerializeField]
    private Enemy SpecialCop;
    [SerializeField]
    private Drop Drop;
    [SerializeField]
    private GameObject Cloud;

    [SerializeField, Tooltip("Starting position for syporio")]
    private Transform _startPos;
    private Player _SyporioInstance;

    public Player SpawnSyporio(){
        Debug.Log("spawnSyporio");
        var syporioObject = Instantiate(Syporio, _startPos.position, Quaternion.identity);
        _SyporioInstance = syporioObject.GetComponent<Player>();
        return _SyporioInstance;
    }

    public Enemy SpawnCop(GameObject CopToSpawn){//add a script to the spawn pos, make fields in the inspector of what to spawn when this enemy is dead 
        //then send it to here as a parameter somehow (maybe strings, positions of other types or whatever) than add to the enemy script those parameters and when it is dead call the propriate functions
        var enemyObject = Instantiate(Cop, CopToSpawn.transform.position, Quaternion.identity);
        Enemy _EnemyInstance = enemyObject.GetComponent<Enemy>();
        _EnemyInstance.setTarget(_SyporioInstance.transform);
        if (CopToSpawn.GetComponent<EnemySpawnPos>() != null)
        {
            EnemySpawnPos ENS = CopToSpawn.gameObject.GetComponent<EnemySpawnPos>();
            //ENS.GetSpawnWhenDead();

            //GameObject a = ENS.GetComponent<GameObject>();
            if (ENS.SpawnWhenDead.tag == "cloud")
            {
                _EnemyInstance.GetComponent<Enemy>().setSpawnWhenDead(ENS.GetSpawnWhenDeadString(), ENS.GetSpawnWhenDeadPosition());
            }
        }
        return _EnemyInstance;
    }

    public Drop SpawnDrop(float x, float y, GameManager gameManager)
    {
        Vector2 pos = new Vector2(x, y + 1f);
        var newDrop = Instantiate(Drop, pos, Quaternion.identity);
        newDrop.GameManager = gameManager;
        return newDrop;
    }

    public void SpawnCloud(float xPos, float yPos)
    {
        var cloudObj = Instantiate(Cloud, new Vector2(xPos, yPos), Quaternion.identity);
        Debug.Log("instan");
    }
}
