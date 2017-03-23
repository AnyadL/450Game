using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTail : MonoBehaviour {

    private Rigidbody2D myBody;
    private BossController myController;
    public float maxX = 68;
    public float minX = -68;
    private float startTime = 0;
    private float direction = 1;
    private float oldval = 0;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        myController.registerTail(gameObject);
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void FixedUpdate()
    {
        sway();
    }

    public void moveUp()
    {
        myBody.MovePosition(myBody.position + (Vector2.up * 10 * Time.fixedDeltaTime));
    }

    public void moveDown()
    {
        myBody.MovePosition(myBody.position - (Vector2.up * 10 * Time.fixedDeltaTime));
    }

    // maybe change this to just use increasing and decreasing velocities like BossHand?
    public void sway()
    {
        float val = Mathf.Sin(Time.time - startTime) * (Mathf.Abs(maxX - minX) / 100f);
        if ((val > 0 && oldval < 0) || (val < 0 && oldval > 0))
        {
            //Debug.Log("val: " + val + " | oldval: " + oldval + " | Highest X: " + myBody.position.x);
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
}
