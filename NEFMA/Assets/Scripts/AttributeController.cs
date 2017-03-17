using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeController : MonoBehaviour {

    private HeroMovement myMovement;
    [HideInInspector] private float health = 1;
    public float maxHealth = 1;

    public float bigCooldown = 10f;
    [HideInInspector] public float nextBigFire;

    [HideInInspector] public int myLayer;
    [HideInInspector] public float nextVulnerable;
    [HideInInspector] public bool knockbacked;
    public float invincibiltyLength = 2.0f;
    [HideInInspector] public EnemyAI enemyAI;
    [HideInInspector] public float speed;
    //[HideInInspector] float pcooldown;
   
    public void Start()
    {
        myMovement = gameObject.GetComponent<HeroMovement>();
        if (gameObject.layer != 13)
        {
            myLayer = gameObject.layer;
        }
        health = maxHealth;

        if (gameObject.tag == "Enemy")
        {
            enemyAI = gameObject.GetComponent<EnemyAI>();
            speed = enemyAI.maxSpeed;
            //pcooldown = enemyAI.projectileCooldown;
        }
    }

    void Update () {
        // if we died
        if (health <= 0)
        {
            // if we are a player
            if (gameObject.tag == "Player")
            {
                killPlayer(myMovement.playerNumber);
                if (Globals.players[myMovement.playerNumber].Name == "Delilah")
                {
                    Destroy(gameObject.GetComponent<DelilahAttack>().wall);
                }
                gameObject.GetComponent<SetPlayerUI>().Update();
            }
            if (gameObject.tag == "BossHead")
            {
                // Defeated Boss, should call some script here
                health = 1;
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
    public void killPlayer(int playerNumber)
    {
        Globals.players[playerNumber].Alive = false;
        Globals.players[playerNumber].GO = null;
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

    IEnumerator stunEnemy()
    {
         Debug.Log("speed:" + speed);
 
         enemyAI.maxSpeed = 0;
         if (enemyAI.isRanged)
         {
            enemyAI.nextProjectileFire = enemyAI.nextProjectileFire + 1.0f;
            enemyAI.rangedBurst = 0;
         }
        yield return new WaitForSeconds(2);
        Debug.Log("Waited");
        enemyAI.maxSpeed = speed;
        //enemyAI.projectileCooldown = pcooldown;
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
        //If player is not currently punching then it will do the attack collisions
        if (gameObject.layer != 14)
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
        }
        // LEXIE: I changed this else if to an if because we still want to die if the layer is 14. Check if this breaks your stuff
        if (collision.gameObject.tag == "DeathLine")
        {
            health = 0;
        }
        else if (collision.gameObject.tag == "Checkpoint")
        {
            health = maxHealth;
        }

       else if (collision.gameObject.tag == "Collect")
        {
            for (int i = 0; i < Globals.players.Count; ++i)
            {
                if (Globals.players[i].GO == gameObject)
                {
                    Globals.players[i].Score += 1;
                    Debug.Log(Globals.players[i].Score);
                }
            }
            Destroy(collision.gameObject);
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
            if (decreaseHealth(2.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if (collision.gameObject.tag == "BigAttack")
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
            //knockback(collision.gameObject.transform.position.x);
            StartCoroutine(stunEnemy());
        }

        //This is for rkyer dashing through enemy or any time where a character is invincible but can still attack enemies
        else if (collision.gameObject.layer == 14)
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
                knockback(collision.gameObject.transform.position.x);
            }
        }
        else if (collision.gameObject.tag == "Deflect")
        {
            knockback(collision.gameObject.transform.position.x);
        }

    }
}
