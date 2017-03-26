using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelilahWall : MonoBehaviour {

    [HideInInspector] public GameObject owner;
    [HideInInspector] public int wallRight;
    [HideInInspector] public bool free = false;
    public float shieldTime;
    public int health = 30;
   

    // Use this for initialization
    void Start () {
        StartCoroutine(AttackTime());
    }

    // Update is called once per frame
    void Update ()
    {
        if (health <= 0)
        {
            owner.GetComponent<DelilahAttack>().destroyWall();
            Destroy(gameObject);
        }
	}

    private void FixedUpdate()
    {
        if (!free)
        {
            transform.position = owner.transform.position + new Vector3(4 * wallRight, 1.8f, 0);
        }
        else{
            Debug.Log("free");
            float velocityDirection = owner.GetComponent<DelilahAttack>().wallVelocity;
            if (!owner.GetComponent<HeroMovement>().facingRight)
            {
                velocityDirection = -velocityDirection;
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health = health - 1;
        }
        else if (collision.gameObject.tag == "EnemyAttack")
        {
            health = health - 3;
        }
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(shieldTime);
        owner.GetComponent<DelilahAttack>().destroyWall();
// var exp = GetComponent<ParticleSystem>();
// exp.Play();
        Destroy(gameObject);
    }
}
