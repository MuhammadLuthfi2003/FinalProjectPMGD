using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance;

    private AudioSource audioSource;

    [Header("Volume Settings")]
    [SerializeField] ScriptableFloat sfxVolume;

    [Header("SFX Settings")]
    public AudioClip clickSFX;
    public AudioClip exitWarningSFX;
    public AudioClip openWindowSFX;
    public AudioClip closeWindowSFX;
    public AudioClip winSFX;
    public AudioClip gameOverSFX;


    [Header("Player SFX")]  
    public AudioClip swingBambooSFX;
    public AudioClip jumpSFX;
    public AudioClip itemPickupSFX;
    public AudioClip hitSFX;
    public AudioClip hitgroundSFX;

    [Header("Enemy SFX")]
    public AudioClip enemyDeathSFX;
    public AudioClip enemyShootSFX;
    


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


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = sfxVolume.value;
    }

    public void PlayClickSFX()
    {
        audioSource.PlayOneShot(clickSFX);
    }

    public void PlayExitSFX()
    {
          audioSource.PlayOneShot(exitWarningSFX);
    }

    public void PlayOpenWindowSFX()
    {
        audioSource.PlayOneShot(openWindowSFX);
    }

    public void PlayCloseWindowSFX()
    {
        audioSource.PlayOneShot(closeWindowSFX);
    }

    public void PlaySwingBambooSFX()
    {
        audioSource.PlayOneShot(swingBambooSFX);
    }

    public void PlayJumpSFX()
    {
        audioSource.PlayOneShot(jumpSFX);
    }

    public void PlayItemPickupSFX()
    {
        audioSource.PlayOneShot(itemPickupSFX);
    }

    public void PlayEnemyDeathSFX()
    {
        audioSource.PlayOneShot(enemyDeathSFX);
    }

    public void PlayEnemyShootSFX()
    {
        audioSource.PlayOneShot(enemyShootSFX);
    }

    public void PlayHitSFX()
    {
        audioSource.PlayOneShot(hitSFX);
    }

    public void PlayWinSFX()
    {
        audioSource.PlayOneShot(winSFX);
    }

    public void PlayGameOverSFX()
    {
        audioSource.PlayOneShot(gameOverSFX);
    }

    public void PlayHitGroundSFX()
    {
        audioSource.PlayOneShot(hitgroundSFX);
    }

    public void UpdateVolume()
    {
        audioSource.volume = sfxVolume.value;
    }
}
