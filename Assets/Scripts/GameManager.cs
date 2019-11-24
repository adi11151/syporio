using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.EventArgs;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    


    private float points = 0;

    [SerializeField, Tooltip("Level Loader instance")]
    private LevelLoader _levelLoader;
    private List<Enemy> _enemyInstances = new List<Enemy>();
    private Player _syporioInstance;

    [SerializeField, Tooltip ("Camera with a camera controller")]
    private CameraController _cameraController;

    [SerializeField, Tooltip("Minimum y position for player. Below this line, he will die")]
    private float _lowDeathLine;



    // Use this for initialization
    void Start()
    {
        
        _syporioInstance = _levelLoader.SpawnSyporio();
        _syporioInstance.Died += PlayerDead;
        _syporioInstance.NewPlatform += NewPlatform;


        _cameraController.Setup(_syporioInstance.gameObject);

        // Pre-spawned collectibles
        var prePlacedCollectibles = FindObjectsOfType<Collectible>();
        for (int collectibleIndex = 0; collectibleIndex < prePlacedCollectibles.Length; collectibleIndex++)
        {
            prePlacedCollectibles[collectibleIndex].GameManager = this;
        }

        //for cops to spawn during game
        var copTraps = FindObjectsOfType<CopTrapCollider>();
        for (int copTrapsIndex = 0; copTrapsIndex < copTraps.Length; copTrapsIndex++)
        {
            copTraps[copTrapsIndex].GameManager = this;
        }

        // Spawn Cops
        var _copSpawns = GameObject.FindGameObjectsWithTag("CopSpawnPos");

        for (int copIndex = 0; copIndex < _copSpawns.Length; copIndex++)
        {

            var enemyInstance = _levelLoader.SpawnCop(_copSpawns[copIndex].gameObject);
            _enemyInstances.Add(enemyInstance);
            enemyInstance.EnemyDead += EnemyDead;
        }


    }

    public void NewPlatform(string PlatformName)
    {
        for (int i = 0; i< _enemyInstances.Count; i++)
        {
            if (_enemyInstances[i]!= null)
            {
                Enemy enemyInstance = _enemyInstances[i].GetComponent<Enemy>();
                enemyInstance.setTargetPlatform(PlatformName);
            }

        }
    }
    //add function to set enemis player platform

    /// <summary>
    /// called from coop trap script when player reached the trap 
    /// </summary>
    public void reachedCopTrap(GameObject pos)
    {
        Debug.Log("reached");
        
        var enemyInstance = _levelLoader.SpawnCop(pos);
       _enemyInstances.Add(enemyInstance);
        enemyInstance.EnemyDead += EnemyDead;

    }

    /// <summary>
    /// Cleanup for dead enemy
    /// </summary>
    public void EnemyDead(Enemy enemy, string type){
        enemy.EnemyDead -= EnemyDead;
        Debug.Log("Dead");

        _levelLoader.SpawnDrop(enemy.transform.position.x, enemy.transform.position.y + 1f, this);
        //_levelLoader.SpawnCloud()

        if (enemy.isThereWhatToSpawn())
        {
            _levelLoader.SpawnCloud(enemy.getSpawnWhenDeadPosition().x, enemy.getSpawnWhenDeadPosition().y);
            Debug.Log("YESSSS!!!!!!");
        }

    }

   /// <summary>
   /// adding point when player took a drop
   /// </summary>
    public void AddPoint(){
        points++;
        Debug.Log ("point+1");
    }

    /// <summary>
    /// when syporio is dead
    /// </summary>
    public void PlayerDead(){
        Debug.Log ("syporio is dead");
        _syporioInstance.Died -= PlayerDead;
        GameOver ();
    }

    /// <summary>
    /// Games is over.
    /// </summary>
    public void GameOver(){
        SceneManager.LoadScene(0);
    }


    /// <summary>
    /// Update function checking each frame if syporio died
    /// </summary>
    void Update()
    {
        CheckPlayerDeath();
    }

    public void CreateCloud(float posX, float posY){
        _levelLoader.SpawnCloud(posX, posY);
    }

    /// <summary>
    /// Checks the player death by checking if it reached to minimum point.
    /// </summary>
    private void CheckPlayerDeath()
    {
        if (_syporioInstance.transform.position.y < _lowDeathLine)
            _syporioInstance.Die();
    }

    /// <summary>
    /// draw gizmos at the minimum position.
    /// </summary>
    public void OnDrawGizmos()
    {
        Vector3 from = new Vector3(-100f, _lowDeathLine, 0f);
        Vector3 to = new Vector3(100f, _lowDeathLine, 0f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(from, to);
    }
}
