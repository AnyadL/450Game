using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelilahAttack : MonoBehaviour {

    public GameObject wallPrefab;
    public GameObject fistPrefab;
    public float fistVelocity;
    [HideInInspector] public GameObject wall;

    public float wallVelocity = 10;
    public float wallLifeTime = 5;

    private HeroMovement myMovement;
    private AttributeController myAttribute;
    public float littleCooldown = 3.0f;
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
        if(!hasWall)
        {
            myMovement.currentMaxSpeed = myMovement.maxSpeed;
        }
        if (hasWall && !myMovement.grounded)
        {
            destroyWall();
            Destroy(wall);

        }
        if ((Input.GetButtonDown("Fire1_" + myMovement.inputNumber)) && (Time.time >= nextLittleFire) && !Globals.gamePaused)
        {
            nextLittleFire = Time.time + littleCooldown;
            RegularFire();
        }

        if (Input.GetButtonDown("Fire2_" + myMovement.inputNumber)&& (Time.time >= myAttribute.nextBigFire) && !Globals.gamePaused)
        {
            myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
            BigFire();
        }
    }

    // Fire a bullet
    void RegularFire()
    {
        wallRight = myMovement.facingRight ? 1 : -1;
        GameObject newBullet = Instantiate(fistPrefab, (transform.position +  new Vector3(4 *wallRight, 4, 0)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -25);

        if (myMovement.facingRight)
        {
            Vector3 theScale = newBullet.transform.localScale;
            theScale.x *= -1;
            newBullet.transform.localScale = theScale;
        }
    }

    //Does the same as RegularFire except with big fireballs
    void BigFire()
    {
        if (hasWall)
        {
            destroyWall();
            Destroy(wall);
        }
        else
        {
            createWall();
            wallRight = myMovement.facingRight ? 1 : -1;
            wall = Instantiate(wallPrefab, (transform.position + new Vector3(6 * wallRight, 1, 0)), Quaternion.identity);
            wall.GetComponent<DelilahWall>().owner = gameObject;
            wall.GetComponent<DelilahWall>().wallRight = wallRight;
        }
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
