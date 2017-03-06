using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script so far just makes the fireballs disappear when they go off screen, more will probably be added later
public class ProjectileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
 
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "LittleAttack")
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "EnemyAttack")
            {
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag ==  "Deflect")
        {
            Debug.Log("Deflect");


        }
    }
}
