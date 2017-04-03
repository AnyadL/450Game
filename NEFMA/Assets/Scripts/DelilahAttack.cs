using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelilahAttack : MonoBehaviour {

    public GameObject wallPrefab;
    public GameObject fistPrefab;
    public float fistVelocity;
    [HideInInspector] public GameObject wall;

    public float wallVelocity = 100;
    public float wallLifeTime = 5;
    public float pushTime = 2;
    //public ParticleSystem explosion;

    public Animator animator;
    [HideInInspector] public bool BigAttack = false;
    [HideInInspector] public bool LittleAttack = false;

    private HeroMovement myMovement;
    private AttributeController myAttribute;
    public float littleCooldown = 3.0f;
    [HideInInspector] public float nextLittleFire;
    [HideInInspector] public bool hasWall = false;
    [HideInInspector] public int wallRight;
    [HideInInspector] public bool wallFaceRight = false;

    public AudioSource sfxWallUp;
    public AudioSource sfxWallPush;
    public AudioSource sfxFist;

    // Use this for initialization
    void Start()
    {
        myMovement = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
        hasWall = false;
        BigAttack = false;
        LittleAttack = false;
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        /*if (hasWall && !myMovement.grounded)
        {
            destroyWall();
            Destroy(wall);

        }*/
        if (Time.time >= nextLittleFire)
        {
            if (Input.GetButtonDown("Fire1_" + myMovement.inputNumber) && !Globals.gamePaused)
            {
                LittleAttack = true;
                animator.SetBool("AttackLittle", LittleAttack);
                nextLittleFire = Time.time + littleCooldown;
                RegularFire();
            }
        }
        else
        {
            LittleAttack = false;
            animator.SetBool("AttackLittle", LittleAttack);
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Fire Big Fireballs
            if (Input.GetButtonDown("Fire2_" + myMovement.inputNumber) && !Globals.gamePaused)
            {
                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                MakeWall();
            }
        }
        else if (wall)
        {
            if (Input.GetButtonDown("Fire2_" + myMovement.inputNumber) && !Globals.gamePaused)
            {
                BigAttack = true;
                animator.SetBool("AttackBig", BigAttack);
                PushWall();
            }
        }
        else
        {
            BigAttack = false;
            animator.SetBool("AttackBig", BigAttack);
        }
    }

    // Fire a bullet
    void RegularFire()
    {
        wallRight = myMovement.facingRight ? 1 : -1;
        GameObject newBullet = Instantiate(fistPrefab, (transform.position +  new Vector3(7 *wallRight, 4, 0)), Quaternion.identity) as GameObject;
        newBullet.transform.rotation = gameObject.transform.rotation; //Rotate the same direction as the ship it is fired from
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -25);

        if (myMovement.facingRight)
        {
            Vector3 theScale = newBullet.transform.localScale;
            theScale.x *= -1;
            newBullet.transform.localScale = theScale;
        }
        wallFaceRight = myMovement.facingRight;

        if (sfxFist != null && !sfxFist.isPlaying)
        {
            sfxFist.pitch = Random.Range(1.0f, 1.5f);
            sfxFist.Play();
        }


    }

//Does the same as RegularFire except with big fireballs
void MakeWall()
    {
        Debug.Log("Big Fire");
            createWall();
            wallRight = myMovement.facingRight ? 1 : -1;

            wall = Instantiate(wallPrefab, (transform.position + new Vector3(4* wallRight, 1.8f, 0)), Quaternion.identity);
            wall.GetComponent<DelilahWall>().owner = gameObject;
            wall.GetComponent<DelilahWall>().wallRight = wallRight;

            if (sfxWallUp != null && !sfxWallUp.isPlaying) {
                sfxWallUp.pitch = Random.Range(1.2f, 1.5f);
                sfxWallUp.Play();
            }

            if(myMovement.facingRight)
            {
                Vector3 theScale = wall.transform.localScale;
                theScale.x *= -1;
                wall.transform.localScale = theScale;
            }

 
    }

    void PushWall()
    {
        Debug.Log("here");
        wall.GetComponent<DelilahWall>().free = true;
        StartCoroutine(ExplodeWall());

        if (sfxWallPush != null && !sfxWallPush.isPlaying)
        {
            sfxWallPush.pitch = Random.Range(1.2f, 1.5f);
            sfxWallPush.Play();
        }

    }
    IEnumerator ExplodeWall()
    {

        //ParticleSystem expl = Instantiate(explosion, wall.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(pushTime);

        destroyWall();
        Destroy(wall);
       // Destroy(expl, 3);
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
