using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetPlayerUI : MonoBehaviour {

    public Slider healthSlider;
    public Slider powerSlider;

    private float health;
    private float currentFire;
    private float oldFire;

    void Start()
    {
        if (healthSlider != null)
        {
            health = gameObject.GetComponent<AttributeController>().health;
            healthSlider.value = health;
        }
        if (powerSlider != null)
        {
            powerSlider.value = gameObject.GetComponent<AgniAttack>().bigCooldown;
            oldFire = 0;
        }
    }

    public void Update()
    {
        if (healthSlider != null)
        {
            if (gameObject.GetComponent<AttributeController>().health != health) {
                health = gameObject.GetComponent<AttributeController>().health;
                healthSlider.value = health;
            }
        }
        if (powerSlider != null)
        {
            currentFire = gameObject.GetComponent<AgniAttack>().nextBigFire;
            if ((currentFire != oldFire) || (Time.time <= currentFire)) // potentiall add 0.000001 to time to avoid time = 0
            {
                powerSlider.value = gameObject.GetComponent<AgniAttack>().bigCooldown - (currentFire - Time.time);
            }
            oldFire = currentFire;
        }
    }
}
