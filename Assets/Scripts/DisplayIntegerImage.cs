using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayIntegerImage : MonoBehaviour
{
    [SerializeField] private ScriptableInteger integerToDisplay;
    [SerializeField] private GameObject[] images;

    private void Start()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i < integerToDisplay.value)
            {
                images[i].SetActive(true);
            }
            else
            {
                images[i].SetActive(false);
            }
        }
    }
}
