using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgniAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    float attackSpeed = 2f;
    float cooldown;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= cooldown)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
                Debug.Log("Shoot");
            }
        }
    }
    // Fire a bullet
    void Fire()
    {
        GameObject bPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100f);
        cooldown = Time.time + attackSpeed;
    }

}

