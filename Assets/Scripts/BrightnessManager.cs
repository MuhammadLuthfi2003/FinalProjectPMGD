using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BrightnessManager : MonoBehaviour
{
    private Image img;

    [SerializeField] private ScriptableFloat brightness;

    void Awake()
    {
        img = GetComponent<Image>();
        img.raycastTarget = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Color32 color = GetColorFromFloat(brightness.value);
        img.color = color;
    }

    public void UpdateBrightness()
    {
        Color32 color = GetColorFromFloat(brightness.value);
        img.color = color;
    }

    private Color32 GetColorFromFloat(float value)
    {
        
        switch (value)
        {
            case 0:
                return new Color32(0, 0, 0, 153); // 60%
                break;
            case 0.2f:
                return new Color32(0, 0, 0, 102); // 40%
                break;
            case 0.4f:
                return new Color32(0, 0, 0, 51); // 20%
                break;
            case 0.6f:
                return new Color32(0, 0, 0, 0); // 0%
                break;
            case 0.8f:
                return new Color32(255, 255, 255, 51); // 20%
                break;
            case 1f:
                return new Color32(255, 255, 255, 102); // 40%
                break;
            default:
                return new Color32(0, 0, 0, 0); // 0%
                break;
        }

    }
}
