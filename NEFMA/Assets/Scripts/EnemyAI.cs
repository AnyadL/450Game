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
[RequireComponent(typeof(AttributeController))]
public class EnemyAI : MonoBehaviour {

    // Cache vars
    private Rigidbody2D myBody;
    private AttributeController myAttributes;

    public Transform groundCheck;
    public Transform wallCheck;
    public Transform crushedCheck;
    public float moveForce = 365f;
    [HideInInspector] public float currentMoveForce;
    public float maxSpeed = 5f;
    [HideInInspector] private bool isGrounded = true;
    [HideInInspector] private bool isBlocked = false;
    [HideInInspector] public int facingRight = -1;
    public bool isRanged = false;
    public float projectileVelocity = 20;
    public float nextProjectileFire = 0;
    public float projectileCooldown = 0.3f;
    public GameObject projectilePrefab;
    public bool ghostOverride = false;

    void Start() {
        myBody = this.GetComponent<Rigidbody2D>();
        myAttributes = this.GetComponent<AttributeController>();
        currentMoveForce = moveForce;

        // dont need anymore?
        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            facingRight *= -1;
        }
    }

    void Update()
    {
        //if (!gameObject.GetComponent<Renderer>().isVisible)
        //{
        //    return;
        //}
        isBlocked = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (isBlocked)
        {      
            bool crushed = Physics2D.Linecast(transform.position, crushedCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            if (crushed)
            {
                myAttributes.decreaseHealth(myAttributes.maxHealth);
            }
            else
            {
                if (!ghostOverride)
                {
                    Flip();
                }
            }
        }

        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        // If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isBlocked && !isGrounded && !ghostOverride)
        {
            Flip();
        }

        if (isRanged)
        {
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
        //if (!gameObject.GetComponent<Renderer>().isVisible)
        //{
        //    return;
        //}
        if (currentMoveForce == 0)
        {
            return;
        }

        if (facingRight * myBody.velocity.x < maxSpeed)
        {
            myBody.AddForce(Vector2.right * facingRight * currentMoveForce);
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
