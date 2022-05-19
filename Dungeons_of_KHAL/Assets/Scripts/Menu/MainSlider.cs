using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderText;


    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            v /= 100;
            _sliderText.text = v.ToString("0%");
        });
    }
}