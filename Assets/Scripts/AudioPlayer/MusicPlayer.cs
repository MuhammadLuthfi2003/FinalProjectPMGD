using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    [Header("Music Settings")]
    [SerializeField] AudioClip music;
    [SerializeField] ScriptableFloat musicVolume;
    [SerializeField] bool isLooping = true;

    private AudioSource audioSource;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.volume = musicVolume.value;
        if (isLooping)
        {
            audioSource.loop = true;
        }
        audioSource.Play();
    }

    public void UpdateVolume()
    {
        audioSource.volume = musicVolume.value;
    }

}
