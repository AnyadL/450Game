/************************************************************
 * Author: Michael Morris
 * File: EnemyAI.cs
 * 
 * Credits:
 * http://www.devination.com/2015/07/unity-2d-platformer-tutorial-part-4.html
 * (Unity 2D Platformer Tutorial - Part 4 - Enemy Movement)
 ***********************************************************/
using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

    // Cache vars
    private Rigidbody2D myBody;
    private AttributeController myAttributes;

    public Transform groundCheck;
    public Transform wallCheck;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    private bool isGrounded = true;
    private bool isBlocked = false;
    public int facingRight = 1;
    public float projectileVelocity = 20;
    public float nextProjectileFire;
    public float projectileCooldown = 0.3f;
    public GameObject projectilePrefab;

    void Start() {
        myBody = this.GetComponent<Rigidbody2D>();
        myAttributes = this.GetComponent<AttributeController>(); ;
    }

    void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        isBlocked = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (myAttributes.isRanged == 1)
        {
            //Fire little fireballs
            if (Time.time >= nextProjectileFire)
            {
                nextProjectileFire = Time.time + projectileCooldown;
                RangedAttack();
            }
        }
    }

    // Great for physics updates, use FixedUpdate instead of Update!
    void FixedUpdate()
    {
        // If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked) {
            Flip();
        }

        if (moveForce == 0)
        {
            return;
        }

        if (facingRight * myBody.velocity.x < maxSpeed)
        {
            myBody.AddForce(Vector2.right * facingRight * moveForce);
        }

        if (Mathf.Abs(myBody.velocity.x) > maxSpeed)
        {
            myBody.velocity = new Vector2(Mathf.Sign(myBody.velocity.x) * maxSpeed, myBody.velocity.y);
        }
    }

    // Reverse enemy movement and facing direction
    public void Flip()
    {
        // Flip the sprite by multiplying the x component of localScale by -1.
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;

        facingRight = -facingRight;
    }

    public void RangedAttack()
    {
        //Checks the direction and sets the bullet velocity to that direction
        float velocityDirection = projectileVelocity;

        velocityDirection = velocityDirection * facingRight;

        //Creates the bullet and makes it move
        GameObject newBullet = Instantiate(projectilePrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
        newBullet.tag = "EnemyAttack";
        newBullet.transform.rotation = gameObject.transform.rotation;
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
    }
}
