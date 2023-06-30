using System;
using System.Collections;
using System.Collections.Generic;
using Gravitation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerGravitation : MonoBehaviour
{
    enum GameState {SettingUp, Playing}

    [SerializeField] private GameObject projectile;
    [SerializeField] private CameraControllerG cameraControllerG;

    private GameState _state = GameState.SettingUp;
    private ChangePlanetMass[] _changePlanetMasses;

    private Slider[] _sliders;

    public float velocityProyectile;
    [SerializeField] private Slider _sliderVelocity;
    [SerializeField] private TMP_Text velocityText;

    private void Start()
    {
        _changePlanetMasses = FindObjectsOfType<ChangePlanetMass>();
        _sliders = FindObjectsOfType<Slider>();
    }


    public void StartPlaying()
    {
        _state = GameState.Playing;
        projectile.SetActive(true);
        cameraControllerG.enabled = true;
        foreach (var changePlanetMass in _changePlanetMasses)
        {
            changePlanetMass.enabled = false;
        }
        foreach (var slider in _sliders)
        {
            slider.gameObject.SetActive(false);
        }
    }
    
    public void UpdateVelocity()
    {
        velocityProyectile = _sliderVelocity.value;
        velocityText.text = "Velocidad del meteorito: " + _sliderVelocity.value;
    }

}
