using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : MonoBehaviour {

    //private Rigidbody2D myBody;
    private BossController myController;
    private AttributeController myAttributes;
    public GameObject fireballPrefab;
    public bool gateOne = false;
    public bool gateTwo = false;
    public bool gateThree = false;
    public bool gateFour = false;
    public bool FORCEPHASE2 = false;
    public bool FORCEPHASE3 = false;
    public bool FORCEPHASE4 = false;

    // Use this for initialization
    void Start () {
        //myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        myController.registerHead(gameObject);
        myAttributes = gameObject.GetComponent<AttributeController>();
    }
	
	// Update is called once per frame
	void Update () {
        if ((!gateOne && (myAttributes.getHealth() / myAttributes.maxHealth) <= 1.00))
        {
            gateOne = true;
            myController.phaseOne();
        }
        else if ((!gateTwo && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.75) || (!gateTwo && FORCEPHASE2))
        {
            gateTwo = true;
            myController.phaseTwo();
            if (FORCEPHASE2)
            {
                myAttributes.decreaseHealth(50);
            }
        }
        else if ((!gateThree && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.5) || (!gateThree && FORCEPHASE3))
        {
            gateThree = true;
            myController.phaseThree();
            if (FORCEPHASE3)
            {
                myAttributes.decreaseHealth(25);
            }
        }
        else if ((!gateFour && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.25) || (!gateFour && FORCEPHASE4))
        {
            gateFour = true;
            myController.phaseFour();
            if (FORCEPHASE4)
            {
                myAttributes.decreaseHealth(50);
            }
        }
    }

    public void fireballs()
    {
        GameObject newBullet = Instantiate(fireballPrefab, (transform.position + (transform.up / 20)), Quaternion.identity) as GameObject;
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -30);
    }
}
