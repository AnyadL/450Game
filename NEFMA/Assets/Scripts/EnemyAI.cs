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

    public LayerMask enemyMask;
    public Vector2 myDirection;

    public float speed = 300f;
    public ForceMode2D forceMode = ForceMode2D.Force;

    // Cache vars
    private Rigidbody2D myBody;
    private float myWidth;
    private float myHeight;


    void Start() {
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }

    // Great for physics updates, use FixedUpdate instead of Update!
    void FixedUpdate()
    {
        //Use this position to cast the isGrounded/isBlocked lines from
        Vector2 lineCastPos = toVector2(transform.position) - toVector2(transform.right) * myWidth + Vector2.up * myHeight;
        
        // Check to see if there's ground in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.Log("Current Pos: " + transform.position);
        Debug.Log("LineCastPos: " + lineCastPos);
        Debug.Log("Grounded: " + isGrounded);

        // Check to see if there's a wall in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos - toVector2(transform.right) * .05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - toVector2(transform.right) * .05f, enemyMask);
        Debug.Log("Obstructed: " + isGrounded);

        // If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked) {
            Flip();
        }

        // Always move forward
        myDirection *= speed * Time.fixedDeltaTime;
        myDirection.Normalize();
        myBody.AddForce(myDirection, forceMode);
    }

    // Reverse enemy movement and facing direction
    public void Flip()
    {
        myDirection *= -1;

        // Flip the sprite by multiplying the x component of localScale by -1.
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
    }

    public static Vector2 toVector2(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

}
