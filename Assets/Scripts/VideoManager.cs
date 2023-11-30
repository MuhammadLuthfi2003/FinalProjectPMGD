using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

[System.Serializable]
public struct PlaylistItem
{
    public GameObject video;
    public VideoPlayer videoPlayer;
    public float time;
    public bool isSkippable;
}

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;

    [SerializeField] private List<PlaylistItem> playlist;
    [SerializeField] private Image transitionImg;
    [SerializeField] private float transitionTime = 2f;
    [SerializeField] private bool seeCutsceneOnReload = false;

    public UnityEvent OnAllVideoDone;
    public UnityEvent OnVideoStart;

    private int currentVideoIndex = 0;
    private bool isTransitioning = false;
    private float halfTransitionTime;
    private float transitionTimer = 0f;
    public float currentAlpha = 0f;

    private bool hasAddedIndex = false;
    private bool hasExecuteVideoStart = false;

    private void Awake()
    {
        if (!seeCutsceneOnReload)
        {
            if (PlayerPrefs.GetInt("HasPlayed") == 1)
            {
                OnAllVideoDone.Invoke();
                gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("HasPlayed") == 0 || !PlayerPrefs.HasKey("HasPlayed"))
            {
                PlayerPrefs.SetInt("HasPlayed", 1);
                PlayerPrefs.Save();

                Setup();
            }
        }
        else
        {
            Setup();
        }
    }

    private void Setup()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.player.GetComponent<PlayerController>().enabled = false;
        }

        halfTransitionTime = transitionTime / 2f;

        foreach (PlaylistItem item in playlist)
        {
            item.video.SetActive(false);
        }

        transitionImg.color = new Color(transitionImg.color.r, transitionImg.color.g, transitionImg.color.b, 0f);

        StartCoroutine(PlayVideo(playlist[currentVideoIndex].time));
    }

    private void Update()
    {
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;

            if (transitionTimer < halfTransitionTime)
            {
                currentAlpha += Time.deltaTime * halfTransitionTime;
                transitionImg.color = new Color(transitionImg.color.r, transitionImg.color.g, transitionImg.color.b, currentAlpha);
            }
            else if (transitionTimer > halfTransitionTime)
            {
                if (!hasAddedIndex)
                {
                    playlist[currentVideoIndex].video.SetActive(false);
                    currentVideoIndex++;
                    hasAddedIndex = true;
                }
                currentAlpha -= Time.deltaTime * halfTransitionTime;
                transitionImg.color = new Color(transitionImg.color.r, transitionImg.color.g, transitionImg.color.b, currentAlpha);
            }
            if (transitionTimer > transitionTime + 1)
            {
                isTransitioning = false;
                if (currentVideoIndex < playlist.Count)
                {
                    StartCoroutine(PlayVideo(playlist[currentVideoIndex].time));
                }
                else
                {
                    OnAllVideoDone.Invoke();
                    StartCoroutine(DisableObj());
                }
            }
        }
        else if (currentVideoIndex < playlist.Count)
        {
            if (playlist[currentVideoIndex].isSkippable)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    playlist[currentVideoIndex].videoPlayer.Stop();
                    isTransitioning = true;
                    transitionTimer = 0f;
                    currentAlpha = 0f;
                    hasAddedIndex = false;
                }
            }
        }

        if (!hasExecuteVideoStart)
        {
            Invoke("ExecuteVideoStart", 2f);
        }
    }

    IEnumerator PlayVideo(float delayTime)
    {
        playlist[currentVideoIndex].video.SetActive(true);
        playlist[currentVideoIndex].videoPlayer.Play();
        yield return new WaitForSeconds(delayTime);
        isTransitioning = true;
        transitionTimer = 0f;
        currentAlpha = 0f;
        hasAddedIndex = false;
    }

    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(transitionTime);
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HasPlayed", 0);
        PlayerPrefs.Save();
    }

    private void ExecuteVideoStart()
    {
        if (!hasExecuteVideoStart)
        {
            OnVideoStart.Invoke();
            hasExecuteVideoStart = true;
        }
    }
}
