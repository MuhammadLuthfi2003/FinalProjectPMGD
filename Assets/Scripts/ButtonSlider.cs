using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] int defaultSliderIndex = 0;

    [Header("Event When Value Changes")]
    public UnityEvent onValueChange;

    [HideInInspector]
    public int currentSliderIndex = 0;
    [HideInInspector]
    public float currentSliderValue = 0;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentSliderIndex = defaultSliderIndex;
        currentSliderValue = buttonSliderDatas[currentSliderIndex].value;
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
        onValueChange.Invoke();
    }

    public void setSprite(int index)
    {
        sprite.sprite = buttonSliderDatas[index].sliderImage;
    }

    public float GetCurrentValue()
    {
        return buttonSliderDatas[currentSliderIndex].value;
    }
}
