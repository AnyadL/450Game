using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectScript : MonoBehaviour {

    //This script is for when a player attack creates an object 
    //That object exists for attackTime seconds and then dissapears 
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
