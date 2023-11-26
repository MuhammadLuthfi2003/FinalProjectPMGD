using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    [SerializeField] ScriptableInteger scriptable;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = scriptable.defaultValue;
        slider.value = scriptable.value;
    }

    private void Update()
    {
        slider.value = scriptable.value;
    }
}
