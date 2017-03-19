﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyAttack : MonoBehaviour
{
    public GameObject ClawPrefab;
    public GameObject HissPrefab;

    private string playerNumber;

    private HeroMovement hm;
    private AttributeController myAttribute;
    public float scratchCooldown = 5.0f;
    public float nextScratch;
    public float hissVelocity;

    // Use this for initialization
    void Start()
    {
        //In order to figure out which way the character is facing I need to access the HeroMovement script
        hm = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextScratch)
        {
            //Fire little fireballs
            if (Input.GetButtonDown("Fire1_"+hm.inputNumber) && !Globals.gamePaused)
            {
                nextScratch = Time.time + scratchCooldown;
                ClawAttack();
            }
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_"+hm.inputNumber) && !Globals.gamePaused)
            {
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                Hiss();
            }
        }
    }

    // Creates a Hiss that stuns enemies
    void Hiss()
    {
        float velocityDirection = hissVelocity;

        GameObject Hiss = Instantiate(HissPrefab, (transform.position), Quaternion.identity) as GameObject;
        Hiss.GetComponent<HissScript>().owner = gameObject;
        Hiss.transform.rotation = gameObject.transform.rotation;
        if (!hm.facingRight)
        {
            velocityDirection = -velocityDirection;
            Hiss.GetComponent<SpriteRenderer>().flipX = true;
        }
 
        Hiss.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);


    }

    //Creates claw attack which persists for a second and then disappears
    void ClawAttack()
    {
        float velocityDirection=1;
        if (!hm.facingRight)
        {
            velocityDirection = -1;
        }
        GameObject ClawMarks = Instantiate(ClawPrefab, (transform.position + velocityDirection*(transform.forward /10)), Quaternion.identity) as GameObject;
        ClawMarks.GetComponent<ClawScript>().owner = gameObject;
        ClawMarks.GetComponent<ClawScript>().velocityDirection = velocityDirection;
        if (hm.facingRight)
        {
            Vector3 theScale = ClawMarks.transform.localScale;
            theScale.x *= -1;
            ClawMarks.transform.localScale = theScale;
        }

    }
}

