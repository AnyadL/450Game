using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool punchRange = false;
    [HideInInspector] public bool attack = false;

    [HideInInspector] public bool inRange = false;

    public int playerNumber = 0;
    public int inputNumber = -1;

    public float moveForce = 400f;
    [HideInInspector] public float currentMoveForce;
    public float maxSpeed = 12f;
    [HideInInspector] public float currentMaxSpeed;
    public float jumpForce = 1800f;
    public Transform groundCheck;
   // public Transform punchCheck;

    [HideInInspector] public bool grounded = false;
    //private Animator anim;
    private Rigidbody2D rb2d;
    private AttributeController myAttributes;
    
    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        myAttributes = GetComponent<AttributeController>();
        currentMoveForce = moveForce;
        currentMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (myAttributes.knockbacked && grounded && Time.time + myAttributes.invincibiltyLength - 0.25f >= myAttributes.nextVulnerable)
        {
            myAttributes.knockbacked = false;
        }

        if (Input.GetButtonDown("Jump_" + inputNumber) && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            attack = true;
        }

    }

    //Does the actions during the frame the hero has to do, in this case jumping, flipping and attacking
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal_" + inputNumber);

        //anim.SetFloat("Speed", Mathf.Abs(h));

        if (!myAttributes.knockbacked && h * rb2d.velocity.x < currentMaxSpeed)
            rb2d.AddForce(Vector2.right * h * currentMoveForce);

        if (Mathf.Abs(rb2d.velocity.x) > currentMaxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * currentMaxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            //anim.SetTrigger("Jump");
            //rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

       // punchRange = Physics2D.Linecast(transform.position, punchCheck.position, 1 << LayerMask.NameToLayer("Enemy"));
        if (attack && punchRange)
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
