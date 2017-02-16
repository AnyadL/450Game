using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttributeController : MonoBehaviour {

    public float health;
    public int isRanged = 0;

    public float bigCooldown = 10f;
    public float nextBigFire;

    // Update is called once per frame
    void Update () {
		if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                gameObject.GetComponent<SetPlayerUI>().Update();
                --Globals.livingPlayers;
                //Debug.Log("living players = " + Globals.livingPlayers);
                if (gameObject.name == "Delilah")
                {
                    Destroy(gameObject.GetComponent<DelilahAttack>().wall);
                }
                if (Globals.livingPlayers <= 0)
                {
                    Globals.livingPlayers = 2;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
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
        //Debug.Log(collision.gameObject.tag);
        //Debug.Log(health);
        if (collision.gameObject.tag == "Enemy")
        {
            health = health - 1; 
        }
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            health = health - 1;
        }
        else if (collision.gameObject.tag == "DeathLine")
        {
            health = 0;
        }
    }

    void enemyCollisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            return;
        }
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            return;
        }
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "LittleAttack")
        {
            //Debug.Log("here");
            health = health - 1;
        }
        else if(collision.gameObject.tag == "BigAttack")
        {
            health = health - 3;
        }
        else if (collision.gameObject.tag == "DeathLine")
        {
            health = 0;
        }
    }
}
