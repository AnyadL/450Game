using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RykerDashScript : MonoBehaviour {
    [HideInInspector] public GameObject owner;
    public bool facingRight;

    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        facingRight = owner.GetComponent<HeroMovement>().facingRight;
        Vector3 theScale = transform.localScale;

        if (facingRight)
        {
            theScale.x = -theScale.x;
        }
        transform.localScale = theScale;
    }

}
