using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSlider : MonoBehaviour
{
    public Slider p1Slider;
    public Slider p2Slider;
    public Slider p3Slider;
    public Slider p4Slider;
    public Slider mainSlider;
    public int initialValue;

    void Start()
    {
        // Set the initial health of the player.
        mainSlider.value = initialValue;
        p1Slider.value = mainSlider.value;
        p2Slider.value = mainSlider.value;
        p3Slider.value = mainSlider.value;
        p4Slider.value = mainSlider.value;

        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

    }


    public void ValueChangeCheck()
    {
        p1Slider.value = mainSlider.value;
        p2Slider.value = mainSlider.value;
        p3Slider.value = mainSlider.value;
        p4Slider.value = mainSlider.value;
    }
}