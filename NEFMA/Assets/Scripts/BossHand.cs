using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour {

    private Rigidbody2D myBody;
    public float maxHeight = 0;
    public float minHeight = 0;
    //public int movementDirection = -1;
    public Vector2 velocity = new Vector2(0, 0);

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        if (myBody.position.y >= maxHeight || myBody.position.y <= minHeight)
        {
            velocity.y = -velocity.y;
        }
        myBody.MovePosition(myBody.position + velocity * Time.fixedDeltaTime);
    }
}
