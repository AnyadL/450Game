using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeController : MonoBehaviour {

    [HideInInspector] private float health = 1;
    public float maxHealth = 1;
    public int isRanged = 0;

    public float bigCooldown = 10f;
    [HideInInspector] public float nextBigFire;

    [HideInInspector] public int myLayer;
    [HideInInspector] public float nextVulnerable;
    [HideInInspector] public bool knockbacked;
    public float invincibiltyLength = 2.0f;

    public void Start()
    {
        if (gameObject.layer != 13)
        {
            myLayer = gameObject.layer;
        }
        health = maxHealth;
    }

    void Update () {
        // if we died
        if (health <= 0)
        {
            // if we are a player
            if (gameObject.tag == "Player")
            {
                killPlayer(gameObject.GetComponent<HeroMovement>().playerNumber);
                if (gameObject.name == "Delilah")
                {
                    Destroy(gameObject.GetComponent<DelilahAttack>().wall);
                }
                gameObject.GetComponent<SetPlayerUI>().Update();
            }
            Destroy(gameObject);
        }

        // if we are still marked as invincible
        if (gameObject.layer == 13)
        {
            if (nextVulnerable <= Time.time)
            {
                gameObject.layer = myLayer;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, Mathf.Sin(2 * Mathf.PI * 5 * (nextVulnerable - Time.time) + 0), Mathf.Sin(2 * Mathf.PI * 5 * (nextVulnerable - Time.time) + 0), 0.75f);
            }
        }
	}

    // Sets global variables for the player identified by playerNumber, marking them as killed
    public void killPlayer(string playerNumber)
    {
        if (playerNumber == Globals.player1.Number.ToString())
        {
            Globals.player1.Alive = false;
            Globals.player1.GO = null;
        }
        else if (playerNumber == Globals.player2.Number.ToString())
        {
            Globals.player2.Alive = false;
            Globals.player2.GO = null;
        }
        else if (playerNumber == Globals.player3.Number.ToString())
        {
            Globals.player3.Alive = false;
            Globals.player3.GO = null;
        }
        else if (playerNumber == Globals.player4.Number.ToString())
        {
            Globals.player4.Alive = false;
            Globals.player4.GO = null;
        }
        --Globals.livingPlayers;
    }

    // responsible for knocking the current gameobject away from whatever hit them
    private void knockback(float x)
    {
        knockbacked = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (x < gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(20, 10, 0), ForceMode2D.Impulse);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-20, 10, 0), ForceMode2D.Impulse);
        }
    }

    // decreases health by damage as long as the current gameobject isnt invincible
    public bool decreaseHealth(float damage)
    {
        if (gameObject.layer == 13)
        {
            return false;
        }
        health = health - damage;
        return true;
    }

    // returns the value of the private variable health
    public float getHealth()
    {
        return health;
    }

    // marks the current gameobject as invincible
    public void takenDamage()
    {
        gameObject.layer = 13;
        nextVulnerable = Time.time + invincibiltyLength;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.75f);
    }

    // determines if the current gameobject is a player or an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCollisions(collision);
        }
        if (gameObject.tag == "Player"){
            playerCollisions(collision);
        }
    }

    // something has collided with the player
    void playerCollisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if (collision.gameObject.tag == "DeathLine")
        {
            health = 0;
        }
        else if (collision.gameObject.tag == "Checkpoint")
        {
            health = maxHealth;
        }
    }

    // something has collided with an enemy
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
        else if (collision.gameObject.tag == "LittleAttack")
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if(collision.gameObject.tag == "BigAttack")
        {
            if (decreaseHealth(3.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if (collision.gameObject.tag == "DeathLine")
        {
            health = 0;
        }
        else if (collision.gameObject.tag == "Stun")
        {
            knockback(collision.gameObject.transform.position.x);
        }
    }
}
