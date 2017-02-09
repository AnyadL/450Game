using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeController : MonoBehaviour {

    public float health;
    
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Enemy")
        {
            //Debug.Log(collision.gameObject.tag);
            enemyCollisions(collision);
        }
        if (gameObject.tag == "Player"){
            playerCollisions(collision);
        }
    }
    void playerCollisions(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(health);
        if (collision.gameObject.tag == "Enemy")
        {
            health = health - 1; 
        }
    }

    void enemyCollisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            return;
        }
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "LittleAttack")
        {
            Debug.Log("here");
            health = health - 1;
        }
        else if(collision.gameObject.tag == "BigAttack")
        {
            health = health - 3;
        }
    }
}
