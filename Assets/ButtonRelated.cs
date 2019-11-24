using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRelated : MonoBehaviour {

    //public GameObject Button;
    public GameObject Platform;
    private bool _buttonPushed = false;
    private bool _platformUp = false;



    private void Awake()
    {
       // enemy = GameObject.FindWithTag("Zombie");

     //   Physics.IgnoreCollision(enemy.collider, collider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_buttonPushed == false)&&(collision.gameObject.tag == "Player"))
        //Physics.IgnoreCollision(collider1: collision, collider2: this )
        {
            if(_platformUp == false)
            {
                ContactPoint2D contact = collision.contacts[0];
                if (contact.point.y > transform.position.y + 0.5)
                {
                    _buttonPushed = true;
                    StartCoroutine(ButtonPushed());
                    _buttonPushed = false;
                }
            }


        }

    }


    // Use this for initialization
    IEnumerator ButtonPushed()
    {
        _platformUp = true;
        transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        Platform.transform.position = new Vector2(Platform.transform.position.x, Platform.transform.position.y + 2);
        yield return new WaitForSeconds(2);
        Platform.transform.position = new Vector2(Platform.transform.position.x, Platform.transform.position.y - 2);
        transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        _platformUp = false;
    }
}
