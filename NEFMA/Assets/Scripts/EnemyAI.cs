/************************************************************
 * Author: Michael Morris
 * File: EnemyAI.cs
 * 
 * Credits:
 * http://www.devination.com/2015/07/unity-2d-platformer-tutorial-part-4.html
 * (Unity 2D Platformer Tutorial - Part 4 - Enemy Movement)
 ***********************************************************/
using System.Collections;
using System.Collections.Generic;
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
    public float projectileCooldown = 3.0f;
    public float projectileBurstCooldown = 0.3f;
    public GameObject projectilePrefab;
    public bool ghostOverride = false;
    private GameObject target;
    public float xRange = 0f;
    public float yRange = 0f;
    private List<GameObject> targets = new List<GameObject>();
    private int rangedBurst = 0;
    public int numberRangedBurst = 3;

    public GameObject bullseye = null;

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

        if (isRanged && gameObject.GetComponent<Renderer>().isVisible)
        {
            Debug.DrawLine(transform.position + (Vector3.right * xRange) + (Vector3.up * yRange), transform.position - (Vector3.right * xRange) + (Vector3.up * yRange), Color.red, 0.1f);
            Debug.DrawLine(transform.position + (Vector3.right * xRange) + (Vector3.up * yRange), transform.position + (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
            Debug.DrawLine(transform.position - (Vector3.right * xRange) + (Vector3.up * yRange), transform.position - (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
            Debug.DrawLine(transform.position + (Vector3.right * xRange) - (Vector3.up * yRange), transform.position - (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
            if (Time.time >= nextProjectileFire)
            {
                if (rangedBurst == numberRangedBurst)
                {
                    rangedBurst = 0;
                    nextProjectileFire = Time.time + projectileCooldown;
                }
                else
                {
                    rangedBurst++;
                    nextProjectileFire = Time.time + projectileBurstCooldown;
                }
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
        for (int i = 0; i < Globals.players.Count; i++)
        {
            if (Globals.players[i].Alive)
            {
                targeter(Globals.players[i].GO);
            }
        }

        if (targets.Count > 0)
        {
            choose();
            //Debug.Log("Target: " + target);
            float tempx = (transform.position.x - target.transform.position.x);
            float tempy = (transform.position.y - target.transform.position.y);
            //Debug.Log("X: " + tempx);
            //Debug.Log("Y: " + tempy);

            //Checks the direction and sets the bullet velocity to that direction
            float velocityDirection = projectileVelocity;

            velocityDirection = velocityDirection * facingRight;

            float angle = Mathf.Atan(tempy / tempx);
            //Debug.Log("AngleD: " + Mathf.Rad2Deg* angle);
            //Debug.Log("AngleR: " + angle);
            float xcomp = Mathf.Cos(angle) * velocityDirection;
            float ycomp = Mathf.Sin(angle) * velocityDirection;
            //Debug.Log("xcomp: " + xcomp);
            //Debug.Log("ycomp: " + ycomp);

            //Creates the bullet and makes it move
            GameObject newBullet = Instantiate(projectilePrefab, wallCheck.position, Quaternion.identity) as GameObject;
            newBullet.tag = "EnemyAttack";
            //newBullet.transform.rotation = gameObject.transform.rotation;
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xcomp, ycomp);
        }
        else if (bullseye)
        {
            float tempx = (transform.position.x - bullseye.transform.position.x);
            float tempy = (transform.position.y - bullseye.transform.position.y);

            //Checks the direction and sets the bullet velocity to that direction
            float velocityDirection = projectileVelocity;

            velocityDirection = velocityDirection * facingRight;

            float angle = Mathf.Atan(tempy / tempx);
            float xcomp = Mathf.Cos(angle) * velocityDirection;
            float ycomp = Mathf.Sin(angle) * velocityDirection;

            //Creates the bullet and makes it move
            GameObject newBullet = Instantiate(projectilePrefab, wallCheck.position, Quaternion.identity) as GameObject;
            newBullet.tag = "EnemyAttack";
            
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(xcomp, ycomp);
        }
    }

    void targeter(GameObject target)
    {
        float tx = target.transform.position.x;
        float ty = target.transform.position.y;
        float mx = gameObject.transform.position.x;
        float my = gameObject.transform.position.y;
        if (Mathf.Abs(mx - tx) <= xRange && Mathf.Abs(my - ty) <= yRange)
        {
            if (lineOfSight(target.transform))
            {
                targets.Add(target);
            }
        }
    }

    bool lineOfSight(Transform target)
    {
        Debug.DrawLine(wallCheck.position, target.position, Color.red, 0.5f);
        RaycastHit2D hit = Physics2D.Linecast(wallCheck.position, target.position);
        Debug.DrawLine(crushedCheck.position, target.position, Color.red, 0.5f);
        RaycastHit2D hit2 = Physics2D.Linecast(crushedCheck.position, target.position);
        if (hit.collider.transform == target || hit2.collider.transform == target)
        {
            return true;
        }
        return false;
    }

    void choose()
    {
        int choice = Random.Range(0, targets.Count);
        target = targets[choice];
        targets.Clear();

        if (((target.transform.position.x > transform.position.x) && facingRight == -1) || ((target.transform.position.x < transform.position.x) && facingRight == 1))
        {
            Flip();
        }
    }
}
