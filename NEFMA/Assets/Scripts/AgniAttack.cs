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

    float attackSpeed = 2f;
    float cooldown;

    // Use this for initialization
    void Start()
    {
        hm = gameObject.GetComponent<HeroMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= cooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RegularFire();
                Debug.Log("Shoot");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                BigFire();
            }
        }
    }
    // Fire a bullet
    void RegularFire()
    {
        float velocityDirection = littleBulletVelocity;

        if (!hm.facingRight)
        {
            velocityDirection = -velocityDirection;
        }
        GameObject newBullet = Instantiate(littleBulletPrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
    }
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

