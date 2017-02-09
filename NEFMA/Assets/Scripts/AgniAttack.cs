using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgniAttack : MonoBehaviour
{
    public GameObject littleBulletPrefab;
    public GameObject bigBulletPrefab;
    
    public float littleBulletVelocity = 20;
    public float bigBulletVelocity = 10;


    private HeroMovement hm;
    public float littleCooldown = 0.3f;
    public float nextLittleFire;

    public float bigCooldown = 10f;
    public float nextBigFire;

    // Use this for initialization
    void Start()
    {

        //In order to figure out which way the character is facing I need to access the HeroMovement script
        hm = gameObject.GetComponent<HeroMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextLittleFire)
        {
            //Fire little fireballs
            if (Input.GetButtonDown("Fire1"))
            {
                nextLittleFire = Time.time + littleCooldown;
                RegularFire();
            }
        }

        if(Time.time >= nextBigFire) { 
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2"))
            {
                nextBigFire = Time.time + bigCooldown;
                BigFire();
            }
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
        GameObject newBullet = Instantiate(littleBulletPrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
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

        GameObject newBullet = Instantiate(bigBulletPrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
    }

}

