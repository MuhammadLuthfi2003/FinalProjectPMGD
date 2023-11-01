using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    [Header("Slides / Images / Panel to display in order")]
    public GameObject[] slides;

    [Header("Slide Settings")]
    public bool loopSlides = false;

    int currentSlideIndex = 0;
    int slideLength = 0;
    // Start is called before the first frame update
    void Start()
    {
        slideLength = slides.Length;

        foreach (GameObject slide in slides)
        {
            slide.SetActive(false);
        }
    }

    public void OpenSlide()
    {
        currentSlideIndex = 0;
        slides[currentSlideIndex].SetActive(true);
    }

    public void NextSlide()
    {
        currentSlideIndex++;

        if (!loopSlides)
        {
            if (currentSlideIndex < slideLength)
            {
                slides[currentSlideIndex - 1].SetActive(false);
                slides[currentSlideIndex].SetActive(true);
            }
            else
            {
                CloseSlide();
            }
        }
        else
        {
            if (currentSlideIndex < slideLength)
            {
                slides[currentSlideIndex - 1].SetActive(false);
                slides[currentSlideIndex].SetActive(true);
            }
            else
            {
                currentSlideIndex = 0;
                slides[slideLength - 1].SetActive(false);
                slides[currentSlideIndex].SetActive(true);
            }
        }
    }

    public void CloseSlide()
    {
        slides[currentSlideIndex].SetActive(false);
        currentSlideIndex = 0;
    }
}
