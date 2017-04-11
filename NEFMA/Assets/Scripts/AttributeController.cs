using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttributeController : MonoBehaviour {

    private HeroMovement myMovement;
    private Rigidbody2D myBody;
    public float health = 1;
    //[HideInInspector] private float health = 1; // Todo put back in
    public float maxHealth = 30;

    public float bigCooldown = 10f;
    [HideInInspector] public float nextBigFire;

    [HideInInspector] public int myLayer;
    [HideInInspector] public float nextVulnerable;
    [HideInInspector] public bool knockbacked;
    public float invincibiltyLength = 2.0f;
    [HideInInspector] public EnemyAI enemyAI;
    [HideInInspector] public float speed;
    [HideInInspector] public bool dashing = false;
    [HideInInspector] public float deflectVelocity = 10;
   
    public void Start()
    {
        myMovement = gameObject.GetComponent<HeroMovement>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        if (gameObject.layer != 13)
        {
            myLayer = gameObject.layer;
        }
        health = maxHealth;

        if (gameObject.tag == "Enemy")
        {
            enemyAI = gameObject.GetComponent<EnemyAI>();
            speed = enemyAI.maxSpeed;
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
                health = 1;
                Debug.Log("Boss Died");
                StartCoroutine(deactivateBoss());
                GameObject.FindWithTag("Boss Controller").GetComponent<BossController>().bossKill();
                return;
            }
            if (gameObject.tag == "Enemy")
            {
                // enemy killed, increase tracker (unless in boss scene)
                if (SceneManager.GetActiveScene().buildIndex != 7)
                    ++Globals.enemiesKilled;

                print("Enemy killed. Updated death count: " + Globals.enemiesKilled);
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

    IEnumerator deactivateBoss()
    {
        yield return new WaitForSeconds(invincibiltyLength);
        this.enabled = false;
    }

    // Sets global variables for the player identified by playerNumber, marking them as killed
    public void killPlayer(int playerNumber)
    {
        Globals.players[playerNumber].Alive = false;
        Globals.players[playerNumber].GO = null;
        ++Globals.players[playerNumber].Deaths;
        --Globals.livingPlayers;
    }

    // responsible for knocking the current gameobject away from whatever hit them
    private void knockback(float x, float forceX = 20, float forceY = 10)
    {
        knockbacked = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (x < gameObject.transform.position.x)
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(forceX, forceY, 0), ForceMode2D.Impulse);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-forceX, forceY, 0), ForceMode2D.Impulse);
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
 
        enemyAI.currentMoveForce = 0;
        if (enemyAI.isRanged)
        {
           enemyAI.nextProjectileFire = enemyAI.nextProjectileFire + 1.0f;
           enemyAI.rangedBurst = 0;

        }
        yield return new WaitForSeconds(3);
        enemyAI.currentMoveForce = enemyAI.moveForce;
    }

    IEnumerator deflectEnemy()
    {

        gameObject.GetComponent<EnemyAI>().currentMoveForce = 0;



        float velocityDirection = 11* (-gameObject.GetComponent<EnemyAI>().facingRight);


        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(velocityDirection, 2, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);

        gameObject.GetComponent<EnemyAI>().currentMoveForce = gameObject.GetComponent<EnemyAI>().moveForce;

    }

    IEnumerator bossCrush(Collider2D collision)
    {
        GameObject hand = collision.GetComponent<BossSecretHands>().myHand;
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponents<Collider2D>()[0], true);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponents<Collider2D>()[1], true);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponent<PolygonCollider2D>(), true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponents<Collider2D>()[0], false);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponents<Collider2D>()[1], false);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), hand.GetComponent<PolygonCollider2D>(), false);
    }

    // determines if the current gameobject is a player or an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCollisions(collision);
        }
        if (gameObject.tag == "Player")
        {
            playerCollisions(collision);
        }
        if (gameObject.tag == "BossHead")
        {
            bossCollisions(collision);
        }
    }

    // something has collided with the player
    void playerCollisions(Collider2D collision)
    {
        if (!dashing)
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
            else if (collision.gameObject.tag == "BossAttack")
            {
                if (decreaseHealth(1.0f))
                {
                    takenDamage();
                    knockback(collision.gameObject.transform.position.x);
                }
            }
            else if (collision.gameObject.tag == "BossHead")
            {
                if (decreaseHealth(1.0f))
                {
                    takenDamage();
                    knockback(collision.gameObject.transform.position.x);
                }
            }
            else if (collision.gameObject.tag == "Fire")
            {
                if (!isAgni())
                {
                    if (decreaseHealth(1.0f))
                    {
                        takenDamage();
                        knockback(collision.gameObject.transform.position.x);
                    }
                }
            }
            else if (collision.gameObject.tag == "Water")
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 3500f, 0));
                Vector3 tempVel = gameObject.GetComponent<Rigidbody2D>().velocity;
                tempVel.y = 0;
                gameObject.GetComponent<Rigidbody2D>().velocity = tempVel;
            }
        }
        else if (dashing)
        {
             if (collision.gameObject.tag == "EnemyAttack")
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "SceneChanger")
        {
            collision.GetComponent<LoadNextScene>().LoadScene();
        }
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
                    if (Globals.players[i].ScoreCounter != null)
                    {
                        Globals.players[i].ScoreCounter.text = Globals.players[i].Score.ToString();
                    }
                }
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Secret Boss Hand")
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
            }
            StartCoroutine(bossCrush(collision));
            myBody.velocity = new Vector2(0, 0);
            float mag = (collision.gameObject.transform.localScale.x / 2) - Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x);
            knockback(collision.gameObject.transform.position.x, mag * 3, 15 + mag);
        }
    }

    bool isAgni()
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Number == gameObject.GetComponent<HeroMovement>().playerNumber)
            {
                if (Globals.players[i].Name == "Agni")
                    return true;
            }
        }
        return false;
    }

    bool isRyker()
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Number == gameObject.GetComponent<HeroMovement>().playerNumber)
            {
                if (Globals.players[i].Name == "Ryker")
                    return true;
            }
        }
        return false;
    }

    bool isDelilah()
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Number == gameObject.GetComponent<HeroMovement>().playerNumber)
            {
                if (Globals.players[i].Name == "Delilah")
                    return true;
            }
        }
        return false;
    }

    bool isKitty()
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Number == gameObject.GetComponent<HeroMovement>().playerNumber)
            {
                if (Globals.players[i].Name == "Kitty")
                    return true;
            }
        }
        return false;
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
        else if (collision.gameObject.tag == "BossAttack")
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
            StartCoroutine(stunEnemy());
        }
        //This is for rkyer dashing through enemy or any time where a character is invincible but can still attack enemies
        else if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<AttributeController>().dashing) { 
                if (decreaseHealth(1.0f))
                 {
                    takenDamage();
                    knockback(collision.gameObject.transform.position.x);
                 }
         }
        }
        else if (collision.gameObject.tag == "Deflect")
        {
            StartCoroutine(deflectEnemy());
        }
        //else if (collision.gameObject.tag == "Secret Boss Hand")
        //{
        //    //if (decreaseHealth(1.0f))
        //    //{
        //    //    takenDamage();
        //    //}
        //    StartCoroutine(bossCrush(collision));
        //    myBody.velocity = new Vector2(0, 0);
        //    float mag = (collision.gameObject.transform.localScale.x / 2) - Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x);
        //    knockback(collision.gameObject.transform.position.x, mag * 3, 15 + mag);
        //}
    }

    // something has collided with the boss head
    void bossCollisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            return;
        }
        else if (collision.gameObject.tag == "BossAttack")
        {
            return;
        }
        else if (collision.gameObject.tag == "LittleAttack")
        {
            if (decreaseHealth(2.0f))
            {
                takenDamage();
            }
        }
        else if (collision.gameObject.tag == "BigAttack")
        {
            if (decreaseHealth(3.0f))
            {
                takenDamage();
            }
        }
        else if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<AttributeController>().dashing)
        {
            if (decreaseHealth(1.0f))
            {
                takenDamage();
            }
        }
    }
}
