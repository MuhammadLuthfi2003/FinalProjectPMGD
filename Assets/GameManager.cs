using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public bool isPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // this will persist across scenes
        }
        else
        {
            Destroy(this.gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
