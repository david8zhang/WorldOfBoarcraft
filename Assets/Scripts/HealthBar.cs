using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance { get; private set; }


    void Awake() {
        instance = this;
    }

    public void SetValue(float value) {
        Slider slider = GetComponent<Slider>();
        slider.value = value;
    }
}
