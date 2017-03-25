using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTail : MonoBehaviour {

    private Rigidbody2D myBody;
    private BossController myController;
    public float maxX = 68;
    public float minX = -68;
    public float maxY = 70;
    public float minY = 60;
    private float startTime = 0;
    private float direction = -1;
    private float oldval = 0;
    public float needle = 0;
    public float numberOfNeedles = 0;
    public GameObject needePrefab;
    public float needleSpeed = 0;
    public float needleDelayTime = 0;
    public float needleAngleSpread = 0;
    private Color debugColor = Color.red;
    public bool movingY = false;
    public bool yDown = true;
    private float cycler = 0;
    private bool shooting = false;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        myController.registerTail(gameObject);
        startTime = Time.time;
        cycler = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time >= cycler && !shooting)
        {
            movingY = true;
        }
    }

    void FixedUpdate()
    {
        if (!movingY)
        {
            sway();
        }
        if (movingY)
        {
            if (yDown)
            {
                moveDown();
            }
            else
            {
                moveUp();
            }
        }
    }

    public void moveUp()
    {
        myBody.MovePosition(myBody.position + (Vector2.up * 10 * Time.fixedDeltaTime));
        if (myBody.position.y >= maxY)
        {
            myBody.MovePosition(new Vector2(myBody.position.x, maxY));
            movingY = false;
            yDown = true;
            startTime = Time.time;
            cycler = Time.time + (2 * Mathf.PI);
            shooting = false;
        }
    }

    public void moveDown()
    {
        myBody.MovePosition(myBody.position - (Vector2.up * 10 * Time.fixedDeltaTime));
        if (myBody.position.y <= minY)
        {
            myBody.MovePosition(new Vector2(myBody.position.x, minY));
            movingY = false;
            yDown = false;
            StartCoroutine(prepAttack());
            startTime = Time.time;
            shooting = true;
            direction = -direction; // this fixes a bug
        }
    }

    IEnumerator prepAttack()
    {
        yield return new WaitForSeconds(Mathf.PI);
        spawnNeedles();
    }

    IEnumerator stopAttack()
    {
        yield return new WaitForSeconds(Mathf.PI - needleDelayTime);
        movingY = true;
    }

    // maybe change this to just use increasing and decreasing velocities like BossHand?
    public void sway()
    {
        float val = Mathf.Sin(Time.time - startTime) * (Mathf.Abs(maxX - minX) / 100f);
        if ((val > 0 && oldval < 0) || (val < 0 && oldval > 0))
        {
            //Debug.Log("val: " + val + " | oldval: " + oldval + " | Highest X: " + myBody.position.x);
            if (direction >= 0)
            {
                myBody.MovePosition(new Vector2(maxX, myBody.position.y));
            }
            else
            {
                myBody.MovePosition(new Vector2(minX, myBody.position.y));
            }
            startTime = Time.time;
            direction = -direction;
            oldval = 0;
            return;
        }
        // headed right
        if (direction >= 0)
        {
            myBody.MovePosition(myBody.position + (Vector2.right * val));
        }
        // headed left
        else
        {
            myBody.MovePosition(myBody.position - (Vector2.right * val));
        }
        oldval = val;
    }

    public void spawnNeedles()
    {
        if (needle < numberOfNeedles)
        {
            if (needle == 0 || needle == 3 || needle == 6) 
            {
                debugColor = Color.green;
            }
            else if (needle == 4 || needle == 5)
            {
                debugColor = Color.blue;
            }
            else
            {
                debugColor = Color.red;
            }
            needle++;
            StartCoroutine(attack());
        }
        else if (needle >= numberOfNeedles)
        {
            needle = 0;
            StartCoroutine(stopAttack());
            return;
        }
    }

    IEnumerator attack()
    {
        float y = 3f;
        float x = 1f;
        GameObject newNeedle1 = Instantiate(needePrefab, (transform.position - (transform.right * x) - (transform.up * y)), Quaternion.Euler(0, 0, -needleAngleSpread));
        GameObject newNeedle2 = Instantiate(needePrefab, (transform.position + (transform.right * 0) - (transform.up * y)), Quaternion.identity);
        GameObject newNeedle3 = Instantiate(needePrefab, (transform.position + (transform.right * x) - (transform.up * y)), Quaternion.Euler(0, 0, needleAngleSpread));

        ///*
        Debug.DrawLine((transform.position - (transform.right * x) - (transform.up * y)), (transform.position - ((transform.right * x) * 40.25454f) - ((transform.up * y) * 50)), debugColor, 60);
        Debug.DrawLine((transform.position + (transform.right * 0) - (transform.up * y)), (transform.position + ((transform.right * 0) * 50) - ((transform.up * y) * 50)), debugColor, 60);
        Debug.DrawLine((transform.position + (transform.right * x) - (transform.up * y)), (transform.position + ((transform.right * x) * 40.25454f) - ((transform.up * y) * 50)), debugColor, 60);
        //*/

        float xcomp, ycomp;
        //float angle = Mathf.Atan(45f * Mathf.Deg2Rad);
        xcomp = Mathf.Cos((90 - needleAngleSpread) * Mathf.Deg2Rad) * -needleSpeed;
        ycomp = Mathf.Sin((90 - needleAngleSpread) * Mathf.Deg2Rad) * -needleSpeed;
        newNeedle1.GetComponent<Rigidbody2D>().velocity = new Vector2(xcomp, ycomp);

        newNeedle2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -needleSpeed);

        xcomp = Mathf.Cos((90 - needleAngleSpread) * Mathf.Deg2Rad) * needleSpeed;
        ycomp = Mathf.Sin((90 - needleAngleSpread) * Mathf.Deg2Rad) * -needleSpeed;
        newNeedle3.GetComponent<Rigidbody2D>().velocity = new Vector2(xcomp, ycomp);

        //Debug.Log(newNeedle1 + " | position: " + newNeedle1.transform.position + " | Velocity: " + newNeedle1.GetComponent<Rigidbody2D>().velocity);
        //Debug.Log(newNeedle2 + " | position: " + newNeedle2.transform.position + " | Velocity: " + newNeedle2.GetComponent<Rigidbody2D>().velocity);
        //Debug.Log(newNeedle3 + " | position: " + newNeedle3.transform.position + " | Velocity: " + newNeedle3.GetComponent<Rigidbody2D>().velocity);
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(needleDelayTime);
        spawnNeedles();
    }
}
