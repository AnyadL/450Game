using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyAttack : MonoBehaviour
{
    public GameObject ClawPrefab;
    public GameObject HissPrefab;

    private string playerNumber;

    private HeroMovement hm;
    private AttributeController myAttribute;
    public float hissCooldown = 3.0f;
    public float nextHiss;

    // Use this for initialization
    void Start()
    {
        //In order to figure out which way the character is facing I need to access the HeroMovement script
        hm = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
        if (hm)
            return;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHiss)
        {
            //Fire little fireballs
            if (Input.GetButtonDown("Fire1_0"))
            {
                nextHiss = Time.time + hissCooldown;
                Hiss();
            }
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_0"))
            {
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                ClawAttack();
            }
        }
    }

    // Creates a Hiss that stuns enemies
    void Hiss()
    {
        GameObject Hiss = Instantiate(HissPrefab, (transform.position + (transform.forward / 10)), Quaternion.identity) as GameObject;
        if (Hiss)
            return;
    }

    //Creates claw attack which persists for a second and then disappears
    void ClawAttack()
    {
 
        GameObject ClawMarks = Instantiate(ClawPrefab, (transform.position + (transform.forward /10)), Quaternion.identity) as GameObject;
        if (ClawMarks)
            return;
    }
}

