using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSliderMover : MonoBehaviour
{
    private int _capacity = 0;
    private int _currentLoad = 0;
    private Slider _slider;
    
    [SerializeField] private BagBehaviour _bagBehaviour;
    [SerializeField] private TMP_Text _sliderText;
    [SerializeField] private Image _sliderFillImage;
    
    [Header("Slider settings")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    private void Start()
    {
        _capacity = _bagBehaviour.Capacity;
        _slider = GetComponent<Slider>();
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        _currentLoad = _bagBehaviour.Wheats.Count;
        _sliderText.text = $"{_currentLoad}/{_capacity}";
        _slider.value = (float) _currentLoad / _capacity;
        _sliderFillImage.color = Color.Lerp(_startColor, _endColor, _slider.value);
    }
}
