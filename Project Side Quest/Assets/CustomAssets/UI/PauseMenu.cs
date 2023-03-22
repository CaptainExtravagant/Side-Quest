using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private UIManager uiManager;

    private Button closeButton;
    private Button restartButton;

    private Toggle damageNumbers;

    private Slider musicSlider;
    private Slider sfxSlider;

	public void Init(UIManager manager)
    {
        uiManager = manager;

        closeButton = GetComponentsInChildren<Button>()[0];
        closeButton.onClick.AddListener(CloseMenu);

        restartButton = GetComponentsInChildren<Button>()[1];
        restartButton.onClick.AddListener(RestartGame);

        damageNumbers = GetComponentInChildren<Toggle>();
        damageNumbers.onValueChanged.AddListener(DamageNumbersValueChanged);

        musicSlider = GetComponentsInChildren<Slider>()[0];
        musicSlider.onValueChanged.AddListener(MusicVolume);

        sfxSlider = GetComponentsInChildren<Slider>()[1];
        sfxSlider.onValueChanged.AddListener(SFXVolume);
    }

    private void DamageNumbersValueChanged(bool newValue)
    {
        uiManager.DamageNumbers(newValue);
    }

    public void CloseMenu()
    {
        uiManager.UnpauseGame();
    }

    private void RestartGame()
    {
        uiManager.ResetGame();
    }

    private void MusicVolume(float volume)
    {
        uiManager.VolumeChange(0, volume);
    }

    private void SFXVolume(float volume)
    {
        uiManager.VolumeChange(1, volume);
    }

}
