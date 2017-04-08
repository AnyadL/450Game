using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpit : MonoBehaviour {

    public float lifeTime = 3.0f;
    private float preTime = 1.0f;
    private BoxCollider2D myCollider;

    // Use this for initialization
    void Start () {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        StartCoroutine(hitboxOn());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator hitboxOn()
    {
        yield return new WaitForSeconds(preTime);
        myCollider.enabled = true;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position - (Vector3.up * 0.01f));
        StartCoroutine(AttackTime());
    }

    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(lifeTime - preTime);
        Destroy(gameObject);
    }
}
