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

    private void Start()
    {
        myLayer = gameObject.layer;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update () {
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                --Globals.livingPlayers;
                killPlayer(gameObject.GetComponent<HeroMovement>().playerNumber);
                if (gameObject.name == "Delilah")
                {
                    Destroy(gameObject.GetComponent<DelilahAttack>().wall);
                }
                gameObject.GetComponent<SetPlayerUI>().Update();
            }
            Destroy(gameObject);
        }

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

    public void killPlayer(string playerNumber)
    {

    }

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

    public bool decreaseHealth(float damage)
    {
        if (gameObject.layer == 13)
        {
            return false;
        }
        health = health - damage;
        return true;
    }

    public float getHealth()
    {
        return health;
    }

    private void takenDamage()
    {
        gameObject.layer = 13;
        nextVulnerable = Time.time + invincibiltyLength;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.75f);
    }

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
