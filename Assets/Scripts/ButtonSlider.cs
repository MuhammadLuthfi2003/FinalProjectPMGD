using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct ButtonSliderData
{
    public Sprite sliderImage;
    public float value;
}

public class ButtonSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Slider Images")]
    [SerializeField] ButtonSliderData[] buttonSliderDatas;

    [Header("Scriptable Float")]
    [SerializeField] ScriptableFloat scriptableFloat;

    [Header("Event When Value Changes")]
    public UnityEvent onValueChange;

    [HideInInspector]
    public int currentSliderIndex = 0;
    [HideInInspector]
    public float currentSliderValue = 0;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        
        bool hasFound = false;

        foreach (ButtonSliderData data in buttonSliderDatas)
        {
            if (data.value == scriptableFloat.value)
            {
                currentSliderValue = data.value;
                hasFound = true;
                break;
            }
            currentSliderIndex++;
        }

        if (!hasFound)
        {
            currentSliderIndex = Mathf.FloorToInt(buttonSliderDatas.Length / 2);
            currentSliderValue = buttonSliderDatas[currentSliderIndex].value;
            scriptableFloat.value = currentSliderValue;
        }

        setSprite(currentSliderIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddValue()
    {
        int newVal = currentSliderIndex + 1;

        if (newVal >= buttonSliderDatas.Length)
        {
            newVal = currentSliderIndex;
        }
        else
        {
            currentSliderIndex = newVal;
        }

        setSprite(currentSliderIndex);
        scriptableFloat.value = buttonSliderDatas[currentSliderIndex].value;
        onValueChange.Invoke();
    }

    public void DecreaseValue()
    {
        int newVal = currentSliderIndex - 1;

        if (newVal < 0)
        {
            newVal = 0;
        }
        else
        {
            currentSliderIndex = newVal;
        }

        setSprite(currentSliderIndex);
        scriptableFloat.value = buttonSliderDatas[currentSliderIndex].value;
        onValueChange.Invoke();
    }

    public void setSprite(int index)
    {
        image.sprite = buttonSliderDatas[index].sliderImage;
    }

    public float GetCurrentValue()
    {
        return buttonSliderDatas[currentSliderIndex].value;
    }
}
