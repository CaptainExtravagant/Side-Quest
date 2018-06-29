using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneUI : HealthBar {

    int characterCount = 1;
    int laneLevel = 1;
    Text levelText;

    Text influenceCost;
    Text goldCost;

    Slider cooldown;

    public void Init(float newMax)
    {
        cooldown = GetComponentsInChildren<Slider>()[0];

        healthSlider = GetComponentsInChildren<Slider>()[1];

        currentText = GetComponentsInChildren<Text>()[3];
        levelText = GetComponentsInChildren<Text>()[0];

        influenceCost = GetComponentsInChildren<Text>()[1];
        goldCost = GetComponentsInChildren<Text>()[2];

        maxValue = newMax;
        currentValue = maxValue;

        cooldown.value = 0;

        healthSlider.value = currentValue / maxValue;
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
            healthSlider.value = 1;
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

    public void UpdateCurrencyValues(GameManager.CURRENCY currency, int newValue)
    {
        if(currency == GameManager.CURRENCY.INFLUENCE)
        {
            influenceCost.text = newValue.ToString();
        }
        else
        {
            goldCost.text = newValue.ToString();
        }
    }

    public void UpdateCooldown(float value)
    {
        cooldown.value = value;
    }
}
