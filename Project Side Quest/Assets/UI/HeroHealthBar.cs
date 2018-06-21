using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : HealthBar {

    int characterCount = 1;
    int laneLevel = 1;
    Text levelText;

    public override void Init()
    {
        healthSlider = GetComponentInChildren<Slider>();

        currentText = GetComponentsInChildren<Text>()[0];
        levelText = GetComponentsInChildren<Text>()[1];
    }

    public override void NewValues(float newMax)
    {
        maxValue = newMax;
        currentValue = maxValue;

        healthSlider.value = currentValue / maxValue;
    }

    public void LevelUp(float newValue)
    {
        NewValues(newValue);

        laneLevel++;
        levelText.text = laneLevel.ToString();
    }

    public override void UpdateValues(float newValue)
    {
        currentValue = newValue;

        healthSlider.value = currentValue / maxValue;
    }

    public void CharacterKilled(bool laneDestroyed)
    {
        characterCount--;
        currentText.text = characterCount.ToString();

        if (laneDestroyed)
        {
            //Some UI Indicator
            healthSlider.value = 0;
            healthSlider.gameObject.GetComponentsInChildren<Image>()[1].color = Color.red;
        }
        else
        {
            currentValue = maxValue;
            healthSlider.value = currentValue / maxValue;
        }
    }

    public void ResetValues()
    {
        characterCount = 1;
        currentText.text = characterCount.ToString();

        laneLevel = 1;
        levelText.text = laneLevel.ToString();

        healthSlider.gameObject.GetComponentsInChildren<Image>()[1].color = Color.green;
        healthSlider.value = currentValue / maxValue;
    }

    public void CharacterCountUp()
    {
        characterCount++;
        currentText.text = characterCount.ToString();
    }
}
