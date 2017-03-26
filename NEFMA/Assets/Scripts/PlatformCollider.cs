using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Transform platform = transform.parent;
        //print("----------------------------------------------------------------------------------------------------");
        //print("Platform: " + platform + " | TL: " + (platform.transform.position.x - platform.transform.localScale.x / 2) + "," + (platform.transform.position.y + platform.transform.localScale.y / 2) + " | TR: " + (platform.transform.position.x + platform.transform.localScale.x / 2) + "," + (platform.transform.position.y + platform.transform.localScale.y / 2));
        //print("Platform: " + platform + " | BL: " + (platform.transform.position.x - platform.transform.localScale.x / 2) + "," + (platform.transform.position.y - platform.transform.localScale.y / 2) + " | BR: " + (platform.transform.position.x + platform.transform.localScale.x / 2) + "," + (platform.transform.position.y - platform.transform.localScale.y / 2));
        //print("----------------------------------------------------------------------------------------------------");
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D jumper)
    {
        //print("--------------------------------------------------");
        Transform platform = transform.parent;
        //print("Entered: " + platform + " | at: " + jumper.transform.position);
        //Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[0], true);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[1], true);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[2], true);
    }

    private void OnTriggerExit2D(Collider2D jumper)
    {
        Transform platform = transform.parent;
        //print("Exited " + platform + " | at: " + jumper.transform.position);
        //Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponent<Collider2D>(), false);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[0], false);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[1], false);
        Physics2D.IgnoreCollision(jumper.GetComponent<Collider2D>(), platform.GetComponents<Collider2D>()[2], false);
        //print("--------------------------------------------------");
    }
}
