using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelilahAttack : MonoBehaviour {

    public GameObject wallPrefab;
    [HideInInspector] public GameObject wall;

    public float wallVelocity = 10;
    public float wallLifeTime = 5;

    private HeroMovement myMovement;
    private AttributeController myAttribute;
    public float littleCooldown = 0.5f;
    [HideInInspector] public float nextLittleFire;
    [HideInInspector] public bool hasWall = false;
    [HideInInspector] public int wallRight;

    // Use this for initialization
    void Start()
    {
        myMovement = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWall && !myMovement.grounded)
        {
            destroyWall();
            Destroy(wall);
        }
        if ((Input.GetButtonDown("Fire1_" + myMovement.playerNumber)) && (Time.time >= nextLittleFire) && (Time.time >= myAttribute.nextBigFire) && myMovement.grounded)
        {
            nextLittleFire = Time.time + littleCooldown;
            RegularFire();
        }

        if (Input.GetButtonDown("Fire2_" + myMovement.playerNumber) && hasWall)
        {
            myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
            BigFire();
        }
    }

    // Fire a bullet
    void RegularFire()
    {
        if (hasWall)
        {
            destroyWall();
            Destroy(wall);
        }
        else
        {
            createWall();
            wallRight = myMovement.facingRight? 1 : -1;
            wall = Instantiate(wallPrefab, (transform.position + new Vector3(6 * wallRight, 1,0)), Quaternion.identity);
            wall.GetComponent<DelilahWall>().owner = gameObject;
            wall.GetComponent<DelilahWall>().wallRight = wallRight;
        }
    }

    //Does the same as RegularFire except with big fireballs
    void BigFire()
    {
        //float velocityDirection = wallVelocity;

        //if (!myMovement.facingRight)
        //{
        //    velocityDirection = -velocityDirection;
        //}

        //GameObject newBullet = Instantiate(bigBulletPrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
        //newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        //newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityDirection, 0);
        destroyWall();

        wall.GetComponent<Rigidbody2D>().velocity = new Vector2(wallVelocity * wallRight, 0);
        wall.GetComponent<DelilahWall>().free = true;
        wall.GetComponent<DelilahWall>().timeToDie = Time.time + wallLifeTime;
    }

    public void destroyWall()
    {
        hasWall = false;
        myMovement.currentMaxSpeed = myMovement.maxSpeed;
    }

    public void createWall()
    {
        hasWall = true;
        myMovement.currentMaxSpeed = myMovement.maxSpeed / 3;
    }
}
