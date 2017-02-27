using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RykerAttack : MonoBehaviour {

    public GameObject stunPrefab;

    private string playerNumber;

    private HeroMovement hm;
    private AttributeController myAttribute;
    public float dashCooldown = 3.0f;
    public float nextDash;

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
        if (Time.time >= nextDash)
        {
            //Fire little fireballs
            if (Input.GetButtonDown("Fire1_0"))
            {
                nextDash = Time.time + dashCooldown;
                Dash();
            }
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_0"))
            {
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                StunAttack();
            }
        }
    }

    // Creates a Hiss that stuns enemies
    void Dash()
    {
       // GameObject Hiss = Instantiate(HissPrefab, (transform.position + (transform.forward / 10)), Quaternion.identity) as GameObject;

    }

    //Creates claw attack which persists for a second and then disappears
    void StunAttack()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject go in gos)
        {
            GameObject stunObject = Instantiate(stunPrefab, (go.transform.position), Quaternion.identity) as GameObject;
        }
    }
}
