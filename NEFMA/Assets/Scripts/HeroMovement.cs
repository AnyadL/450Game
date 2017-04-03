using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    [HideInInspector] public bool inRange = false;

    public int playerNumber = 0;
    public int inputNumber = -1;

    public float moveForce = 400f;
    [HideInInspector] public float currentMoveForce;
    public float maxSpeed = 12f;
    [HideInInspector] public float currentMaxSpeed;
    public float jumpForce = 1800f;
    public Transform groundCheck;
    //public Transform punchCheck; // TODO: Remove punch check from hero prefabs

    [HideInInspector] public bool grounded = false;
    //private Animator anim;
    private Rigidbody2D rb2d;
    private AttributeController myAttributes;
    [HideInInspector] public float currentSpeed;
    [HideInInspector] public bool doublejump = false;
    private bool candoublejump = false;
    private bool isKitty = false;
    private int jumpCount = 0;

    // Use this for initialization
    void Awake()
    {
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        myAttributes = GetComponent<AttributeController>();
        currentMoveForce = moveForce;
        currentMaxSpeed = maxSpeed;
        if (gameObject.name == "Kitty(Clone)" || gameObject.name == "Kitty")
        {
            isKitty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        if (grounded)
        {
            jumpCount = 0;
        }
        if (myAttributes.knockbacked && grounded && Time.time + myAttributes.invincibiltyLength - 0.25f >= myAttributes.nextVulnerable)
        {
            myAttributes.knockbacked = false;
        }

        if (Input.GetButtonDown("Jump_" + inputNumber) && !Globals.gamePaused)
        {
            if (grounded && !myAttributes.knockbacked)
            {
                jump = true;
                jumpCount = 1;
            }
            else if (candoublejump && isKitty && !myAttributes.knockbacked)
            {
                doublejump = true;
                jumpCount = 2;
            }
            else if (isKitty && jumpCount == 0)
            {
                doublejump = true;
                jumpCount = 2;
            }

        }

        //Debug.Log(rb2d.velocity);
      
        //if (Input.GetButtonDown("Fire3_" + inputNumber) && !Globals.gamePaused)
        //{
        //    attack = true;
        //}
    }

    //Does the actions during the frame the hero has to do, in this case jumping, flipping and attacking
    void FixedUpdate()
    {
        //Physics2D.IgnoreLayerCollision(8, 11, (rb2d.velocity.y > 0.0f)); // Doesnt work for multiplayer
        currentSpeed = Input.GetAxisRaw("Horizontal_" + inputNumber);

        //anim.SetFloat("Speed", Mathf.Abs(h));

        //if (!myAttributes.knockbacked && currentSpeed * rb2d.velocity.x < currentMaxSpeed)
        //    rb2d.AddForce(Vector2.right * currentSpeed * currentMoveForce);

        if (currentSpeed != 0 && !Globals.gamePaused)
        {
            moveCharacter(currentSpeed, currentMoveForce);

            if (Mathf.Abs(rb2d.velocity.x) > currentMaxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * currentMaxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.y <= -95f && !Globals.gamePaused)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -95f);
        }

        //if (currentSpeed > 0 && !facingRight)
        //    Flip();
        //else if (currentSpeed < 0 && facingRight)
        //    Flip();

        if (jump)
        {
            //anim.SetTrigger("Jump");
            //rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            jumpCharacter(0f, jumpForce);
            //rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            candoublejump = true;
   
        }
        if (doublejump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            jumpCharacter(0f, jumpForce);
            //rb2d.AddForce(new Vector2(0f, jumpForce));
            doublejump = false;
            candoublejump = false;
        }
        //punchRange = Physics2D.Linecast(transform.position, punchCheck.position, 1 << LayerMask.NameToLayer("Enemy"));
        //if (attack && punchRange)
        //{
        //    Debug.Log("attack");
        //    attack = false;
        //    StartCoroutine(Punch());
        //}
    }

    //IEnumerator Punch()
    //{
    //    Debug.Log("Punch");
    //    gameObject.layer = 14;
    //    yield return new WaitForSeconds(0.02f);
    //    gameObject.layer = myLayer;
    //}

    // speedVector is a value between [-1, 1]
    public void moveCharacter(float speedVector, float force)
    {
        if (!myAttributes.knockbacked && speedVector * rb2d.velocity.x < currentMaxSpeed)
            rb2d.AddForce(Vector2.right * speedVector * force);

        if (speedVector > 0 && !facingRight)
            Flip();
        else if (speedVector < 0 && facingRight)
            Flip();
    }

    public void jumpCharacter(float xForce, float yForce)
    {
        rb2d.AddForce(new Vector2(xForce, yForce));
    }

    //Check if within hitting range of an enemy, enemies should have 2 colliders
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        inRange = true;
    //    }
    //}

    //flip the sprite in different directions when switching
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
