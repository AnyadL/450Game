﻿using System.Collections;
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
    public bool moving = true;
    public float downForce = 6;
    public float upForce = 0.2f;
    public float midpoint = 0;
    public GameObject handArt;
    [HideInInspector] public float waitTime = 1;

    public AudioSource sfxSlam;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        myController = GameObject.FindWithTag("Boss Controller").GetComponent<BossController>();
        if (leftHand)
        {
            myController.registerHand(gameObject, -1);
        }
        else
        {
            myController.registerHand(gameObject, 1);
        }
        midpoint = maxHeight - (Mathf.Abs(maxHeight - minHeight) / 2) + 0.1f;
        currentMaxHeight = maxHeight;
        currentMinHeight = minHeight;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void handDown()
    {
        myBody.velocity -= new Vector2(0, downForce);
    }

    public void handUp()
    {
        // Speeding up
        if (myBody.position.y <= midpoint)
        {
            myBody.velocity += new Vector2(0, upForce);
        }
        // Slowing down
        else
        {
            myBody.velocity -= new Vector2(0, upForce);
        }
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            return;
        }
        // Headed Downwards
        if (myBody.velocity.y <= 0)
        {
            if (myBody.position.y > currentMinHeight)
            {
                handDown();
                if (sfxSlam != null && !sfxSlam.isPlaying)
                {
                    sfxSlam.pitch = Random.Range(0.9f, 1.2f);
                    sfxSlam.Play();
                }
            }
            else
            {
                myBody.velocity = new Vector2(0, 0);
                transform.position = new Vector3(transform.position.x, currentMinHeight, transform.position.z);
                StartCoroutine(slamHands());
            }
        }
        // Headed Upwards
        else
        {
            if (myBody.position.y < currentMaxHeight)
            {
                handUp();
            }
            else
            {
                myBody.velocity = new Vector2(0, 0);
                transform.position = new Vector3(transform.position.x, currentMaxHeight, transform.position.z);
                StartCoroutine(waitHands());
            }
        }
        if (handArt != null)
        {
            handArt.transform.position = transform.position - (Vector3.up * 1.1f);
        }
    }

    IEnumerator slamHands()
    {
        moving = false;
        yield return new WaitForSeconds(waitTime);
        handUp();
        moving = true;
        if (leftHand)
        {
            myController.headChange(0);
        }
    }

    IEnumerator waitHands()
    {
        moving = false;
        yield return new WaitForSeconds(waitTime);
        handDown();
        moving = true;
        if (leftHand)
        {
            myController.headChange(1);
        }
    }
}
