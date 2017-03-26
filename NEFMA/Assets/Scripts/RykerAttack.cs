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
            //Dashes through enemies
            if (Input.GetButtonDown("Fire1_"+hm.inputNumber) && !Globals.gamePaused)
            {
                nextDash = Time.time + dashCooldown;
                StartCoroutine(Dash());
            }
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
        GameObject dashObject = Instantiate(dashPrefab, (gameObject.transform.position), Quaternion.identity) as GameObject;
        if (hm.facingRight)
        {
            //transform.position += new Vector3(dashSpeed * Time.deltaTime, dashTime, 0.0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(100, 0);
        }
        else
        {
            transform.position -= new Vector3(dashSpeed * Time.deltaTime, dashTime, 0.0f);
        }
        yield return new WaitForSeconds(0.5f);
        myAttribute.dashing = false;
        Destroy(dashObject);
    }

    //Creates claw attack which persists for a second and then disappears
    void StunAttack()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

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

    }
}