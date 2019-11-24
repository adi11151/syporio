using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 
public class Player: MonoBehaviour {

    [SerializeField, Tooltip("Player's speed multiplier")]
    private float movementSpeed = 20f;

    /// <summary>
    /// Is player facing left
    /// </summary>
    private bool _isLeft = false;

    /// <summary>
    /// Holds the X axis value
    /// </summary>
    private float _speedX;
    private Rigidbody2D _syporioRigidbody;
	
    private bool _isJumping;

    /// <summary>
    /// Time in air since syporio touched the ground
    /// </summary>
    private float _timeInAir;

    private string _currentPlatform = "Ground 1";

	public event Action Died;

    public event Action<string> NewPlatform;

    void Start () {
		_syporioRigidbody = gameObject.GetComponent<Rigidbody2D> () ;
	}

	// Update is called once per frame
	void Update () {
		move ();
		jump ();
	}

    public void Die()
    {
        if (Died != null)
        {
            Died();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cloud")
        {
            transform.parent = null;

        }
/*        if (collision.gameObject.tag == "Ground")
        {
            if (NewPlatform != null)
            {
                NewPlatform("None");

            }
        }
  */  }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ContactPoint2D contact = collision.contacts[0];
            Enemy EnemyInstance = collision.gameObject.GetComponent<Enemy>();
            if (contact.point.y > EnemyInstance.transform.position.y)
            {
                Debug.Log("enemy dead");
                //Enemy EnemyInstance = collision.gameObject.GetComponent<Enemy> ();
                EnemyInstance.Die();

            }
            else
            {
                Debug.Log("playerDead");
                Die();
            }
        }

        if (collision.gameObject.tag == "Collectible")
        {
            collision.gameObject.GetComponent<Collectible>().Collect();
            Destroy(collision.collider.gameObject);
        }

        if (collision.gameObject.tag == "cloud")
        {
            playerOnPlatform(collision);
        }

        if (collision.gameObject.tag == "Ground")
        {
            if (collision.gameObject.name != _currentPlatform)
            {
                if (NewPlatform != null)
                {
                    _currentPlatform = collision.gameObject.name;
                    NewPlatform(_currentPlatform);

                }
            }


        }

    }


	void FixedUpdate(){
		if (_isJumping) {
			_syporioRigidbody.AddForce(Vector2.up * 200);
		}

	}
    /// <summary>
    /// Jump this instance.
    /// </summary>
	void jump(){
		if (Input.GetKey (KeyCode.Space) && _timeInAir < 0.15f) {
			_isJumping = true;
			_timeInAir += Time.deltaTime;

		} else
			_isJumping = false;
		if (_syporioRigidbody.velocity.y == 0) {
			_timeInAir = 0;
		}
	}


    /// <summary>
    /// Move this instance.
    /// </summary>
	void move(){
		_speedX = Input.GetAxis("Horizontal");
		if (_speedX < 0.0f && _isLeft == false) {
			flipPlayer ();
			Debug.Log ("pressed");
		} else if (_speedX > 0.0f && _isLeft == true) {
			flipPlayer ();
		}
		_syporioRigidbody.velocity = new Vector2 (_speedX * movementSpeed, _syporioRigidbody.velocity.y);
	}

    /// <summary>
    /// checking players direction and flips it
    /// </summary>
	void flipPlayer(){
        _isLeft = !_isLeft;
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

	}

   void playerOnPlatform(Collision2D other)
    {

        if (other.gameObject.tag == "cloud")
        {
            transform.parent = other.transform;

        }
    }

    void OnCollisionExit2D(Collider other)
    {

    }

}

