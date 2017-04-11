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
    public bool gateFive = false;
    public bool FORCEPHASE2 = false;
    public bool FORCEPHASE3 = false;
    public bool FORCEPHASE4 = false;
    public bool FORCEPHASE5 = false;

    // Use this for initialization
    void Start () {
        //myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        myController.registerHead(gameObject);
        myAttributes = gameObject.GetComponent<AttributeController>();
        //fireballs();
    }
	
	// Update is called once per frame
	void Update () {
        if ((!gateOne && (myAttributes.getHealth() / myAttributes.maxHealth) <= 1.00))
        {
            startPhase1();
        }
        else if ((!gateTwo && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.75) || (!gateTwo && FORCEPHASE2))
        {
            startPhase2();
        }
        else if ((!gateThree && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.5) || (!gateThree && FORCEPHASE3))
        {
            startPhase3();
        }
        else if ((!gateFour && (myAttributes.getHealth() / myAttributes.maxHealth) <= 0.25) || (!gateFour && FORCEPHASE4))
        {
            startPhase4();
        }
        else if (!gateFive && FORCEPHASE5)
        {
            gateFive = true;
            if (!gateOne)
            {
                startPhase1();
            }
            FORCEPHASE2 = true;
            if (!gateTwo)
            {
                startPhase2();
            }
            FORCEPHASE3 = true;
            if (!gateThree)
            {
                startPhase3();
            }
            FORCEPHASE4 = true;
            if (!gateFour)
            {
                startPhase4();
            }
            myAttributes.decreaseHealth(25);
        }
    }

    public void startPhase1()
    {
        gateOne = true;
        myController.phaseOne();
    }

    public void startPhase2()
    {
        gateTwo = true;
        myController.phaseTwo();
        if (FORCEPHASE2)
        {
            myAttributes.decreaseHealth(50);
            myAttributes.takenDamage();
        }
    }

    public void startPhase3()
    {
        gateThree = true;
        myController.phaseThree();
        if (FORCEPHASE3)
        {
            myAttributes.decreaseHealth(25);
            myAttributes.takenDamage();
        }
    }

    public void startPhase4()
    {
        gateFour = true;
        myController.phaseFour();
        if (FORCEPHASE4)
        {
            myAttributes.decreaseHealth(50);
            myAttributes.takenDamage();
        }
    }

    public void startPhase5()
    {

    }

    public void fireballs()
    {
        Vector3 bulletPosition = new Vector3(transform.position.x + 0.64f, transform.position.y - 8f, -3);
        
        Instantiate(fireballPrefab, bulletPosition, Quaternion.identity);
        //newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -30);
    }
}
