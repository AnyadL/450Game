using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeController : MonoBehaviour {

    public float health;
    
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        health = health - 1; 
    }
}
