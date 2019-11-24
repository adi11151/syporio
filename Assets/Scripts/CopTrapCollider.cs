using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bundle
{
    [SerializeField] Transform position;
    [SerializeField] string type;

}

public class CopTrapCollider : MonoBehaviour {
    //public bundle[] _copToSpawn;
    [SerializeField]public GameObject[] _copToSpawn;
    //[SerializeField] public string[] _type;
    [HideInInspector]public GameManager GameManager;
    private bool hited = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
         if ((collision.gameObject.tag == "Player")&&(!hited))
        {
            hited = true;
            for (int i = 0; i< _copToSpawn.Length; i++)
            {
                copTrapHited(i);
            }

        }
        else
        {
            if(hited)
                Destroy(this.gameObject);
        }

    }

    public void copTrapHited(int i)
    {

        if(_copToSpawn[i]!= null)
            GameManager.reachedCopTrap(_copToSpawn[i]);
    }

}
