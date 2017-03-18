using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawScript : MonoBehaviour {

    public float attackTime = 1.5f;
    // Use this for initialization
    [HideInInspector] public GameObject owner;
    [HideInInspector]public float velocityDirection;

    void Start()
    {
        transform.position = owner.transform.position + new Vector3(6 * velocityDirection, 1, 0);
        StartCoroutine(AttackTime());
    }

    private void FixedUpdate()
    {

        transform.position = owner.transform.position + new Vector3(6*velocityDirection, 1, 0);
        
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackTime);
        Destroy(gameObject);
    }
}
