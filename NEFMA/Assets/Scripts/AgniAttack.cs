using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgniAttack : MonoBehaviour
{
    public GameObject littleBulletPrefab;
    public GameObject bigBulletPrefab;
    
    public float littleBulletVelocity = 20;
    public float bigBulletVelocity = 10;

    private string playerNumber;

    private HeroMovement hm;
    private AttributeController myAttribute;
    public float littleCooldown = 0.3f;
    public float nextLittleFire;
    [HideInInspector]public bool BigAttacking = false;
    [HideInInspector]public bool LittleAttacking = false; 
    public Animator animator;

    public AudioSource sfxSmallFireBall;
    public AudioSource sfxBigFireBall;

    // Use this for initialization
    void Start()
    {
        //In order to figure out which way the character is facing I need to access the HeroMovement script
        hm = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
        BigAttacking = false;
        LittleAttacking = false;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextLittleFire)
        {
            //Fire little fireballs
            if (Input.GetButtonDown("Fire1_" + hm.inputNumber) && !Globals.gamePaused)
            {
                LittleAttacking = true;
                animator.SetBool("LittleAttacking", LittleAttacking);
                nextLittleFire = Time.time + littleCooldown;
                RegularFire();
                sfxSmallFireBall.pitch = Random.Range(1.0f, 1.5f);
                sfxSmallFireBall.Play();
            }
            else
            {
                LittleAttacking = false;
                animator.SetBool("LittleAttacking", LittleAttacking);
            }
        }

        if(Time.time >= myAttribute.nextBigFire) { 
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_" + hm.inputNumber) && !Globals.gamePaused)
            {
                BigAttacking = true;
                animator.SetBool("BigAttacking", BigAttacking);
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                BigFire();
                sfxBigFireBall.Play();
            }

        }
        else
        {
            BigAttacking = false;
            animator.SetBool("BigAttacking", BigAttacking);
        }

    }

    // Fire a bullet
    void RegularFire()
    {


        //Checks the direction and sets the bullet velocity to that direction
        float velocityDirection = littleBulletVelocity;

        if (!hm.facingRight)
        {
            velocityDirection = -velocityDirection;
        }

        //Creates the bullet and makes it move
        GameObject newBullet = Instantiate(littleBulletPrefab, (transform.position - (transform.up)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
    }

    //Does the same as RegularFire except with big fireballs
    void BigFire()
    {
        float velocityDirection = bigBulletVelocity;

        if (!hm.facingRight)
        {
            velocityDirection = -velocityDirection;
        }

        GameObject newBullet = Instantiate(bigBulletPrefab, (transform.position - (transform.up)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 30);
    }
}

