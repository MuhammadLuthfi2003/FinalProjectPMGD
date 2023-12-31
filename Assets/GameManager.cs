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
    public ScriptableInteger shield;

    [Header("Jumlah Kayu Bakar yang harus dikumpulkan")]
    public int kayubakarGoal = 5;

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
        ResetScriptableValues();
        kayubakar.defaultValue = kayubakarGoal;
        kayubakar.value = kayubakar.defaultValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (kayubakar.value == 0 && isPlaying)
        {
            OnObjectiveComplete.Invoke();
            isPlaying = false;
        }
    }

    public void ResetScriptableValues()
    {
        if (score.resetOnEnable)
        {
            score.value = score.defaultValue;
        }

        if (flag.resetOnEnable)
        {
            flag.value = flag.defaultValue;
        }

        if (healthScriptable.resetOnEnable)
        {
            healthScriptable.value = healthScriptable.defaultValue;
        }

        if (shield.resetOnEnable)
        {
            shield.value = shield.defaultValue;
        }
    }
}
