using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HissScript : MonoBehaviour {

    public float attackTime = 0.5f;
    [HideInInspector] public GameObject owner;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(AttackTime());
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(attackTime);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = owner.transform.position;
    }
}
