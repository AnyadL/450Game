using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpit : MonoBehaviour {

    public float attackTime = 1.5f;
    private BoxCollider2D myCollider;

    // Use this for initialization
    void Start () {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
        StartCoroutine(AttackTime());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackTime);
        Destroy(gameObject);
    }
}
