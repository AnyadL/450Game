using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public float movespeed;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public string playerNumber;

    private bool grounded = false;
    private Vector3 moveDirection = Vector3.zero;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //TODO: Make it so hero can't jump off the screen if they continuously click button 
        grounded = true;
        if (Input.GetButtonDown("Jump_" + playerNumber) && grounded)
        {
          
           moveDirection.y = jumpForce;

        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal_" + playerNumber);
            Vector2 movement = new Vector2(moveHorizontal, rb.velocity.y);
            rb.AddForce(movement * movespeed);
        }

    }
	
}
