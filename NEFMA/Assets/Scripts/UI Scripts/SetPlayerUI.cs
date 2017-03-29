using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SetPlayerUI : MonoBehaviour {

    private AttributeController myAttribute;

    public Slider healthSlider;
    public Slider powerSlider;

    private float health;
    private float currentFire;
    private float oldFire;

    void Start()
    {
        myAttribute = gameObject.GetComponent<AttributeController>();
        if (healthSlider != null)
        {
            health = myAttribute.getHealth();
            healthSlider.maxValue = health;
            healthSlider.value = health;
            print("getting health: " + health);
        }
        if (powerSlider != null)
        {
            powerSlider.maxValue = myAttribute.bigCooldown;
            powerSlider.value = myAttribute.bigCooldown;
            oldFire = 0;
        }
    }

    public void Update()
    {
        if (healthSlider != null)
        {
            if (myAttribute.getHealth() != health) {
                health = myAttribute.getHealth();
                if (health > healthSlider.maxValue)
                    healthSlider.maxValue = health;
                healthSlider.value = health;
                
                print("getting health: " + health);
            }
        }
        if (powerSlider != null)
        {
            currentFire = myAttribute.nextBigFire;
            if ((currentFire != oldFire) || (Time.time <= currentFire)) // potentiall add 0.000001 to time to avoid time = 0
            {
                powerSlider.value = myAttribute.bigCooldown - (currentFire - Time.time);
            }
            oldFire = currentFire;
        }
    }
}
