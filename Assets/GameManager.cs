using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public bool isPlaying = false;
    public ScriptableInteger score;
    public ScriptableInteger flag;
    public ScriptableInteger healthScriptable;
    public ScriptableInteger kayubakar;

    [Header("Event objective reaches 0")]
    public UnityEvent OnObjectiveComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        kayubakar.value = kayubakar.defaultValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (kayubakar.value == 0)
        {
            OnObjectiveComplete.Invoke();
        }
    }
}
