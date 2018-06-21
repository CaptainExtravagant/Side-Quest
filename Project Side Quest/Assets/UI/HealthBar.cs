using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    protected float currentValue;
    protected float maxValue;

    protected Slider healthSlider;
    protected Text currentText;
    Text maxText;
    
    public virtual void Init()
    {
        healthSlider = GetComponentInChildren<Slider>();

        currentText = GetComponentsInChildren<Text>()[0];
        maxText = GetComponentsInChildren<Text>()[2];
    }

    public virtual void UpdateValues(float newValue)
    {
        currentValue = newValue;

        currentText.text = Mathf.RoundToInt(currentValue).ToString();

        healthSlider.value = currentValue / maxValue;
    }

    public virtual void NewValues(float newMax)
    {
        maxValue = newMax;
        currentValue = maxValue;

        currentText.text = currentValue.ToString();
        maxText.text = maxValue.ToString();

        healthSlider.value = currentValue / maxValue;
    }
}
