using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusedObjectScript : MonoBehaviour {

    public float attackTime = 1.5f;
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
}
