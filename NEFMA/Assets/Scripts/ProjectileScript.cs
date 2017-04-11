using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script so far just makes the fireballs disappear when they go off screen, more will probably be added later
public class ProjectileScript : MonoBehaviour {

    public AudioSource sfxSmallExplosion;
    public AudioSource sfxBigExplosion;

	// Use this for initialization
	void Start () {
 
    }

    private void Update()
    {
        if (!gameObject.GetComponent<Renderer>().isVisible && gameObject.tag!="BigAttack")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.tag != "BigAttack")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Deflect")
        {
            Vector2 oldVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = -oldVelocity;
            gameObject.tag = "LittleAttack";
        }
        if (collision.gameObject.tag == "DeathLine")
        {
            Destroy(gameObject);
        }
        if (gameObject.tag == "BossAttack")
        {
            return;
        }
        if (collision.gameObject.tag == "Platform")
        {
            if (gameObject.tag != "BigAttack")
            {
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject.tag == "LittleAttack")
            {          
                if (sfxSmallExplosion != null && !sfxSmallExplosion.isPlaying)
                {
                    sfxSmallExplosion.pitch = Random.Range(0.9f, 1.3f);
                    sfxSmallExplosion.Play();
                }
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
    }
}
