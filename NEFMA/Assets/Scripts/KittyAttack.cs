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
    public float scratchCooldown = 5.0f;
    public float nextScratch;

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
            if (Input.GetButtonDown("Fire1_"+hm.inputNumber))
            {
                nextScratch = Time.time + scratchCooldown;
                ClawAttack();
            }
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_"+hm.inputNumber))
            {
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                Hiss();
            }
        }
    }

    // Creates a Hiss that stuns enemies
    void Hiss()
    {
        GameObject Hiss = Instantiate(HissPrefab, (transform.position + (transform.forward / 10)), Quaternion.identity) as GameObject;
        Hiss.GetComponent<HissScript>().owner = gameObject;
    }

    //Creates claw attack which persists for a second and then disappears
    void ClawAttack()
    {
        GameObject ClawMarks = Instantiate(ClawPrefab, (transform.position + (transform.forward /10)), Quaternion.identity) as GameObject;
        if (ClawMarks)
            return;
    }
}

