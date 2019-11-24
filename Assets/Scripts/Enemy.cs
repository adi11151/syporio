using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {
	
    private Transform _followTarget;
    private Rigidbody2D _enemy;
    private bool _isLeft = true;
    public Vector2 SpawnWhenDeadPos;
    public string SpawnWhenDeadString;
    public virtual event Action<Enemy, string> EnemyDead;
    Vector2 targetDirection;
    GameObject TargetPlatform;
    public string GroundPlaced;
    bool TargetOnTheSamePlatform;
    //[SerializeField]public string[] toSpawnWhenDead;
//	public delegate void EnemyDead(Enemys m, float e);


    //float speed = 10;
	// Use this for initialization
	void Start () {

        TargetOnTheSamePlatform = true;
        targetDirection.x = 1f;
        _enemy = gameObject.GetComponent<Rigidbody2D>();
	}

    public void setTargetPlatform(string platform)//enemy has to know if he and the player on the same platform
    {
        if (platform == GroundPlaced)
        {
            TargetOnTheSamePlatform = true;
        }
        else
             TargetOnTheSamePlatform = false;
    }

    public void setSpawnWhenDead(String whatToSpawnString, Vector2 whatToSpawnPosition )
    {
        SpawnWhenDeadPos = new Vector2(whatToSpawnPosition.x, whatToSpawnPosition.y);
        SpawnWhenDeadString = whatToSpawnString;
    }

    public void setTarget(Transform target){
		_followTarget = target;
	}

    public void setTargetPlatform(GameObject platform)//enemy has to know if he and the player on
    {
        TargetPlatform = platform;
    }

    public bool isThereWhatToSpawn()
    {
        if (SpawnWhenDeadString == "")
            return false;
        else return true;
    }

    public Vector2 getSpawnWhenDeadPosition()
    {
        return SpawnWhenDeadPos;
    }

    public string getSpawnWhenDeadString()
    {
        return SpawnWhenDeadString;
    }

    public virtual void Die(){
		transform.localScale = new Vector3(transform.localScale.x, 0.1f,transform.localScale.z );
		if (EnemyDead != null) {
			EnemyDead (this, "Default");
		}
		Destroy (this.gameObject, 0.2f);
	}

    void flipIcon(Vector2 scale)
    {
        scale.x *= -1;
        _isLeft = !_isLeft;
        transform.localScale = scale;
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update() {
        Vector2 targetHeading = _followTarget.transform.position - transform.position;
        Vector2 scale = transform.localScale;
        if (!TargetOnTheSamePlatform){
            //check if the target is on the same platform of too far
            targetDirection = targetDirection.normalized;

            }else{

            targetDirection = targetHeading.normalized;
            if (targetHeading.x > 0.0f && _isLeft == true)
            {
                flipIcon(scale);
            }
            else if (targetHeading.x < 0.0f && _isLeft == false)
            {
                flipIcon(scale);
            }
           
        }
        _enemy.velocity = new Vector2(targetDirection.x * 1.5f, _enemy.velocity.y);

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndOfPlatform")
        { 
            targetDirection.x *= -1f;
            targetDirection = targetDirection.normalized;
            flipIcon(transform.localScale);
        }

        if (collision.gameObject.tag == "Ground")
        {
            GroundPlaced = collision.gameObject.name;
        }
    }
}
