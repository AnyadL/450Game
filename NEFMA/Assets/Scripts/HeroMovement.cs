using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool attack = false;
    [HideInInspector]public bool inRange = false;

    public string playerNumber;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

    private bool grounded = false;
    //private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump_"+playerNumber) && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            attack = true;
        }

    }

    //Does the actions during the frame the hero has to do, in this case jumping, flipping and attacking
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal_"+playerNumber);

        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            //anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            jump = false;
        }
        if (attack && inRange)
        {
            Debug.Log("attack");
            attack = false;
        }
    }

    //Check if within hitting range of an enemy, enemies should have 2 colliders
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            inRange = true;
        }
    }

    //flip the sprite in different directions when switching
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
