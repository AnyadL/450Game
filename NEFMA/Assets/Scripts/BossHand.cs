using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {

    private Rigidbody2D myBody;
    private BossController myController;
    public bool leftHand = true;
    public float maxHeight = 0;
    public float minHeight = 0;
    private float currentMaxHeight = 0;
    private float currentMinHeight = 0;
    public Vector2 velocity = new Vector2(0, 0);
    [HideInInspector] public float nextSwipe;
    public float swipeCooldown = 10f;
    public bool moving = true;
    [HideInInspector] public float nextMove = 0;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        if (leftHand)
        {
            myController.registerBodyPart(gameObject, -1);
        }
        else
        {
            myController.registerBodyPart(gameObject, 1);
        }
        currentMaxHeight = maxHeight;
        currentMinHeight = minHeight;
        nextSwipe = Time.time + swipeCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        if (!moving)
        {
            if (nextMove <= Time.time && nextMove != 0)
            {
                moving = true;
            }
        }
        //if (nextSwipe <= Time.time && nextSwipe != 0)
        //{
        //    raiseHand(maxHeight);
        //}
    }

    public void raiseHand(float height)
    {
        currentMaxHeight = height;
        currentMinHeight = height;
        velocity.y = Mathf.Abs(velocity.y);
        moving = true;
        nextSwipe = 0;
    }

    public void resetHands(int hand)
    {
        currentMaxHeight = maxHeight;
        currentMinHeight = minHeight;
        if (hand == 1)
        {
            moving = true;
        }
        else
        {
            moving = false;
            nextMove = Time.time + velocity.y / maxHeight;
        }
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            return;
        }
        if (currentMaxHeight != currentMinHeight || myBody.position.y <= currentMaxHeight)
        {
            if ((myBody.position.y >= currentMaxHeight || myBody.position.y <= currentMinHeight ) && currentMaxHeight != currentMinHeight)
            {
                velocity.y = -velocity.y;
            }
            myBody.MovePosition(myBody.position + velocity * Time.fixedDeltaTime);
        }
        if (currentMaxHeight == currentMinHeight && myBody.position.y >= currentMaxHeight)
        {
            moving = false;
            nextMove = 0;
        }
    }
}
