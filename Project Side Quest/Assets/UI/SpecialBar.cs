using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour {

    protected Slider timerSlider;

    protected float currentTime = 0;
    protected float maxTime = 0;

    protected bool timerActive = false;

	public void Init()
    {
        timerSlider = GetComponentInChildren<Slider>();
        currentTime = 0;
        timerSlider.value = 0;
        timerActive = false;
    }

    private void Update()
    {
        if(timerActive)
        {
            currentTime += Time.deltaTime;

            timerSlider.value = currentTime / maxTime;

            if(currentTime >= maxTime)
            {
                currentTime = 0;
                timerActive = false;
            }
        }
    }

    public void ResetTimer()
    {
        timerActive = true;
        currentTime = 0;
    }

    public void NewBoss(float specialTime)
    {
        //Debug.Log("Special Bar New Boss = " + specialTime);

        maxTime = specialTime;
        currentTime = 0;

        timerSlider.value = 0;

        timerActive = true;
    }

    public void BossKilled()
    {
        timerActive = false;

        currentTime = 0;
        timerSlider.value = 1;
    }
}
