using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelilahWall : MonoBehaviour {

    [HideInInspector] public GameObject owner;
    [HideInInspector] public int wallRight;
    [HideInInspector] public bool free = false;
    [HideInInspector] public float timeToDie;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (free && Time.time >= timeToDie)
        {
            Destroy(gameObject);
        }
	}

    private void FixedUpdate()
    {
        if (!free)
        {
            transform.position = owner.transform.position + new Vector3(6 * wallRight, 1, 0);
        }
    }
}
