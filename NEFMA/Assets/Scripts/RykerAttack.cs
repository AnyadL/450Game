using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RykerAttack : MonoBehaviour
{

    public GameObject stunPrefab;
    public GameObject dashPrefab;

    private string playerNumber;

    private HeroMovement hm;
    private AttributeController myAttribute;
    public float dashCooldown = 3.0f;
    public float nextDash;
    public float dashSpeed;
    public float dashTime;
    public Animator animator;


    [HideInInspector] public bool LittleAttack;
    [HideInInspector] public bool BigAttack;

    public AudioSource sfxDash;
    public AudioSource sfxStun;

    // Use this for initialization
    void Start()
    {
        //In order to figure out which way the character is facing I need to access the HeroMovement script
        hm = gameObject.GetComponent<HeroMovement>();
        myAttribute = gameObject.GetComponent<AttributeController>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextDash)
        {
            //Dashes through enemies
            if (Input.GetButtonDown("Fire1_"+hm.inputNumber) && !Globals.gamePaused)
            {
                LittleAttack = true;
                animator.SetBool("AttackLittle", LittleAttack);
                nextDash = Time.time + dashCooldown;
                StartCoroutine(Dash());

                if (sfxDash != null && !sfxDash.isPlaying)
                {
                    sfxDash.pitch = Random.Range(0.9f, 1.2f);
                    sfxDash.Play();
                }
            }
        }
        else
        {
            LittleAttack = false;
            animator.SetBool("AttackLittle", LittleAttack);
        }

        if (Time.time >= myAttribute.nextBigFire)
        {
            //Stuns all enemies on the screen 
            if (Input.GetButtonDown("Fire2_"+hm.inputNumber) && !Globals.gamePaused)
            {

                myAttribute.nextBigFire = Time.time + myAttribute.bigCooldown;
                StunAttack();
            }
        }

    }

    // Creates a Hiss that stuns enemies
    IEnumerator Dash()
    {
        myAttribute.dashing = true;
        int myLayer = gameObject.layer;
        gameObject.layer = 11;
        GameObject dashObject = Instantiate(dashPrefab, (gameObject.transform.position), Quaternion.identity) as GameObject;
        if (hm.facingRight)
        {
            //transform.position += new Vector3(dashSpeed * Time.deltaTime, dashTime, 0.0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(150, 0);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-150, 0);
        }
        yield return new WaitForSeconds(dashTime);
        gameObject.layer = myLayer;
        myAttribute.dashing = false;
        Destroy(dashObject);

    }

    //Creates claw attack which persists for a second and then disappears
    void StunAttack()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (sfxStun != null && !sfxStun.isPlaying)
        {
            sfxStun.pitch = Random.Range(0.9f, 1.2f);
            sfxStun.Play();
        }

        foreach (GameObject go in gos)
        {
            if (go.GetComponent<SpriteRenderer>().isVisible)
            {
                //GameObject stunObject = Instantiate(stunPrefab, (go.transform.position), Quaternion.identity) as GameObject;
                Vector3 stunPosition = new Vector3(go.transform.position.x, go.transform.position.y + 1, go.transform.position.z);
                Instantiate(stunPrefab, (stunPosition) + (Vector3.up * 0.5f), Quaternion.identity);
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(-go.GetComponent<Rigidbody2D>().velocity.x, go.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        StartCoroutine(Stun());


    }

    IEnumerator Stun()
    {
        BigAttack = true;
        animator.SetBool("AttackBig", BigAttack);
        myAttribute.dashing = true;
        hm.currentMoveForce = 0;

        yield return new WaitForSeconds(0.5f);

    
        BigAttack = false;
        animator.SetBool("AttackBig", BigAttack);
        myAttribute.dashing = false;
        hm.currentMoveForce = hm.moveForce;
    }
}